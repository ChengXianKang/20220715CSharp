use master
go
create database OAsys_db
go
use OAsys_db
go
create table OAsys_Dept
(
	D_id int primary key identity(1,1),
	D_name varchar(50) not null,
	D_Manager varchar(50) not null,
	D_Remars varchar(200) not null
)
create table OAsys_Employee
(
	E_id int primary key identity(1,1),
	E_name varchar(50) not null,
	E_Sex varchar(50) not null,
	E_Dept int not null references OAsys_Dept(D_id),
	E_Position varchar(50) not null,
	E_Email varchar(50) not null
)

insert into OAsys_Dept(D_name,D_Manager,D_Remars)
values('������','......','......')
insert into OAsys_Dept(D_name,D_Manager,D_Remars)
values('��Ʒ��','......','......')
insert into OAsys_Dept(D_name,D_Manager,D_Remars)
values('���²�','......','......')

insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('����','��',1,'����','liubei@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('����','��',2,'����','guanyu@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('�ŷ�','��',3,'����','zhangfei@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('����','Ů',1,'����','daqiao@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('С��','Ů',2,'����ʦ','xiaoqiao@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('����','��',2,'����ʦ','zhaoyun@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('����','Ů',1,'����','diaocan@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('��Τ','��',3,'����','dianwei@163.com')