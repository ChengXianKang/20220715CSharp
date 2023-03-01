import java.io.File;

public class Demo01 {

	public static void main(String[] args) {
		// File获取文件或文件夹相关信息
		File f = new File("JavaFile\\a.txt");
		if(f.exists())   //是否存在
		{
			System.out.println("名字:" + f.getName());
			System.out.println("相对路径:" + f.getPath());
			System.out.println("物理路径:" + f.getAbsolutePath());
			System.out.println("文件大小:" + f.length() + "字节");
			
		}
	}

}
