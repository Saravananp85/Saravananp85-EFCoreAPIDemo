
Use Demo;

Create Table [dbo].[Roles]
(
	Id int Identity(1,1) Primary Key,
	Name varchar(50)
)

Insert into dbo.Roles (Name)
values('Admin'), ('Supervisor'), ('Agent');

Create Table [dbo].[Users]
(
	Id int identity(1,1) Primary Key,
	FirstName varchar(50) not null,
	LastName Varchar(50) null,
	EmailId varchar(250) not null unique,
	RoleId int not null Foreign Key references Roles(Id),
	IsActive bit not null,
	CreatedDate datetime not null, 
	ModifiedDate datetime
)

Insert into dbo.Users(FirstName, LastName, EmailId, RoleId, IsActive, CreatedDate)
Values 
('John', 'M', 'm.john@servion.com', 2, 1, getdate()),
('Raja', 'A', 'a.raja@servion.com', 1, 1, getdate());

select * from users

select * from UserSupervisorMapping

delete from UserSupervisorMapping

Create Table [dbo].[UserSupervisorMapping]
(
	Id int Identity(1,1) Primary Key,
	UserId int not null foreign key references Users(Id),
	SupervisorId int not null foreign key references Users(Id)
)