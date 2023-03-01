
public class Demo01 {

	public static void main(String[] args) {
		//异常处理的基本结构，基本语法
		try
		{
			int a = 10;
			int b = 0;
			System.out.println(a/b);
		}
		catch(Exception ex)
		{
			System.out.println("程序出错了:"+ex.getMessage());
		}
		finally
		{
			System.out.println("欢迎使用此程序！");
		}
		
	}

}
