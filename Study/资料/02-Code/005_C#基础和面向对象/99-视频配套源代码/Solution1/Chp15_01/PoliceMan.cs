using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp15_01
{
    //警察
    class PoliceMan:Employee
    {
        //方法重写
        public override void Work()
        {
            Console.WriteLine("我正在努力抓小偷!");
        }
    }
}
