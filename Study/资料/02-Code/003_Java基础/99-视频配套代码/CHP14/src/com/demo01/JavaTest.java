package com.demo01;

import java.util.ArrayList;

public class JavaTest {

	public static void main(String[] args) {
		// Arraylist
		Student stu1 = new Student("0001", "刘备", "13554545487");
		Student stu2 = new Student("0002", "关羽", "13554545487");
		Student stu3 = new Student("0003", "张飞", "13554545487");
		//将3个学生添加到Arraylist集合
		ArrayList list = new ArrayList();
		list.add(stu1);
		list.add(stu2);
		list.add(stu3);
		//输出集合元素个数
		System.out.println("集合中元素个数:"+list.size());
		//通过循环遍历，打印学生信息
		for (int i = 0; i < list.size(); i++) {
			Student stu = (Student)list.get(i);
			stu.SayHi();
		}
		//集合中删除一个学生
		//list.remove(1);
		list.remove(stu2);
		System.out.println("删除之后--------------------------------------");
		//通过循环遍历，打印学生信息
		for (int i = 0; i < list.size(); i++) {
			Student stu = (Student)list.get(i);
			stu.SayHi();
		}
		
		//判断某个元素是否在集合中
		//判断关羽是否在集合中
		System.out.println("关羽是否在集合中:" + list.contains(stu2));
		//判断张飞是否在集合中
		System.out.println("张飞是否在集合中:" + list.contains(stu3));
		
	}

}
