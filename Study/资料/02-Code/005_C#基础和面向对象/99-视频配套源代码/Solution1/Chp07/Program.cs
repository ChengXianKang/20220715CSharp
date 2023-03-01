using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp07
{
    class Program
    {
        static void Main(string[] args)
        {
            //for:实现打印1，2，3，4.。。。10
            //for (int i = 1; i <= 10; i++)
            //{
            //    Console.WriteLine(i);
            //}

            //计算1+2+3+100
            //int sum = 0;
            //for (int i = 1; i <= 100; i++)
            //{
            //    sum = sum + i;
            //}

            //计算机1+3+5+7+...99
            //int sum = 0;
            //for (int i = 1; i <= 99; i=i+2)
            //{
            //    sum = sum + i;
            //}
            //Console.WriteLine(sum);

            //循环退出：break;continue
            //break:退出整个循环
            //continue:退出当前本次循环，马上进入下一次循环
            //for (int i = 1; i <= 10; i++)
            //{
            //    if (i == 5)
            //        break;
            //    Console.WriteLine(i);
            //}

            //for (int i = 1; i <= 10; i++)
            //{
            //    if (i == 5)
            //        continue;
            //    Console.WriteLine(i);
            //}

            //循环嵌套
            //for (int i = 1; i <=3 ; i++)
            //{
            //    for (int j = 1; j <=3; j++)
            //    {
            //        Console.WriteLine("i={0},j={1}",i,j);
            //    }
            //}
            //嵌套循环执行顺序：外层处于某一个状态，里层全部执行完毕，再来改变外层循环的状态

            //打印九九乘法表
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("{0}*{1}={2}\t",i,j,i*j);
                }
                Console.WriteLine();
            }


        }
    }
}
