
public class Demo03 {

	public static void main(String[] args) {
		//�󳤷��������Բ������ܺ�
		double area = rectArea(5,3) + circleArea(5);
		System.out.println("����ܺ�:" + area);
	}
	
	//�󳤷������(�в������з���ֵ)
	public static double rectArea(double c,double k)
	{
		double area = c*k;
		return area;
	}
	//��Բ�����(�в������з���ֵ)
	public static double circleArea(double r)
	{
		double area = 3.14*r*r;
		return area;
	}	
	
}
