--专业
create table ProfessionInfo
(
	ProfessionID int primary key identity(1,1), --专业编号
	ProfessionName varchar(50) not null unique --专业名称
)
--学生
create table StudentInfo
(
	StuID varchar(20) primary key,  --学生学号
	StuName varchar(50) not null,		--学生姓名
	StuAge int not null check(StuAge > 0 and StuAge < 130), --学生年龄
	StuSex char(2) not null check(StuSex in('男','女')),  --学生性别
	StuHobby nvarchar(100), --爱好
	ProfessionID int not null references ProfessionInfo(ProfessionID), --所属专业编号
)
--添加专业信息
insert into ProfessionInfo(ProfessionName) values('电子竞技')
insert into ProfessionInfo(ProfessionName) values('软件开发')
insert into ProfessionInfo(ProfessionName) values('医疗护理')
--插入学生信息
insert into StudentInfo(StuID,StuName,StuAge,StuSex,StuHobby,ProfessionID)
values('001','刘备',18,'男','',1)
insert into StudentInfo(StuID,StuName,StuAge,StuSex,StuHobby,ProfessionID)
values('002','关羽',20,'男','',2)
insert into StudentInfo(StuID,StuName,StuAge,StuSex,StuHobby,ProfessionID)
values('003','张飞',19,'男','',2)
insert into StudentInfo(StuID,StuName,StuAge,StuSex,StuHobby,ProfessionID)
values('004','孙尚香',17,'女','',3)

