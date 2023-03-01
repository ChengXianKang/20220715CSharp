using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam13
{
    public partial class FrmAdd : Form
    {
        public int DeptId { get; set; } //部门编号
        public FrmAdd()
        {
            InitializeComponent();
        }
        private void BindDept()
        {
            string sql = "select * from OAsys_Dept";
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["D_id"] = 0;
            dr["D_name"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            this.cmbDept.DataSource = dt;
            this.cmbDept.ValueMember = "D_id";
            this.cmbDept.DisplayMember = "D_name";
        }
        private void FrmAdd_Load(object sender, EventArgs e)
        {
            if (this.DeptId != 0)
            {
                this.label1.Visible = false;
                this.cmbDept.Visible = false;
            }
            BindDept();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            string sql = @"insert into OAsys_Employee(E_name,E_Sex,E_Dept,E_Position,E_Email)
                values(@E_name,@E_Sex,@E_Dept,@E_Position,@E_Email)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("E_name", this.txtName.Text);
            DBHelper.SetParameter("E_Sex", this.txtSex.Text);
            if(DeptId == 0)
                DBHelper.SetParameter("E_Dept", this.cmbDept.SelectedValue.ToString());
            else
                DBHelper.SetParameter("E_Dept", DeptId);
            DBHelper.SetParameter("E_Position", this.txtPosition.Text);
            DBHelper.SetParameter("E_Email", this.txtMail.Text);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("添加成功!");
            }
            else
            {
                MessageBox.Show("添加失败");
            }
           ((MainForm)this.Owner).BindData();
            this.Close();
        }
    }
}
