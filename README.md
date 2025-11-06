<h2>🇩🇪 Installation</h2>

<h1>CS2-Parachute</h1>

<h3>Plugin herunterladen</h3>
<p>
Lade die neueste Version des Plugins herunter (z. B. <code>Parachute_v1.6.1.zip</code> oder direkt aus dem Git-Repository).<br>
Hier das neue Release: 
<a href="https://github.com/ddbmaster/CS2-Parachute/releases" target="_blank">➡️ Releases auf GitHub</a>
</p>

<h3>Dateien entpacken</h3>
<p>Entpacke den Inhalt in deinen Counter-Strike 2-Serverordner:</p>

<pre><code>csgo/
└── addons/
    └── counterstrikesharp/
        └── plugins/
            └── Parachute/
                Parachute.cs
                Parachute.json
</code></pre>

<h3>Server neu starten oder Plugin neu laden</h3>
<ul>
  <li>Starte den Server neu</li>
  <li>oder verwende im Server-Konsolenfenster:</li>
</ul>
<pre><code>css_reloadplugin Parachute
</code></pre>

<h3>Konfiguration anpassen</h3>
<p>Die Konfigurationsdatei befindet sich unter:</p>
<pre><code>csgo/cfg/plugins/Parachute/Parachute.json
</code></pre>

<p><strong>Beispiel:</strong></p>

<pre><code>{
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
</code></pre>

<ul>
  <li><b>Enabled</b> → Aktiviert oder deaktiviert das Plugin</li>
  <li><b>DecreaseVec / FallSpeed</b> → Steuert die Sinkgeschwindigkeit</li>
  <li><b>AccessFlag</b> → Optionales Admin-Recht für Nutzung (<code>@css/admin</code> etc.)</li>
  <li><b>ParachuteModelEnabled</b> → Fallschirm-Modell aktivieren/deaktivieren</li>
  <li><b>DisableWhenCarryingHostage</b> → Deaktiviert Fallschirm bei Geisel-Missionen</li>
</ul>

<h3>Testen</h3>
<p>
Verbinde dich mit dem Server, springe aus einer Höhe und halte <b>E (Benutzen)</b> gedrückt,<br>
um den Fallschirm zu aktivieren.
</p>

<h3>Optional</h3>
<p>
Du kannst das Modell in der Config anpassen oder deaktivieren, wenn du eigene Models nutzt.<br>
<strong>Standardmodell:</strong><br>
<code>models/props_survival/parachute/chute.vmdl</code>
</p>












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
