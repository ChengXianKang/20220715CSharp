using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp15_01
{
    //医生
    class Doctor:Employee
    {
        //方法隐藏
        //public new void Work()
        //{
        //    Console.WriteLine("我正在努力给病人看病!");
        //}

        //方法重写
        public override void Work()
        {
            Console.WriteLine("我正在努力给病人看病!");
        }

    }
}
