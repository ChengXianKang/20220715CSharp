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
values('技术部','......','......')
insert into OAsys_Dept(D_name,D_Manager,D_Remars)
values('产品部','......','......')
insert into OAsys_Dept(D_name,D_Manager,D_Remars)
values('人事部','......','......')

insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('刘备','男',1,'主管','liubei@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('关羽','男',2,'经理','guanyu@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('张飞','男',3,'副总','zhangfei@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('大乔','女',1,'助理','daqiao@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('小乔','女',2,'工程师','xiaoqiao@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('赵云','男',2,'工程师','zhaoyun@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('貂蝉','女',1,'经理','diaocan@163.com')
insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
values('典韦','男',3,'经理','dianwei@163.com')