using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//委托作为参数传递
namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 生产并包装两个手机-常规写法
            //WrapFactory wrapFactory = new WrapFactory();
            //Box box1 = wrapFactory.WarpXiaoMi();
            //Console.WriteLine(box1.MyPhone.Name + "生产及包装已经完成!");
            //Box box2 = wrapFactory.WarpHuawei();
            //Console.WriteLine(box2.MyPhone.Name + "生产及包装已经完成!");
            #endregion

            #region 生产并包装两个手机-委托作为参数
            PhoneFactory phoneFactory = new PhoneFactory();
            MakePhone makeXiaomi = new MakePhone(phoneFactory.MakeXiaoMi);
            MakePhone makeHuawei = new MakePhone(phoneFactory.MakeHuawei);
            WrapFactory wrapFactory = new WrapFactory();
            Box box1 = wrapFactory.WarpPhone(makeXiaomi);
            Console.WriteLine(box1.MyPhone.Name + "生产及包装已经完成!");
            Box box2 = wrapFactory.WarpPhone(makeHuawei);
            Console.WriteLine(box2.MyPhone.Name + "生产及包装已经完成!");
            #endregion

        }
    }
}
