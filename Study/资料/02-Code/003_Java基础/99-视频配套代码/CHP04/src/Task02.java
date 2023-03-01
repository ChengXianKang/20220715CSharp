
public class Task02 {

	public static void main(String[] args) {
		//将1998-2008年之间的闰年年份输出。闰年的条件为下面二者成立一个即为闰年：
		//（1）年份数能被4整除，而且不能被100整除。
		//（2）年份数能够被400整除。
		for (int year = 1998; year <= 2008 ; year++) 
		{
			if((year%4 == 0 && year%100 != 0) || year % 400 == 0)
				System.out.println(year);
		}
	}

}
