
public class Demo02 {

	public static void main(String[] args) {
		// �����Բ�ͬ���쳣��Ҫ������ͬ�Ĵ���ʹ�ö��catch�����в�׽
		try
		{
			//int a = 10/0;  //��������
			
			//�±�Խ��
			//int[] arr = new int[] {10,20,30};
			//System.out.println(arr[3]);
			//����ת������
			//Object dog = new Dog();
			//Cat cat = (Cat)dog;
			//���ָ�ʽ����
			int i = Integer.parseInt("hello,world");
		}
		catch(ArithmeticException ex)
		{
			System.out.println("��������:"+ex.getMessage());
		}
		catch(ArrayIndexOutOfBoundsException ex)
		{
			System.out.println("�±�Խ��:"+ex.getMessage());
		}
		catch(ClassCastException ex)
		{
			System.out.println("����ת������:"+ex.getMessage());
		}
		catch(NumberFormatException ex)
		{
			System.out.println("���ָ�ʽ����:"+ex.getMessage());
		}
		catch(Exception ex)
		{
			System.out.println("�������:" + ex.getMessage());
		}
		finally
		{
			System.out.println("��ӭʹ�ô˳���");
		}

	}

}
