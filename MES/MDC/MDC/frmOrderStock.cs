using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Utils.Common;

namespace MDC
{
    public partial class frmOrderStock : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 原料库存表
        /// </summary>
        private DataTable dtStock;
        /// <summary>
        /// 工单需求表
        /// </summary>
        private DataTable dtOrder;
        /// <summary>
        /// 数据行数
        /// </summary>
        private int rowCount;
        /// <summary>
        /// 需要隐藏的物料编码列表
        /// </summary>
        private List<string> lstDelCode = new List<string>();
 
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
        //        return cp;
        //    }
        //}
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}

        public frmOrderStock()
        {
            InitializeComponent();
        }

        private void frmOrderStock_Load(object sender, EventArgs e)
        {
   
            GetData();
            //lblTime.Text = string.Format("更新时间：{0}", DateTime.Now.ToString("HH:mm:ss"));

            cmbPage.Items.Clear();
            cmbPage.Items.Add(1);
            cmbMaterial.SelectedIndex = 0;
            cmbState.SelectedIndex = 0;
            cmbPage.SelectedIndex = 0;
            cmbUnit.SelectedIndex = 2;
            // 显示首页
            ShowData("全部", "全部", 1, 30);

            tmrRefresh.Start();
        }

        /// <summary>
        /// 换页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));
        }

        /// <summary>
        /// 切换每页排数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));
        }

        /// <summary>
        /// 从数据库获取数据
        /// </summary>
        private void GetData()
        {
            string sql = "EXEC TPL_Material_Order_Pro";
            DataSet ds = conn.ExecuteDataSet(sql, "Base");

            lblTime.Text = string.Format("更新时间：{0}", DateTime.Now.ToString("HH:mm:ss"));

            // 原料库存表
            this.dtStock = ds.Tables[0];
            // 工单需求表
            this.dtOrder = ds.Tables[1];
            //数据行数
            this.rowCount = this.dtStock.Rows.Count;

            //初始化物料选择下拉框
            DataView dvStock = new DataView(this.dtStock);
            dvStock.Sort = "MaterialName ASC";
            DataTable dtMaterial = dvStock.ToTable(true, new string[] { "MaterialName" });
            List<string> lstMaterial = dtMaterial.AsEnumerable().Select(d => d.Field<string>("MaterialName")).ToList();
            lstMaterial.Insert(0, "全部");
            cmbMaterial.DataSource = lstMaterial;
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        private void ShowData(string Material, string State, int Page, int Unit)
        {
            try
            {
                //this.lblUnlock.Text = string.Format("解锁|{0}", this.dtNum.Rows[0]["UnlockNum"].ToString());
                //this.lbl60Day.Text = string.Format("超60天|{0}", this.dtNum.Rows[0]["Store60DayNum"].ToString());
                //this.lblExpired.Text = string.Format("已过期|{0}", this.dtNum.Rows[0]["ExpiredNum"].ToString());

                DataView dvStock = new DataView(this.dtStock);
                //设置筛选条件
                string filter = "";

                string delCode = "";
                if (lstDelCode.Count > 0)
                {
                    delCode = string.Join("','", lstDelCode);
                    filter = string.Format("MaterialCode NOT IN ('{0}')", delCode);
                }

                //物料名称
                if(Material != "全部")
                {
                    if (filter != "")
                    {
                        filter += string.Format(" AND MaterialName = '{0}'", Material); 
                    }
                    else
                    {
                        filter = string.Format("MaterialName = '{0}'", Material); 
                    }
                }

                //库存状态
                if (State != "全部")
                {
                    if (filter != "")
                    {
                        filter += string.Format(" AND StockStatus = '{0}'", State);
                    }
                    else
                    {
                        filter = string.Format("StockStatus = '{0}'", State);
                    }
                }
                if (filter != "")
                {
                    dvStock.RowFilter = filter;
                }

                //初始化页码选择下拉框
                cmbPage.Items.Clear();
                double max = Math.Ceiling((double)((double)dvStock.Count / (double)Unit));
                for (int i = 1; i <= max; i++)
                {
                    cmbPage.Items.Add(i);
                }
                if (cmbPage.Items.Count == 0)
                {
                    cmbPage.Items.Add(1);
                }
                if (Page > max)
                {
                    Page = 1;
                    cmbPage.SelectedIndex = 0;
                }
                else
                {
                    cmbPage.SelectedIndex = cmbPage.Items.IndexOf(Page);
                }

                //选取指定页行数
                DataTable dt = this.dtStock.Clone(); //克隆DataTable 的结构，包括所有DataTable 架构和约束。
                for (int i = (Page - 1) * Unit; i < dvStock.Count; i++)
                {
                    if (i >= Page * Unit)
                    {
                        break;
                    }
                    dt.ImportRow(dvStock[i].Row); //取前TopValue行，其他的不添加至DataTable
                }

                //显示内容
                this.dgvData.DataSource = dt;
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据加载失败.", ex);
            }

        }


        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex >= dgvData.Rows.Count)
            {
                return;
            }

            string status = dgvData.Rows[e.RowIndex].Cells["colStockStatus"].Value.ToString().Trim();
            if (status == "1.库存不足")
            {
                // 设置单元格的背景色
                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                // 设置单元格的前景色
                dgvData.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(10, 30, 70);
            }
            else if (status == "2.低于下限")
            {
                // 设置单元格的背景色
                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                // 设置单元格的前景色
                dgvData.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(10, 30, 70);
            }
        }

        private void cmbMaterial_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbPage.SelectedIndex = 0;
            ShowData(cmbMaterial.Text, cmbState.Text, 1, int.Parse(cmbUnit.Text));
        }

        private void cmbState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbPage.SelectedIndex = 0;
            ShowData(cmbMaterial.Text, cmbState.Text, 1, int.Parse(cmbUnit.Text));
        }

        private void mnuDel_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvData.SelectedRows)
            {
                lstDelCode.Add(row.Cells["colMaterialCode"].Value.ToString());
            }
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));
        }

        private void dgvData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            string materialCode = dgvData.Rows[e.RowIndex].Cells["colMaterialCode"].Value.ToString();
            if (!string.IsNullOrEmpty(materialCode))
            {
                frmOrderStockSub sub = new frmOrderStockSub(this.dtOrder, materialCode);
                sub.ShowDialog(this);
            }
        }

        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || dgvData.SelectedRows.Count == 0) return;
            string materialCode = dgvData.SelectedRows[0].Cells["colMaterialCode"].Value.ToString();
            if (!string.IsNullOrEmpty(materialCode))
            {
                frmOrderStockSub sub = new frmOrderStockSub(this.dtOrder, materialCode);
                sub.ShowDialog(this);
            }
 
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            cmbPage.SelectedIndex = 0;
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (cmbPage.SelectedIndex > 0)
            {
                cmbPage.SelectedIndex -= 1;
            }
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (cmbPage.SelectedIndex < cmbPage.Items.Count - 1)
            {
                cmbPage.SelectedIndex += 1;
            }
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            cmbPage.SelectedIndex = cmbPage.Items.Count - 1;
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (cmbPage.SelectedIndex == cmbPage.Items.Count - 1)
            {
                cmbPage.SelectedIndex = 0;
                int idx = cmbMaterial.SelectedIndex;
                GetData();
                if (idx >= cmbMaterial.Items.Count)
                {
                    idx = 0;
                }
                cmbMaterial.SelectedIndex = idx;
            }
            else
            {
                cmbPage.SelectedIndex += 1;
            }
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int idx = cmbMaterial.SelectedIndex;
            GetData();
            if (idx >= cmbMaterial.Items.Count)
            {
                idx = 0;
            }
            cmbMaterial.SelectedIndex = idx;
            ShowData(cmbMaterial.Text, cmbState.Text, int.Parse(cmbPage.Text), int.Parse(cmbUnit.Text));
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAuto.Checked)
            {
                tmrRefresh.Start(); 
            }
            else
            {
                tmrRefresh.Stop();
            }
        }

    }
}
