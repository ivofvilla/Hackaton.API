#!/bin/bash

# Espera até que o SQL Server esteja disponível
until /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P YourStrong!Passw0rd -Q 'SELECT 1'; do
  >&2 echo "SQL Server is starting up"
  sleep 1
done

# Executa o script SQL se necessário
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P YourStrong!Passw0rd -d master -i /init-db/database.sql

# Inicia a aplicação
exec dotnet Hackaton.Api.dll
