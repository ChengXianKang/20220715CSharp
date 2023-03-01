
public class Demo08 {

	public static void main(String[] args) {
		// 字符串切割
		String str = "七月的风,八月的雨,卑微的我喜欢遥远的你";
		//把歌词切割，一句一行
		String[] strArr = str.split(",");
		for (int i = 0; i < strArr.length; i++) 
		{
			System.out.println(strArr[i]);
		}
		

	}

}
