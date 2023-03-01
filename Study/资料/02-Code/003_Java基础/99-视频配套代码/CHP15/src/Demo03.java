import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

public class Demo03 {

	public static void main(String[] args) {
		// FileInputStream:�ֽ�������ж��ļ�
		FileInputStream fis = null;
		try {
			fis = new FileInputStream("C:\\JavaFile\\a.txt");
			int size = fis.available();  //ȡ�ļ���С���ֽڣ�
			byte[] arr = new byte[size];
			fis.read(arr);  //���ļ���ȡ���ֽ�����
			String str = new String(arr);   //�ֽ�����ת�ַ���
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
