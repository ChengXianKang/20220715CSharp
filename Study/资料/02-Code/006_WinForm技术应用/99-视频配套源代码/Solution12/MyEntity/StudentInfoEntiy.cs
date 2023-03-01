using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEntity
{
    public class StudentInfoEntiy
    {
        public StudentInfoEntiy()
        {
            this.StuID = "";
            this.StuName = "";
            this.StuAge = 0;
            this.StuSex = "";
            this.StuHobby = "";
            this.ProfessionID = 0;
            this.ProfessionName = "";
        }
        public string StuID { get; set; }  //学生学号
        public string StuName { get; set; }  //学生姓名
        public int StuAge { get; set; }  //学生年龄
        public string StuSex { get; set; }  //学生性别
        public string StuHobby { get; set; }  //学生爱好
        public int ProfessionID { get; set; }  //学生所属专业编号
        public string ProfessionName { get; set; } //学生所属专业名称
    }
}
