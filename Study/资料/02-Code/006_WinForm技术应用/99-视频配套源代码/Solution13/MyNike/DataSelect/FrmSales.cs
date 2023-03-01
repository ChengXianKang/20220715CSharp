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
    public partial class FrmSales : Form
    {
        public FrmSales()
        {
            InitializeComponent();
        }
        private void BindCmb()
        {
            //绑定导购员下拉框
            string sql = "select * from SalesMan where Role = '导购员'";
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["SalesmanID"] = "0";
            dr["SalesmanName"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            this.cmbDaogou.DataSource = dt;
            this.cmbDaogou.ValueMember = "SalesmanID";
            this.cmbDaogou.DisplayMember = "SalesmanName";
        }
        private void BindTime()
        {
            if (this.cmbTimeSelect.Text.Equals("本日"))
            {
                this.dtpStartTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
                DateTime now = DateTime.Now;
                this.dtpStartTime.Value = now;
                this.dtpEndTime.Value = now;
            }
            if (this.cmbTimeSelect.Text.Equals("本周"))
            {
                this.dtpStartTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
                DateTime now = DateTime.Now;
                int dayOfWeek = (int)now.DayOfWeek;
                if (dayOfWeek == 0)
                    dayOfWeek = 7;
                this.dtpStartTime.Value = now.AddDays(-dayOfWeek + 1);
                this.dtpEndTime.Value = now.AddDays(7 - dayOfWeek);
            }
            if (this.cmbTimeSelect.Text.Equals("本月"))
            {
                this.dtpStartTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
                DateTime now = DateTime.Now;
                int dayOfMonth = now.Day;
                this.dtpStartTime.Value = now.AddDays(-dayOfMonth + 1);
                this.dtpEndTime.Value = now.AddDays(-dayOfMonth + 1).AddMonths(1).AddDays(-1);
            }
            if (this.cmbTimeSelect.Text.Equals("本年"))
            {
                this.dtpStartTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
                DateTime now = DateTime.Now;
                int dayOfYear = now.DayOfYear;
                this.dtpStartTime.Value = now.AddDays(-dayOfYear + 1);
                this.dtpEndTime.Value = now.AddDays(-dayOfYear + 1).AddYears(1).AddDays(-1);
            }
        }

        private void BindData()
        {
            string sql = @"select ReceiptsCode 流水号,SalesDate 购物日期,AllMoney 购物金额,
                Lirun 单笔利润,
                DaogouMan.SalesmanName 导购员,CashierMan.SalesmanName 收银员
                from Sales
                left join SalesMan DaogouMan on Sales.SalesmanID = DaogouMan.SalesmanID
                left join SalesMan CashierMan on Sales.CashierID = CashierMan.SalesmanID
                left join
                (select SalesID,sum((GoodsMoney-StorePrice)*Quantity) Lirun
                from SalesDetail left join Goods on SalesDetail.GoodsID = Goods.GoodsID
                group by SalesID) LirunTable on  Sales.SalesID = LirunTable.SalesID
                where SalesDate between @start and @end ";
            if (!this.cmbDaogou.SelectedValue.ToString().Equals("0"))
                sql += " and Sales.SalesmanID = " + this.cmbDaogou.SelectedValue.ToString();
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("start", this.dtpStartTime.Value.ToString("yyyy-MM-dd"));
            DBHelper.SetParameter("end", this.dtpEndTime.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            this.dataGridView1.DataSource = dt;
            this.lblRecord.Text = "销售记录" + dt.Rows.Count + "条";
            double xsSum = 0; //销售总金额
            double lrSum = 0; //利润总金额
            foreach (DataRow item in dt.Rows)
            {
                xsSum += double.Parse(item["购物金额"].ToString());
                lrSum += double.Parse(item["单笔利润"].ToString());
            }
            this.lblSumMoney.Text = "销售金额￥" + xsSum + "元,利润￥" + lrSum + "元";
        }
        private void FrmSales_Load(object sender, EventArgs e)
        {
            BindCmb();
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
