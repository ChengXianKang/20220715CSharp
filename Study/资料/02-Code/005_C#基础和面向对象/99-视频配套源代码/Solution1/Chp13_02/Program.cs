using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//例如：在员工类中，有一个计算工资的方法，对于A类人员没有项目提成，对于B类人员有项目提成。
//A类人员工资 = 底薪 + 工龄* 工龄工资
//B类人员工资 = 底薪+工龄* 工龄工资 + 项目提成（项目总金额* 0.1）
namespace Chp13_02
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("hello");
            //Console.WriteLine(
            Employee emp = new Employee();
            double salary = emp.GetSalary(3000, 5, 200, 60000);
            Console.WriteLine(salary);
        }
    }
}
