import java.io.File;
import java.io.IOException;

public class Demo02 {

	public static void main(String[] args) {
		// File�ļ�����
//		File f = new File("C:\\JavaFile\\b.txt");
//		if(!f.exists())
//		{
//			try {
//				f.createNewFile();   //�����ļ�
//			} catch (IOException e) {
//				// TODO Auto-generated catch block
//				e.printStackTrace();
//			}
//		}
		
		// File�ļ��д���
//		File f = new File("C:\\JavaFile\\MyFile");
//		if(!f.exists())
//		{
//			f.mkdirs();
//		}
		
		//ɾ���ļ�
//		File f = new File("C:\\JavaFile\\b.txt");
//		if(f.exists())
//		{
//			f.delete();  //ɾ��
//		}
		
		//�ļ���ɾ��(ֻ��ɾ�����ļ���)
		File f = new File("C:\\JavaFile\\MyFile");
		if(f.exists())
		{
			f.delete();
		}

	}

}
