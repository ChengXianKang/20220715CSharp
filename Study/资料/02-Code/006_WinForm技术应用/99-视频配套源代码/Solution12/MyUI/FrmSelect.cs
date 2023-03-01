using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyEntity;
using MyBLL;
namespace MyUI
{
    public partial class FrmSelect : Form
    {
        ProfessionInfoBLL proBll = new ProfessionInfoBLL();
        StudentInfoBLL stuBll = new StudentInfoBLL();
        public FrmSelect()
        {
            InitializeComponent();
        }

        #region 绑定下拉框
        private void BindPro()
        {
            List<ProfessionInfoEntity> list = new List<ProfessionInfoEntity>();
            list = proBll.List();
            list.Insert(0,new ProfessionInfoEntity { ProfessionID=0,ProfessionName="--请选择--"});
            this.cmbPro.DataSource = list;
            this.cmbPro.DisplayMember = "ProfessionName";
            this.cmbPro.ValueMember = "ProfessionID";
        }
        #endregion

        #region 绑定学生数据
        private void BindData()
        {
            StudentInfoEntiy searchObj = new StudentInfoEntiy();
            searchObj.ProfessionID = int.Parse(this.cmbPro.SelectedValue.ToString());
            searchObj.StuName = this.txtName.Text;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = stuBll.Search(searchObj);
        }
        #endregion

        #region 窗体加载
        private void FrmSelect_Load(object sender, EventArgs e)
        {
            BindPro();
            BindData();
        }
        #endregion

        #region 搜索按钮
        private void btSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加是否确定删除的对话框
            DialogResult result = MessageBox.Show("确定要删除数据吗，删除之后无法恢复！", "提示框",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            string stuid = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if(stuBll.Delete(stuid) == 1)
                MessageBox.Show("删除成功!");
            else
                MessageBox.Show("删除失败!");
            BindData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            FrmAdd frm = new FrmAdd();
            frm.Show();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            string stuid = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            FrmEdit frm = new FrmEdit();
            frm.StuID = stuid;
            frm.Show();
        }
    }
}
