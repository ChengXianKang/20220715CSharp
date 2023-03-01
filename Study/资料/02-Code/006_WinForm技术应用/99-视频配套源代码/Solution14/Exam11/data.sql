use master
go
create database ClassManagement
go
use ClassManagement
go

create table y_Class
(
	ClassId int primary key identity(1,1), --�������Զ�����
	ClassName nvarchar(64) not null
)
create table y_Student
(
	StudentId int primary key identity(1,1), --�������Զ�����
	ClassId int not null references y_Class(ClassId),
	Name nvarchar(64) not null,
	Gender char(2) not null,
	Age int not null
)

select * from y_Class
select * from y_Student

insert into y_Class(ClassName) values('һ�꼶һ��')
insert into y_Class(ClassName) values('һ�꼶����')
insert into y_Student(ClassId,Name,Gender,Age)
values(1,'����','��',18)
insert into y_Student(ClassId,Name,Gender,Age)
values(2,'����','Ů',17)
insert into y_Student(ClassId,Name,Gender,Age)
values(2,'����','Ů',17)
insert into y_Student(ClassId,Name,Gender,Age)
values(1,'����','��',17)


