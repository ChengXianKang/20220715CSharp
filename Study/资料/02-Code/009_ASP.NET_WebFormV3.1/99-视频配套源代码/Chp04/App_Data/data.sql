create table MyUser --用户
(
	UserId int primary key identity(1,1), --用户编号
	UserAccount varchar(50) not null,  --账号
	UserPwd varchar(50) not null,  --密码
	UserMail varchar(100) not null, --邮箱
	UserPhone varchar(20) not null, --电话
	UserSex varchar(2) not null,    --性别
)
insert into MyUser(UserAccount,UserPwd,UserMail,UserPhone,UserSex)
values('liubei','123456','liubei@qq.com','13547896547','男')
insert into MyUser(UserAccount,UserPwd,UserMail,UserPhone,UserSex)
values('guanyu','123456','guanyu@qq.com','15356875478','男')
insert into MyUser(UserAccount,UserPwd,UserMail,UserPhone,UserSex)
values('zhangfei','123456','zhangfei@qq.com','13666689874','男')