
public class Demo02 {

	public static void main(String[] args) {
		// ��ά����
		// ��ά����Ķ���
		//int[][] arr = new int[3][4];   //����⣬����һ��12��Ԫ�أ�3��4��
		
		//�������ά����ڶ��У��ڶ���Ԫ�ظ�ֵ
		//arr[1][1] = 26;
		
		//��ά����ĳ�ʼ��
		//int[][] arr = new int[][] {{1,2,3,4},{5,6,7,8},{9,10,11,12}};
		
		//arr.length:����ĳ��ȣ������һ������
		//arr[0].length:�����һ�еĳ��ȣ�����ǵ�һ��һ������Ԫ��
		
		//ѭ����ӡ��ά���飬�����е���ʽ��ӡ
		int[][] arr = new int[][] {{1,2,3,4},{5,6,7,8},{9,10,11,12}};
		for (int i = 0; i < arr.length; i++) {
			for (int j = 0; j < arr[i].length; j++) {
				System.out.print(arr[i][j] + "\t");
			}
			System.out.println();
		}
		

	}

}
