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
    public partial class frmOrderStockSub : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 工单需求表
        /// </summary>
        private DataTable dtOrder;
        /// <summary>
        /// 物料编码
        /// </summary>
        private string materialCode;
 
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

        public frmOrderStockSub()
        {
            InitializeComponent();
        }

        public frmOrderStockSub(DataTable dtOrder, string materialCode)
        {
            InitializeComponent();
            this.dtOrder = dtOrder;
            this.materialCode = materialCode;
        }

        private void frmOrderStockSub_Load(object sender, EventArgs e)
        {
            cmbType.SelectedIndex = 0;
            cmbSeachText.Items.Add(this.materialCode);
            cmbSeachText.SelectedIndex = 0;
            // 显示首页
            ShowData(0, this.materialCode);
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        private void ShowData(int type, string searchText)
        {
            try
            {
                DataView dvStock = new DataView(this.dtOrder);

                if (searchText != "")
                {
                    //设置筛选条件
                    string filter = "";

                    if (type == 0)
                    {
                        filter = string.Format("SPOS_SMCode = '{0}'", searchText);
                    }
                    else
                    {
                        filter = string.Format("SPOM_JobCode = '{0}'", searchText);
                    }

                    dvStock.RowFilter = filter;
                }

                //显示内容
                this.dgvData.DataSource = dvStock;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!cmbSeachText.Items.Contains(cmbSeachText.Text))
            {
                cmbSeachText.Items.Insert(0, cmbSeachText.Text);
            }
            ShowData(cmbType.SelectedIndex, cmbSeachText.Text);
        }

    }
}
