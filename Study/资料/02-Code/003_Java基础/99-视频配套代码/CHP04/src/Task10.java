
public class Task10 {

	public static void main(String[] args) {
		// 打印数字三角形
		int k=1;
		for (int i = 1; i <= 6; i++) 
		{
			for (int j = 1; j <= 6-i; j++) 
			{
				System.out.print(" ");
			}
			for (int j = 1; j <= 2*i-1; j++) 
			{
				System.out.print(k++);
				if(k == 10)
					k=1;
			}
			System.out.println();
		}

	}

}
