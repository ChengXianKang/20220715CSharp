package com.demo03;

import java.util.Scanner;

public class JavaTest {

	public static void main(String[] args) {
		// 人领养动物，输入类型,1-猫,2-狗
		People p = new People();
		Scanner input = new Scanner(System.in);
		System.out.println("请输入动物类型，1-猫,2-狗");
		int type = input.nextInt();
		
		Animal animal = p.BuyAnimal(type);
		
		p.SayHi(animal);
	}

}
