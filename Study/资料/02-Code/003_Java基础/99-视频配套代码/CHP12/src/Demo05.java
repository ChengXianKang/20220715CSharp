import java.io.File;
import java.io.IOException;

public class Demo05 {

	public static void main(String[] args) {
		// 程序必须处理的异常，不处理编译无法通过
		File file = new File("D:\\a.txt");
		try {
			file.createNewFile();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
