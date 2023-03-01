using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Func委托:一种泛型委托，Func有一系列的重载。

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //直接调用任意一种运算
            //Calculator cal = new Calculator();
            //int result = cal.Add(1, 2);
            //Console.WriteLine(result);

            Calculator cal = new Calculator();
            Func<int, int, int> func = new Func<int, int, int>(cal.Add);
            int result = func(1, 2);     //或func.Invoke(1, 2);
            Console.WriteLine(result);
        }
    }
}
