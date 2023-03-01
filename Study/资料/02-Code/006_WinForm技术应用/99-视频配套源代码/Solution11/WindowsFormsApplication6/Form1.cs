using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DBHelper.PrepareProc("procInsertMember");
                DBHelper.SetParameter("acc", this.txtAccount.Text);
                DBHelper.SetParameter("pwd", this.txtPwd.Text);
                DBHelper.SetParameter("memName", this.txtNickName.Text);
                DBHelper.SetParameter("memPhone", this.txtPhone.Text);
                DBHelper.SetReturnParameter("returnValue");
                DBHelper.ExecNonQuery();
                int result = (int)DBHelper.GetParameter("returnValue");
                if (result == 1)
                    MessageBox.Show("添加成功!");
            }
            catch (Exception ex)
            {
                int result = (int)DBHelper.GetParameter("returnValue");
                if (result == -1)
                    MessageBox.Show("用户名重名了，违反了唯一约束!");
                if (result == -100)
                    MessageBox.Show(ex.Message);
            }
        }


    }
}
