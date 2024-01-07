# RichillCapital.Infrastructure

## Run Tests

```powershell
dotnet test --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=../../coverage/lcov.info -- MSTest.Parallelize.Workers=5
```

## Packaging

```powershell
dotnet pack -o ./dist -c Release
```

## Push to NuGet

```powershell
dotnet nuget push .\dist\RichillCapital.Infrastructure.1.0.0.nupkg -k <api-key> -s https://api.nuget.org/v3/index.json
```