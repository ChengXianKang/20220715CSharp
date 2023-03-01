using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam11
{
    public partial class FrmEdit : Form
    {
        public int StuID { get; set; }  //学生编号
        public FrmEdit()
        {
            InitializeComponent();
        }

        private void BindClass()
        {
            string sql = "select * from y_Class";
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["ClassName"] = "--请选择--";
            dr["ClassId"] = 0;
            dt.Rows.InsertAt(dr, 0);
            this.cmbClass.DataSource = dt;
            this.cmbClass.DisplayMember = "ClassName";
            this.cmbClass.ValueMember = "ClassId";
        }
        private void BindDetail()
        {
            string sql = "select * from y_Student where StudentId = " + this.StuID;
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count != 1)
            {
                MessageBox.Show("找不到该学生信息!");
                this.Close();
                return;
            }
            this.txtName.Text = dt.Rows[0]["Name"].ToString();
            this.txtAge.Text = dt.Rows[0]["Age"].ToString();
            this.cmbClass.SelectedValue = dt.Rows[0]["ClassId"].ToString();
            this.cmbSex.Text = dt.Rows[0]["Gender"].ToString();
        }
        private void FrmEdit_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(this.StuID.ToString());
            BindClass();
            BindDetail();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            string sql = "update y_Student set ClassId=@ClassId,Name=@Name,Gender=@Gender,Age=@Age where StudentId=@StudentId";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ClassId", this.cmbClass.SelectedValue.ToString());
            DBHelper.SetParameter("Name", this.txtName.Text);
            DBHelper.SetParameter("Gender", this.cmbSex.Text);
            DBHelper.SetParameter("Age", this.txtAge.Text);
            DBHelper.SetParameter("StudentId", this.StuID);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("修改失败");
            }
            ((Form1)(this.Owner)).BindData();
            this.Close();
        }
    }
}
