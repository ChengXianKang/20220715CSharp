using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp02
{
    class Program
    {
        static void Main(string[] args)
        {
            //1-变量和常量
            //变量
            //int age;  //变量的声明（int:数据类型  age:变量名）
            //age = 18; //变量赋值
            //int age = 18;  //声明+赋值
            //age = 19;
            //Console.WriteLine(age);

            //常量(使用const关键字，常量名习惯大写)
            //const double PI = 3.14;
            //PI = 3.15;

            //-------------------------------------------------------------------
            //数据类型：（1）值类型（基本类型）(2)引用类型
            //值类型:
            //(1)整数 byte(sbyte),short(ushort),int(uint),long(ulong)
            //(2)小数 float  double
            //(3)字符 char
            //(4)布尔 bool

            //基本语法演示
            //byte i = 20;
            //Console.WriteLine(i);
            //float f = 10.1F;   //给float类型赋值的时候需要加后缀F
            //Console.WriteLine(f);
            //char c = 'a';  //给char类型赋值，使用单引号，单引号里面只能是一个字符
            //Console.WriteLine(c);
            //bool b = false;  //给bool类型赋值，只能是true或者false
            //Console.WriteLine(b);

            //sizeof:获取数据类型占用空间；MaxValue:数据类型最大值;MinValue:数据类型最小值
            //Console.WriteLine("byte空间:" + sizeof(byte) + "最小值:" + byte.MinValue + "最大值:" + byte.MaxValue);
            //Console.WriteLine("sbyte空间:" + sizeof(sbyte) + "最小值:" + sbyte.MinValue + "最大值:" + sbyte.MaxValue);
            //Console.WriteLine("short空间:" + sizeof(short) + "最小值:" + short.MinValue + "最大值:" + short.MaxValue);
            //Console.WriteLine("ushort空间:" + sizeof(ushort) + "最小值:" + ushort.MinValue + "最大值:" + ushort.MaxValue);
            //Console.WriteLine("int空间:" + sizeof(int) + "最小值:" + int.MinValue + "最大值:" + int.MaxValue);
            //Console.WriteLine("uint空间:" + sizeof(uint) + "最小值:" + uint.MinValue + "最大值:" + uint.MaxValue);
            //Console.WriteLine("long空间:" + sizeof(long) + "最小值:" + long.MinValue + "最大值:" + long.MaxValue);
            //Console.WriteLine("ulong空间:" + sizeof(ulong) + "最小值:" + ulong.MinValue + "最大值:" + ulong.MaxValue);
            //Console.WriteLine("float空间:" + sizeof(float) + "最小值:" + float.MinValue + "最大值:" + float.MaxValue);
            //Console.WriteLine("double空间:" + sizeof(double) + "最小值:" + double.MinValue + "最大值:" + double.MaxValue);
            //Console.WriteLine("char空间:" + sizeof(char));
            //Console.WriteLine("bool空间:" + sizeof(bool));

            //string类型：特殊类型，一种特殊的引用类型
            //string str = "hello";  //使用双引号赋值，双引号里面可以存储多个字符
            //Console.WriteLine(str);

            //变量在Console打印
            //int lbAge = 50;
            //int gyAge = 60;
            //int zfAge = 30;
            ////拼接字符串打印变量的值
            //Console.WriteLine("刘备年龄:" + lbAge +",关羽年龄:"+gyAge+",张飞年龄:"+zfAge);
            ////使用占位符
            //Console.WriteLine("刘备年龄:{0},关羽年龄:{1},张飞年龄:{2}",lbAge,gyAge,zfAge);

            //----------------------------------------------------------------------------------------------
            //键盘输入
            Console.WriteLine("请输入您的姓名:");
            string name = Console.ReadLine();
            Console.WriteLine("请输入您的年龄:");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("您的姓名:"+name);
            Console.WriteLine("您的年龄:"+age);
            

        }
    }
}
