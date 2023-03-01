import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Scanner;

public class Demo04 {

	public static void main(String[] args) {
		// 运算符
		//1-算术运算符( +  -  *  / %)
		
//		int a = 2;
//		int b = 3;
//		int c = a+b;
//		System.out.println(c);  //5
		//备注：数字相加和数学加法一致
		
//		String a = "2";
//		String b = "3";
//		String c = a+b;
//		System.out.println(c);  //23
		//备注：当+左边和右边为字符串，+运算进行拼接
		
//		int a = 2;
//		String b = "3";
//		System.out.println(a+b);  //23
		//备注：数字和字符串进行+运算，仍然是拼接
		
//		int a = 5;
//		int b = 2;
//		System.out.println(a/b);  //2
		//备注：两个整数相除，结果还是整数。(两个数据类型一致的数据进行运算，得到的结果仍然是这个数据类型)
		
//		double a = 5.0;
//		int b = 2;
//		System.out.println(a/b);
		//备注：小数和整数除法，结果是小数（两个数据类型不一致的的数据运算，结果是范围大的数据类型）
	
		//---------------------------------------------------------------------------------------
		//2-自增，自减运算符(++(自动+1)    --（自动-1）)
//		int a= 5;
//		++a;
//		System.out.println(a);
		
		//自增运算符++写在前面，先自增，后运算
		//自增运算符++写在后面，先运算，后自增
//		int a = 5;
//		int b = a++;
//		System.out.println("a="+a+",b="+b);    //a=6,b = 5
		
//		int a = 5;
//		int b = ++a;
//		System.out.println("a="+a+",b="+b);    //a=6,b = 6
		
		
		//----------------------------------------------------------------------
		//3-赋值（= += -= *=  /=  %=）
		// a+=b  等价  a = a+b;
//		int a = 5;
//		int b = 3;
//		a+=b;
//		System.out.println(a);
		
		//4-比较运算符(==  != >  <  >=  <=)
		//一个=代表赋值，两个==代表比较是否相等
		//数字比较
//		int a = 5;
//		int b = 5;
//		System.out.println(a==b);
		
		//字符串比较
//		String a = "hello";
//		String b = "hello";
//		System.out.println(a==b);
		
		//输入字符串比较
//		Scanner input  = new Scanner(System.in);
//		System.out.println("请输入第一个字符串:");
//		String a = input.nextLine();
//		System.out.println("请输入第二个字符串:");
//		String b = input.nextLine();	
//		System.out.println(a==b);
		
		//字符串比较不要使用==,使用equals
//		Scanner input  = new Scanner(System.in);
//		System.out.println("请输入第一个字符串:");
//		String a = input.nextLine();
//		System.out.println("请输入第二个字符串:");
//		String b = input.nextLine();	
//		System.out.println(a.equals(b));		
		
		//----------------------------------------------------------
		//5-逻辑运算符
		//(1) &&:逻辑与，两个条件都成立，表达式成立
		//(2) ||:逻辑或，两个条件有一个成立，表达式成立
		//(3) !:逻辑非 ，条件成立，表达式不成立；条件不成立，表达式成立
		
//		int a = 20;
//		int b = 50;
//		System.out.println(a>b && a>0);   //false
//		System.out.println(a>b || a> 0);    //true
//		System.out.println(!(a>b));    //true
		
		//6-三目运算符( ? : )
		// 条件?结果1:结果2
		//特点，条件成立，取结果1，条件不成立，取结果2
		//用户输入两个数字，求比较大的那一个。
//		Scanner input  = new Scanner(System.in);
//		System.out.println("请输入第一个数字:");
//		int a = input.nextInt();
//		System.out.println("请输入第二个数字:");
//		int b = input.nextInt();
//		int c = a>b?a:b;
//		System.out.println(c);
		
		
		
		

		
		
		
		
		
		
		

	}

}
