
public class Task03 {

	public static void main(String[] args) {
		// 求100-999之间的所有水仙花数（水仙花数是指一个 n 位数 ( n≥3 )，它的每个位上的数字的 n 次幂之和等于它本身。
		// （例如：1^3 + 5^3+ 3^3 = 153））
		//System.out.println(Math.pow(2, 3));
		
		for (int i = 100; i <= 999; i++) {
			int b = i/100;   //百位
			int s = i/10%10; //十位
			int g = i%10;  //个位
			if(b*b*b + s*s*s + g*g*g == i)
				System.out.println(i);
		}
	}

}
