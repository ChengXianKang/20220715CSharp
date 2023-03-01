using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam04
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void BindData()
        {
            string sql = @"select *,
                case 
                    when S_type = 0 then '在读'
	                else '毕业'
                end S_type_cn
                from Mysys_Student where 1 = 1 ";
            sql += string.Format(" and S_Time1 between '{0}' and '{1}'",
                this.dtpStart.Value.ToString("yyyy-MM-dd") + " 00:00:00",
                this.dtpEnd.Value.ToString("yyyy-MM-dd") + " 23:59:59");
            if (this.cbType.Checked == true)
                sql += " and S_type = 0";
            else  //如果没勾代表所有，此else不需要，如果勾了代表查毕业才需要此else
                sql += " and S_type = 1";
            DBHelper.PrepareSql(sql);
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = DBHelper.ExecQuery();
        }

        private void btBiye_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要毕业的学生!");
                return;
            }
            if (this.dataGridView1.SelectedRows[0].Cells[0].Value == null)
            {
                MessageBox.Show("请选择要毕业的学生!");
                return;
            }
            DialogResult r = MessageBox.Show("确定学生是否毕业", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (r == DialogResult.Cancel)
                return;
            int sid = int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "update Mysys_Student set S_type = 1 where S_id = " + sid;
            DBHelper.PrepareSql(sql);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("毕业成功!");
            }
            else
            {
                MessageBox.Show("毕业失败!");
            }
            BindData();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            if (!this.dtpStart.Value.ToString("yyyyMMdd").Equals(this.dtpEnd.Value.ToString("yyyyMMdd")))
            {
                if (this.dtpStart.Value > this.dtpEnd.Value)
                {
                    MessageBox.Show("开始时间不能大于结束时间");
                    return;
                }
            }
            BindData();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void cbType_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }

    }
}
