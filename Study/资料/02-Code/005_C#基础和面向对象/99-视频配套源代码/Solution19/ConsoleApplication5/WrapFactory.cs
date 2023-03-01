using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    delegate Phone MakePhone();
    class WrapFactory
    {
        #region 为每种手机进行包装盒工作-常规写法
        //包装小米手机
        public Box WarpXiaoMi()
        {
            PhoneFactory phoneFactory = new PhoneFactory();
            Phone phone = phoneFactory.MakeXiaoMi();
            Box box = new Box();
            box.MyPhone = phone;
            return box;
        }
        //包装华为手机
        public Box WarpHuawei()
        {
            PhoneFactory phoneFactory = new PhoneFactory();
            Phone phone = phoneFactory.MakeHuawei();
            Box box = new Box();
            box.MyPhone = phone;
            return box;
        }
        #endregion

        #region 将委托作为参数
        public Box WarpPhone(MakePhone mp)
        {
            Phone phone = mp();
            Box box = new Box();
            box.MyPhone = phone;
            return box;
        }
        #endregion

    }
}
