using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp05
{
    class Program
    {
        static void Main(string[] args)
        {
            //星期一：语文、体育、英语
            //星期二：数学、化学、计算机
            //星期三：政治、历史、物理
            //星期四：语文、数学、英语
            //星期五：计算机、英语、美术
            //星期六和星期天：休息
            //输入数字，打印课表
            //Console.WriteLine("请输入星期几:");
            //int week = int.Parse(Console.ReadLine());
            //switch (week)
            //{
            //    case 1:
            //        Console.WriteLine("语文、体育、英语"); break;
            //    case 2:
            //        Console.WriteLine("数学、化学、计算机"); break;
            //    case 3:
            //        Console.WriteLine("政治、历史、物理"); break;
            //    case 4:
            //        Console.WriteLine("语文、数学、英语"); break;
            //    case 5:
            //        Console.WriteLine("计算机、英语、美术"); break;
            //    case 6:
            //    case 7:
            //        Console.WriteLine("休息"); break;
            //    default:
            //        Console.WriteLine("输入错误");
            //        break;
            //}

            //输入学生成绩：0 - 29:重修,30 - 59:补考，60 - 79：良好，80 - 100：优秀
            Console.WriteLine("请输入分数:");
            int score = int.Parse(Console.ReadLine());
            switch (score/10)
            {
                case 0:
                case 1:
                case 2:
                    Console.WriteLine("重修");
                    break;
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("补考");
                    break;
                case 6:
                case 7:
                    Console.WriteLine("良好");
                    break;
                case 8:
                case 9:
                case 10:
                    Console.WriteLine("优秀");
                    break;
                default:
                    Console.WriteLine("输入错误!");
                    break;
            }



        }
    }
}
