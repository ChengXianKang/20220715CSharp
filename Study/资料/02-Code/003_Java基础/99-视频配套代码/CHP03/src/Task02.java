import java.util.Scanner;

public class Task02 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
//		（1）年份数能被4整除，而且不能被100整除。
//		（2）年份数能够被400整除。
		Scanner input = new Scanner(System.in);
		System.out.println("请输入一个年份");
		int year = input.nextInt();
		if( (year%4==0 && year % 100 != 0 ) || year % 400 == 0)
		{
			System.out.println("闰年");
		}
		else
		{
			System.out.println("平年");
		}
	}

}
