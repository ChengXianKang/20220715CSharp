using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp17
{
    class Program
    {
        static void Main(string[] args)
        {
            //ArrayList集合演示（有序）：

            //Student stu1 = new Student("0001", "刘备", "男");
            //Student stu2 = new Student("0002", "关羽", "男");
            //Student stu3 = new Student("0003", "大乔", "女");
            //Student stu4 = new Student("0004", "小乔", "女");
            //Student stu5 = new Student("0005", "张飞", "男");
            ////ArrayList集合的定义
            //ArrayList list = new ArrayList();
            ////添加元素
            //list.Add(stu1);
            //list.Add(stu2);
            //list.Add(stu3);
            //list.Add(stu4);
            //list.Add(stu5);
            ////获取元素个数
            //Console.WriteLine("集合元素个数："+list.Count);
            ////取第二个元素（ArrayList是有序集合）
            //Console.WriteLine("第二个元素:" + ((Student)list[1]).Name);
            ////遍历集合
            //Console.WriteLine("for循环遍历:");
            //for (int i = 0; i < list.Count; i++)
            //{
            //    Student tempStu = (Student)list[i];
            //    Console.WriteLine("学号:{0},姓名:{1},性别:{2}", 
            //        tempStu.Num,tempStu.Name,tempStu.Sex);
            //}
            //Console.WriteLine("foreach循环遍历:");
            //foreach (Student item in list)
            //{
            //    Console.WriteLine("学号:{0},姓名:{1},性别:{2}",
            //        item.Num, item.Name, item.Sex);
            //}
            //删除元素
            //list.Remove(stu3); //根据对象来删除
            //list.RemoveAt(2);  //根据下标编号来删除
            //Console.WriteLine("删除之后:");
            //foreach (Student item in list)
            //{
            //    Console.WriteLine("学号:{0},姓名:{1},性别:{2}",
            //        item.Num, item.Name, item.Sex);
            //}

            //插入元素
            //Student stu6 = new Student("0006","赵云","男");
            //list.Insert(2, stu6);
            //Console.WriteLine("插入元素之后:");
            //foreach (Student item in list)
            //{
            //    Console.WriteLine("学号:{0},姓名:{1},性别:{2}",
            //        item.Num, item.Name, item.Sex);
            //}

            //集合清除
            //list.Clear();

            //----------------------------------------------------------------
            //HashTable集合（无序,以键值对的方式进行存储）：
            Student stu1 = new Student("0001", "刘备", "男");
            Student stu2 = new Student("0002", "关羽", "男");
            Student stu3 = new Student("0003", "大乔", "女");
            Student stu4 = new Student("0004", "小乔", "女");
            Student stu5 = new Student("0005", "张飞", "男");
            //定义集合
            Hashtable list = new Hashtable();
            //添加元素(键值对方式进行添加，学号是键，学生对象是值)
            list.Add(stu1.Num, stu1);
            list.Add(stu2.Num, stu2);
            list.Add(stu3.Num, stu3);
            list.Add(stu4.Num, stu4);
            list.Add(stu5.Num, stu5);
            //取某一个元素
            Console.WriteLine("学号为0003的人："+((Student)list["0003"]).Name);
            //循环遍历(遍历键，遍历值)
            Console.WriteLine("遍历键:");
            foreach (string item in list.Keys)
            {
                Console.Write(item + "  ");
            }
            Console.WriteLine();
            Console.WriteLine("遍历值:");
            foreach (Student item in list.Values)
            {
                Console.WriteLine("学号:{0},姓名:{1},性别:{2}",
                    item.Num, item.Name, item.Sex);
            }
            //删除
            list.Remove("0003");
            Console.WriteLine("删除之后:");
            foreach (Student item in list.Values)
            {
                Console.WriteLine("学号:{0},姓名:{1},性别:{2}",
                    item.Num, item.Name, item.Sex);
            }
            //清除集合
            list.Clear();

        }
    }
}
