import java.util.Scanner;

public class Demo01 {

	public static void main(String[] args) {
		//�������洢���ݵ�����
		
		//������������������  ������;��
		//int a;   //int������������Ϊ����,a�Ǳ�����
		
		//������ֵ
//		int a;
//		a = 10;
		
		//����+��ֵ
		//int a = 10;
		
		//�������������淶
		//(1)ֻ�������֣���ĸ���»��ߣ���Ԫ�������
		//int a%^%;   //���󣬰����˳������֣���ĸ���»��ߣ���Ԫ����������ַ���
		
		//(2)��һ����ĸ����������
		//int 1a;  //����
		
		//(3)������ϵͳ�Ĺؼ���
		//int int; //����
		
		//(4)���飺���ּ���ʶ�塣
		//		(1)�շ�����(�����洢ѧ������, studentName)
		//     (2)�»���(�����洢ѧ������, student_name)
		
		//�������������洢�Լ������䣬��ߣ����أ����ҽ�������ֵ��ӡ����
//		int age = 18;
//		int high = 180;
//		int weight = 180;
//		System.out.println("�ҵĻ�����Ϣ����:");
//		System.out.println("����:"+age);
//		System.out.println("���:"+high);
//		System.out.println("����:"+weight);
		
		//���û������Լ������䣬��ߣ����أ����ҽ�������ֵ��ӡ����
		Scanner input = new Scanner(System.in);
		System.out.println("��������������:");
		int $age = input.nextInt();
		System.out.println("�������������:");
		int high = input.nextInt();
		System.out.println("��������������:");
		int weight = input.nextInt();
		System.out.println("�ҵĻ�����Ϣ����:");
		System.out.println("����:"+$age);
		System.out.println("���:"+high);
		System.out.println("����:"+weight);	
		
		
		
		
		
		
		
		
		
		

	}

}
