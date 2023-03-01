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

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btOpenFile_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = this.openFileDialog1.FileName;
            }
        }

        private void txtOpenFolder_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFolderName.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtFileName.Text))
            {
                MessageBox.Show("您选择的文件不存在!");
                return;
            }
            if (!Directory.Exists(this.txtFolderName.Text))
            {
                MessageBox.Show("您选择的文件夹不存在!");
                return;
            }
            string[] arrName = this.txtFileName.Text.Split('\\');
            string newName = arrName[arrName.Length-1];
            //第三个参数true代表出现同名文件的时候采取覆盖的操作
            File.Copy(this.txtFileName.Text, this.txtFolderName.Text + "\\" + newName, true);
            MessageBox.Show("复制成功！");
        }

        private void btMove_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtFileName.Text))
            {
                MessageBox.Show("您选择的文件不存在!");
                return;
            }
            if (!Directory.Exists(this.txtFolderName.Text))
            {
                MessageBox.Show("您选择的文件夹不存在!");
                return;
            }
            string[] arrName = this.txtFileName.Text.Split('\\');
            string newName = arrName[arrName.Length - 1];
            if (File.Exists(this.txtFolderName.Text + "\\" + newName))
            {
                //MessageBox.Show("目标文件已经存在，不能移动!");
                //return;

                File.Delete(this.txtFolderName.Text + "\\" + newName);
            }
            File.Move(this.txtFileName.Text, this.txtFolderName.Text + "\\" + newName);
            MessageBox.Show("移动成功!");
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtFileName.Text))
            {
                MessageBox.Show("您选择的文件不存在!");
                return;
            }
            File.Delete(this.txtFileName.Text);
            MessageBox.Show("删除成功!");
        }
    }
}
