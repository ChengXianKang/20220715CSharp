use master
go
create database Test
go
use Test
go
create table Mysys_Student  
(
	S_id int primary key identity(1,1),  
	S_name varchar(50) not null, 
	S_sex varchar(50) not null,
	S_Pro varchar(50) not null,
	S_Area varchar(50) not null,
	S_Time1 Datetime not null,
	S_type int not null,
	S_Time2 Datetime
)


--添加测试数据
insert into Mysys_Student(S_name,S_sex,S_Pro,S_Area,S_Time1,S_type)
values('刘备','男','计算机软件','湖北','2017-6-16',0)
insert into Mysys_Student(S_name,S_sex,S_Pro,S_Area,S_Time1,S_type)
values('关羽','男','互联网营销','湖南','2017-6-16',0)
insert into Mysys_Student(S_name,S_sex,S_Pro,S_Area,S_Time1,S_type)
values('张飞','男','企业信息化','河南','2017-6-16',0)

select *,
case 
	when S_type=0 then '在读'
	else '毕业'
end S_type_cn
from Mysys_Student







select *,
case 
	when S_type = 0 then '在读' 
    when S_type = 1 then '毕业' 
end S_Type_cn
from Mysys_Student