using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp08
{
    class Program
    {
        static void Main(string[] args)
        {
            //数组声明
            //int[] arr;

            //数组定义
            //arr = new int[5];

            //数组声明+定义
            //int[] arr = new int[5];

            //数组赋值
            //arr[0] = int.Parse(Console.ReadLine());  //通过键盘赋值
            //arr[1] = 60;  //直接赋值
            //arr[2] = 85;
            //arr[3] = 89;
            //arr[4] = 90;

            //数组取值
            //Console.WriteLine(arr[2]);

            //数组的初始化（定义的时候把进行赋值）
            //int[] arr = new int[5] { 50,60,80,70,90};
            //int[] arr = new int[] { 50, 60, 80, 70, 90 };
            //int[] arr = { 50, 60, 80, 70, 90 };

            //数组的循环遍历（把所有的值以此打印）
            //int[] arr = new int[5] { 50, 60, 80, 70, 90 };
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.WriteLine(arr[i]);
            //}

            //循环输入数组的值，循环打印数组的值
            //int[] arr = new int[5];
            ////循环输入
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.WriteLine("请输入第{0}个元素的值:",i+1);
            //    arr[i] = int.Parse(Console.ReadLine());
            //}
            //Console.WriteLine("输入完毕，数组内容为:");
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.WriteLine(arr[i]);
            //}

            //----------------------------------------------------------------
            //二维数组的定义(规则)
            //int[,] arr = new int[3, 4];
            //arr[0, 1] = 26; //代表第一行，第二列的元素进行赋值。
            //二维数组的初始化
            //int[,] arr = new int[3, 4] { {11,22,33,44 }, {24,25,26,27 }, { 31,32,35,36} };
            ////二维数组的遍历
            //for (int i = 0; i < arr.GetLength(0); i++)  //循环行
            //{
            //    for (int j = 0; j < arr.GetLength(1); j++) //循环列
            //    {
            //        Console.Write(arr[i,j]+"\t");
            //    }
            //    Console.WriteLine();
            //}

            //二维数组的定义(不规则)
            //int[][] arr = new int[3][];
            //arr[0] = new int[3];   //第一行有三个元素
            //arr[1] = new int[4];   //第二行有四个元素
            //arr[2] = new int[5];   //第三行有五个元素
            //arr[0][1] = 26; //代表第一行，第二列的元素进行赋值。

            //不规则二维数组的初始化
            int[][] arr = new int[3][] {
                new int[3] { 11,12,13},
                new int[4] { 22,33,44,55},
                new int[5] { 78,79,80,81,82}
            };
            //不规则二维数组的遍历
            for (int i = 0; i < arr.Length; i++)  //行
            {
                for (int j = 0; j < arr[i].Length; j++) //列
                {
                    Console.Write(arr[i][j] + "\t");
                }
                Console.WriteLine();
            }



        }
    }
}
