public class Task03 {
	public static void main(String[] args) {
//		int[] arr = new int[] {30,50,20,80,60};
//		for (int i = 0; i < arr.length-1; i++) 
//		{
//			for (int j = i+1; j < arr.length; j++) 
//			{
//				if(arr[i] > arr[j])
//				{
//					int c = arr[i];  arr[i]=arr[j]; arr[j] = c;
//				}
//			}
//		}
//		for (int i = 0; i < arr.length; i++) {
//			System.out.print(arr[i] + "\t");
//		}
		int[] arr = new int[] {30,50,20,80,60};
		for (int i = 0; i < arr.length-1 ; i++) 
		{
			for (int j = 0; j < arr.length-1-i; j++)
			{
				if(arr[j] > arr[j+1])
				{
					int c = arr[j]; 	arr[j] = arr[j+1];	arr[j+1] = c;
				}			
			}			
		}
		for (int i = 0; i < arr.length; i++) 
		{
			System.out.print(arr[i] + "\t");
		}
		
	}
}
