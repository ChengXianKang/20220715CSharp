--��Ա��Ϣ
create table MyUser
(
	UserId int primary key identity(1,1), --�û����
	UserAccount varchar(50) not null, --�û���
	UserPwd varchar(50) not null, --����
	Question varchar(200) not null, --�ܱ�����
	Answer varchar(200) not null, --�����
	UserMail varchar(200) not null, --����
	UserPhone varchar(20) not null, --�绰
	UserSex varchar(2) not null, --�Ա�
	UserPro varchar(50) not null, --רҵ
	UserHobby varchar(200) not null, --����
	UserPhoto varchar(100) not null,--ͷ����Ƭ
	UserSelf text --���ҽ���
)
--�ܱ�����
create table MyQuestion
(
	QuestionId int primary key identity(1,1), --�ܱ�������
	QuestionTitle varchar(200) not null
)
insert into MyQuestion(QuestionTitle) values('����ϲ����С�����˭?')
insert into MyQuestion(QuestionTitle) values('����ϲ����С�����˭?')
insert into MyQuestion(QuestionTitle) values('��Сѧͬ����˭?')
insert into MyQuestion(QuestionTitle) values('����а�������˭?')

select * from MyQuestion

select * from MyUser

