
public class Demo05 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int[] arr = new int[] {1,2,3,4,5};
		test(arr);
		for (int i = 0; i < arr.length; i++) {
			System.out.println(arr[i]);
		}
	}
	
	public static void test(int[] a)
	{
		a[0] = a[0]+1;
		a[1] = a[1]+1;
	}
	//数组作为参数，方法里面改变数组元素值，在调用的地方，会跟着一起改变。
	//引用类型作为参数，方法里面改变值，在调用的地方，会跟着一起改变。（引用类型传递的引用，是指针）
	

}
