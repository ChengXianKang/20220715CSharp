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
    public partial class Form2 : Form
    {
        public Form2()
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
            //if (!File.Exists(this.txtFileName.Text))
            //{
            //    MessageBox.Show("您选择的文件不存在!");
            //    return;
            //}
            //if (!Directory.Exists(this.txtFolderName.Text))
            //{
            //    MessageBox.Show("您选择的文件夹不存在!");
            //    return;
            //}
            //string[] arrName = this.txtFileName.Text.Split('\\');
            //string newName = arrName[arrName.Length - 1];
            ////第三个参数true代表出现同名文件的时候采取覆盖的操作
            //File.Copy(this.txtFileName.Text, this.txtFolderName.Text + "\\" + newName, true);
            //MessageBox.Show("复制成功！");
            FileInfo fInfo = new FileInfo(this.txtFileName.Text);
            if (fInfo.Exists == false)
            {
                MessageBox.Show("您选择的文件不存在!");
                return;
            }
            DirectoryInfo dInfo = new DirectoryInfo(this.txtFolderName.Text);
            if (dInfo.Exists == false)
            {
                MessageBox.Show("您选择的文件夹不存在!");
                return;
            }
            string[] arrName = this.txtFileName.Text.Split('\\');
            string newName = arrName[arrName.Length - 1];

            fInfo.CopyTo(this.txtFolderName.Text + "\\" + newName,true);
            MessageBox.Show("复制成功");
        }

        private void btMove_Click(object sender, EventArgs e)
        {
            FileInfo fInfo = new FileInfo(this.txtFileName.Text);
            if (fInfo.Exists == false)
            {
                MessageBox.Show("您选择的文件不存在!");
                return;
            }
            DirectoryInfo dInfo = new DirectoryInfo(this.txtFolderName.Text);
            if (dInfo.Exists == false)
            {
                MessageBox.Show("您选择的文件夹不存在!");
                return;
            }
            string[] arrName = this.txtFileName.Text.Split('\\');
            string newName = arrName[arrName.Length - 1];
            FileInfo temp = new FileInfo(this.txtFolderName.Text + "\\" + newName);
            if (temp.Exists == true)
            {
                //MessageBox.Show("文件已经存在!");
                //return;
                temp.Delete();
            }
            fInfo.MoveTo(this.txtFolderName.Text + "\\" + newName);
            MessageBox.Show("移动成功！");
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            FileInfo fInfo = new FileInfo(this.txtFileName.Text);
            if (fInfo.Exists == false)
            {
                MessageBox.Show("您选择的文件不存在!");
                return;
            }
            fInfo.Delete();
            MessageBox.Show("删除成功!");
        }
    }
}
