using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication5
{
    [Serializable]
    class Student
    {
        public string StuNum { get; set; } //学号
        public string StuName { get; set; } //姓名
        public int StuAge { get; set; } //年龄
    }
}
