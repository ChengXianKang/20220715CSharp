using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp16_02
{
    class Monkey : Animal, IClimbTree
    {
        public void ClimbTree()
        {
            Console.WriteLine("我用我的臂力爬树");
        }
    }
}
