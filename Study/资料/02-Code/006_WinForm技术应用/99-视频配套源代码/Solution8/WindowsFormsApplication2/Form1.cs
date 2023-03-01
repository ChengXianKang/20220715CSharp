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

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BindData()
        {
            //1-定义连接字符串 
            //string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            //2-编写连接字符串（sql用户名密码方式连接）
            string connStr = "server=.;database=DBTEST;uid=sa;pwd=123456";
            //2-定义连接对象，打开连接
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            //3-编写sql语句
            string sql = "select * from Member";
            //4-数据适配器抽取信息
            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();  //数据表格
            adp.Fill(dt);
            this.dataGridView1.AutoGenerateColumns = false;   //自动列取消
            this.dataGridView1.DataSource = dt;

            //显示人数
            adp.SelectCommand.CommandText = "select count(*) from Member";
            int count = (int)adp.SelectCommand.ExecuteScalar();
            this.lblCount.Text = "会员人数:" + count;
            conn.Close();
        }
        //窗体加载事件
        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }
        //修改数据后跟新数据库的按钮事件
        private void btUpdate_Click(object sender, EventArgs e)
        {
            //1-定义连接字符串 
            //string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            //2-编写连接字符串（sql用户名密码方式连接）
            string connStr = "server=.;database=DBTEST;uid=sa;pwd=123456";
            //2-定义连接对象，打开连接
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            //3-编写sql语句
            string sql = "select * from Member";
            //4-数据适配器抽取信息
            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();  //数据表格
            adp.Fill(dt);

            //添加一条数据
            DataRow dr = dt.NewRow();
            dr["MemberAccount"] = "weiyan";
            dr["MemberPwd"] = "123456";
            dr["MemberName"] = "魏延";
            dr["MemberPhone"] = "15352565585";
            dt.Rows.Add(dr);
            //修改一条数据
            dt.Rows[1]["MemberPwd"] = "654321";
            //删除一条数据
            dt.Rows[4].Delete();
            //跟新数据到数据库
            SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(adp);
            adp.Update(dt);
            conn.Close();
            //确认DataTable的数据变化，并且重新绑定到控件
            //dt.AcceptChanges();
            //this.dataGridView1.DataSource = dt;
            //MessageBox.Show("数据跟新成功！");
            BindData();
            MessageBox.Show("数据跟新成功！");
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
