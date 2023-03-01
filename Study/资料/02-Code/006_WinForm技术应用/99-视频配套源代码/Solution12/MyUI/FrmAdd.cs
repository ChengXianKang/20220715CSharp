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
    public partial class FrmAdd : Form
    {
        public FrmAdd()
        {
            InitializeComponent();
        }

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
        private void FrmAdd_Load(object sender, EventArgs e)
        {
            BindPro();
        }

        private void btAdd_Click(object sender, EventArgs e)
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
            if (stuBll.Add(entity) == 1)
                MessageBox.Show("新增成功!");
            else
                MessageBox.Show("新增失败!");
            this.Close();
        }
    }
}
