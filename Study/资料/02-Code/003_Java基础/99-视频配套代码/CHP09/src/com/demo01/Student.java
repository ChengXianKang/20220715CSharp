package com.demo01;
//classǰ��public�������У����еط������Է���
//calssǰ��ȱʡ��������ͬһ��������Է���
public class Student 
{
	public Student()
	{
		;
	}	
	public Student(String no,String name,String sex)
	{
		this.stuNO = no;
		this.stuName = name;
		this.stuSex = sex;
	}
	
	//�ֶ�
	public String stuNO;  //ѧ��
	public String stuName;  //����
	public String stuSex; //�Ա�
	//����
	public void sayHi()  //���ҽ���
	{
		System.out.println("��Һ�");
		System.out.println("�ҵ�ѧ����:"+this.stuNO);
		System.out.println("�ҵ�������:" + this.stuName);
		System.out.println("�ҵ��Ա���:" + this.stuSex);
	}
	
}
