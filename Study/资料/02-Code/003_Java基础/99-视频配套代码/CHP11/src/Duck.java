//鸭子
public class Duck extends Animal implements ISwim
{
	public void Eating()
	{
		System.out.println("我正在吃虫子！");
	}
	public void Swim()
	{
		System.out.println("我用鸭掌游泳!");
	}
}
