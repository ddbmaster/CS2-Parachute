🇩🇪 Installation

Plugin herunterladen
Lade die neueste Version des Plugins herunter (z. B. Parachute_v1.6.1.zip oder aus dem Git-Repository).
Hier das neue:  [releases](https://github.com/ddbmaster/CS2-Parachute/releases) 
Dateien entpacken
Entpacke den Inhalt in deinen Counter-Strike 2-Serverordner:

csgo/
└── addons/
    └── counterstrikesharp/
        └── plugins/
            └── Parachute/
                Parachute.cs
                Parachute.json


Server neu starten oder Plugin neu laden
Starte den Server neu
oder
Verwende im Server-Konsolenfenster:
css_reloadplugin Parachute

Konfiguration anpassen
Die Konfigurationsdatei befindet sich unter:
csgo/cfg/plugins/Parachute/Parachute.json

Beispiel:

{
  "Enabled": true,
  "DecreaseVec": 50,
  "Linear": true,
  "FallSpeed": 100,
  "AccessFlag": "",
  "TeleportTicks": 300,
  "ParachuteModelEnabled": true,
  "ParachuteModel": "models/props_survival/parachute/chute.vmdl",
  "DisableWhenCarryingHostage": false
}

Enabled → Aktiviert oder deaktiviert das Plugin
DecreaseVec / FallSpeed → Steuert die Sinkgeschwindigkeit
AccessFlag → Optionales Admin-Recht für Nutzung (@css/admin etc.)
ParachuteModelEnabled → Fallschirm-Modell aktivieren/deaktivieren
DisableWhenCarryingHostage → Deaktiviert Fallschirm bei Geisel-Missionen

Testen
Verbinde dich mit dem Server, springe aus einer Höhe und halte E (Benutzen) gedrückt,
um den Fallschirm zu aktivieren.

Optional
Du kannst das Modell in der Config anpassen oder deaktivieren, wenn du eigene Models nutzt.
Standardmodell:
models/props_survival/parachute/chute.vmdl




# CS2-Parachute

Parachute function when you keep pressed E on the air. 

### Requirements
* [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp/) (Version="1.0.340" or higher)

### Installation

Drag and drop from [releases](https://github.com/Franc1sco/CS2-Parachute/releases) to game/csgo/addons/counterstrikesharp/plugins

### Configuration

Configure the file parachute.json generated on addons/counterstrikesharp/configs/plugins/Parachute
```json
{
  "Enabled": true,
  "DecreaseVec": 50,
  "Linear": true,
  "FallSpeed": 100,
  "AccessFlag": "@css/vip",
  "TeleportTicks": 300,
  "ParachuteModelEnabled": true,
  "ConfigVersion": 1
}
```
* Enable - Enable or disable the plugin.
* DecreaseVec - 0: dont use Realistic velocity-decrease - x: sets the velocity-decrease.
* Linear - 0: disables linear fallspeed - 1: enables it
* FallSpeed - speed of the fall when you use the parachute
* TeleportTicks - 300: ticks until perform a teleport (for prevent console spam).
* ParachuteModelEnabled - true: Add a parachute model while using it.
* AccessFlag - access required for can use parachuse, leave blank "" for public access.
