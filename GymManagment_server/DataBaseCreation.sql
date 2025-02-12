
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
BirthDate date,
[Address] Nvarchar(100) Not null,
Difficulty Nvarchar(50) Not null,
GenderId int,
IsManager bit Not Null Default 0

);


Create table Gyms(
[Name] nvarchar(50),
[GymId] int PRIMARY KEY IDENTITY,
[Level] int,
[Address] Nvarchar(100) Not null,
Price int Not null,
PhoneNumber Nvarchar(15) Not null,
GymManager int Foreign Key References Users(Id)
);


Create table Trainers(
[Name] nvarchar(50),
TrainerId int PRIMARY KEY IDENTITY,
[Description] Nvarchar(100) Not null,
GymId int  Foreign Key References Gyms(GymId) Not null
);


Create table Classes(
[Name] nvarchar(50),
ClassId int PRIMARY KEY IDENTITY,
Difficulty INT,
[Description] Nvarchar(100) Not null,
GymId int  Foreign Key References Gyms(GymId) Not null
)

Create table Comments(
CommentId int PRIMARY KEY IDENTITY,
UserId int Foreign Key References Users(Id) Not null,
[Rank] int Not null,
[Description] Nvarchar(150) Not null);
    
CREATE LOGIN [TaskAdminLogin] WITH PASSWORD = 'Petel123';
Go

CREATE USER [TaskAdminUser] FOR LOGIN [TaskAdminLogin];
Go

ALTER ROLE db_owner ADD MEMBER [TaskAdminUser];
Go

INSERT INTO Users Values (N'Itamar1',N'Itamar12@gmail.com',N'Itamar123','10-08-2007',N'Golda Meir 5 Hod Hasharon',N'Begginer',2,1)
INSERT INTO Users Values(N'Admin',N'o@gmail.com',N'123','10-08-2007',N'Golda Meir 5 Hod Hasharon',N'Expert',2,2)
INSERT INTO Gyms Values(N'Itamarfittnes',1,N'Golda Meir 5 Hod Hasharon',150,N'054131478',1)
INSERT INTO Gyms Values(N'Profit',2,N'Golda Meir 7 Hod Hasharon',200,N'054931278',2)
INSERT INTO GYMS VALUES (N'Itamarfit',3,N'Golda Meir 5 Hod Hasharon',100,N'0504847514')
insert into TRAINERS values(N'Ben',N'Ben is an excellent triner that specilice in yoga classes')
insert into CLASSES values(N'Zumba',2,N'Zumba class is an intermidiate class that is a lot of fun')
SELECT*FROM Users

--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=GymManagment_server;User ID=TaskAdminLogin;Password=Petel123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BenDBContext -DataAnnotations -force