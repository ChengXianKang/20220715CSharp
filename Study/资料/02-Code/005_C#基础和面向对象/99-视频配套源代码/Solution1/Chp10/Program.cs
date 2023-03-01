using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10
{

    class Program
    {
        struct Hero  //名字，简介，攻击力，防御力
        {
            public string name;  //名字
            public string remark; //简介
            public int gongji;  //攻击力
            public int fangyu;  //防御力
        }
        //枚举的定义
        //enum OrderStatus {新订单=1,已发货,已收货 }
        static void Main(string[] args)
        {
            //使用枚举
            //下了订单
            //OrderStatus status = OrderStatus.新订单;
            //Console.WriteLine((int)status);
            ////发货之后
            //status = OrderStatus.已发货;
            //Console.WriteLine((int)status);
            ////收到快递
            //status = OrderStatus.已收货;
            //Console.WriteLine((int)status);

            Hero houyi;
            houyi.name = "后裔";
            houyi.remark = "攻速型的后期射手";
            houyi.gongji = 1000;
            houyi.fangyu = 20;
            Console.WriteLine("名字:{0},介绍:{1},攻击:{2},防御:{3}",
                houyi.name,houyi.remark,houyi.gongji,houyi.fangyu);

        }


    }
}
