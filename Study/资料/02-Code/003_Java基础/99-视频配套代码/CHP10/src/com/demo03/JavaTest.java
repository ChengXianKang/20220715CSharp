package com.demo03;

import java.util.Scanner;

public class JavaTest {

	public static void main(String[] args) {
		// �����������������,1-è,2-��
		People p = new People();
		Scanner input = new Scanner(System.in);
		System.out.println("�����붯�����ͣ�1-è,2-��");
		int type = input.nextInt();
		
		Animal animal = p.BuyAnimal(type);
		
		p.SayHi(animal);
	}

}
