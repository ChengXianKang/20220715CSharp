package com.demo01;
//class前面public，代表公有，所有地方都可以访问
//calss前面缺省，代表在同一个包里可以访问
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
	
	//字段
	public String stuNO;  //学号
	public String stuName;  //姓名
	public String stuSex; //性别
	//方法
	public void sayHi()  //自我介绍
	{
		System.out.println("大家好");
		System.out.println("我的学号是:"+this.stuNO);
		System.out.println("我的姓名是:" + this.stuName);
		System.out.println("我的性别是:" + this.stuSex);
	}
	
}
