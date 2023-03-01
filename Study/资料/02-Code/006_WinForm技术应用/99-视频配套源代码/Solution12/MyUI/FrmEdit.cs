using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyEntity;
using MyBLL;

namespace MyUI
{
    public partial class FrmEdit : Form
    {
        public FrmEdit()
        {
            InitializeComponent();
        }
        public string StuID { get; set; } //学生编号
        ProfessionInfoBLL proBll = new ProfessionInfoBLL();
        StudentInfoBLL stuBll = new StudentInfoBLL();
        #region 绑定下拉框
        private void BindPro()
        {
            List<ProfessionInfoEntity> list = new List<ProfessionInfoEntity>();
            list = proBll.List();
            list.Insert(0, new ProfessionInfoEntity { ProfessionID = 0, ProfessionName = "--请选择--" });
            this.cmbPro.DataSource = list;
            this.cmbPro.DisplayMember = "ProfessionName";
            this.cmbPro.ValueMember = "ProfessionID";
        }
        #endregion

        #region 绑定详情
        private void BindDetail()
        {
            StudentInfoEntiy entity = new StudentInfoEntiy();
            entity = stuBll.Detail(this.StuID);
            this.txtId.Text = entity.StuID;
            this.txtName.Text = entity.StuName;
            this.txtAge.Text = entity.StuAge.ToString();
            this.cmbPro.SelectedValue = entity.ProfessionID;
            //性别处理
            if (entity.StuSex.Equals("男"))
                this.rbBoy.Checked = true;
            else
                this.rbGirl.Checked = true;
            //爱好处理
            string[] arrHobby = entity.StuHobby.Split(',');
            foreach (string hobby in arrHobby)
            {
                foreach (CheckBox ck in this.panel2.Controls)
                {
                    if (ck.Text.Equals(hobby))
                        ck.Checked = true;
                }
            }
        }
        #endregion

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            BindPro();
            BindDetail();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            //性别处理
            string sex = "";
            if (this.rbBoy.Checked == true) sex = this.rbBoy.Text;
            if (this.rbGirl.Checked == true) sex = this.rbGirl.Text;
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
            StudentInfoEntiy entity = new StudentInfoEntiy();
            entity.StuID = this.txtId.Text;
            entity.StuName = this.txtName.Text;
            entity.StuAge = int.Parse(this.txtAge.Text);
            entity.StuSex = sex;
            entity.StuHobby = hobby;
            entity.ProfessionID = int.Parse(this.cmbPro.SelectedValue.ToString());
            if (stuBll.Update(entity) == 1)
                MessageBox.Show("修改成功!");
            else
                MessageBox.Show("修改失败!");
            this.Close();
        }
    }
}
