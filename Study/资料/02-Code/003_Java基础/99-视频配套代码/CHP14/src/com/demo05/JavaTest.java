package com.demo05;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Set;

public class JavaTest {

	public static void main(String[] args) {
		// HashMap
		//实例化三个学生
		Student stu1 = new Student("0001", "刘备", "13554545487");
		Student stu2 = new Student("0002", "关羽", "13554545487");
		Student stu3 = new Student("0003", "张飞", "13554545487");
		//创建HashMap集合
		HashMap<String, Student> map = new HashMap<String, Student>();
		//将学生添加到集合（以键值对形式进行添加）
		map.put(stu1.no, stu1);
		map.put(stu2.no, stu2);
		map.put(stu3.no, stu3);
		//map.put("0004","赵云");
		//根据键取值(取出学号0002的学生，打印该学生信息)
		Student stu = map.get("0002");
		stu.SayHi();
		//显示集合中元素个数
		System.out.println("集合中元素个数为:"+map.size());
		//判断某个学生是否存在（通过键（0002）判断）
		System.out.println("学号0002的学生是否存在:"+map.containsKey("0002"));
		//删除学生（删除学号0002的学生）
		map.remove("0002");
		//显示键集合，值集合
		System.out.println(map.keySet());  //键集合
		System.out.println(map.values());  //值集合
		
		//循环显示所有学生信息-循环键
//		Set keys = map.keySet();
//		for(Object key:keys)
//		{
//			Student tempStu = (Student)map.get(key);
//			tempStu.SayHi();
//		}
		
		//循环显示所有学生信息-循环值
//		Collection values = map.values();
//		for(Object value:values)
//		{
//			Student tempStu = (Student)value;
//			tempStu.SayHi();
//		}
		
		//通过迭代器方式循环
		Set keys = map.keySet();
		Iterator it = keys.iterator();
		while(it.hasNext())
		{
			String key = (String)it.next();
			Student tempStu = map.get(key);
			tempStu.SayHi();
		}
		
		
		
		
		
	}

}
