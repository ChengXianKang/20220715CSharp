using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//借助委托实现事件触发
namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {   
            EventTest et = new EventTest();
            et.ValueChanged += new EventTest.MyEventHandler(myEventFunction);
            et.Value = 100;
            et.Value = 200;

        }

        public static void  myEventFunction()
        {
            Console.WriteLine("value值在发生改变！");
        }


    }
}
