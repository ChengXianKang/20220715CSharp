import java.util.Scanner;
public class Task02 {
	public static void main(String[] args) {
		//	����{10,20,30,40,50,60,70,80,90},�ü�������һ�����֣��ж��Ƿ����
		int[] arr = new int[] {10,20,30,40,50,60,70,80,90};
		Scanner input = new Scanner(System.in);
		System.out.println("������һ������:");
		int num = input.nextInt();
		if(num < arr[0] || num >  arr[arr.length-1])
		{
			System.out.println("û���ҵ�!");
			return;
		}
		int start = 0; //�������
		int end = arr.length-1; //���ҵ��յ�
		boolean result = false; //�����Ҳ���
		while(start <= end)
		{
			int mid = (start+end)/2; //�ҵ��м��
			if(num < arr[mid])
				end = mid-1;
			else if(num > arr[mid])
				start = mid+1;
			else
			{
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
