package com.demo05;

public class Student {
    String no; //ѧ��
    String name; //����
    String phone; //�绰
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
    //��������Ϊ��
    public void SayHi()
    {
        System.out.println("��Һ�,�ҵ�ѧ��:"+this.no+",�ҵ�����:"+this.name+",�ҵĵ绰:"+this.phone);
    }
}
