using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp15_02_01
{
    class Program
    {
        //模拟人和动物打招呼
        static void Main(string[] args)
        {
            //People p = new People();
            //Cat cat = new Cat();
            //p.SayHi(cat);

            //People p = new People();
            //Cat cat = new Cat();
            //p.PlayGame(cat);

            //People p = new People();
            //Dog dog = new Dog();
            //p.PlayGame(dog);

            //People p = new People();
            //Animal animal = p.OwnAnimal(1);
            //Console.WriteLine("恭喜您,领养了一只猫，猫的名字是"+animal.Name);

            People p = new People();
            Animal animal = p.OwnAnimal(2);
            Console.WriteLine("恭喜您,领养了一只狗，狗的名字是" + animal.Name);

        }

    }
}
