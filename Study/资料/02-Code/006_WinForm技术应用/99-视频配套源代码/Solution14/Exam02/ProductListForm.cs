using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam02
{
    public partial class ProductListForm : Form
    {
        public ProductListForm()
        {
            InitializeComponent();
        }

        private void ProductListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void BindData()
        {
            string sql = "select * from Product left join Category on Product.CategoryID = Category.CategoryID where 1 = 1 ";
            if (!this.txtName.Text.Equals(""))
                sql += " and ProductName like '%" + this.txtName.Text + "%'";
            if (!this.txtRemark.Text.Equals(""))
                sql += " and Remark like '%" + this.txtRemark.Text + "%'";
            DBHelper.PrepareSql(sql);
            this.dgvGoods.AutoGenerateColumns = false;
            this.dgvGoods.DataSource = DBHelper.ExecQuery();
        }
        private void ProductListForm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void 删除选中商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("您确定要删除吗?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.No)
                return;
            int productId = int.Parse(this.dgvGoods.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "delete from Product where ProductID = @ProductID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProductID", productId);
            DBHelper.ExecNonQuery();
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            ProductEditForm frm = new ProductEditForm();
            frm.Owner = this;
            frm.Show();
        }
    }
}
