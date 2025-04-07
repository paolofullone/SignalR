/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P Password123! -d master -i /tmp/init.sql -e
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P Password123! -d master -i /tmp/populate.sql -e