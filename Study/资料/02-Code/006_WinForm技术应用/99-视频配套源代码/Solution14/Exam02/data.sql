use master
go
create database Test
go
use Test
go
create table UserInfo
(
	UserID int primary key identity(1,1), --用户编号
	UserName varchar(20) not null, --用户名
	Passwords varchar(20) not null --密码
)

create table Category
(
	CategoryID int primary key identity(1,1), --类别编号
	CategoryName varchar(20) not null --类别名称
)

create table Product
(
	ProductID int primary key identity(1,1), --商品编号
	ProductName varchar(200) not null, --商品名称
	IsUp char(2) not null, --是否上架
	UnitPrice int not null, --商品单价
	Remark varchar(500), --商品备注
	CategoryID int references Category(CategoryID) --所属类别编号
)

insert into UserInfo(UserName,Passwords) values('admin','123456')
insert into Category(CategoryName) values('男装')
insert into Category(CategoryName) values('女装')
insert into Category(CategoryName) values('家电')
select * from Category
insert into Product(ProductName,IsUp,UnitPrice,Remark,CategoryID)
values('韩版条纹连衣裙','是','89','还差货',2)
insert into Product(ProductName,IsUp,UnitPrice,Remark,CategoryID)
values('新款潮男翻领POLO衬衫','否','55','下星期上货',1)
insert into Product(ProductName,IsUp,UnitPrice,Remark,CategoryID)
values('TCL55英寸4k超清电视','是','2888','性价比很高',3)


select * from UserInfo
--select * from Product left join Category on Product.CategoryID = Category.CategoryID where 1=1 

