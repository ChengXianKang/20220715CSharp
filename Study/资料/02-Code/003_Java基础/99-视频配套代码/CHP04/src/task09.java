
public class task09 {

	public static void main(String[] args) {
		// ��ӡ���ĵ���������
//		for (int i = 1; i <= 6; i++) 
//		{
//			for (int j = 1; j <= 6-i; j++) //�ո�
//			{
//				System.out.print(" ");
//			}
//			for (int j = 1; j <= 2*i-1; j++) //��ӡ*
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
			for (int j = 1; j <= 6-i; j++) //�ո�
			{
				System.out.print(" ");
			}
			for (int j = 1; j <= 2*i-1; j++) //��ӡ*
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
