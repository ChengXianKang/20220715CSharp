import java.util.Scanner;

public class Task01 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Scanner input = new Scanner(System.in);
		System.out.println("������һ�����֣�");
		int num = input.nextInt();
		int b = num / 100;
		int s = num/10%10;
		int g = num % 10;
		if(b*b*b + s*s*s + g*g*g == num)
		{
			System.out.println("��ˮ�ɻ�");
		}
		else
		{
			System.out.println("����ˮ�ɻ�");
		}
	}

}
