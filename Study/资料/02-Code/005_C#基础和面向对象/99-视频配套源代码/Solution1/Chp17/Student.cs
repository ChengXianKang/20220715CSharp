using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp17
{
    class Student
    {
        public Student(string num,string name,string sex)
        {
            this.Num = num;
            this.Name = name;
            this.Sex = sex;
        }
        public string Num { get; set; } //学号
        public string Name { get; set; } //姓名
        public string Sex { get; set; } //性别
    }
}
