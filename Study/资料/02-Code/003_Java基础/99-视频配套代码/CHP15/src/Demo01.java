import java.io.File;

public class Demo01 {

	public static void main(String[] args) {
		// File��ȡ�ļ����ļ��������Ϣ
		File f = new File("JavaFile\\a.txt");
		if(f.exists())   //�Ƿ����
		{
			System.out.println("����:" + f.getName());
			System.out.println("���·��:" + f.getPath());
			System.out.println("����·��:" + f.getAbsolutePath());
			System.out.println("�ļ���С:" + f.length() + "�ֽ�");
			
		}
	}

}
