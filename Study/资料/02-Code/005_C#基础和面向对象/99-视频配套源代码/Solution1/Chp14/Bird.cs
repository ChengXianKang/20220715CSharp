using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp14
{
    class Bird:Animal
    {
        public Bird()
        {
            Console.WriteLine("我是Bird的构造函数");
        }
        public Bird(string temp):base(temp)
        {
            
            Console.WriteLine("我是Bird的有参构造函数");
        }
    }
}
