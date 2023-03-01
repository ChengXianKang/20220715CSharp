using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using Utils;

namespace MDC
{
    public partial class frmDeliveryBoard : Form
    {
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;
        /// <summary>
        /// 看板数据表
        /// </summary>
        DataTable dtBoard;
        /// <summary>
        /// 投料记录明细表
        /// </summary>
        DataTable dtDailyData;
        /// <summary>
        /// 规格型号表
        /// </summary>
        DataTable dtModel;
        /// <summary>
        /// 自动刷新定时器
        /// </summary>
        private System.Windows.Forms.Timer tmrRefresh = new System.Windows.Forms.Timer();
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime timeStart;
        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime timeStop;
        /// <summary>
        /// 产线列表
        /// </summary>
        private DataTable dtLine;
        /// <summary>
        /// 车间名称
        /// </summary>
        private string shopName;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmDeliveryBoard()
        {
            InitializeComponent();
            dgvDetails.Columns["Tid"].Visible = false;
            dgvDetails.Columns["Model"].DisplayIndex = 0;
            dgvDetails.Columns["Code"].DisplayIndex = 1;
            dgvDetails.Columns["Order"].DisplayIndex = 2;
            dgvDetails.Columns["Line"].DisplayIndex = 3;
            dgvDetails.Columns["SN"].DisplayIndex = 4;
            dgvDetails.Columns["Count"].DisplayIndex = 5;
            dgvDetails.Columns["State"].DisplayIndex = 6;
            dgvDetails.Columns["DeliveryShop"].DisplayIndex = 7;
            dgvDetails.Columns["DeliveryLine"].DisplayIndex = 8;
            dgvDetails.Columns["DeliveryOP"].DisplayIndex = 9;
            dgvDetails.Columns["DeliveryIP"].DisplayIndex = 10;
            dgvDetails.Columns["DeliveryTime"].DisplayIndex = 11;
            dgvDetails.Columns["CheckShop"].DisplayIndex = 12;
            dgvDetails.Columns["CheckOP"].DisplayIndex = 13;
            dgvDetails.Columns["CheckIP"].DisplayIndex = 14;
            dgvDetails.Columns["CheckTime"].DisplayIndex = 15;
            dgvDetails.Columns["ReceiveOP"].DisplayIndex = 16;
            dgvDetails.Columns["ReceiveTime"].DisplayIndex = 17;
            dgvDetails.Columns["ReceiveShop"].DisplayIndex = 18;
            dgvDetails.Columns["ReceiveLine"].DisplayIndex = 19;
            dgvDetails.Columns["ReceiveOrder"].DisplayIndex = 20;
            dgvDetails.Columns["ReceiveIP"].DisplayIndex = 21;
            rdoDaily.Checked = true;
            rdoHistory.Checked = false;
            chtData.Visible = true;
            chtHistory.Visible = false;
            chtData.Dock = DockStyle.Fill;
            splitDaily.Panel1Collapsed = false;
            pnlDetails.Visible = false;
            dgvData.Visible = true;
            dgvData.Dock = DockStyle.Fill;

            dgvData.AutoGenerateColumns = false;
            dgvDetails.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDeliveryBoard_Load(object sender, EventArgs e)
        {
            // 当前程序版本
            this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = string.Format("Cell车间配料看板  {0}", this.versionName);

            // 初始化产线选择下拉框
            this.dtLine = BaseUI.GetProcedureLine().Table;
            DataTable dtshop = new DataView(this.dtLine).ToTable(true, new string[] { "SPL_ShopName" });
            cmbShopName.DisplayMember = "SPL_ShopName";
            cmbShopName.ValueMember = "SPL_ShopName";
            cmbShopName.DataSource = dtshop;
            cmbShopName.SelectedIndex = 0;
            
            GetClassTime();

            this.tmrRefresh.Interval = 30000;
            this.tmrRefresh.Tick += new EventHandler(tmrRefresh_Tick);
        }

        /// <summary>
        /// 窗体显示时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDeliveryBoard_Shown(object sender, EventArgs e)
        {
            try
            {
                // 异步刷新数据
                CallWithTimeout(RefreshData, 60000);

                // 开启自动刷新
                this.tmrRefresh.Start();
            }
            catch (TimeoutException tex)
            {
                LogHelper.Error("刷新数据超时", tex);
                this.failMsg = new FailMessage("刷新数据超时！\n" + tex.Message, 1000);
                this.failMsg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据刷新失败！", ex);
                this.failMsg = new FailMessage("数据刷新失败！\n" + ex.Message, 1000);
                this.failMsg.ShowDialog(this);
            }
        }

        /// <summary>
        /// 点击查看班次数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoDaily_Click(object sender, EventArgs e)
        {
            rdoDaily.Checked = true;
            rdoHistory.Checked = false;

            splitDaily.Panel1Collapsed = false;
            pnlDetails.Visible = false;
            dgvData.Visible = true;
            dgvData.Dock = DockStyle.Fill;

            chtData.Visible = true;
            chtHistory.Visible = false;
            chtData.Dock = DockStyle.Fill;

            lblTime.Visible = true;
            dtpClassDate.Visible = true;
            rdoClassA.Visible = true;
            rdoClassB.Visible = true;

            GetClassTime();
            btnRefresh.PerformClick();
        }
        /// <summary>
        /// 点击查看历史数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoHistory_Click(object sender, EventArgs e)
        {
            rdoDaily.Checked = false;
            rdoHistory.Checked = true;

            splitDaily.Panel1Collapsed = false;
            pnlDetails.Visible = false;
            dgvData.Visible = true;
            dgvData.Dock = DockStyle.Fill;

            chtData.Visible = false;
            chtHistory.Visible = true;
            chtHistory.Dock = DockStyle.Fill;

            lblTime.Visible = false;
            dtpClassDate.Visible = false;
            rdoClassA.Visible = false;
            rdoClassB.Visible = false;

            btnRefresh.PerformClick();
        }
        /// <summary>
        /// 选择班次日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpClassDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dtpClassDate.Value;
            DateTime time1 = date.Date.Add(new TimeSpan(7, 30, 0));
            DateTime time2 = date.Date.Add(new TimeSpan(19, 30, 0));
            DateTime time4 = date.Date.Add(new TimeSpan(1, 7, 30, 0));
            if (rdoClassA.Checked)//当天A班
            {
                this.timeStart = time1;
                this.timeStop = time2;
            }
            else//当天B班
            {
                this.timeStart = time2;
                this.timeStop = time4;
            }
        }
        /// <summary>
        /// 选择A班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoClassA_Click(object sender, EventArgs e)
        {
            DateTime date = dtpClassDate.Value;
            DateTime time1 = date.Date.Add(new TimeSpan(7, 30, 0));
            DateTime time2 = date.Date.Add(new TimeSpan(19, 30, 0));
            this.timeStart = time1;
            this.timeStop = time2;
        }
        /// <summary>
        /// 选择B班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoClassB_Click(object sender, EventArgs e)
        {
            DateTime date = dtpClassDate.Value;
            DateTime time2 = date.Date.Add(new TimeSpan(19, 30, 0));
            DateTime time4 = date.Date.Add(new TimeSpan(1, 7, 30, 0));
            //当天B班
            this.timeStart = time2;
            this.timeStop = time4;
        }

        /// <summary>
        /// 导出到文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = dtpClassDate.Value.ToString("yyyyMMdd") + (rdoClassA.Checked ? "升降机投料A班" : "升降机投料B班");
                ExcelHelper.ExportExcel(dgvDetails, filename);
                //DGVToExcel.ExportExcel(filename, dgvDetails, 1);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Excel导出失败.", ex);
                this.failMsg = new FailMessage("数据导出失败！\n" + ex.Message, 1000);
                this.failMsg.ShowDialog(this);
            }
        }


        #region 方法
        /// <summary>
        /// 获取当前所在的班次时间
        /// </summary>
        private void GetClassTime()
        {
            DateTime timenow = DateTime.Now;
            DateTime time1 = DateTime.Today.Add(new TimeSpan(7, 30, 0));
            DateTime time2 = DateTime.Today.Add(new TimeSpan(19, 30, 0));
            DateTime time3 = DateTime.Today.Add(new TimeSpan(-1, 19, 30, 0));
            DateTime time4 = DateTime.Today.Add(new TimeSpan(1, 7, 30, 0));
            if (timenow >= time1 && timenow < time2)//当天A班
            {
                this.timeStart = time1;
                this.timeStop = time2;
                dtpClassDate.Value = time1.Date;
                rdoClassA.Checked = true;
                rdoClassB.Checked = false;
            }
            else if (timenow < time1)//前一天B班
            {
                this.timeStart = time3;
                this.timeStop = time1;
                dtpClassDate.Value = time3.Date;
                rdoClassA.Checked = false;
                rdoClassB.Checked = true;
            }
            else//当天B班
            {
                this.timeStart = time2;
                this.timeStop = time4;
                dtpClassDate.Value = time2.Date;
                rdoClassA.Checked = false;
                rdoClassB.Checked = true;
            }
        }

        /// <summary>
        /// 自动刷新定时器程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (cmbShopName.SelectedIndex + 1 < cmbShopName.Items.Count)
                {
                    cmbShopName.SelectedIndex = cmbShopName.SelectedIndex + 1;
                }
                else
                {
                    cmbShopName.SelectedIndex = 0;
                }
                this.dtBoard = GetBoardData();
                ShowBoardData(this.dtBoard);
                //chtData.Titles[0].Text = string.Format("{0}当班配料统计表", cmbShopName.Text);
                ////GetClassTime();
                //CallWithTimeout(RefreshData, 30000);
            }
            catch (TimeoutException tex)
            {
                LogHelper.Error("刷新数据超时", tex);
                lblError.Text = "刷新数据超时！";
                //this.failMsg = new FailMessage("刷新数据超时！\n" + tex.Message, 1000);
                //this.failMsg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据刷新失败！", ex);
                lblError.Text = "数据刷新失败！";
                //this.failMsg = new FailMessage("数据刷新失败！\n" + ex.Message, 1000);
                //this.failMsg.ShowDialog(this);
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            this.dtBoard = GetBoardData();
            ShowBoardData(this.dtBoard);
        }

        /// <summary>
        /// 异步调用方法，并控制执行时间
        /// </summary>
        /// <param name="action">方法</param>
        /// <param name="timeoutMilliseconds">超时时间（毫秒）</param>
        private void CallWithTimeout(Action action, int timeoutMilliseconds)
        {
            Thread threadToKill = null;
            Action wrappedAction = () =>
            {
                threadToKill = Thread.CurrentThread;
                action();
            };

            IAsyncResult result = wrappedAction.BeginInvoke(null, null);
            if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
            {
                wrappedAction.EndInvoke(result);
            }
            else
            {
                threadToKill.Abort();
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// 更新显示数据的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ShowBoardDataCallback(DataTable dtBoardData);
        /// <summary>
        /// 更新显示数据
        /// </summary>
        private void ShowBoardData(DataTable dtBoardData)
        {

                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                ShowBoardDataCallback d = new ShowBoardDataCallback(ShowBoardData);
                this.BeginInvoke(d, new object[] { dtBoardData });
            }
            else
            {
                try
                {
                    // 数据填充到DataGridView
                    this.dgvData.DataSource = dtBoardData;

                    if (rdoDaily.Checked)
                    {
                        chtData.Titles[0].Text = string.Format("{0}当班配料统计表", cmbShopName.Text);
                        // 数据显示在Chart
                        chtData.DataSource = dtBoardData;
                        chtData.Series[0].XValueMember = "Model";
                        chtData.Series[0].YValueMembers = "DeliveryLot";
                        chtData.Series[1].XValueMember = "Model";
                        chtData.Series[1].YValueMembers = "CheckLot";
                        chtData.Series[2].XValueMember = "Model";
                        chtData.Series[2].YValueMembers = "ReceiveLot";
                        chtData.Series[0].ChartType = SeriesChartType.Column;
                        chtData.Series[1].ChartType = SeriesChartType.Column;
                        chtData.Series[2].ChartType = SeriesChartType.Column;
                        chtData.Titles[1].Text = string.Format("更新时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        chtHistory.Titles[0].Text = string.Format("{0}历史剩余统计表", cmbShopName.Text);
                        // 数据显示在Chart
                        chtHistory.DataSource = dtBoardData;
                        chtHistory.Series[0].XValueMember = "Model";
                        chtHistory.Series[0].YValueMembers = "LeaveLot";
                        chtHistory.Series[0].ChartType = SeriesChartType.Column;
                        chtHistory.Titles[1].Text = string.Format("更新时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("数据刷新失败！", ex);
                    //this.failMsg = new FailMessage("数据刷新失败！\n" + ex.Message, 1000);
                    //this.failMsg.ShowDialog(this);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 获取看板数据表
        /// </summary>
        /// <returns></returns>
        private DataTable GetBoardData()
        {
            // 看板数据表
            DataTable table = new DataTable();
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("DeliveryCount", typeof(Int32));
            table.Columns.Add("DeliveryLot", typeof(Int32));
            table.Columns.Add("CheckCount", typeof(Int32));
            table.Columns.Add("CheckLot", typeof(Int32));
            table.Columns.Add("ReceiveCount", typeof(Int32));
            table.Columns.Add("ReceiveLot", typeof(Int32));
            table.Columns.Add("LeaveCount", typeof(Int32));
            table.Columns.Add("LeaveLot", typeof(Int32));
            //table.PrimaryKey = new DataColumn[] { table.Columns["Model"] };

            
            if (rdoDaily.Checked)
            {
                if (string.IsNullOrEmpty(this.shopName))
                {
                    // 当班配料信息表
                    this.dtDailyData = DAL.DAL_TPL_Glass_Delivery.GetDailyData(this.timeStart, this.timeStop);
                }
                else
                {
                    //DataView dv = new DataView(this.dtLine);
                    //dv.RowFilter = string.Format("SPL_ShopName = '{0}'", this.shopName);
                    //DataTable dt = dv.ToTable(true, new string[] { "SPL_JobCode" });
                    //string[] line = dt.AsEnumerable().Select(d => d.Field<string>("SPL_JobCode")).ToArray();
                    // 当班配料信息表
                    this.dtDailyData = DAL.DAL_TPL_Glass_Delivery.GetDailyData(this.timeStart, this.timeStop, this.shopName);
                }
            }
            else
            {
                // 历史配料信息表
                this.dtDailyData = DAL.DAL_TPL_Glass_Delivery.GetDailyData(this.shopName);
            }

            // 获取规格型号列表
            DataView dvModel = new DataView(dtDailyData);
            this.dtModel = dvModel.ToTable(true, "GD_ProductModel");
            dvModel = dtModel.DefaultView;
            // 遍历每一种型号
            foreach (DataRowView row in dvModel)
            {
                // 规格型号
                string model = row["GD_ProductModel"].ToString();
                // 新建数据行
                DataRow dataRow = table.NewRow();
                // 筛选当前型号数据
                DataView dvDelivery = new DataView(dtDailyData);
                dvDelivery.RowFilter = string.Format("GD_ProductModel = '{0}'", model);

                // 规格型号
                dataRow["Model"] = model;
                // 发料批数
                dataRow["DeliveryLot"] = dvDelivery.Count;
                // 计算发料数量,接收数量,接收批数,投料数量，投料批数
                int deliveryCount = 0;      //发料数量
                int checkCount = 0;         //收料数量
                int checkLot = 0;           //收料批数
                int receiveCount = 0;       //投料数量
                int receiveLot = 0;         //投料批数
                foreach (DataRowView rowDelivery in dvDelivery)
                {
                    // 增加发料数量
                    deliveryCount += Convert.ToInt32(rowDelivery["GD_MaterialCount"]);

                    // 如果有收料时间记录，则增加收料数量和批数
                    if (Convert.ToDateTime(rowDelivery["GD_CheckTime"]) != new DateTime(1900,1,1))
                    {
                        checkCount += Convert.ToInt32(rowDelivery["GD_MaterialCount"]);
                        checkLot += 1;
                    }

                    // 如果有上料人员工号记录，则增加已用数量和批数
                    if (Convert.ToDateTime(rowDelivery["GD_ReceiveTime"]) != new DateTime(1900, 1, 1))
                    {
                        receiveCount += Convert.ToInt32(rowDelivery["GD_MaterialCount"]);
                        receiveLot += 1;
                    }
                }
                dataRow["DeliveryCount"] = deliveryCount;
                dataRow["CheckCount"] = checkCount;
                dataRow["CheckLot"] = checkLot;
                dataRow["ReceiveCount"] = receiveCount;
                dataRow["ReceiveLot"] = receiveLot;
                dataRow["LeaveCount"] = deliveryCount - receiveCount;
                dataRow["LeaveLot"] = dvDelivery.Count - receiveLot;

                table.Rows.Add(dataRow);
            }
            return table;
        }
        #endregion 方法

        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                CallWithTimeout(RefreshData, 300000);
            }
            catch (TimeoutException tex)
            {
                LogHelper.Error("刷新数据超时", tex);
                this.failMsg = new FailMessage("刷新数据超时！\n" + tex.Message, 1000);
                this.failMsg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据刷新失败！", ex);
                this.failMsg = new FailMessage("数据刷新失败！\n" + ex.Message, 1000);
                this.failMsg.ShowDialog(this);
            }
        }

        /// <summary>
        /// 鼠标左键点击柱状图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chtData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            Chart chart = sender as Chart;
            dgvData.ClearSelection();
            System.Windows.Forms.DataVisualization.Charting.HitTestResult Result = new System.Windows.Forms.DataVisualization.Charting.HitTestResult();
            Result = chart.HitTest(e.X, e.Y);
            if (Result.Series != null)
            {
                string model = Result.Series.Points[Result.PointIndex].AxisLabel;
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    if (dgvData["colModel", i].Value.ToString() == model)
                    {
                        dgvData.Rows[i].Selected = true;
                        dgvData.FirstDisplayedScrollingRowIndex = i;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 双击投料信息表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvData.SelectedRows.Count != 1 || e.Button != System.Windows.Forms.MouseButtons.Left) return;
            this.tmrRefresh.Stop();//停止轮询车间
            splitDaily.Panel1Collapsed = true;
            dgvData.Visible = false;
            pnlDetails.Visible = true;
            pnlDetails.Dock = DockStyle.Fill;

            // 选中型号
            string model = dgvData.SelectedRows[0].Cells["colModel"].Value.ToString();
            // 初始化型号选中下拉框
            cmbModel.Items.Clear();
            foreach (DataRowView row in this.dtModel.DefaultView)
            {
                // 规格型号
                cmbModel.Items.Add(row["GD_ProductModel"].ToString());
            }
            // 选中当前型号
            if (cmbModel.Items.Contains(model))
            {
                cmbModel.SelectedIndex = cmbModel.Items.IndexOf(model);
            }
            // 默认查看已发料
            cmbView.SelectedIndex = 0;
        }

        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            splitDaily.Panel1Collapsed = false;
            pnlDetails.Visible = false;
            dgvData.Visible = true;
            dgvData.Dock = DockStyle.Fill;
            this.tmrRefresh.Start();
        }

        /// <summary>
        /// 选择型号，刷新投料详细信息表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModel.SelectedIndex == -1 || cmbModel.Text == "") return;
            if (cmbView.SelectedIndex == -1 || cmbView.Text == "") return;

            string model = cmbModel.Text;
            // 筛选并显示选中型号的投料详细信息
            DataView dv = new DataView(this.dtDailyData);
            string filter = "";
            switch (cmbView.SelectedIndex)
            {
                case 0: //已发料，即全部数据
                    filter = string.Format("GD_ProductModel = '{0}'", model);
                    break;
                case 1: //已收料
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State <> '已发料'", model);
                    break;
                case 2: //已投料
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State = '已投料'", model);
                    break;
                case 3: //未收料
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State = '已发料'", model);
                    break;
                case 4: //未投料
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State <> '已投料'", model);
                    break;
                case 5: //发料漏扫描
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_DeliveryOP = ''", model);
                    break;
                case 6: //收料漏扫描
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State <> '已发料' AND GD_CheckOP = ''", model);
                    break;
            }
            dv.RowFilter = filter;
            dgvDetails.DataSource = dv;
        }

        /// <summary>
        /// 选择查看类型
        /// 已发料
        /// 已收料
        /// 已投料
        /// 未收料
        /// 未投料
        /// 发料漏扫描
        /// 收料漏扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModel.SelectedIndex == -1 || cmbModel.Text == "") return;
            if (cmbView.SelectedIndex == -1 || cmbView.Text == "") return;

            string model = cmbModel.Text;
            // 筛选并显示选中型号的投料详细信息
            DataView dv = new DataView(this.dtDailyData);
            string filter = "";
            switch (cmbView.SelectedIndex)
            {
                case 0: //已发料，即全部数据
                    filter = string.Format("GD_ProductModel = '{0}'", model);
                    break;
                case 1: //已收料
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State <> '已发料'", model);
                    break;
                case 2: //已投料
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State = '已投料'", model);
                    break;
                case 3: //未收料
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State = '已发料'", model);
                    break;
                case 4: //未投料
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State <> '已投料'", model);
                    break;
                case 5: //发料漏扫描
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_DeliveryOP = ''", model);
                    break;
                case 6: //收料漏扫描
                    filter = string.Format("GD_ProductModel = '{0}' AND GD_State <> '已发料' AND GD_CheckOP = ''", model);
                    break;
            }
            dv.RowFilter = filter;
            dgvDetails.DataSource = dv;
        }

        private void cmbShopName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbShopName.SelectedIndex == -1)
            {
                this.shopName = "";
            }
            else
            {
                this.shopName = cmbShopName.Text;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmMaterialConfig dlg = new frmMaterialConfig();
            dlg.ShowDialog(this);
        }


    }
}
