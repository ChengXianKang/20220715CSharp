using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//自定义委托及委托的多播（组播）
namespace ConsoleApplication4
{
    delegate void ChangeNum(int n);
    class Program
    {
        static void Main(string[] args)
        {
            //自定义委托调用函数方法
            //Calculator cal = new Calculator();
            //cal.Num = 100;
            //ChangeNum cnAdd = new ChangeNum(cal.Add);
            //cnAdd(10);
            //Console.WriteLine(cal.Num);

            //委托的多播（组播）
            Calculator cal = new Calculator();
            cal.Num = 100;
            ChangeNum cnAdd = new ChangeNum(cal.Add);
            ChangeNum cnSub = new ChangeNum(cal.Sub);
            ChangeNum cnMul = new ChangeNum(cal.Mul);
            ChangeNum cnDiv = new ChangeNum(cal.Div);

            ChangeNum cn = cnAdd;
            cn += cnMul;
            cn += cnSub;
            cn(50);
            Console.WriteLine(cal.Num);

        }
    }
}
