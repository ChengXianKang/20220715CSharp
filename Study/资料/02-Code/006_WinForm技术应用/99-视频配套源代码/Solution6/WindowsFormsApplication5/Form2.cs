using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        List<Student> list = new List<Student>();
        private void Form2_Load(object sender, EventArgs e)
        {
            BindData();
        }

        //绑定数据
        private void BindData()
        {
            if (!File.Exists("list.ini"))
                return;
            FileStream fs = new FileStream("list.ini", FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            list = (List<Student>)bf.Deserialize(fs);
            fs.Close();
            this.listView1.Items.Clear();
            foreach (Student stu in list)
            {
                ListViewItem item = new ListViewItem(stu.StuNum);
                item.SubItems.Add(stu.StuName);
                item.SubItems.Add(stu.StuAge.ToString());
                this.listView1.Items.Add(item);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            Student stu = new Student();
            stu.StuNum = this.txtNum.Text;
            stu.StuName = this.txtName.Text;
            stu.StuAge = int.Parse(this.txtAge.Text);
            list.Add(stu);

            FileStream fs = new FileStream("list.ini", FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, list);
            fs.Close();
            MessageBox.Show("保存成功!");
            BindData();

        }
    }
}
