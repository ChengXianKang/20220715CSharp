using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp15_02_01
{
    class Dog:Animal
    {
        
        //重写
        public override void Speaking()
        {
            Console.WriteLine("旺旺旺.......");
        }
        //玩接飞盘
        public void CatchFlyDisc()
        {
            Console.WriteLine("我在和我的主人玩接飞盘的游戏");
        }
    }
}
