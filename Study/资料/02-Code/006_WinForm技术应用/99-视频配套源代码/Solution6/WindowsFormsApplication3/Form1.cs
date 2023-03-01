using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void btSelFolder1_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder1.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btSelFolder2_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder2.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btMove_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.txtFolder1.Text) || !Directory.Exists(this.txtFolder2.Text))
            {
                MessageBox.Show("文件夹不存在!");
                return;
            }
            string[] arrFolderName = this.txtFolder1.Text.Split('\\');
            string newName = arrFolderName[arrFolderName.Length-1];
            if (Directory.Exists(this.txtFolder2.Text + "\\" + newName))
            {
                //MessageBox.Show("对不起，目标文件夹已经存在了");
                //return;
                Directory.Delete(this.txtFolder2.Text + "\\" + newName,true);
            }
            //move只支持相同盘符操作。
            Directory.Move(this.txtFolder1.Text, this.txtFolder2.Text+"\\"+ newName);

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.txtFolder1.Text))
            {
                MessageBox.Show("文件夹不存在!");
                return;
            }
            Directory.Delete(this.txtFolder1.Text,true);
        }
    }
}
