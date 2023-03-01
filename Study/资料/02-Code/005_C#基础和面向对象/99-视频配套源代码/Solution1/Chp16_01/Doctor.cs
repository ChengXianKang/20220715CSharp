using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp16_01
{
    class Doctor:Employee
    {
        public override void Working()
        {
            Console.WriteLine("我正在努力为病人看病...");
        }
    }
}
