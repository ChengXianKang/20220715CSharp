using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp14
{
    class Animal
    {
        protected internal string Name { get; set; } //名字
        public Animal()
        {
            Console.WriteLine("我是Animal的构造函数");
        }
        public Animal(string temp)
        {
            
            Console.WriteLine("我是Animal的有参构造函数");
        }
    }
}
