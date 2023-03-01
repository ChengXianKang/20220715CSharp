using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp03
{
    class Program
    {
        static void Main(string[] args)
        {
            //运算符
            //(1)算数运算符

            //int a = 1;
            //int b = 2;
            //Console.WriteLine(a+b);   //结果：3(数学加法)

            //int a = 1;
            //string b = "2";
            //Console.WriteLine(a + b);  //结果：12（拼接）

            //int a = 5;
            //int b = 2;
            //Console.WriteLine(a/b); //结果：2（相同类型运算后结果还是这个类型）
            //上述除法，直接取整，直接删掉小数部分

            //int a = 1;
            //a++;
            //Console.WriteLine(a);  //结果：2

            //int a = 1;
            //++a;
            //Console.WriteLine(a);  //结果：2

            //++在前，先自增，++在后，后自增
            //int a = 1;
            //int b = a++;
            //Console.WriteLine("a={0},b={1}",a,b);  //a=2,b=1

            //int a = 1;
            //int b = ++a;
            //Console.WriteLine("a={0},b={1}", a, b);  //a=2,b=2

            //(2)赋值运算符
            //int a = 1;
            //1 = a;  //报错，=左边不能是1，应该是变量

            //int a = 10;
            //int b = 20;
            //b += a;  //等价于b = b+a;
            //Console.WriteLine("a={0},b={1}", a, b); //a=10,b=30

            //(3)关系运算符
            //int a = 10;
            //int b = 20;
            //Console.WriteLine(a=b); //20(=代表赋值)

            //int a = 10;
            //int b = 20;
            //Console.WriteLine(a==b); //false

            //(4)逻辑运算符（&&-与 ||-或 !-非）
            //&&:当且仅当两个操作数都为真，条件才为真
            //||:如果任何两个操作数任何一个为真，条件为真
            //!:如果条件为true，结果false；如果条件false,结果true
            //int a = 10;
            //int b = 20;
            //Console.WriteLine(a<b && b>30);  //false
            //Console.WriteLine(a < b || b > 30);  //true
            //Console.WriteLine(!(a < b || b > 30));  //false

            //(5)三目运算符 条件?结果1：结果2
            //条件成立，结果1，否则，结果2
            //定义两个变量，并进行赋值，定义第三个变量取前面两个变量的最大值
            //int a = 10;
            //int b = 20;
            //int c = a > b ? a : b;
            //Console.WriteLine(c);

            //------------------------------------------------------------------------------------
            //数据类型转换(1-自动转换，2-强制转换)
            //自动转换：范围小，精度小的类型向范围大，精度大的类型转换
            //int a = 10;
            //long b = a;
            //Console.WriteLine(b);

            //float a = 10.1F;
            //double b = a;
            //Console.WriteLine(b);

            //强制转换
            //long a = 10;
            //int b = (int)a;
            //Console.WriteLine(b);

            //double a = 10.9;
            //int b = (int)a;
            //Console.WriteLine(b);  //结果10

            //字符和数字的转换
            //字符转数字（自动）
            //char c = 'A';
            //int b = c;
            //Console.WriteLine(b);  //65,证明A字符的ASCII码65

            //数字转字符（强制）
            //int b = 65;
            //char c = (char)b;
            //Console.WriteLine(c);

            //值类型和字符串
            //值--》字符串
            //int a = 10;
            //string b = a.ToString("F2");
            //Console.WriteLine(b);
            //字符串--》值
            string str = "10";
            int a = int.Parse(str);
            Console.WriteLine(a);


        }
    }
}
