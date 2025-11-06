🇩🇪 Installation
# CS2-Parachute
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
