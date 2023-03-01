
public class Demo04 {

	public static void main(String[] args) {
		// 循环退出 --打印1,2,3，...10,当i==5的时候退出
//		for(int i=1;i<=10;i++)
//		{
//			if(i==5)
//				break;
//			System.out.println(i);
//		}
		//break退出整个循环，后面的循环次数不会被执行
		
		
		for(int i=1;i<=10;i++)
		{
			if(i==5)
				continue;
			System.out.println(i);
		}	
		//continue退出本次循环，马上跳过本次循环进入到下一次循环

	}

}
