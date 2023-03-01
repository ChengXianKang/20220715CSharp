using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp16_01
{
    abstract class Employee
    {
        public void Meeting()
        {
            Console.WriteLine("我们正在开会!");
        }
        public abstract void Working();
    }
}
