import java.io.File;
import java.io.IOException;

public class Demo05 {

	public static void main(String[] args) {
		// ������봦����쳣������������޷�ͨ��
		File file = new File("D:\\a.txt");
		try {
			file.createNewFile();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
