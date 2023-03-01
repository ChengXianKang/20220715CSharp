using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comType.Items.Add("--请选择--");
            this.comType.Items.Add("数码产品");
            this.comType.Items.Add("家居产品");
            this.comType.Items.Add("衣服鞋帽");
            this.comType.Items.Add("母婴幼儿");
            this.comType.Text = "--请选择--";
            this.lblType.Text = "";
        }

        private void comType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblType.Text = this.comType.Text;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            #region 注释代码
            //string str = "";
            //if (this.checkBox1.Checked == true)
            //{
            //    if (!str.Equals(""))
            //        str += ",";
            //    str += this.checkBox1.Text;
            //}               
            //if (this.checkBox2.Checked == true)
            //{
            //    if (!str.Equals(""))
            //        str += ",";
            //    str += this.checkBox2.Text;
            //}
            //if (this.checkBox3.Checked == true)
            //{
            //    if (!str.Equals(""))
            //        str += ",";
            //    str += this.checkBox3.Text;
            //}
            //if (this.checkBox4.Checked == true)
            //{
            //    if (!str.Equals(""))
            //        str += ",";
            //    str += this.checkBox4.Text;
            //}

            //MessageBox.Show("促销时间:" + str);

            //string str = "";
            //foreach (CheckBox item in this.panel1.Controls)
            //{
            //    if (item.Checked == true)
            //    {
            //        if (!str.Equals(""))
            //            str += ",";
            //        str += item.Text;
            //    }
            //}
            //MessageBox.Show("促销时间:" + str);
            #endregion

            //if (this.radioButton1.Checked == true)
            //    MessageBox.Show("促销方案:" + this.radioButton1.Text);
            //if (this.radioButton2.Checked == true)
            //    MessageBox.Show("促销方案:" + this.radioButton2.Text);
            //if (this.radioButton3.Checked == true)
            //    MessageBox.Show("促销方案:" + this.radioButton3.Text);
            //if (this.radioButton4.Checked == true)
            //    MessageBox.Show("促销方案:" + this.radioButton4.Text);
            //if (this.radioButton5.Checked == true)
            //    MessageBox.Show("促销方案:" + this.radioButton5.Text);
            foreach (RadioButton item in this.panel2.Controls)
            {
                if (item.Checked == true)
                {
                    MessageBox.Show("促销方案:" + item.Text);
                    break;
                }
            }

        }


    }
}
