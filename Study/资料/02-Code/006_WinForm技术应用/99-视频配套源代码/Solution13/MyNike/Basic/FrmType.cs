using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNike.Basic
{
    public partial class FrmType : Form
    {
        public FrmType()
        {
            InitializeComponent();
        }

        private void BindData()
        {
            DBHelper.PrepareSql("select * from [Type]");
            this.dgvType.DataSource = DBHelper.ExecQuery();
        }
        private void FrmType_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            string sql = "insert into [Type](TypeName) values(@TypeName)";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("TypeName", this.txtType.Text);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("数据添加成功!");
                BindData();
            }
            else
            {
                MessageBox.Show("数据添加失败!");
            }
        }

        private void dgvType_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.dgvType.SelectedRows[0].Cells[0].Value.ToString());
            DBHelper.PrepareSql("select * from [Type] where TypeId = @TypeId");
            DBHelper.SetParameter("TypeId", id);
            DataTable dt = new DataTable();
            dt = DBHelper.ExecQuery();
            if (dt.Rows.Count != 1)
            {
                MessageBox.Show("获取不到数据!");
                BindData();
                return;
            }
            this.txtType.Text = dt.Rows[0]["TypeName"].ToString();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            int typeId = int.Parse(this.dgvType.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "update  [Type]  set TypeName=@TypeName where TypeID=@TypeID";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("TypeName", this.txtType.Text);
            DBHelper.SetParameter("TypeID", typeId);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("数据修改成功!");
                BindData();
            }
            else
            {
                MessageBox.Show("数据修改失败!");
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result =  MessageBox.Show("你是否真的要删除，删除后无法恢复!", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
                return;
            int typeId = int.Parse(this.dgvType.SelectedRows[0].Cells[0].Value.ToString());
            string sql = "delete from [Type] where TypeId = @TypeId ";
            DBHelper.PrepareSql(sql);
            DBHelper.SetParameter("TypeID", typeId);
            int rowCount = DBHelper.ExecNonQuery();
            if (rowCount == 1)
            {
                MessageBox.Show("数据删除成功!");
                BindData();
            }
            else
            {
                MessageBox.Show("数据删除失败!");
            }
        }
    }
}
