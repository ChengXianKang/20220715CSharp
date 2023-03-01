import java.io.File;
import java.io.IOException;

public class Demo02 {

	public static void main(String[] args) {
		// File文件创建
//		File f = new File("C:\\JavaFile\\b.txt");
//		if(!f.exists())
//		{
//			try {
//				f.createNewFile();   //创建文件
//			} catch (IOException e) {
//				// TODO Auto-generated catch block
//				e.printStackTrace();
//			}
//		}
		
		// File文件夹创建
//		File f = new File("C:\\JavaFile\\MyFile");
//		if(!f.exists())
//		{
//			f.mkdirs();
//		}
		
		//删除文件
//		File f = new File("C:\\JavaFile\\b.txt");
//		if(f.exists())
//		{
//			f.delete();  //删除
//		}
		
		//文件夹删除(只能删除空文件夹)
		File f = new File("C:\\JavaFile\\MyFile");
		if(f.exists())
		{
			f.delete();
		}

	}

}
