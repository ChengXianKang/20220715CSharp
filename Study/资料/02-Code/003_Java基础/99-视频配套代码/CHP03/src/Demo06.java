import java.text.SimpleDateFormat;
import java.util.Date;
public class Demo06 {
	public static void main(String[] args) {
		// switch...case (�����ǽ�����ȱȽϵ�ʱ��ʹ��)�������пα������ڽ����ſ�
		//����һ�����ģ���ѧ��Ӣ��
		//���ڶ������ģ�����������
		//����������ѧ������������
		//�����ģ���ʷ������������
		//�����壺��������������
		//���������գ���Ϣ
		//����򿪣���ӡ����α�
		Date date = new Date();
		SimpleDateFormat df = new SimpleDateFormat("E");
		String weekday = df.format(date);
		//switch�����������ݿ����Ǳ�����Ҳ�����Ǳ��ʽ
		//ֵ�������������߿���ת�������������ͣ�byte short int char��ö�٣��߰汾jdk֧��String
		switch (weekday) {
		case "����һ":
			System.out.println("���ģ���ѧ��Ӣ��"); break;
		case "���ڶ�":
			System.out.println("���ģ�����������");break;
		case "������":
			System.out.println("��ѧ������������");break;
		case "������":
			System.out.println("��ʷ������������");break;
		case "������":
			System.out.println("��������������");break;
		case "������":
		case "������":
			System.out.println("��Ϣ");break;
		default:
			System.out.println("ϵͳ�쳣"); break;
		}

	}

}
