
public class Task08 {

	public static void main(String[] args) {
		// ��ӡ���������ǡ������Ǽ�����
//		for (int i = 1; i <= 6 ; i++) 
//		{
//			for (int j = 1; j <= 6-i; j++) 
//			{
//				System.out.print(" ");
//			}
//			for (int j = 1; j <= 2*i-1; j++) 
//			{
//				System.out.print("*");
//			}	
//			System.out.println();
//		}
		
		
//		for (int i = 1; i <= 6 ; i++) 
//		{
//			for (int j = 1; j <= i-1; j++) 
//			{
//				System.out.print(" ");
//			}
//			for (int j = 1; j <= 2*(6-i)+1; j++) 
//			{
//				System.out.print("*");
//			}	
//			System.out.println();
//		}
		
		for (int i = 1; i <= 6 ; i++) 
		{
			for (int j = 1; j <= 6-i; j++) 
			{
				System.out.print(" ");
			}
			for (int j = 1; j <= 2*i-1; j++) 
			{
				System.out.print("*");
			}	
			System.out.println();
		}
		for (int i = 2; i <= 6 ; i++) 
		{
			for (int j = 1; j <= i-1; j++) 
			{
				System.out.print(" ");
			}
			for (int j = 1; j <= 2*(6-i)+1; j++) 
			{
				System.out.print("*");
			}	
			System.out.println();
		}
	}

}
