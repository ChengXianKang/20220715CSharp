import java.util.Scanner;
public class Task02 {
	public static void main(String[] args) {
		//	数组{10,20,30,40,50,60,70,80,90},用键盘输入一个数字，判断是否存在
		int[] arr = new int[] {10,20,30,40,50,60,70,80,90};
		Scanner input = new Scanner(System.in);
		System.out.println("请输入一个数字:");
		int num = input.nextInt();
		if(num < arr[0] || num >  arr[arr.length-1])
		{
			System.out.println("没有找到!");
			return;
		}
		int start = 0; //查找起点
		int end = arr.length-1; //查找的终点
		boolean result = false; //假设找不到
		while(start <= end)
		{
			int mid = (start+end)/2; //找到中间点
			if(num < arr[mid])
				end = mid-1;
			else if(num > arr[mid])
				start = mid+1;
			else
			{
				result = true;
				break;
			}
		}
		if(result == true)
			System.out.println("找到了");
		else
			System.out.println("没有找到");		
	}
}
