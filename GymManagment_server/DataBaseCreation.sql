namespace GymManagment_server
{
    public class DataBaseCreation
    {
    Use master
Go
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'GymManagment_server')
BEGIN
    DROP DATABASE GymManagment_server;
END
Go
Create Database GymManagment_server
Go
Use GymManagment_server
Go

CREATE TABLE Users(
Username Nvarchar(100),
[Password] Nvarchar(100) PRIMARY KEY,
Age int,
Gender nvarchar(100),
--GymId int Foreign key,
);


CREATE TABLE GYMS(
[Name] nvarchar(100),
[GymId] int PRIMARY KEY IDENTITY,
[Level] int,);


CREATE TABLE TRAINERS(
[Name] nvarchar(100),
TrainerId int PRIMARY KEY IDENTITY,
NumOfClasses int,);


CREATE TABLE CLASSES(
[Name] nvarchar(100),
ClassId int PRIMARY KEY IDENTITY,
DIFFICULTY INT,);
    }
}
