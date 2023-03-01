public class Task05 {
	public static void main(String[] args) {
		int[] arr = new int[] {30,50,20,80,60};
		for (int i = 0; i < arr.length-1; i++) 
		{
			int removeNum = arr[i+1]; //从数组中抽出的数字
			int j = 0;
			for (j = i; j >= 0; j--) 
			{
				if(removeNum < arr[j])
				{
					arr[j+1] = arr[j];
				}
				else
				{
					break;
				}
			}
			arr[j+1] = removeNum;
		}
		//打印排序后的结果
		for (int i = 0; i < arr.length; i++) 
		{
			System.out.print(arr[i] + "\t");
		}	
	}

}
