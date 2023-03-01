import java.util.Scanner;

public class Demo02 {

	public static void main(String[] args) {
		// 字符串比较
//		String str1 = "hello";
//		String str2 = "hello";
//		System.out.println(str1 == str2);  //true
		//结论：字符串比较可用使用==(错误的结论)
		
//		Scanner input = new Scanner(System.in);
//		System.out.println("请输入第一个字符串:");
//		String str1 = input.nextLine();
//		System.out.println("请输入第一个字符串:");
//		String str2 = input.nextLine();
//		System.out.println(str1 == str2);  //键盘输入内容一致，但结果false
		//结论：字符串比较不能使用==
		
		//如何解决：使用Java提供的函数类进行比较
		
		Scanner input = new Scanner(System.in);
		System.out.println("请输入第一个字符串:");
		String str1 = input.nextLine();
		System.out.println("请输入第一个字符串:");
		String str2 = input.nextLine();
		System.out.println(str1.equals(str2));  //true
		
		//最终结论：字符串比较使用equals进行。
		
	}

}
