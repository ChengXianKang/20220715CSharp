using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp16_02
{
    class Dog : Animal, ISwim
    {
        public void Swim()
        {
            Console.WriteLine("我用狗刨式游泳!");
        }
    }
}
