using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class PhoneFactory
    {
        public Phone MakeXiaoMi()
        {
            Phone phone = new Phone();
            phone.Name = "小米手机";
            return phone;
        }
        public Phone MakeHuawei()
        {
            Phone phone = new Phone();
            phone.Name = "华为手机";
            return phone;
        }
    }
}
