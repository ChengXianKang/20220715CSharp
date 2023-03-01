using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyNike.Basic;
using MyNike.Erp;
using MyNike.DataSelect;

namespace MyNike
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.lblWelCome.Text = LoginConfig.SalesmanName + "(" + LoginConfig.Role + "),欢迎您";
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss");
        }

        #region 商品分类管理
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            FrmType frm = new FrmType();
            frm.MdiParent = this;
            frm.Show();
            frm.Top = 230;
        }
        #endregion

        #region 员工管理
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FrmSaleMan frm = new FrmSaleMan();
            frm.MdiParent = this;
            frm.Show();
            frm.Top = 230;
        }
        #endregion

        #region 商品浏览
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmGoodView frm = new FrmGoodView();
            frm.MdiParent = this;
            frm.Show();
            frm.Top = 230;
        }
        #endregion

        #region 商品入库
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FrmGoodPutIn frm = new FrmGoodPutIn();
            frm.MdiParent = this;
            frm.Show();
            frm.Top = 230;
        }
        #endregion

        #region 收银台
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmCash frm = new FrmCash();          
            frm.Show();
            this.Hide();
        }
        #endregion

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            FrmSalary frm = new FrmSalary();
            frm.MdiParent = this;
            frm.Show();
            frm.Top = 230;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            FrmSales frm = new FrmSales();
            frm.MdiParent = this;
            frm.Show();
            frm.Top = 230;
        }
    }
}
