using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//委托与匿名函数实现回调
namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            //Student stu = new Student();
            //CallBackDelegate myDelegate = delegate (int score)
            //{
            //    if (score >= 60)
            //        Console.WriteLine("已经给" + stu.Name + "的邮箱:"
            //            + stu.Mail + "发送了录用入职邀请！");
            //    else
            //        Console.WriteLine("很遗憾，我们不能录用您！");
            //};
            //stu.Exam();   //无参数调用
            //stu.Exam(myDelegate);  //有参数调用

            Student stu = new Student();
            //stu.Exam();
            //或
            stu.Exam((score) =>
            {
                if (score >= 60)
                    Console.WriteLine("已经给" + stu.Name + "的邮箱:" + 
                        stu.Mail + "发送了录用入职邀请！");
                else
                    Console.WriteLine("很遗憾，我们不能录用您！");
            });

        }
    }
}
