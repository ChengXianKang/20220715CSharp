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
    public partial class frmWarnConfig : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();

        private DataTable dtDevice;

        public frmWarnConfig()
        {
            InitializeComponent();
            this.dgwDevice.AutoGenerateColumns = false;
        }

        private void frmWarnConfig_Load(object sender, EventArgs e)
        {
            DataTable dtLine = GetProduceLine();
            cmbLine.ValueMember = "SPL_JobCode";
            cmbLine.DisplayMember = "SPL_Name";
            cmbLine.DataSource = dtLine;
            cmbLine.SelectedIndex = 0;
        }

        /// <summary>
        /// 获取产线列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetProduceLine()
        {
            string sql = "SELECT SPL_JobCode,SPL_Name FROM Store_Procedure_Line ORDER BY SPL_JobCode";
            DataTable dt = conn.ExecuteDataTable(sql, "Base");
            return dt;
        }

        /// <summary>
        /// 获取设备参数
        /// </summary>
        /// <returns></returns>
        private DataTable GetDeviceData(string LineCode)
        {
            string sql = string.Format(@"SELECT dbo.Store_TaiWei.STW_Tid AS Tid, dbo.Store_Procedure_Line.SPL_JobCode, dbo.Store_Procedure_Line.SPL_Name, 
                dbo.Store_GongXu_Main.SGX_JobCode, dbo.Store_GongXu_Main.SGX_Name, dbo.Store_TaiWei.STW_Tid, dbo.Store_TaiWei.STW_JobCode, 
                dbo.Store_TaiWei.STW_CardIP, dbo.Store_TaiWei.STW_CardName,
				ISNULL(SUBSTRING(dbo.Store_TaiWei.STW_WarnType,1,1),'0') AS RwWarn, 
				ISNULL(SUBSTRING(dbo.Store_TaiWei.STW_WarnType,2,1),'0') AS JudgeWarn, 
				ISNULL(SUBSTRING(dbo.Store_TaiWei.STW_WarnType,3,1),'0') AS PointWarn, 
                ISNULL(SUBSTRING(dbo.Store_TaiWei.STW_WarnType,4,1),'0') AS ModelChangeEnabled, 
                dbo.Store_TaiWei.STW_Interval,
				dbo.Store_TaiWei.STW_Name
FROM      dbo.Store_Procedure_Line_Sub LEFT OUTER JOIN
                dbo.Store_Procedure_Line ON 
                dbo.Store_Procedure_Line_Sub.SPLS_SPLTid = dbo.Store_Procedure_Line.SPL_Tid LEFT OUTER JOIN
                dbo.Store_TaiWei ON dbo.Store_Procedure_Line_Sub.SPLS_STWTid = dbo.Store_TaiWei.STW_Tid LEFT OUTER JOIN
                dbo.Store_GongXu_Main ON 
                dbo.Store_Procedure_Line_Sub.SPLS_GongXuID = dbo.Store_GongXu_Main.SGX_Tid
WHERE STW_CardIP <> '0.0.0.0' AND dbo.Store_Procedure_Line.SPL_JobCode = '{0}'
ORDER BY dbo.Store_Procedure_Line.SPL_JobCode, dbo.Store_GongXu_Main.SGX_JobCode, dbo.Store_TaiWei.STW_JobCode", LineCode);
            DataTable dt = conn.ExecuteDataTable(sql, "Base");
            return dt;
        }

        /// <summary>
        /// 选择产线，刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 修改设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwDevice_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            int tid = Convert.ToInt32(this.dgwDevice.Rows[e.RowIndex].Cells["Tid"].Value);
            string sql;
            if (dgwDevice.Columns[e.ColumnIndex].Name == "RwWarn" || dgwDevice.Columns[e.ColumnIndex].Name == "JudgeWarn" || dgwDevice.Columns[e.ColumnIndex].Name == "PointWarn" || dgwDevice.Columns[e.ColumnIndex].Name == "ModelChangeEnabled")
            {
                string warnType = this.dgwDevice.Rows[e.RowIndex].Cells["RwWarn"].Value.ToString() + this.dgwDevice.Rows[e.RowIndex].Cells["JudgeWarn"].Value.ToString() + this.dgwDevice.Rows[e.RowIndex].Cells["PointWarn"].Value.ToString() + this.dgwDevice.Rows[e.RowIndex].Cells["ModelChangeEnabled"].Value.ToString();
                sql = string.Format(@"UPDATE dbo.Store_TaiWei SET STW_WarnType = '{0}' WHERE STW_Tid = {1}", warnType, tid);
            }
            //else if (dgwDevice.Columns[e.ColumnIndex].Name == "Interval")
            //{
            //    string interval = this.dgwDevice.Rows[e.RowIndex].Cells["Interval"].Value.ToString();
            //    sql = string.Format(@"UPDATE dbo.Store_TaiWei SET STW_Interval = {0} WHERE STW_Tid = {1}", interval, tid);
            //}
            else
            {
                return;
            }

            int n = conn.ExecuteAction(sql, "Base");
            if (n <= 0)
            {
                MessageBox.Show(this, "操作失败，请刷新后重试.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 响应CheckBox点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwDevice_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgwDevice.IsCurrentCellDirty)
            {
                dgwDevice.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            if (cmbLine.SelectedIndex < 0) return;
            string lineCode = cmbLine.SelectedValue.ToString();
            this.dtDevice = GetDeviceData(lineCode);
            this.dgwDevice.DataSource = this.dtDevice;
        }
        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 结束编辑，提交单元格内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwDevice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            int tid = Convert.ToInt32(this.dgwDevice.Rows[e.RowIndex].Cells["Tid"].Value);
            string sql;
            if (dgwDevice.Columns[e.ColumnIndex].Name == "Interval")
            {
                string interval = this.dgwDevice.Rows[e.RowIndex].Cells["Interval"].Value.ToString();
                if (string.IsNullOrWhiteSpace(interval))
                {
                    interval = "0";
                }
                sql = string.Format(@"UPDATE dbo.Store_TaiWei SET STW_Interval = {0} WHERE STW_Tid = {1}", interval, tid);
            }
            else
            {
                return;
            }

            int n = conn.ExecuteAction(sql, "Base");
            if (n <= 0)
            {
                MessageBox.Show(this, "操作失败，请刷新后重试.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
