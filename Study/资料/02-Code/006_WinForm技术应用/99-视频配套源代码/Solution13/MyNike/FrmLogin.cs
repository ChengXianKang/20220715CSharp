using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyNike.Erp;

namespace MyNike
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (this.txtAcc.Text.Equals("") || this.txtPwd.Text.Equals(""))
            {
                MessageBox.Show("用户名和密码不能为空!");
                return;
            }
            string sql = "select * from SalesMan where Mobile = @Mobile and Pwd = @Pwd";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("Mobile", this.txtAcc.Text);
            DBHelper.SetParameter("Pwd", txtPwd.Text);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["Role"].ToString().Equals("导购员"))
                {
                    MessageBox.Show("只有店长和收银员可以登录系统!");
                    return;
                }
                LoginConfig.SalesmanID = int.Parse(dt.Rows[0]["SalesmanID"].ToString());
                LoginConfig.SalesmanName = dt.Rows[0]["SalesmanName"].ToString();
                LoginConfig.Mobile = dt.Rows[0]["Mobile"].ToString();
                LoginConfig.Role = dt.Rows[0]["Role"].ToString();
                if (dt.Rows[0]["Role"].ToString().Equals("店长"))
                {
                    FrmMain frm = new FrmMain();
                    frm.Show();
                    this.Hide();
                    return;
                }
                if (dt.Rows[0]["Role"].ToString().Equals("收银员"))
                {
                    FrmCash frm = new FrmCash();
                    frm.Show();
                    this.Hide();
                    return;
                }
            }
            else
            {
                MessageBox.Show("用户名或密码错误!");
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
