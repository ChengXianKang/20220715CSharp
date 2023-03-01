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
    public partial class FrmEdit : Form
    {
        public FrmEdit()
        {
            InitializeComponent();
        }
        public int MemId { get; set; }  //接受外部传递过来的会员编号
        //绑定会员详情到文本框
        private void BindDetail()
        {
            //1-定义连接字符串 
            string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            //2-定义连接对象，打开连接
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            //3-编写sql语句
            string sql = "select * from Member where MemberId = " + this.MemId;
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
        private void FrmEdit_Load(object sender, EventArgs e)
        {
            BindDetail();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DBTEST;Data Source=.";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = string.Format("update Member set MemberAccount='{0}',MemberPwd='{1}',MemberName='{2}',MemberPhone='{3}' where MemberId='{4}'"
                , this.txtAccount.Text, this.txtPwd.Text, this.txtNickName.Text, this.txtPhone.Text, this.MemId);
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rowCount = cmd.ExecuteNonQuery();
            conn.Close();
            if (rowCount == 1)
                MessageBox.Show("修改成功!");
            else
                MessageBox.Show("修改失败!");
            //刷新查询窗体数据并关闭当前窗体
            ((FrmSelect)this.Owner).BindData();
            this.Close();
        }
    }
}
