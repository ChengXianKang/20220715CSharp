using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class MyDate
    {
        //判断年份是否是闰年
        public bool Is29(int year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                return true;
            else
                return false;
        }
        //判断是否需要工作（星期一到星期五需要工作，周六周日不需要）
        public bool IsWork(DateTime date)
        {
            if ((int)date.DayOfWeek == 0 || (int)date.DayOfWeek == 6)
                return false;
            else
                return true;
        }
    }
}
