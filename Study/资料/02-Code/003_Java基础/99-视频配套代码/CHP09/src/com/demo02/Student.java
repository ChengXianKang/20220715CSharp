package com.demo02;
public class Student 
{
	//�ֶ�
	//public:���У����еط������Է��ʣ�private:˽�У�ֻ���Լ��ڲ����Է��� 
	private String stuNO;  //ѧ��
	private String stuName;  //����
	private String stuSex; //�Ա�
	
	public String getStuNO() {
		return stuNO;
	}
	public void setStuNO(String stuNO) {
		this.stuNO = stuNO;
	}
	public String getStuName() {
		return stuName;
	}
	public void setStuName(String stuName) {
		this.stuName = stuName;
	}
	public String getStuSex() {
		return stuSex;
	}


	public void setStuSex(String stuSex) 
	{
		if(stuSex.equals("��") || stuSex.equals("Ů"))
			this.stuSex = stuSex;
		else
			this.stuSex = "";
	}



	
	
	//����
	public void sayHi()  //���ҽ���
	{
		System.out.println("��Һ�");
		System.out.println("�ҵ�ѧ����:"+this.stuNO);
		System.out.println("�ҵ�������:" + this.stuName);
		System.out.println("�ҵ��Ա���:" + this.stuSex);
	}
	
}
