using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp12
{
    class Program
    {
        static void Main(string[] args)
        {
            //实例成员的调用
            //Student stu = new Student();
            //stu.num = "001";
            //stu.PrintInfo();

            //静态成员的调用
            //Student.num = "001";
            //Student.PrintInfo();

            Student stu1 = new Student();
            stu1.num = "001";
            stu1.name = "刘备";
            Student.count++;
            Student stu2 = new Student();
            stu2.num = "002";
            stu2.name = "关羽";
            Student.count++;
            Student stu3 = new Student();
            stu3.num = "003";
            stu3.name = "张飞";
            Student.count++;
            Console.WriteLine("学生人数:"+ Student.count);

        }
    }
}
