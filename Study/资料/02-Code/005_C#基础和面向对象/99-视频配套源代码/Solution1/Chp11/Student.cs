using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp11
{
    class Student
    {
        //特性（名词）
        public string num; //学号
        public string name; //姓名
        public string sex;  //性别
        public int age;  //年龄

        //行为(动词)
        public void SayHi()
        {
            Console.WriteLine("大家好,我的学号是{0},姓名是{1},性别是{2},年龄是{3}",
                this.num,this.name,this.sex,this.age);
        }


    }
}
