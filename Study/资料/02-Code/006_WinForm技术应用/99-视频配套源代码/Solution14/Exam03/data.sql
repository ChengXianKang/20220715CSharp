use master
go
create database Test
go
use Test
go
create table Product
(
	ProductID int primary key identity(1,1), --商品编号
	ProductName varchar(200) not null, --商品名称
	IsUp char(2) not null, --是否上架
	UnitPrice int not null, --商品单价
	Remark varchar(500), --商品备注
)

create table Sales
(
	SalesID int primary key identity(1,1), --销售编号
	ProductID int, --销售商品编号
	SalesMan varchar(50), --销售员
	SalesCount int, --销售数量
	SalesDate smalldatetime, --销售时间
)

insert into Product(ProductName,IsUp,UnitPrice,Remark)
values('韩版条纹连衣裙','是','89','还差货')
insert into Product(ProductName,IsUp,UnitPrice,Remark)
values('新款潮男翻领POLO衬衫','否','55','下星期上货')
insert into Product(ProductName,IsUp,UnitPrice,Remark)
values('TCL55英寸4k超清电视','是','2888','性价比很高')

insert into Sales(ProductID,SalesMan,SalesCount,SalesDate) values(1,'宋钟基',2,GETDATE())
insert into Sales(ProductID,SalesMan,SalesCount,SalesDate) values(1,'宋钟基',3,GETDATE())

select * from Product
select * from Sales
select * from Sales left join Product on Sales.ProductID = Product.ProductID where Sales.ProductID=



