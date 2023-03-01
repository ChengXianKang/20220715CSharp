using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Predicate:一个传入参数，返回值为bool

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            //直接调用两个方法
            //bool result = false;
            //MyDate myDate = new MyDate();
            //result = myDate.Is29(2020);
            //Console.WriteLine("闰年:"+result);
            //result = myDate.IsWork(new DateTime(2020, 7, 3));
            //Console.WriteLine("工作:"+result);

            //委托调用两个方法
            bool result = false;
            MyDate myDate = new MyDate();
            Predicate<int> pre1 = new Predicate<int>(myDate.Is29);
            result = pre1(2020); //或result = pre1.Invoke(2020);
            Console.WriteLine("闰年:" + result);
            Predicate<DateTime> pre2 = new Predicate<DateTime>(myDate.IsWork);
            result = pre2(new DateTime(2020, 7, 3)); //或result = pre2.Invoke(new DateTime(2020, 7, 3));
            Console.WriteLine("工作:" + result);
        }
    }
}
