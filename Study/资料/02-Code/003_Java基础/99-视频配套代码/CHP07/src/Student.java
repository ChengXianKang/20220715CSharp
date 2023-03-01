public class Student 
{
	//字段
	public String stuNO;  //学号
	public String stuName;  //姓名
	public String stuSex; //性别
	//方法
	public void sayHi()  //自我介绍
	{
		System.out.println("大家好");
		System.out.println("我的学号是:"+this.stuNO);
		System.out.println("我的姓名是:" + this.stuName);
		System.out.println("我的性别是:" + this.stuSex);
	}
	
}
