
public class Demo02 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		rectArea(5,3);
		
		circleArea(5);
	}
	
	//�󳤷������(�в�����û�з���ֵ)
	public static void rectArea(double c,double k)
	{
		double area = c*k;
		System.out.println("���������Ϊ:" + area);
	}
	//��Բ�����(�в�����û�з���ֵ)
	public static void circleArea(double r)
	{
		double area = 3.14*r*r;
		System.out.println("Բ�����Ϊ:"+area);
	}	

}
