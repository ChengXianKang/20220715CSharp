using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp18
{
    class Student
    {
        public Student(string num, string name)
        {
            this.Num = num;
            this.Name = name;
        }
        public string Num { get; set; } //学号
        public string Name { get; set; } //姓名
    }
}
