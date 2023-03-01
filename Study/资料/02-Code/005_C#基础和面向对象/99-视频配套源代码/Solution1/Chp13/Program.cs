using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp13
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee();
            emp.Num = "0000001";
            emp.Name = "刘德华";
            emp.Sex = "中";
            emp.SayHi();
        }
    }
}
