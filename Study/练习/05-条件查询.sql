--�Ƚ��������=  !=  >  <  >=  <= (IS NULL)  in  like  (BETWEEN...AND...)��
--�߼��������and or not ��

--����ָ���У��������Ա���н���绰����ѯ�Ա�ΪŮ��Ա����Ϣ,���Զ�����������

--��ѯ��н���ڵ���10000 ��Ա����Ϣ( ������ )

--��ѯ��н���ڵ���10000 ��ŮԱ����Ϣ(������)

--��ʾ������������1980-1-1֮�󣬶�����н���ڵ���10000��ŮԱ����Ϣ��

--��ʾ����н���ڵ���15000 ��Ա��,������н���ڵ���8000��ŮԱ����Ϣ��

--��ѯ��н��10000-20000 ֮��Ա����Ϣ( ������ ) 

--��ѯ����ַ�ڱ��������Ϻ���Ա����Ϣ

--��ѯ����Ա����Ϣ(���ݹ������򣬽�������) order by: ����  asc: ����  desc: ����

--��ʾ���е�Ա����Ϣ���������ֵĳ��Ƚ��е�������

--��ѯ������ߵ�5���˵���Ϣ

--��ѯ������ߵ�10%��Ա����Ϣ


--��ѯ����ַû����д��Ա����Ϣ

--��ѯ����ַ�Ѿ���д��Ա����Ϣ


--��ѯ���е�80��Ա����Ϣ


--��ѯ������30-40 ֮�䣬���ҹ�����15000-30000 ֮���Ա����Ϣ(between and)

--��ѯ����з 6.22--7.22 ��Ա����Ϣ
--��ѯ���ʱ����Ƹߵ���

--��ѯ����������ͬһ�����е���

--��ѯ����ФΪ�����Ա��Ϣ

--��ѯ����Ա����Ϣ�����һ����ʾ����(��,ţ,��,��,��,��,��,��,��,��,��,��)
select PeopleName ����,PeopleSex �Ա�,PeopleSalary ����,PeoplePhone �绰,PEOPLEBIRTH ����,
case
	when year(PeopleBirth) % 12 = 4 then '��'
	when year(PeopleBirth) % 12 = 5 then 'ţ'
	when year(PeopleBirth) % 12 = 6 then '��'
	when year(PeopleBirth) % 12 = 7 then '��'
	when year(PeopleBirth) % 12 = 8 then '��'
	when year(PeopleBirth) % 12 = 9 then '��'
	when year(PeopleBirth) % 12 = 10 then '��'
	when year(PeopleBirth) % 12 = 11 then '��'
	when year(PeopleBirth) % 12 = 0 then '��'
	when year(PeopleBirth) % 12 = 1 then '��'
	when year(PeopleBirth) % 12 = 2 then '��'
	when year(PeopleBirth) % 12 = 3 then '��'
	ELSE ''
end ��Ф
from People

select PeopleName ����,PeopleSex �Ա�,PeopleSalary ����,PeoplePhone �绰,PEOPLEBIRTH ����,
case year(PeopleBirth) % 12
	when 4 then '��'
	when 5 then 'ţ'
	when 6 then '��'
	when 7 then '��'
	when 8 then '��'
	when 9 then '��'
	when 10 then '��'
	when 11 then '��'
	when 0 then '��'
	when 1 then '��'
	when 2 then '��'
	when 3 then '��'
	ELSE ''
end ��Ф
from People

