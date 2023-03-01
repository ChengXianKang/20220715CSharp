
public class Task06 {

	public static void main(String[] args) {
		// 使用循环打印100-200之间所有的素数（只能够被1和自己整除的数叫做素数）。
		for (int num = 100; num <= 200; num++) 
		{
			boolean result = true;  //假设num是素数
			for (int i = 2; i <= Math.sqrt(num) ; i++) 
			{
				if(num % i == 0)
				{
					result = false;
					break;
				}
			}
			if(result == true)
				System.out.print(num+"\t");
		}
	}

}
