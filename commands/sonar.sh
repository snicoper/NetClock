#!/bin/bash

# Requiere dotnet-sonarscanner.
# dotnet tool install --global dotnet-sonarscanner

APP_ROOT="$(dirname "$(dirname "$(readlink -fm "$0")")")"

source $APP_ROOT/commands/_variables.sh

cd $APP_ROOT/webapi

dotnet sonarscanner begin /k:"6bd9325c-d346-48b5-ab33-4993b61d1567" /d:sonar.host.url="http://sonar.local" /d:sonar.login="8ba567a58f7dff6a80e28e5167bb48a59e75b9dc"
dotnet build NetClock.sln
dotnet sonarscanner end /d:sonar.login="8ba567a58f7dff6a80e28e5167bb48a59e75b9dc"

cd $APP_ROOT/webapp

sonar-scanner \
  -Dsonar.projectKey=NetClockApp \
  -Dsonar.sources=. \
  -Dsonar.host.url=http://sonar.local \
  -Dsonar.login=8ba567a58f7dff6a80e28e5167bb48a59e75b9dc
