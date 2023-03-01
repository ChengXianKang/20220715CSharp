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
    public partial class frmMaterialProgress : Form
    {
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
        /// <summary>
        /// 看板数据表
        /// </summary>
        private DataTable dtBoardData;
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;
        /// <summary>
        /// 自动刷新定时器
        /// </summary>
        private System.Windows.Forms.Timer tmrRefresh = new System.Windows.Forms.Timer();
        /// <summary>
        /// 自动轮播定时器
        /// </summary>
        private System.Windows.Forms.Timer tmrPlay = new System.Windows.Forms.Timer();
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 当前显示图表工单
        /// </summary>
        private string CurrentOrder;
        /// <summary>
        /// 工单列表
        /// </summary>
        private List<string> lstOrder;
        ///// <summary>
        ///// 数据加载中
        ///// </summary>
        //private bool isLoading = false;
        /// <summary>
        /// 当前选择的车间代码
        /// </summary>
        private string workShopCode = "C4车间";
        /// <summary>
        /// 数据刷新时间
        /// </summary>
        private DateTime refreshTime;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmMaterialProgress()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMaterialProgress_Load(object sender, EventArgs e)
        {
            // 当前程序版本
            this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = string.Format("工单配送进度看板  {0}", this.versionName);

            this.dtpBeginDate.Value = DateTime.Today.AddDays(-1);
            this.dtpEndDate.Value = DateTime.Today;
            this.cmbWorkShop.SelectedIndex = 1;

            // 初始化看板数据表
            this.dtBoardData = new DataTable();
            //this.dtBoardData.Columns.Add("ProductOrder", typeof(string));   //工单
            //this.dtBoardData.Columns.Add("ProductSpec", typeof(string));    //型号
            //this.dtBoardData.Columns.Add("LineName", typeof(string));       //产线
            //this.dtBoardData.Columns.Add("MaterialName", typeof(string));   //物料名称
            //this.dtBoardData.Columns.Add("MaterialSpec", typeof(string));   //物料规格
            //this.dtBoardData.Columns.Add("DemandNum", typeof(Int32));       //总需求
            ////this.dtBoardData.Columns.Add("OrderNum", typeof(Int32));        //工单数
            ////this.dtBoardData.Columns.Add("AddNum", typeof(Int32));          //补料数
            //this.dtBoardData.Columns.Add("CallNum", typeof(Int32));         //已叫料
            //this.dtBoardData.Columns.Add("SendNum", typeof(Int32));         //已配送
            //this.dtBoardData.Columns.Add("ReceiveNum", typeof(Int32));      //已接收
            //this.dtBoardData.Columns.Add("InputNum", typeof(Int32));        //已上料

            // 初始化配置参数
            if (!appConfig.ContainsKey("RefreshTime"))
            {
                appConfig.SetConfig("RefreshTime", "60000");
            }
            if (!appConfig.ContainsKey("PlayInterval"))
            {
                appConfig.SetConfig("PlayInterval", "30000");
            }

            this.tmrRefresh.Interval = appConfig.GetConfigInt("RefreshTime");
            this.tmrRefresh.Tick += new EventHandler(tmrRefresh_Tick);

            this.tmrPlay.Interval = appConfig.GetConfigInt("PlayInterval");
            this.tmrPlay.Tick += new EventHandler(tmrPlay_Tick);
        }

        /// <summary>
        /// 窗体显示时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMaterialProgress_Shown(object sender, EventArgs e)
        {
            try
            {
                // 异步刷新数据
                //CallWithTimeout(RefreshData, 60000);

                this.bgkRefresh.RunWorkerAsync();

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
        /// 选择车间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWorkShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWorkShop.SelectedIndex == -1)
            {
                this.workShopCode = "";
            }
            else
            {
                this.workShopCode = cmbWorkShop.Text;
            }
        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                //CallWithTimeout(RefreshData, 30000);
                //RefreshData();
                if (this.bgkRefresh.IsBusy) return;
                this.bgkRefresh.RunWorkerAsync();
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
                string MaterialName = Result.Series.Points[Result.PointIndex].AxisLabel;
                //for (int i = 0; i < dgvData.Rows.Count; i++)
                //{
                //    if (dgvData["colModel", i].Value.ToString() == model)
                //    {
                //        dgvData.Rows[i].Selected = true;
                //        dgvData.FirstDisplayedScrollingRowIndex = i;
                //        return;
                //    }
                //}
            }
        }

        private void dgvData_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) return;
            // 当前点击的工单
            this.CurrentOrder = dgvData.SelectedRows[0].Cells["colOrder"].Value.ToString();

            // 图标中显示该工单数据
            ShowChart(this.dtBoardData, ref this.CurrentOrder);
        }

        /// <summary>
        /// 上一个工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (this.lstOrder.Count == 0) return;

            int idx = 0;

            if (this.lstOrder.Contains(this.CurrentOrder))
            {
                idx = this.lstOrder.IndexOf(this.CurrentOrder) - 1;
            }
            if (idx < 0)
            {
                idx = 0;
            }

            this.CurrentOrder = this.lstOrder[idx];
            dgvData.ClearSelection();   //取消所有选择
            for (int i = 0; i < dgvData.Rows.Count; i++)
            {
                if (dgvData["colOrder", i].Value.ToString() == this.CurrentOrder)
                {
                    dgvData.Rows[i].Selected = true;
                    dgvData.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
            ShowChart(this.dtBoardData, ref this.CurrentOrder);
        }

        /// <summary>
        /// 下一个工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnForward_Click(object sender, EventArgs e)
        {
            if (this.lstOrder.Count == 0) return;

            int idx = 0;

            if (this.lstOrder.Contains(this.CurrentOrder))
            {
                idx = this.lstOrder.IndexOf(this.CurrentOrder) + 1;
            }
            if (idx >= this.lstOrder.Count)
            {
                idx = this.lstOrder.Count - 1;
            }

            this.CurrentOrder = this.lstOrder[idx];
            dgvData.ClearSelection();   //取消所有选择
            for (int i = 0; i < dgvData.Rows.Count; i++)
            {
                if (dgvData["colOrder", i].Value.ToString() == this.CurrentOrder)
                {
                    dgvData.Rows[i].Selected = true;
                    dgvData.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
            ShowChart(this.dtBoardData, ref this.CurrentOrder);
        }

        /// <summary>
        /// 自动轮播按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            // 待播放状态
            if (btnPlay.ImageKey == "play")
            {
                btnPlay.ImageKey = "stop";
                this.tmrPlay.Start();
            }
            // 播放中状态
            else
            {
                btnPlay.ImageKey = "play";
                this.tmrPlay.Stop();
            }
        }
        #region 方法
 
        /// <summary>
        /// 自动刷新定时器程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                //lblError.Text = "";
                //GetClassTime();
                //CallWithTimeout(RefreshData, 30000);
                if (this.bgkRefresh.IsBusy) return;
                this.bgkRefresh.RunWorkerAsync();
            }
            catch (TimeoutException tex)
            {
                LogHelper.Error("刷新数据超时", tex);
                //lblError.Text = "刷新数据超时！";
                //this.failMsg = new FailMessage("刷新数据超时！\n" + tex.Message, 1000);
                //this.failMsg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据刷新失败！", ex);
                //lblError.Text = "数据刷新失败！";
                //this.failMsg = new FailMessage("数据刷新失败！\n" + ex.Message, 1000);
                //this.failMsg.ShowDialog(this);
            }
        }

        /// <summary>
        /// 自动轮播定时器程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrPlay_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.lstOrder.Count == 0) return;

                int idx = 0;

                if (this.lstOrder.Contains(this.CurrentOrder))
                {
                    idx = this.lstOrder.IndexOf(this.CurrentOrder) + 1;
                }
                if (idx >= this.lstOrder.Count)
                {
                    idx = 0;
                }

                this.CurrentOrder = this.lstOrder[idx];
                dgvData.ClearSelection();   //取消所有选择
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    if (dgvData["colOrder", i].Value.ToString() == this.CurrentOrder)
                    {
                        dgvData.Rows[i].Selected = true;
                        dgvData.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
                ShowChart(this.dtBoardData, ref this.CurrentOrder);
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据刷新失败！", ex);
                //lblError.Text = "数据刷新失败！";
                //this.failMsg = new FailMessage("数据刷新失败！\n" + ex.Message, 1000);
                //this.failMsg.ShowDialog(this);
            }
        }

        /// <summary>
        /// 获取看板数据表
        /// </summary>
        private DataTable GetBoardData()
        {
            DataTable dtBoard = new DataTable();
            try
            {
                // 从数据库获取物流配送进度数据表
                string sql = string.Format("EXEC TPL_ProductMaterial_Progress_Current '{0}'", this.workShopCode);
                dtBoard = conn.ExecuteDataTable(sql, "Base");
                //dv.Sort = "tsm_LineCode ASC,tsm_ProductCode ASC";

//                string orderList = "";
//                foreach (DataRowView row in dv)
//                {
//                    string order = row["tsm_ProductCode"].ToString();
//                    if (!orderList.Contains(order))
//                    {
//                        orderList += "'" + order + "',";
//                    }

//                    dtBoard.Rows.Add(
//                        new object[] { 
//                            row["tsm_ProductCode"],
//                            row["tsm_ProductSpec"],
//                            row["tsm_LineName"],
//                            row["tsm_MaterialName"],
//                            row["tsm_MaterialSpec"],
//                            row["tsm_TotalDemandNum"],
//                            //row["tsm_TotalDemandNum"],
//                            //0,
//                            row["tsm_TotalCallNum"],
//                            row["tsm_TotalSendNum"],
//                            row["tsm_TotalReciveNum"],
//                            row["tsm_TotalInputNum"]
//                        });
//                }

//                // 获取LCD上料数据
//                if (orderList != "")
//                {
//                    orderList = orderList.TrimEnd(',');
//                    sql = string.Format(@"SELECT Store_ProduceOrder_Main.SPOM_JobCode AS ProductOrder, 
//		                                Store_ProduceOrder_Main.SPOM_SMSpec AS ProductSpec,
//		                                Store_Procedure_Line.SPL_Name AS LineName,
//		                                'LCD' AS MaterialName,
//                                        ISNULL(TPL_Glass_Delivery.GD_ProductModel,'无') AS MaterialSpec, 
//                                        Store_ProduceOrder_Main.SPOM_Nums AS DemandNum,
//		                                (Store_ProduceOrder_Main.SPOM_Nums) AS CallNum,
//                                        0 AS NotCallNum,
//		                                ISNULL(SUM(TPL_Glass_Delivery.GD_MaterialCount),0) AS SendNum,
//		                                ISNULL(SUM(TPL_Glass_Delivery.GD_MaterialCount),0) AS ReceiveNum,
//		                                ISNULL(SUM(TPL_Glass_Delivery.GD_MaterialCount),0) AS InputNum
//                                        FROM Store_ProduceOrder_Main
//                                        LEFT JOIN [YJ_TXDMES].[dbo].[TPL_Glass_Delivery] ON Store_ProduceOrder_Main.SPOM_JobCode = TPL_Glass_Delivery.GD_ReceiveOrder
//                                        LEFT JOIN Store_Procedure_Line ON Store_ProduceOrder_Main.SPOM_SPLTid = Store_Procedure_Line.SPL_Tid
//                                        WHERE SPOM_JobCode IN({0})
//                                        GROUP BY Store_ProduceOrder_Main.SPOM_JobCode,Store_ProduceOrder_Main.SPOM_SMSpec,Store_Procedure_Line.SPL_Name,TPL_Glass_Delivery.GD_ProductModel,Store_ProduceOrder_Main.SPOM_Nums"
//                        , orderList);
//                    DataTable dt = conn.ExecuteDataTable(sql, "Base");

//                    foreach (DataRow row in dt.Rows)
//                    {
//                        dtBoard.ImportRow(row);
//                    }
//                }
//                // 按产线、工单、物料名排序
//                dtBoard.DefaultView.Sort = "LineName ASC,ProductOrder ASC,MaterialName ASC";
//                dtBoard = dtBoard.DefaultView.ToTable();
            }
            catch (Exception ex)
            {
                LogHelper.Error("物流看板刷新数据失败.", ex);
                throw ex;
            }
            this.refreshTime = DateTime.Now;
            return dtBoard;
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
        private delegate void ShowBoardDataCallback();
        /// <summary>
        /// 更新显示数据
        /// </summary>
        private void ShowBoardData()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.dgvData.InvokeRequired)
            {
                ShowBoardDataCallback d = new ShowBoardDataCallback(ShowBoardData);
                this.dgvData.BeginInvoke(d);
            }
            else
            {
                try
                {
                    this.lblRefreshTime.Text = string.Format("更新时间:{0}", this.refreshTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    this.dgvData.DataSource = this.dtBoardData.DefaultView;
                    // 数据显示在Chart
                    ShowChart(dtBoardData, ref this.CurrentOrder);

                    dgvData.ClearSelection();   //取消所有选择
                    for (int i = 0; i < dgvData.Rows.Count; i++)
                    {
                        if (dgvData["colOrder", i].Value.ToString() == this.CurrentOrder)
                        {
                            dgvData.Rows[i].Selected = true;
                            dgvData.FirstDisplayedScrollingRowIndex = i;
                            break;
                        }
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
        /// 更新图表显示数据
        /// </summary>
        /// <param name="dtBoardData"></param>
        /// <param name="ProductOrder"></param>
        private void ShowChart(DataTable dtBoardData, ref string ProductOrder)
        {
            try
            {
                string order = "";
                string model = "";
                string line = "";
                DataView dv = new DataView(dtBoardData);
                if (dtBoardData.Rows.Count > 0)
                {
                    dv.RowFilter = string.Format("ProductOrder = '{0}'", ProductOrder);
                    if (dv.Count == 0)
                    {
                        ProductOrder = dtBoardData.Rows[0]["ProductOrder"].ToString();
                        dv.RowFilter = string.Format("ProductOrder = '{0}'", ProductOrder);
                    }
                    order = dv[0]["ProductOrder"].ToString();
                    model = dv[0]["ProductSpec"].ToString();
                    line = dv[0]["LineName"].ToString();
                }
                else
                {
                    ProductOrder = "";
                }
                chtData.Titles[0].Text = string.Format("工单:{0}  |   型号:{1}  |   产线:{2}", order, model, line);
                chtData.Titles[1].Text = string.Format("更新时间：{0}", this.refreshTime.ToString("yyyy-MM-dd HH:mm:ss"));
                // 数据显示在Chart
                chtData.DataSource = dv;
                chtData.Series[0].XValueMember = "MaterialName";
                chtData.Series[0].YValueMembers = "DemandNum";
                //chtData.Series[1].XValueMember = "MaterialName";
                //chtData.Series[1].YValueMembers = "AddNum";
                chtData.Series[1].XValueMember = "MaterialName";
                chtData.Series[1].YValueMembers = "CallNum";
                chtData.Series[2].XValueMember = "MaterialName";
                chtData.Series[2].YValueMembers = "SendNum";
                chtData.Series[3].XValueMember = "MaterialName";
                chtData.Series[3].YValueMembers = "ReceiveNum";
                chtData.Series[4].XValueMember = "MaterialName";
                chtData.Series[4].YValueMembers = "InputNum";
                chtData.Series[0].ChartType = SeriesChartType.Column;
                chtData.Series[1].ChartType = SeriesChartType.Column;
                chtData.Series[2].ChartType = SeriesChartType.Column;
                chtData.Series[3].ChartType = SeriesChartType.Column;
                chtData.Series[4].ChartType = SeriesChartType.Column;
                //chtData.Series[5].ChartType = SeriesChartType.Column;
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据刷新失败！", ex);
                //this.failMsg = new FailMessage("数据刷新失败！\n" + ex.Message, 1000);
                //this.failMsg.ShowDialog(this);
                throw ex;
            }
        }
        #endregion 方法



        /// <summary>
        /// 数据刷新后台处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgkRefresh_DoWork(object sender, DoWorkEventArgs e)
        {
            // 获取看板数据
            DataTable dt = GetBoardData();
            e.Result = dt;
        }
        /// <summary>
        /// 数据刷新后台处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgkRefresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTable dt = new DataTable();
            if (e.Result != null)
            {
                dt = e.Result as DataTable;
            }
            this.dtBoardData = dt;

            // 获取工单列表
            this.lstOrder = new List<string>();
            DataView dvBoard = new DataView(this.dtBoardData);
            DataTable dtOrder = dvBoard.ToTable(true, "ProductOrder");
            foreach (DataRow row in dtOrder.Rows)
            {
                this.lstOrder.Add(row["ProductOrder"].ToString());
            }
            ShowBoardData();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMaterialProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrPlay.Stop();
            this.tmrRefresh.Stop();
        }





    }
}
