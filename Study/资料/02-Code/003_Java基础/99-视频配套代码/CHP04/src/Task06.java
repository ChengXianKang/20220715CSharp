
public class Task06 {

	public static void main(String[] args) {
		// ʹ��ѭ����ӡ100-200֮�����е�������ֻ�ܹ���1���Լ���������������������
		for (int num = 100; num <= 200; num++) 
		{
			boolean result = true;  //����num������
			for (int i = 2; i <= Math.sqrt(num) ; i++) 
			{
				if(num % i == 0)
				{
					result = false;
					break;
				}
			}
			if(result == true)
				System.out.print(num+"\t");
		}
	}

}
