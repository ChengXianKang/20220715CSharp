using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp16_02
{
    class Cat:Animal,IClimbTree
    {
        public void ClimbTree()
        {
            Console.WriteLine("我用尖锐的爪子爬树");
        }
    }
}
