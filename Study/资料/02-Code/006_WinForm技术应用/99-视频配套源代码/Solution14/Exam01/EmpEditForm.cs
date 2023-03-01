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
    public partial class EmpEditForm : Form
    {
        public int EmpID { get; set; }
        public EmpEditForm()
        {
            InitializeComponent();
        }
        //绑定部门下拉框
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
        private void BindDetail()
        {
            if (this.EmpID == 0)
                return;
            string sql = "select * from Emp where EmpID = @EmpID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("EmpID", this.EmpID);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("查无此人!");
                return;
            }
            this.txtName.Text = dt.Rows[0]["EmpName"].ToString();
            if (dt.Rows[0]["Sex"].ToString().Equals("男"))
                this.rbMan.Checked = true;
            if (dt.Rows[0]["Sex"].ToString().Equals("女"))
                this.rbWoman.Checked = true;
            this.txtAge.Text = dt.Rows[0]["Age"].ToString();
            this.txtPhone.Text = dt.Rows[0]["Tel"].ToString();
            this.cmbDept.SelectedValue = dt.Rows[0]["DeptID"].ToString();
        }
        private void EmpEditForm_Load(object sender, EventArgs e)
        {
            BindCmb();
            BindDetail();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Equals(""))
            {
                MessageBox.Show("姓名不能为空!");
                return;
            }
            if (this.txtAge.Text.Equals(""))
            {
                MessageBox.Show("年龄不能为空!");
                return;
            }
            if (this.txtPhone.Text.Equals(""))
            {
                MessageBox.Show("电话不能为空!");
                return;
            }
            int age = 0;
            bool b = int.TryParse(this.txtAge.Text, out age);
            if (b == false)
            {
                MessageBox.Show("年龄必须是数字!");
                return;
            }
            string sql = "insert into Emp(EmpName,Sex,Age,Tel,DeptID) values(@EmpName,@Sex,@Age,@Tel,@DeptID)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("EmpName",this.txtName.Text);
            DBHelper.SetParameter("Sex",this.rbMan.Checked==true?"男":"女");
            DBHelper.SetParameter("Age",this.txtAge.Text);
            DBHelper.SetParameter("Tel",this.txtPhone.Text);
            DBHelper.SetParameter("DeptID", this.cmbDept.SelectedValue.ToString());
            if (DBHelper.ExecNonQuery() == 1)
            {
                MessageBox.Show("添加成功");
            }
            else
            {
                MessageBox.Show("添加失败");
            }
           ((EmpListForm)(this.Owner)).BindData();
            this.Close();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Equals(""))
            {
                MessageBox.Show("姓名不能为空!");
                return;
            }
            if (this.txtAge.Text.Equals(""))
            {
                MessageBox.Show("年龄不能为空!");
                return;
            }
            if (this.txtPhone.Text.Equals(""))
            {
                MessageBox.Show("电话不能为空!");
                return;
            }
            int age = 0;
            bool b = int.TryParse(this.txtAge.Text, out age);
            if (b == false)
            {
                MessageBox.Show("年龄必须是数字!");
                return;
            }
            string sql = "update Emp set EmpName=@EmpName,Sex=@Sex,Age=@Age,Tel=@Tel,DeptID=@DeptID where EmpID=@EmpID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("EmpName", this.txtName.Text);
            DBHelper.SetParameter("Sex", this.rbMan.Checked == true ? "男" : "女");
            DBHelper.SetParameter("Age", this.txtAge.Text);
            DBHelper.SetParameter("Tel", this.txtPhone.Text);
            DBHelper.SetParameter("DeptID", this.cmbDept.SelectedValue.ToString());
            DBHelper.SetParameter("EmpID", this.EmpID);
            if (DBHelper.ExecNonQuery() == 1)
            {
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("修改失败");
            }
           ((EmpListForm)(this.Owner)).BindData();
            this.Close();
        }
    }
}
