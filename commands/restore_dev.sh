#!/bin/bash

APP_ROOT="$(dirname "$(dirname "$(readlink -fm "$0")")")"

source $APP_ROOT/commands/_variables.sh

cd $APP_ROOT

# Reinstalar la base de datos, requiere ~/.pgpass
read -p "${GREEN}¿Restaurar la base de datos? ${YELLOW}(y/[N])${RESTORE} " yn
if [[ "$yn" == "y" || "$yn" == "Y" ]]
then
  dropdb --if -U postgres $DATABASE_NAME
  echo "${MAGENTA}Eliminada base de datos ${YELLOW}${DATABASE_NAME}${RESTORE}"

  # Rehacer migraciones? solo en etapa de desarrollo.
  read -p "${GREEN}¿Eliminar migraciones? ${YELLOW}(y/[N])${RESTORE} " yn
  if [[ "$yn" == "y" || "$yn" == "Y" ]]
  then
    rm -rf webapi/src/Infrastructure/Persistence/Migrations

    cd $APP_ROOT/webapi/src/WebApi

    dotnet ef migrations add InitialApplication \
      -p ../Infrastructure/Infrastructure.csproj \
      -c ApplicationDbContext \
      -o ../Infrastructure/Persistence/Migrations/Application
  fi

  cd $APP_ROOT/webapi/src/WebApi
  dotnet run /seed

  cd $APP_ROOT
fi
