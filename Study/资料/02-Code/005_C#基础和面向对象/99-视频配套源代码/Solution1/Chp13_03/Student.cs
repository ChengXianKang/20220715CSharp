using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp13_03
{
    class Student
    {
        public string Num { get; set; }  //学号
        public string Name { get; set; }  //姓名
        public Student()
        {
            ;
        }
        public Student(string num,string name)
        {
            this.Num = num;
            this.Name = name;
        }

        public void SayHi()
        {
            Console.WriteLine("我是{0},我的学号是{1}",this.Name,this.Num);
        }


    }
}
