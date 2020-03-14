# NetClock

Herramientas de dotnet ef ahora se instalan global

[https://www.nuget.org/packages/dotnet-ef/3.1.0](https://www.nuget.org/packages/dotnet-ef/3.1.0)

`dotnet tool install -g dotnet-ef`

## Migración

```bash
cd src/WebApi

dotnet ef migrations add Initial \
    -p ../Infrastructure/Infrastructure.csproj \
    -c ApplicationDbContext \
    -o ../Infrastructure/Persistence/Migrations/ApplicationMigrations

dotnet ef database update -c ApplicationDbContext
```
