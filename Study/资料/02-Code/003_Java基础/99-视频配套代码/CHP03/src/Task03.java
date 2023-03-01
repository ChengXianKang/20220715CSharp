import java.util.Scanner;

public class Task03 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		//输入一个英文字母，通过判断后给出提示，该英文字母是大写字母还是小写字母。
//		System.out.println((int)'A'); //65
//		System.out.println((int)'Z'); //90
//		System.out.println((int)'a'); //97
//		System.out.println((int)'z'); //122
		
		Scanner input = new Scanner(System.in);
		System.out.println("请输入一个字母:");
		char myChar = input.nextLine().charAt(0);
		if(myChar >= 65 && myChar <= 90)
		{
			System.out.println("大写字母");
		}
		else if(myChar >= 97 && myChar <= 122)
		{
			System.out.println("小写字母");
		}
		else
		{
			System.out.println("其它");
		}
	}

}
