--商品类别
create table GoodType
(
	TypeId int primary key, --编号，主键
	ParentId int not null, --父类编号
	TypeName varchar(50) not null
)
insert into GoodType(TypeId,ParentId,TypeName) values(1,0,'电器')
insert into GoodType(TypeId,ParentId,TypeName) values(2,0,'数码')
	--电器下级
	insert into GoodType(TypeId,ParentId,TypeName) values(3,1,'冰箱')		
	insert into GoodType(TypeId,ParentId,TypeName) values(4,1,'空调')
	insert into GoodType(TypeId,ParentId,TypeName) values(5,1,'洗衣机')
	--数码下级
	insert into GoodType(TypeId,ParentId,TypeName) values(6,2,'电脑')
	insert into GoodType(TypeId,ParentId,TypeName) values(7,2,'手机')	
		--电脑下级
		insert into GoodType(TypeId,ParentId,TypeName) values(8,6,'台式')
		insert into GoodType(TypeId,ParentId,TypeName) values(9,6,'笔记本')
		insert into GoodType(TypeId,ParentId,TypeName) values(10,6,'一体机')