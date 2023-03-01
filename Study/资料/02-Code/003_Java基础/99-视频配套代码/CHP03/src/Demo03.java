public class Demo03 {
	public static void main(String[] args) {
		//多分支
		//假设程序接受了一个人分数，
		//30以下:重修 , 30-60:补考，60-80：良好，80-100：优秀
		int score = 50;
		if(score < 30)
			System.out.println("重修");
		else if(score < 60)
			System.out.println("补考");
		else if(score < 80)
			System.out.println("良好");
		else 
			System.out.println("优秀");
		

	}

}
