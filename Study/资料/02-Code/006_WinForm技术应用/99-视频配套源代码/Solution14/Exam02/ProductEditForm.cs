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
    public partial class ProductEditForm : Form
    {
        public ProductEditForm()
        {
            InitializeComponent();
        }

        private void BindCmb()
        {
            string sql = "select * from Category";
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["CategoryID"] = 0;
            dr["CategoryName"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            this.cmbType.DataSource = dt;
            this.cmbType.ValueMember = "CategoryID";
            this.cmbType.DisplayMember = "CategoryName";
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Equals(""))
            {
                MessageBox.Show("商品名称不能为空!");
                return;
            }
            if (this.txtPrice.Text.Equals(""))
            {
                MessageBox.Show("单价不能为空!");
                return;
            }
            double price;
            bool b = double.TryParse(this.txtPrice.Text, out price);
            if (b == false)
            {
                MessageBox.Show("单价必须是数字!");
                return;
            }
            string sql = "insert into Product(ProductName,IsUp,UnitPrice,Remark,CategoryID) values(@ProductName,@IsUp,@UnitPrice,@Remark,@CategoryID)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ProductName",this.txtName.Text);
            DBHelper.SetParameter("IsUp",this.rbYes.Checked==true?"是":"否");
            DBHelper.SetParameter("UnitPrice",this.txtPrice.Text);
            DBHelper.SetParameter("Remark",this.txtRemark.Text);
            DBHelper.SetParameter("CategoryID",this.cmbType.SelectedValue.ToString());
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("添加成功!");
            }
            else
            {
                MessageBox.Show("添加失败!");
            }
           ((ProductListForm)this.Owner).BindData();
            this.Close();
        }

        private void ProductEditForm_Load(object sender, EventArgs e)
        {
            BindCmb();
        }
    }
}
