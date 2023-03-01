
public class Task01 {

	public static void main(String[] args) {
		// 用循环的方法计算1+2+3+4+5+。。。100的和。
		int sum = 0;
		for (int i = 1; i <= 100; i++) {
			sum = sum + i;
		}
		System.out.println(sum);
	}

}
