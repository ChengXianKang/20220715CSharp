import java.util.Scanner;

public class Task01 {
	public static void main(String[] args) {
		// ����{50,60,70,80,90,100,40,30,20},�ü�������һ�����֣��ж��Ƿ����
		int[] arr = new int[] {50,60,70,80,90,100,40,30,20};
		Scanner input = new Scanner(System.in);
		System.out.println("������һ������:");
		int num = input.nextInt();
		boolean result = false; //�����Ҳ���
		for (int i = 0; i < arr.length; i++) {
			if(num == arr[i]) {
				result = true;
				break;
			}
		}
		if(result == true)
			System.out.println("�ҵ���");
		else
			System.out.println("û���ҵ�");
		

	}

}
