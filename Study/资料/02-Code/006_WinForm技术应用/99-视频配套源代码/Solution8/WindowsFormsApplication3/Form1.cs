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

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //方案一：使用DataAdapter的command跟新数据
        private void btAdd1_Click(object sender, EventArgs e)
        {
            //1-定义连接字符串 
            //string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            //2-编写连接字符串（sql用户名密码方式连接）
            string connStr = "server=.;database=DBTEST;uid=sa;pwd=123456";
            //2-定义连接对象，打开连接
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            //3-编写sql语句
            string sql = string.Format("insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)  values('{0}', '{1}', '{2}', '{3}')"
                , this.txtAccount.Text, this.txtPwd.Text, this.txtNickName.Text, this.txtPhone.Text);
            //4-数据适配器
            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
            //执行sql语句
            int rowCount = adp.SelectCommand.ExecuteNonQuery();
            conn.Close();
            if(rowCount == 1)
                MessageBox.Show("添加成功!");
            else
                MessageBox.Show("添加失败!");
        }

        //方案二：直接使用SqlCommand对象跟新数据
        private void btAdd2_Click(object sender, EventArgs e)
        {
            //1-编写连接字符串（windows方式连接）
            //string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            //2-编写连接字符串（sql用户名密码方式连接）
            string connStr = "server=.;database=DBTEST;uid=sa;pwd=123456";
            //2-创建连接对象，打开数据库连接
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            //3-编写sql语句
            string sql = string.Format("insert into Member(MemberAccount,MemberPwd,MemberName,MemberPhone)  values('{0}', '{1}', '{2}', '{3}')"
                 , this.txtAccount.Text, this.txtPwd.Text, this.txtNickName.Text, this.txtPhone.Text);
            //4-定义执行命令的对象执行命令
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rowCount = cmd.ExecuteNonQuery();      
            conn.Close();
            if (rowCount == 1)
                MessageBox.Show("添加成功!");
            else
                MessageBox.Show("添加失败!");
        }
    }
}
