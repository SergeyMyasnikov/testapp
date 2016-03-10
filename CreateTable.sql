USE master
GO

IF EXISTS(SELECT * from sys.databases WHERE name = 'Clients')
BEGIN
	DROP DATABASE Clients
END

CREATE DATABASE Clients
GO

USE Clients;
GO

CREATE TABLE dbo.Clients
   (id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	name nvarchar(50) NULL,
	payment float NULL,
	creation_date date NULL)
GO