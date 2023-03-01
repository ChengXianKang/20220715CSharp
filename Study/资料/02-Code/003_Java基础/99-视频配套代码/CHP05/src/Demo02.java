
public class Demo02 {

	public static void main(String[] args) {
		// 二维数组
		// 二维数组的定义
		//int[][] arr = new int[3][4];   //简单理解，数组一共12个元素，3行4列
		
		//如果给二维数组第二行，第二列元素赋值
		//arr[1][1] = 26;
		
		//二维数组的初始化
		//int[][] arr = new int[][] {{1,2,3,4},{5,6,7,8},{9,10,11,12}};
		
		//arr.length:数组的长度，求的是一共几行
		//arr[0].length:数组第一行的长度，求的是第一行一共几个元素
		
		//循环打印二维数组，以行列的形式打印
		int[][] arr = new int[][] {{1,2,3,4},{5,6,7,8},{9,10,11,12}};
		for (int i = 0; i < arr.length; i++) {
			for (int j = 0; j < arr[i].length; j++) {
				System.out.print(arr[i][j] + "\t");
			}
			System.out.println();
		}
		

	}

}
