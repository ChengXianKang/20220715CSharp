package com.demo05;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Set;

public class JavaTest {

	public static void main(String[] args) {
		// HashMap
		//ʵ��������ѧ��
		Student stu1 = new Student("0001", "����", "13554545487");
		Student stu2 = new Student("0002", "����", "13554545487");
		Student stu3 = new Student("0003", "�ŷ�", "13554545487");
		//����HashMap����
		HashMap<String, Student> map = new HashMap<String, Student>();
		//��ѧ����ӵ����ϣ��Լ�ֵ����ʽ������ӣ�
		map.put(stu1.no, stu1);
		map.put(stu2.no, stu2);
		map.put(stu3.no, stu3);
		//map.put("0004","����");
		//���ݼ�ȡֵ(ȡ��ѧ��0002��ѧ������ӡ��ѧ����Ϣ)
		Student stu = map.get("0002");
		stu.SayHi();
		//��ʾ������Ԫ�ظ���
		System.out.println("������Ԫ�ظ���Ϊ:"+map.size());
		//�ж�ĳ��ѧ���Ƿ���ڣ�ͨ������0002���жϣ�
		System.out.println("ѧ��0002��ѧ���Ƿ����:"+map.containsKey("0002"));
		//ɾ��ѧ����ɾ��ѧ��0002��ѧ����
		map.remove("0002");
		//��ʾ�����ϣ�ֵ����
		System.out.println(map.keySet());  //������
		System.out.println(map.values());  //ֵ����
		
		//ѭ����ʾ����ѧ����Ϣ-ѭ����
//		Set keys = map.keySet();
//		for(Object key:keys)
//		{
//			Student tempStu = (Student)map.get(key);
//			tempStu.SayHi();
//		}
		
		//ѭ����ʾ����ѧ����Ϣ-ѭ��ֵ
//		Collection values = map.values();
//		for(Object value:values)
//		{
//			Student tempStu = (Student)value;
//			tempStu.SayHi();
//		}
		
		//ͨ����������ʽѭ��
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
