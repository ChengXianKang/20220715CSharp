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
insert into Dept(DeptName) values('�����')
insert into Dept(DeptName) values('�߻���')
insert into Dept(DeptName) values('���²�')
insert into Dept(DeptName) values('�г���')
insert into Dept(DeptName) values('�ۺ�')

insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('����','��',40,'132456789',2)
insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('����','��',35,'132456789',5)
insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('�ŷ�','��',30,'132456789',1)
insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('����','Ů',26,'132456789',4)
insert into Emp(EmpName,Sex,Age,Tel,DeptID) 
values('����','Ů',29,'132456789',3)

select * from Emp inner join Dept on Emp.DeptID = Dept.DeptID

select * from Dept


