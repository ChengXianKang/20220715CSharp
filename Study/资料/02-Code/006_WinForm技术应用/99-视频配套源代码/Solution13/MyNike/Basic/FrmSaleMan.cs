using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNike.Basic
{
    public partial class FrmSaleMan : Form
    {
        public FrmSaleMan()
        {
            InitializeComponent();
        }

        private void BindCmb()
        {
            this.cmbSex.Items.Add("--请选择--");
            this.cmbSex.Items.Add("男");
            this.cmbSex.Items.Add("女");
            this.cmbSex.Text = "--请选择--";

            this.cmbRole.Items.Add("--请选择--");
            this.cmbRole.Items.Add("店长");
            this.cmbRole.Items.Add("导购员");
            this.cmbRole.Items.Add("收银员");
            this.cmbRole.Text = "--请选择--";
        }
        private void BindData()
        {
            DBHelper.PrepareSql("select * from SalesMan");
            this.dataGridView1.DataSource = DBHelper.ExecQuery();
        }
        private void FrmSaleMan_Load(object sender, EventArgs e)
        {
            BindCmb();
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            string sql = "insert into SalesMan(SalesmanName,Mobile,Pwd,Gender,Wage,CommissionRate,[Role]) values(@SalesmanName, @Mobile, @Pwd, @Gender, @Wage, @CommissionRate,@Role)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("SalesmanName",this.txtName.Text);
            DBHelper.SetParameter("Mobile",this.txtMobile.Text);
            DBHelper.SetParameter("Pwd",this.txtPwd.Text);
            DBHelper.SetParameter("Gender",this.cmbSex.Text);
            DBHelper.SetParameter("Wage",this.txtWage.Text);
            DBHelper.SetParameter("CommissionRate",this.txtCommissionRate.Text);
            DBHelper.SetParameter("Role",this.cmbRole.Text);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("数据添加成功!");
                BindData();
            }
            else
            {
                MessageBox.Show("数据添加失败!");
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int manId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "select * from SalesMan where SalesmanId = @SalesmanId";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("SalesmanId", manId);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count != 1)
            {
                MessageBox.Show("获取不到数据!");
                BindData();
                return;
            }
            this.txtName.Text = dt.Rows[0]["SalesmanName"].ToString();
            this.txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
            this.txtPwd.Text = dt.Rows[0]["Pwd"].ToString();
            this.cmbSex.Text = dt.Rows[0]["Gender"].ToString();
            this.txtWage.Text = dt.Rows[0]["Wage"].ToString();
            this.txtCommissionRate.Text = dt.Rows[0]["CommissionRate"].ToString();
            this.cmbRole.Text = dt.Rows[0]["Role"].ToString();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            int manId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "update SalesMan set SalesmanName=@SalesmanName,Mobile=@Mobile,Pwd=@Pwd,Gender=@Gender,Wage=@Wage,CommissionRate=@CommissionRate,Role=@Role where SalesmanID=@SalesmanID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("SalesmanName", this.txtName.Text);
            DBHelper.SetParameter("Mobile", this.txtMobile.Text);
            DBHelper.SetParameter("Pwd", this.txtPwd.Text);
            DBHelper.SetParameter("Gender", this.cmbSex.Text);
            DBHelper.SetParameter("Wage", this.txtWage.Text);
            DBHelper.SetParameter("CommissionRate", this.txtCommissionRate.Text);
            DBHelper.SetParameter("Role", this.cmbRole.Text);
            DBHelper.SetParameter("SalesmanId", manId);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("数据修改成功!");
                BindData();
            }
            else
            {
                MessageBox.Show("数据修改失败!");
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("你是否真的要删除，删除后无法恢复!", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
                return;
            int manId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "delete from SalesMan where SalesmanID=@SalesmanID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("SalesmanId", manId);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("数据删除成功!");
                BindData();
            }
            else
            {
                MessageBox.Show("数据删除失败!");
            }
        }
    }
}
