using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp13
{
    class Employee
    {
        //private代表私有，本类可以使用
        //public代表公有，任何地方都可以访问
        public string Num { get; set; }  //工号

        private string name; //名字
        public string Name
        {
            get { return name; }   //设置属性可读          
            set{ name = value; } //设置属性可写
        }

        private string sex;  //性别
        public string Sex
        {
            get { return sex; }   //设置属性可读
            //设置属性可写
            set
            {
                if (value.Equals("男") || value.Equals("女"))
                    sex = value;
                else
                    sex = "赋值错误!";
            }   
        }
        public void SayHi()
        {
            Console.WriteLine("大家好,我是{0},我的工号是{1},我的性别是{2}",Name,Num,Sex);
        }
    }
}
