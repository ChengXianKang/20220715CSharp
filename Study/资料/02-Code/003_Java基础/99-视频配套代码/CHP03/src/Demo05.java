
public class Demo05 {

	public static void main(String[] args) {
		//�ж�������ͬ�����֧ȥʵ��
		//�ж�������ͬ��Ƕ��ȥʵ��
		//�����������ԣ��������
		//�Ա��У������50�����ϼ��񣬷��򲻼���
		//�Ա�Ů�������30�����ϼ��񣬷��򲻼���
		String sex = "Ů";
		int count = 40;
		if(sex.equals("��"))
		{
			if(count >= 50)
				System.out.println("����");
			else
				System.out.println("������");
		}
		else
		{
			if(count >= 30)
				System.out.println("����");
			else
				System.out.println("������");			
		}
	}

}
