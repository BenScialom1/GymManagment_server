
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

CREATE TABLE TRAINERCLASSES(
AssignmentId int PRIMARY KEY,
 Schedule NVARCHAR(100),
[TrainerId] int,
ClassId int,
FOREIGN KEY (TrainerId) REFERENCES TRAINERS(TrainerId),
FOREIGN KEY (ClassId) REFERENCES CLASSES(ClassId));
    
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