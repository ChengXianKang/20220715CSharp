using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp15_02_01
{
    class Cat:Animal
    {
        
        //重写
        public override void Speaking()
        {
            Console.WriteLine("喵喵喵.......");
        }
        //玩捉迷藏
        public void HideAndSeek()
        {
            Console.WriteLine("我和我的主人在玩捉迷藏!");
        }
    }
}
