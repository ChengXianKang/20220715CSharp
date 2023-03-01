using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp09
{
    class Program
    {
        static void Main(string[] args)
        {
            //PrintHello();
            //PrintHello();
            //GetYuanArea(5);  //求半径为5的圆面积
            //GetCfxArea(6, 4); //求长方形的面积
            //求圆面积和长方形面积的总和
            //double area = GetYuanArea(5) + GetCfxArea(6, 4);
            //Console.WriteLine("面积总和:" + area);

            //int a = 1;
            //int b = 2;
            //Swap1(a, b);
            ////此时没有交换，值类型作为参数传递，函数里面变化，外面不变
            //Console.WriteLine("a={0},b={1}", a, b);

            //int a = 1;
            //int b = 2;
            //Swap2(ref a, ref b);
            ////此时有交换，ref关键字，代表传递的不是值，而是变量的引用
            ////参数传递，函数里面参数发生变化，外面一起变化
            //Console.WriteLine("a={0},b={1}", a, b);

            //数组名做为参数，实际上传递的是引用
            //数组名做为参数，函数里面数组元素值发生变化，外面一起变化
            int[] arr = new int[5] { 1, 2, 3, 4, 5 };
            Swap3(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + "\t");
            }

        }

        #region 没有参数，没有返回值的方法
        //没有参数，没有返回值的方法
        static void PrintHello()
        {
            Console.WriteLine("您好，欢迎来到****管理系统!");
        }
        #endregion

        #region 有参数，没有返回值
        //有参数，没有返回值
        //static void GetYuanArea(double r) //根据圆半径求圆面积
        //{
        //    double area = 3.14 * r * r;
        //    Console.WriteLine("圆面积:" + area);
        //}
        //static void GetCfxArea(double c,double k) //根据长方形长和宽求圆面积
        //{
        //    double area = c*k;
        //    Console.WriteLine("长方形面积:"+area);
        //}
        #endregion

        #region 有参数，有返回值的函数
        //有参数，有返回值的函数
        //static double GetYuanArea(double r) //根据圆半径求圆面积
        //{
        //    double area = 3.14 * r * r;
        //    return area;
        //}
        //static double GetCfxArea(double c, double k) //根据长方形长和宽求圆面积
        //{
        //    double area = c * k;
        //    return area;
        //}
        #endregion

        static void Swap1(int a,int b)  //实现两个数交换
        {
            int c = a;
            a = b;
            b = c;
        }
        static void Swap2(ref int a, ref int b)  //实现两个数交换
        {
            int c = a;
            a = b;
            b = c;
        }
        static void Swap3(int[] arr)  //实现两个数交换
        {
            int c = arr[0];
            arr[0] = arr[1];
            arr[1] = c;
        }

    }
}
