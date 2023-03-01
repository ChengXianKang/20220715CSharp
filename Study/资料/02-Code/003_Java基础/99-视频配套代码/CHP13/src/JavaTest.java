import org.apache.log4j.Logger;
import org.apache.log4j.PropertyConfigurator;

public class JavaTest {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		PropertyConfigurator.configure("log4j.properties");
		Logger log = Logger.getLogger(JavaTest.class.getName());
		log.error("程序产生错误..................");
		log.warn("程序产生警告...................");
		log.info("程序发生了.....状况");
		log.debug("程序debug信息");
		
		
	}

}
