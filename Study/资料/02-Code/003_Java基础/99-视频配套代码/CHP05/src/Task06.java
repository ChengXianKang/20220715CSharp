public class Task06 {
	public static void main(String[] args) 
	{
		int[][] arr = new int[][]{{1,2,3,4},{5,6,7,8},{9,10,11,12}};
		for (int i = 0; i < arr.length; i++) 
		{
			for (int j = 0; j < arr[i].length; j++) 
			{
				System.out.print(arr[i][j] + "\t");
			}
			System.out.println();
		}
		int[][] newArr = new int[4][3];
		for (int i = 0; i < arr.length; i++) 
		{
			for (int j = 0; j < arr[i].length; j++) 
			{
				newArr[j][i] = arr[i][j];
			}	
		}
		System.out.println("行列互换之后：");
		for (int i = 0; i < newArr.length; i++) 
		{
			for (int j = 0; j < newArr[i].length; j++) 
			{
				System.out.print(newArr[i][j] + "\t");
			}
			System.out.println();
		}
		
	}
}
