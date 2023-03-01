using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    delegate void CallBackDelegate(int score);
    class Student
    {
        public string Name { get; set; } //姓名
        public string Mail { get; set; } //邮箱

        public int Score { get; set; } //分数

        public void Exam(CallBackDelegate callback = null)
        {
            Console.WriteLine("请填写姓名:");
            this.Name = Console.ReadLine();
            Console.WriteLine("请填写邮箱:");
            this.Mail = Console.ReadLine();

            Random rnd = new Random();
            this.Score = rnd.Next(0, 101);
            Console.WriteLine("该学生面试成绩为:" + this.Score);
            if (callback != null)
                callback(this.Score);
        }
    }

}
