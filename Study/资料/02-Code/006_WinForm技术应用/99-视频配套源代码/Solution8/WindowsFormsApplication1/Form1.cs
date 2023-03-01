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

        private void Form1_Load(object sender, EventArgs e)
        {
            //1-编写连接字符串（windows方式连接）
            //string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            //2-编写连接字符串（sql用户名密码方式连接）
            string connStr = "server=.;database=DBTEST;uid=sa;pwd=123456";
            //2-创建连接对象，打开数据库连接
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            //3-编写sql语句
            string sql = "select * from Member";
            //4-定义执行命令的对象执行命令
            SqlCommand cmd = new SqlCommand(sql, conn);

            //5-利用DataReader读取数据
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ListViewItem item = new ListViewItem(rd["MemberId"].ToString());
                item.SubItems.Add(rd["MemberAccount"].ToString());
                item.SubItems.Add(rd["MemberPwd"].ToString());
                item.SubItems.Add(rd["MemberName"].ToString());
                item.SubItems.Add(rd["MemberPhone"].ToString());
                this.listView1.Items.Add(item);
            }
            rd.Close();
            //显示人数
            cmd.CommandText = "select count(*) from Member";
            int count = (int)cmd.ExecuteScalar();
            this.lblCount.Text = "会员人数:" + count;
            conn.Close();
        }




    }
}
