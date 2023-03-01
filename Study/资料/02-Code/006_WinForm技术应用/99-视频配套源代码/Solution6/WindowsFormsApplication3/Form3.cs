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
    public partial class Form3 : Form
    {
        public Form3()
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

        /// <summary>
        /// 编写通用的递归方法，实现文件夹复制
        /// </summary>
        /// <param name="startFolder">原始文件夹路径</param>
        /// <param name="endFolder">目标文件夹路径</param>
        //endFolderPath:处理之后的文件夹路径
        //例如将D:\temp\Solution4文件夹复制到E:\FileTest，
        //那么endFolderPath参数应该是“E:\FileTest\Solution4”
        private void CopyFolder(string startFolderPath, string endFolderPath)
        {
            //创建文件夹
            Directory.CreateDirectory(endFolderPath);
            //循环复制文件夹下面的所有文件
            DirectoryInfo startDir = new DirectoryInfo(startFolderPath);
            foreach (FileInfo item in startDir.GetFiles())
            {
                item.CopyTo(endFolderPath + "\\" + item.Name);
            }
            //循环所有的子目录，形成递归调用
            foreach (DirectoryInfo item in startDir.GetDirectories())
            {
                string startPath = item.FullName;
                string endPath = endFolderPath + "\\" + item.Name;
                CopyFolder(startPath, endPath);
            }
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            //判断两个文件夹是否存在
            if (!Directory.Exists(this.txtFolder1.Text) || !Directory.Exists(this.txtFolder2.Text))
            {
                MessageBox.Show("文件夹已经存在");
                return;
            }
            //判断目标地址是否已经有了同名的文件夹
            string[] arrFolderName = this.txtFolder1.Text.Split('\\');
            string newName = arrFolderName[arrFolderName.Length-1];
            if (Directory.Exists(this.txtFolder2.Text + "\\" + newName))
            {
                //方案一:
                //MessageBox.Show("目标地址已经有了同名的文件夹");
                //return;
                Directory.Delete(this.txtFolder2.Text + "\\" + newName,true);
            }
            CopyFolder(this.txtFolder1.Text, this.txtFolder2.Text + "\\" + newName);
            MessageBox.Show("复制成功!");
        }

        private void btMove_Click(object sender, EventArgs e)
        {
            //判断两个文件夹是否存在
            if (!Directory.Exists(this.txtFolder1.Text) || !Directory.Exists(this.txtFolder2.Text))
            {
                MessageBox.Show("文件夹已经存在");
                return;
            }
            //判断目标地址是否已经有了同名的文件夹
            string[] arrFolderName = this.txtFolder1.Text.Split('\\');
            string newName = arrFolderName[arrFolderName.Length - 1];
            if (Directory.Exists(this.txtFolder2.Text + "\\" + newName))
            {
                //方案一:
                //MessageBox.Show("目标地址已经有了同名的文件夹");
                //return;
                Directory.Delete(this.txtFolder2.Text + "\\" + newName, true);
            }
            CopyFolder(this.txtFolder1.Text, this.txtFolder2.Text + "\\" + newName);
            Directory.Delete(this.txtFolder1.Text,true);
            MessageBox.Show("移动成功!");
        }

    }
}
