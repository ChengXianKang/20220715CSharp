
public class Demo02 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		rectArea(5,3);
		
		circleArea(5);
	}
	
	//求长方形面积(有参数，没有返回值)
	public static void rectArea(double c,double k)
	{
		double area = c*k;
		System.out.println("长方形面积为:" + area);
	}
	//求圆的面积(有参数，没有返回值)
	public static void circleArea(double r)
	{
		double area = 3.14*r*r;
		System.out.println("圆的面积为:"+area);
	}	

}
