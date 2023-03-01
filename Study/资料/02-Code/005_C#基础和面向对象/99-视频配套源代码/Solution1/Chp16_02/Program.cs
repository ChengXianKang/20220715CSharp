using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp16_02
{
    class Program
    {
        static void Main(string[] args)
        {

            //Employee emp = new Doctor();
            //emp.Working();

            Duck duck = new Duck();
            duck.Eat();
            duck.Swim();

            Cat cat = new Cat();
            cat.Eat();
            cat.ClimbTree();


        }
    }
}
