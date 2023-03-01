import java.util.Scanner;

public class Demo04 {

	public static void main(String[] args) {
		// 自定义异常
		try {
			Test();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			System.out.println("程序错误:"+e.getMessage());
		}
	}
	
	public static void Test() throws Exception 
	{
		Scanner input  = new Scanner(System.in);
		System.out.println("请输入性别:");
		String sex = input.nextLine();
		if(!sex.equals("男") && !sex.equals("女"))
			throw new Exception("性别异常");
		else
			System.out.println(sex);
	}
	
}
