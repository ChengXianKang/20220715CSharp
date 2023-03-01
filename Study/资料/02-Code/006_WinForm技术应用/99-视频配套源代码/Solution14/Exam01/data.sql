use master
go
create database Test
go
use Test
go
create table Dept
(
	DeptID int primary key identity(1,1),
	DeptName varchar(20) not null
)
create table Emp
(
	EmpID int primary key identity(1,1),
	EmpName varchar(20) not null,
	Sex char(2) not null,
	Age int not null,
	Tel varchar(20) not null,
	DeptID int references Dept(DeptID)
)
insert into Dept(DeptName) values('软件部')
insert into Dept(DeptName) values('策划部')
insert into Dept(DeptName) values('人事部')
insert into Dept(DeptName) values('市场部')
insert into Dept(DeptName) values('售后部')

insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('刘备','男',40,'132456789',2)
insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('关羽','男',35,'132456789',5)
insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('张飞','男',30,'132456789',1)
insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('貂蝉','女',26,'132456789',4)
insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('大桥','女',29,'132456789',3)

select * from Emp inner join Dept on Emp.DeptID = Dept.DeptID

select * from Dept


