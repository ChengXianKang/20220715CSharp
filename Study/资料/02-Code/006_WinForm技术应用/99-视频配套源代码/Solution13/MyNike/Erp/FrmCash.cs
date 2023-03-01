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
    public partial class FrmCash : Form
    {
        public FrmCash()
        {
            InitializeComponent();
        }

        private void FrmCash_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void BindControl()
        {
            //绑定导购员下拉框
            string sql = "select * from SalesMan where Role = '导购员'";
            DBHelper.PrepareSql(sql);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["SalesmanID"] = "0";
            dr["SalesmanName"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            this.cmbDaogou.DataSource = dt;
            this.cmbDaogou.ValueMember = "SalesmanID";
            this.cmbDaogou.DisplayMember = "SalesmanName";
            //流水号
            this.lblLiushui.Text = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            //绑定总金额
            this.lblSumMoney.Text = "共：¥0.00元";
            //绑定商品数量
            this.lblCount.Text = "商品数量：0";
            //绑定收银员
            this.lblCashier.Text = "收银员：" + LoginConfig.SalesmanName;
            //绑定应收，实收，找零文本框
            this.txtYingshou.Text = "0.00";
            this.txtShishou.Text = "0.00";
            this.txtZhaolin.Text = "0.00";
        }
        private void FrmCash_Load(object sender, EventArgs e)
        {
            BindControl();
        }

        #region 当商品列表发生改变的时候跟新其他所有控件的值
        private void UpdateInfo()
        {
            double sumMoney = 0;
            int sumCount = 0;
            foreach (ListViewItem good in this.lvGoods.Items)
            {
                sumMoney += int.Parse(good.SubItems[6].Text) * double.Parse(good.SubItems[5].Text);
                sumCount += int.Parse(good.SubItems[6].Text);
            }
            this.lblSumMoney.Text = "共：¥"+sumMoney.ToString("F2")+"元";
            this.lblCount.Text = "商品数量："+sumCount;
            this.txtYingshou.Text = sumMoney.ToString("F2");
            this.txtZhaolin.Text = (double.Parse(this.txtShishou.Text)-double.Parse(this.txtYingshou.Text)).ToString("F2");
        }
        #endregion

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = (int)e.KeyChar;
            if (keyCode != 13) //判断不是回车键，退出
                return;
            foreach (ListViewItem good in this.lvGoods.Items)
            {
                if (this.txtBarCode.Text.Equals(good.Text))
                {
                    int goodCount = int.Parse(good.SubItems[6].Text);
                    goodCount++;
                    good.SubItems[6].Text = goodCount.ToString();
                    UpdateInfo();
                    return;
                }
            }
            string sql = "select * from Goods inner join [Type] on Goods.TypeID = [Type].TypeID where BarCode = @BarCode";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("BarCode", this.txtBarCode.Text);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("没有找到该商品!");
                return;
            }
            ListViewItem item = new ListViewItem(dt.Rows[0]["BarCode"].ToString());
            item.SubItems.Add(dt.Rows[0]["GoodsName"].ToString());
            item.SubItems.Add(dt.Rows[0]["TypeName"].ToString());
            double salePrice = double.Parse(dt.Rows[0]["SalePrice"].ToString());
            double discount = double.Parse(dt.Rows[0]["Discount"].ToString());
            item.SubItems.Add(salePrice.ToString());
            item.SubItems.Add(discount.ToString());
            item.SubItems.Add((salePrice* discount).ToString("F2"));
            item.SubItems.Add("1");
            item.SubItems.Add(dt.Rows[0]["GoodsID"].ToString());
            this.lvGoods.Items.Add(item);
            UpdateInfo();
        }

        private void lvGoods_KeyPress(object sender, KeyPressEventArgs e)
        {
            //+ 43  - 45  backspace 8
            if (this.lvGoods.SelectedItems.Count == 0)
                return;
            int keyCode = (int)e.KeyChar;
            if (keyCode == 43) //+
            {
                int goodCount = int.Parse(this.lvGoods.SelectedItems[0].SubItems[6].Text);
                goodCount++;
                this.lvGoods.SelectedItems[0].SubItems[6].Text = goodCount.ToString();
            }
            if (keyCode == 45) //-
            {
                int goodCount = int.Parse(this.lvGoods.SelectedItems[0].SubItems[6].Text);
                if (goodCount == 1)
                    return;
                goodCount--;
                this.lvGoods.SelectedItems[0].SubItems[6].Text = goodCount.ToString();
            }
            if (keyCode == 8) //backspace
            {
                this.lvGoods.SelectedItems[0].Remove();
            }
            UpdateInfo();
        }

        private void txtShishou_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = (int)e.KeyChar;
            if (keyCode != 13)
                return;
            this.txtZhaolin.Text = (double.Parse(this.txtShishou.Text) - double.Parse(this.txtYingshou.Text)).ToString("F2");
        }

        private void btJiesuan_Click(object sender, EventArgs e)
        {
            //账单
            string sql = "insert into Sales(ReceiptsCode,SalesDate,AllMoney,SalesmanID,CashierID) values(@ReceiptsCode, getdate(), @AllMoney, @SalesmanID, @CashierID) select @@identity";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("ReceiptsCode",this.lblLiushui.Text);
            DBHelper.SetParameter("AllMoney", this.txtYingshou.Text);
            DBHelper.SetParameter("SalesmanID", this.cmbDaogou.SelectedValue.ToString());
            DBHelper.SetParameter("CashierID", LoginConfig.SalesmanID);
            int salesId = int.Parse(DBHelper.ExecScalar().ToString());
            //明细
            foreach (ListViewItem good in this.lvGoods.Items)
            {
                sql = "insert into SalesDetail(SalesID,GoodsID,Quantity,GoodsMoney) values(@SalesID,@GoodsID,@Quantity,@GoodsMoney)";
                DBHelper.PrepareSql(sql);
                DBHelper.SetParameter("SalesID", salesId);
                DBHelper.SetParameter("GoodsID", good.SubItems[7].Text);
                DBHelper.SetParameter("Quantity", good.SubItems[6].Text);
                DBHelper.SetParameter("GoodsMoney", good.SubItems[5].Text);
                DBHelper.ExecNonQuery();
            }
            MessageBox.Show("结算成功!");
            this.txtBarCode.Text = "";
            this.lvGoods.Items.Clear();
            BindControl();
        }
    }
}
