# NetClock

Herramientas de dotnet ef ahora se instalan global

https://www.nuget.org/packages/dotnet-ef/3.0.0

`dotnet tool install -g dotnet-ef`

```bash
dotnet ef migrations add init --project ../Infrastructure/Infrastructure.csproj --context ApplicationDbContext -o ../Infrastructure/Persistence/Migrations
```
