using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam01
{
    public partial class EmpListForm : Form
    {
        public EmpListForm()
        {
            InitializeComponent();
        }
        private void BindCmb()
        {
            string sql = "select * from Dept";
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["DeptID"] = 0;
            dr["DeptName"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            this.cmbDept.DataSource = dt;
            this.cmbDept.ValueMember = "DeptID";
            this.cmbDept.DisplayMember = "DeptName";
        }
        public void BindData()
        {
            string sql = "select * from Emp inner join Dept on Emp.DeptID = Dept.DeptID where 1 = 1 ";
            if (!this.txtName.Text.Equals(""))
                sql += " and EmpName like '%" + this.txtName.Text + "%'";
            if (!this.txtPhone.Text.Equals(""))
                sql += " and Tel like '%" + this.txtPhone.Text + "%'";
            if (!this.cmbDept.SelectedValue.ToString().Equals("0"))
                sql += " and Emp.DeptID = " + this.cmbDept.SelectedValue.ToString();
            if (this.rbMan.Checked == true)
                sql += " and Sex = '" + this.rbMan.Text + "'";
            if (this.rbWoman.Checked == true)
                sql += " and Sex = '" + this.rbWoman.Text + "'";
            DBHelper.PrepareSql(sql);
            this.dgvEmployee.AutoGenerateColumns = false;
            this.dgvEmployee.DataSource = DBHelper.ExecQuery();
        }
        private void EmpListForm_Load(object sender, EventArgs e)
        {
            BindCmb();
            BindData();
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r= MessageBox.Show("您确定要删除吗", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.No)
                return;
            int empId = int.Parse(this.dgvEmployee.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "delete from Emp where EmpID = @EmpID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("EmpID", empId);
            if (DBHelper.ExecNonQuery() == 1)
            {
                MessageBox.Show("删除成功");
            }
            else
            {
                MessageBox.Show("删除失败");
            }
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            EmpEditForm frm = new EmpEditForm();
            frm.EmpID = 0;
            frm.Owner = this;
            frm.Show();
        }

        private void dgvEmployee_DoubleClick(object sender, EventArgs e)
        {
            int empId = int.Parse(this.dgvEmployee.SelectedRows[0].Cells[0].Value.ToString());
            EmpEditForm frm = new EmpEditForm();
            frm.EmpID = empId;
            frm.Owner = this;
            frm.Show();
        }
    }
}
