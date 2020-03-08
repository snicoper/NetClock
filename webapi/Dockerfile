FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app

# Copy everything else and build
COPY . ./
WORKDIR /app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=base /app/out ./
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT ["dotnet", "NetClock.WebApi.dll"]
