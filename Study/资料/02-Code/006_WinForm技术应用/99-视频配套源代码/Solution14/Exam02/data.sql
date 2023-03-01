use master
go
create database Test
go
use Test
go
create table UserInfo
(
	UserID int primary key identity(1,1), --�û����
	UserName varchar(20) not null, --�û���
	Passwords varchar(20) not null --����
)

create table Category
(
	CategoryID int primary key identity(1,1), --�����
	CategoryName varchar(20) not null --�������
)

create table Product
(
	ProductID int primary key identity(1,1), --��Ʒ���
	ProductName varchar(200) not null, --��Ʒ����
	IsUp char(2) not null, --�Ƿ��ϼ�
	UnitPrice int not null, --��Ʒ����
	Remark varchar(500), --��Ʒ��ע
	CategoryID int references Category(CategoryID) --���������
)

insert into UserInfo(UserName,Passwords) values('admin','123456')
insert into Category(CategoryName) values('��װ')
insert into Category(CategoryName) values('Ůװ')
insert into Category(CategoryName) values('�ҵ�')
select * from Category
insert into Product(ProductName,IsUp,UnitPrice,Remark,CategoryID)
values('������������ȹ','��','89','�����',2)
insert into Product(ProductName,IsUp,UnitPrice,Remark,CategoryID)
values('�¿�з���POLO����','��','55','�������ϻ�',1)
insert into Product(ProductName,IsUp,UnitPrice,Remark,CategoryID)
values('TCL55Ӣ��4k�������','��','2888','�Լ۱Ⱥܸ�',3)


select * from UserInfo
--select * from Product left join Category on Product.CategoryID = Category.CategoryID where 1=1 

