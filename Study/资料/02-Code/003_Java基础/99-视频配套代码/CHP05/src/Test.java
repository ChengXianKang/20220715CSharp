import java.util.Scanner;

public class Test {

	public static void main(String[] args) {
		int[] arr = new int[] {30,50,20,80,60};
		for (int i = 0; i < arr.length-1; i++) 
		{
			int removeNum = arr[i+1];   //从数组抽出的数字
			int prepareIndex = i; //比较的开始位置，进行--操作，依次往前移动
			for (prepareIndex = i; prepareIndex >= 0; prepareIndex--) 
			{
				if(removeNum < arr[prepareIndex])  //如果抽取数字小于循环中的数字，将循环中的数字后移
					arr[prepareIndex+1] = arr[prepareIndex];
				else //如果抽取数字大于等于循环中的数字，则退出循环，在循环外将抽取的数字插入空位 
					break;
			}
			arr[prepareIndex+1] = removeNum;		
		}
		//打印排序后结果
		for (int i = 0; i < arr.length; i++) 
			System.out.print(arr[i]+"\t");
	}

}
