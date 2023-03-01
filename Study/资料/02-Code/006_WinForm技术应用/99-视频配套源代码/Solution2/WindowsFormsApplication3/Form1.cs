using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 商品入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAdd frm = new FrmAdd();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 商品查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此功能正在努力开发过程中....");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("月底还没到，报表不能查看！");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = string.Format("欢迎来到****系统，当前时间:{0}",
                DateTime.Now.ToShortDateString());

        }
    }
}
