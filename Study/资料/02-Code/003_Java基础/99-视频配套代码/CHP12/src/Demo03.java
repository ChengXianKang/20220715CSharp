
public class Demo03 {

	public static void main(String[] args) {
		// �쳣���׳�
		try {
			Test();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			System.out.println("�������:"+e.getMessage());
		}
	}

	public static void Test() throws Exception 
	{
		int a = 10 / 0;
	}	
}
