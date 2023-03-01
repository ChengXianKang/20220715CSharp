using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp04
{
    class Program
    {
        static void Main(string[] args)
        {

            //输入身份证18号码，18岁以上，提示“欢迎光临”
            //身份证：420107198605064133
            //Console.WriteLine("请输入身份证号码:");
            //string code = Console.ReadLine();
            //int birthYear = int.Parse(code.Substring(6, 4));
            //int age = DateTime.Now.Year - birthYear;
            //if (age >= 18)
            //{
            //    Console.WriteLine("欢迎光临");
            //}

            //输入身份证18号码，18岁以上，提示“欢迎光临”，18岁以下，提示“好好学习”
            //Console.WriteLine("请输入身份证号码:");
            //string code = Console.ReadLine();
            //int birthYear = int.Parse(code.Substring(6, 4));
            //int age = DateTime.Now.Year - birthYear;
            //if (age >= 18)   //如果条件成立
            //{
            //    Console.WriteLine("欢迎光临");
            //}
            //else  //否则
            //{
            //    Console.WriteLine("好好学习");
            //}

            //输入学生成绩：0-29:重修,30-59:补考，60-79：良好，80-100：优秀
            //Console.WriteLine("请输入分数:");
            //int score = int.Parse(Console.ReadLine());
            //if (score >= 80)
            //    Console.WriteLine("优秀");
            //else if (score >= 60)
            //    Console.WriteLine("良好");
            //else if (score >= 30)
            //    Console.WriteLine("补考");
            //else
            //    Console.WriteLine("重修");

            //体育考试：踢毽子考试，性别男：30个及格，女生40个及格。
            //输入性别和个数，给出是否及格的结果
            Console.WriteLine("请输入性别:");
            string sex = Console.ReadLine();
            Console.WriteLine("请输入踢毽子个数:");
            int count = int.Parse(Console.ReadLine());
            if (sex.Equals("男"))
            {
                if (count >= 30)
                {
                    Console.WriteLine("及格");
                }
                else
                {
                    Console.WriteLine("不及格");
                }
            }
            else
            {
                if (count >= 40)
                {
                    Console.WriteLine("及格");
                }
                else
                {
                    Console.WriteLine("不及格");
                }
            }
        }
    }
}
