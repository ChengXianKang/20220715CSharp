
public class Demo06 {

	public static void main(String[] args) {
		//�ַ������� IndexOf,lastIndexOf���������ַ������ַ����г��ֵ�λ�ã�
		
		//�໰����,���ҡ�������
//		String str = "Hell����o,Wo����rld";
//		int index = str.indexOf("����");   
//		System.out.println(index);         
		//�����ַ������ҵ�ʱ��û���ҵ�����-1
		//�ҵ��ˣ����ص�һ�����ҵ��ĵط���������������0��ʼ��
		
		//IndexOf������֮�󷵻ص�һ�����ֵ�λ�ã�lastIndexOf����֮�󷵻����һ�����ֵ�λ��
		String str = "Hell����o,Wo����rld";
		int index = str.lastIndexOf("����");   
		System.out.println(index); 
		
	}

}
