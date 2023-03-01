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
using Utils.HBaseClass;
using Utils.Model;

namespace MDC
{
    public partial class frmOTP : Form
    {
        #region 私有字段
        /// <summary>
        /// 数据上传客户端
        /// </summary>
        private Client client;
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
        /// 编码解析字符串
        /// </summary>
        private string AnalysisCode;
        ///// <summary>
        ///// 扫描码查询的已过工序信息表
        ///// </summary>
        //private DataTable processData;
        /// <summary>
        /// 玻璃信息
        /// </summary>
        private GlassInfo glassInfo;
        /// <summary>
        /// 玻璃信息列表
        /// </summary>
        private List<GlassInfo> lstGlassInfo = new List<GlassInfo>() { null, null, null };
        /// <summary>
        /// 待绑定条码信息
        /// </summary>
        private ProductInfo bindInfo;
        ///// <summary>
        ///// 对应码查询的已过工序信息表
        ///// </summary>
        //private DataTable processData2;
        ///// <summary>
        ///// 当前站点玻璃过站信息
        ///// </summary>
        //private Dictionary<string, string> dicGlassInfo;
        /// <summary>
        /// Code1扫码类型
        /// </summary>
        private string scanType1;
        ///// <summary>
        ///// Code2扫码类型
        ///// </summary>
        ////private string scanType2;
        /// <summary>
        /// 本机IP
        /// </summary>
        private string ipAddress = "";
        ///// <summary>
        ///// 成品码绑定的工控机IP地址
        ///// </summary>
        //private string ipQrBinding = "";
        /// <summary>
        /// 当前站点的类别（关键、过站、补扫）
        /// </summary>
        private SiteType siteType;
        /// <summary>
        /// 当前工序是否关键工序
        /// </summary>
        private bool isBind;
        ///// <summary>
        ///// 是否允许混线生产
        ///// </summary>
        //private bool isMixedLine = false;
        /// <summary>
        /// 工单可混产线列表
        /// </summary>
        private List<string> mixLineList = new List<string>();
        ///// <summary>
        ///// TP是否有码
        ///// </summary>
        //private bool isTP;
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;        //失败提示框
        /// <summary>
        /// 成功提示框
        /// </summary>
        private SuccessMessage successMsg;  //成功提示框
        private bool IsScan = true;//是否扫描
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
        /// <summary>
        /// 自动重连定时器
        /// </summary>
        private System.Timers.Timer tmrAutoReconnect;
        /// <summary>
        /// 最大日志行数
        /// </summary>
        private int maxLogCount;
        /// <summary>
        /// 是否记录程序运行日志
        /// </summary>
        private bool isWriteLog;
        /// <summary>
        /// 玻璃信息查询超时时间
        /// </summary>
        private int searchTimeout;
        /// <summary>
        /// 最近若干片玻璃的绑定结果记录（字符串的每一位代表一片玻璃的绑定结果，0：OK，1：NG，2：混料）
        /// </summary>
        private string ScanNote = "";
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
        private int Error_Count = 0;
        /// <summary>
        /// 最近50片扫描记录示意图块宽度
        /// </summary>
        private int lblWidth = 4;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 上次扫描的Code1
        /// </summary>
        private string LastCode1 = "";
        ///// <summary>
        ///// 华为客户料号（逗号分隔）
        ///// </summary>
        //private string HWCusNumber = "";
        /// <summary>
        /// 标当前操作是否为返检
        /// </summary>
        private bool isRepeat = false;
        /// <summary>
        /// 当前工序是否允许返检
        /// </summary>
        private bool processRepeatEnabled = false;
        /// <summary>
        /// 弹框提醒类型（bit0:重工良品，bit1:复判良品，bit2:点线类不良）
        /// </summary>
        private string warnType;
        /// <summary>
        /// 相邻工序过站间隔时间
        /// </summary>
        private int processInterval = 0;
        /// <summary>
        /// 对接治具类型
        /// </summary>
        private int fixtureType = 0;
        /// <summary>
        /// 扫描枪串口
        /// </summary>
        private string scannerPort = "COM1";
        /// <summary>
        /// 治具列表 串口号，治具二维码
        /// </summary>
        private Dictionary<string, string> fixtureList = new Dictionary<string, string>();
        /// <summary>
        /// 测试治具串口列表
        /// </summary>
        private Dictionary<string, SerialPortUtil> fixturePortList = new Dictionary<string, SerialPortUtil>();
        /// <summary>
        /// 串口号列表
        /// </summary>
        private List<string> lstPortName;
        /// <summary>
        /// 治具编码列表
        /// </summary>
        private List<string> lstFixtureCode;
        /// <summary>
        /// 串口类列表
        /// </summary>
        private List<SerialPortUtil> lstFixturePort = new List<SerialPortUtil>();
        /// <summary>
        /// 治具名标签列表
        /// </summary>
        private List<Label> lblPort = new List<Label>();
        /// <summary>
        /// 治具编码文本框列表
        /// </summary>
        private List<TextBox> txtFixture = new List<TextBox>();
        /// <summary>
        /// 编码名称标签列表
        /// </summary>
        private List<Label> lblCode = new List<Label>();
        /// <summary>
        /// 扫描编码文本框列表
        /// </summary>
        private List<TextBox> txtCode = new List<TextBox>();
        ///// <summary>
        ///// 玻璃码文本框列表
        ///// </summary>
        //private List<TextBox> txtGlass = new List<TextBox>();
        /// <summary>
        /// 玻璃码列表
        /// </summary>
        private List<string> lstGlass = new List<string>() { "", "", "" };
        /// <summary>
        /// 客户码列表
        /// </summary>
        private List<string> lstQrCode = new List<string>() { "", "", ""};
        /// <summary>
        /// 客户码文本框列表
        /// </summary>
        private List<TextBox> txtQrCode = new List<TextBox>();
        /// <summary>
        /// 状态信息标签列表
        /// </summary>
        private List<Label> lblInfo = new List<Label>();

        ///// <summary>
        ///// 治具通信后台处理
        ///// </summary>
        //private BackgroundWorker bgkFixture;
        /// <summary>
        /// 治具通信后台处理组件列表
        /// </summary>
        private List<BackgroundWorker> lstBgk = new List<BackgroundWorker>();
        /// <summary>
        /// 测试过程提示信息
        /// </summary>
        private enum ProgressInfo
        {
            待扫码 = 0,
            扫治具 = 1,
            扫玻璃 = 2,
            测试中 = 3,
            PASS = 4,
            FAIL = 5,
            错误 = 6,
            超时 = 7,
            已完成 = 8
        }

        #endregion 私有字段

        #region 构造函数
        /// <summary>
        /// frmMain的构造函数
        /// </summary>
        public frmOTP()
        {
            InitializeComponent();

            txtSPLName.Dock = DockStyle.Fill;
            txtIPAddress.Dock = DockStyle.Fill;
            txtPort.Dock = DockStyle.Fill;
            txtProcessIP.Dock = DockStyle.Fill;
            txtOpCode.Dock = DockStyle.Fill;
            txtOrder.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;
            txtScanCode.Dock = DockStyle.Fill;
            txtQrCode1.Dock = DockStyle.Fill;
            txtRule1.Dock = DockStyle.Fill;
            txtCode1.Dock = DockStyle.Fill;
            txtCode2.Dock = DockStyle.Fill;
            txtCode3.Dock = DockStyle.Fill;
            txtFixture1.Dock = DockStyle.Fill;
            txtFixture2.Dock = DockStyle.Fill;
            txtFixture3.Dock = DockStyle.Fill;
            txtQrCode1.Dock = DockStyle.Fill;
            txtQrCode2.Dock = DockStyle.Fill;
            txtQrCode3.Dock = DockStyle.Fill;
            lblInfo1.Dock = DockStyle.Fill;
            lblInfo2.Dock = DockStyle.Fill;
            lblInfo3.Dock = DockStyle.Fill;

            txtDigit1.Dock = DockStyle.Fill;
            comProcess.Dock = DockStyle.Fill;
            txtLCDCount.Dock = DockStyle.Fill;
            txtOKCount.Dock = DockStyle.Fill;
            txtNGCount.Dock = DockStyle.Fill;
            txtErrorCount.Dock = DockStyle.Fill;
            //txtDescribe.Dock = DockStyle.Fill;
            flpScanNote.Dock = DockStyle.Fill;
            txtFixtureCode.Dock = DockStyle.Fill;
            cmbSerialPort.Dock = DockStyle.Fill;

            txtSPLName.Clear();
            txtIPAddress.Clear();
            txtPort.Clear();
            txtProcessIP.Clear();
            txtOpCode.Clear();
            txtEarlyProcess.Clear();
            txtModel.Clear();
            txtOrder.Clear();
            txtScanCode.Clear();
            txtQrCode1.Clear();
            txtQrCode2.Clear();
            txtQrCode3.Clear();
            txtRule1.Clear();
            txtCode1.Clear();
            txtCode2.Clear();
            txtCode3.Clear();
            lblInfo1.Text = "扫治具";
            lblInfo2.Text = "扫治具";
            lblInfo3.Text = "扫治具";
            txtDigit1.Clear();
            txtProcessNumber.Clear();
            txtProcessCode.Clear();
            txtFixtureCode.Clear();
            cmbSerialPort.Items.Clear();
            txtLCDCount.Text = "0";
            txtOKCount.Text = "0";
            txtNGCount.Text = "0";
            txtErrorCount.Text = "0";
            //txtDescribe.Clear();
            lvwNote.LostFocus += new EventHandler(lvwNote_LostFocus);

            comProcess.Enabled = false;
            cmbSerialPort.Enabled = false;
            txtFixtureCode.ReadOnly = true;

            this.tmrAutoReconnect = new System.Timers.Timer(3000);
            this.tmrAutoReconnect.AutoReset = false;
            this.tmrAutoReconnect.Elapsed += new System.Timers.ElapsedEventHandler(tmrAutoReconnect_Elapsed);

            this.lblPort = new List<Label>() { lblPort1, lblPort2, lblPort3 };
            this.txtFixture = new List<TextBox>() { txtFixture1, txtFixture2, txtFixture3 };
            this.lblCode = new List<Label>() { lblCode1, lblCode2, lblCode3 };
            this.txtCode = new List<TextBox>() { txtCode1, txtCode2, txtCode3 };
            this.txtQrCode = new List<TextBox>() { txtQrCode1, txtQrCode2, txtQrCode3 };
            this.lblInfo = new List<Label>() { lblInfo1, lblInfo2, lblInfo3 };
        }

        #endregion 构造函数

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOTP_Load(object sender, EventArgs e)
        {
            try
            {
                //pnlNG.Visible = false;
                pnlNote.Visible = true;
                pnlNote.Dock = DockStyle.Fill;

                int width = (flpScanNote.Width - 3) / 25 - 1;
                if (width <= 0)
                {
                    width = 1;
                }
                this.lblWidth = width;

                // 程序初始化
                Initialize();
            }
            catch (Exception ex)
            {
                LogHelper.Error("窗体加载失败", ex);
                ShowResult(NoteState.Error, "", "程序初始化失败!\n" + ex.Message);
                this.Close();
                return;
            }
        }

        /// <summary>
        /// 窗体显示时，连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOTP_Shown(object sender, EventArgs e)
        {
            //// 打开串口
            //OpenDeviceCom();

            // 连接服务器
            ClientStart();

        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOTP_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;

            foreach (SerialPortUtil port in this.fixturePortList.Values)
            {
                port.ClosePort();
                if (!port.IsOpen)
                {
                    ShowResult(NoteState.Success, port.PortName, "治具串口已关闭(" + this.fixtureList[port.PortName] + ")");
                }
            }
            //CloseDeviceCom();
        }

        /// <summary>
        /// 窗体尺寸变化时，刷新显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOTP_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
        /// <summary>
        /// 当列表失去焦点时，取消选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwNote_LostFocus(object sender, EventArgs e)
        {
            lvwNote.SelectedItems.Clear();
        }
        /// <summary>
        /// 复位按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ////pnlNG.Visible = false;
                //pnlNote.Visible = true;
                //pnlNote.Dock = DockStyle.Fill;
                //btnNG.Text = "不良";

                // 断开服务器
                ClientStop();

                // 关闭串口
                Program.CloseDeviceCom();
                Program.ScanDevice.DataReceived -= ScanDevice_DataReceived;

                foreach (SerialPortUtil port in this.fixturePortList.Values)
                {
                    port.DataReceived -= Fixture_DataReceived;
                    port.DataWrited -= Fixture_DataWrited;
                    port.ClosePort();
                    if(!port.IsOpen)
                    {
                        ShowResult(NoteState.Success, port.PortName, "治具串口已关闭(" + this.fixtureList[port.PortName] + ")");
                    }
                }

                // 清空数据
                txtScanCode.Clear();
                txtFixtureCode.Clear();
                txtFixture1.Clear();
                txtFixture2.Clear();
                txtFixture3.Clear();
                txtQrCode1.Clear();
                txtQrCode2.Clear();
                txtQrCode3.Clear();
                txtCode1.Clear();
                txtCode2.Clear();
                txtCode3.Clear();
                lblInfo1.Text = "扫治具";
                lblInfo2.Text = "扫治具";
                lblInfo3.Text = "扫治具";
                comProcess.SelectedIndex = -1;
                this.AnalysisCode = "";
                //this.LCD_Count = 0;
                //this.OK_Count = 0;
                //this.NG_Count = 0;
                //this.Error_Count = 0;
                //txtLCDCount.Text = "0";
                //txtOKCount.Text = "0";
                //txtNGCount.Text = "0";
                //txtErrorCount.Text = "0";
                this.ScanNote = "";
                this.flpScanNote.Controls.Clear();
                // 清空日志
                this.lvwNote.Items.Clear();

                // 打开串口
                Program.OpenDeviceCom();

                // 程序初始化
                Initialize();

                // 连接接收服务器
                ClientStart();

                // 连接HBase服务器
                GetHBaseDataClass.Instance.Reconnect();

                txtScanCode.Focus();
            }
            catch (Exception ex)
            {
                LogHelper.Error("复位失败", ex);
                ShowResult(NoteState.Error, "", "程序初始化失败!" + ex.Message);
            }
        }

        ///// <summary>
        ///// 选择工序后下拉框关闭时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void comProcess_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (comProcess.SelectedIndex == -1) return;

        //    // 初始化窗体信息
        //    PageInit();
        //}

        /// <summary>
        /// 实际扫描数据文本框按下回车键，开始处理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScanCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtScanCode.Text != "")
            {
                // 分析处理数据
                ScanDataHandler(txtScanCode.Text.Trim());
            }
        }

        ///// <summary>
        ///// 不良申报按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnNG_Click(object sender, EventArgs e)
        //{
        //    if (btnNG.Text == "不良")
        //    {
        //        if (comProcess.SelectedIndex == -1)
        //        {
        //            ShowResult(NoteState.NG, "", "未选择本站工序.");
        //            return;
        //        }
        //        //Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
        //        //frmNG FormNG = new frmNG();
        //        //DialogResult rst = FormNG.ShowDialog(this);
        //        //if (rst == System.Windows.Forms.DialogResult.OK)
        //        //{
        //        //    foreach (string fpc in FormNG.NGGlassList.Keys)
        //        //    {
        //        //        ShowResult(NoteState.Success, fpc, "不良申报成功，不良描述：" + FormNG.NGDesc);
        //        //    }
        //        //}
        //        //Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;

        //        btnNG.Text = "返回";
        //        pnlNote.Visible = false;
        //        pnlNG.Visible = true;
        //        pnlNG.Dock = DockStyle.Fill;

        //        InitBNC();

        //        txtScanCode.SelectAll();
        //        txtScanCode.Focus();
        //    }
        //    else
        //    {
        //        btnNG.Text = "不良";
        //        pnlNote.Visible = true;
        //        pnlNG.Visible = false;
        //        pnlNote.Dock = DockStyle.Fill;
        //        txtScanCode.SelectAll();
        //        txtScanCode.Focus();

        //    }
        //}

        /// <summary>
        /// 自动重连服务器定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrAutoReconnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (client == null)
            {
                client = new Client(ClientPrint, txtIPAddress.Text, txtPort.Text);
            }
            if (!client.connected)
            {
                client.ipString = txtIPAddress.Text.Trim();
                client.port = YJ.Common.Utils.StrToInt(txtPort.Text.Trim(), 0);
                client.start();
            }
            if (client != null)
            {
                ShowResult(NoteState.Success, txtIPAddress.Text + "：" + txtPort.Text, "客户端 " + client.localIpPort);
                if (client.connected)
                {
                    this.tmrAutoReconnect.Stop();
                }
                else
                {
                    this.tmrAutoReconnect.Start();
                }
            }
            else
            {
                this.tmrAutoReconnect.Start();
            }
        }

        /// <summary>
        /// 绑定记录示意图尺寸变化
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
        #region 方法
        /// <summary>
        /// 程序初始化
        /// </summary>
        private void Initialize()
        {
            //btnNG.Text = "不良";
            //pnlNote.Visible = true;
            //pnlNG.Visible = false;
            //pnlNote.Dock = DockStyle.Fill;

            //this.bgkFixture = new BackgroundWorker();
            //this.bgkFixture.WorkerReportsProgress = true;
            //this.bgkFixture.DoWork += BgkFixture_DoWork;
            //this.bgkFixture.ProgressChanged += BgkFixture_ProgressChanged;
            //this.bgkFixture.RunWorkerCompleted += BgkFixture_RunWorkerCompleted;

            // 当前程序版本
            this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // 登录人员账号
            txtOpCode.Text = BaseUI.UI_BOOLOGNAME;
            // 玻璃信息查询超时时间
            searchTimeout = appConfig.GetConfigInt("SearchTimeout");

            // 当前站点IP
            this.ipAddress = BaseUI.GetLocalIP();
#if DEBUG
            //this.ipAddress = "172.20.21.207";    //	LCD清洗投入机	
            //this.ipAddress = "172.20.21.181";
            //this.ipAddress = "192.168.1.150";   //机台补发
            this.ipAddress = "172.20.21.122";   //烧录
            this.ipAddress = "172.20.21.189";   //精简画面测试
#endif
            txtProcessIP.Text = this.ipAddress;

            try
            {
                // 获取本机IP所在的产线、工序、产线在制品的型号
                BaseUI.GetLineModelProcedure(this.ipAddress);
            }
            catch (Exception ex)
            {
                throw new Exception("获取产线在制品信息失败.", ex);
            }

            txtModel.Text = BaseUI.UI_SMSPEC;//成品规格
            txtOrder.Text = BaseUI.UI_SPOMJobCode;//工单编码

            // 上报服务器IP地址
            string interfaceIP = null;
            try
            {
                interfaceIP = YJ.Common.Utils.GetAppConfig("HBase_Interface_IP", "appSettings");
            }
            catch (Exception)
            {
                // APP.config中无HBase_Interface_IP字段
            }
            if (string.IsNullOrEmpty(interfaceIP))
            {
                // 未配置服务器地址HBase_Interface_IP，则使用数据库获取的上报服务器IP地址
                txtIPAddress.Text = BaseUI.UI_SPLIPAddress;
            }
            else
            {
                txtIPAddress.Text = interfaceIP;
            }

            // 上报服务器端口号
            txtPort.Text = BaseUI.UI_SPLPORT;
            //txtPort.Text = "9040";

            // 产线名称
            txtSPLName.Text = BaseUI.UI_SPLNAME;
            // 站点类型
            if (BaseUI.UI_SGXJOBCODE == "050")
            {
                this.siteType = SiteType.过站补扫点;
            }
            else if (BaseUI.UI_IsBind)
            {
                this.siteType = SiteType.关键扫描点;
                //btnNG.Enabled = false;
            }
            else
            {
                this.siteType = SiteType.过站扫描点;
                //btnNG.Enabled = true;
            }

            //// 不良申报
            //if (BaseUI.UI_RIGHT.ContainsKey("021212"))
            //{
            //    //btnNG.Visible = true;
            //}
            //else
            //{
            //    btnNG.Visible = false;
            //    this.tlpInfo.SetCellPosition(this.btnReset, new TableLayoutPanelCellPosition(6, 0));
            //    this.tlpInfo.SetRowSpan(this.btnReset, 3);
            //}

            // 最大日志行数
            maxLogCount = appConfig.GetConfigInt("MaxLogCount");
            // 是否记录程序运行日志
            isWriteLog = appConfig.GetConfigBool("WriteAppLog");

            //// 设置下拉框权限
            //SetUserRight(this.siteType);

            //初始化工序
            BindProcessComboBox(BaseUI.UI_SPOMJobCode, BaseUI.UI_SPLJOBCODE);

            //初始化治具配置
            if (!appConfig.ContainsKey("对接治具类型") || !appConfig.ContainsKey("扫描枪串口") || !appConfig.ContainsKey("治具串口") || !appConfig.ContainsKey("治具编码"))
            {
                throw new Exception("请重新下载最新版程序包.");
            }

            this.fixtureType = appConfig.GetConfigInt("对接治具类型");
            if (this.fixtureType == 0)
            {
                throw new Exception("对接治具类型配置错误.");
            }

            this.scannerPort = appConfig.GetConfigString("扫描枪串口");
            string[] fixturePort = (appConfig.GetConfigString("治具串口")).Split(',');
            this.lstPortName = fixturePort.ToList<string>();
            string[] fixtureCode = (appConfig.GetConfigString("治具编码")).Split(',');
            this.lstFixtureCode = fixtureCode.ToList<string>();
            if (fixturePort.Length != fixtureCode.Length)
            {
                throw new Exception("治具串口和编码配置个数不匹配.");
            }
            else if(fixturePort.Length == 0)
            {
                throw new Exception("未配置治具串口和编码.");
            }
            else if (fixturePort.Length > 3)
            {
                throw new Exception("只支持最多3个治具.");
            }

            if(fixturePort.Length == 1)
            {
                tlpFixture1.Visible = true;
                tlp1.Visible = true;
                tlpFixture2.Visible = false;
                tlp2.Visible = false;
                tlpFixture3.Visible = false;
                tlp3.Visible = false;
            }
            else if (fixturePort.Length == 2)
            {
                tlpFixture1.Visible = true;
                tlp1.Visible = true;
                tlpFixture2.Visible = true;
                tlp2.Visible = true;
                tlpFixture3.Visible = false;
                tlp3.Visible = false;
            }
            else if (fixturePort.Length == 3)
            {
                tlpFixture1.Visible = true;
                tlp1.Visible = true;
                tlpFixture2.Visible = true;
                tlp2.Visible = true;
                tlpFixture3.Visible = true;
                tlp3.Visible = true;
            }

            this.fixtureList = new Dictionary<string, string>();
            for (int i = 0; i < fixturePort.Length; i++)
            {
                this.fixtureList.Add(fixturePort[i], fixtureCode[i]);
                this.lblPort[i].Text = "治具" + fixturePort[i] + ":";
                this.lblPort[i].Tag  = fixturePort[i];
                this.txtFixture[i].Text = fixtureCode[i];
                this.txtFixture[i].BackColor = Color.Lavender;
                this.txtCode[i].BackColor = Color.Lavender;
                this.txtQrCode[i].BackColor = Color.Lavender;
                this.lblInfo[i].BackColor = Color.Lavender;
            }

            //初始化串口下拉列表
            SerialPortUtil.SetPortNameValues(this.cmbSerialPort);
            if (this.cmbSerialPort.Items.Count == 0)
            {
                throw new Exception("串口初始化失败.");
            }
            this.cmbSerialPort.SelectedIndex = -1;

            //关闭自动串口除扫描枪串口的其他串口
            List<string> portList = Program.ScanDevice.portList.Keys.ToList<string>();
            foreach (string item in portList)
            {
                if (item != this.scannerPort)
                {
                    Program.ScanDevice.ClosePort(item);
                    Program.ScanDevice.portList.Remove(item);
                    Program.ScanDevice.DataReceived -= ScanDevice_DataReceived;
                }
            }
            // 绑定串口数据接收事件处理程序
            Program.ScanDevice.DataReceived += new Utils.DataReceivedEventHandler(ScanDevice_DataReceived);

            //根据治具列表打开治具串口
            this.fixturePortList = new Dictionary<string, SerialPortUtil>();
            this.lstFixturePort = new List<SerialPortUtil>();
            this.lstBgk = new List<BackgroundWorker>();
            foreach (string COM in this.lstPortName)
            {
                if(!this.cmbSerialPort.Items.Contains(COM))
                {
                    throw new Exception("治具串口" + COM + "不存在，请检查配置");
                }
                SerialPortUtil port = new SerialPortUtil(COM, SerialPortBaudRates.BaudRate_115200, Parity.None, SerialPortDatabits.EightBits, StopBits.One);
                port.DataReceived += new Utils.DataReceivedEventHandler(Fixture_DataReceived);
                port.DataWrited += new Utils.DataWritedEventHandler(Fixture_DataWrited);
                this.fixturePortList.Add(COM, port);
                this.lstFixturePort.Add(port);

                BackgroundWorker bgkFixture = new BackgroundWorker();
                bgkFixture.WorkerReportsProgress = true;
                bgkFixture.DoWork += BgkFixture_DoWork;
                bgkFixture.ProgressChanged += BgkFixture_ProgressChanged;
                bgkFixture.RunWorkerCompleted += BgkFixture_RunWorkerCompleted;
                this.lstBgk.Add(bgkFixture);

                port.OpenPort();
                if (port.IsOpen)
                {
                    ShowResult(NoteState.Success, COM, "治具串口已打开(" + this.fixtureList[COM] + ")");
                }
                else
                {
                    ShowResult(NoteState.Error, COM, "治具串口打开失败(" + this.fixtureList[COM] + ")");
                }
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
                object GXCode = comProcess.SelectedValue;
                //获取工序链
                this.dtProcessTable = BaseUI.GetProduceRouteData(ProductionOrder, LineCode);
                comProcess.DisplayMember = "SGX_Name";  //工序名称
                comProcess.ValueMember = "SGX_JobCode"; //工序编码
                comProcess.DataSource = this.dtProcessTable;

                //获取工序链列表
                this.dicProcessRoute = this.dtProcessTable.Rows.Cast<DataRow>().ToDictionary(x => x["SGX_JobCode"].ToString(), x => x["SGX_Name"].ToString());

                //判断是否TP测试工序
                if(BaseUI.UI_SGXJOBCODE != "011"
                    && BaseUI.UI_SGXJOBCODE != "012"
                    && BaseUI.UI_SGXJOBCODE != "027"
                    && BaseUI.UI_SGXJOBCODE != "029"
                    && BaseUI.UI_SGXJOBCODE != "060"
                    && BaseUI.UI_SGXJOBCODE != "66")
                {
                    ShowResult(NoteState.NG, ipAddress, "当前站点不是烧录/电测岗位");
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                // 选择当前工序
                try
                {
                    comProcess.SelectedValue = BaseUI.UI_SGXJOBCODE;
                }
                catch (Exception)
                {
                    ShowResult(NoteState.NG, ipAddress, "当前工单不需要过本站工序");
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                // 初始化窗体信息
                PageInit();
            }
            catch (Exception exp)
            {
                LogHelper.Error("初始化工序链失败." + exp.Message, exp);
                ShowResult(NoteState.Error, "", "初始化工序链失败." + exp.Message);
            }
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        private void ClientStart()
        {
            try
            {
                if (string.IsNullOrEmpty(txtIPAddress.Text))
                {
                    ShowResult(NoteState.NG, "", "监听ip地址不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(txtPort.Text))
                {
                    ShowResult(NoteState.NG, "", "监听端口不能为空！");
                    return;
                }
                if (client == null)
                {
                    client = new Client(ClientPrint, txtIPAddress.Text, txtPort.Text);
                }
                if (!client.connected)
                {
                    client.ipString = txtIPAddress.Text.Trim();
                    client.port = YJ.Common.Utils.StrToInt(txtPort.Text.Trim(), 0);
                    client.start();
                }
                if (client != null)
                {
                    if (client.localIpPort != null && client.localIpPort != "")
                    {
                        ShowResult(NoteState.Success, txtIPAddress.Text + "：" + txtPort.Text, "客户端 " + client.localIpPort);
                    }
                    if (!client.connected)
                    {
                        this.tmrAutoReconnect.Start();
                    }
                }

                txtScanCode.SelectAll();
                txtScanCode.Focus();
            }
            catch (Exception exp)
            {
                LogHelper.Error("客户端启动监听失败", exp);
                ShowResult(NoteState.Error, txtIPAddress.Text + "：" + txtPort.Text, "客户端启动监听失败！" + exp.Message.ToString());
            }
        }
        /// <summary>
        /// 断开服务器
        /// </summary>
        private void ClientStop()
        {
            try
            {
                if (client != null && client.connected)
                {
                    client.stop();
                }
                txtScanCode.SelectAll();
                txtScanCode.Focus();
            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                ShowResult(NoteState.Error, "", "断开服务器失败." + exp.Message.ToString());
            }
        }
        /// <summary>
        /// 客户端输出信息
        /// </summary>
        /// <param name="info"></param>
        private void ClientPrint(string info)
        {
            try
            {
                if (lvwNote.InvokeRequired)
                {
                    Client.Print F = new Client.Print(ClientPrint);
                    this.BeginInvoke(F, new object[] { info });
                }
                else
                {
                    if (info != null)
                    {
                        if (info.Contains("失败"))
                        {
                            ShowResult(NoteState.Error, "", info);
                        }
                        else if (info == "31313131")
                        {
                            ShowResult(NoteState.Success, txtIPAddress.Text + "：" + txtPort.Text, "长时间无操作，与服务器连接将自动断开");
                        }
                        else
                        {
                            ShowResult(NoteState.Success, "", info);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                ShowResult(NoteState.Error, "", "客户端输出信息发生异常." + exp.Message.ToString());
            }
        }
        /// <summary>
        /// 发送信息到服务器
        /// </summary>
        /// <param name="sendstring">发送字符串</param>
        /// <returns></returns>
        private NoteResult ClientSend(string sendstring)
        {
            string code = txtCode1.Text;
            try
            {
                if (client != null && client.connected)
                {
                    client.Send(client.StringToHexString(sendstring, System.Text.Encoding.GetEncoding("GBK")));

                    return new NoteResult(NoteState.OK, code, sendstring);
                }
                else
                {
                    return new NoteResult(NoteState.NG, code, "服务器已断开连接");
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                return new NoteResult(NoteState.Error, code, "发送信息到服务器发生异常." + exp.Message.ToString());
            }
        }

        /// <summary>
        /// 跨线程输出结果的委托
        /// </summary>
        private delegate void AddNoteCallback(NoteState state, string code, string message);

        /// <summary>
        /// 跨线程输出结果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        private void AddNote(NoteState state, string code, string message)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    AddNoteCallback d = new AddNoteCallback(AddNote);
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
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
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
                    if (lvwNote.Items.Count >= this.maxLogCount)
                    {
                        lvwNote.Items.Clear();
                    }

                    if (this.isRepeat)
                    {
                        message = "返检：" + message;
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
                            if (this.isRepeat)
                            {
                                message = "返检成功！";
                            }
                            else
                            {
                                message = "操作成功！";
                            }
                            // 弹框提示
                            this.successMsg = new SuccessMessage(message, 2);
                            this.successMsg.ShowDialog(this);
                            break;

                        case NoteState.NG:
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 2);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;

                        case NoteState.Fail:
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 2);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;

                        case NoteState.Error:
                            this.failMsg = new FailMessage(message, 2);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;
                    }
                    this.AnalysisCode = "";
                    txtScanCode.Clear();
                    //txtGlass1.Clear();
                    //txtCode1.Clear();
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
        /// 输出结果
        /// </summary>
        /// <param name="result">提示结果对象</param>
        private void ShowResult(NoteResult result)
        {
            ShowResult(result.State, result.Code, result.Message);
        }


        /// <summary>
        /// 跨线程输出结果的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ShowResultCallback2(NoteState state, string code, string message, int fixtureNo);

        /// <summary>
        /// 跨线程输出结果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        private void ShowResult2(NoteState state, string code, string message, int fixtureNo)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    ShowResultCallback2 d = new ShowResultCallback2(ShowResult2);
                    this.BeginInvoke(d, new object[] { state, code, message, fixtureNo });
                }
                else
                {
                    if (lvwNote.Items.Count >= this.maxLogCount)
                    {
                        lvwNote.Items.Clear();
                    }

                    if (this.isRepeat)
                    {
                        message = "返检：" + message;
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
                            if (this.isRepeat)
                            {
                                message = "返检成功！";
                            }
                            else
                            {
                                message = "操作成功！";
                            }
                            // 弹框提示
                            this.successMsg = new SuccessMessage(message, 2);
                            this.successMsg.ShowDialog(this);
                            break;

                        case NoteState.NG:
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 2);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;

                        case NoteState.Fail:
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 2);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;

                        case NoteState.Error:
                            this.failMsg = new FailMessage(message, 2);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;
                    }
                    this.AnalysisCode = "";
                    txtScanCode.Clear();
                    this.lstGlass[fixtureNo] = "";
                    this.txtCode[fixtureNo].Clear();
                    this.txtQrCode[fixtureNo].Clear();
                    this.txtFixture[fixtureNo].BackColor = Color.Lavender;
                    this.txtCode[fixtureNo].BackColor = Color.Lavender;
                    this.txtQrCode[fixtureNo].BackColor = Color.Lavender;
                    if(this.lstFixtureCode.Count > 1)
                    {
                        this.lblInfo[fixtureNo].Text = ProgressInfo.扫治具.ToString();
                    }
                    else
                    {
                        this.lblInfo[fixtureNo].Text = ProgressInfo.扫玻璃.ToString();
                    }
                    this.lblInfo[fixtureNo].ForeColor = Color.FromArgb(41, 57, 85);
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
        /// 输出结果
        /// </summary>
        /// <param name="result">提示结果对象</param>
        private void ShowResult(NoteResult result, int fixtureNo)
        {
            ShowResult2(result.State, result.Code, result.Message, fixtureNo);
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

        ///// <summary>
        ///// 根据站点类别和人员ID，给型号/工序下拉框赋权
        ///// </summary>
        ///// <param name="processType">站点工序类别</param>
        //private void SetUserRight(SiteType processType)
        //{
        //    try
        //    {
        //        //string RCodeMaterial = "";
        //        string RCodeProcess = "";

        //        switch (processType)
        //        {
        //            case SiteType.关键扫描点:
        //                //RCodeMaterial = "02120101";
        //                RCodeProcess = "02120102";
        //                break;
        //            case SiteType.过站扫描点:
        //                //RCodeMaterial = "02120201";
        //                RCodeProcess = "02120202";
        //                break;
        //            case SiteType.过站补扫点:
        //                //RCodeMaterial = "02120301";
        //                RCodeProcess = "02120302";
        //                break;
        //        }
        //        //工序下拉框权限
        //        comProcess.Enabled = BaseUI.UI_RIGHT.ContainsKey(RCodeProcess);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("获取当前账号操作权限失败", ex);
        //        ShowResult(NoteState.Error, "", "获取当前账号操作权限失败!" + ex.Message);
        //    }
        //}
        /// <summary>
        /// 根据本站工序初始化窗体信息
        /// </summary>
        private void PageInit()
        {
            if (comProcess.SelectedValue == null) return;
            try
            {
                 // 筛选当前选择的工序信息
                this.dvProcess = new DataView(dtProcessTable);
                this.dvProcess.RowFilter = "SGX_JobCode='" + comProcess.SelectedValue + "'";
                if (this.dvProcess.Count > 0)
                {
                    // 是否关键工序
                    if (comProcess.SelectedValue.ToString() == "018" && !Convert.ToBoolean(this.dvProcess[0]["SPOM_IsTP"]))
                    {
                        // 如果TP贴合工序TP无码，则为过站工序
                        this.isBind = false;
                    }
                    else
                    {
                        this.isBind = Convert.ToBoolean(this.dvProcess[0]["SGX_IsBind"]);
                    }

                    // 是否允许返检
                    switch (this.dvProcess[0]["SGX_Rework"].ToString())
                    {
                        case "02":
                            this.processRepeatEnabled = true;
                            break;
                        default:
                            this.processRepeatEnabled = false;
                            break;
                    }
                    
                    //txtSPLName.Text = this.dvProcess[0]["lineName"].ToString();             //产线编码
                    txtProcessNumber.Text = this.dvProcess[0]["PR_NoSeq"].ToString();          //当前工序序号
                    txtProcessCode.Text = this.dvProcess[0]["SGX_JobCode"].ToString();      //当前工序编码
                    //txtEarlyProcess.Text = sqlView[0]["EarlyProcessName"].ToString();   //前置工序
                    BaseUI.UI_CurrentProcedureNo = this.dvProcess[0]["PR_NoSeq"].ToString();          //当前工序序号
                    BaseUI.UI_CurrentProcedureCode = this.dvProcess[0]["SGX_JobCode"].ToString();      //当前工序编码
                    //当前站点IP地址,补扫是获取的工序对应IP，正常工序为本机IP
                    BaseUI.UI_EquCardIP = this.siteType == SiteType.过站补扫点 ? this.dvProcess[0]["STW_CardIP"].ToString() : this.ipAddress;
                    txtProcessIP.Text = BaseUI.UI_EquCardIP;

                    // 机台IP设置的弹框提醒类型
                    this.warnType = BaseUI.GetDeviceWarnType(BaseUI.UI_EquCardIP);

                    // 相邻工序过站间隔时间
                    this.processInterval = BaseUI.GetProcessInterval(BaseUI.UI_EquCardIP);

                    // 当前已过工序的列表
                    this.dicProcess = BaseUI.GetProcedure(this.dtProcessTable);

                    // 扫描类型
                    if (String.IsNullOrEmpty(this.dvProcess[0]["PR_ScanType"].ToString()))
                    {
                        ShowResult(NoteState.NG, "", "请先在MES系统中配置工序【" + comProcess.Text + "】的扫描类型");
                        return;
                    }
                    string PR_ScanType = Enum.GetName(typeof(ScanType), Convert.ToInt32(this.dvProcess[0]["PR_ScanType"]));
                    string[] scanType = PR_ScanType.Split(new string[] { "_and_", "_or_" }, StringSplitOptions.RemoveEmptyEntries);

                    txtScanCode.Clear();
                    //txtGlass1.Clear();
                    //txtCode1.Clear();
                    this.AnalysisCode = "";
                    this.LastCode1 = "";

 
                    if (comProcess.SelectedValue.ToString() != "018" && scanType.Length != 1)
                    {
                        ShowResult(NoteState.NG, "", PR_ScanType + ":工序【" + comProcess.Text + "】的扫描类型配置错误");
                        return;
                    }
                    this.scanType1 = scanType[0];
                    //this.scanType2 = "";
                    lblCode1.Text = "1#" + this.scanType1 + "码:";
                    lblCode2.Text = "2#" + this.scanType1 + "码:";
                    lblCode3.Text = "3#" + this.scanType1 + "码:";
                    txtRule1.Text = this.dvProcess[0]["Rule_" + this.scanType1].ToString();
                    txtDigit1.Text = this.dvProcess[0]["Len_" + this.scanType1].ToString();

                    //OTP烧录需要一拖三
                    if (comProcess.SelectedValue.ToString() == "66")
                    {
                        lblInfo1.Text = ProgressInfo.扫治具.ToString();
                        lblInfo2.Text = ProgressInfo.扫治具.ToString();
                        lblInfo3.Text = ProgressInfo.扫治具.ToString();
                    }
                    //电测是一拖一
                    else
                    {
                        lblInfo1.Text = ProgressInfo.扫玻璃.ToString();
                        lblInfo2.Text = ProgressInfo.扫玻璃.ToString();
                        lblInfo3.Text = ProgressInfo.扫玻璃.ToString();
                    }

                    // 设置程序标题
                        this.Text = this.siteType.ToString() + "--" + this.dvProcess[0]["SGX_Name"].ToString() + "   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
                }
                else
                {
                    ShowResult(NoteState.NG, ipAddress, "当前工单不需要过此工序");
                    comProcess.SelectedValue = -1;
                    return;
                }
                this.Refresh();
                txtScanCode.SelectAll();
                txtScanCode.Focus();
            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                ShowResult(NoteState.Error, "", "初始化数据失败." + exp.Message.ToString());
            }

        }
        

        /// <summary>
        /// 收到测试治具串口数据
        /// </summary>
        /// <param name="e"></param>
        private void Fixture_DataReceived(Utils.DataReceivedEventArgs e)
        {
            try
            {
                string portName = e.PortName;
                string ReString = e.HexStringReceived.Replace(" ", "");
                string data = e.HexStringReceived.Replace(" ", "");

                FixtureDataHandler(portName, data);
            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                ShowResult(NoteState.Error, e.PortName, "接收治具数据失败." + exp.Message.ToString());
            }
        }

        /// <summary>
        /// 治具串口发送数据事件处理
        /// </summary>
        /// <param name="e"></param>
        private void Fixture_DataWrited(DataWritedEventArgs e)
        {
            //try
            //{

            //}
            //catch (Exception exp)
            //{
            //    LogHelper.Error(exp.Message, exp);
            //    ShowResult(NoteState.Error, this.fixturePort, "接收治具数据失败." + exp.Message.ToString());
            //}
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
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                ShowResult(NoteState.Error, Program.ScanDevice.PortName, "扫描设备接收数据失败." + exp.Message.ToString());
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
            int fixtureNo = -1;
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (IsScan)
                {
                    if (this.InvokeRequired)
                    {
                        ScanDataHandlerCallback d = new ScanDataHandlerCallback(ScanDataHandler);
                        this.BeginInvoke(d, new object[] { DataString });
                    }
                    else
                    {
                        // 第1步：站点基本信息检测
                        #region 第1步：站点基本信息检测
                        if (string.IsNullOrWhiteSpace(txtSPLName.Text))
                        {
                            ShowResult(NoteState.Fail, "", "获取设备所属产线失败");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(txtOpCode.Text))
                        {
                            ShowResult(NoteState.Fail, "", "获取扫描人员账号失败");
                            return;
                        }

                        #endregion 第1步：站点基本信息检测

                        // 第2步：解析扫描数据
                        #region 第2步：解析扫描数据
                        txtScanCode.Text = DataString.Trim();  // 实际扫描数据
                        if (DataString.Contains("OK:") || DataString.Contains("ERROR:"))
                        {
                            ShowResult(NoteState.Fail, DataString, "编码不符合规则，请确认扫描枪是否打开了测试模式");
                            return;
                        }

                        string[] ds = DataString.Split(':');
                        if (ds.Length == 0)
                        {
                            ShowResult(NoteState.Fail, DataString, "编码格式不符合规则");
                            return;
                        }
                        this.AnalysisCode = ds[0].ToString().Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");    //扫描数据解析

                        // YFPCA-SKI65101B18S|F|S1|201121|01F68          YFPCA-SKI65101B18S/F/S1/201121/01F68
                        this.AnalysisCode = this.AnalysisCode.Replace("|", "/").Replace(";", "；");

                        // 基本长度判断
                        if (AnalysisCode.Length <= 9)
                        {
                            ShowResult(NoteState.Fail, DataString, "默认编码长度至少为10位以上，请确认条码长度");
                            return;
                        }
                        // 小写字母检查
                        if (BaseUI.LowerCheck(AnalysisCode))
                        {
                            if (appConfig.ContainsKey("LowerEnabled"))
                            {
                                if (!appConfig.GetConfigBool("LowerEnabled"))
                                {
                                    ShowResult(NoteState.Fail, DataString, "条码中禁止包含小写字母");
                                    return;
                                }
                            }
                            else
                            {
                                ShowResult(NoteState.Fail, DataString, "条码中禁止包含小写字母");
                                return;
                            }
                        }
                        #endregion 第2步：解析扫描数据

                        NoteResult rst = null;

                        //确定已扫描治具编码的治具序号
                        for (int i = 0; i < this.lstFixtureCode.Count; i++)
                        {
                            if (lblInfo[i].Text == ProgressInfo.扫玻璃.ToString())
                            {
                                fixtureNo = i;
                                break;
                            }
                            else if(lblInfo[i].Text == ProgressInfo.扫治具.ToString())
                            {
                                fixtureNo = 3;
                            }
                        }

                        //未扫描治具
                        if (fixtureNo == -1)
                        {
                            rst = new NoteResult(NoteState.Fail, DataString, "治具占用中，请稍后再试");
                        }
                        else if (fixtureNo == 3)
                        {
                            // 第3步：判断是否合法的治具编码
                            #region 第4步：判断是否合法的治具编码
                            rst = isValidFixtureCode(AnalysisCode, out fixtureNo);
                            #endregion 第4步：判断是否合法的治具编码c
                        }
                        else
                        {
                            // 第4步：判断是否合法的Code1
                            #region 第3步：判断是否合法的Code1

                            // 判断是否合法的Code1
                            rst = isValidCode1(AnalysisCode, fixtureNo);
                            #endregion 第3步：判断是否合法的Code1
                        }

                        // 第5步：输出检测结果,发送数据到服务器
                        #region 第5步：输出检测结果,发送数据到服务器
                        switch (rst.State)
                        {
                            case NoteState.Fail:    // 检测到异常
                                if (fixtureNo != -1)
                                {
                                    this.lblInfo[fixtureNo].Text = ProgressInfo.错误.ToString();
                                    this.lblInfo[fixtureNo].ForeColor = Color.Red;
                                }
                                // 输出提示信息
                                ShowResult(rst, fixtureNo);
                                break;

                            case NoteState.NG:      // 漏工序
                                if (fixtureNo != -1)
                                {
                                    this.lblInfo[fixtureNo].Text = ProgressInfo.错误.ToString();
                                    this.lblInfo[fixtureNo].ForeColor = Color.Red;
                                    this.txtCode[fixtureNo].BackColor = Color.Red;
                                }
                                // 输出提示信息
                                ShowResult(rst, fixtureNo);
                                this.LCD_Count += 1;
                                this.NG_Count += 1;
                                txtLCDCount.Text = this.LCD_Count.ToString();
                                txtNGCount.Text = this.NG_Count.ToString();
                                AddScanNote("1");
                                break;

                            case NoteState.Error:   // 混线、不良品、错绑
                                if (fixtureNo != -1)
                                {
                                    this.lblInfo[fixtureNo].Text = ProgressInfo.错误.ToString();
                                    this.lblInfo[fixtureNo].ForeColor = Color.Red;
                                    this.txtCode[fixtureNo].BackColor = Color.Red;
                                }
                                ShowResult(rst, fixtureNo);
                                this.LCD_Count += 1;
                                this.Error_Count += 1;
                                txtLCDCount.Text = this.LCD_Count.ToString();
                                txtErrorCount.Text = this.Error_Count.ToString();
                                AddScanNote("2");
                                break;

                            case NoteState.Success:
                                // 返回，等待扫描第二个编码
                                if (fixtureNo != -1)
                                {
                                    this.txtFixture[fixtureNo].BackColor = Color.Lime;
                                    this.lblInfo[fixtureNo].Text = ProgressInfo.扫玻璃.ToString();
                                }
                                this.AnalysisCode = "";
                                txtScanCode.Clear();
                                txtScanCode.Focus();
                                break;
                            case NoteState.OK:
                                ShowResult(NoteState.Success, DataString, "已扫描编码");
                                if (fixtureNo != -1)
                                {
                                    this.txtCode[fixtureNo].BackColor = Color.Lime;
                                    this.txtQrCode[fixtureNo].BackColor = Color.Lime;
                                    this.txtQrCode[fixtureNo].Text = lstQrCode[fixtureNo];
                                    this.lblInfo[fixtureNo].Text = ProgressInfo.测试中.ToString();
                                }
                                this.lstBgk[fixtureNo].RunWorkerAsync(fixtureNo);

                                break;
                        }
                        #endregion 第5步：输出检测结果,发送数据到服务器
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据解析失败", ex);
                ShowResult(NoteState.Error, "", "数据解析失败." + ex.Message.ToString());
            }
        }


        private void BgkFixture_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgk = sender as BackgroundWorker;
            int fixtureNo = Convert.ToInt32(e.Argument);
            string portName = this.lstPortName[fixtureNo];
            SerialPortUtil port = this.lstFixturePort[fixtureNo];
            //SerialPortUtil port = this.fixturePortList[portName];

            //如果是电测岗位，需要发送扫码成功允许测试指令给治具
            if (BaseUI.UI_SGXJOBCODE == "011"
                   || BaseUI.UI_SGXJOBCODE == "012"
                   || BaseUI.UI_SGXJOBCODE == "027"
                   || BaseUI.UI_SGXJOBCODE == "029"
                   || BaseUI.UI_SGXJOBCODE == "060")
            {
                // 发送扫码成功允许测试指令给治具
                TestStart(fixtureNo);
            }

            //bgk.ReportProgress(10, fixtureNo);

            //PublicSub.Delay(4000);

            //data = Encoding.GetEncoding("GBK").GetBytes("开启TP测试\r\n");
            //port.WriteData(data);
            ////port.WriteData("开启TP测试\r\n");//B0B4BFAAB9D8BCFC0D0A
            //bgk.ReportProgress(20, fixtureNo);
        }
        private void BgkFixture_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //string portName = e.UserState.ToString();
            //string fixtureCode = this.fixtureList[portName];
            int fixtureNo = Convert.ToInt32(e.UserState);
            string portName = this.lstPortName[fixtureNo];
            string fixtureCode = this.lstFixtureCode[fixtureNo];
            int i = e.ProgressPercentage;
            if (i == 10)
            {
                AddNote(NoteState.Success, "扫码成功允许测试\r\n", portName + "发送数据5A5A5A5A5A，治具:" + fixtureCode);
            }
            else if (i == 20)
            {
                AddNote(NoteState.Success, "开启TP测试\r\n", portName + "发送数据，治具:" + fixtureCode);
            }
        }

        private void BgkFixture_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
        }


        /// <summary>
        /// 分析处理治具数据的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void FixtrueDataHandlerCallback(string PortName, string DataString);
        /// <summary>
        /// 分析处理治具数据
        /// </summary>
        /// <param name="DataString">治具接收字符串</param>
        private void FixtureDataHandler(string PortName, string DataString)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    FixtrueDataHandlerCallback d = new FixtrueDataHandlerCallback(FixtureDataHandler);
                    this.BeginInvoke(d, new object[] { PortName, DataString });
                }
                else
                {
                    int fixtureNo = this.lstPortName.IndexOf(PortName);
                    //治具编码
                    //string fixtureCode = this.fixtureList[PortName];
                    string fixtureCode = this.lstFixtureCode[fixtureNo];

                    //if (ReString.Length >= 12)
                    //{
                    //    string data = ReString.Substring(0, 12);
                    //    // 治具请求客户码指令
                    //    if (data == "55AA88770101")
                    //    {
                    //        ShowResult(NoteState.Success, e.PortName, "获取客户码指令:" + data);
                    //        if (string.IsNullOrEmpty(this.qrCode))
                    //        {
                    //            ShowResult(NoteState.Fail, e.PortName, "当前玻璃客户码获取失败");
                    //        }
                    //        else
                    //        {
                    //            //发送客户码给治具
                    //            SendQrCodeToFixture();
                    //        }
                    //    }
                    //    // 客户码正确接收应答
                    //    else if (data == "55AA8877FFFF")
                    //    {
                    //        ShowResult(NoteState.Success, this.fixturePort, "治具接收成功:" + data);
                    //    }
                    //}

                    //扫码成功允许测试
                    if (DataString.Contains("5A5A5A5A5A"))
                    {
                        AddNote(NoteState.Success, PortName, "治具收到允许测试指令" + ": " + DataString);
                    }
                    //PG开机成功指令
                    else if (DataString.Contains("A5A5A5A5A5"))
                    {
                        AddNote(NoteState.Success, PortName, "收到治具PG开机成功指令" + ": " + DataString);
                    }
                    // 治具请求客户码指令
                    else if (DataString.Contains("55AA88770101"))
                    {
                        ShowResult(NoteState.Success, PortName, "获取客户码指令:" + DataString);
                        if (string.IsNullOrEmpty(this.txtQrCode[fixtureNo].Text))
                        {
                            if(string.IsNullOrEmpty(this.lstQrCode[fixtureNo]))
                            {
                                ShowResult(NoteState.Fail, PortName, "当前玻璃客户码获取失败");
                            }
                            else
                            {
                                //发送客户码给治具
                                SendQrCodeToFixture(fixtureNo);
                            }
                        }
                        else
                        {
                            //发送客户码给治具
                            SendQrCodeToFixture(fixtureNo);
                        }
                    }
                    // 客户码正确接收应答
                    else if (DataString.Contains("55AA8877FFFF"))
                    {
                        ShowResult(NoteState.Success, PortName, "治具接收客户码成功:" + DataString);

                        if (BaseUI.UI_SGXJOBCODE != "66") return;
                        //OTP烧录直接发送过站数据给服务器
                        if (fixtureNo != -1)
                        {
                            this.lblInfo[fixtureNo].Text = ProgressInfo.PASS.ToString();
                            this.lblInfo[fixtureNo].ForeColor = Color.Lime;
                        }
                        // 生成发送字符串
                        string sendCode = GetSendString("0", "", fixtureNo);
                        if (sendCode == "")
                        {
                            ShowResult2(NoteState.NG, "", "生成发送字符串失败!", fixtureNo);
                            return;
                        }

                        // 发送数据到服务器
                        NoteResult rst = ClientSend(sendCode.Replace("\r", "").Replace("\n", ""));
                        if (rst.State == NoteState.OK)
                        {
                            this.LastCode1 = txtCode1.Text.Trim().Replace("\r", "").Replace("\n", "");
                            this.LCD_Count += 1;
                            this.OK_Count += 1;
                            txtLCDCount.Text = this.LCD_Count.ToString();
                            txtOKCount.Text = this.OK_Count.ToString();
                            AddScanNote("0");
                        }
                        if (fixtureNo != -1)
                        {
                            this.lblInfo[fixtureNo].Text = ProgressInfo.已完成.ToString();
                            this.lblInfo[fixtureNo].ForeColor = Color.FromArgb(41, 57, 85);
                        }
                        ShowResult(rst, fixtureNo);
                    }
                    else if(DataString.Length >= 26)
                    {
                        string data = DataString.Substring(0, 4);
                        // PG测试结果返回MES指令
                        if (data == "5A0F")
                        {
                            ShowResult(NoteState.Success, PortName, "PG测试结果返回MES:" + DataString);

                            //不良项
                            string NGDesc = "";
                            //逐项分析测试结果
                            data = DataString.Substring(4, 2);
                            if(data == "01")
                            {
                                NGDesc += "画面电压电流NG|";
                            }
                            data = DataString.Substring(6, 2);
                            if (data == "01")
                            {
                                NGDesc += "睡眠电流NG|";
                            }
                            data = DataString.Substring(8, 2);
                            if (data == "01")
                            {
                                NGDesc += "VCOM NG|";
                            }
                            data = DataString.Substring(10, 2);
                            if (data == "01")
                            {
                                NGDesc += "ID NG|";
                            }
                            data = DataString.Substring(12, 2);
                            if (data == "01")
                            {
                                NGDesc += "二维码NG|";
                            }
                            data = DataString.Substring(14, 2);
                            if (data == "01")
                            {
                                NGDesc += "TE NG|";
                            }
                            data = DataString.Substring(16, 2);
                            if (data == "01")
                            {
                                NGDesc += "PWM NG|";
                            }
                            data = DataString.Substring(18, 2);
                            if (data == "01")
                            {
                                NGDesc += "硬件ID NG|";
                            }
                            data = DataString.Substring(20, 2);
                            if (data == "01")
                            {
                                NGDesc += "RST NG|";
                            }
                            data = DataString.Substring(22, 2);
                            if (data == "01")
                            {
                                NGDesc += "ESD NG|";
                            }
                            NGDesc = NGDesc.TrimEnd('|');

                            //发送字符串
                            string sendCode = "";

                            //测试PASS
                            if(NGDesc == "")
                            {
                                if (fixtureNo != -1)
                                {
                                    this.lblInfo[fixtureNo].Text = ProgressInfo.PASS.ToString();
                                    this.lblInfo[fixtureNo].ForeColor = Color.Lime;
                                }
                                // 生成发送字符串
                                sendCode = GetSendString("0", "", fixtureNo);
                            }
                            //测试FAIL
                            else
                            {
                                if (fixtureNo != -1)
                                {
                                    this.lblInfo[fixtureNo].Text = ProgressInfo.FAIL.ToString();
                                    this.lblInfo[fixtureNo].ForeColor = Color.Red;
                                }
                                // 生成发送字符串
                                sendCode = GetSendString("1", NGDesc, fixtureNo);
                             }
                            if (sendCode == "")
                            {
                                ShowResult2(NoteState.NG, "", "生成发送字符串失败!", fixtureNo);
                                return;
                            }

                            // 发送数据到服务器
                            NoteResult rst = ClientSend(sendCode.Replace("\r", "").Replace("\n", ""));
                            if (rst.State == NoteState.OK)
                            {
                                this.LastCode1 = txtCode1.Text.Trim().Replace("\r", "").Replace("\n", "");
                                this.LCD_Count += 1;
                                this.OK_Count += 1;
                                txtLCDCount.Text = this.LCD_Count.ToString();
                                txtOKCount.Text = this.OK_Count.ToString();
                                AddScanNote("0");
                            }
                            if (fixtureNo != -1)
                            {
                                this.lblInfo[fixtureNo].Text = ProgressInfo.已完成.ToString();
                                this.lblInfo[fixtureNo].ForeColor = Color.FromArgb(41, 57, 85);
                            }
                            ShowResult(rst, fixtureNo);
                        }
                        // 客户码正确接收应答
                        else
                        {
                            AddNote(NoteState.Success, PortName, "治具返回数据: " + DataString);
                        }
                    }
                    //收到其他数据
                    else
                    {
                        AddNote(NoteState.Success, PortName, "治具返回数据: " + DataString);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("治具串口数据处理失败", ex);
                ShowResult(NoteState.Error, "", "治具串口数据处理失败." + ex.Message.ToString());
            }
        }


        /// <summary>
        /// 扫码成功允许测试指令的委托
        /// </summary>
        private delegate void TestStartCallback(int fixtureNo);
        /// <summary>
        /// 扫码成功允许测试指令
        /// </summary>
        private void TestStart(int fixtureNo)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    TestStartCallback d = new TestStartCallback(TestStart);
                    this.BeginInvoke(d, new object[] { fixtureNo });
                }
                else
                {
                    if (fixtureNo < 0)
                    {
                        ShowResult(NoteState.Error, fixtureNo.ToString(), "治具编号无效");
                        return;
                    }
                   
                    //生成发送数据
                    byte[] sendBytes = new byte[5];
                    sendBytes[0] = 0x5A;
                    sendBytes[1] = 0x5A;
                    sendBytes[2] = 0x5A;
                    sendBytes[3] = 0x5A;
                    sendBytes[4] = 0x5A;
               
                    this.lstFixturePort[fixtureNo].WriteData(sendBytes);
                    ShowResult(NoteState.Success, this.lstPortName[fixtureNo] + ":5A5A5A5A5A", "扫码成功允许测试(治具" + this.lstFixtureCode[fixtureNo] + ")");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("允许测试指令发送失败" + this.lstPortName[fixtureNo], ex);
                ShowResult(NoteState.Error, this.lstPortName[fixtureNo], "允许测试指令发送失败." + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 发送客户码给治具的委托
        /// </summary>
        private delegate void SendQrCodeToFixtureCallback(int fixtureNo);
        /// <summary>
        /// 发送客户码给治具
        /// </summary>
        private void SendQrCodeToFixture(int fixtureNo)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    SendQrCodeToFixtureCallback d = new SendQrCodeToFixtureCallback(SendQrCodeToFixture);
                    this.BeginInvoke(d, new object[] { fixtureNo });
                }
                else
                {
                    if(string.IsNullOrEmpty(this.txtQrCode[fixtureNo].Text))
                    {
                        ShowResult(NoteState.Error, this.txtCode[fixtureNo].Text, "当前玻璃客户码获取失败");
                        return;
                    }
                    //二维码字符串转成二进制数组
                    byte[] codeBytes = Encoding.UTF8.GetBytes(this.txtQrCode[fixtureNo].Text);
                    //计算校验和
                    int checksum = 0;
                    foreach (byte chData in codeBytes)
                    {
                        checksum += chData;
                    }
                    checksum += 0x03;
                    //获取校验和高低8位
                    byte checksum_H = Convert.ToByte(checksum >> 8);
                    byte checksum_L = Convert.ToByte(checksum & 0x00ff);
                    //二维码长度
                    int len = codeBytes.Length;
                    //生成发送数据
                    byte[] sendBytes = new byte[9 + len];
                    sendBytes[0] = 0x55;
                    sendBytes[1] = 0xAA;
                    sendBytes[2] = 0x88;
                    sendBytes[3] = 0x77;
                    sendBytes[4] = 0x01;
                    sendBytes[5] = 0x02;
                    sendBytes[6] = Convert.ToByte(len);
                    Array.Copy(codeBytes, 0, sendBytes, 7, len);
                    sendBytes[7 + len] = checksum_H;
                    sendBytes[8 + len] = checksum_L;

                    byte[] receiveBytes = new byte[6];
                    //this.fixture.SendCommand(sendBytes, ref receiveBytes, 5000);
                    //if(receiveBytes[0] == 0x55 && receiveBytes[1] == 0xAA && receiveBytes[2] == 0x88 && receiveBytes[3] == 0x77 && receiveBytes[4] == 0xFF && receiveBytes[5] == 0xFF)
                    //{
                    //    ShowResult(NoteState.Success, this.fixturePort, "治具接收成功:55AA8877FFFF");
                    //    return;
                    //}
                    //else
                    //{
                    //    ShowResult(NoteState.Fail, this.fixturePort, "治具接收数据超时");
                    //    return;
                    //}

                    this.lstFixturePort[fixtureNo].WriteData(sendBytes);
                    ShowResult(NoteState.Success, this.lstFixturePort[fixtureNo].PortName, "已发送客户码(" + this.txtQrCode[fixtureNo].Text + ")给治具" + this.lstFixtureCode[fixtureNo]);
                    //// 清除玻璃绑定客户码信息
                    //this.qrCode = null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("发送客户码给治具失败", ex);
                ShowResult(NoteState.Error, "", "发送客户码给治具失败." + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern uint GetTickCount();


        /// 判断是否合法的治具编码
        /// </summary>
        /// <param name="DataString">扫描编码</param>
        /// <returns>NoteResult</returns>
        private NoteResult isValidFixtureCode(string DataString, out int fixtureNo)
        {
            fixtureNo = -1;
            //如果匹配了玻璃编码规则，则报错
            if (DataString.Substring(0, DataString.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : DataString.Length) == txtRule1.Text)
            {
                IsScan = false;
                return new NoteResult(NoteState.Fail, DataString, "请先扫描治具编码");
            }

            if (!DataString.Contains("-ASSY-") 
                && !DataString.Contains("-CFOG-")
                 && !DataString.Contains("-ASSYOTP-")
                  && !DataString.Contains("-CFOG/TP-"))
            {
                IsScan = false;
                return new NoteResult(NoteState.Fail, DataString, "请扫描正确的治具编码");
            }

            //匹配治具编码获取治具序号
            for (int i = 0; i < this.lstFixtureCode.Count; i++)
            {
                if (DataString == this.lstFixtureCode[i])
                {
                    fixtureNo = i;
                    break;
                }
            }

            //未能匹配治具编码
            if (fixtureNo == -1)
            {
                IsScan = false;
                return new NoteResult(NoteState.Fail, DataString, "程序未配置该治具编码");
            }
            else
            {
                // 治具编码
                this.txtFixtureCode.Text = DataString;
                // 治具串口
                this.cmbSerialPort.SelectedIndex = this.cmbSerialPort.Items.IndexOf(this.lstPortName[fixtureNo]);

                return new NoteResult(NoteState.Success);
            }
        }

        /// <summary>
        /// 判断是否合法的Code1
        /// </summary>
        /// <param name="DataString">扫描编码</param>
        /// <param name="FixtureNo">治具编号</param>
        /// <returns>NoteResult</returns>
        private NoteResult isValidCode1(string DataString, int FixtureNo)
        {
            this.txtQrCode[FixtureNo].Clear();
            this.glassInfo = null;
            this.lstGlass[FixtureNo] = "";
            this.lstQrCode[FixtureNo] = "";
            this.lstGlassInfo[FixtureNo] = null;
            this.isRepeat = false;

                //不匹配Code1编码规则
                if (DataString.Substring(0, DataString.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : DataString.Length) != txtRule1.Text 
                && (DataString.Contains("-ASSY-") || DataString.Contains("-CFOG-") || DataString.Contains("-ASSYOTP-") || DataString.Contains("-CFOG/TP-")))
            {
                    IsScan = false;
                    return new NoteResult(NoteState.Fail, DataString, "请扫描" + this.scanType1 + "编码");
            }

            #region 从HBase数据库查询过站信息
            try
            {
                //异步查询Code1过站信息
                CallWithTimeout(GetProcessData1, this.searchTimeout);
                //GetProcessData1();
                if (this.glassInfo == null)
                {
                    IsScan = false;
                    return new NoteResult(NoteState.Fail, DataString, "HBase数据库连接异常.");
                }
            }
            catch (TimeoutException tex)
            {
                IsScan = false;
                GetHBaseDataClass.Instance.Reconnect();
                return new NoteResult(NoteState.Fail, DataString, "HBase数据库连接超时." + tex.Message.ToString());
            }
            catch (Exception exp)
            {
                IsScan = false;
                return new NoteResult(NoteState.Fail, DataString, "查询玻璃绑定信息失败." + exp.Message.ToString());
            }

            #endregion 从HBase数据库查询过站信息

            #region 编码合法性检测
            // 第一道工序，检查LCD编码是否已存在
            if (txtProcessNumber.Text == "1" && this.glassInfo.ProductInfo != null && !string.IsNullOrWhiteSpace(this.glassInfo.ProductInfo.LCDCode))
            {
                IsScan = false;
                return new NoteResult(NoteState.Error, DataString, "当前玻璃码已绑定，请勿多次绑定");
            }
            // 其他工序，检查是否获取玻璃信息
            if (txtProcessNumber.Text != "1" && (this.glassInfo.ProductInfo == null || string.IsNullOrWhiteSpace(this.glassInfo.ProductInfo.LCDCode)))
            {

                IsScan = false;
                return new NoteResult(NoteState.Error, DataString, "获取FOG绑定信息失败");
            }
            #endregion 编码合法性检测

            #region 混线生产检测
            // 与当前产线不一致，判断是否允许混线
            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionLineCode) && this.glassInfo.ProductInfo.ProductionLineCode != BaseUI.UI_SPLJOBCODE)
            {
                // 获取成品料维护中配置的可混产线列表
                List<string> lstMixLine = BaseUI.GetMixLine(this.glassInfo.ProductInfo.FinishesCode);
                // 未配置可混产线，说明禁止混线
                if (lstMixLine.Count == 0)
                {
                    IsScan = false;
                    return new NoteResult(NoteState.Error, DataString, "该玻璃产线为" + this.glassInfo.ProductInfo.ProductionLineCode + ",禁止混线。");
                }
                // 混线产线数量>0，且不包含当前产线,则禁止在本线体混线
                else if (!lstMixLine.Contains(BaseUI.UI_SPLJOBCODE))
                {
                    IsScan = false;
                    return new NoteResult(NoteState.Error, DataString, "该玻璃产线为" + this.glassInfo.ProductInfo.ProductionLineCode + ",禁止在当前产线生产。");
                }
            }
            #endregion 混线生产检测

            #region 型号切换检测
            // 不同型号时检测是否允许切换型号
            if (this.glassInfo.ProductInfo != null && this.glassInfo.ProductInfo.FinishesModel != txtModel.Text)
            {
                if (!string.IsNullOrWhiteSpace(this.warnType) && this.warnType.Substring(3, 1) == "0")
                {
                    IsScan = false;
                    return new NoteResult(NoteState.Error, DataString, "该玻璃型号为" + this.glassInfo.ProductInfo.FinishesModel + ",与当前产线不一致。");
                }
            }
            #endregion 型号切换检测

            #region 判断玻璃是否已包装
            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode) && BaseUI.isInPackage(this.glassInfo.ProductInfo.LCDCode))
            {
                IsScan = false;
                return new NoteResult(NoteState.Fail, DataString, "该玻璃已包装，无法过站！");
            }
            #endregion 判断玻璃是否已包装


            #region 切换工单，重新初始化页面
            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionOrder) && this.glassInfo.ProductInfo.ProductionOrder != txtOrder.Text)
            {
                txtOrder.Text = this.glassInfo.ProductInfo.ProductionOrder;
                txtModel.Text = this.glassInfo.ProductInfo.FinishesModel;
                // 初始化工序BaseUI.UI_SPLJOBCODE
                //BindProcessComboBox(this.glassInfo.ProductInfo.ProductionOrder, this.glassInfo.ProductInfo.ProductionLineCode);
                BindProcessComboBox(this.glassInfo.ProductInfo.ProductionOrder, BaseUI.UI_SPLJOBCODE);

                if (comProcess.SelectedValue == null)
                {
                    return new NoteResult(NoteState.NG, DataString, "该玻璃工单无本站点工序");
                }
            }
            #endregion 切换工单，重新初始化页面

            #region Code1编码规则检查
            if (txtRule1.Text == "")
            {
                IsScan = false;
                return new NoteResult(NoteState.Fail, "", "未设置" + this.scanType1 + "编码规则");
            }
            if (txtDigit1.Text == "")
            {
                IsScan = false;
                return new NoteResult(NoteState.Fail, "", "未设置" + this.scanType1 + "编码长度");
            }

            bool isRightCode = false;   // 正确编码
            bool isDigit = false;//编码长度是否匹配
            if (txtDigit1.Text == "0" || DataString.Length == YJ.Common.Utils.StrToInt(txtDigit1.Text, 0))
            {
                isDigit = true;
                if (DataString.Substring(0, DataString.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : DataString.Length ) == txtRule1.Text)
                {
                    txtCode1.Text = DataString;
                    isRightCode = true;
                }
            }
            // 所有编码长度都不匹配，提示错误信息
            if (!isDigit)
            {
                IsScan = false;
                return new NoteResult(NoteState.Error, DataString, DataString + "\n编码长度" + DataString.Length + "不符合规则");
            }
            // 所有编码规则都不匹配，提示错误信息
            if (!isRightCode)
            {
                IsScan = false;
                return new NoteResult(NoteState.Error, DataString, DataString + "\n编码格式不符合规则");
            }

            #endregion Code1编码规则检查

            //#region 重复扫描过滤
            //if (!String.IsNullOrEmpty(this.LastCode1) && DataString == this.LastCode1)
            //{
            //    IsScan = false;
            //    return new NoteResult(NoteState.Fail, DataString, "当前玻璃已扫描,\n请勿重复扫描");
            //}
            //#endregion 重复扫描过滤

            this.txtCode[FixtureNo].Text = DataString;
            this.txtCode[FixtureNo].BackColor = Color.Lime;


            if (this.glassInfo.ProductInfo != null)
            {
                // 填充玻璃编码
                //txtGlass1.Text = this.glassInfo.ProductInfo.LCDCode;//玻璃码
                
                this.lstGlass[FixtureNo] = this.glassInfo.ProductInfo.LCDCode;//玻璃码
                // data_06为NULL，则创建一个
                if (this.glassInfo.GlassState == null)
                {
                    this.glassInfo.GlassState = new GlassState();
                }
                // LCDState缺省值为良品
                if (string.IsNullOrWhiteSpace(this.glassInfo.GlassState.LCDState))
                {
                    this.glassInfo.GlassState.LCDState = "良品";
                }

                #region 不良玻璃检测
                switch (this.glassInfo.GlassState.LCDState)
                {
                    case "待复判":
                        // 如果是FOG AOI提报的不良，需要自动复判成良品
                        this.glassInfo.Exception = GetHBaseDataClass.Instance.GetException(this.glassInfo.ProductInfo.LCDCode);
                        if (this.glassInfo.Exception == null || this.glassInfo.Exception.Count == 0)
                        {
                            break;
                        }
                        IsScan = false;
                        return new NoteResult(NoteState.Error, DataString, "此玻璃已申报不良，\r\n请提交复判！");

                    case "复判重工":
                        IsScan = false;
                        return new NoteResult(NoteState.Error, DataString, "此玻璃已复判重工，\r\n请提交重工组！");

                    case "复判报废":
                        IsScan = false;
                        return new NoteResult(NoteState.Error, DataString, "此玻璃已复判报废，\r\n不能过站！");

                    case "重工报废":
                        IsScan = false;
                        return new NoteResult(NoteState.Error, DataString, "此玻璃已重工报废，\r\n不能过站！");
                }
                #endregion 不良玻璃检测

                #region 漏工序检测
                if (this.glassInfo.GlassState != null)
                {
                    string Procedure = BaseUI.GetLeakProcedure(this.glassInfo.GlassState.LogCode, BaseUI.NowCode, dicProcess);
                    if (Procedure != "")
                    {
                        string msg = "检测到工序 " + Procedure + " 未扫描.";
                        IsScan = false;
                        return new NoteResult(NoteState.NG, DataString, msg);
                    }
                    // 如果是027 组装AOI三合一测试复判良品，则042 AOI复判TP为必过工序(排除本站工序就是042)
                    if (!string.IsNullOrEmpty(this.glassInfo.GlassState.LCDState) && this.glassInfo.GlassState.LCDState == "复判良品" && BaseUI.UI_CurrentProcedureCode != "042" && this.dicProcessRoute.ContainsKey("042") && !this.glassInfo.GlassState.LogCode.Contains("042"))
                    {
                        // 获取玻璃不良信息
                        this.glassInfo.Exception = GetHBaseDataClass.Instance.GetException(this.glassInfo.ProductInfo.LCDCode);
                        if (this.glassInfo.Exception != null && this.glassInfo.Exception.Count != 0)
                        {
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
                            // 027组装AOI三合一测试的复判良品，工序链中包含042 AOI复判TP，而玻璃履历未过042，则提示漏工序
                            if ( lastEx.ProcessCode == "027" && lastEx.ExceptionState == "复判良品")
                            {
                                string msg = "检测到工序 AOI复判TP 未扫描.";
                                IsScan = false;
                                return new NoteResult(NoteState.NG, DataString, msg);
                            }
                        }//if (this.glassInfo.Exception != null && this.glassInfo.Exception.Count != 0)
                    }//if (!string.IsNullOrEmpty(this.glassInfo.GlassState.LCDState) && this.glassInfo.GlassState.LCDState == "复判良品")

                    //工序重复扫描检测
                    bool Isrepeat = BaseUI.CheckProcedure(this.glassInfo.GlassState.LogCode);
                    if (Isrepeat)
                    {
                        if (this.processRepeatEnabled)
                        {
                            this.isRepeat = true;
                        }
                        else
                        {
                            string msg = "当前工序已过站,\r\n请勿重复扫描!";
                            IsScan = false;
                            return new NoteResult(NoteState.NG, DataString, msg);
                        }
                    }
                }
                #endregion 漏工序检测

                #region 相邻工序过站间隔检测
                if (this.glassInfo.GlassState != null && this.processInterval > 0)
                {
                    // 获取上一道工序的过站时间
                    dynamic lastProcess = GetHBaseDataClass.Instance.GetLastProcessInfo(this.glassInfo.ProductInfo.LCDCode, this.glassInfo.GlassState.LogNumber);
                    if (lastProcess != null)
                    {
                        DateTime lastProcessTime = Convert.ToDateTime(lastProcess.outTime.ToString());
                        TimeSpan t1 = DateTime.Now - lastProcessTime;
                        TimeSpan t2 = new TimeSpan(0, this.processInterval, 0);
                        if(t1 < t2)
                        {
                            TimeSpan t = t2 - t1;
                            string m = t.TotalMinutes.ToString("0.0");
                            string msg = "还需" + m + "分钟可过站.";
                            IsScan = false;
                            return new NoteResult(NoteState.NG, DataString, msg);
                        }
                    }
                }
                #endregion 相邻工序过站间隔检测

                #region 对接测试治具
                if (string.IsNullOrEmpty(this.glassInfo.ProductInfo.QRCode))
                {
                    string msg = "该玻璃未绑定客户码!";
                    IsScan = false;
                    return new NoteResult(NoteState.NG, DataString, msg);
                }
                else
                {
                    this.txtQrCode[FixtureNo].Text = this.glassInfo.ProductInfo.QRCode;//客户码
                    this.lstQrCode[FixtureNo] = this.glassInfo.ProductInfo.QRCode;//客户码
                }
                #endregion 对接测试治具

                #region 复判良品和重工良品检测

                if (this.glassInfo.GlassState != null && !string.IsNullOrEmpty(this.glassInfo.GlassState.LCDState))
                {
                    switch (this.glassInfo.GlassState.LCDState)
                    {
                        case "重工良品":
                            //// LOGO测试检测到重工品弹框提示
                            //if (comProcess.SelectedValue != null && comProcess.SelectedValue.ToString() == "037")
                            if (!string.IsNullOrWhiteSpace(this.warnType) && this.warnType.Substring(0,1) == "1")
                            {
                                WarnMessage warnMsg = new WarnMessage("重 工 良 品 ！", MessageBoxButtons.OK, 1, "OK");
                                warnMsg.ShowDialog(this);
                                txtScanCode.Focus();
                            }

                            break;
                        case "复判良品":
                            //  LOGO测试检测到复判良品，需判断申报不良是否包含点线类不良
                            //if (comProcess.SelectedValue != null && comProcess.SelectedValue.ToString() == "037")
                            if (!string.IsNullOrWhiteSpace(this.warnType) && this.warnType.Substring(1, 1) == "1")
                            {
                                // 如果是复测AOI提报的点线类不良，需要弹框提醒
                                this.glassInfo.Exception = GetHBaseDataClass.Instance.GetException(this.glassInfo.ProductInfo.LCDCode);
                                if (this.glassInfo.Exception == null || this.glassInfo.Exception.Count == 0)
                                {
                                    WarnMessage warnMsg = new WarnMessage("复 判 良 品 ！", MessageBoxButtons.OK, 1, "OK");
                                    warnMsg.ShowDialog(this);
                                    txtScanCode.Focus();
                                }
                                else
                                {
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
                                    //if (this.HWCusNumber.Contains(this.glassInfo.ProductInfo.CustomerNumber) && lastEx.ProcessCode == "031" && lastEx.ExceptionState == "复判良品" && lastEx.ExceptionContent.Contains("点"))
                                    if (!string.IsNullOrWhiteSpace(this.warnType) && this.warnType.Substring(2, 1) == "1" && lastEx.ProcessCode == "027" && lastEx.ExceptionState == "复判良品" && lastEx.ExceptionContent.Contains("点"))
                                    {
                                        WarnMessage warnMsg = new WarnMessage("复 判 良 品 ！\r\n（点线类不良）", MessageBoxButtons.OK, 1, "OK");
                                        warnMsg.ShowDialog(this);
                                        txtScanCode.Focus();
                                    }
                                    else
                                    {
                                        WarnMessage warnMsg = new WarnMessage("复 判 良 品 ！", MessageBoxButtons.OK, 1, "OK");
                                        warnMsg.ShowDialog(this);
                                        txtScanCode.Focus();
                                    }
                                }
                            }

                            break;
                    }
                }
                #endregion 复判良品和重工良品检测

            }//if (this.glassInfo.ProductInfo != null)


            // 匹配成功,提交数据
            return new NoteResult(NoteState.OK);
        }

        /// <summary>
        /// 生成发送字符串
        /// </summary>
        /// <returns>string</returns>
        private string GetSendString(string NGState, string NGDesc)
        {
            if (string.IsNullOrEmpty(NGDesc))
            {
                NGDesc = "null";
            }
            string sendString = "RGJT{RGZD;" + txtFixtureCode.Text + ";" + txtProcessIP.Text + ";" + txtQrCode1.Text.Trim() + ";" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";" + NGState + ";" + NGDesc + "}";
            // 过站补扫点
            //sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + "172.16.7.1" + ";;" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#;" + NGState + ";" + NGDesc + "}";
            //sendString = "RGJT{JTBF;" + txtFixtureCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#;" + NGState + ";" + NGDesc + "}";
            return sendString;
        }

        /// <summary>
        /// 生成发送字符串
        /// </summary>
        /// <returns>string</returns>
        private string GetSendString(string NGState, string NGDesc, int FixtureNo)
        {
            if (string.IsNullOrEmpty(NGDesc))
            {
                NGDesc = "null";
            }
            if (FixtureNo == -1) return "";
            string fixtureCode = "", glassCode = "", scanCode = "", sendString = "";
            try
            {
                fixtureCode = this.lstFixtureCode[FixtureNo];
                glassCode = this.lstGlass[FixtureNo].Trim();
                scanCode = this.txtCode[FixtureNo].Text.Trim();

                sendString = "RGJT{RGZD;" + fixtureCode + ";" + txtProcessIP.Text + ";" + glassCode + ";" + scanCode + "|" + scanCode + ";" + txtOpCode.Text.Trim() + ";" + NGState + ";" + NGDesc + "}";
            }
            catch (Exception ex)
            {
                LogHelper.Error(FixtureNo + "#治具生成发送字符串失败", ex);
                ShowResult(NoteState.Error, FixtureNo + "#治具", "生成发送字符串失败!" + ex.Message.ToString());
            }
            // 过站补扫点
            //sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + "172.16.7.1" + ";;" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#;" + NGState + ";" + NGDesc + "}";
            //sendString = "RGJT{JTBF;" + txtFixtureCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#;" + NGState + ";" + NGDesc + "}";
            return sendString;
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
            this.glassInfo = GetHBaseDataClass.Instance.GetGlassState(this.AnalysisCode);//Code1查询结果
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



        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (btnSetting.Text == "设  置")
            {
                btnSetting.Text = "保  存";
                cmbSerialPort.Enabled = true;
                txtFixtureCode.ReadOnly = false;
            }
            else
            {
                string port = "", code = "";
                foreach (KeyValuePair<string, string> item in this.fixtureList)
                {
                    port += item.Key + ",";
                    code += item.Value + ",";
                }
                port = port.TrimEnd(',');
                code = code.TrimEnd(',');
                appConfig.SetConfig("治具串口", port);
                appConfig.SetConfig("治具编码", code);

                btnSetting.Text = "设  置";
                cmbSerialPort.Enabled = false;
                txtFixtureCode.ReadOnly = true;
            }
        }

        private void cmbSerialPort_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSerialPort.SelectedIndex != -1)
            {
                string port = cmbSerialPort.Text;
                if (this.fixtureList.ContainsKey(port))
                {
                    txtFixtureCode.Text = this.fixtureList[port];
                }
                else
                {
                    txtFixtureCode.Clear();
                }
            }
        }


        private void txtFixtureCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (cmbSerialPort.SelectedIndex == -1) return;
            if (e.KeyCode != Keys.Enter) return;
            string port = cmbSerialPort.Text;
            if (string.IsNullOrWhiteSpace(txtFixtureCode.Text))
            {
                if (this.fixtureList.ContainsKey(port))
                {
                    this.fixtureList.Remove(port);
                }
            }
            else
            {
                if (this.fixtureList.ContainsKey(port))
                {
                    this.fixtureList[port] = txtFixtureCode.Text.Trim();
                }
                else
                {
                    this.fixtureList.Add(port, txtFixtureCode.Text.Trim());
                }
            }
            this.cmbSerialPort.Focus();
        }
        #endregion 方法

        #region 不良申报
        /// <summary>
        /// 后台处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWriteData_DoWork(object sender, DoWorkEventArgs e)
        {
            NoteResult rst;
            try
            {
                #region 传参
                object[] val = null;
                if (e.Argument != null)
                {
                    val = e.Argument as object[];
                }
                string code = val[0].ToString();
                string lcd = val[1].ToString();
                string bncid = val[2].ToString();
                string bncname = val[3].ToString();
                string ip = val[4].ToString();
                int pid = Convert.ToInt32(val[5]);
                string order = val[6].ToString();
                #endregion 传参

                //// 查询玻璃过站信息
                //#region 查询玻璃过站信息
                //this.dicGlassInfo = GetHBaseDataClass.Instance.GetGlassProcessInfo(code, BaseUI.UI_CurrentProcedureCode);
                //// 判断是否获取到玻璃信息
                //if (dicGlassInfo == null)
                //{
                //    rst = new NoteResult(NoteState.Error, txtCode1.Text, "获取FOG绑定信息失败!");
                //    e.Result = rst;
                //    return;
                //}
                //#endregion 查询玻璃过站信息

                #region 发送过站数据
                // 生成发送字符串
                string sendCode = GetSendString("1", bncname);
                if (sendCode == "" || bncname == "" || bncname == "null")
                {
                    rst = new NoteResult(NoteState.NG, code, "生成发送字符串失败!");
                    e.Result = rst;
                    return;
                }

                // 发送数据到服务器
                rst = ClientSend(sendCode.Replace("\r", "").Replace("\n", ""));
                if (rst.State == NoteState.OK)
                {
                    bgwWriteData.ReportProgress(50, rst);
                }
                else
                {
                    e.Result = rst;
                    return;
                }
                #endregion 发送过站数据


                string time = BaseUI.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");
                // 调用存储过程，提交数据
                bool isAdded = BaseUI.Add_ProcessFail("正操作", "重工", code, lcd, time, bncid, bncname, "01", ip, pid, order);
                if (isAdded)
                {
                    rst = new NoteResult(NoteState.OK, code, "不良申报成功，不良描述：" + bncname);
                    e.Result = rst;
                }
                else
                {
                    rst = new NoteResult(NoteState.NG, code, "不良信息提交失败!");
                    e.Result = rst;
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error("不良信息提交失败!", ex);
                //e.Result = new NoteResult(NoteState.NG, code, "不良信息提交失败!" + ex.Message);
            }
        }

        /// <summary>
        /// 后台处理程序进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWriteData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;
            NoteResult rst = e.UserState as NoteResult;
            if (rst.State == NoteState.OK)
            {
                this.LastCode1 = txtCode1.Text.Trim().Replace("\r", "").Replace("\n", "");
                this.LCD_Count += 1;
                this.OK_Count += 1;
                txtLCDCount.Text = this.LCD_Count.ToString();
                txtOKCount.Text = this.OK_Count.ToString();
                AddScanNote("0");

                // 添加Note信息
                lvwNote.AddNote(rst.State, rst.Code, rst.Message); ;

                // 后台添加运行日志
                if (this.isWriteLog)
                {
                    Thread threadLog = new Thread(new ParameterizedThreadStart(this.AddAppLog));
                    threadLog.IsBackground = true;
                    threadLog.Start(lvwNote.Items[lvwNote.Items.Count - 1]);
                }

            }
        }

        /// <summary>
        /// 后台处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWriteData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                NoteResult rst = e.Result as NoteResult;
                ShowResult(rst);
                //pnlNote.Visible = true;
                //pnlNote.Dock = DockStyle.Fill;
            }
        }

        #endregion 不良申报
    }
}
