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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int imgIndex = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = this.imageList1.Images[0];
            this.btPrev.Enabled = false;
        }

        private void btPrev_Click(object sender, EventArgs e)
        {
            imgIndex--;
            this.pictureBox1.Image = this.imageList1.Images[imgIndex];
            SetButtonEnable();
        }
        private void btNext_Click(object sender, EventArgs e)
        {
            imgIndex++;
            this.pictureBox1.Image = this.imageList1.Images[imgIndex];
            SetButtonEnable();
        }

        //控制上一张和下一张按钮的可用性
        private void SetButtonEnable()
        {
            this.btPrev.Enabled = true;
            this.btNext.Enabled = true;
            if(imgIndex == 0)
                this.btPrev.Enabled = false;
            if (imgIndex == this.imageList1.Images.Count - 1)
                this.btNext.Enabled = false;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
