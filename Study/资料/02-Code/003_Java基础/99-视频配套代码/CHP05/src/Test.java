import java.util.Scanner;

public class Test {

	public static void main(String[] args) {
		int[] arr = new int[] {30,50,20,80,60};
		for (int i = 0; i < arr.length-1; i++) 
		{
			int removeNum = arr[i+1];   //��������������
			int prepareIndex = i; //�ȽϵĿ�ʼλ�ã�����--������������ǰ�ƶ�
			for (prepareIndex = i; prepareIndex >= 0; prepareIndex--) 
			{
				if(removeNum < arr[prepareIndex])  //�����ȡ����С��ѭ���е����֣���ѭ���е����ֺ���
					arr[prepareIndex+1] = arr[prepareIndex];
				else //�����ȡ���ִ��ڵ���ѭ���е����֣����˳�ѭ������ѭ���⽫��ȡ�����ֲ����λ 
					break;
			}
			arr[prepareIndex+1] = removeNum;		
		}
		//��ӡ�������
		for (int i = 0; i < arr.length; i++) 
			System.out.print(arr[i]+"\t");
	}

}
