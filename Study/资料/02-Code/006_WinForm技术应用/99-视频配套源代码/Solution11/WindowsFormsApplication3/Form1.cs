using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            DBHelper.PrepareProc("procInsertMember");
            DBHelper.SetParameter("acc", this.txtAccount.Text);
            DBHelper.SetParameter("pwd", this.txtPwd.Text);
            DBHelper.SetParameter("memName", this.txtNickName.Text);
            DBHelper.SetParameter("memPhone", this.txtPhone.Text);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
                MessageBox.Show("新增成功!");
            else
                MessageBox.Show("新增失败");
        }
    }
}
