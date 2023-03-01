import java.util.Scanner;

public class Task05 {

	public static void main(String[] args) {
		//用户输入一个数字n（n>=3），判断是否为素数(只能被1和自身整除的数)
		Scanner input = new Scanner(System.in);
		System.out.println("请输入一个数字n（n>=3）:");
		int n = input.nextInt();
		boolean result = true;   //假设是素数
		for (int i = 2; i <= Math.sqrt(n); i++) 
		{
			if(n%i == 0)
			{
				result = false;
				break;
			}
		}
		if(result == true)
			System.out.println("是素数");
		else
			System.out.println("不是素数");
		
	}

}
