
public class Demo02 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		// while循环-打印1,2,3，...10
		int i = 11;
		do
		{
			System.out.println(i);
			i++;
		}while(i<=10);
		//while和do...while区别
		//(1)while先判断条件，在执行代码；而do...while先执行代码，在判断条件
		//(2)while循环如果初始状态条件不成立，那么while循环体一次都不会执行；
		//    do...while，不管初始状态条件是否成立，do...while循环体内容至少执行一次
	}

}
