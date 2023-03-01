package com.demo05;

public class Student {
    String no; //学号
    String name; //姓名
    String phone; //电话
    public Student()
    {
    	;
    }
    public Student(String no,String name,String phone)
    {
    	this.no = no;
    	this.name = name;
    	this.phone = phone;
    }
    //方法（行为）
    public void SayHi()
    {
        System.out.println("大家好,我的学号:"+this.no+",我的姓名:"+this.name+",我的电话:"+this.phone);
    }
}
