using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp18
{
    class Teacher
    {
        public Teacher(string name,double salary)
        {
            this.Name = name;
            this.Salary = salary;
        }
        public string Name { get; set; } //名字
        public double Salary { get; set; } //月薪
    }
}
