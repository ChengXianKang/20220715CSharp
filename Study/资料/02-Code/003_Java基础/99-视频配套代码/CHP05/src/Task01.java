import java.util.Scanner;

public class Task01 {
	public static void main(String[] args) {
		// 数组{50,60,70,80,90,100,40,30,20},用键盘输入一个数字，判断是否存在
		int[] arr = new int[] {50,60,70,80,90,100,40,30,20};
		Scanner input = new Scanner(System.in);
		System.out.println("请输入一个数字:");
		int num = input.nextInt();
		boolean result = false; //假设找不到
		for (int i = 0; i < arr.length; i++) {
			if(num == arr[i]) {
				result = true;
				break;
			}
		}
		if(result == true)
			System.out.println("找到了");
		else
			System.out.println("没有找到");
		

	}

}
