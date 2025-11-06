using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Entities.Constants;

namespace Parachute;

public class ConfigGen : BasePluginConfig
{
    [JsonPropertyName("Enabled")] public bool Enabled { get; set; } = true;
    [JsonPropertyName("DecreaseVec")] public float DecreaseVec { get; set; } = 50;
    [JsonPropertyName("Linear")] public bool Linear { get; set; } = true;
    [JsonPropertyName("FallSpeed")] public float FallSpeed { get; set; } = 100;
    [JsonPropertyName("AccessFlag")] public string AccessFlag { get; set; } = "";
    [JsonPropertyName("TeleportTicks")] public int TeleportTicks { get; set; } = 300;
    [JsonPropertyName("ParachuteModelEnabled")] public bool ParachuteModelEnabled { get; set; } = true;
    [JsonPropertyName("ParachuteModel")] public string ParachuteModel { get; set; } = "models/props_survival/parachute/chute.vmdl";
    [JsonPropertyName("DisableWhenCarryingHostage")] public bool DisableWhenCarryingHostage { get; set; } = false;
}

[MinimumApiVersion(179)]
public class Parachute : BasePlugin, IPluginConfig<ConfigGen>
{
    public override string ModuleName => "CS2 Parachute";
    public override string ModuleAuthor => "ddbmaster";
    public override string ModuleVersion => "1.6.1";

    public ConfigGen Config { get; set; } = null!;
    public void OnConfigParsed(ConfigGen config) => Config = config;

    private readonly Dictionary<int, bool> bUsingPara = new();
    private readonly Dictionary<int, int> gParaTicks = new();
    private readonly Dictionary<int, CBaseEntity?> gParaModel = new();

    public override void Load(bool hotReload)
    {
        if (!Config.Enabled)
        {
            Console.WriteLine("[Parachute] Plugin not enabled!");
            return;
        }

        if (hotReload)
        {
            foreach (var p in Utilities.GetPlayers())
            {
                if (p is null || !p.IsValid || p.IsBot) continue;
                int idx = (int)p.Index;
                bUsingPara[idx] = false;
                gParaTicks[idx] = 0;
                gParaModel[idx] = null;
            }
        }

        if (Config.ParachuteModelEnabled)
        {
            RegisterListener<Listeners.OnServerPrecacheResources>(manifest =>
            {
                manifest.AddResource(Config.ParachuteModel);
            });
        }

        RegisterEventHandler<EventPlayerConnectFull>((@event, _) =>
        {
            var player = @event.Userid;
            if (player == null || player.IsBot || !player.IsValid)
                return HookResult.Continue;

            int idx = (int)player.Index;
            bUsingPara[idx] = false;
            gParaTicks[idx] = 0;
            gParaModel[idx] = null;
            return HookResult.Continue;
        });

        RegisterEventHandler<EventPlayerDisconnect>((@event, _) =>
        {
            var player = @event.Userid;
            if (player == null || player.IsBot || !player.IsValid)
                return HookResult.Continue;

            int idx = (int)player.Index;

            if (bUsingPara.TryGetValue(idx, out var usingPara) && usingPara)
            {
                bUsingPara[idx] = false;
                StopPara(player);
            }

            bUsingPara.Remove(idx);
            gParaTicks.Remove(idx);

            if (gParaModel.TryGetValue(idx, out var ent) && ent is not null && ent.IsValid)
                ent.Remove();

            gParaModel.Remove(idx);
            return HookResult.Continue;
        });

        RegisterListener<Listeners.OnTick>(() =>
        {
            foreach (var player in Utilities.GetPlayers())
            {
                if (player is null || !player.IsValid || player.IsBot)
                    continue;

                int idx = (int)player.Index;
                if (!bUsingPara.ContainsKey(idx)) bUsingPara[idx] = false;
                if (!gParaTicks.ContainsKey(idx)) gParaTicks[idx] = 0;
                if (!gParaModel.ContainsKey(idx)) gParaModel[idx] = null;

                if (!(string.IsNullOrEmpty(Config.AccessFlag) || AdminManager.PlayerHasPermissions(player, Config.AccessFlag)))
                    continue;

                var pawn = player.PlayerPawn.Value;
                if (pawn == null || !pawn.IsValid || !player.PawnIsAlive)
                {
                    if (bUsingPara.TryGetValue(idx, out var wasUsing) && wasUsing)
                    {
                        bUsingPara[idx] = false;
                        StopPara(player);
                    }
                    continue;
                }

                // Stop if on ground
                if (pawn.OnGroundLastTick)
                {
                    if (bUsingPara.TryGetValue(idx, out var isUsingGround) && isUsingGround)
                    {
                        bUsingPara[idx] = false;
                        StopPara(player);
                    }
                    continue;
                }

                // Hostage check
                if (Config.DisableWhenCarryingHostage)
                {
                    try
                    {
                        if (pawn.HostageServices?.CarriedHostageProp.Value != null)
                        {
                            if (bUsingPara.TryGetValue(idx, out var wasUsingHost) && wasUsingHost)
                            {
                                bUsingPara[idx] = false;
                                StopPara(player);
                            }
                            continue;
                        }
                    }
                    catch { }
                }

                bool usePressed = (player.Buttons & PlayerButtons.Use) != 0;
                if (usePressed)
                    StartPara(player);
                else if (bUsingPara.TryGetValue(idx, out var usingPara) && usingPara)
                {
                    bUsingPara[idx] = false;
                    StopPara(player);
                }
            }
        });

        RegisterEventHandler<EventPlayerDeath>((@event, _) =>
        {
            var player = @event.Userid;
            if (player == null || !player.IsValid)
                return HookResult.Continue;

            int idx = (int)player.Index;
            if (bUsingPara.TryGetValue(idx, out var usingPara) && usingPara)
            {
                bUsingPara[idx] = false;
                StopPara(player);
            }
            return HookResult.Continue;
        });
    }

    private void StopPara(CCSPlayerController player)
    {
        if (player == null || !player.IsValid) return;
        int idx = (int)player.Index;

        try { player.GravityScale = 1.0f; } catch { }

        if (gParaTicks.ContainsKey(idx))
            gParaTicks[idx] = 0;

        if (gParaModel.TryGetValue(idx, out var ent) && ent is not null)
        {
            try
            {
                if (ent.IsValid) ent.Remove();
            }
            catch { }
            gParaModel[idx] = null;
        }
    }

    private void StartPara(CCSPlayerController player)
    {
        if (player == null || !player.IsValid) return;
        int idx = (int)player.Index;

        if (!bUsingPara.ContainsKey(idx)) bUsingPara[idx] = false;
        if (!gParaTicks.ContainsKey(idx)) gParaTicks[idx] = 0;
        if (!gParaModel.ContainsKey(idx)) gParaModel[idx] = null;

        if (!bUsingPara[idx])
        {
            bUsingPara[idx] = true;
            try { player.GravityScale = 0.1f; } catch { }

            if (Config.ParachuteModelEnabled)
            {
                var entity = Utilities.CreateEntityByName<CDynamicProp>("prop_dynamic_override");
                if (entity != null && entity.IsValid)
                {
                    try
                    {
                        entity.MoveType = MoveType_t.MOVETYPE_NOCLIP;
                        entity.Collision.CollisionGroup = (byte)CollisionGroup.COLLISION_GROUP_NONE;
                        entity.Collision.CollisionAttribute.CollisionGroup = (byte)CollisionGroup.COLLISION_GROUP_NONE;
                        entity.DispatchSpawn();
                        entity.SetModel(Config.ParachuteModel);
                    }
                    catch { }
                    gParaModel[idx] = entity;
                }
            }
        }

        var pawn = player.PlayerPawn.Value;
        if (pawn == null || !pawn.IsValid) return;

        var velocity = pawn.AbsVelocity;
        if (velocity == null) return;

        float fallspeed = Config.FallSpeed * -1.0f;
        bool isFallSpeed = velocity.Z >= fallspeed;

        if (velocity.Z < 0.0f)
        {
            if ((isFallSpeed && Config.Linear) || Math.Abs(Config.DecreaseVec) <= float.Epsilon)
                velocity.Z = fallspeed;
            else
                velocity.Z += Config.DecreaseVec;

            if (gParaTicks.TryGetValue(idx, out var ticks))
            {
                var position = pawn.AbsOrigin!;
                var angle = pawn.AbsRotation!;

                if (ticks > Config.TeleportTicks)
                {
                    try
                    {
                        // ⚙️ FIX: Teleport jetzt korrekt auf dem Pawn!
                        pawn.Teleport(position, angle, velocity);
                    }
                    catch { }
                    gParaTicks[idx] = 0;
                }
                else
                {
                    gParaTicks[idx] = ticks + 1;
                }

                if (gParaModel.TryGetValue(idx, out var modelEnt) && modelEnt is not null && modelEnt.IsValid)
                {
                    try { modelEnt.Teleport(position, angle, velocity); } catch { }
                }
            }
        }
    }
}
