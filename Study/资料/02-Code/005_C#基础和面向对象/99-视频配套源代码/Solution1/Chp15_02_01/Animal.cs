using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp15_02_01
{
    class Animal
    {
        public string Name { get; set; } //名字
        //说话,虚方法，给子类重写
        public virtual void Speaking()
        {
            Console.WriteLine("hello!");
        }

    }
}
