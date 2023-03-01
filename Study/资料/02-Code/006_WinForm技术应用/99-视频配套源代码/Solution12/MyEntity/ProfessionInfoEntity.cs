using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEntity
{
    public class ProfessionInfoEntity
    {
        public ProfessionInfoEntity()
        {
            this.ProfessionID = 0;
            this.ProfessionName = "";
        }
        public int ProfessionID { get; set; }  //专业编号
        public string ProfessionName { get; set; }//专业名称
    }
}
