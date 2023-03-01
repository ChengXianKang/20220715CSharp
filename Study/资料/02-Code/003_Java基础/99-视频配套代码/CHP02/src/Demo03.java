
public class Demo03 {

	public static void main(String[] args) {
		// 数据类型的转换（（1）自动转换 （2）强制转换）
		
		//测试
//		int a = 10;
//		long b = a;
//		System.out.println(b);
		//int转换成long可以自动的
		
//		long a = 10;
//		int b = a;
//		System.out.println(b);		
		//long转换成int不可以自动的
		
		//表示范围小的数据类型可以自动转换到表示范围大的数据类型
		//表示范围大的数据类型不可以自动转换到表示范围小的数据类型
		
		//（1）自动转换
//		short a = 50;
//		int b = a;
//		System.out.println(b);
		
//		int a = 50;
//		double b = a;
//		System.out.println(b);
		
		//强制转换
//		double a = 50.5;
//		int b = (int)a;
//		System.out.println(b);
		
		//数据类型转换不兼容性
		//1.数字和布尔 不能相互转换
		//2.字符和布尔 不能相互转换
		//3.字符可以自动转换成数字，数字如果转字符，需要强制转换的
		
		//备注：字符和数字相互转换，使用 ASCII 码进行转换
//		char myChar = 'a';
//		int i = myChar;
//		System.out.println(i);
		
		int i = 97;
		char myChar = (char)i;
		System.out.println(myChar);
		
		
		

	}

}
