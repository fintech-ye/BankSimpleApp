docker network create bank-network

docker volume create mssql_volume

docker run -d --network=bank-network --name mssql-server -p 1433:1433 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=CBY@123456" -e "MSSQL_PID=Evaluation"  -v mssql_volume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2022-latest

docker cp ./db_init/ mssql-server:/tmp/

docker exec -it mssql-server /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P CBY@123456 -i /tmp/db_init/db_create.sql -i /tmp/db_init/db_insert.sql

docker run -d --network=bank-network --name=bank-app -p 8080:80 -e DB_SERVER="mssql-server,1433" -e DB_NAME="cby" -e DB_UID="sa" -e DB_PASSWD="CBY@123456" maghbari/bank-simple-app

#/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P CBY@123456 -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'cby') BEGIN CREATE DATABASE cby; END"
#/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -d cby -Q "IF OBJECT_ID(N'dbo.accounts', N'U') IS NULL BEGIN CREATE TABLE [dbo].[accounts] ([BIC] VARCHAR (50) NOT NULL,[account_name] VARCHAR (50) NOT NULL,[balance]  INT CONSTRAINT [DEFAULT_accounts_balance] DEFAULT ((0)) NOT NULL, [bank_code] CHAR (10) NULL, [currency]     CHAR (10)    CONSTRAINT [DEFAULT_accounts_currency] DEFAULT ((886)) NOT NULL, CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED ([BIC] ASC)); END"
#/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -d cby -Q  "INSERT INTO dbo.accounts (BIC, account_name, balance, bank_code, currency) VALUES ('CBYEYESA', 'Central Bank of Yemen', 1000000000, 'CBYE', '886'), ('KRMBYESA', 'Kuraimi Islamic Bank', 50000000, 'KRMB', '886');"
#/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD --i db_init.sql
