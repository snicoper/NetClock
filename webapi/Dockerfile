FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Staging

# Copy everything else and build
COPY . ./
WORKDIR /app
RUN dotnet publish -c Debug -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=base /app/out ./
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "NetClock.WebApi.dll"]
