
public class Demo05 {

	public static void main(String[] args) {
		// ѭ��Ƕ��
//		for(int i=1;i<=10;i++)
//		{
//			for(int j=1;j<=10;j++)
//			{
//				System.out.println("i="+i+",j="+j);
//			}
//		}
		//ѭ��Ƕ�ף������ѭ������ĳһ��״̬��ʱ����ִ���ڲ�ѭ�����ڴ�ѭ��ִ����ɣ���ȥ�ı����ѭ��
		//����:i=1,j=1,2,3,4,5,6,7,8,9,10  ;i=2,j=1,2,3,4,5,6,7,8,9,10,...........
		
		
		//����ѭ��Ƕ�״�ӡ�žų˷���
		for(int i=1;i<=9;i++)
		{
			for(int j=1;j<=i;j++)
			{
				System.out.print(i+"*"+j+"="+i*j +"\t");
			}
			System.out.println();
		}	
	}

}
