# NetClock

[CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)

Herramientas de dotnet ef ahora se instalan global

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

[Ver](commands/restore_dev.sh)
