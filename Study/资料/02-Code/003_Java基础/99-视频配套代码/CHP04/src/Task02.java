
public class Task02 {

	public static void main(String[] args) {
		//��1998-2008��֮������������������������Ϊ������߳���һ����Ϊ���꣺
		//��1��������ܱ�4���������Ҳ��ܱ�100������
		//��2��������ܹ���400������
		for (int year = 1998; year <= 2008 ; year++) 
		{
			if((year%4 == 0 && year%100 != 0) || year % 400 == 0)
				System.out.println(year);
		}
	}

}
