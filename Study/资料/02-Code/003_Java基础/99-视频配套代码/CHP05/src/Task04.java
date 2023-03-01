
public class Task04 {

	public static void main(String[] args) {
		int[] arr = new int[] {30,50,20,80,60};
		for (int i = 0; i < arr.length-1; i++) 
		{
			int minIndex = i; //假设最小值的下标为0
			for (int j = i+1; j< arr.length; j++) 
			{
				if(arr[minIndex] > arr[j])
					minIndex = j;
			}	
			int c = arr[minIndex];	arr[minIndex] = arr[i]; 		arr[i] = c;
		}
		for (int i = 0; i < arr.length; i++) 
		{
			System.out.print(arr[i] + "\t");
		}

	}

}
