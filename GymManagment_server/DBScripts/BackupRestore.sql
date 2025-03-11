Use master
Go

-- Create a login for the admin user
CREATE LOGIN [TaskAdminLogin] WITH PASSWORD = 'Petel123';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [TaskAdminLogin];
Go

Create Database GymManagment_server
go