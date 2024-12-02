
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

Create table Users(
Username Nvarchar(50),
Email Nvarchar(50) Unique Not null,
[Password] Nvarchar(50) ,
Id int PRIMARY KEY Identity,
Age int,
Adress Nvarchar(100) Not null,
Difficulty Nvarchar(50) Not null,
Gender nvarchar(50),
IsManager bit Not Null Default 0

);


Create table Gyms(
[Name] nvarchar(50),
[GymId] int PRIMARY KEY IDENTITY,
[Level] int,
Adress Nvarchar(100) Not null,
Price int Not null,
PhoneNumber Nvarchar(15) Not null,
IsManager bit Foreign Key References Users(IsManager)
);


Create table Trainers(
[Name] nvarchar(50),
TrainerId int PRIMARY KEY IDENTITY,
[Description] Nvarchar(100) Not null,
Class Nvarchar(50) Not null
);


Create table Classes(
[Name] nvarchar(50),
ClassId int PRIMARY KEY IDENTITY,
DIFFICULTY INT,
[Description] Nvarchar(100) Not null);

Create table Comments(
[Rank] int Not null,
[Description] Nvarchar(150) Not null);
    
CREATE LOGIN [TaskAdminLogin] WITH PASSWORD = 'Petel123';
Go

CREATE USER [TaskAdminUser] FOR LOGIN [TaskAdminLogin];
Go

ALTER ROLE db_owner ADD MEMBER [TaskAdminUser];
Go

INSERT INTO Users Values (N'Itamar1',N'Itamar123',17,N'Male')
INSERT INTO GYMS VALUES (N'Itamarfit',3)
insert into TRAINERS values(N'Ben',2)
insert into CLASSES values(N'Zumba',2)

--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=GymManagment_server;User ID=TaskAdminLogin;Password=Petel123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BenDBContext -DataAnnotations -force