using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            //1-定义连接字符串 
            //string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            //2-编写连接字符串（sql用户名密码方式连接）
            string connStr = "server=.;database=DBTEST;uid=sa;pwd=123456";
            //2-定义连接对象，打开连接
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            //3-编写sql语句(此处如果用户名密码同时输入' or '1'='1 则可以造成注入)
            string sql = string.Format("select * from Member where MemberAccount='{0}' and MemberPwd='{1}'"
                ,this.txtAccount.Text,this.txtPwd.Text);
            //4-数据适配器抽取信息
            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();  //数据表格
            adp.Fill(dt);
            conn.Close();
            if (dt.Rows.Count == 0)
                MessageBox.Show("用户名或密码错误！");
            else
                MessageBox.Show("登录成功！");
        }
    }
}
