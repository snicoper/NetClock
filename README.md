# NetClock

Herramientas de dotnet ef ahora se instalan global

[https://www.nuget.org/packages/dotnet-ef/3.1.0](https://www.nuget.org/packages/dotnet-ef/3.1.0)

`dotnet tool install -g dotnet-ef`

```bash
dotnet ef migrations add init --project ../Infrastructure/Infrastructure.csproj --context ApplicationDbContext -o ../Infrastructure/Persistence/Migrations
```

```bash
dotnet run /seed
```

```bash
dotnet run
```

* ASP.net core 3.1
* Angular 9
* PostgresSQL
