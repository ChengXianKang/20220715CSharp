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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private bool isLoaded = false;
        private void BindDept()
        {
            string sql = "select * from OAsys_Dept";
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["D_id"] = 0;
            dr["D_name"] = "全部";
            dt.Rows.InsertAt(dr, 0);
            this.cmbDept.DataSource = dt;
            this.cmbDept.ValueMember = "D_id";
            this.cmbDept.DisplayMember = "D_name";
        }
        public void BindData()
        {
            string sql = "select * from OAsys_Employee inner join OAsys_Dept on OAsys_Employee.E_Dept = OAsys_Dept.D_id where 1 = 1 ";
            if (!this.cmbDept.SelectedValue.ToString().Equals("0"))
                sql += " and E_Dept = " + this.cmbDept.SelectedValue.ToString();
            DBHelper.PrepareSql(sql);
            this.dgvEmp.AutoGenerateColumns = false;
            this.dgvEmp.DataSource = DBHelper.ExecQuery();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            BindDept();
            BindData();
            isLoaded = true;
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(isLoaded == true)
                BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            FrmAdd frm = new FrmAdd();
            frm.DeptId = int.Parse(this.cmbDept.SelectedValue.ToString());
            frm.Owner = this;
            frm.Show();
        }
    }
}
