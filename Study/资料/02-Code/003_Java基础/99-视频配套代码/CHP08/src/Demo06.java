
public class Demo06 {

	public static void main(String[] args) {
		//字符串查找 IndexOf,lastIndexOf（查找子字符串在字符串中出现的位置）
		
		//脏话查找,查找“垃圾”
//		String str = "Hell垃圾o,Wo垃圾rld";
//		int index = str.indexOf("垃圾");   
//		System.out.println(index);         
		//进行字符串查找的时候，没有找到返回-1
		//找到了，返回第一个被找到的地方的索引，索引从0开始数
		
		//IndexOf，查找之后返回第一个出现的位置，lastIndexOf查找之后返回最后一个出现的位置
		String str = "Hell垃圾o,Wo垃圾rld";
		int index = str.lastIndexOf("垃圾");   
		System.out.println(index); 
		
	}

}
