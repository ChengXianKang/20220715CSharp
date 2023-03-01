
public class Demo05 {

	public static void main(String[] args) {
		// 循环嵌套
//		for(int i=1;i<=10;i++)
//		{
//			for(int j=1;j<=10;j++)
//			{
//				System.out.println("i="+i+",j="+j);
//			}
//		}
		//循环嵌套：当外层循环处于某一个状态的时候，先执行内层循环，内存循环执行完成，才去改变外层循环
		//规律:i=1,j=1,2,3,4,5,6,7,8,9,10  ;i=2,j=1,2,3,4,5,6,7,8,9,10,...........
		
		
		//利用循环嵌套打印九九乘法表
		for(int i=1;i<=9;i++)
		{
			for(int j=1;j<=i;j++)
			{
				System.out.print(i+"*"+j+"="+i*j +"\t");
			}
			System.out.println();
		}	
	}

}
