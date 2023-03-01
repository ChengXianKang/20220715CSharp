using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp12
{
    class Student
    {
        #region 实例成员
        //public string num;  //学号
        //public void PrintInfo()
        //{
        //    Console.WriteLine("hello");
        //}
        #endregion

        #region 静态成员
        //public static string num;  //学号
        //public static void PrintInfo()
        //{
        //    Console.WriteLine("hello");
        //}
        #endregion

        public string num;  //学号
        public string name; //姓名
        public static int count = 0;   //学生人数
    }
}
