package com.demo03;
//»À
public class People 
{
	public void SayHi(Animal animal)
	{
		animal.Speaking();
	}
	
	public Animal BuyAnimal(int type)
	{
		Animal animal;
		if(type == 1)
			animal = new Cat();
		else if(type == 2)
			animal = new Dog();
		else
		{
			animal = null;
		}
		return animal;
	}
	
}
