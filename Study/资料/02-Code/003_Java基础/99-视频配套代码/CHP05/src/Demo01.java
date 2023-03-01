import java.util.Scanner;

public class Demo01 {
	public static void main(String[] args) {
		//数组声明
		//int[] arr;
		//数组定义
		//arr = new int[5];
		
		//数组声明+定义
		//int[] arr = new int[5];
		//给数组进行赋值
		//arr[0] = 10;
		//arr[1] = 10;
		//arr[2] = 10;
		//arr[3] = 10;
		//arr[4] = 10;

		//数组的初始化
		//int[] arr = new int[] {10,50,60,80,90};
		
		//数组的输出
		//System.out.println(arr[1]);
		
		//利用循环来打印数组元素的值
//		for (int i = 0; i < arr.length; i++) {
//			System.out.println(arr[i]);
//		}
		
		//例：用键盘输入数组元素值，打印最大值，最小值，总和，平均值
		Scanner input  = new Scanner(System.in);
		int[] arr = new int[5];
		//键盘输入
		for (int i = 0; i < arr.length; i++) {
			System.out.println("请输入数组第"+(i+1)+"个元素的值：");
			arr[i] = input.nextInt();
		}
		//计算结果
		int sum = 0;
		int avg = 0;
		int max = arr[0];  //假设数组第一个元素为最大值
		int min = arr[0];  //假设数组第一个元素为最小值
		for (int i = 0; i < arr.length; i++) {
			if(arr[i] > max)
				max = arr[i];
			if(arr[i] < min)
				min = arr[i];
			sum += arr[i];
		}
		avg = sum / arr.length;
		System.out.println("最大值:"+max);
		System.out.println("最小值:"+min);
		System.out.println("总和:"+sum);
		System.out.println("平均值:"+avg);
		
		
	}

}
