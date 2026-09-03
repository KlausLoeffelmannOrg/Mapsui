# Mapsui Direct2D development solution

`Mapsui.Direct2D.slnx` isolates the Windows-only .NET 10 Direct2D spin-off from
Mapsui's existing .NET 8/9 solutions.

For local cross-repository development, check out these branches side by side:

```text
<root>\
  Mapsui\  (feature/mapsui-direct2d)
  WARP\    (feature/mapsui-direct2d)
```

Then build from this directory so `global.json` selects .NET SDK 10:

```powershell
dotnet restore Mapsui.Direct2D.slnx -p:TargetFrameworks=net10.0-windows10.0.22000.0
dotnet build Mapsui.Direct2D.slnx --no-restore -p:TargetFrameworks=net10.0-windows10.0.22000.0 -p:EnforceCodeStyleInBuild=false
```

The projects also detect `external\WARP`, which is the CI checkout layout. To
use another checkout, pass
`-p:WarpToolkitDirectXProject=<path-to-WarpToolkit.WinForms.DirectX.csproj>`.
Once a compatible package is published, pass
`-p:WarpToolkitDirectXVersion=<version>` instead.

The `TargetFrameworks` override keeps the current WARP multi-target projects on
their .NET 10 target when a .NET 11 SDK is not installed.
`EnforceCodeStyleInBuild=false` avoids treating pre-existing Mapsui formatting
diagnostics newly emitted by the .NET 10 SDK as compilation errors; formatting
for changed files remains enforced separately.
