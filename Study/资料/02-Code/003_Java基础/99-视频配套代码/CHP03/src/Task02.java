import java.util.Scanner;

public class Task02 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
//		��1��������ܱ�4���������Ҳ��ܱ�100������
//		��2��������ܹ���400������
		Scanner input = new Scanner(System.in);
		System.out.println("������һ�����");
		int year = input.nextInt();
		if( (year%4==0 && year % 100 != 0 ) || year % 400 == 0)
		{
			System.out.println("����");
		}
		else
		{
			System.out.println("ƽ��");
		}
	}

}
