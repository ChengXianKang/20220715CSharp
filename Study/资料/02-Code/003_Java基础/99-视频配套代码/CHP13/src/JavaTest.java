import org.apache.log4j.Logger;
import org.apache.log4j.PropertyConfigurator;

public class JavaTest {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		PropertyConfigurator.configure("log4j.properties");
		Logger log = Logger.getLogger(JavaTest.class.getName());
		log.error("�����������..................");
		log.warn("�����������...................");
		log.info("��������.....״��");
		log.debug("����debug��Ϣ");
		
		
	}

}
