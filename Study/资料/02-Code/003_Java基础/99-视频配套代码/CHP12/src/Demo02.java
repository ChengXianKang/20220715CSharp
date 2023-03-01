
public class Demo02 {

	public static void main(String[] args) {
		// 如果针对不同的异常需要作出不同的处理，使用多个catch来进行捕捉
		try
		{
			//int a = 10/0;  //算数错误
			
			//下标越界
			//int[] arr = new int[] {10,20,30};
			//System.out.println(arr[3]);
			//类型转换错误
			//Object dog = new Dog();
			//Cat cat = (Cat)dog;
			//数字格式错误
			int i = Integer.parseInt("hello,world");
		}
		catch(ArithmeticException ex)
		{
			System.out.println("算数错误:"+ex.getMessage());
		}
		catch(ArrayIndexOutOfBoundsException ex)
		{
			System.out.println("下标越界:"+ex.getMessage());
		}
		catch(ClassCastException ex)
		{
			System.out.println("类型转换错误:"+ex.getMessage());
		}
		catch(NumberFormatException ex)
		{
			System.out.println("数字格式错误:"+ex.getMessage());
		}
		catch(Exception ex)
		{
			System.out.println("程序错误:" + ex.getMessage());
		}
		finally
		{
			System.out.println("欢迎使用此程序！");
		}

	}

}
