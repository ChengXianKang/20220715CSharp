--会员信息
create table MyUser
(
	UserId int primary key identity(1,1), --用户编号
	UserAccount varchar(50) not null, --用户名
	UserPwd varchar(50) not null, --密码
	Question varchar(200) not null, --密保问题
	Answer varchar(200) not null, --问题答案
	UserMail varchar(200) not null, --邮箱
	UserPhone varchar(20) not null, --电话
	UserSex varchar(2) not null, --性别
	UserPro varchar(50) not null, --专业
	UserHobby varchar(200) not null, --爱好
	UserPhoto varchar(100) not null,--头像，照片
	UserSelf text --自我介绍
)
--密保问题
create table MyQuestion
(
	QuestionId int primary key identity(1,1), --密保问题编号
	QuestionTitle varchar(200) not null
)
insert into MyQuestion(QuestionTitle) values('你最喜欢的小姐姐是谁?')
insert into MyQuestion(QuestionTitle) values('你最喜欢的小哥哥是谁?')
insert into MyQuestion(QuestionTitle) values('你小学同桌是谁?')
insert into MyQuestion(QuestionTitle) values('你初中班主任是谁?')

select * from MyQuestion

select * from MyUser

