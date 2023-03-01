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
    class Employee
    {
        /// <summary>
        /// 计算A类员工的工资
        /// </summary>
        /// <param name="baseSa">基本工资</param>
        /// <param name="workYear">工作年限</param>
        /// <param name="yearSa">工龄工资</param>
        /// <returns>返回总工资</returns>
        public double GetSalary(double baseSa,int workYear, double yearSa)
        {
            double sumSa = baseSa + workYear * yearSa;
            return sumSa;
        }
        /// <summary>
        /// 计算B类员工的工资
        /// </summary>
        /// <param name="baseSa">基本工资</param>
        /// <param name="workYear">工作年限</param>
        /// <param name="yearSa">工龄工资</param>
        /// <param name="ProSa">参与项目总金额</param>
        /// <returns></returns>
        public double GetSalary(double baseSa, int workYear, double yearSa,double ProSa)
        {
            double sumSa = baseSa + workYear * yearSa + ProSa * 0.1;
            return sumSa;
        }
    }
}
