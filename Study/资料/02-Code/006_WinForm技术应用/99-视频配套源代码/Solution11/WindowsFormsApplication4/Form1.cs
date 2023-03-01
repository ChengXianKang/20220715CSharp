using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            DBHelper.PrepareProc("procGetInfoByAcc");
            DBHelper.SetParameter("acc", this.txtAccount.Text);
            DBHelper.SetOutParameter("memName", SqlDbType.NVarChar, 20);
            DBHelper.SetOutParameter("phone", SqlDbType.NVarChar, 20);
            DBHelper.ExecNonQuery();
            this.lblName.Text = "姓名：" + DBHelper.GetParameter("memName").ToString();
            this.lblPhone.Text = "电话：" + DBHelper.GetParameter("phone").ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
