create table MyUser --�û�
(
	UserId int primary key identity(1,1), --�û����
	UserAccount varchar(50) not null,  --�˺�
	UserPwd varchar(50) not null,  --����
	UserMail varchar(100) not null, --����
	UserPhone varchar(20) not null, --�绰
	UserSex varchar(2) not null,    --�Ա�
)
insert into MyUser(UserAccount,UserPwd,UserMail,UserPhone,UserSex)
values('liubei','123456','liubei@qq.com','13547896547','��')
insert into MyUser(UserAccount,UserPwd,UserMail,UserPhone,UserSex)
values('guanyu','123456','guanyu@qq.com','15356875478','��')
insert into MyUser(UserAccount,UserPwd,UserMail,UserPhone,UserSex)
values('zhangfei','123456','zhangfei@qq.com','13666689874','��')