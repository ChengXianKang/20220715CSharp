import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

public class Demo04 {

	public static void main(String[] args) {
		// FileOutputStream:字节数组进行写文件
		FileOutputStream fos = null;
		try {
			fos = new FileOutputStream("C:\\JavaFile\\b.txt");
			String str = "Hello,Java";
			byte[] arr =str.getBytes();   //将字符串编程字节数组
			fos.write(arr);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		finally
		{
			try {
				fos.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		

	}

}
