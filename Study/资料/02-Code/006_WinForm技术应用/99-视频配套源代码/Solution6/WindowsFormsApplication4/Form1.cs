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

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            //显示文件路径
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = this.openFileDialog1.FileName;
            }
            else
                return;
            //显示文本内容
            //方案一：FileStream将文件一次性读取到byte[],然后转换成字符串
            //FileStream fs = new FileStream(this.txtFileName.Text, FileMode.Open, FileAccess.Read);
            //int len = (int)fs.Length;  //获取文件流中的字节长度 
            //byte[] arrByte = new byte[len]; //定义字节数组用于保存内容
            //fs.Read(arrByte, 0, len); //文件流读入字节数组
            //this.txtContent.Text = Encoding.Default.GetString(arrByte); //字节数组转字符串存入文本框
            //fs.Close();

            //方案二：FileStream将文件逐字节读取到byte[],然后转换成字符串
            //FileStream fs = new FileStream(this.txtFileName.Text, FileMode.Open, FileAccess.Read);
            //int len = (int)fs.Length;  //获取文件流中的字节长度 
            //byte[] arrByte = new byte[len]; //定义字节数组用于保存内容
            //int index = 0; //保存变化过程中数组下标
            //int code = fs.ReadByte(); //读取第一个字节，如果code=-1,证明读完了
            //while (code != -1)
            //{
            //    arrByte[index] = Convert.ToByte(code); //读取内容存入字节数组
            //    code = fs.ReadByte();  //继续读字节
            //    index++;
            //}
            //this.txtContent.Text = Encoding.Default.GetString(arrByte); //字节数组转字符串存入文本框
            //fs.Close();

            //方案三：使用File静态类
            //this.txtContent.Text = File.ReadAllText(this.txtFileName.Text,Encoding.Default);

            //方案四：使用StreamReader流读取器读取文件内容
            FileStream fs = new FileStream(this.txtFileName.Text, FileMode.Open, FileAccess.Read);
            StreamReader sd = new StreamReader(fs, Encoding.Default);
            this.txtContent.Text = sd.ReadToEnd();
            sd.Close();
            fs.Close();


        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //方案一：File类静态方法
            //File.WriteAllText(this.txtFileName.Text, this.txtContent.Text,Encoding.Default);
            //MessageBox.Show("保存成功！");

            //方案二：使用StreamWriter流写入器
            FileStream fs = new FileStream(this.txtFileName.Text, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(this.txtContent.Text);
            sw.Close();
            fs.Close();
            MessageBox.Show("保存成功！");

        }
    }
}
