package com.demo02;
public class Student 
{
	//字段
	//public:公有，所有地方都可以访问，private:私有，只有自己内部可以访问 
	private String stuNO;  //学号
	private String stuName;  //姓名
	private String stuSex; //性别
	
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
		if(stuSex.equals("男") || stuSex.equals("女"))
			this.stuSex = stuSex;
		else
			this.stuSex = "";
	}



	
	
	//方法
	public void sayHi()  //自我介绍
	{
		System.out.println("大家好");
		System.out.println("我的学号是:"+this.stuNO);
		System.out.println("我的姓名是:" + this.stuName);
		System.out.println("我的性别是:" + this.stuSex);
	}
	
}
