using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNike.Erp
{
    public partial class FrmGoodPutIn : Form
    {
        public FrmGoodPutIn()
        {
            InitializeComponent();
        }
        private void BindType()
        {
            DataTable dt = new DataTable();
            DBHelper.PrepareSql("select * from [Type]");
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["TypeName"] = "--请选择--";
            dr["TypeId"] = "0";
            dt.Rows.InsertAt(dr, 0);
            this.cmbType.DataSource = dt;
            this.cmbType.DisplayMember = "TypeName";
            this.cmbType.ValueMember = "TypeID";
        }

        private void btCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmGoodPutIn_Load(object sender, EventArgs e)
        {
            BindType();
        }

        private void btReadInfo_Click(object sender, EventArgs e)
        {
            DBHelper.PrepareSql("select * from Goods where BarCode = @BarCode");
            DBHelper.SetParameter("BarCode", this.txt_goodsCode.Text);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该商品为全新商品！");
            }
            else
            {
                this.txt_goodsName.Text = dt.Rows[0]["GoodsName"].ToString();
                this.cmbType.SelectedValue = int.Parse(dt.Rows[0]["TypeID"].ToString());
                this.txt_storePrice.Text = dt.Rows[0]["StorePrice"].ToString();
                this.txt_salePrice.Text = dt.Rows[0]["SalePrice"].ToString();
                this.txt_discount.Text = dt.Rows[0]["Discount"].ToString();
            }
        }

        private void btIn_Click(object sender, EventArgs e)
        {

            DBHelper.PrepareSql("select * from Goods where BarCode = @BarCode");
            DBHelper.SetParameter("BarCode", this.txt_goodsCode.Text);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 0)
            {
                string sql = "insert into Goods(BarCode,TypeID,GoodsName,StorePrice,SalePrice,Discount,StockNum,StockDate) values(@BarCode,@TypeID,@GoodsName,@StorePrice,@SalePrice,@Discount,@StockNum,getdate())";
                DBHelper.PrepareSql(sql);
                DBHelper.SetParameter("BarCode",this.txt_goodsCode.Text);
                DBHelper.SetParameter("TypeID", this.cmbType.SelectedValue.ToString());
                DBHelper.SetParameter("GoodsName", this.txt_goodsName.Text);
                DBHelper.SetParameter("StorePrice", this.txt_storePrice.Text);
                DBHelper.SetParameter("SalePrice", this.txt_salePrice.Text);
                DBHelper.SetParameter("Discount", this.txt_discount.Text);
                DBHelper.SetParameter("StockNum", this.txt_stockNum.Text);
                int rowCount = DBHelper.ExecNonQuery();
                if (rowCount == 1)
                    MessageBox.Show("入库成功!");
                else
                    MessageBox.Show("入库失败!");
            }
            else
            {
                string sql = "update Goods set TypeID=@TypeID,GoodsName=@GoodsName,StorePrice=@StorePrice,SalePrice=@SalePrice,Discount=@Discount,StockNum=StockNum+@StockNum,StockDate=getdate() where BarCode=@BarCode";
                DBHelper.PrepareSql(sql);
                DBHelper.SetParameter("TypeID", this.cmbType.SelectedValue.ToString());
                DBHelper.SetParameter("GoodsName", this.txt_goodsName.Text);
                DBHelper.SetParameter("StorePrice", this.txt_storePrice.Text);
                DBHelper.SetParameter("SalePrice", this.txt_salePrice.Text);
                DBHelper.SetParameter("Discount", this.txt_discount.Text);
                DBHelper.SetParameter("StockNum", this.txt_stockNum.Text);
                DBHelper.SetParameter("BarCode", this.txt_goodsCode.Text);
                int rowCount = DBHelper.ExecNonQuery();
                if (rowCount == 1)
                    MessageBox.Show("入库成功!");
                else
                    MessageBox.Show("入库失败!");
            }
        }
    }
}
