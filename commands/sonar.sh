#!/bin/bash

# podman run -d --name sonarqube -e SONAR_ES_BOOTSTRAP_CHECKS_DISABLE=true -p 9000:9000 sonarqube:latest
# Requiere dotnet-sonarscanner.
# dotnet tool install --global dotnet-sonarscanner

APP_ROOT="$(dirname "$(dirname "$(readlink -fm "$0")")")"

source $APP_ROOT/commands/_variables.sh

cd $APP_ROOT/webapi

dotnet sonarscanner begin /k:"6bd9325c-d346-48b5-ab33-4993b61d1567" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="167dcee8c13d19adbce492cd4d359a38ec0a70fe"
dotnet build NetClock.sln
dotnet sonarscanner end /d:sonar.login="167dcee8c13d19adbce492cd4d359a38ec0a70fe"

cd $APP_ROOT/webapp

sonar-scanner \
  -Dsonar.projectKey=NetClockApp \
  -Dsonar.sources=. \
  -Dsonar.host.url=http://localhost:9000 \
  -Dsonar.login=167dcee8c13d19adbce492cd4d359a38ec0a70fe
