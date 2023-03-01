using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chp11.School;
using Chp11.B;

namespace Chp11
{
    class Program
    {
        static void Main(string[] args)
        {
            Student stu = new Student();  //实例化对象
            stu.num = "000001";
            stu.name = "刘备";
            stu.sex = "男";
            stu.age = 20;
            stu.SayHi();

            //两个相互操作的类不在同一个空间下
            //方案一（全名）:
            //Chp11.School.Teacher teacher = new School.Teacher();
            //方案二(using)：
            //Teacher teacher = new Teacher();

            Employee emp = new Employee();

        }
    }
}
