using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNike.DataSelect
{
    public partial class FrmSalary : Form
    {
        public FrmSalary()
        {
            InitializeComponent();
        }
        private void BindTime()
        {
            DateTime now = DateTime.Now;
            int day = now.Day;
            if (this.cmbTimeSelect.Text.Equals("本月"))
            {
                this.dtpStartTime.Value = now.AddDays(-day + 1);
                this.dtpEndTime.Value = now.AddDays(-day + 1).AddMonths(1).AddDays(-1);
            }
            if (this.cmbTimeSelect.Text.Equals("上一月"))
            {
                this.dtpStartTime.Value = now.AddDays(-day + 1).AddMonths(-1);
                this.dtpEndTime.Value = now.AddDays(-day);
            }
        }
        private void BindData()
        {
            string sql = @"declare @AllPeopleMoney decimal(10,2)
                select @AllPeopleMoney = sum(AllMoney)  from Sales where SalesDate between @start and @end
                if @AllPeopleMoney is null
	                set @AllPeopleMoney = 0
                select SalesmanName 姓名,[Role] 类型,Mobile 电话,Wage 基本工资,
                case
	                when [Role] = '收银员' then 0
	                else CommissionRate
                end 提成比例,
                case 
	                when  [Role] = '店长' then @AllPeopleMoney
	                when  xiaoshouMoney is null then 0
	                else xiaoshouMoney
                end 月销售额,
                case
	                when  [Role] = '店长' then Convert(decimal(9,2),Wage+@AllPeopleMoney*CommissionRate)
	                when xiaoshouMoney is null then Convert(decimal(9,2),Wage)
	                else Convert(decimal(9,2),Wage+xiaoshouMoney*CommissionRate)
                end 应发工资
                from SalesMan
                left join 
                (select SalesmanID,sum(AllMoney) xiaoshouMoney from Sales  where SalesDate between @start and @end group by SalesmanID) xiaoshouSum
                on SalesMan.SalesmanID = xiaoshouSum.SalesmanID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("start", this.dtpStartTime.Value.ToString("yyyy-MM-dd"));
            DBHelper.SetParameter("end", this.dtpEndTime.Value.ToString("yyyy-MM-dd"));
            this.dataGridView1.DataSource = DBHelper.ExecQuery();
        }
        private void FrmSalary_Load(object sender, EventArgs e)
        {
            BindTime();
            BindData();
        }

        private void cmbTimeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTime();

        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
