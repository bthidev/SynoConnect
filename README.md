# SynoConnect

An app we can use for control Synology DownloadStation

## Installation

You need .net core 5.0

```
dotnet restore
```

## Usage

```
dotnet run SynoConnect.Desktop/SynoConnect.Desktop.csproj
```

## publish 

```
 dotnet publish -c Release /p:DebugType=None /p:PublishTrimmed=true /p:TrimMode=link /p:SuppressTrimAnalysisWarnings=true /p:PublishSingleFile=true -r win-x64
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)
