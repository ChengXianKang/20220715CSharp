package com.demo01;
public class Demo01 {
	public static void main(String[] args) {
		
		Student stu1 = new Student();   //创建对象
		stu1.stuNO = "0001";
		stu1.stuName = "刘德华";
		stu1.stuSex = "男";
		
		stu1.sayHi();
		
		Student stu2 = new Student("0001","刘德华","男");
		stu2.sayHi();
		
		
		
		
	}

}
