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
    public partial class FrmAdd : Form
    {
        public FrmAdd()
        {
            InitializeComponent();
        }
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
        private void FrmAdd_Load(object sender, EventArgs e)
        {
            BindProfession();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {

            string sql = "insert into StudentInfo(StuID,StuName,StuAge,StuSex,StuHobby,ProfessionID) values(@StuID,@StuName,@StuAge,@StuSex,@StuHobby,@ProfessionID)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("StuID", this.txtId.Text);
            DBHelper.SetParameter("StuName",this.txtName.Text);
            DBHelper.SetParameter("StuAge",this.txtAge.Text);
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
            DBHelper.SetParameter("ProfessionID",this.cmbPro.SelectedValue.ToString());
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("新增成功!");
                this.Close();
            }
            else
            {
                MessageBox.Show("新增失败!");
            }
        }
    }
}
