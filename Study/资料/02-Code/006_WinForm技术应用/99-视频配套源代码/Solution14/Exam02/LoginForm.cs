using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam02
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (this.txtID.Text.Equals(""))
            {
                MessageBox.Show("请输入用户名!");
                return;
            }
            if (this.txtPwd.Text.Equals(""))
            {
                MessageBox.Show("请输入密码!");
                return;
            }
            string sql = "select * from UserInfo where UserName=@UserName and Passwords=@Passwords";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("UserName", this.txtID.Text);
            DBHelper.SetParameter("Passwords", this.txtPwd.Text);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 1)
            {
                ProductListForm frm = new ProductListForm();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("用户名或密码错误!");
            }
        }
    }
}
