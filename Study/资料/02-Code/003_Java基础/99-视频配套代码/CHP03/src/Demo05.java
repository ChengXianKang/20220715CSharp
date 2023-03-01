
public class Demo05 {

	public static void main(String[] args) {
		//判断条件相同，多分支去实现
		//判断条件不同，嵌套去实现
		//假设体育考试，考踢毽子
		//性别男，踢毽子50个以上及格，否则不及格
		//性别女，踢毽子30个以上及格，否则不及格
		String sex = "女";
		int count = 40;
		if(sex.equals("男"))
		{
			if(count >= 50)
				System.out.println("及格");
			else
				System.out.println("不及格");
		}
		else
		{
			if(count >= 30)
				System.out.println("及格");
			else
				System.out.println("不及格");			
		}
	}

}
