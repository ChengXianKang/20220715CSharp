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

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //绑定数据的方法
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
            conn.Close();
        }
        //窗体加载事件
        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }
        //添加信息按钮事件
        private void btAdd_Click(object sender, EventArgs e)
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
            BindData();
        }
        //网格控件的点击事件
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //当AllowUserToAddRows=True的时候，防止用户选择最后一个空行
            if (this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Equals(""))
            {
                MessageBox.Show("请正确选择!");
                return;
            }
            int memId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            //MessageBox.Show(memId.ToString());
            //1-定义连接字符串 
            string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            //2-定义连接对象，打开连接
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            //3-编写sql语句
            string sql = "select * from Member where MemberId = " + memId;
            //-抽取数据
            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            conn.Close();
            this.txtAccount.Text = dt.Rows[0]["MemberAccount"].ToString();
            this.txtPwd.Text = dt.Rows[0]["MemberPwd"].ToString();
            this.txtNickName.Text = dt.Rows[0]["MemberName"].ToString();
            this.txtPhone.Text = dt.Rows[0]["MemberPhone"].ToString();
        }

        //修改按钮的点击事件
        private void btUpdate_Click(object sender, EventArgs e)
        {
            int memId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = string.Format("update Member set MemberAccount='{0}',MemberPwd='{1}',MemberName='{2}',MemberPhone='{3}' where MemberId='{4}'"
                , this.txtAccount.Text, this.txtPwd.Text, this.txtNickName.Text, this.txtPhone.Text, memId);
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rowCount = cmd.ExecuteNonQuery();
            conn.Close();
            if (rowCount == 1)
                MessageBox.Show("修改成功!");
            else
                MessageBox.Show("修改失败!");
            BindData();
        }
        //删除信息弹出菜单事件
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("您确定要删除吗?", "****系统", MessageBoxButtons.YesNo);
            if (r == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            int memId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = "delete from Member where MemberId = " + memId;
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rowCount = cmd.ExecuteNonQuery();
            conn.Close();
            if (rowCount == 1)
                MessageBox.Show("删除成功!");
            else
                MessageBox.Show("删除失败!");
            BindData();
        }
    }
}
