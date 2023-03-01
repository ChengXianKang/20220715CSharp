using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void BindData()
        {
            string sql = "select * from y_Student inner join y_Class on y_Student.ClassId = y_Class.ClassId";
            DBHelper.PrepareSql(sql);
            this.dgvStudent.AutoGenerateColumns = false;
            this.dgvStudent.DataSource = DBHelper.ExecQuery();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            int StuID = int.Parse(this.dgvStudent.SelectedRows[0].Cells[0].Value.ToString());
            FrmEdit frm = new FrmEdit();
            frm.StuID = StuID;
            frm.Owner = this;
            frm.Show();
        }
    }
}
