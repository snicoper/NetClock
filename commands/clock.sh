#!/bin/bash

APP_ROOT="$(dirname "$(dirname "$(readlink -fm "$0")")")"

cd $APP_ROOT

cloc ./ \
  --exclude_dir node_modules,.vscode \
  --exclude_ext json,xml,sql
