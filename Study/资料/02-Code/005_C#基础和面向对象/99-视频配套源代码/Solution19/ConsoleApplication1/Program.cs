using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Action委托:用于调用无参数，无返回值的函数
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //直接调用
            //OASystem oa = new OASystem();
            //oa.WelComeChinese();
            //oa.WelComeEnglish();


            //委托调用
            OASystem oa = new OASystem();
            Action action1 = new Action(oa.WelComeChinese);
            Action action2 = new Action(oa.WelComeEnglish);
            action1();       //或action1.Invoke();
            action2();      //或action2.Invoke();

        }
    }
}
