
public class task09 {

	public static void main(String[] args) {
		// 打印空心等腰三角形
//		for (int i = 1; i <= 6; i++) 
//		{
//			for (int j = 1; j <= 6-i; j++) //空格
//			{
//				System.out.print(" ");
//			}
//			for (int j = 1; j <= 2*i-1; j++) //打印*
//			{
//				if(i==1 || i==6 || j == 1 || j == 2*i-1)
//					System.out.print("*");
//				else
//					System.out.print(" ");
//			}
//			System.out.println();
//		}

		
		for (int i = 1; i <= 6; i++) 
		{
			for (int j = 1; j <= 6-i; j++) //空格
			{
				System.out.print(" ");
			}
			for (int j = 1; j <= 2*i-1; j++) //打印*
			{
				if(i==1 || i==6 || j == 1 || j == 2*i-1 || i == j)
					System.out.print("*");
				else
					System.out.print(" ");
			}
			System.out.println();
		}
		
	}

}
