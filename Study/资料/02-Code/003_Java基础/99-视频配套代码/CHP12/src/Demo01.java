
public class Demo01 {

	public static void main(String[] args) {
		//�쳣����Ļ����ṹ�������﷨
		try
		{
			int a = 10;
			int b = 0;
			System.out.println(a/b);
		}
		catch(Exception ex)
		{
			System.out.println("���������:"+ex.getMessage());
		}
		finally
		{
			System.out.println("��ӭʹ�ô˳���");
		}
		
	}

}
