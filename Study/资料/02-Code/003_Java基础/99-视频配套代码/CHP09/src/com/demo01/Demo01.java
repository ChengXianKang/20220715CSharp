package com.demo01;
public class Demo01 {
	public static void main(String[] args) {
		
		Student stu1 = new Student();   //��������
		stu1.stuNO = "0001";
		stu1.stuName = "���»�";
		stu1.stuSex = "��";
		
		stu1.sayHi();
		
		Student stu2 = new Student("0001","���»�","��");
		stu2.sayHi();
		
		
		
		
	}

}
