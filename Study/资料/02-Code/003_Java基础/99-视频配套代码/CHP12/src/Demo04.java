import java.util.Scanner;

public class Demo04 {

	public static void main(String[] args) {
		// �Զ����쳣
		try {
			Test();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			System.out.println("�������:"+e.getMessage());
		}
	}
	
	public static void Test() throws Exception 
	{
		Scanner input  = new Scanner(System.in);
		System.out.println("�������Ա�:");
		String sex = input.nextLine();
		if(!sex.equals("��") && !sex.equals("Ů"))
			throw new Exception("�Ա��쳣");
		else
			System.out.println(sex);
	}
	
}
