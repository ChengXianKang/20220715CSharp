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
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btWrite_Click(object sender, EventArgs e)
        {
            Student stu = new Student();
            stu.StuNum = this.txtNum.Text;
            stu.StuName = this.txtName.Text;
            stu.StuAge = int.Parse(this.txtAge.Text);
            FileStream fs = new FileStream("stu.ini", FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, stu); //序列化
            fs.Close();
            MessageBox.Show("序列化成功!");
            
        }

        private void btRead_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("stu.ini", FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            Student stu = (Student)bf.Deserialize(fs);
            this.txtNum.Text = stu.StuNum;
            this.txtName.Text = stu.StuName;
            this.txtAge.Text = stu.StuAge.ToString();
            fs.Close();
        }
    }
}
