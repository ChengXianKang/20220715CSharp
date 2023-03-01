import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

public class Demo03 {

	public static void main(String[] args) {
		// FileInputStream:字节数组进行读文件
		FileInputStream fis = null;
		try {
			fis = new FileInputStream("C:\\JavaFile\\a.txt");
			int size = fis.available();  //取文件大小（字节）
			byte[] arr = new byte[size];
			fis.read(arr);  //将文件读取到字节数组
			String str = new String(arr);   //字节数组转字符串
			System.out.println(str);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		finally
		{
			try {
				fis.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
	}

}
