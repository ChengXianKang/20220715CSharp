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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }
        private void BindData()
        {
            string sql = "select * from Product  where 1 = 1 ";
            if (!this.txtSearchName.Text.Equals(""))
                sql += " and ProductName like '%" + this.txtSearchName.Text + "%'";
            if (!this.txtSearchRemark.Text.Equals(""))
                sql += " and Remark like '%" + this.txtSearchRemark.Text  + "%'";
            DBHelper.PrepareSql(sql);
            this.dgvGoods.AutoGenerateColumns = false;
            this.dgvGoods.DataSource = DBHelper.ExecQuery();
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Equals(""))
            {
                MessageBox.Show("商品名称不能为空!");
                return;
            }
            if (this.rbYes.Checked == false && this.rbNo.Checked == false)
            {
                MessageBox.Show("请选择是否上架!");
                return;
            }
            if (this.txtPrice.Text.Equals(""))
            {
                MessageBox.Show("单价不能为空!");
                return;
            }
            int price = 0;
            bool b = int.TryParse(this.txtPrice.Text, out price);
            if (b == false)
            {
                MessageBox.Show("单价只能是数字!");
                return;
            }
            string sql = @"insert into Product(ProductName,IsUp,UnitPrice,Remark)
                        values(@ProductName, @IsUp, @UnitPrice, @Remark)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProductName", this.txtName.Text);
            DBHelper.SetParameter("IsUp", this.rbYes.Checked == true ? "是" : "否");
            DBHelper.SetParameter("UnitPrice", this.txtPrice.Text);
            DBHelper.SetParameter("Remark",this.txtRemark.Text);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
                MessageBox.Show("添加成功!");
            else
                MessageBox.Show("添加失败!");
            BindData();
        }

        private void 删除选中商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("你确定要删除吗", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.No)
                return;
            int productId = int.Parse(this.dgvGoods.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "delete from Product where ProductID=@ProductID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProductID", productId);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
                MessageBox.Show("删除成功!");
            else
                MessageBox.Show("删除失败!");
            BindData();
        }

        private void dgvGoods_DoubleClick(object sender, EventArgs e)
        {
            int productId = int.Parse(this.dgvGoods.SelectedRows[0].Cells[0].Value.ToString());
            OrderForm frm = new OrderForm();
            frm.ProductId = productId;
            frm.Show();
        }
    }
}
