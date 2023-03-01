using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDC
{
    public partial class dlgMaterialSelect : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();

        private DataTable dtMaterial = new DataTable();

        public string SM_Tid = "";
        public string SM_nbCode = "";
        public string SM_Name = "";
        public string SM_Spec = "";

        public dlgMaterialSelect()
        {
            InitializeComponent();
            dgwMaterial.AutoGenerateColumns = false;
            dgwMaterial.RowPostPaint += this.dgwMaterial_RowPostPaint;

        }

        private void dlgMaterialSelect_Load(object sender, EventArgs e)
        {
            SM_Tid = "";
            SM_nbCode = "";
            SM_Name = "";
            SM_Spec = "";
            this.Text = "选择成品物料（正在加载物料列表...）";
            bgwGetData.RunWorkerAsync();
        }

        private void dgwMaterial_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //自动编号，与数据无关
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               dgwMaterial.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                  (e.RowIndex + 1).ToString(),
                   dgwMaterial.RowHeadersDefaultCellStyle.Font,
                   rectangle,
                   dgwMaterial.RowHeadersDefaultCellStyle.ForeColor,
                   TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private DataTable GetMaterial()
        {
            string sql = @"SELECT SM_Tid
                            ,SM_nbCode
                            ,SM_Name
                            ,SM_Spec
                        FROM Store_Material
                        WHERE SM_Type = '成品'
                        ORDER BY SM_Name DESC,SM_nbCode,SM_Spec";
            DataTable dt = conn.ExecuteDataTable(sql, "Base");
            return dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text))
            {
                this.dgwMaterial.DataSource = this.dtMaterial;
                this.Refresh();
            }
            else
            {
                DataView dv = new DataView(this.dtMaterial);
                dv.RowFilter = string.Format("SM_nbCode LIKE '%{0}%' OR SM_Spec LIKE '%{0}%'", txtKey.Text.Trim());
                this.dgwMaterial.DataSource = dv;
                this.Refresh();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgwMaterial.SelectedRows.Count == 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            DataGridViewRow row = dgwMaterial.SelectedRows[0];
            SM_Tid = row.Cells["Tid"].Value.ToString();
            SM_nbCode = row.Cells["SMCode"].Value.ToString();
            SM_Name = row.Cells["SMName"].Value.ToString();
            SM_Spec = row.Cells["SMSpec"].Value.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void bgwGetData_DoWork(object sender, DoWorkEventArgs e)
        {
            this.dtMaterial = GetMaterial();
        }

        private void bgwGetData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dgwMaterial.DataSource = this.dtMaterial;
            this.Text = "选择成品物料";
            this.Refresh();
        }
    }
}
