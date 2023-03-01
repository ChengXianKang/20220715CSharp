using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNike.Erp
{
    public partial class FrmGoodView : Form
    {
        public FrmGoodView()
        {
            InitializeComponent();
        }

        private void BindType()
        {
            DataTable dt = new DataTable();
            DBHelper.PrepareSql("select * from [Type]");
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["TypeName"] = "--请选择--";
            dr["TypeId"] = "0";
            dt.Rows.InsertAt(dr, 0);
            this.cmbType.DataSource = dt;
            this.cmbType.DisplayMember = "TypeName";
            this.cmbType.ValueMember = "TypeID";
        }
        private void cmbTimeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbTimeSelect.Text.Equals("全部"))
            {
                this.dtpStartTime.Enabled = false;
                this.dtpEndTime.Enabled = false;
            }
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
                this.dtpStartTime.Value = now.AddDays(-dayOfWeek+1);
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
            string sql = "select * from Goods inner join [Type] on Goods.TypeID = [Type].TypeID where 1 = 1 ";
            if (!this.txtBarCode.Text.Equals(""))
                sql += " and BarCode = '" + this.txtBarCode.Text +"'";
            if (!this.txtGoodsName.Text.Equals(""))
                sql += " and GoodsName like '%" + this.txtGoodsName.Text + "%'";
            if (!this.cmbTimeSelect.Text.Equals("全部"))
                sql += string.Format(" and StockDate between '{0}' and '{1}'",
                    this.dtpStartTime.Value.ToString("yyyy-MM-dd") + " 00:00:00",
                    this.dtpEndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59");
            if (!this.cmbType.SelectedValue.ToString().Equals("0"))
                sql += " and Goods.TypeID = " + this.cmbType.SelectedValue.ToString();
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            this.dgvGoods.AutoGenerateColumns = false;
            this.dgvGoods.DataSource = dt;
            this.lblRowCount.Text = "当前共" + dt.Rows.Count + "条记录!";
        }

        private void FrmGoodView_Load(object sender, EventArgs e)
        {
            BindType();
            this.dtpStartTime.Enabled = false;
            this.dtpEndTime.Enabled = false;
            BindData();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            BindData();
            //MessageBox.Show(this.dtpStartTime.Value.ToString());
        }
    }
}
