
public class Demo03 {

	public static void main(String[] args) {
		// 异常的抛出
		try {
			Test();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			System.out.println("程序错误:"+e.getMessage());
		}
	}

	public static void Test() throws Exception 
	{
		int a = 10 / 0;
	}	
}
