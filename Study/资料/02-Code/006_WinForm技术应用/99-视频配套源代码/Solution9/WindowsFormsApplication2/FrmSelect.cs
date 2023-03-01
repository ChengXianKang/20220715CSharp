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

namespace WindowsFormsApplication5
{
    public partial class FrmSelect : Form
    {
        public FrmSelect()
        {
            InitializeComponent();
        }
        //绑定数据的方法
        public void BindData()
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
        private void FrmSelect_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            FrmAdd frm = new FrmAdd();
            frm.Owner = this;
            frm.Show();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Equals(""))
            {
                MessageBox.Show("请正确选择!");
                return;
            }
            int memId = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            FrmEdit frm = new FrmEdit();
            frm.MemId = memId;
            frm.Owner = this;
            frm.Show();
        }

        //删除
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
