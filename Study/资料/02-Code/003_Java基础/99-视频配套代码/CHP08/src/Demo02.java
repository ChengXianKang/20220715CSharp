import java.util.Scanner;

public class Demo02 {

	public static void main(String[] args) {
		// �ַ����Ƚ�
//		String str1 = "hello";
//		String str2 = "hello";
//		System.out.println(str1 == str2);  //true
		//���ۣ��ַ����ȽϿ���ʹ��==(����Ľ���)
		
//		Scanner input = new Scanner(System.in);
//		System.out.println("�������һ���ַ���:");
//		String str1 = input.nextLine();
//		System.out.println("�������һ���ַ���:");
//		String str2 = input.nextLine();
//		System.out.println(str1 == str2);  //������������һ�£������false
		//���ۣ��ַ����Ƚϲ���ʹ��==
		
		//��ν����ʹ��Java�ṩ�ĺ�������бȽ�
		
		Scanner input = new Scanner(System.in);
		System.out.println("�������һ���ַ���:");
		String str1 = input.nextLine();
		System.out.println("�������һ���ַ���:");
		String str2 = input.nextLine();
		System.out.println(str1.equals(str2));  //true
		
		//���ս��ۣ��ַ����Ƚ�ʹ��equals���С�
		
	}

}
