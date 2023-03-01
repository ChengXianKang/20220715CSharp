create table Member
(
	MemberId int primary key identity(1,1),
	MemberAccount nvarchar(20) unique ,
	MemberPwd nvarchar(20),
	MemberName nvarchar(20),
	MemberPhone nvarchar(20)
)

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
--�洢����-------------------------------------------------------------------------------------
--��ѯMember����������(û�в���)
create proc procSelectMember
as
	select * from Member
go
--����
exec procSelectMember

--��ӻ�Ա��Ϣ�������������
create proc procInsertMember
	@acc nvarchar(20),
	@pwd nvarchar(20),
	@memName nvarchar(20),
	@memPhone nvarchar(20)
as
	insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
	values(@acc,@pwd,@memName,@memPhone)
go
--����
exec procInsertMember 'sunwukong','123456','�����','13554856985'

--�����˺Ų�ѯ�����͵绰������������������������
create proc procGetInfoByAcc
	@acc nvarchar(20),
	@memName nvarchar(20) output,
	@phone nvarchar(20) output
as
	select @memName = (select MemberName from Member where MemberAccount=@acc)
	select @phone = (select MemberPhone from Member where MemberAccount=@acc)
go
--����
declare @name nvarchar(20)
declare @phone nvarchar(20)
exec procGetInfoByAcc 'machao',@name output,@phone output
select @name,@phone

--���������������û��������룬����û���������ȷ���������볤��<8���Զ�������8λ����
--��ͬʱ�������������������Ϊ�������Ҳ��Ϊ���������
select FLOOR(RAND()*10) --0-9֮�������
drop proc procPwdUpgrade
create proc procPwdUpgrade
@acc nvarchar(20),
@pwd nvarchar(20) output
as
	if not exists(select * from Member where MemberAccount=@acc and MemberPwd=@pwd)
		set @pwd = ''
	else
	begin
		if len(@pwd) < 8
		begin
			declare @len int = 8- len(@pwd)
			declare @i int = 1
			while @i <= @len
			begin		
				set @pwd = @pwd + cast(FLOOR(RAND()*10) as varchar(1))
				set @i = @i+1
			end
			update Member set MemberPwd = @pwd where MemberAccount=@acc
		end
	end
go
--����
declare @pwd nvarchar(20) = '123456'
exec procPwdUpgrade 'liubei',@pwd output
select @pwd



drop proc procInsertMember
--��ӻ�Ա��Ϣ���з���ֵ��
create proc procInsertMember
	@acc nvarchar(20),
	@pwd nvarchar(20),
	@memName nvarchar(20),
	@memPhone nvarchar(20)
as
	insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
	values(@acc,@pwd,@memName,@memPhone)
	declare @myErr int = @@error
	if @myErr = 0
		return 1
	else if @myErr = 2627 --ΨһԼ��
		return -1
	else
		return -100
go
--����
declare @return int
exec @return = procInsertMember 'sunwukong','123456','�����','13554854785'
print @return

select * from Member
