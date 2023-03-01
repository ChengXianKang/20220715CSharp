using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp06
{
    class Program
    {
        static void Main(string[] args)
        {
            //while循环打印1-10
            //int i = 1;
            //while (i <= 10)
            //{
            //    Console.WriteLine(i);
            //    i++;
            //}

            //do...while循环打印1-10
            //int i = 1;
            //do
            //{
            //    Console.WriteLine(i);
            //    i++;
            //} while (i <= 10) ;

            //while和do...while区别
            //while先判断条件，在执行循环体
            //do...while,先执行循环体，后判断条件
            //while循环有可能一次都不执行，do...while循环至少会执行一次

            //int i = 11;
            //while (i <= 10)  //一次都没有执行
            //{
            //    Console.WriteLine(i);
            //    i++;
            //}

            //int i = 11;
            //do
            //{
            //    Console.WriteLine(i);
            //    i++;
            //} while (i <= 10);

            //不停的输入数字，最后统计正数和负数的个数，如果输入0代表结束
            //方案一：
            //int zs = 0;
            //int fs = 0;
            //int num = 1;
            //while (num != 0)
            //{
            //    Console.WriteLine("请输入数字:");
            //    num = int.Parse(Console.ReadLine());
            //    if (num > 0)
            //        zs++;
            //    if (num < 0)
            //        fs++;
            //}
            //Console.WriteLine("正数个数:{0},负数个数:{1}",zs,fs);

            //方案二：
            int zs = 0;
            int fs = 0;
            while (true)
            {
                Console.WriteLine("请输入数字:");
                int num = int.Parse(Console.ReadLine());
                if (num > 0)
                    zs++;
                if (num < 0)
                    fs++;
                if (num == 0)
                    break;   //break用于退出整个循环
            }
            Console.WriteLine("正数个数:{0},负数个数:{1}", zs, fs);
            



        }
    }
}
