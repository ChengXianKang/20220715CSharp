using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {

        }

        //密码升级，传入用户名和密码，
        //如果用户名密码正确，并且密码长度<8，自动升级成8位密码
        private void btUpgrade_Click(object sender, EventArgs e)
        {
            DBHelper.PrepareProc("procPwdUpgrade");
            DBHelper.SetParameter("acc", this.txtAccount.Text);
            DBHelper.SetInOutParameter("pwd", SqlDbType.NVarChar, 20, this.txtPwd.Text);
            DBHelper.ExecNonQuery();
            this.lblNewPwd.Text = DBHelper.GetParameter("pwd").ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
