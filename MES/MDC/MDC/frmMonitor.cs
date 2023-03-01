using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Net;
using Utils;
using Utils.Common;
using Utils.HBaseClass;
using Utils.Model;
using Newtonsoft.Json.Linq;


namespace MDC
{
    public partial class frmMonitor : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 产线及设备总表
        /// </summary>
        private DataTable dtLineDevice;
        /// <summary>
        /// 产线列表
        /// </summary>
        private DataTable dtLine;
        /// <summary>
        /// 指定产线对应的工序列表
        /// </summary>
        private DataTable dtProcess;
        ///// <summary>
        ///// 指定产线工序对应的设备列表
        ///// </summary>
        //private DataTable dtDevice;

        private List<dynamic> lstProcessInfo;

        private List<dynamic> lstExceptionInfo;

        public frmMonitor()
        {
            InitializeComponent();
        }

        private void frmMonitor_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;   //实例化Graphics 对象g
            //Color FColor = Color.FromArgb(0, 20, 50); //颜色1
            //Color TColor = Color.FromArgb(20, 60, 120);  //颜色2 
            //Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical);  //实例化刷子，第一个参数指示上色区域，第二个和第三个参数分别渐变颜色的开始和结束，第四个参数表示颜色的方向。
            //g.FillRectangle(b, this.ClientRectangle);  //进行上色
        }


        private void frmMonitor_Load(object sender, EventArgs e)
        {
            // 产线及设备总表
            this.dtLineDevice = GetProductionLineDevice();
            // 产线列表
            this.dtLine = this.dtLineDevice.DefaultView.ToTable(true, new string[] { "SPL_JobCode", "SPL_Name" });
            // 初始化产线选择下拉框
            DataView dvLine = new DataView(this.dtLine);
            dvLine.Sort = "SPL_JobCode ASC";
            cmbSPLName.DisplayMember = "SPL_Name";
            cmbSPLName.ValueMember = "SPL_JobCode";
            cmbSPLName.DataSource = dvLine;
            cmbSPLName.SelectedIndex = -1;

            Dictionary<string, DateTime> lstTime = new Dictionary<string, DateTime>();
            // 初始化时间选择下拉框
            DateTime dtStart = new DateTime(2019, 1, 1, 7, 30, 0);
            DateTime dtStop = dtStart.AddDays(1);
            DateTime dt = dtStart;
            while (dt < dtStop)
            {
                string txtTime = string.Format("{0} - {1}", dt.ToString("HH:mm"), (dt.AddMinutes(30)).ToString("HH:mm"));
                lstTime.Add(txtTime, dt);
                dt = dt.AddMinutes(30);
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = lstTime;
            cmbTime.DataSource = bs;
            cmbTime.DisplayMember = "Key";
            cmbTime.ValueMember = "Value";

            this.dgvProcessInfo.Columns.AddRange(new DataGridViewColumn[]{
                    new DataGridViewTextBoxColumn(){ Name = "PrfinishesCode", HeaderText = "成品料号",DataPropertyName = "finishesCode"},
                    new DataGridViewTextBoxColumn(){ Name = "PrfinishesModel", HeaderText = "成品型号",DataPropertyName = "finishesModel"},
                    new DataGridViewTextBoxColumn(){ Name = "PrclientProductNo", HeaderText = "客户料号",DataPropertyName = "clientProductNo"},
                    new DataGridViewTextBoxColumn(){ Name = "PrproductionOrder", HeaderText = "生产工单",DataPropertyName = "productionOrder"},
                    new DataGridViewTextBoxColumn(){ Name = "PrdeviceName", HeaderText = "机台名称",DataPropertyName = "deviceName"},
                    new DataGridViewTextBoxColumn(){ Name = "PrdeviceIp", HeaderText = "机台IP",DataPropertyName = "deviceIp"},
                    new DataGridViewTextBoxColumn(){ Name = "PrglassCode", HeaderText = "玻璃编码",DataPropertyName = "glassCode"},
                    new DataGridViewTextBoxColumn(){ Name = "PrInTime", HeaderText = "采集时间",DataPropertyName = "InTime"},
                    new DataGridViewTextBoxColumn(){ Name = "PrbfFlag", HeaderText = "是否补扫",DataPropertyName = "bfFlag"},
                    new DataGridViewTextBoxColumn(){ Name = "PrscanOP", HeaderText = "扫描人员",DataPropertyName = "scanOP"},
                    new DataGridViewTextBoxColumn(){ Name = "PrtrackNumber", HeaderText = "扫描码",DataPropertyName = "trackNumber"},
                    new DataGridViewTextBoxColumn(){ Name = "ProppositeNumber", HeaderText = "对应码",DataPropertyName = "oppositeNumber"},
                    new DataGridViewTextBoxColumn(){ Name = "PrexceptionState", HeaderText = "是否不良",DataPropertyName = "exceptionState"},
                    new DataGridViewTextBoxColumn(){ Name = "PrexceptionContent", HeaderText = "不良描述",DataPropertyName = "exceptionContent"},
                    new DataGridViewTextBoxColumn(){ Name = "PrlotNumber", HeaderText = "生产批号",DataPropertyName = "lotNumber"},
                    new DataGridViewTextBoxColumn(){ Name = "PrrepeatStatus", HeaderText = "重工次数",DataPropertyName = "repeatStatus"},
                    new DataGridViewTextBoxColumn(){ Name = "PrreworkStatus", HeaderText = "是否返检",DataPropertyName = "reworkStatus"},
                }
            );
            this.dgvProcessInfo.AutoGenerateColumns = false;

            this.dgvExceptionInfo.Columns.AddRange(new DataGridViewColumn[]{
                    new DataGridViewTextBoxColumn(){ Name = "ExfinishesCode", HeaderText = "成品料号",DataPropertyName = "finishesCode"},
                    new DataGridViewTextBoxColumn(){ Name = "ExfinishesModel", HeaderText = "成品型号",DataPropertyName = "finishesModel"},
                    new DataGridViewTextBoxColumn(){ Name = "ExproductionOrder", HeaderText = "生产工单",DataPropertyName = "productionOrder"},
                    new DataGridViewTextBoxColumn(){ Name = "ExglassCode", HeaderText = "玻璃编码",DataPropertyName = "glassCode"},
                    new DataGridViewTextBoxColumn(){ Name = "ExexceptionState", HeaderText = "当前状态",DataPropertyName = "exceptionState"},
                    new DataGridViewTextBoxColumn(){ Name = "ExlotNumber", HeaderText = "不良批号",DataPropertyName = "lotNumber"},
                    new DataGridViewTextBoxColumn(){ Name = "ExexceptionContent", HeaderText = "申报不良",DataPropertyName = "exceptionContent"},
                    new DataGridViewTextBoxColumn(){ Name = "ExscanNumber", HeaderText = "申报人员",DataPropertyName = "scanNumber"},
                    new DataGridViewTextBoxColumn(){ Name = "ExscanTime", HeaderText = "申报时间",DataPropertyName = "scanTime"},
                    new DataGridViewTextBoxColumn(){ Name = "ExdeviceIp", HeaderText = "申报IP",DataPropertyName = "deviceIp"},
                    new DataGridViewTextBoxColumn(){ Name = "ExjudgeContent", HeaderText = "复判不良",DataPropertyName = "judgeContent"},
                    new DataGridViewTextBoxColumn(){ Name = "ExjudgeNumber", HeaderText = "复判人员",DataPropertyName = "judgeNumber"},
                    new DataGridViewTextBoxColumn(){ Name = "ExjudgeTime", HeaderText = "复判时间",DataPropertyName = "judgeTime"},
                    new DataGridViewTextBoxColumn(){ Name = "ExjudgeIp", HeaderText = "复判IP",DataPropertyName = "judgeIp"},
                    new DataGridViewTextBoxColumn(){ Name = "ExreWorkContent", HeaderText = "重工不良",DataPropertyName = "reWorkContent"},
                    new DataGridViewTextBoxColumn(){ Name = "ExreWorkNumber", HeaderText = "重工人员",DataPropertyName = "reWorkNumber"},
                    new DataGridViewTextBoxColumn(){ Name = "ExreWorkTime", HeaderText = "重工时间",DataPropertyName = "reWorkTime"},
                    new DataGridViewTextBoxColumn(){ Name = "ExreWorkIp", HeaderText = "重工IP",DataPropertyName = "reWorkIp"},
                    new DataGridViewTextBoxColumn(){ Name = "ExreWorkProcess", HeaderText = "解绑工序",DataPropertyName = "reWorkProcess"},
                    new DataGridViewTextBoxColumn(){ Name = "ExrepeatStatus", HeaderText = "重工次数",DataPropertyName = "repeatStatus"},
                    new DataGridViewTextBoxColumn(){ Name = "ExreworkStatus", HeaderText = "返检申报",DataPropertyName = "reworkStatus"}
                }
            );
            this.dgvExceptionInfo.AutoGenerateColumns = false;

            cmbSPLName.Focus();
        }

        /// <summary>
        /// 获取产线及设备列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetProductionLineDevice()
        {
            string sql = "SELECT * FROM TPL_ProductionLine_Device_View";
            DataTable dt = conn.ExecuteDataTable(sql, "Base");
            return dt;
        }

        /// <summary>
        /// 选择产线后，初始化工序选择下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSPLName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSPLName.SelectedIndex == -1) return;
            string lineCode = cmbSPLName.SelectedValue.ToString();
            DataView dvLineDevice = new DataView(this.dtLineDevice);
            dvLineDevice.RowFilter = string.Format("SPL_JobCode = '{0}'", lineCode);
            this.dtProcess = dvLineDevice.ToTable(true, new string[] { "SGX_JobCode", "SGX_Name" });
            DataView dvProcess = new DataView(this.dtProcess);
            dvProcess.Sort = "SGX_JobCode ASC";
            cmbProcess.DisplayMember = "SGX_Name";
            cmbProcess.ValueMember = "SGX_JobCode";
            cmbProcess.DataSource = dvProcess;
            cmbProcess.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ////2020-01-02 修改LINE01产线编码
            //GetLCDCode();
            //return;

            //DataSet dtOrder = GetHBaseDataClass.Instance.SummaryScanByWip("12419", "SCDD20191025015");
            //DataTable dt = GetHBaseDataClass.Instance.GetExceptionInfo("P38BTD508HHZ");

            if (cmbSPLName.SelectedIndex == -1 || cmbProcess.SelectedIndex == -1 || cmbTime.SelectedIndex == -1) return;
            string lineCode = cmbSPLName.SelectedValue.ToString();
            string processCode = cmbProcess.SelectedValue.ToString();

            
            int part = 1;
            if (cmbTime.SelectedIndex % 2 == 0)
            {
                part = 2;
            }
            else
            {
                part = 1;
            }
            DateTime time = dtpDate.Value.Date.AddHours(((DateTime)cmbTime.SelectedValue).Hour);
            //// 获取产线、工序对应的所有设备
            //DataView dvDevice = new DataView(this.dtLineDevice);
            //dvDevice.RowFilter = string.Format("SPL_JobCode = '{0}' AND SGX_JobCode = '{1}'", lineCode, processCode);
            //this.dtDevice = dvDevice.ToTable(true, new string[] { "STW_JobCode" });

            this.lstProcessInfo = GetHBaseDataClass.Instance.GetGlassByTime(lineCode, processCode, time, part);
            //this.lstExceptionInfo = GetHBaseDataClass.Instance.GetExceptionByTime(lineCode, processCode, time, part);
            this.lstExceptionInfo = GetHBaseDataClass.Instance.GetJudgeExceptionByTime(lineCode, processCode, time, part);
            
            ////绑定数据
            //dgvProcessInfo.DataSource = this.lstProcessInfo;
            //dgvExceptionInfo.DataSource = this.lstExceptionInfo;
            //foreach (DataRow row in this.dtDevice.Rows)
            //{
            //    string deviceCode = row["STW_JobCode"].ToString();
            //    this.lstProcessInfo.AddRange(GetHBaseDataClass.Instance.GetGlassByTime(deviceCode, time, part));
            //    this.lstExceptionInfo.AddRange(GetHBaseDataClass.Instance.GetExceptionByTime(deviceCode, time, part));
            //}

            //可以实现排序的类
            SortableBindingList<dynamic> bindingListProcess = new SortableBindingList<dynamic>(this.lstProcessInfo);
            SortableBindingList<dynamic> bindingListException = new SortableBindingList<dynamic>(this.lstExceptionInfo);
            //绑定数据
            dgvProcessInfo.DataSource = bindingListProcess;
            dgvExceptionInfo.DataSource = bindingListException;
        }


        private void GetLCDCode()
        {
            List<string> lstGlass = new List<string>();
            if (cmbSPLName.SelectedIndex == -1 || cmbProcess.SelectedIndex == -1) return;
            string lineCode = cmbSPLName.SelectedValue.ToString();
            string processCode = cmbProcess.SelectedValue.ToString();
            DateTime date = dtpDate.Value.Date;
            for (int h = 0; h < 24; h++)
            {
                DateTime time = date.AddHours(h);
                int part = 1;
                lstGlass.AddRange(GetHBaseDataClass.Instance.GetGlassCodeByTime(lineCode, processCode, time, part));
            }
            

        }

        /// <summary>
        /// 过站明细表中双击不良玻璃，定位至不良明细表对应玻璃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProcessInfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.RowIndex < 0) return;
            DataGridViewRow row = dgvProcessInfo.Rows[e.RowIndex];
            string glassCode = row.Cells["PrglassCode"].Value.ToString();
            dgvExceptionInfo.ClearSelection();
            int r = -1, c = -1;
            foreach (DataGridViewRow ex in dgvExceptionInfo.Rows)
            {
                if (ex.Cells["ExglassCode"].Value.ToString() == glassCode)
                {
                    r = ex.Cells["ExglassCode"].RowIndex;
                    c = ex.Cells["ExglassCode"].ColumnIndex;
                    break;
                }
            }
            if (r != -1 && c != -1)
            {
                tabControl1.SelectedIndex = 1;
                dgvExceptionInfo.CurrentCell = dgvExceptionInfo[c, r];
                dgvExceptionInfo.Focus();
            }
        }

        /// <summary>
        /// 不良明细表中双击不良玻璃，定位至过站明细表对应玻璃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExceptionInfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.RowIndex < 0) return;
            DataGridViewRow row = dgvExceptionInfo.Rows[e.RowIndex];
            string glassCode = row.Cells["ExglassCode"].Value.ToString();
            dgvProcessInfo.ClearSelection();
            int r = -1, c = -1;
            foreach (DataGridViewRow ex in dgvProcessInfo.Rows)
            {
                if (ex.Cells["PrglassCode"].Value.ToString() == glassCode)
                {
                    r = ex.Cells["PrglassCode"].RowIndex;
                    c = ex.Cells["PrglassCode"].ColumnIndex;
                    break;
                }
            }
            if (r != -1 && c != -1)
            {
                tabControl1.SelectedIndex = 0;
                dgvProcessInfo.CurrentCell = dgvProcessInfo[c, r];
                dgvProcessInfo.Focus();
            }
        }

    }
}
