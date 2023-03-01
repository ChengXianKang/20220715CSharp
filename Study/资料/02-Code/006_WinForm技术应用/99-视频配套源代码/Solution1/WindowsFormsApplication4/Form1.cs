using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //判断是否用户点击关闭按钮
            if(e.CloseReason == CloseReason.UserClosing)
            {
                MessageBox.Show("关闭窗口也改变不了你喜欢我的事实！");
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bt1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("就知道你喜欢我！");
            Application.Exit();
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("就知道你喜欢我！");
            Application.Exit();
        }

        private void bt1_MouseEnter(object sender, EventArgs e)
        {
            if (this.bt1.Text.Equals("不  是"))
            {
                this.bt1.Text = "是  的";
                this.bt2.Text = "不  是";
            }
        }

        private void bt2_MouseEnter(object sender, EventArgs e)
        {
            if (this.bt2.Text.Equals("不  是"))
            {
                this.bt2.Text = "是  的";
                this.bt1.Text = "不  是";
            }
        }

    }
}
