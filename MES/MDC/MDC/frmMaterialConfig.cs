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
    public partial class frmMaterialConfig : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();

        private DataTable dtMaterial = new DataTable();

        dlgMaterialSelect dlg1;
        dlgMaterialSelect dlg2;

        public frmMaterialConfig()
        {
            InitializeComponent();
            dgwMaterial.AutoGenerateColumns = false;
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

        private void tXD_Material_ConfigBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "是否保存数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;
            this.Validate();
            this.tXD_Material_ConfigBindingSource.EndEdit();
            MessageBox.Show("保存成功！");
        }

        private void frmMaterialConfig_Load(object sender, EventArgs e)
        {
            dgwMaterial.Enabled = true;

            txtCode2.Enabled = false;
            txtCode1.Enabled = false;
            txtName1.Enabled = false;
            txtSpec1.Enabled = false;
            txtName2.Enabled = false;
            txtSpec2.Enabled = false;

            txtCode2.Clear();
            txtCode1.Clear();
            txtName1.Clear();
            txtSpec1.Clear();
            txtName2.Clear();
            txtSpec2.Clear();

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
            string sql = @"SELECT SMB_Tid
                            ,Store_Material.SM_Name AS SMB_Name
                            ,Store_Material.SM_Spec AS SMB_Spec
                            ,Store_Material1.SM_Name AS SMB_Name_Bonded
                            ,Store_Material1.SM_Spec AS SMB_Spec_Bonded
                            ,SMB_SMTid
                            ,Store_Material.SM_nbCode AS SMB_SMCode
                            ,SMB_SMTid_Bonded
                            ,Store_Material1.SM_nbCode AS SMB_SMCode_Bonded
                            ,Base_Op_Operator.BOO_LogName
                            ,SMB_AddDate
                            FROM Store_Material_Bonded
                            JOIN Store_Material ON Store_Material.SM_Tid = Store_Material_Bonded.SMB_SMTid
                            JOIN Store_Material AS Store_Material1 ON Store_Material1.SM_Tid = Store_Material_Bonded.SMB_SMTid_Bonded
                            JOIN Base_Op_Operator ON Base_Op_Operator.BOO_Pid = Store_Material_Bonded.SMB_Pid";
            this.dtMaterial = conn.ExecuteDataTable(sql, "Base");
            this.dgwMaterial.DataSource = this.dtMaterial;
            this.Refresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dgwMaterial_Click(object sender, EventArgs e)
        {
            if (dgwMaterial.SelectedRows.Count == 0)
            {
                return;
            }
            else if (dgwMaterial.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgwMaterial.SelectedRows[0];
                txtCode1.Text = row.Cells["SMB_SMCode"].Value.ToString();
                txtName1.Text = row.Cells["SMB_Name"].Value.ToString();
                txtSpec1.Text = row.Cells["SMB_Spec"].Value.ToString();
                txtCode2.Text = row.Cells["SMB_SMCode_Bonded"].Value.ToString();
                txtName2.Text = row.Cells["SMB_Name_Bonded"].Value.ToString();
                txtSpec2.Text = row.Cells["SMB_Spec_Bonded"].Value.ToString();
            }
            else
            {
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgwMaterial.Enabled = false;

            txtCode2.Enabled = true;
            txtCode1.Enabled = true;
            txtName1.Enabled = true;
            txtSpec1.Enabled = true;
            txtName2.Enabled = true;
            txtSpec2.Enabled = true;

            txtCode2.Clear();
            txtCode1.Clear();
            txtName1.Clear();
            txtSpec1.Clear();
            txtName2.Clear();
            txtSpec2.Clear();

            btnRefresh.Enabled = false;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;

            txtCode2.Focus();

            btnSave.Tag = "Add";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgwMaterial.SelectedRows.Count != 1) return;

            dgwMaterial.Enabled = false;

            txtCode2.Enabled = true;
            txtCode1.Enabled = true;
            txtName1.Enabled = true;
            txtSpec1.Enabled = true;
            txtName2.Enabled = true;
            txtSpec2.Enabled = true;

            btnRefresh.Enabled = false;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;

            txtCode2.Focus();

            btnSave.Tag = "Edit";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Tag == null) return;

            if (txtCode1.Text.Trim() == "")
            {
                MessageBox.Show(this, "请选择非保税型号！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtCode2.Text.Trim() == "")
            {
                MessageBox.Show(this, "请选择保税型号！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtCode1.Text.Trim() == txtCode2.Text.Trim())
            {
                MessageBox.Show(this, "两个物料型号不能为同一编码！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtName1.Text.Trim() != txtName2.Text.Trim())
            {
                MessageBox.Show(this, "两个物料的名称不一致！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            if (txtSpec1.Text.Trim() != txtSpec2.Text.Trim())
            {
                MessageBox.Show(this, "两个物料的规格型号不一致！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            string type = btnSave.Tag.ToString();
            if (type == "Add")
            {
                string sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtCode2.Text.Trim());
                DataTable dt = conn.ExecuteDataTable(sql, "Base");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(this, "IP(" + txtCode2.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtName2.Text.Trim());
                dt = conn.ExecuteDataTable(sql, "Base");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(this, "IP(" + txtName2.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtSpec2.Text.Trim() != "")
                {
                    sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtSpec2.Text.Trim());
                    dt = conn.ExecuteDataTable(sql, "Base");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "IP(" + txtSpec2.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                sql = string.Format(@"INSERT INTO TXD_Material_Config 
                                        (TAC_LineCode,TAC_ProcessCode,TAC_ProcessName,TAC_DeviceIP,TAC_Reader1IP,TAC_Reader2IP) 
                                        VALUES 
                                        ('{0}','{1}','{2}','{3}','{4}','{5}')", txtCode1.Text.Trim(), txtName1.Text.Trim(), txtSpec1.Text.Trim(), txtCode2.Text.Trim(), txtName2.Text.Trim(), txtSpec2.Text.Trim());
                int n = conn.ExecuteAction(sql, "Base");
                if (n == 1)
                {
                    RefreshData();
                    dgwMaterial.Enabled = true;

                    txtCode2.Enabled = false;
                    txtCode1.Enabled = false;
                    txtName1.Enabled = false;
                    txtSpec1.Enabled = false;
                    txtName2.Enabled = false;
                    txtSpec2.Enabled = false;

                    txtCode2.Clear();
                    txtCode1.Clear();
                    txtName1.Clear();
                    txtSpec1.Clear();
                    txtName2.Clear();
                    txtSpec2.Clear();

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
                DataGridViewRow row = dgwMaterial.SelectedRows[0];

                if (txtCode2.Text.Trim() != row.Cells["DeviceIP"].Value.ToString())
                {
                    string sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtCode2.Text.Trim());
                    DataTable dt = conn.ExecuteDataTable(sql, "Base");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "IP(" + txtCode2.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (txtName2.Text.Trim() != row.Cells["Reader1IP"].Value.ToString())
                {
                    string sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtName2.Text.Trim());
                    DataTable dt = conn.ExecuteDataTable(sql, "Base");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "IP(" + txtName2.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (txtSpec2.Text.Trim() != "" && txtSpec2.Text.Trim() != row.Cells["Reader2IP"].Value.ToString())
                {
                    string sql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}' OR TAC_Reader1IP = '{0}' OR TAC_Reader2IP = '{0}'", txtSpec2.Text);
                    DataTable dt = conn.ExecuteDataTable(sql, "Base");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "IP(" + txtSpec2.Text + ")已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    txtCode1.Text.Trim(), 
                    txtName1.Text.Trim(), 
                    txtSpec1.Text.Trim(), 
                    txtCode2.Text.Trim(), 
                    txtName2.Text.Trim(), 
                    txtSpec2.Text.Trim(),
                    row.Cells["Tid"].Value);
                int n = conn.ExecuteAction(sqlstr, "Base");
                if (n == 1)
                {
                    RefreshData();
                    dgwMaterial.Enabled = true;

                    txtCode2.Enabled = false;
                    txtCode1.Enabled = false;
                    txtName1.Enabled = false;
                    txtSpec1.Enabled = false;
                    txtName2.Enabled = false;
                    txtSpec2.Enabled = false;

                    txtCode2.Clear();
                    txtCode1.Clear();
                    txtName1.Clear();
                    txtSpec1.Clear();
                    txtName2.Clear();
                    txtSpec2.Clear();

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
            if (dgwMaterial.SelectedRows.Count == 0) return;
            if (MessageBox.Show(this, "已选择" + dgwMaterial.SelectedRows.Count + "行数据，\r\n是否确定删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;

            List<string> lstId = new List<string>();
            foreach (DataGridViewRow row in dgwMaterial.SelectedRows)
            {
                lstId.Add(row.Cells["Tid"].Value.ToString());
            }
            string id = string.Join(",", lstId);
            string sql = string.Format(@"DELETE FROM TXD_AutoScan_Config WHERE TAC_Tid IN({0})", id);
            int n = conn.ExecuteAction(sql, "Base");
            if (n > 0)
            {
                RefreshData();
                dgwMaterial.Enabled = true;

                txtCode2.Enabled = false;
                txtCode1.Enabled = false;
                txtName1.Enabled = false;
                txtSpec1.Enabled = false;
                txtName2.Enabled = false;
                txtSpec2.Enabled = false;

                txtCode2.Clear();
                txtCode1.Clear();
                txtName1.Clear();
                txtSpec1.Clear();
                txtName2.Clear();
                txtSpec2.Clear();

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

        private void dgwMaterial_DoubleClick(object sender, EventArgs e)
        {
            btnEdit.PerformClick();
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            
            dlg1 = new dlgMaterialSelect();
            DialogResult rst = dlg1.ShowDialog(this);
            if (rst == System.Windows.Forms.DialogResult.OK)
            {
                lblTid1.Tag = dlg1.SM_Tid;
                txtCode1.Text = dlg1.SM_nbCode;
                txtName1.Text = dlg1.SM_Name;
                txtSpec1.Text = dlg1.SM_Spec;
            }
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            dlg2 = new dlgMaterialSelect();
            DialogResult rst = dlg2.ShowDialog(this);
            if (rst == System.Windows.Forms.DialogResult.OK)
            {
                lblTid2.Tag = dlg2.SM_Tid;
                txtCode2.Text = dlg2.SM_nbCode;
                txtName2.Text = dlg2.SM_Name;
                txtSpec2.Text = dlg2.SM_Spec;
            }
        }


    }
}
