IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'cby')
BEGIN
  CREATE DATABASE cby;
END
