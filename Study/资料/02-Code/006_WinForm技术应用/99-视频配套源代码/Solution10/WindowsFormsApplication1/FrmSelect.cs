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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 绑定专业信息到下拉框
        private void BindProfession()
        {
            DataTable dt = new DataTable();
            DBHelper.PrepareSql("select * from ProfessionInfo");
            dt = DBHelper.ExecQuery();
            DataRow dr = dt.NewRow();
            dr["ProfessionID"] = 0;
            dr["professionName"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            this.cmbPro.DataSource = dt;
            this.cmbPro.DisplayMember = "professionName";
            this.cmbPro.ValueMember = "ProfessionID";
        }
        #endregion

        #region 绑定学生数据
        private void BindData()
        {
            string sql = "select * from StudentInfo inner join ProfessionInfo on StudentInfo.ProfessionID=ProfessionInfo.ProfessionID  where 1 = 1 ";
            if(!this.cmbPro.SelectedValue.ToString().Equals("0"))
                sql += " and StudentInfo.ProfessionID = " + this.cmbPro.SelectedValue.ToString();
            if(!this.txtName.Text.Equals(""))
                sql += " and StuName like '%" + this.txtName.Text + "%'";
            this.dataGridView1.AutoGenerateColumns = false;
            DBHelper.PrepareSql(sql);
            this.dataGridView1.DataSource = DBHelper.ExecQuery();
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            BindProfession();
            BindData();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            FrmAdd frm = new FrmAdd();
            //frm.Owner = this;
            frm.Show();
        }
        private void btEdit_Click(object sender, EventArgs e)
        {
            string stuid = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            FrmEdit frm = new FrmEdit();
            frm.StuID = stuid;
            frm.Show();
        }


        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加是否确定删除的对话框
            DialogResult result = MessageBox.Show("确定要删除数据吗，删除之后无法恢复！", "提示框",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            string stuid = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string sql = "delete from StudentInfo where StuID = @StuID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("StuID", stuid);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
                MessageBox.Show("删除成功!");
            else
                MessageBox.Show("删除失败!");
            BindData();
        }


    }
}
