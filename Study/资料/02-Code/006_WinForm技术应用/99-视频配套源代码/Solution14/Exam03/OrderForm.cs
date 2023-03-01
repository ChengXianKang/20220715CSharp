using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam03
{
    public partial class OrderForm : Form
    {
        public int ProductId { get; set; }  //商品编号
        public OrderForm()
        {
            InitializeComponent();
        }

        private void BindDetail()
        {
            string sql = "select * from Product where ProductID=@ProductID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProductID", ProductId);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("找不到商品!");
                this.Close();
            }
            else
            {
                //绑定数据
                this.txtName.Text = dt.Rows[0]["ProductName"].ToString();
            }
        }

        private void BindData()
        {
            string sql = "select * from Sales left join Product on Sales.ProductID=Product.ProductID where Sales.ProductID = @ProductID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProductID", this.ProductId);
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = DBHelper.ExecQuery();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            BindDetail();
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            string sql = @"insert into Sales(ProductID,SalesMan,SalesCount,SalesDate) 
                            values(@ProductID,@SalesMan,@SalesCount,getdate())";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProductID", this.ProductId);
            DBHelper.SetParameter("SalesMan",this.txtSaleMan.Text);
            DBHelper.SetParameter("SalesCount", this.txtCount.Text);
            int rowCount = DBHelper.ExecNonQuery();

            if (rowCount == 1)
                MessageBox.Show("添加成功!");
            else
                MessageBox.Show("添加失败!");
            BindData();
        }
    }
}
