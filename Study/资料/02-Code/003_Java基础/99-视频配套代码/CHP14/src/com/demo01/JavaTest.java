package com.demo01;

import java.util.ArrayList;

public class JavaTest {

	public static void main(String[] args) {
		// Arraylist
		Student stu1 = new Student("0001", "����", "13554545487");
		Student stu2 = new Student("0002", "����", "13554545487");
		Student stu3 = new Student("0003", "�ŷ�", "13554545487");
		//��3��ѧ����ӵ�Arraylist����
		ArrayList list = new ArrayList();
		list.add(stu1);
		list.add(stu2);
		list.add(stu3);
		//�������Ԫ�ظ���
		System.out.println("������Ԫ�ظ���:"+list.size());
		//ͨ��ѭ����������ӡѧ����Ϣ
		for (int i = 0; i < list.size(); i++) {
			Student stu = (Student)list.get(i);
			stu.SayHi();
		}
		//������ɾ��һ��ѧ��
		//list.remove(1);
		list.remove(stu2);
		System.out.println("ɾ��֮��--------------------------------------");
		//ͨ��ѭ����������ӡѧ����Ϣ
		for (int i = 0; i < list.size(); i++) {
			Student stu = (Student)list.get(i);
			stu.SayHi();
		}
		
		//�ж�ĳ��Ԫ���Ƿ��ڼ�����
		//�жϹ����Ƿ��ڼ�����
		System.out.println("�����Ƿ��ڼ�����:" + list.contains(stu2));
		//�ж��ŷ��Ƿ��ڼ�����
		System.out.println("�ŷ��Ƿ��ڼ�����:" + list.contains(stu3));
		
	}

}
