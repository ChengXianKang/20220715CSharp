import java.util.Scanner;

public class Task03 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		//����һ��Ӣ����ĸ��ͨ���жϺ������ʾ����Ӣ����ĸ�Ǵ�д��ĸ����Сд��ĸ��
//		System.out.println((int)'A'); //65
//		System.out.println((int)'Z'); //90
//		System.out.println((int)'a'); //97
//		System.out.println((int)'z'); //122
		
		Scanner input = new Scanner(System.in);
		System.out.println("������һ����ĸ:");
		char myChar = input.nextLine().charAt(0);
		if(myChar >= 65 && myChar <= 90)
		{
			System.out.println("��д��ĸ");
		}
		else if(myChar >= 97 && myChar <= 122)
		{
			System.out.println("Сд��ĸ");
		}
		else
		{
			System.out.println("����");
		}
	}

}
