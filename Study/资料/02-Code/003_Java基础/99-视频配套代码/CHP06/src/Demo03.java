
public class Demo03 {

	public static void main(String[] args) {
		//求长方形面积和圆面积的总和
		double area = rectArea(5,3) + circleArea(5);
		System.out.println("面积总和:" + area);
	}
	
	//求长方形面积(有参数，有返回值)
	public static double rectArea(double c,double k)
	{
		double area = c*k;
		return area;
	}
	//求圆的面积(有参数，有返回值)
	public static double circleArea(double r)
	{
		double area = 3.14*r*r;
		return area;
	}	
	
}
