import java.util.Scanner;

public class Task04 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

		//��ţ�������ã������ߣ����򣬺����������
		//4	  5     6    7     8     9    10   11  0      1    2     3
		Scanner input = new Scanner(System.in);
		System.out.println("���������:");
		int year = input.nextInt();
		switch (year%12) {
		case 0: System.out.println("��");  break;
		case 1: System.out.println("��");   break;
		case 2: System.out.println("��");   break;
		case 3: System.out.println("��");  break;
		case 4: System.out.println("��");  break;
		case 5: System.out.println("ţ");  break;
		case 6: System.out.println("��");   break;
		case 7: System.out.println("��");  break;
		case 8: System.out.println("��");  break;
		case 9: System.out.println("��");  break;
		case 10: System.out.println("��");  break;
		case 11: System.out.println("��");  break;
		default:
			break;
		}
		
	}

}
