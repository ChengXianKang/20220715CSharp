using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FrmEdit : Form
    {
        
        public FrmEdit()
        {
            InitializeComponent();
        }
        public string StuID { get; set; } //学生编号
        #region 绑定专业信息到下拉框
        private void BindProfession()
        {
            DataTable dt = new DataTable();
            DBHelper.PrepareSql("select * from ProfessionInfo");
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["ProfessionID"] = 0;
            dr["professionName"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            this.cmbPro.DataSource = dt;
            this.cmbPro.DisplayMember = "professionName";
            this.cmbPro.ValueMember = "ProfessionID";
        }
        #endregion
        private void BindDetail()
        {
            string sql = "select * from StudentInfo where StuID = " + this.StuID;
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            this.txtId.Text = dt.Rows[0]["StuID"].ToString();
            this.txtName.Text = dt.Rows[0]["StuName"].ToString();
            this.txtAge.Text = dt.Rows[0]["StuAge"].ToString();
            this.cmbPro.SelectedValue = dt.Rows[0]["ProfessionID"].ToString();
            //性别处理
            if (dt.Rows[0]["StuSex"].ToString().Equals("男"))
                this.rbBoy.Checked = true;
            else
                this.rbGirl.Checked = true;
            //爱好处理
            string[] arrHobby = dt.Rows[0]["StuHobby"].ToString().Split(',');
            foreach (string hobby in arrHobby)
            {
                foreach (CheckBox ck in this.panel2.Controls)
                {
                    if (ck.Text.Equals(hobby))
                        ck.Checked = true;
                }
            }

        }
        private void FrmEdit_Load(object sender, EventArgs e)
        {
            BindProfession();
            BindDetail();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            string sql = "update StudentInfo set StuName=@StuName,StuAge=@StuAge,StuSex=@StuSex,StuHobby=@StuHobby,ProfessionID=@ProfessionID where StuID=@StuID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("StuName", this.txtName.Text);
            DBHelper.SetParameter("StuAge", this.txtAge.Text);
            //性别处理
            string sex = "";
            if (this.rbBoy.Checked == true) sex = this.rbBoy.Text;
            if (this.rbGirl.Checked == true) sex = this.rbGirl.Text;
            DBHelper.SetParameter("StuSex", sex);
            //爱好处理
            string hobby = "";
            foreach (CheckBox ck in this.panel2.Controls)
            {
                if (ck.Checked == true)
                {
                    if (!hobby.Equals(""))
                        hobby += ",";
                    hobby += ck.Text;
                }
            }
            DBHelper.SetParameter("StuHobby", hobby);
            DBHelper.SetParameter("ProfessionID", this.cmbPro.SelectedValue.ToString());
            DBHelper.SetParameter("StuID", this.StuID);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("修改成功!");
                this.Close();
            }
            else
            {
                MessageBox.Show("修改失败!");
            }
        }
    }
}
