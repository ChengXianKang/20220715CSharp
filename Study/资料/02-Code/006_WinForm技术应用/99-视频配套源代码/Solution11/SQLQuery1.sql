create table Member
(
	MemberId int primary key identity(1,1),
	MemberAccount nvarchar(20) unique ,
	MemberPwd nvarchar(20),
	MemberName nvarchar(20),
	MemberPhone nvarchar(20)
)

insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('liubei','123456','刘备','4659874564')
insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('guanyu','123456','关羽','42354234124')
insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('zhangfei','123456','张飞','41253445')
insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('zhangyun','123456','赵云','75675676547')
insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
values('machao','123456','马超','532523523')

select * from Member
--存储过程-------------------------------------------------------------------------------------
--查询Member表所有数据(没有参数)
create proc procSelectMember
as
	select * from Member
go
--调用
exec procSelectMember

--添加会员信息（有输入参数）
create proc procInsertMember
	@acc nvarchar(20),
	@pwd nvarchar(20),
	@memName nvarchar(20),
	@memPhone nvarchar(20)
as
	insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)
	values(@acc,@pwd,@memName,@memPhone)
go
--调用
exec procInsertMember 'sunwukong','123456','孙悟空','13554856985'

--根据账号查询姓名和电话（有输入参数，有输出参数）
create proc procGetInfoByAcc
	@acc nvarchar(20),
	@memName nvarchar(20) output,
	@phone nvarchar(20) output
as
	select @memName = (select MemberName from Member where MemberAccount=@acc)
	select @phone = (select MemberPhone from Member where MemberAccount=@acc)
go
--调用
declare @name nvarchar(20)
declare @phone nvarchar(20)
exec procGetInfoByAcc 'machao',@name output,@phone output
select @name,@phone

--密码升级，传入用户名和密码，如果用户名密码正确，并且密码长度<8，自动升级成8位密码
--有同时输入输出参数（密码作为输入参数也作为输出参数）
select FLOOR(RAND()*10) --0-9之间随机数
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
--调用
declare @pwd nvarchar(20) = '123456'
exec procPwdUpgrade 'liubei',@pwd output
select @pwd



drop proc procInsertMember
--添加会员信息（有返回值）
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
	else if @myErr = 2627 --唯一约束
		return -1
	else
		return -100
go
--调用
declare @return int
exec @return = procInsertMember 'sunwukong','123456','孙悟空','13554854785'
print @return

select * from Member
