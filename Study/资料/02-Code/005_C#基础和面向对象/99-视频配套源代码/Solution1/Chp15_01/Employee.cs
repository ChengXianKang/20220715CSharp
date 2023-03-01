using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp15_01
{
    //员工
    class Employee
    {
        //工作,提供给子类隐藏
        //public void Work()
        //{
        //    Console.WriteLine("我正在努力工作!");
        //}

        //工作，提供给子类重写,virtual代表虚函数
        public virtual void Work()
        {
            Console.WriteLine("我正在努力工作!");
        }
    }
}
