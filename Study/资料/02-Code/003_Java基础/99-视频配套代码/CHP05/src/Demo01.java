import java.util.Scanner;

public class Demo01 {
	public static void main(String[] args) {
		//��������
		//int[] arr;
		//���鶨��
		//arr = new int[5];
		
		//��������+����
		//int[] arr = new int[5];
		//��������и�ֵ
		//arr[0] = 10;
		//arr[1] = 10;
		//arr[2] = 10;
		//arr[3] = 10;
		//arr[4] = 10;

		//����ĳ�ʼ��
		//int[] arr = new int[] {10,50,60,80,90};
		
		//��������
		//System.out.println(arr[1]);
		
		//����ѭ������ӡ����Ԫ�ص�ֵ
//		for (int i = 0; i < arr.length; i++) {
//			System.out.println(arr[i]);
//		}
		
		//�����ü�����������Ԫ��ֵ����ӡ���ֵ����Сֵ���ܺͣ�ƽ��ֵ
		Scanner input  = new Scanner(System.in);
		int[] arr = new int[5];
		//��������
		for (int i = 0; i < arr.length; i++) {
			System.out.println("�����������"+(i+1)+"��Ԫ�ص�ֵ��");
			arr[i] = input.nextInt();
		}
		//������
		int sum = 0;
		int avg = 0;
		int max = arr[0];  //���������һ��Ԫ��Ϊ���ֵ
		int min = arr[0];  //���������һ��Ԫ��Ϊ��Сֵ
		for (int i = 0; i < arr.length; i++) {
			if(arr[i] > max)
				max = arr[i];
			if(arr[i] < min)
				min = arr[i];
			sum += arr[i];
		}
		avg = sum / arr.length;
		System.out.println("���ֵ:"+max);
		System.out.println("��Сֵ:"+min);
		System.out.println("�ܺ�:"+sum);
		System.out.println("ƽ��ֵ:"+avg);
		
		
	}

}
