#!/bin/bash
docker-compose down
docker-compose build

export WEBAPP_STORAGE_HOME=./data
echo ${WEBAPP_STORAGE_HOME}
docker-compose up