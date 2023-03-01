using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Utils.HBaseClass;
using Utils;
using Utils.Model;

namespace MDC
{
    public partial class frmOlded : Form
    {
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 工控机本地IP
        /// </summary>
        private string ipAddress;
        /// <summary>
        /// 玻璃信息
        /// </summary>
        private GlassInfo glassInfo;
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;        //失败提示框
        /// <summary>
        /// 成功提示框
        /// </summary>
        private SuccessMessage successMsg;  //成功提示框
        /// <summary>
        /// 警告提示框
        /// </summary>
        private WarnMessage warnMsg;//警告提示框
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 玻璃信息查询超时时间
        /// </summary>
        private int searchTimeout;
        /// <summary>
        /// 最大日志行数
        /// </summary>
        private int maxLogCount;
        /// <summary>
        /// 是否记录程序运行日志
        /// </summary>
        private bool isWriteLog;
        /// <summary>
        /// 工序路由
        /// </summary>
        private DataTable dtProcessRoute;
        /// <summary>
        /// 玻璃计数
        /// </summary>
        private int LCD_Count = 0;
        /// <summary>
        /// OK计数
        /// </summary>
        private int OK_Count = 0;
        /// <summary>
        /// NG计数
        /// </summary>
        private int NG_Count = 0;
        /// <summary>
        /// 混料计数
        /// </summary>
        private int RuleError_Count = 0;
        /// <summary>
        /// 产线编码
        /// </summary>
        private string LineCode { get; set; }
        /// <summary>
        /// 产线名称
        /// </summary>
        private string LineName { get; set; }

        /// <summary>
        /// 当前工单的所有工序列表DataTable
        /// </summary>
        private DataTable dtProcessTable;
        /// <summary>
        /// 工序列表DataView
        /// </summary>
        private DataView dvProcess;
        /// <summary>
        /// 工序路由列表
        /// </summary>
        private Dictionary<string, string> dicProcessRoute;
        /// <summary>
        /// 当前已过工序的Dictionary
        /// </summary>
        private Dictionary<string, string> dicProcess;

        /// <summary>
        /// 最近50片扫描记录示意图块宽度
        /// </summary>
        private int lblWidth = 4;

        /// <summary>
        /// 最近若干片玻璃的绑定结果记录（字符串的每一位代表一片玻璃的绑定结果，0：OK，1：NG，2：混料）
        /// </summary>
        private string ScanNote = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmOlded()
        {
            InitializeComponent();

            txtProcessIP.Dock = DockStyle.Fill;
            txtCurrentOpCode.Dock = DockStyle.Fill;
            txtScanCode.Dock = DockStyle.Fill;
            txtLCDCode.Dock = DockStyle.Fill;

            txtEndDate.Dock = DockStyle.Fill;
            txtStartDate.Dock = DockStyle.Fill;
            txtDuration.Dock = DockStyle.Fill;
            txtProductOrder.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;

            cmbLineName.Dock = DockStyle.Fill;

            txtProcessIP.Clear();
            txtCurrentOpCode.Clear();
            txtScanCode.Clear();
            txtLCDCode.Clear();

            txtEndDate.Clear();
            txtStartDate.Clear();
            txtDuration.Clear();
            txtProductOrder.Clear();
            txtModel.Clear();
            txtModel.Tag = null;

            cmbLineName.Items.Clear();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOlded_Load(object sender, EventArgs e)
        {
            try
            {
                int width = (flpScanNote.Width - 3) / 25 - 1;
                if (width <= 0)
                {
                    width = 1;
                }
                this.lblWidth = width;
                // 当前程序版本
                this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                this.Text = string.Format("老化过站   {0}   （技术支持：深圳市鼎立特科技有限公司）", versionName);

                // 绑定串口数据接收事件处理程序
                Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;
                // 程序初始化
                Initialize();
            }
            catch (Exception ex)
            {
                LogHelper.Error("窗体加载失败", ex);
                ShowResult(NoteState.Error, "", "程序初始化失败!" + ex.Message);
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNGReworkScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 解除绑定串口数据接收事件处理程序
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
        }

        /// <summary>
        /// 窗口大小改变时，刷新窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNGReview_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
            txtScanCode.Focus();
            txtScanCode.SelectAll();
        }


        /// <summary>
        /// 程序初始化
        /// </summary>
        private void Initialize()
        {
            try
            {
                // 玻璃信息查询超时时间
                searchTimeout = appConfig.GetConfigInt("SearchTimeout");

                // 最大日志行数
                maxLogCount = appConfig.GetConfigInt("MaxLogCount");
                // 是否记录程序运行日志
                isWriteLog = appConfig.GetConfigBool("WriteAppLog");

                // 登录人员账号
                //txtCurrentOpCode.Text = BaseUI.UI_BOOLOGNAME;
                txtCurrentOpCode.Text = BaseUI.UI_BOOPNAME;

                // 当前站点IP
                this.ipAddress = BaseUI.GetLocalIP();
                txtProcessIP.Text = this.ipAddress;

                // 初始化工序
                this.dtProcessRoute = GetProcessRoute();
                if (dtProcessRoute == null || dtProcessRoute.Rows.Count == 0)
                {
                    ShowResult(NoteState.Error, "", "获取返修工序路由失败!");
                }
                BindingReworkProcess();//产线 过站名称 下拉绑定

                this.Refresh();
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取返修工序路由
        /// </summary>
        /// <returns></returns>
        private DataTable GetProcessRoute()
        {
            string sql = @"SELECT [SGXLS_No], [SGX_JobCode], [SGXLS_SGXName] 
                                  FROM [YJ_CWMES].[dbo].[Store_GongXuLian_Sub]
                                  JOIN [YJ_CWMES].[dbo].[Store_GongXuLian_Main] ON [SGXLS_SGXLTid] = [SGXL_Tid]
                                  JOIN [YJ_CWMES].[dbo].[Store_GongXu_Main] ON [SGXLS_SGXTid] = [SGX_Tid]
                                  WHERE [SGXL_Name] = '返修工序路由'
                                  ORDER BY [SGXLS_No]";
            DataTable dt = conn.ExecuteDataTable(sql, "Base");
            return dt;
        }

        /// <summary>
        /// 初始化 产线 过站名称 下拉绑定
        /// </summary>
        private void BindingReworkProcess()
        {
            try
            {
                DataTable dt = BaseUI.GetPLineList();
                cmbLineName.DisplayMember = "SPL_Name";
                cmbLineName.ValueMember = "SPL_JobCode";
                cmbLineName.DataSource = dt;
                if (cmbLineName.Items.Count > 0)
                {
                    cmbLineName.SelectedIndex = 0;
                }

                var oldList = BaseUI.GetOldStation();
                cmbSiteName.DataSource = oldList;
                cmbSiteName.DisplayMember = "Name";
                cmbSiteName.ValueMember = "Name";
                if (cmbSiteName.Items.Count > 0)
                {
                    cmbSiteName.SelectedIndex = 0;
                }

            }
            catch (Exception exp)
            {
                LogHelper.Error("初始化工序失败." + exp.Message, exp);
                ShowResult(NoteState.Error, "", "初始化工序失败." + exp.Message);
            }
        }


        private void cmbProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbLineName.SelectedIndex == -1) return;
            //string processCode = cmbLineName.SelectedValue.ToString();
            //if(this.dtProcessRoute != null && this.dtProcessRoute.Rows.Count >0)
            //{
            //    DataView dv = new DataView(this.dtProcessRoute);
            //    dv.RowFilter = string.Format("SGX_JobCode = '{0}'", processCode);
            //    if(dv.Count ==0)
            //    {
            //        ShowResult(NoteState.Error, "", "返修工序路由中不存在当前返修岗位"+cmbLineName.Text);
            //        cmbLineName.Tag = null;
            //    }
            //    else
            //    {
            //        cmbLineName.Tag = dv[0]["SGXLS_No"];
            //    }
            //}
        }

        /// <summary>
        /// 跨线程输出结果的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ShowResultCallback(NoteState state, string code, string message);

        /// <summary>
        /// 跨线程输出结果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        private void ShowResult(NoteState state, string code, string message)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    ShowResultCallback d = new ShowResultCallback(ShowResult);
                    this.BeginInvoke(d, new object[] { state, code, message });
                }
                else
                {
                    if (lvwNote.Items.Count >= this.maxLogCount)
                    {
                        lvwNote.Items.Clear();
                    }

                    // 添加Note信息
                    lvwNote.AddNote(state, code, message);
                    // 后台添加运行日志
                    if (this.isWriteLog)
                    {
                        Thread threadLog = new Thread(new ParameterizedThreadStart(this.AddAppLog));
                        threadLog.IsBackground = true;
                        threadLog.Start(lvwNote.Items[lvwNote.Items.Count - 1]);
                    }

                    switch (state)
                    {
                        case NoteState.Success:
                            break;
                        case NoteState.OK:
                            // 弹框提示
                            this.successMsg = new SuccessMessage("操作成功", 2);
                            this.successMsg.ShowDialog(this);
                            break;

                        case NoteState.NG:
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 2);
                            this.failMsg.ShowDialog(this);
                            break;

                        case NoteState.Error:
                            // 记录错误日志
                            LogHelper.Error(message, new Exception(message));
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 1000);
                            this.failMsg.ShowDialog(this);
                            break;
                    }
                    txtScanCode.SelectAll();
                    txtScanCode.Focus();
                    // 清除扫码枪串口接收缓冲区
                    Program.ScanDevice.DiscardBuffer();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }


        /// <summary>
        /// 添加程序运行日志
        /// </summary>
        /// <param name="logString"></param>
        private void AddAppLog(object logObject)
        {
            try
            {
                ListViewItem item = logObject as ListViewItem;
                string msg = "";
                msg += item.SubItems[1].Text.PadRight(10) + "   ";
                msg += item.SubItems[2].Text + "   ";
                msg += item.SubItems[3].Text.PadRight(70) + "   ";
                msg += item.SubItems[4].Text;
                LogHelper.Note(msg);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 接收扫描枪串口数据
        /// </summary>
        /// <param name="e"></param>
        private void ScanDevice_DataReceived(Utils.DataReceivedEventArgs e)
        {
            try
            {
                byte[] ReDatas = e.BytesReceived;
                string DataString = (new UTF8Encoding().GetString(ReDatas)).Replace("\r", "").Replace("\n", "");
                Debug.WriteLine("收到串口数据：" + DataString);
                // 分析处理数据
                ScanDataHandler(DataString);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, Program.ScanDevice.PortName, "扫描设备接收数据失败." + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 实际扫描数据文本框按下回车键，开始处理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScanCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtScanCode.Text.Trim() != "")
            {
                // 分析处理数据
                ScanDataHandler(txtScanCode.Text.Trim().Replace("\r", "").Replace("\n", ""));
            }
        }

        /// <summary>
        /// 分析处理扫描数据的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ScanDataHandlerCallback(string DataString);

        ScanDataHandlerCallback d;
        /// <summary>
        /// 分析处理扫描数据
        /// </summary>
        /// <param name="DataString">扫描枪接收字符串</param>
        private void ScanDataHandler(string DataString)
        {
            DataString = DataString.Trim();
            try
            {
                if (this.InvokeRequired)
                {
                    d = new ScanDataHandlerCallback(ScanDataHandler);
                    this.BeginInvoke(d, new object[] { DataString });
                }
                else
                {
                    //txtScanCode.Enabled = false;
                    // 第1步：站点基本信息检测
                    #region 第1步：站点基本信息检测
                    if (string.IsNullOrWhiteSpace(cmbLineName.SelectedValue.ToString()))
                    {
                        ShowResult(NoteState.Fail, "", "当前账号未配置返修过站权限");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtCurrentOpCode.Text))
                    {
                        ShowResult(NoteState.Fail, "", "获取扫描人员账号失败");
                        return;
                    }
                    #endregion 第1步：站点基本信息检测

                    // 第2步：解析扫描数据
                    #region 第2步：解析扫描数据
                    //编码规则解析并填充文本框

                    if (DataString.Contains("OK:") || DataString.Contains("ERROR:"))
                    {
                        ShowResult(NoteState.NG, "", "编码不符合规则，请确认扫描枪是否打开了测试模式");
                        return;
                    }

                    string[] ds = DataString.Split(':');
                    if (ds.Length == 0)
                    {
                        ShowResult(NoteState.NG, "", "编码不符合规则");
                        return;
                    }
                    string AnalysisCode = ds[0].ToString().Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");    //扫描数据解析


                    // YFPCA-SKI65101B18S|F|S1|201121|01F68          YFPCA-SKI65101B18S/F/S1/201121/01F68
                    AnalysisCode = AnalysisCode.Replace("|", "/").Replace(";", "；");

                    //if (ds[0].Length <= 9)
                    //{
                    //    ShowResult(NoteState.NG, "", "默认编码长度至少为10位以上，请确认条码长度");
                    //    return;
                    //}

                    string scanCode = AnalysisCode.Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");
                    txtScanCode.Text = scanCode;
                    #endregion 第2步：解析扫描数据

                    #region 查询玻璃信息
                    try
                    {
                        LineCode = cmbLineName.SelectedValue.ToString();
                        LineName = cmbLineName.Text.ToString();
                        var InSiteName = cmbSiteName.SelectedValue.ToString();//进站
                        var OutSiteName = cmbSiteName.SelectedValue.ToString();//出站站
                        var ProcessIP = txtProcessIP.Text.Trim();
                        var StartDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//"yyyy-MM-dd hh:mm:ss"

                        this.glassInfo = null;

                        CallWithTimeout(GetProcessData1, this.searchTimeout);//查询玻璃相关信息

                        var BaseInfo = this.glassInfo.ProductInfo;
                        if (this.glassInfo == null)//对玻璃信息进行基本判断
                        {
                            ShowResult(NoteState.NG, scanCode, "内部查询玻璃绑定信息失败,如多次出现此错误，请关掉程序重新打开");
                            return;
                        }
                        if (this.glassInfo.ProductInfo == null || string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode))
                        {
                            ShowResult(NoteState.NG, scanCode, "未能查询到玻璃信息.");
                            return;
                        }

                        #region 检查是否已包装

                        string sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Sub WITH (NOLOCK) WHERE IBS_GlassCode='{0}'", this.glassInfo.ProductInfo.LCDCode);
                        DataView dv = conn.ExecuteDataView(sql, "Base");
                        if (dv.Count > 0)
                        {
                            ShowResult(NoteState.NG, "", "当前玻璃已包装，无法过站");
                            return;
                        }
                        sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Small_Sub WITH (NOLOCK) WHERE ISS_GlassCode='{0}'", this.glassInfo.ProductInfo.LCDCode);
                        dv = conn.ExecuteDataView(sql, "Base");
                        if (dv.Count > 0)
                        {
                            ShowResult(NoteState.NG, "", "当前玻璃已包装，无法过站");
                            return;
                        }
                        #endregion

                        #region 不良玻璃检测
                        //// 如果无LCDState或者是良品/待复判，则还需要查询SQL Server中的不良和复判信息
                        //if (this.glassInfo.GlassState == null || string.IsNullOrWhiteSpace(this.glassInfo.GlassState.LCDState) || this.glassInfo.GlassState.LCDState == "良品" || this.glassInfo.GlassState.LCDState == "待复判")
                        //{
                        //    if (this.glassInfo.GlassState == null)
                        //    {
                        //        this.glassInfo.GlassState = new GlassState();
                        //    }
                        //    ExceptionState reviewResult = BaseUI.GetFailReviewResult(this.glassInfo.ProductInfo.LCDCode);
                        //    if (string.IsNullOrWhiteSpace(this.glassInfo.GlassState.LCDState))
                        //    {
                        //        this.glassInfo.GlassState.LCDState = reviewResult.ToString();
                        //    }
                        //    else if ((this.glassInfo.GlassState.LCDState == "良品" || this.glassInfo.GlassState.LCDState == "待复判") && (reviewResult != ExceptionState.良品 && reviewResult != ExceptionState.待复判))
                        //    {
                        //        this.glassInfo.GlassState.LCDState = reviewResult.ToString();
                        //    }
                        //}

                        //if (this.glassInfo.GlassState != null && !string.IsNullOrEmpty(this.glassInfo.GlassState.LCDState))
                        //{
                        switch (this.glassInfo.GlassState.LCDState)
                        {
                            case "待复判":
                                // 如果是FOG AOI提报的不良，需要自动复判成良品
                                this.glassInfo.Exception = GetHBaseDataClass.Instance.GetException(this.glassInfo.ProductInfo.LCDCode);
                                if (this.glassInfo.Exception == null || this.glassInfo.Exception.Count == 0)
                                {
                                    break;
                                }
                                // 获取最近时间的不良记录
                                this.glassInfo.LastExceptionKey = null;
                                foreach (string key in this.glassInfo.Exception.Keys)
                                {
                                    if (this.glassInfo.LastExceptionKey == null)
                                    {
                                        this.glassInfo.LastExceptionKey = key;
                                    }
                                    else
                                    {
                                        if (DateTime.Compare(DateTime.Parse(this.glassInfo.Exception[key].ScanTime), DateTime.Parse(this.glassInfo.Exception[this.glassInfo.LastExceptionKey].ScanTime)) > 0)
                                        {
                                            this.glassInfo.LastExceptionKey = key;
                                        }
                                    }
                                }
                                // 当前不良信息
                                ExceptionInfo lastEx = this.glassInfo.Exception[glassInfo.LastExceptionKey];
                                if (lastEx.ProcessCode == "009" && (lastEx.ExceptionState == "待复判"))
                                {
                                    GetHBaseDataClass.Instance.ExceptionJudge(ref this.glassInfo, "复判良品", "Admin", "", BaseUI.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"), txtProcessIP.Text);
                                    //更新lcdState
                                    this.glassInfo.GlassState.LCDState = "良品";
                                    //更新Data_06
                                    GetHBaseDataClass.Instance.UpdateData06(this.glassInfo.ProductInfo.LCDCode, this.glassInfo.GlassState);
                                    break;
                                }
                                else
                                {
                                    new NoteResult(NoteState.Error, scanCode, "此玻璃已申报不良，\r\n请提交复判！");
                                }
                                break;
                            case "复判重工":
                                NG_Count += 1;
                                new NoteResult(NoteState.Error, scanCode, "此玻璃已复判重工，\r\n请提交重工组！");
                                break;
                            case "复判报废":
                                NG_Count += 1;
                                new NoteResult(NoteState.Error, scanCode, "此玻璃已复判报废，\r\n不能过站！");
                                break;
                            case "重工报废":
                                NG_Count += 1;

                                new NoteResult(NoteState.Error, scanCode, "此玻璃已重工报废，\r\n不能过站！");
                                break;
                        }
                        txtNGCount.Text = NG_Count.ToString();
                        //}
                        #endregion 不良玻璃检测

                        #region 漏工序检测

                        this.dtProcessTable = BaseUI.GetProduceRouteData(BaseInfo.ProductionOrder, LineCode);

                        //comProcess.DisplayMember = "SGX_Name";  //工序名称
                        //comProcess.ValueMember = "SGX_JobCode"; //工序编码
                        //comProcess.DataSource = this.dtProcessTable;
                        string msg = string.Empty;
                        string LogNumber = this.glassInfo.GlassState.LogNumber;//已过工序
                        //if (this.glassInfo.GlassState.LCDState != "良品")
                        //{
                        //    msg = "不良品";
                        //    new NoteResult(NoteState.NG, scanCode, msg);
                        //    //return;
                        //}
                        if (!this.glassInfo.GlassState.LogCode.Contains("029"))
                        {
                            msg += ",精简画面";
                            
                            //return;
                        }
                        if (!this.glassInfo.GlassState.LogCode.Contains("092"))
                        {
                            msg += ",最终外观";
                        }
                        if (!this.glassInfo.GlassState.LogCode.Contains("031"))
                        {
                            msg += ",最终外观2";
                        }
                        if (!this.glassInfo.GlassState.LogCode.Contains("033"))
                        {
                            msg += ",QQC检测";
                        }
                        if (msg != string.Empty)
                        {
                            msg = "检测到产品：" + msg + " 不能进行老化过站操作 ";
                            ShowResult(NoteState.NG, scanCode, msg);
                            return;
                        }
                        //获取工序链列表  msg = "检测到产品为不良品,不能进行老化过站操作";
                        this.dicProcessRoute = this.dtProcessTable.Rows.Cast<DataRow>().ToDictionary(x => x["SGX_JobCode"].ToString(), x => x["SGX_Name"].ToString());
                        this.dicProcess = BaseUI.GetProcedure(this.dtProcessTable);
                        if (this.glassInfo.GlassState != null)
                        {
                            string Procedure = BaseUI.GetLeakProcedure(this.glassInfo.GlassState.LogCode, BaseUI.NowCode, dicProcess);
                            if (Procedure != "")
                            {
                                NG_Count += 1;
                                txtNGCount.Text = NG_Count.ToString();
                                msg = "检测到工序 " + Procedure + " 未扫描.";
                                //new NoteResult(NoteState.NG, scanCode, msg);
                                //return;
                                ShowResult(NoteState.NG, scanCode, msg);
                                return;
                            }
                        }

                        bool Isrepeat = BaseUI.CheckProcedure(this.glassInfo.GlassState.LogCode);
                        if (Isrepeat)
                        {
                            msg = "当前工序已过站,\r\n请勿重复扫描!";
                            //new NoteResult(NoteState.NG, scanCode, msg);
                            //return;
                            ShowResult(NoteState.NG, scanCode, msg);
                            return;
                        }
                        #endregion 漏工序检测


                        txtProductOrder.Text = BaseInfo.ProductionOrder;
                        txtStartDate.Text = StartDate;
                        txtEndDate.Text = "";
                        txtModel.Text = BaseInfo.FinishesModel;
                        txtLCDCode.Text = BaseInfo.LCDCode;


                        var SingleGlass = BaseUI.GetSingleOldGlass(scanCode);
                        if (cmbSiteName.SelectedValue.ToString() == "老化进站")
                        {
                            #region 老化进站
                            if (SingleGlass != null && SingleGlass.Rows.Count != 0)
                            {
                                var InOutFlag = SingleGlass.Rows[0]["InOutFlag"].ToString();//进出站标识 0=进站 1=出站
                                var Duration = SingleGlass.Rows[0]["Duration"].ToString();//时长
                                var InStartDate = SingleGlass.Rows[0]["StartDate"].ToString();//开始
                                var InEndDate = SingleGlass.Rows[0]["EndDate"].ToString();//结束
                                if (InOutFlag == "0")
                                {
                                    ShowResult(NoteState.NG, scanCode, "该玻璃信息已经进站老化,请勿重复进站老化.");
                                }
                                else if (InOutFlag == "1")
                                {
                                    txtStartDate.Text = InStartDate;
                                    txtEndDate.Text = InEndDate;
                                    txtDuration.Text = Duration;
                                    DialogResult dgr = MessageBox.Show("该玻璃信息已经老化出站,是否重复老化进站", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (dgr == DialogResult.OK)
                                    {
                                        txtEndDate.Text = "";
                                        txtDuration.Text = "";
                                        BaseUI.RepeatOldOutStation(BaseInfo.LCDCode, StartDate, "老化进站", BaseUI.UI_BOOPID, BaseUI.UI_BOOPNAME);
                                        ShowResult(NoteState.OK, txtScanCode.Text, "重新进站老化操作成功！");
                                    }
                                    else
                                    {
                                        return;
                                    }
                                    //ShowResult(NoteState.NG, scanCode, "该玻璃信息已经老化出站,请勿重复进站老化.");
                                }
                            }
                            else
                            {
                                string InsertSql = " insert into OldStation(LCDCode,FPCCode,ClientCode,StartDate,EndDate," +
                                "Duration,LineCode,LineName,SiteName,CreateId,CreateName,ModifyId,ModifyName," +
                                "ClientIP,WorkOrderCode,Model,InOutFlag) values" +
                                "('" + BaseInfo.LCDCode + "', '" + BaseInfo.FPCCode + "', '" + BaseInfo.QRCode + "', '" + StartDate + "', ''," +
                                " '', '" + LineCode + "', '" + LineName + "', '" + InSiteName + "', '" + BaseUI.UI_BOOPID + "'," +
                                " '" + BaseUI.UI_BOOPNAME + "','','', '" + ProcessIP + "', '" + BaseInfo.ProductionOrder + "', '" + BaseInfo.FinishesModel + "', '0') ";

                                var Flag = BaseUI.InsertOldInStation(InsertSql);
                                if (Flag)
                                {
                                    AddScanNote("0");
                                    LCD_Count += 1;
                                    txtLCDCount.Text = LCD_Count.ToString();
                                    OK_Count += 1;
                                    txtOKCount.Text = OK_Count.ToString();
                                    ShowResult(NoteState.OK, txtScanCode.Text, "操作成功！");
                                }
                                else
                                {
                                    ShowResult(NoteState.Fail, scanCode, "数据保存失败.");
                                }
                            }
                            #endregion
                        }
                        else if (cmbSiteName.SelectedValue.ToString() == "老化出站")
                        {
                            #region 老化出站
                            if (SingleGlass != null && SingleGlass.Rows.Count != 0)
                            {
                                var InOutFlag = SingleGlass.Rows[0]["InOutFlag"].ToString();//进出站标识 0=进站 1=出站
                                var Duration = SingleGlass.Rows[0]["Duration"].ToString();//时长
                                var InStartDate = SingleGlass.Rows[0]["StartDate"].ToString();//开始
                                var InEndDate = SingleGlass.Rows[0]["EndDate"].ToString();//结束
                                txtStartDate.Text = InStartDate;
                                txtEndDate.Text = StartDate;
                                DateTime d1 = InStartDate.ToDate();
                                DateTime d2 = StartDate.ToDate();
                                TimeSpan d3 = d2.Subtract(d1);

                                var DurationOn = d3.Days.ToString() + "天" + d3.Hours.ToString() + "小时" + d3.Minutes.ToString() + "分钟";
                                txtDuration.Text = DurationOn;
                                //+ d3.Seconds.ToString() + "秒";
                                if (d3.Hours < 8 && d3.Days == 0)
                                {
                                    ShowResult(NoteState.NG, scanCode, "该玻璃老化时长为:" + txtDuration.Text + " 小于8小时,还不能出站操作.");
                                    return;
                                }
                                if (InOutFlag == "1")
                                {
                                    txtEndDate.Text = InEndDate;
                                    txtDuration.Text = Duration;
                                    ShowResult(NoteState.NG, scanCode, "该玻璃信息已经老化出站,请勿重复操作.");
                                    return;
                                }
                                else
                                {
                                    string EndDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//"yyyy-MM-dd hh:mm:ss"
                                    bool flag = BaseUI.UpdateOldOutStation(scanCode, EndDate, DurationOn, "老化出站", BaseUI.UI_BOOPID, BaseUI.UI_BOOPNAME);
                                    if (flag)
                                    {
                                        GetHBaseDataClass.Instance.UnBound039(this.glassInfo);
                                        AddScanNote("0");
                                        LCD_Count += 1;
                                        txtLCDCount.Text = LCD_Count.ToString();
                                        OK_Count += 1;
                                        txtOKCount.Text = OK_Count.ToString();
                                        ShowResult(NoteState.OK, txtScanCode.Text, "老化出站成功！");
                                    }
                                }
                            }
                            else
                            {
                                ShowResult(NoteState.NG, scanCode, "未查询到该玻璃老化进站信息,请确认");
                                return;
                            }
                            #endregion
                        }

                        Thread.Sleep(100);
                    }
                    catch (TimeoutException tex)
                    {
                        GetHBaseDataClass.Instance.Reconnect();
                        ShowResult(NoteState.Error, scanCode, "HBase数据库连接超时." + tex.Message.ToString());
                        return;
                    }
                    catch (Exception exp)
                    {
                        ShowResult(NoteState.Error, scanCode, "查询玻璃绑定信息失败." + exp.Message);
                        return;
                    }

                    #endregion 查询玻璃信息
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, "", ex.Message);
            }
            finally
            {
                //if (this.InvokeRequired)
                //{
                //    d = new ScanDataHandlerCallback(ScanDataHandler);
                //    this.BeginInvoke(d, new object[] { "" });
                //}

                this.RumFromInvoke(() =>
                {
                    txtScanCode.Text = "";
                });

            }
        }

        public void CreanView()
        {
            txtProductOrder.Clear();
            txtStartDate.Clear();
            txtEndDate.Clear();
            txtModel.Clear();
            txtLCDCode.Clear();
            txtScanCode.Clear();
            txtDuration.Clear();
            flpScanNote.Controls.Clear();

            txtOKCount.Clear();
            txtLCDCount.Clear();
            txtNGCount.Clear();
            txtBadRate.Clear();
        }

        /// <summary>
        /// 获取已过工序列表
        /// </summary>
        /// <param name="LogCode">已过工序编码</param>
        private Dictionary<string, string> GetProcessLogData(string exceptionKey)
        {
            if (string.IsNullOrEmpty(exceptionKey)) return null;
            string sql = string.Format(@"SELECT [PRS_ProcessCode]
                                                            ,[PRS_ProcessName]
                                                            ,[PRS_AddDate]
                                                        FROM[YJ_CWMES].[dbo].[TPL_ProductRepair_Main]
                                                        JOIN[YJ_CWMES].[dbo].[TPL_ProductRepair_Sub]
                                                        ON[PRM_Tid] = [PRS_PRMTid]
                                                        WHERE[PRM_ExceptionKey] = '{0}'
                                                        ORDER BY[PRS_AddDate]", exceptionKey);
            DataTable dt = conn.ExecuteDataTable(sql, "Base");
            Dictionary<string, string> dicProcess = dt.Rows.Cast<DataRow>().ToDictionary(x => x["PRS_ProcessCode"].ToString(), x => x["PRS_ProcessName"].ToString());
            return dicProcess;
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
        /// 获取Code1对应的玻璃基本信息和当前状态
        /// </summary>
        private void GetProcessData1()
        {
            this.glassInfo = GetHBaseDataClass.Instance.GetGlassInfo(txtScanCode.Text);//Code1查询结果
        }

        /// <summary>
        /// 后台处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWriteData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] val = null;
                if (e.Argument != null)
                {
                    val = e.Argument as object[];
                }
                GlassInfo glassInfo = (GlassInfo)val[0];
                string state = val[1].ToString();
                string op = val[2].ToString();
                string ng = val[3].ToString();
                string time = val[4].ToString();
                string ip = val[5].ToString();
                string process = val[6].ToString();

                //string judgeRst = GetHBaseDataClass.Instance.ExceptionRework(glassInfo, state, op, ng, time, ip, process, this.isRoot);
                //if (judgeRst != "RowKeyCode不存在" && judgeRst != "重工成功")
                //{
                //    e.Result = new Exception(judgeRst);
                //    return;
                //}
                //else
                //{
                //    e.Result = null;
                //}
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        /// <summary>
        /// 后台处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWriteData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Result == null)
            {
                ShowResult(NoteState.OK, "", "操作成功！");
            }
            else
            {
                Exception ex = e.Result as Exception;
                ShowResult(NoteState.NG, "", "提交失败，请重试！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 根据选择的站点名称改变窗口标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSiteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSiteName.SelectedIndex == -1) return;
            else
            {
                this.Text = cmbSiteName.Text;
                CreanView();
            }
        }
        /// <summary>
        /// 大小改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flpScanNote_SizeChanged(object sender, EventArgs e)
        {
            int width = (flpScanNote.Width - 3) / 25 - 1;
            if (width <= 0)
            {
                width = 1;
            }
            foreach (Label lbl in flpScanNote.Controls)
            {
                lbl.Width = width;
            }
            this.lblWidth = width;
        }
        /// <summary>
        /// 添加绑定记录
        /// </summary>
        /// <param name="State"></param>
        private void AddScanNote(string State)
        {
            if (this.ScanNote.Length >= 50)
            {
                this.ScanNote = this.ScanNote.Substring(1);
                this.flpScanNote.Controls.RemoveAt(0);
            }

            this.ScanNote += State;

            Color color;
            switch (State)
            {
                case "0":
                    color = Color.Lime;
                    break;
                case "1":
                    color = Color.Red;
                    break;
                case "2":
                    color = Color.Orange;
                    break;
                default:
                    color = Color.Lime;
                    break;
            }

            Label lbl = new Label()
            {
                BackColor = color,
                Margin = new System.Windows.Forms.Padding(1, 1, 0, 1),
                Size = new System.Drawing.Size(this.lblWidth, 10)

            };

            this.flpScanNote.Controls.Add(lbl);
        }
    }
}
