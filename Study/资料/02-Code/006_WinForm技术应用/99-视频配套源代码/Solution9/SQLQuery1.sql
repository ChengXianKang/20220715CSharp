create table Member
(
	MemberId int primary key identity(1,1),
	MemberAccount nvarchar(20) unique check(len(MemberAccount) between 6 and 12),
	MemberPwd nvarchar(20),
	MemberName nvarchar(20),
	MemberPhone nvarchar(20)
)
truncate table Member

insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('liubei','123456','����','4659874564')
insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('guanyu','123456','����','42354234124')
insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('zhangfei','123456','�ŷ�','41253445')
insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('zhangyun','123456','����','75675676547')
insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('machao','123456','��','532523523')


select * from Member

select * from sysprocesses where dbid= db_id('DBTEST')