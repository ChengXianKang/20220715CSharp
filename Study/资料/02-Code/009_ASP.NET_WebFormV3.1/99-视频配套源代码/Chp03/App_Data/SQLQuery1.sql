--��Ʒ���
create table GoodType
(
	TypeId int primary key, --��ţ�����
	ParentId int not null, --������
	TypeName varchar(50) not null
)
insert into GoodType(TypeId,ParentId,TypeName) values(1,0,'����')
insert into GoodType(TypeId,ParentId,TypeName) values(2,0,'����')
	--�����¼�
	insert into GoodType(TypeId,ParentId,TypeName) values(3,1,'����')		
	insert into GoodType(TypeId,ParentId,TypeName) values(4,1,'�յ�')
	insert into GoodType(TypeId,ParentId,TypeName) values(5,1,'ϴ�»�')
	--�����¼�
	insert into GoodType(TypeId,ParentId,TypeName) values(6,2,'����')
	insert into GoodType(TypeId,ParentId,TypeName) values(7,2,'�ֻ�')	
		--�����¼�
		insert into GoodType(TypeId,ParentId,TypeName) values(8,6,'̨ʽ')
		insert into GoodType(TypeId,ParentId,TypeName) values(9,6,'�ʼǱ�')
		insert into GoodType(TypeId,ParentId,TypeName) values(10,6,'һ���')