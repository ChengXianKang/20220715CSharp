using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp18
{
    class Program
    {
        static void Main(string[] args)
        {
            //Student stu1 = new Student("0001", "刘备");
            //Student stu2 = new Student("0002", "公孙瓒");
            //Teacher th = new Teacher("卢植", 30000);
            //ArrayList list = new ArrayList();
            //list.Add(stu1);
            //list.Add(stu2);
            //list.Add(th);
            ////遍历
            //foreach (Student item in list)
            //{
            //    Console.WriteLine("学号:{0},姓名:{1}",item.Num,item.Name);
            //}

            //Student stu1 = new Student("0001", "刘备");
            //Student stu2 = new Student("0002", "公孙瓒");
            //Teacher th = new Teacher("卢植", 30000);
            //List<Student> list = new List<Student>();
            //list.Add(stu1);
            //list.Add(stu2);
            ////list.Add(th);
            ////遍历
            //foreach (Student item in list)
            //{
            //    Console.WriteLine("学号:{0},姓名:{1}", item.Num, item.Name);
            //}

            Student stu1 = new Student("0001", "刘备");
            Student stu2 = new Student("0002", "公孙瓒");
            Teacher th = new Teacher("卢植", 30000);
            Dictionary<string, Student> list = new Dictionary<string, Student>();
            list.Add(stu1.Num, stu1);
            list.Add(stu2.Num, stu2);
            //list.Add(th.Name,th);
            foreach (Student item in list.Values)
            {
                Console.WriteLine("学号:{0},姓名:{1}", item.Num, item.Name);
            }
        }
    }
}
