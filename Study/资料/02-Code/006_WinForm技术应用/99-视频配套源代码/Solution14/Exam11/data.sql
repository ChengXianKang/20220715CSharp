use master
go
create database ClassManagement
go
use ClassManagement
go

create table y_Class
(
	ClassId int primary key identity(1,1), --主键，自动增长
	ClassName nvarchar(64) not null
)
create table y_Student
(
	StudentId int primary key identity(1,1), --主键，自动增长
	ClassId int not null references y_Class(ClassId),
	Name nvarchar(64) not null,
	Gender char(2) not null,
	Age int not null
)

select * from y_Class
select * from y_Student

insert into y_Class(ClassName) values('一年级一班')
insert into y_Class(ClassName) values('一年级二班')
insert into y_Student(ClassId,Name,Gender,Age)
values(1,'张三','男',18)
insert into y_Student(ClassId,Name,Gender,Age)
values(2,'李四','女',17)
insert into y_Student(ClassId,Name,Gender,Age)
values(2,'王五','女',17)
insert into y_Student(ClassId,Name,Gender,Age)
values(1,'赵六','男',17)


