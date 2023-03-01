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
    public partial class frmAutoScanConfig : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();

        private DataTable dtAutoScan = new DataTable();

        public frmAutoScanConfig()
        {
            InitializeComponent();
            dgwAutoScan.AutoGenerateColumns = false;
        }

        private void dgwAutoScan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //自动编号，与数据无关
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               dgwAutoScan.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                  (e.RowIndex + 1).ToString(),
                   dgwAutoScan.RowHeadersDefaultCellStyle.Font,
                   rectangle,
                   dgwAutoScan.RowHeadersDefaultCellStyle.ForeColor,
                   TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void tXD_AutoScan_ConfigBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "是否保存数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;
            this.Validate();
            this.tXD_AutoScan_ConfigBindingSource.EndEdit();
            MessageBox.Show("保存成功！");
        }

        private void frmAutoScanConfig_Load(object sender, EventArgs e)
        {
            dgwAutoScan.Enabled = true;

            txtDeviceIP.Enabled = false;
            txtLineCode.Enabled = false;
            txtProcessCode.Enabled = false;
            txtProcessName.Enabled = false;
            txtReader1IP.Enabled = false;
            txtReader2IP.Enabled = false;

            txtDeviceIP.Clear();
            txtLineCode.Clear();
            txtProcessCode.Clear();
            txtProcessName.Clear();
            txtReader1IP.Clear();
            txtReader2IP.Clear();

            btnRefresh.Visible = true;
            btnSave.Visible = true;
            btnAdd.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = true;

            btnRefresh.Enabled = true;
            btnSave.Enabled = false;
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;

            RefreshData();

        }

        private void RefreshData()
        {
            string sql = "SELECT * FROM TXD_AutoScan_Config ORDER BY TAC_ProcessCode, TAC_LineCode";
            this.dtAutoScan = conn.ExecuteDataTable(sql, "Base");
            this.dgwAutoScan.DataSource = this.dtAutoScan;
            this.Refresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dgwAutoScan_Click(object sender, EventArgs e)
        {
            if (dgwAutoScan.SelectedRows.Count == 0)
            {
                return;
            }
            else if (dgwAutoScan.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgwAutoScan.SelectedRows[0];
                txtLineCode.Text = row.Cells["LineCode"].Value.ToString();
                txtProcessCode.Text = row.Cells["ProcessCode"].Value.ToString();
                txtProcessName.Text = row.Cells["ProcessName"].Value.ToString();
                txtDeviceIP.Text = row.Cells["DeviceIP"].Value.ToString();
                txtReader1IP.Text = row.Cells["Reader1IP"].Value.ToString();
                txtReader2IP.Text = row.Cells["Reader2IP"].Value.ToString();
            }
            else
            {
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgwAutoScan.Enabled = false;

            txtDeviceIP.Enabled = true;
            txtLineCode.Enabled = true;
            txtProcessCode.Enabled = true;
            txtProcessName.Enabled = true;
            txtReader1IP.Enabled = true;
            txtReader2IP.Enabled = true;

            txtDeviceIP.Clear();
            txtLineCode.Clear();
            txtProcessCode.Clear();
            txtProcessName.Clear();
            txtReader1IP.Clear();
            txtReader2IP.Clear();

            btnRefresh.Enabled = false;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;

            txtDeviceIP.Focus();

            btnSave.Tag = "Add";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgwAutoScan.SelectedRows.Count != 1) return;

            dgwAutoScan.Enabled = false;

            txtDeviceIP.Enabled = true;
            txtLineCode.Enabled = true;
            txtProcessCode.Enabled = true;
            txtProcessName.Enabled = true;
            txtReader1IP.Enabled = true;
            txtReader2IP.Enabled = true;

            btnRefresh.Enabled = false;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;

            txtDeviceIP.Focus();

            btnSave.Tag = "Edit";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Tag == null) return;

            if (txtDeviceIP.Text.Trim() == "")
            {
                MessageBox.Show(this, "工控机IP不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtLineCode.Text.Trim() == "")
            {
                MessageBox.Show(this, "产线编码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtProcessCode.Text.Trim() == "")
            {
                MessageBox.Show(this, "工序编码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtProcessName.Text.Trim() == "")
            {
                MessageBox.Show(this, "工序名称不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtReader1IP.Text.Trim() == "")
            {
                MessageBox.Show(this, "1#扫码枪IP不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtDeviceIP.Text.Trim() == txtReader1IP.Text.Trim() || txtDeviceIP.Text.Trim() == txtReader2IP.Text.Trim() || txtReader1IP.Text.Trim() == txtReader2IP.Text.Trim())
            {
                MessageBox.Show(this, "IP地址不允许重复！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            string type = btnSave.Tag.ToString();
            if (type == "Add")
            {
                string sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtDeviceIP.Text.Trim());
                DataTable dt = conn.ExecuteDataTable(sql, "Base");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(this, "IP(" + txtDeviceIP.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtReader1IP.Text.Trim());
                dt = conn.ExecuteDataTable(sql, "Base");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(this, "IP(" + txtReader1IP.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtReader2IP.Text.Trim() != "")
                {
                    sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtReader2IP.Text.Trim());
                    dt = conn.ExecuteDataTable(sql, "Base");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "IP(" + txtReader2IP.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                sql = string.Format(@"INSERT INTO TXD_AutoScan_Config 
                                        (TAC_LineCode,TAC_ProcessCode,TAC_ProcessName,TAC_DeviceIP,TAC_Reader1IP,TAC_Reader2IP) 
                                        VALUES 
                                        ('{0}','{1}','{2}','{3}','{4}','{5}')", txtLineCode.Text.Trim(), txtProcessCode.Text.Trim(), txtProcessName.Text.Trim(), txtDeviceIP.Text.Trim(), txtReader1IP.Text.Trim(), txtReader2IP.Text.Trim());
                int n = conn.ExecuteAction(sql, "Base");
                if (n == 1)
                {
                    RefreshData();
                    dgwAutoScan.Enabled = true;

                    txtDeviceIP.Enabled = false;
                    txtLineCode.Enabled = false;
                    txtProcessCode.Enabled = false;
                    txtProcessName.Enabled = false;
                    txtReader1IP.Enabled = false;
                    txtReader2IP.Enabled = false;

                    txtDeviceIP.Clear();
                    txtLineCode.Clear();
                    txtProcessCode.Clear();
                    txtProcessName.Clear();
                    txtReader1IP.Clear();
                    txtReader2IP.Clear();

                    btnRefresh.Enabled = true;
                    btnSave.Enabled = false;
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;

                    MessageBox.Show(this, "添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                else
                {
                    MessageBox.Show(this, "添加失败，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (type == "Edit")
            {
                DataGridViewRow row = dgwAutoScan.SelectedRows[0];

                if (txtDeviceIP.Text.Trim() != row.Cells["DeviceIP"].Value.ToString())
                {
                    string sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtDeviceIP.Text.Trim());
                    DataTable dt = conn.ExecuteDataTable(sql, "Base");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "IP(" + txtDeviceIP.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (txtReader1IP.Text.Trim() != row.Cells["Reader1IP"].Value.ToString())
                {
                    string sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtReader1IP.Text.Trim());
                    DataTable dt = conn.ExecuteDataTable(sql, "Base");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "IP(" + txtReader1IP.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (txtReader2IP.Text.Trim() != "" && txtReader2IP.Text.Trim() != row.Cells["Reader2IP"].Value.ToString())
                {
                    string sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtReader2IP.Text);
                    DataTable dt = conn.ExecuteDataTable(sql, "Base");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "IP(" + txtReader2IP.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sqlstr = string.Format(@"UPDATE TXD_AutoScan_Config SET
                                                TAC_LineCode = '{0}',
                                                TAC_ProcessCode = '{1}',
                                                TAC_ProcessName = '{2}',
                                                TAC_DeviceIP = '{3}',
                                                TAC_Reader1IP = '{4}',
                                                TAC_Reader2IP = '{5}'
                                                WHERE TAC_Tid = {6}",
                    txtLineCode.Text.Trim(), 
                    txtProcessCode.Text.Trim(), 
                    txtProcessName.Text.Trim(), 
                    txtDeviceIP.Text.Trim(), 
                    txtReader1IP.Text.Trim(), 
                    txtReader2IP.Text.Trim(),
                    row.Cells["Tid"].Value);
                int n = conn.ExecuteAction(sqlstr, "Base");
                if (n == 1)
                {
                    RefreshData();
                    dgwAutoScan.Enabled = true;

                    txtDeviceIP.Enabled = false;
                    txtLineCode.Enabled = false;
                    txtProcessCode.Enabled = false;
                    txtProcessName.Enabled = false;
                    txtReader1IP.Enabled = false;
                    txtReader2IP.Enabled = false;

                    txtDeviceIP.Clear();
                    txtLineCode.Clear();
                    txtProcessCode.Clear();
                    txtProcessName.Clear();
                    txtReader1IP.Clear();
                    txtReader2IP.Clear();

                    btnRefresh.Enabled = true;
                    btnSave.Enabled = false;
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;

                    MessageBox.Show(this, "修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                else
                {
                    MessageBox.Show(this, "修改失败，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgwAutoScan.SelectedRows.Count == 0) return;
            if (MessageBox.Show(this, "已选择" + dgwAutoScan.SelectedRows.Count + "行数据，\r\n是否确定删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;

            List<string> lstId = new List<string>();
            foreach (DataGridViewRow row in dgwAutoScan.SelectedRows)
            {
                lstId.Add(row.Cells["Tid"].Value.ToString());
            }
            string id = string.Join(",", lstId);
            string sql = string.Format(@"DELETE FROM TXD_AutoScan_Config WHERE TAC_Tid IN({0})", id);
            int n = conn.ExecuteAction(sql, "Base");
            if (n > 0)
            {
                RefreshData();
                dgwAutoScan.Enabled = true;

                txtDeviceIP.Enabled = false;
                txtLineCode.Enabled = false;
                txtProcessCode.Enabled = false;
                txtProcessName.Enabled = false;
                txtReader1IP.Enabled = false;
                txtReader2IP.Enabled = false;

                txtDeviceIP.Clear();
                txtLineCode.Clear();
                txtProcessCode.Clear();
                txtProcessName.Clear();
                txtReader1IP.Clear();
                txtReader2IP.Clear();

                btnRefresh.Enabled = true;
                btnSave.Enabled = false;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;

                MessageBox.Show(this, "删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            else
            {
                MessageBox.Show(this, "删除失败，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dgwAutoScan_DoubleClick(object sender, EventArgs e)
        {
            btnEdit.PerformClick();
        }


    }
}
