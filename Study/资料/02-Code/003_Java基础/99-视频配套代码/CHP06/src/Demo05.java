
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
	//������Ϊ��������������ı�����Ԫ��ֵ���ڵ��õĵط��������һ��ı䡣
	//����������Ϊ��������������ı�ֵ���ڵ��õĵط��������һ��ı䡣���������ʹ��ݵ����ã���ָ�룩
	

}
