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
    public partial class frmNGRework : Form
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
        /// 当前工单的所有工序列表DataTable
        /// </summary>
        private DataTable dtProcessTable;
        ///// <summary>
        ///// 当前工单工序链
        ///// </summary>
        //Dictionary<string, string> dicProcess;
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
        /// 所有不良项数据表
        /// </summary>
        private DataTable dtBNC;
        /// <summary>
        /// 是否有最高解绑权限
        /// </summary>
        private bool isRoot = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmNGRework()
        {
            InitializeComponent();

            txtProcessIP.Dock = DockStyle.Fill;
            txtCurrentOpCode.Dock = DockStyle.Fill;
            btnScrap.Dock = DockStyle.Fill;
            btnErrJudge.Dock = DockStyle.Fill;
            comProcess.Dock = DockStyle.Fill;
            txtDescribe.Dock = DockStyle.Fill;
            txtBNCCode.Dock = DockStyle.Fill;
            btnSearch.Dock = DockStyle.Fill;
            txtScanCode.Dock = DockStyle.Fill;
            txtLCD.Dock = DockStyle.Fill;
            txtProcess.Dock = DockStyle.Fill;
            txtOp.Dock = DockStyle.Fill;
            txtTime.Dock = DockStyle.Fill;
            txtDeviceIP.Dock = DockStyle.Fill;
            txtProductOrder.Dock = DockStyle.Fill;
            txtFinishesModel.Dock = DockStyle.Fill;
            txtLineCode.Dock = DockStyle.Fill;
            txtNG.Dock = DockStyle.Fill;
            txtNGType.Dock = DockStyle.Fill;
            btnCancel.Dock = DockStyle.Fill;
            btnOK.Dock = DockStyle.Fill;
            cmbSPLName.Dock = DockStyle.Fill;

            txtProcessIP.Clear();
            txtCurrentOpCode.Clear();
            txtDescribe.Clear();
            txtDescribe.Tag = "";
            txtBNCCode.Clear();
            txtScanCode.Clear();
            txtLCD.Clear();
            txtProcess.Clear();
            txtOp.Clear();
            txtTime.Clear();
            txtDeviceIP.Clear();
            txtProductOrder.Clear();
            txtFinishesModel.Clear();
            txtFinishesModel.Tag = null;
            txtLineCode.Clear();
            txtNG.Clear();
            txtNGType.Clear();
            comProcess.Items.Clear();
            flpNG.Controls.Clear();
            cmbSPLName.Items.Clear();

            lblType.Tag = null;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNGReview_Load(object sender, EventArgs e)
        {
            try
            {
                // 最高解绑权限
                this.isRoot = BaseUI.UI_RIGHT.ContainsKey("02120502");

                // 当前程序版本
                this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (this.isRoot)
                {
                    this.Text = string.Format("重工解绑(强制解绑)   {0} （技术支持：深圳市鼎立特科技有限公司）", versionName);
                }
                else
                {
                    this.Text = string.Format("重工解绑   {0}   （技术支持：深圳市鼎立特科技有限公司）", versionName);
                }

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
        private void frmNGReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnCancel.Enabled)
            {
                e.Cancel = true;
            }
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
            this.flpNG.Refresh();
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

                // 登录人员账号
                txtCurrentOpCode.Text = BaseUI.UI_BOOLOGNAME;

                // 当前站点IP
                this.ipAddress = BaseUI.GetLocalIP();
                txtProcessIP.Text = this.ipAddress;

                // 初始化产线选择下拉框
                DataView dvLine = BaseUI.GetProcedureLine();
                cmbSPLName.DisplayMember = "SPL_Name";
                cmbSPLName.ValueMember = "SPL_JobCode";
                cmbSPLName.DataSource = dvLine;
                cmbSPLName.SelectedIndex = 0;

                // 初始化不良项数据表
                this.dtBNC = BaseUI.GetBNCTable();

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
        /// 初始化工序选择下拉框
        /// </summary>
        /// <param name="ProductionOrder">生产工单</param>
        /// <param name="LineCode">产线编码</param>
        private void BindProcessComboBox(string ProductionOrder, string LineCode)
        {
            try
            {
                //获取工序链
                this.dtProcessTable = BaseUI.GetProduceRouteData(ProductionOrder, LineCode);

                //Dictionary<string, string> dicProcess = new Dictionary<string, string>();
                //foreach (DataRow row in this.dtProcessTable.Rows)
                //{
                                        
                //}

                DataView dvProcess = new DataView(this.dtProcessTable);
                dvProcess.Sort = "PR_NoSeq DESC";
                comProcess.DisplayMember = "SGX_Name";  //工序名称
                comProcess.ValueMember = "SGX_JobCode"; //工序编码
                comProcess.DataSource = dvProcess;
            }
            catch (Exception exp)
            {
                LogHelper.Error("初始化工序链失败." + exp.Message, exp);
                ShowResult(NoteState.Error, "", "初始化工序链失败." + exp.Message);
            }
        }

        /// <summary>
        /// 初始化不良项
        /// </summary>
        private void InitBNC(string processCode, string type)
        {
            txtDescribe.Clear();
            txtDescribe.Tag = "";
            flpNG.Controls.Clear();
            txtBNCCode.Clear();

            // 添加不良项列表
            int width = 0;
            DataView dv = new DataView(this.dtBNC);
            dv.RowFilter = string.Format("TPD_ProcessCode = '{0}' AND BNC_Flag = '{1}'", processCode, type);
            dv.Sort = "TPD_BNCName ASC";

            foreach (DataRowView row in dv)
            {
                CheckBox chk = new CheckBox()
                {
                    Appearance = System.Windows.Forms.Appearance.Button,
                    AutoSize = true,
                    Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                    ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85))))),
                    Location = new System.Drawing.Point(3, 3),
                    Margin = new System.Windows.Forms.Padding(3, 10, 3, 10),
                    UseVisualStyleBackColor = true,
                    Name = row["TPD_BNCTid"].ToString(),    //不良项ID
                    Text = row["TPD_BNCName"].ToString(),   //不良项名称
                    Tag = row["TPD_BNCCode"],               //不良项编码
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.flpNG.Controls.Add(chk);
                chk.CheckStateChanged += this.BNC_CheckStateChanged;

                if (chk.Width > width)
                {
                    width = chk.Width;
                }
            }

            foreach (CheckBox chk in this.flpNG.Controls)
            {
                int height = chk.Height;
                chk.AutoSize = false;
                chk.Width = width;
                chk.Height = height;
            }

            this.Refresh();
            txtScanCode.SelectAll();
            txtScanCode.Focus();
        }

        /// <summary>
        /// 报废按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScrap_Click(object sender, EventArgs e)
        {
            Set_TFM_Type("报废");
        }
        /// <summary>
        /// 误判按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrJudge_Click(object sender, EventArgs e)
        {
            Set_TFM_Type("误判");
        }

        /// <summary>
        /// 设置异常类型
        /// </summary>
        /// <param name="Type"></param>
        private void Set_TFM_Type(string Type)
        {
            switch (Type)
            {
                case "报废":
                    lblType.Tag = "报废";
                    btnScrap.BackColor = Color.Red;
                    btnErrJudge.BackColor = Color.Transparent;

                    comProcess.Enabled = true;
                    comProcess.SelectedIndex = -1;
                    flpNG.Enabled = true;
                    txtBNCCode.Enabled = true;
                    btnSearch.Enabled = true;

                    if (txtProcess.Text != "")
                    {
                        InitBNC(txtProcess.Tag.ToString(), "报废");
                    }
                    break;
                case "误判":
                    lblType.Tag = "误判";
                    btnScrap.BackColor = Color.Transparent;
                    btnErrJudge.BackColor = Color.Red;

                    comProcess.SelectedIndex = -1;
                    txtDescribe.Clear();
                    txtDescribe.Tag = "";
                    flpNG.Controls.Clear();
                    txtBNCCode.Clear();
                    comProcess.Enabled = false;
                    flpNG.Enabled = false;
                    txtBNCCode.Enabled = true;
                    btnSearch.Enabled = true;

                    //// 根据不良项获取解绑工序(多个备选工序用|分隔)
                    //string reProcess = GetReworkProcess(lastEx.JudgeContent, txtProcess.Tag.ToString());
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(reProcess))
                    //    {
                    //        List<string> lstRw = reProcess.Split('|').ToList<string>();
                    //        foreach (string rwCode in lstRw)
                    //        {
                    //            try
                    //            {
                    //                comProcess.SelectedValue = rwCode;
                    //                if (comProcess.SelectedIndex == -1)
                    //                {
                    //                    continue;
                    //                }
                    //                else
                    //                {
                    //                    break;
                    //                }
                    //            }
                    //            catch (Exception)
                    //            {
                    //                continue;
                    //            }
                    //        }

                    //        if (comProcess.SelectedIndex != -1)
                    //        {
                    //            comProcess.Enabled = false;
                    //        }
                    //        else
                    //        {
                    //            comProcess.Enabled = true;
                    //            comProcess.SelectedIndex = -1;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        comProcess.Enabled = true;
                    //        comProcess.SelectedIndex = -1;
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    comProcess.Enabled = true;
                    //    comProcess.SelectedIndex = -1;
                    //}


                    break;
                default:
                    lblType.Tag = null;
                    btnScrap.BackColor = Color.Transparent;
                    btnErrJudge.BackColor = Color.Transparent;

                    comProcess.Enabled = true;
                    flpNG.Enabled = false;
                    txtBNCCode.Enabled = true;
                    btnSearch.Enabled = true;

                    comProcess.SelectedIndex = -1;
                    txtDescribe.Clear();
                    txtDescribe.Tag = "";
                    flpNG.Controls.Clear();
                    lvwProcess.Items.Clear();
                    txtBNCCode.Clear();

                    txtLCD.Clear();
                    txtProcess.Clear();
                    txtOp.Clear();
                    txtTime.Clear();
                    txtProductOrder.Clear();
                    txtFinishesModel.Clear();
                    txtFinishesModel.Tag = null;
                    txtLineCode.Clear();
                    txtNG.Clear();
                    txtNGType.Clear();
                    txtDeviceIP.Clear();
                    txtScanCode.Clear();
                    txtScanCode.Focus();
                    break;
            }
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
                    switch (state)
                    {
                        case NoteState.Success:
                            break;
                        case NoteState.OK:
                            // 弹框提示
                            this.successMsg = new SuccessMessage("操作成功", 2);
                            this.successMsg.ShowDialog(this);
                            Set_TFM_Type("");
                            break;

                        case NoteState.NG:
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 1000);
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
        /// <summary>
        /// 分析处理扫描数据
        /// </summary>
        /// <param name="DataString">扫描枪接收字符串</param>
        private void ScanDataHandler(string DataString)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    ScanDataHandlerCallback d = new ScanDataHandlerCallback(ScanDataHandler);
                    this.BeginInvoke(d, new object[] { DataString });
                }
                else
                {
                    btnOK.Enabled = false;
                    btnSearch.Enabled = false;
                    txtScanCode.Enabled = false;

                    // 第1步：站点基本信息检测
                    #region 第1步：站点基本信息检测
                    if (string.IsNullOrWhiteSpace(cmbSPLName.Text))
                    {
                        ShowResult(NoteState.Fail, "", "请选择当前产线");
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
                    txtScanCode.Text = DataString.Trim();  // 实际扫描数据

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
                    
                    if (ds[0].Length <= 9)
                    {
                        ShowResult(NoteState.NG, "", "默认编码长度至少为10位以上，请确认条码长度");
                        return;
                    }

                    string scanCode = AnalysisCode.Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");
                    txtScanCode.Text = scanCode;
                    #endregion 第2步：解析扫描数据

                    #region 查询玻璃信息
                    try
                    {
                        this.glassInfo = null;
                        txtDescribe.Clear();
                        txtDescribe.Tag = "";
                        txtBNCCode.Clear();
                        txtLCD.Clear();
                        txtProcess.Clear();
                        txtOp.Clear();
                        txtTime.Clear();
                        txtProductOrder.Clear();
                        txtFinishesModel.Clear();
                        txtFinishesModel.Tag = null;
                        txtLineCode.Clear();
                        txtNG.Clear();
                        txtNGType.Clear();
                        comProcess.DataSource = null;
                        flpNG.Controls.Clear();
                        lvwProcess.Items.Clear();

                        CallWithTimeout(GetProcessData1, this.searchTimeout);
                        if (this.glassInfo == null)
                        {
                            ShowResult(NoteState.NG, DataString, "内部查询玻璃绑定信息失败,如多次出现此错误，请关掉程序重新打开");
                            return;
                        }
                        if (this.glassInfo.ProductInfo == null || string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode))
                        {
                            ShowResult(NoteState.NG, DataString, "未能查询到玻璃信息.");
                            return;
                        }
                    }
                    catch (TimeoutException tex)
                    {
                        GetHBaseDataClass.Instance.Reconnect();
                        ShowResult(NoteState.Error, DataString, "HBase数据库连接超时." + tex.Message.ToString());
                        return;
                    }
                    catch (Exception exp)
                    {
                        ShowResult(NoteState.Error, DataString, "查询玻璃绑定信息失败." + exp.Message);
                        return;
                    }

                    #endregion 查询玻璃信息

                    #region 查询玻璃不良信息

                    // 旧的不良信息查询方式，从SQL Server中查询不良信息
                    Utils.Model.TPL_ProcessFail_Main ProcessFailInfo = null;
                    try
                    {
                        ProcessFailInfo = BaseUI.GetProcessFail_Main(this.glassInfo.ProductInfo.LCDCode);
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("SQL查询玻璃不良申报信息失败.", exp);
                        //ShowResult(NoteState.Error, "", "查询玻璃不良申报信息失败！" + exp.Message);
                        //return;
                    }

                    // HBase无不良信息
                    if ((this.glassInfo.Exception == null || this.glassInfo.Exception.Count == 0))
                    {
                        // 如果未查询到不良信息，但是data_06的玻璃状态不是良品，则回写data_06
                        if (this.glassInfo.GlassState.LCDState != "良品")
                        {
                            //更新lcdState
                            this.glassInfo.GlassState.LCDState = "良品";
                            //更新Data_06
                            bool UpdateSuccess = GetHBaseDataClass.Instance.UpdateData06(this.glassInfo.ProductInfo.LCDCode, this.glassInfo.GlassState);
                        }
                        if (!isRoot)
                        {
                            ShowResult(NoteState.NG, "", "当前玻璃未申报不良，无法重工解绑");
                            return;
                        }
                    }
                    //// HBase和SQL Server均无不良信息
                    //if ((this.glassInfo.Exception == null || this.glassInfo.Exception.Count == 0) && ProcessFailInfo == null)
                    //{
                    //    // 如果未查询到不良信息，但是data_06的玻璃状态不是良品，则回写data_06
                    //    if (this.glassInfo.GlassState.LCDState != "良品")
                    //    {
                    //        //更新lcdState
                    //        this.glassInfo.GlassState.LCDState = "良品";
                    //        //更新Data_06
                    //        bool UpdateSuccess = GetHBaseDataClass.Instance.UpdateData06(this.glassInfo.ProductInfo.LCDCode, this.glassInfo.GlassState);
                    //    }
                    //    if (!isRoot)
                    //    {
                    //        ShowResult(NoteState.NG, "", "当前玻璃未申报不良，无法重工解绑");
                    //        return;
                    //    }
                    //}

                    //// HBase未申报不良，但是SQL Server有不良信息
                    //if ((this.glassInfo.Exception == null || this.glassInfo.Exception.Count == 0) && ProcessFailInfo != null)
                    //{
                    //    this.glassInfo.Exception = new Dictionary<string, ExceptionInfo>();
                    //    ExceptionInfo exInfo = new ExceptionInfo()
                    //    {
                    //        ExceptionKey = string.Format("$G{0}$T{1}$P{2}", ProcessFailInfo.TFM_GlassCode, ((DateTime)ProcessFailInfo.TFM_AddDate).ToString("yyyyMMddHHmmss"), ProcessFailInfo.TFM_ProcessCode),
                    //        GlassCode = ProcessFailInfo.TFM_GlassCode,
                    //        ProductionOrder = ProcessFailInfo.TFM_ProductOrder,
                    //        FinishesCode = this.glassInfo.ProductInfo.FinishesCode,
                    //        FinishesModel = this.glassInfo.ProductInfo.FinishesModel,
                    //        ProductionLineCode = ProcessFailInfo.TFM_LineCode,
                    //        ProcessCode = ProcessFailInfo.TFM_ProcessCode,
                    //        processName = BaseUI.GetProcessName(ProcessFailInfo.TFM_ProcessCode),
                    //        ScanNumber = BaseUI.GetOpName(ProcessFailInfo.TFM_AddPid == null ? null : ProcessFailInfo.TFM_AddPid.ToString()),
                    //        ScanTime = ((DateTime)ProcessFailInfo.TFM_AddDate).ToString("yyyy-MM-dd HH:mm:ss"),
                    //        DeviceIp = ProcessFailInfo.TFM_DeviceIP,
                    //        ExceptionContent = ProcessFailInfo.TFM_Describe,
                    //    };

                    //    // 判断是否已复判
                    //    if (ProcessFailInfo.TFM_Status == "正操作")
                    //    {
                    //        exInfo.ExceptionState = "待复判";
                    //    }
                    //    else
                    //    {
                    //        exInfo.JudgeNumber = BaseUI.GetOpName(ProcessFailInfo.TFM_CheckPid == null ? null : ProcessFailInfo.TFM_CheckPid.ToString());
                    //        exInfo.JudgeTime = ((DateTime)ProcessFailInfo.TFM_CheckDate).ToString("yyyy-MM-dd HH:mm:ss");
                    //        exInfo.JudgeIp = "";
                    //        exInfo.JudgeContent = ProcessFailInfo.TFM_Describe;

                    //        // 申报/复判类型
                    //        if (ProcessFailInfo.TFM_Type == "" || ProcessFailInfo.TFM_Type == "误判")
                    //        {
                    //            exInfo.ExceptionState = "复判良品";
                    //        }
                    //        else
                    //        {
                    //            exInfo.ExceptionState = "复判" + ProcessFailInfo.TFM_Type;
                    //        }
                    //    }

                    //    this.glassInfo.Exception.Add(exInfo.ExceptionKey, exInfo);
                    //}

                    // 获取最近时间的不良记录
                    this.glassInfo.LastExceptionKey = null;
                    ExceptionInfo lastEx = null;
                    if (this.glassInfo != null && this.glassInfo.Exception != null)
                    {
                        foreach (string key in this.glassInfo.Exception.Keys)
                        {
                            if (this.glassInfo.LastExceptionKey == null)
                            {
                                this.glassInfo.LastExceptionKey = key;
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(this.glassInfo.Exception[key].ScanTime) && !string.IsNullOrWhiteSpace(this.glassInfo.Exception[this.glassInfo.LastExceptionKey].ScanTime) && DateTime.Compare(DateTime.Parse(this.glassInfo.Exception[key].ScanTime), DateTime.Parse(this.glassInfo.Exception[this.glassInfo.LastExceptionKey].ScanTime)) > 0)
                                {
                                    this.glassInfo.LastExceptionKey = key;
                                }
                            }
                        }
                        // 当前不良信息
                        lastEx = this.glassInfo.Exception[glassInfo.LastExceptionKey];
                    }
                    //// HBase和SQL Server都有不良信息，但是HBase未复判，SQL Server已复判
                    //if (lastEx != null && ProcessFailInfo != null && lastEx.ExceptionState == "待复判" && ProcessFailInfo.TFM_Status != "正操作")
                    //{
                    //    // 确认是否同一次提报的不良
                    //    if (lastEx.ProcessCode == ProcessFailInfo.TFM_ProcessCode && lastEx.DeviceIp == ProcessFailInfo.TFM_DeviceIP)
                    //    {
                    //        // 复判时间晚于HBase不良申报时间
                    //        if (DateTime.Compare(DateTime.Parse(lastEx.ScanTime), (DateTime)ProcessFailInfo.TFM_CheckDate) < 0)
                    //        {
                    //            lastEx.JudgeNumber = BaseUI.GetOpName(ProcessFailInfo.TFM_CheckPid == null ? null : ProcessFailInfo.TFM_CheckPid.ToString());
                    //            lastEx.JudgeTime = ((DateTime)ProcessFailInfo.TFM_CheckDate).ToString("yyyy-MM-dd HH:mm:ss");
                    //            lastEx.JudgeIp = "";
                    //            lastEx.JudgeContent = ProcessFailInfo.TFM_Describe;

                    //            // 申报/复判类型
                    //            if (ProcessFailInfo.TFM_Type == "" || ProcessFailInfo.TFM_Type == "误判")
                    //            {
                    //                lastEx.ExceptionState = "复判良品";
                    //            }
                    //            else
                    //            {
                    //                lastEx.ExceptionState = "复判" + ProcessFailInfo.TFM_Type;
                    //            }
                    //            // 更新玻璃不良信息
                    //            this.glassInfo.Exception[glassInfo.LastExceptionKey] = lastEx;
                    //        }
                    //    }
                    //}
                    // 显示玻璃不良信息
                    ShowGlassInfo(this.glassInfo);

                    // 混线生产
                    if (lastEx != null && lastEx.ProductionLineCode != cmbSPLName.SelectedValue.ToString() && !isRoot)
                    {
                        //DataTable dtPro = BaseUI.GetOrderInfo(lastEx.ProductionOrder);
                        //string mixLine = "0";
                        //string lineName = "";
                        //if (dtPro != null && dtPro.Rows.Count > 0)
                        //{
                        //    mixLine = (dtPro.Rows[0]["SPOM_MixedLine"] ?? "0").ToString();
                        //    lineName = (dtPro.Rows[0]["SPL_Name"] ?? "").ToString();
                        //}

                        //// 工单不允许混线
                        //if (mixLine == "0")
                        //{
                        //    ShowResult(NoteState.NG, "", "该玻璃产线与当前产线不一致。");
                        //    return;
                        //}
                        //else
                        //{
                        //    List<string> lstMixLine = BaseUI.GetMixLine(lastEx.ProductionOrder);
                        //    if (lstMixLine.Count == 0)
                        //    {
                        //        ShowResult(NoteState.NG, "", string.Format("该玻璃工单未配置可混产线。", lastEx.ProductionLineName));
                        //        return;
                        //    }
                        //    else if (!lstMixLine.Contains(cmbSPLName.SelectedValue.ToString()))
                        //    {
                        //        ShowResult(NoteState.NG, "", string.Format("该玻璃产线为{0},禁止在当前产线重工！", lastEx.ProductionLineName));
                        //        return;
                        //    }
                        //}
                        ////ShowResult(NoteState.NG, "", "该玻璃不良复判产线与当前产线不一致。");
                        ////return;
                        List<string> lstMixLine = BaseUI.GetMixLine(lastEx.FinishesCode);
                        if (lstMixLine.Count == 0)
                        {
                            ShowResult(NoteState.NG, "", "该玻璃产线与当前产线不一致。");
                            return;
                        }
                        else if (!lstMixLine.Contains(cmbSPLName.SelectedValue.ToString()))
                        {
                            ShowResult(NoteState.NG, "", string.Format("该玻璃产线为{0},禁止在当前产线重工！", lastEx.ProductionLineName));
                            return;
                        }
                    }
                    // 检查是否已包装
                    string sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Sub WITH (NOLOCK) WHERE IBS_GlassCode='{0}'", this.glassInfo.ProductInfo.LCDCode);
                    DataView dv = conn.ExecuteDataView(sql, "Base");
                    if (dv.Count > 0)
                    {
                        ShowResult(NoteState.NG, "", "当前玻璃已包装，无法重工解绑");
                        return;
                    }
                    sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Small_Sub WITH (NOLOCK) WHERE ISS_GlassCode='{0}'", this.glassInfo.ProductInfo.LCDCode);
                    dv = conn.ExecuteDataView(sql, "Base");
                    if (dv.Count > 0)
                    {
                        ShowResult(NoteState.NG, "", "当前玻璃已包装，无法重工解绑");
                        return;
                    }

                    #region 不良信息
                    if (isRoot)
                    {
                        Set_TFM_Type("误判");
                        btnErrJudge.Enabled = true;
                        comProcess.Enabled = true;
                        comProcess.SelectedIndex = -1;
                        btnOK.Enabled = true;
                    }
                    else
                    {
                        if (lastEx.ExceptionState == "待复判")
                        {
                            ShowResult(NoteState.NG, "", "当前玻璃尚未不良复判，无法重工解绑");
                            return;
                        }
                        //else if (lastEx.ExceptionState == "复判良品" || lastEx.ExceptionState == "重工良品")
                        else if (lastEx.ExceptionState == "复判良品")//2020-04-10 重工良品可再次更新为重工报废
                        {
                            ShowResult(NoteState.NG, "", "当前玻璃为" + lastEx.ExceptionState + "，尚未申报不良");
                            return;
                        }
                        else if (lastEx.ExceptionState == "重工良品")
                        {
                            this.warnMsg = new WarnMessage(lastEx.ExceptionState + "！\r\n是否改判？", MessageBoxButtons.OKCancel, 5, "Cancel");
                            if (this.warnMsg.ShowDialog(this) == DialogResult.Cancel)
                            {
                                return;
                            }
                            else
                            {
                                Set_TFM_Type("报废");
                                btnErrJudge.Enabled = true;
                                comProcess.Enabled = true;
                                comProcess.SelectedIndex = -1;
                                btnOK.Enabled = true;
                            }
                        }
                        else if(lastEx.ExceptionState == "重工报废")
                        {
                            this.warnMsg = new WarnMessage(lastEx.ExceptionState + "！\r\n是否改判？", MessageBoxButtons.OKCancel, 5, "Cancel");
                            if (this.warnMsg.ShowDialog(this) == DialogResult.Cancel)
                            {
                                return;
                            }
                            else
                            {
                                Set_TFM_Type("误判");
                                btnErrJudge.Enabled = true;
                                comProcess.Enabled = false;
                                comProcess.SelectedIndex = -1;
                                btnOK.Enabled = true;
                            }
                        }
                        else if (lastEx.ExceptionState == "复判报废")
                        {
                            Set_TFM_Type("报废");
                            btnErrJudge.Enabled = false;
                            comProcess.Enabled = true;
                            comProcess.SelectedIndex = -1;
                            btnOK.Enabled = true;
                        }
                        else
                        {
                            Set_TFM_Type("误判");
                            btnErrJudge.Enabled = true;
                            comProcess.Enabled = true;
                            // 根据不良项获取解绑工序(多个备选工序用|分隔)
                            string reProcess = GetReworkProcess(lastEx.JudgeContent, txtProcess.Tag.ToString());
                            try
                            {
                                if (!string.IsNullOrEmpty(reProcess))
                                {
                                    List<string> lstRw = reProcess.Split('|').ToList<string>();
                                    foreach (string rwCode in lstRw)
                                    {
                                        try
                                        {
                                            comProcess.SelectedValue = rwCode;
                                            if (comProcess.SelectedIndex == -1)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }
                                    }

                                    if (comProcess.SelectedIndex != -1)
                                    {
                                        comProcess.Enabled = false;
                                    }
                                    else
                                    {
                                        comProcess.Enabled = true;
                                        comProcess.SelectedIndex = -1;
                                    }
                                }
                                else
                                {
                                    comProcess.Enabled = true;
                                    comProcess.SelectedIndex = -1;
                                }
                            }
                            catch (Exception)
                            {
                                comProcess.Enabled = true;
                                comProcess.SelectedIndex = -1;
                            }
                            finally
                            {
                                btnOK.Enabled = true;
                            }
                        }
                    }
                    #endregion 不良信息

                    #endregion 查询玻璃是否已提交不良信息
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
                //btnOK.Enabled = true;
                btnSearch.Enabled = true;
                txtScanCode.Enabled = true;
                txtScanCode.Focus();
                txtScanCode.SelectAll();
            }
        }

        /// <summary>
        /// 根据复判不良选项，查找对应的重投工序
        /// </summary>
        /// <param name="JudgeContent">复判不良选项</param>
        /// <returns></returns>
        private string GetReworkProcess(string JudgeContent, string ProcessCode)
        {
            string processcode = "";
            string[] bncName = JudgeContent.Split('|');
            string bnc = string.Join("','", bncName);

            DataView dv = new DataView(this.dtBNC);
            dv.RowFilter = string.Format("TPD_ProcessCode = '{0}' AND TPD_BNCName IN ('{1}')", ProcessCode, bnc);
            dv.Sort = "BNC_RepairSGX ASC";

            //List<string> lstProcess = new List<string>();
            //foreach (DataRowView item in dv)
            //{
            //    lstProcess.Add(item["BNC_RepairSGX"].ToString().PadLeft(3, '0'));
            //}

            //foreach (DataRow row in this.dtProcessTable.Rows)
            //{
            //    processcode = row["SGX_JobCode"].ToString();
            //    if(lstProcess.Contains(processcode))
            //    {
            //        return processcode;
            //    }
            //}
            //return null;
            if(dv.Count > 0)
            {
                processcode = dv[0]["BNC_RepairNote"].ToString();
            }
            return processcode;
        }

        /// <summary>
        /// 显示玻璃不良信息
        /// </summary>
        /// <param name="glassInfo"></param>
        private void ShowGlassInfo(GlassInfo glassInfo)
        {
            if (glassInfo == null) return;
            // 玻璃码
            txtLCD.Text = glassInfo.ProductInfo.LCDCode;
            // 工单编码
            txtProductOrder.Text = glassInfo.ProductInfo.ProductionOrder;
            // 产线编码
            txtLineCode.Text = glassInfo.ProductInfo.ProductionLineCode;
            // 成品型号
            txtFinishesModel.Text = glassInfo.ProductInfo.FinishesModel;
            txtFinishesModel.Tag = glassInfo.ProductInfo.FinishesCode;

            if (!string.IsNullOrEmpty(glassInfo.LastExceptionKey))
            {
                // 填充数据
                ExceptionInfo exInfo = glassInfo.Exception[glassInfo.LastExceptionKey];

                // 提报站点工序名
                txtProcess.Text = exInfo.processName;
                txtProcess.Tag = exInfo.ProcessCode;
                
                // 申报/复判类型
                txtNGType.Text = exInfo.ExceptionState;
                // 判断是否已复判
                if (exInfo.ExceptionState == "待复判")
                {
                    lblOP.Text = "申报人员:";
                    lblTime.Text = "申报时间:";
                    lblNG.Text = "申报不良:";
                    lblNGType.Text = "申报状态:";
                    lblDeviceIP.Text = "申 报 I P:";
                    txtOp.Text = exInfo.ScanNumber;
                    txtTime.Text = exInfo.ScanTime;
                    // 异常描述
                    txtNG.Text = exInfo.ExceptionContent;
                    txtDeviceIP.Text = exInfo.DeviceIp;
                }
                else if (exInfo.ExceptionState.Contains("复判"))
                {
                    lblOP.Text = "复判人员:";
                    lblTime.Text = "复判时间:";
                    lblNG.Text = "复判不良:";
                    lblNGType.Text = "复判状态:";
                    lblDeviceIP.Text = "复 判 I P:";
                    txtOp.Text = exInfo.JudgeNumber;
                    txtTime.Text = exInfo.JudgeTime;
                    // 异常描述
                    txtNG.Text = exInfo.JudgeContent;
                    txtDeviceIP.Text = exInfo.JudgeIp;
                }
                else
                {
                    lblOP.Text = "重工人员:";
                    lblTime.Text = "重工时间:";
                    lblNG.Text = "重工不良:";
                    lblNGType.Text = "重工状态:";
                    lblDeviceIP.Text = "重 工 I P:";
                    txtOp.Text = exInfo.ReworkNumber;
                    txtTime.Text = exInfo.ReworkTime;
                    // 异常描述
                    txtNG.Text = exInfo.ReworkContent;
                    txtDeviceIP.Text = exInfo.ReworkIp;
                }
            }
            //// 获取工单产线对应的工序链，并绑定重投工序下拉框
            //BindProcessComboBox(glassInfo.ProductInfo.ProductionOrder, glassInfo.ProductInfo.ProductionLineCode);

            // 获取工序过站信息
            string processLog = glassInfo.GlassState.LogCode;
            Dictionary<string, string> dicProcessLog = GetProcessLogData(processLog);
            if (dicProcessLog.Count > 0)
            {
                int i = dicProcessLog.Count;
                foreach (string key in dicProcessLog.Keys)
                {
                    ListViewItem item = lvwProcess.Items.Add((i--).ToString());
                    item.SubItems.Add(dicProcessLog[key]);
                }

                //绑定数据
                comProcess.DataSource = new BindingSource(dicProcessLog, null);
                comProcess.ValueMember = "Key";//文本对应的值
                comProcess.DisplayMember = "Value";//显示的文本
            }
            //DataView dvProcess = new DataView(this.dtProcessTable);
            //dvProcess.Sort = "PR_NoSeq DESC";
            //comProcess.DisplayMember = "SGX_Name";  //工序名称
            //comProcess.ValueMember = "SGX_JobCode"; //工序编码
            //comProcess.DataSource = dvProcess;
        }

        /// <summary>
        /// 获取已过工序列表
        /// </summary>
        /// <param name="LogCode">已过工序编码</param>
        private Dictionary<string, string> GetProcessLogData(string LogCode)
        {
            if (string.IsNullOrEmpty(LogCode)) return null;
            //List<string> lst = new List<string>();
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            string[] code = LogCode.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            if (code.Length > 0)
            {
                LogCode = string.Join("','", code);
                string sql = string.Format("SELECT SGX_JobCode, SGX_Name FROM Store_GongXu_Main WHERE SGX_JobCode IN('{0}')", LogCode);
                DataTable dt = conn.ExecuteDataTable(sql, "Base");
                Dictionary<string, string> dicProcess = dt.Rows.Cast<DataRow>().ToDictionary(x => x["SGX_JobCode"].ToString(), x => x["SGX_Name"].ToString());
                for (int i = code.Length - 1; i >= 0; i--)
                {
                    dicResult.Add(code[i], dicProcess[code[i]]);
                }
            }
            return dicResult;
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
        /// 不良项选择状态变化时，重新生成不良描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BNC_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox item = sender as CheckBox;
            if (item.Checked)
            {
                item.BackColor = Color.Red;
            }
            else
            {
                item.BackColor = Color.Transparent;
            }
            string desc = "";
            string id = "";
            txtDescribe.Clear();
            txtDescribe.Tag = "";
            foreach (CheckBox chk in this.flpNG.Controls)
            {
                if (chk.CheckState == CheckState.Checked)
                {
                    desc += chk.Text + "|";
                    id += chk.Name + "|";
                }
            }
            txtDescribe.Text = desc.TrimEnd('|');
            txtDescribe.Tag = id.TrimEnd('|');
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtLCD.Text == "")
            {
                ShowResult(NoteState.NG, "", "请先扫描编码，\r\n获取不良信息！");
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }
            if (lblType.Tag == null)
            {
                ShowResult(NoteState.NG, "", "请先选择重工结果");
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }
            if(comProcess.SelectedIndex == -1 && txtNGType.Text.Contains("复判"))
            {
                ShowResult(NoteState.NG, "", "请先选择解绑工序");
                comProcess.Focus();
                return;
            }
 
            #region 混线生产检测
            // 与当前产线不一致，判断是否允许混线
            if (txtLineCode.Text != "" && txtLineCode.Text.Length >= 2 && txtLineCode.Text != cmbSPLName.SelectedValue.ToString() && txtFinishesModel.Tag != null)
            {
                List<string> lstMixLine = BaseUI.GetMixLine(txtFinishesModel.Tag.ToString());
                if (lstMixLine.Count == 0)
                {
                    ShowResult(NoteState.NG, "", "玻璃生产产线为" + txtLineCode.Text + "，与当前产线不一致。");
                    txtScanCode.SelectAll();
                    txtScanCode.Focus();
                    return;
                }
                else if (!lstMixLine.Contains(cmbSPLName.SelectedValue.ToString()))
                {
                    ShowResult(NoteState.NG, "", string.Format("该玻璃产线为{0},禁止在当前产线重工！", txtLineCode.Text));
                    txtScanCode.SelectAll();
                    txtScanCode.Focus();
                    return;
                }
            }
            #endregion 混线生产检测

            if (lblType.Tag.ToString() != "误判" && (txtDescribe.Tag == null || txtDescribe.Tag.ToString() == "" ))
            {
                ShowResult(NoteState.NG, "", "请判定不良项！");
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }

            //if (lblType.Tag != null && lblType.Tag.ToString() == "报废" && comProcess.SelectedValue.ToString() != "018" && comProcess.SelectedValue.ToString() != "024")
            //{
            //    ShowResult(NoteState.NG, "", "报废品只允许解绑TP贴合或背光贴合！");
            //    comProcess.Focus();
            //    return;
            //}

            string state = "重工良品";
            if (lblType.Tag != null && lblType.Tag.ToString() == "报废")
            {
                state = "重工报废";
            }

            btnOK.Enabled = false;
            btnCancel.Enabled = false;

            string reworkTime = BaseUI.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");
            string reworkProcess = (comProcess.SelectedValue ?? "").ToString();
            // 数据提交后台处理
            bgwWriteData.RunWorkerAsync(new object[] { this.glassInfo, state, txtCurrentOpCode.Text, txtDescribe.Text, reworkTime, txtProcessIP.Text, reworkProcess });
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

                string judgeRst = GetHBaseDataClass.Instance.ExceptionRework(glassInfo, state, op, ng, time, ip, process, this.isRoot);
                if (judgeRst != "RowKeyCode不存在" && judgeRst != "重工成功")
                {
                    e.Result = new Exception(judgeRst);
                    return;
                }
                else
                {
                    e.Result = null;
                }
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
            btnOK.Enabled = true;
            btnCancel.Enabled = true;

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
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Set_TFM_Type("");
        }

        /// <summary>
        /// 不良编码按下回车键，查找不良项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBNCCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BNCCodeSearch(txtBNCCode.Text.Trim());
            }
        }
        /// <summary>
        /// 查找不良项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BNCCodeSearch(txtBNCCode.Text.Trim());
        }

        /// <summary>
        /// 查找指定的不良项编码
        /// </summary>
        /// <param name="code"></param>
        private void BNCCodeSearch(string code)
        {
            foreach (CheckBox chk in flpNG.Controls)
            {
                if (code == "")
                {
                    chk.Visible = true;
                }
                else
                {
                    if (chk.Tag.ToString().Contains(code.ToUpper()))
                    {
                        chk.Visible = true;
                    }
                    else
                    {
                        chk.Visible = false;
                    }
                }
            }
        }

        private void comProcess_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(comProcess.SelectedValue.ToString() == "005")
            {
                ShowResult(NoteState.NG, "", "禁止解绑LCD投入,请重新选择!");
                comProcess.SelectedIndex = -1;
                return;
            }
        }
    }
}
