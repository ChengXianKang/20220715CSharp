import java.text.SimpleDateFormat;
import java.util.Date;
public class Demo06 {
	public static void main(String[] args) {
		// switch...case (条件是进行相等比较的时候使用)，假设有课表，按星期进行排课
		//星期一：语文，数学，英语
		//星期二：语文，体育，音乐
		//星期三：数学，体育，音乐
		//星期四：历史，体育，音乐
		//星期五：地理，体育，音乐
		//星期六和日：休息
		//程序打开，打印当天课表
		Date date = new Date();
		SimpleDateFormat df = new SimpleDateFormat("E");
		String weekday = df.format(date);
		//switch括号里面内容可以是变量，也可是是表达式
		//值必须是整数或者可以转换成整数的类型，byte short int char，枚举，高版本jdk支持String
		switch (weekday) {
		case "星期一":
			System.out.println("语文，数学，英语"); break;
		case "星期二":
			System.out.println("语文，体育，音乐");break;
		case "星期三":
			System.out.println("数学，体育，音乐");break;
		case "星期四":
			System.out.println("历史，体育，音乐");break;
		case "星期五":
			System.out.println("地理，体育，音乐");break;
		case "星期六":
		case "星期日":
			System.out.println("休息");break;
		default:
			System.out.println("系统异常"); break;
		}

	}

}
