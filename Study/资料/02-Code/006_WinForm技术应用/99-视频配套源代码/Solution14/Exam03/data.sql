use master
go
create database Test
go
use Test
go
create table Product
(
	ProductID int primary key identity(1,1), --��Ʒ���
	ProductName varchar(200) not null, --��Ʒ����
	IsUp char(2) not null, --�Ƿ��ϼ�
	UnitPrice int not null, --��Ʒ����
	Remark varchar(500), --��Ʒ��ע
)

create table Sales
(
	SalesID int primary key identity(1,1), --���۱��
	ProductID int, --������Ʒ���
	SalesMan varchar(50), --����Ա
	SalesCount int, --��������
	SalesDate smalldatetime, --����ʱ��
)

insert into Product(ProductName,IsUp,UnitPrice,Remark)
values('������������ȹ','��','89','�����')
insert into Product(ProductName,IsUp,UnitPrice,Remark)
values('�¿�з���POLO����','��','55','�������ϻ�')
insert into Product(ProductName,IsUp,UnitPrice,Remark)
values('TCL55Ӣ��4k�������','��','2888','�Լ۱Ⱥܸ�')

insert into Sales(ProductID,SalesMan,SalesCount,SalesDate) values(1,'���ӻ�',2,GETDATE())
insert into Sales(ProductID,SalesMan,SalesCount,SalesDate) values(1,'���ӻ�',3,GETDATE())

select * from Product
select * from Sales
select * from Sales left join Product on Sales.ProductID = Product.ProductID where Sales.ProductID=



