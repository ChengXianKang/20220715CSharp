using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form2 : Form
    {
        public Form2()
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
            DirectoryInfo startInfo = new DirectoryInfo(this.txtFolder1.Text);
            DirectoryInfo endInfo = new DirectoryInfo(this.txtFolder2.Text);
            if (startInfo.Exists == false || endInfo.Exists == false)
            {
                MessageBox.Show("文件夹不存在");
                return;
            }
            string[] arrFolderName = this.txtFolder1.Text.Split('\\');
            string newName = arrFolderName[arrFolderName.Length - 1];
            DirectoryInfo temp = new DirectoryInfo(this.txtFolder2.Text + "\\" + newName);
            if (temp.Exists == true)
            {
                //方案一:
                //MessageBox.Show("在目标位置，具有同名的文件夹!");

                //return;
                temp.Delete(true);
            }
            //此移动操作和Directory操作一样，只能在相同盘符去操作
            startInfo.MoveTo(this.txtFolder2.Text + "\\" + newName);
            MessageBox.Show("移动成功!");
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            DirectoryInfo startInfo = new DirectoryInfo(this.txtFolder1.Text);
            if (startInfo.Exists == false)
            {
                MessageBox.Show("文件夹不存在");
                return;
            }
            startInfo.Delete(true);
            MessageBox.Show("删除成功!");
        }
    }
}
