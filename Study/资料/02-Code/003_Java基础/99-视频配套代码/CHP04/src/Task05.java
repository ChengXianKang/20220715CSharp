import java.util.Scanner;

public class Task05 {

	public static void main(String[] args) {
		//�û�����һ������n��n>=3�����ж��Ƿ�Ϊ����(ֻ�ܱ�1��������������)
		Scanner input = new Scanner(System.in);
		System.out.println("������һ������n��n>=3��:");
		int n = input.nextInt();
		boolean result = true;   //����������
		for (int i = 2; i <= Math.sqrt(n); i++) 
		{
			if(n%i == 0)
			{
				result = false;
				break;
			}
		}
		if(result == true)
			System.out.println("������");
		else
			System.out.println("��������");
		
	}

}
