
public class Task03 {

	public static void main(String[] args) {
		// ��100-999֮�������ˮ�ɻ�����ˮ�ɻ�����ָһ�� n λ�� ( n��3 )������ÿ��λ�ϵ����ֵ� n ����֮�͵���������
		// �����磺1^3 + 5^3+ 3^3 = 153����
		//System.out.println(Math.pow(2, 3));
		
		for (int i = 100; i <= 999; i++) {
			int b = i/100;   //��λ
			int s = i/10%10; //ʮλ
			int g = i%10;  //��λ
			if(b*b*b + s*s*s + g*g*g == i)
				System.out.println(i);
		}
	}

}
