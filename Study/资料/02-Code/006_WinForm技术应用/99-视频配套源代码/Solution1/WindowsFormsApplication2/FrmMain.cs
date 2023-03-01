using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            MessageBox.Show("欢迎来到****系统");
        }

        private void FrmMain_Click(object sender, EventArgs e)
        {
            MessageBox.Show("我被主人点击了一下！");
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("窗体马上要关掉了！");
            DialogResult result =  MessageBox.Show("确定关闭此窗体吗?", "提示框", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.No)
                e.Cancel = true; //取消窗体关闭
        }
    }
}
