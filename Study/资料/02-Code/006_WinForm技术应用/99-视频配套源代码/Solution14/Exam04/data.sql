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


--��Ӳ�������
insert into Mysys_Student(S_name,S_sex,S_Pro,S_Area,S_Time1,S_type)
values('����','��','��������','����','2017-6-16',0)
insert into Mysys_Student(S_name,S_sex,S_Pro,S_Area,S_Time1,S_type)
values('����','��','������Ӫ��','����','2017-6-16',0)
insert into Mysys_Student(S_name,S_sex,S_Pro,S_Area,S_Time1,S_type)
values('�ŷ�','��','��ҵ��Ϣ��','����','2017-6-16',0)

select *,
case 
	when S_type=0 then '�ڶ�'
	else '��ҵ'
end S_type_cn
from Mysys_Student







select *,
case 
	when S_type = 0 then '�ڶ�' 
    when S_type = 1 then '��ҵ' 
end S_Type_cn
from Mysys_Student