import java.util.Scanner;

public class Demo01 {

	public static void main(String[] args) {
		//变量：存储数据的容器
		
		//变量声明（数据类型  变量名;）
		//int a;   //int代表数据类型为整型,a是变量名
		
		//变量赋值
//		int a;
//		a = 10;
		
		//声明+赋值
		//int a = 10;
		
		//变量名的命名规范
		//(1)只能由数字，字母，下划线，美元符号组成
		//int a%^%;   //错误，包含了除了数字，字母，下划线，美元符号以外的字符。
		
		//(2)第一个字母不能是数字
		//int 1a;  //错误
		
		//(3)不能是系统的关键字
		//int int; //错误
		
		//(4)建议：名字见名识义。
		//		(1)驼峰命名(变量存储学生姓名, studentName)
		//     (2)下划线(变量存储学生姓名, student_name)
		
		//声明三个变量存储自己的年龄，身高，体重，并且将变量的值打印出来
//		int age = 18;
//		int high = 180;
//		int weight = 180;
//		System.out.println("我的基本信息如下:");
//		System.out.println("年龄:"+age);
//		System.out.println("身高:"+high);
//		System.out.println("体重:"+weight);
		
		//由用户输入自己的年龄，身高，体重，并且将变量的值打印出来
		Scanner input = new Scanner(System.in);
		System.out.println("请输入您的年龄:");
		int $age = input.nextInt();
		System.out.println("请输入您的身高:");
		int high = input.nextInt();
		System.out.println("请输入您的体重:");
		int weight = input.nextInt();
		System.out.println("我的基本信息如下:");
		System.out.println("年龄:"+$age);
		System.out.println("身高:"+high);
		System.out.println("体重:"+weight);	
		
		
		
		
		
		
		
		
		
		

	}

}
