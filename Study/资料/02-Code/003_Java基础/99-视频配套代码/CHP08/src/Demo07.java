
public class Demo07 {

	public static void main(String[] args) {
		// 字符串截取  420107 19880402 4156
		String str = "420107198804024156";
		//截取字符串，返回身份证中的出生年份
		String result = str.substring(6,10);
		System.out.println(result);
		

	}

}
