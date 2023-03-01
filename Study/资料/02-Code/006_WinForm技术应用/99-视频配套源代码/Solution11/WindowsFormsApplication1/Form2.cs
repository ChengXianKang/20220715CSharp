using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            DBHelper.PrepareSql(string.Format("exec procInsertMember '{0}','{1}','{2}','{3}'"
                ,this.txtAccount.Text,this.txtPwd.Text,this.txtNickName.Text,this.txtPhone.Text));
            DBHelper.ExecNonQuery();
        }
    }
}
