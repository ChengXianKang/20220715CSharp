using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void 普通模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCommon frm = new FrmCommon();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 单例模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingle frm = FrmSingle.CreateForm();
            frm.MdiParent = this;
            frm.Show();

        }
    }
}
