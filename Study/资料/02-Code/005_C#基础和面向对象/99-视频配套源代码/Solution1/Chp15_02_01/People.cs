using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp15_02_01
{
    class People
    {
        //和动物打招呼
        public void SayHi(Animal animal)
        {
            animal.Speaking();
        }

        public void PlayGame(Animal animal)
        {
            if (animal is Dog)
                ((Dog)animal).CatchFlyDisc();
            else if (animal is Cat)
                ((Cat)animal).HideAndSeek();
            else
                Console.WriteLine("参数传递错误");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">领养类型,1-猫，2-狗</param>
        /// <returns></returns>
        public Animal OwnAnimal(int type)
        {
            if (type == 1)
            {
                Cat cat = new Cat();
                cat.Name = "tom";
                return cat;
            }
            else if (type == 2)
            {
                Dog dog = new Dog();
                dog.Name = "snoopy";
                return dog;
            }
            else
            {
                return null;
            }
        }



    }
}
