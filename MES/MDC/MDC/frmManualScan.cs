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
    public partial class frmManualScan : Form
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
        /// <summary>
        /// Code2扫码类型
        /// </summary>
        private string scanType2;
        /// <summary>
        /// 本机IP
        /// </summary>
        private string ipAddress = "";
        /// <summary>
        /// 成品码绑定的工控机IP地址
        /// </summary>
        private string ipQrBinding = "";
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
        /// <summary>
        /// 华为客户料号（逗号分隔）
        /// </summary>
        private string HWCusNumber = "";
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
        /// 治具串口
        /// </summary>
        private string fixturePort = "COM2";
        /// <summary>
        /// 扫描枪通讯类
        /// </summary>
        private SerialPortUtil scanner;
        /// <summary>
        /// 测试治具通讯类
        /// </summary>
        private SerialPortUtil fixture;
        /// <summary>
        /// 玻璃绑定的客户码
        /// </summary>
        private string qrCode = null;

        /// <summary>
        /// 必须输入治具编号的工序编码
        /// </summary>
        private string[] isNeedFixture = new string[] { "011", "012", "015", "027", "029", "56", "66", "060", "59" };

        #endregion 私有字段

        #region 构造函数
        /// <summary>
        /// frmMain的构造函数
        /// </summary>
        public frmManualScan()
        {
            InitializeComponent();

            txtSPLName.Dock = DockStyle.Fill;
            //txtIPAddress.Dock = DockStyle.Fill;
            //txtPort.Dock = DockStyle.Fill;
            txtProcessIP.Dock = DockStyle.Fill;
            txtOpCode.Dock = DockStyle.Fill;
            txtOrder.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;
            txtScanCode.Dock = DockStyle.Fill;
            txtglassCode.Dock = DockStyle.Fill;
            txtRule1.Dock = DockStyle.Fill;
            txtRule2.Dock = DockStyle.Fill;
            txtCode1.Dock = DockStyle.Fill;
            txtCode2.Dock = DockStyle.Fill;
            txtDigit1.Dock = DockStyle.Fill;
            txtDigit2.Dock = DockStyle.Fill;
            comProcess.Dock = DockStyle.Fill;
            txtLCDCount.Dock = DockStyle.Fill;
            txtOKCount.Dock = DockStyle.Fill;
            txtNGCount.Dock = DockStyle.Fill;
            txtErrorCount.Dock = DockStyle.Fill;
            txtDescribe.Dock = DockStyle.Fill;
            flpScanNote.Dock = DockStyle.Fill;
            txtMouldCode.Dock = DockStyle.Fill;

            txtSPLName.Clear();
            txtIPAddress.Clear();
            txtPort.Clear();
            txtProcessIP.Clear();
            txtOpCode.Clear();
            txtEarlyProcess.Clear();
            txtModel.Clear();
            txtOrder.Clear();
            txtScanCode.Clear();
            txtglassCode.Clear();
            txtRule1.Clear();
            txtRule2.Clear();
            txtCode1.Clear();
            txtCode2.Clear();
            txtDigit1.Clear();
            txtDigit2.Clear();
            txtProcessNumber.Clear();
            txtProcessCode.Clear();
            txtMouldCode.Clear();
            txtLCDCount.Text = "0";
            txtOKCount.Text = "0";
            txtNGCount.Text = "0";
            txtErrorCount.Text = "0";
            txtDescribe.Clear();
            lvwNote.LostFocus += new EventHandler(lvwNote_LostFocus);

            this.tmrAutoReconnect = new System.Timers.Timer(3000);
            this.tmrAutoReconnect.AutoReset = false;
            this.tmrAutoReconnect.Elapsed += new System.Timers.ElapsedEventHandler(tmrAutoReconnect_Elapsed);
        }

        #endregion 构造函数

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManualScan_Load(object sender, EventArgs e)
        {
            try
            {
                pnlNG.Visible = false;
                pnlNote.Visible = true;
                pnlNote.Dock = DockStyle.Fill;

                int width = (flpScanNote.Width - 3) / 25 - 1;
                if (width <= 0)
                {
                    width = 1;
                }
                this.lblWidth = width;

                //dlgMouldCode dlgMould = new dlgMouldCode();
                //DialogResult rst = dlgMould.ShowDialog(this);
                //if (rst == System.Windows.Forms.DialogResult.OK)
                //{
                //    // 设备编码
                //    //txtMouldCode.Text = YJ.Common.Utils.GetAppConfig("mouldcode", "appSettings");
                //    txtMouldCode.Text = dlgMould.MouldCode;
                //}
                //else
                //{
                //    this.Close();
                //}

                if (appConfig.ContainsKey("对接治具类型"))
                {
                    this.fixtureType = appConfig.GetConfigInt("对接治具类型");
                    if (this.fixtureType != 0)
                    {
                        this.scannerPort = appConfig.GetConfigString("扫描枪串口");
                        this.fixturePort = appConfig.GetConfigString("治具串口");

                        List<string> portList = Program.ScanDevice.portList.Keys.ToList<string>();
                        foreach (string item in portList)
                        {
                            if (item != this.scannerPort)
                            {
                                Program.ScanDevice.ClosePort(item);
                            }
                        }

                        this.fixture = new SerialPortUtil(this.fixturePort, SerialPortBaudRates.BaudRate_115200, Parity.None, SerialPortDatabits.EightBits, StopBits.One);
                        this.fixture.DataReceived += new Utils.DataReceivedEventHandler(Fixture_DataReceived);
                        this.fixture.DataWrited += Fixture_DataWrited;
                        this.fixture.OpenPort();
                        if (this.fixture.IsOpen)
                        {
                            ShowResult(NoteState.Success, this.fixturePort, "治具对接串口已打开");
                        }
                        else
                        {
                            ShowResult(NoteState.Error, this.fixturePort, "治具对接串口打开失败");
                        }
                    }
                }

                // 绑定串口数据接收事件处理程序
                Program.ScanDevice.DataReceived += new Utils.DataReceivedEventHandler(ScanDevice_DataReceived);

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

        private void Fixture_DataWrited(DataWritedEventArgs e)
        {
            try
            {

            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                ShowResult(NoteState.Error, this.fixturePort, "接收治具数据失败." + exp.Message.ToString());
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
                string ReString = e.HexStringReceived.Replace(" ", "");
                if (ReString.Length >= 12)
                {
                    string data = ReString.Substring(0, 12);
                    // 治具请求客户码指令
                    if (data == "55AA88770101")
                    {
                        ShowResult(NoteState.Success, this.fixturePort, "获取客户码指令:" + data);
                        if (string.IsNullOrEmpty(this.qrCode))
                        {
                            ShowResult(NoteState.Fail, this.fixturePort, "当前玻璃客户码获取失败");
                        }
                        else
                        {
                            //发送客户码给治具
                            SendQrCodeToFixture();
                        }
                    }
                    // 客户码正确接收应答
                    else if (data == "55AA8877FFFF")
                    {
                        ShowResult(NoteState.Success, this.fixturePort, "治具接收成功:" + data);
                    }
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                ShowResult(NoteState.Error, this.fixturePort, "接收治具数据失败." + exp.Message.ToString());
            }
        }


        /// <summary>
        /// 窗体显示时，连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManualScan_Shown(object sender, EventArgs e)
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
        private void frmManualScan_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;

            if (this.fixture != null && this.fixture.IsOpen)
            {
                this.fixture.ClosePort();
                ShowResult(NoteState.Success, this.fixture.PortName, "治具串口已关闭(" + this.fixture.PortName + ")");
            }
            //CloseDeviceCom();
        }

        /// <summary>
        /// 窗体尺寸变化时，刷新显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManualScan_SizeChanged(object sender, EventArgs e)
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
                pnlNG.Visible = false;
                pnlNote.Visible = true;
                pnlNote.Dock = DockStyle.Fill;
                btnNG.Text = "不良";

                // 断开服务器
                ClientStop();

                // 关闭串口
                Program.CloseDeviceCom();

                // 清空数据
                txtScanCode.Clear();
                txtglassCode.Clear();
                txtCode1.Clear();
                txtCode2.Clear();
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

                // 程序初始化
                Initialize();

                // 打开串口
                Program.OpenDeviceCom();

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

        /// <summary>
        /// 选择工序后下拉框关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comProcess_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comProcess.SelectedIndex == -1) return;
            string processCode = comProcess.SelectedValue.ToString();
            if (this.siteType == SiteType.过站补扫点)
            {
                //if ((BaseUI.UI_NoAccessProcess != null && BaseUI.UI_NoAccessProcess.Contains(processCode)) || (BaseUI.UI_OnlyAccessProcess != null && !BaseUI.UI_OnlyAccessProcess.Contains(processCode)))
                //{
                //    ShowResult(NoteState.NG, "", "当前账号无补扫此工序权限\r\n【" + comProcess.Text + "】");
                //    comProcess.SelectedIndex = -1;
                //    return;
                //}
                this.txtMouldCode.Clear();

                if (BaseUI.UI_AccessProcess == null || BaseUI.UI_AccessProcess.Count == 0)
                {
                    ShowResult(NoteState.NG, "", "当前账号未配置补扫权限");
                    comProcess.SelectedIndex = -1;
                    return;
                }
                else if (!BaseUI.UI_AccessProcess.Contains(processCode))
                {
                    ShowResult(NoteState.NG, "", "当前账号无补扫此工序权限\r\n【" + comProcess.Text + "】");
                    comProcess.SelectedIndex = -1;
                    return;
                }
            }
            // 初始化窗体信息
            PageInit();
        }

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

        /// <summary>
        /// 不良申报按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNG_Click(object sender, EventArgs e)
        {
            if (btnNG.Text == "不良")
            {
                if (comProcess.SelectedIndex == -1)
                {
                    ShowResult(NoteState.NG, "", "未选择本站工序.");
                    return;
                }
                //Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
                //frmNG FormNG = new frmNG();
                //DialogResult rst = FormNG.ShowDialog(this);
                //if (rst == System.Windows.Forms.DialogResult.OK)
                //{
                //    foreach (string fpc in FormNG.NGGlassList.Keys)
                //    {
                //        ShowResult(NoteState.Success, fpc, "不良申报成功，不良描述：" + FormNG.NGDesc);
                //    }
                //}
                //Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;

                btnNG.Text = "返回";
                pnlNote.Visible = false;
                pnlNG.Visible = true;
                pnlNG.Dock = DockStyle.Fill;

                InitBNC();

                txtScanCode.SelectAll();
                txtScanCode.Focus();
            }
            else
            {
                btnNG.Text = "不良";
                pnlNote.Visible = true;
                pnlNG.Visible = false;
                pnlNote.Dock = DockStyle.Fill;
                txtScanCode.SelectAll();
                txtScanCode.Focus();

            }
        }

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
            btnNG.Text = "不良";
            pnlNote.Visible = true;
            pnlNG.Visible = false;
            pnlNote.Dock = DockStyle.Fill;

            // 当前程序版本
            this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // 登录人员账号
            txtOpCode.Text = BaseUI.UI_BOOPNAME + BaseUI.UI_BOOLOGNAME;
            // 玻璃信息查询超时时间
            searchTimeout = appConfig.GetConfigInt("SearchTimeout");

            // 获取华为客户料号
            if (appConfig.ContainsKey("HWCusNumber"))
            {
                this.HWCusNumber = appConfig.GetConfigString("HWCusNumber");
                if (string.IsNullOrEmpty(this.HWCusNumber))
                {
                    appConfig.SetConfig("HWCusNumber", "23020712,23020730,23020737,23020909");
                    this.HWCusNumber = "23020712,23020730,23020737,23020909";
                }
            }
            else
            {
                appConfig.SetConfig("HWCusNumber", "23020712,23020730,23020737,23020909");
                this.HWCusNumber = "23020712,23020730,23020737,23020909";
            }

            // 当前站点IP
            this.ipAddress = BaseUI.GetLocalIP();
#if DEBUG
            //this.ipAddress = "172.20.21.207";    //	LCD清洗投入机	
            //this.ipAddress = "172.20.21.181";
            //this.ipAddress = "192.168.1.150";   //机台补发
            //this.ipAddress = "172.20.23.23";   //机台补发
            //this.ipAddress = "1.1.1.10";
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

            //#if DEBUG
            //            string[] ip = BaseUI.UI_SPLIPAddress.Split('.');
            //            if (ip.Length == 4)
            //            {
            //                txtIPAddress.Text = string.Format("192.168.41.{0}", ip[3]);
            //            }
            //            //txtIPAddress.Text = "192.168.41.3";
            //#endif
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
                btnNG.Enabled = false;
            }
            else
            {
                this.siteType = SiteType.过站扫描点;
                btnNG.Enabled = true;
            }

            // 不良申报
            if (BaseUI.UI_RIGHT.ContainsKey("021212"))
            {
                btnNG.Visible = true;
            }
            else
            {
                btnNG.Visible = false;
                this.tlpInfo.SetCellPosition(this.btnReset, new TableLayoutPanelCellPosition(6, 0));
                this.tlpInfo.SetRowSpan(this.btnReset, 3);
            }

            // 最大日志行数
            maxLogCount = appConfig.GetConfigInt("MaxLogCount");
            // 是否记录程序运行日志
            isWriteLog = appConfig.GetConfigBool("WriteAppLog");

            // 设置下拉框权限
            SetUserRight(this.siteType);

            //根据工序改变颜色
            ChangeColour();

            //初始化工序
            BindProcessComboBox(BaseUI.UI_SPOMJobCode, BaseUI.UI_SPLJOBCODE);
        }

        #region 根据工序改变控件颜色
        /// <summary>
        /// 根据当前站点工序，更改程序配色
        /// </summary>
        private void ChangeColour()
        {
            // 如果是前测工序，则将配色改成深色
            if (BaseUI.UI_SGXJOBCODE == "011")
            {
                this.BackColor = System.Drawing.Color.FromArgb(0, 15, 40);
                txtSPLName.BorderStyle = BorderStyle.FixedSingle;
                txtIPAddress.BorderStyle = BorderStyle.FixedSingle;
                txtPort.BorderStyle = BorderStyle.FixedSingle;
                txtMouldCode.BorderStyle = BorderStyle.FixedSingle;
                txtProcessIP.BorderStyle = BorderStyle.FixedSingle;
                txtOpCode.BorderStyle = BorderStyle.FixedSingle;
                txtEarlyProcess.BorderStyle = BorderStyle.FixedSingle;
                txtScanCode.BorderStyle = BorderStyle.FixedSingle;
                txtglassCode.BorderStyle = BorderStyle.FixedSingle;
                txtRule1.BorderStyle = BorderStyle.FixedSingle;
                txtRule2.BorderStyle = BorderStyle.FixedSingle;
                txtCode1.BorderStyle = BorderStyle.FixedSingle;
                txtCode2.BorderStyle = BorderStyle.FixedSingle;
                txtDigit1.BorderStyle = BorderStyle.FixedSingle;
                txtDigit2.BorderStyle = BorderStyle.FixedSingle;
                txtProcessNumber.BorderStyle = BorderStyle.FixedSingle;
                txtProcessCode.BorderStyle = BorderStyle.FixedSingle;
                pnlNote.BorderStyle = BorderStyle.FixedSingle;
                btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 15, 40);
                foreach (Control c in this.Controls)
                {
                    if (c.GetType().Name == "TableLayoutPanel")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(41, 57, 85);
                    }
                    if (c.GetType().Name == "Panel")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(0, 15, 40);
                    }
                }
                foreach (Control c in this.tlpCode.Controls)
                {
                    if (c.GetType().Name == "TextBox")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(0, 15, 40);
                        c.ForeColor = System.Drawing.Color.FromArgb(246, 246, 246);
                    }
                    if (c.GetType().Name == "Label")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(41, 57, 85);
                        c.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
                    }

                }
                foreach (Control c in this.tlpInfo.Controls)
                {
                    if (c.GetType().Name == "TextBox")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(0, 15, 40);
                        c.ForeColor = System.Drawing.Color.FromArgb(246, 246, 246);
                    }
                    if (c.GetType().Name == "Label")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(41, 57, 85);
                        c.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
                    }
                    if (c.GetType().Name == "ComboBox")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(0, 15, 40);
                        c.ForeColor = System.Drawing.Color.FromArgb(246, 246, 246);
                        (c as ComboBox).FlatStyle = FlatStyle.Flat;
                    }
                    if (c.GetType().Name == "Button")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(0, 15, 40);
                        if (c == btnNG)
                        {
                            c.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            c.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
                        }
                    }
                }
                pnlNote.BackColor = System.Drawing.Color.FromArgb(41, 57, 85);
                foreach (Control c in this.pnlNote.Controls)
                {
                    if (c.GetType().Name == "ListViewNote")
                    {
                        c.BackColor = System.Drawing.Color.FromArgb(0, 15, 40);
                        c.ForeColor = Color.Black;
                    }

                }
            }
        }
        #endregion

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

                // 判断本机IP对应的工序编码是补扫还是正常工序
                if (this.siteType == SiteType.过站补扫点)
                {
                    if (GXCode == null)
                    {
                        // 设置程序标题
                        this.Text = this.siteType.ToString() + "--【请选择本站工序】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
                        // 补扫站点，待选择
                        comProcess.SelectedIndex = -1;
                    }
                    else
                    {
                        // 正常工序站点，选择当前工序
                        try
                        {
                            comProcess.SelectedValue = GXCode;
                        }
                        catch (Exception)
                        {
                            // 设置程序标题
                            this.Text = this.siteType.ToString() + "--【请选择本站工序】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
                            // 补扫站点，待选择
                            comProcess.SelectedIndex = -1;
                            return;
                        }
                        // 初始化窗体信息
                        PageInit();
                    }
                }
                else
                {
                    // 正常工序站点，选择当前工序
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
            string code = txtCode2.Visible ? txtCode1.Text + " | " + txtCode2.Text : txtCode1.Text;
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
                    txtglassCode.Clear();
                    txtCode1.Clear();
                    txtCode2.Clear();
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
        /// 根据站点类别和人员ID，给型号/工序下拉框赋权
        /// </summary>
        /// <param name="processType">站点工序类别</param>
        private void SetUserRight(SiteType processType)
        {
            try
            {
                //string RCodeMaterial = "";
                string RCodeProcess = "";

                switch (processType)
                {
                    case SiteType.关键扫描点:
                        //RCodeMaterial = "02120101";
                        RCodeProcess = "02120102";
                        break;
                    case SiteType.过站扫描点:
                        //RCodeMaterial = "02120201";
                        RCodeProcess = "02120202";
                        break;
                    case SiteType.过站补扫点:
                        //RCodeMaterial = "02120301";
                        RCodeProcess = "02120302";
                        break;
                }
                //工序下拉框权限
                comProcess.Enabled = BaseUI.UI_RIGHT.ContainsKey(RCodeProcess);
            }
            catch (Exception ex)
            {
                LogHelper.Error("获取当前账号操作权限失败", ex);
                ShowResult(NoteState.Error, "", "获取当前账号操作权限失败!" + ex.Message);
            }
        }
        /// <summary>
        /// 根据本站工序初始化窗体信息
        /// </summary>
        private void PageInit()
        {
            if (comProcess.SelectedValue == null) return;
            try
            {
                //btnNG.Text = "不良";
                //pnlNote.Visible = true;
                //pnlNG.Visible = false;
                //pnlNote.Dock = DockStyle.Fill;

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

                    //// 是否允许跨线生产
                    //switch (YJ.Common.Utils.StrToInt(this.dvProcess[0]["SPOM_MixedLine"], 0))
                    //{
                    //    case 0:
                    //        this.isMixedLine = false;
                    //        break;
                    //    case 1:
                    //        this.isMixedLine = true;
                    //        break;
                    //    default:
                    //        this.isMixedLine = false;
                    //        break;
                    //}

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


                    ////如果是TP贴合，则获取成品码绑定的工控机IP地址
                    //if (comProcess.SelectedValue.ToString() == "024")
                    //{
                    //    // 筛选出成品码生成绑定工序032对应的设备IP地址
                    //    DataView dvQr = new DataView(dtProcessTable);
                    //    dvQr.RowFilter = "SGX_JobCode='032'";
                    //    // 背光贴合的下一道工序是032成品码生成(喷码)
                    //    if (dvQr.Count > 0 && YJ.Common.Utils.StrToInt(dvQr[0]["PR_NoSeq"], 0) == YJ.Common.Utils.StrToInt(this.dvProcess[0]["PR_NoSeq"], 0) + 1)
                    //    {
                    //        this.ipQrBinding = dvQr[0]["STW_CardIP"].ToString();
                    //    }
                    //    else
                    //    {
                    //        this.ipQrBinding = "";
                    //    }
                    //}

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
                    txtglassCode.Clear();
                    txtCode1.Clear();
                    txtCode2.Clear();
                    this.AnalysisCode = "";
                    this.LastCode1 = "";

                    if (txtProcessCode.Text == "006") //COG，允许扫LCD或FPC
                    {
                        if (scanType.Length != 2)
                        {
                            ShowResult(NoteState.NG, "", PR_ScanType + ":工序【" + comProcess.Text + "】的扫描类型配置错误");
                            return;
                        }
                        this.scanType1 = scanType[0];
                        this.scanType2 = scanType[1];
                        lblCode2.Visible = true;
                        txtCode2.Visible = true;
                        lblRule2.Visible = true;
                        txtRule2.Visible = true;
                        txtDigit2.Visible = true;
                        lblCode1.Text = this.scanType1 + "编码:";
                        txtRule1.Text = this.dvProcess[0]["Rule_" + this.scanType1].ToString();
                        txtDigit1.Text = this.dvProcess[0]["Len_" + this.scanType1].ToString();
                        lblCode2.Text = this.scanType2 + "编码:";
                        txtRule2.Text = this.dvProcess[0]["Rule_" + this.scanType2].ToString();
                        txtDigit2.Text = this.dvProcess[0]["Len_" + this.scanType2].ToString();
                        btnNG.Enabled = false;
                    }
                    else
                    {
                        // 是否关键工序
                        if (this.isBind)
                        {
                            if (scanType.Length != 2)
                            {
                                ShowResult(NoteState.NG, "", PR_ScanType + ":工序【" + comProcess.Text + "】的扫描类型配置错误");
                                return;
                            }
                            this.scanType1 = scanType[0];
                            this.scanType2 = scanType[1];
                            lblCode2.Visible = true;
                            txtCode2.Visible = true;
                            lblRule2.Visible = true;
                            txtRule2.Visible = true;
                            txtDigit2.Visible = true;
                            lblCode1.Text = this.scanType1 + "编码:";
                            txtRule1.Text = this.dvProcess[0]["Rule_" + this.scanType1].ToString();
                            txtDigit1.Text = this.dvProcess[0]["Len_" + this.scanType1].ToString();
                            lblCode2.Text = this.scanType2 + "编码:";
                            txtRule2.Text = this.dvProcess[0]["Rule_" + this.scanType2].ToString();
                            txtDigit2.Text = this.dvProcess[0]["Len_" + this.scanType2].ToString();
                            btnNG.Enabled = false;
                        }
                        else
                        {
                            if (comProcess.SelectedValue.ToString() != "018" && scanType.Length != 1)
                            {
                                ShowResult(NoteState.NG, "", PR_ScanType + ":工序【" + comProcess.Text + "】的扫描类型配置错误");
                                return;
                            }
                            this.scanType1 = scanType[0];
                            this.scanType2 = "";
                            lblCode2.Visible = false;
                            txtCode2.Visible = false;
                            lblRule2.Visible = false;
                            txtRule2.Visible = false;
                            txtDigit2.Visible = false;
                            lblCode1.Text = this.scanType1 + "编码:";
                            txtRule1.Text = this.dvProcess[0]["Rule_" + this.scanType1].ToString();
                            txtDigit1.Text = this.dvProcess[0]["Len_" + this.scanType1].ToString();
                            btnNG.Enabled = true;
                        }

                        // 如果治具编码为空且为指定工序，则强制弹出治具输入框
                        if (string.IsNullOrEmpty(txtMouldCode.Text.Trim()) && this.isNeedFixture.Contains(txtProcessCode.Text))
                        {
                            GetMouldCode();
                        }
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
                        // 清除玻璃绑定客户码信息
                        this.qrCode = null;

                        // 第1步：站点基本信息检测
                        #region 第1步：站点基本信息检测
                        if (string.IsNullOrWhiteSpace(txtSPLName.Text))
                        {
                            ShowResult(NoteState.Fail, "", "获取设备所属产线失败");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(comProcess.Text))
                        {
                            string msg = "";
                            if (this.siteType == SiteType.过站补扫点)
                            {
                                msg = "请先选择要补扫的工序.";
                                ShowResult(NoteState.Fail, "", msg);
                                return;
                            }
                            //else
                            //{
                            //    msg = "获取当前站点工序失败，请复位后重试.";
                            //    ShowResult(NoteState.Fail, "", msg);
                            //    return;
                            //}
                        }
                        else if (string.IsNullOrEmpty(txtMouldCode.Text.Trim()) && this.isNeedFixture.Contains(txtProcessCode.Text))
                        {
                            ShowResult(NoteState.Fail, "", "请先输入治具编码！");
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

                        //// 小写字母检查LowerEnabled
                        //if (!appConfig.GetConfigBool("LowerEnabled") && BaseUI.LowerCheck(AnalysisCode))
                        //{
                        //    ShowResult(NoteState.Fail, DataString, "条码中禁止包含小写字母");
                        //    return;
                        //}
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

                        // 第3步：判断是否合法的Code2（如果有）
                        #region 第3步：判断是否合法的Code2（如果有）
                        if (txtCode2.Visible && comProcess.SelectedIndex != -1)
                        {
                            // 判断是否合法的Code2
                            rst = isValidCode2(AnalysisCode);
                        }
                        #endregion 第3步：判断是否合法的Code2（如果有）

                        // 第4步：判断是否合法的Code1
                        #region 第4步：判断是否合法的Code1
                        if (rst == null)
                        {
                            // 判断是否合法的Code1
                            rst = isValidCode1(AnalysisCode);
                        }
                        #endregion 第4步：判断是否合法的Code1



                        // 第5步：输出检测结果,发送数据到服务器
                        #region 第5步：输出检测结果,发送数据到服务器
                        switch (rst.State)
                        {
                            case NoteState.Fail:    // 检测到异常
                                // 输出提示信息
                                ShowResult(rst);
                                break;

                            case NoteState.NG:      // 漏工序
                                // 输出提示信息
                                ShowResult(rst);
                                this.LCD_Count += 1;
                                this.NG_Count += 1;
                                txtLCDCount.Text = this.LCD_Count.ToString();
                                txtNGCount.Text = this.NG_Count.ToString();
                                AddScanNote("1");
                                break;

                            case NoteState.Error:   // 混线、不良品、错绑
                                ShowResult(rst);
                                this.LCD_Count += 1;
                                this.Error_Count += 1;
                                txtLCDCount.Text = this.LCD_Count.ToString();
                                txtErrorCount.Text = this.Error_Count.ToString();
                                AddScanNote("2");
                                break;

                            case NoteState.Success:
                                // 返回，等待扫描第二个编码
                                this.AnalysisCode = "";
                                txtScanCode.Clear();
                                txtScanCode.Focus();
                                break;
                            case NoteState.OK:

                                //如果正在不良申报，则自动提交
                                if (btnNG.Text == "返回")
                                {
                                    btnOK.PerformClick();
                                }
                                //如果是人工过站
                                else
                                {
                                    // 生成发送字符串
                                    string sendCode = GetSendString("0", "");
                                    if (sendCode == "")
                                    {
                                        ShowResult(NoteState.NG, "", "生成发送字符串失败!");
                                        return;
                                    }

                                    // 发送数据到服务器
                                    rst = ClientSend(sendCode.Replace("\r", "").Replace("\n", ""));
                                    if (rst.State == NoteState.OK)
                                    {
                                        this.LastCode1 = txtCode1.Text.Trim().Replace("\r", "").Replace("\n", "");
                                        this.LCD_Count += 1;
                                        this.OK_Count += 1;
                                        txtLCDCount.Text = this.LCD_Count.ToString();
                                        txtOKCount.Text = this.OK_Count.ToString();
                                        AddScanNote("0");

                                        //// 024背光贴合，若需生成并绑定成品码
                                        //if (comProcess.SelectedValue.ToString() == "024" && !string.IsNullOrEmpty(this.ipQrBinding))
                                        //{
                                        //    // 背光编码
                                        //    string blcode = txtCode2.Text;
                                        //    //System.Threading.Thread thread = new Thread(new ParameterizedThreadStart(QRCodeBinding));
                                        //    //thread.Name = blcode + "|" + DateTime.Now.ToString("HH:mm:ss");
                                        //    //thread.IsBackground = true;
                                        //    //thread.Start(blcode);
                                        //    //// 委托异步处理成品码绑定
                                        //    //asyncQRCodeBinding async = new asyncQRCodeBinding(QRCodeBinding);
                                        //    //async.BeginInvoke(blcode, null, null);
                                        //    QRCodeBinding(blcode);
                                        //}
                                    }
                                    ShowResult(rst);
                                }
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

        /// <summary>
        /// 发送客户码给治具的委托
        /// </summary>
        private delegate void SendQrCodeToFixtureCallback();
        /// <summary>
        /// 发送客户码给治具
        /// </summary>
        private void SendQrCodeToFixture()
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    SendQrCodeToFixtureCallback d = new SendQrCodeToFixtureCallback(SendQrCodeToFixture);
                    this.BeginInvoke(d);
                }
                else
                {
                    if (string.IsNullOrEmpty(this.qrCode))
                    {
                        ShowResult(NoteState.Error, this.fixturePort, "当前玻璃客户码获取失败");
                        return;
                    }
                    //二维码字符串转成二进制数组
                    byte[] codeBytes = Encoding.UTF8.GetBytes(this.qrCode);
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

                    this.fixture.WriteData(sendBytes);
                    ShowResult(NoteState.Success, this.fixturePort, "已发送客户码(" + this.qrCode + ")给治具");
                    // 清除玻璃绑定客户码信息
                    this.qrCode = null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("发送客户码给治具失败", ex);
                ShowResult(NoteState.Error, "", "发送客户码给治具失败." + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 异步处理成品码绑定的委托
        /// </summary>
        /// <param name="blCode">背光编码</param>
        private delegate void asyncQRCodeBinding(string blCode);

        /// <summary>
        /// 异步检测编码绑定成功后，生成成品码并发送给服务器
        /// </summary>
        /// <param name="blCode"></param>
        private void QRCodeBinding(object blCode)
        {
            string code = blCode.ToString();
            NoteResult rst = null;
            ProductInfo bindData = null;
            // 检测是否绑定成功
            rst = isCodeBinded(code, out bindData);
            if (rst.State != NoteState.OK)
            {
                rst.Message = "背光编码绑定失败！";
                ShowResult(rst);
                return;
            }

            // 背光绑定成功，生成成品码
            // 调用方法生成成品码
            string qrCode = HuaWeiBarCode.GetSNCode(
                bindData.ProductionLineCode, //data_01产线编码
                bindData.LCDCode, //玻璃码
                bindData.FPCCode, //FPC编码
                bindData.TPCode, //TP编码
                bindData.BLCode, //背光编码
                bindData.ICCode, //IC编码
                bindData.CustomerNumber//客户料号
                );

            if (qrCode.Contains("err"))
            {
                rst = new NoteResult(NoteState.NG, DateTime.Now, bindData.BLCode, "成品码生成失败！" + qrCode);
                ShowResult(rst);
                return;
            }
            // 已生成成品码
            rst = new NoteResult(NoteState.Success, DateTime.Now, bindData.BLCode, "生成成品码：" + qrCode);
            ShowResult(rst);
            // 绑定成品码
            SendQRCode(bindData.LCDCode, bindData.BLCode, qrCode);
        }

        /// <summary>
        /// 发送成品码的委托
        /// </summary>
        private delegate void SendQRCodeCallback(string lcdCode, string blCode, string qrCode);

        /// <summary>
        /// 发送成品码到服务器
        /// </summary>
        private void SendQRCode(string lcdCode, string blCode, string qrCode)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    ScanDataHandlerCallback d = new ScanDataHandlerCallback(ScanDataHandler);
                    this.BeginInvoke(d, new object[] { lcdCode, blCode, qrCode });
                }
                else
                {
                    //CPPM{CPM;EC888;172.16.7.243;23020730F988A070198Z000FF000G000Q000000089HL98P467340B||FN0630000005F0401A097S97S0169B;1|1|0;0;;0;20190831002444}
                    //string sendString = "CPPM{CPM;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";" + qrCode + "||" + blCode + ";1|1|0;0;;0;" + DateTime.Now.ToString("yyyyMMddHHmmss") + ";" + txtOpCode.Text.Trim() + "}";
                    string sendString = "RGJT{RGZD;" + txtMouldCode.Text + ";" + this.ipQrBinding + ";" + lcdCode + ";" + blCode + "|" + qrCode + ";" + txtOpCode.Text.Trim() + ";0;null}";
                    // 发送数据到服务器
                    NoteResult rst = ClientSend(sendString.Replace("\r", "").Replace("\n", ""));
                    if (rst.State == NoteState.OK)
                    {
                        rst.State = NoteState.Success;
                        rst.Code = string.Format("{0} | {1}", blCode, qrCode);
                    }
                    ShowResult(rst);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("成品码绑定失败", ex);
                ShowResult(NoteState.Error, "", "成品码绑定失败." + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern uint GetTickCount();

        /// <summary>
        /// 检测编码是否已绑定
        /// </summary>
        /// <returns></returns>
        private NoteResult isCodeBinded(string bindCode, out ProductInfo BindData)
        {
            ProductInfo bindData = null;
            bool istimeout = false;  // 读取超时
            uint start = GetTickCount();  // 记录开始时间
            while (!istimeout)
            {
                #region 从HBase数据库查询过站信息
                try
                {
                    bindData = GetHBaseDataClass.Instance.GetProductInfo(bindCode);//Code2查询结果
                    if (bindData == null)
                    {
                        BindData = null;
                        return new NoteResult(NoteState.Fail, bindCode, "编码绑定失败.");
                    }
                    else if (!string.IsNullOrWhiteSpace(bindData.LCDCode))
                    {
                        BindData = bindData;
                        return new NoteResult(NoteState.OK);
                    }
                }
                catch (Exception exp)
                {
                    BindData = null;
                    return new NoteResult(NoteState.Fail, bindCode, "编码绑定失败." + exp.Message.ToString());
                }
                #endregion 从HBase数据库查询过站信息

                if (GetTickCount() - start > 10000)
                {
                    istimeout = true;
                }
                Application.DoEvents();
            }//while (!found && !istimeout)
            BindData = null;
            return new NoteResult(NoteState.Fail, bindCode, "编码绑定失败.");
        }

        /// <summary>
        /// 判断是否合法的Code2
        /// </summary>
        /// <param name="DataString">扫描编码</param>
        /// <returns>NoteResult</returns>
        private NoteResult isValidCode2(string DataString)
        {
            NoteResult rst = null;
            this.bindInfo = null;
            if (txtCode2.Visible)
            {
                // 判断是否设置规则长度
                if (txtRule2.Text == "")
                {
                    IsScan = false;
                    return new NoteResult(NoteState.Fail, "", "未设置" + this.scanType2 + "编码规则");
                }
                else if (txtDigit2.Text == "")
                {
                    IsScan = false;
                    return new NoteResult(NoteState.Fail, "", "未设置" + this.scanType2 + "编码长度");
                }
                //判断是否匹配Code2编码长度和规则
                // 不匹配Code1规则，匹配Code2规则
                if (DataString.Substring(0, DataString.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : DataString.Length) != txtRule1.Text && DataString.Substring(0, DataString.Length >= txtRule2.Text.Length ? txtRule2.Text.Length : DataString.Length) == txtRule2.Text)
                {
                    if (txtDigit2.Text == "0" || DataString.Length == YJ.Common.Utils.StrToInt(txtDigit2.Text, 0))
                    {
                        txtCode2.Text = DataString;

                        // 关键工序必须先扫描Code1
                        if (this.isBind && string.IsNullOrWhiteSpace(txtCode1.Text))
                        {
                            IsScan = false;
                            return new NoteResult(NoteState.Fail, DataString, "请先扫描" + this.scanType1 + "编码");
                        }

                        // 查询Code2绑定信息
                        try
                        {
                            //异步查询Code1过站信息
                            CallWithTimeout(GetProcessData2, this.searchTimeout);
                            if (this.bindInfo == null)
                            {
                                IsScan = false;
                                return new NoteResult(NoteState.Fail, DataString, "内部查询玻璃绑定信息失败,如多次出现此错误，请关掉程序重新打开");
                            }

                        }
                        catch (TimeoutException tex)
                        {
                            LogHelper.Error("HBase数据库连接超时", tex);
                            IsScan = false;
                            GetHBaseDataClass.Instance.Reconnect();
                            return new NoteResult(NoteState.Fail, DataString, "HBase数据库连接超时." + tex.Message.ToString());
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("查询" + this.scanType2 + "编码绑定信息失败.", exp);
                            IsScan = false;
                            return new NoteResult(NoteState.Fail, txtCode2.Text, "查询" + this.scanType2 + "编码绑定信息失败." + exp.Message.ToString());
                        }
                        string bindcode = "";// Code2已绑定的编码
                        switch (this.scanType1)
                        {
                            case "LCD":
                                bindcode = this.bindInfo.LCDCode;
                                break;
                            case "FPC":
                                bindcode = this.bindInfo.FPCCode;
                                break;
                            case "BL":
                                bindcode = this.bindInfo.BLCode;
                                break;
                        }
                        if (this.isBind)
                        {
                            if (string.IsNullOrEmpty(bindcode))
                            {
                                // 匹配成功，可进行下一步操作
                                rst = new NoteResult(NoteState.OK);
                            }
                            else
                            {
                                if (bindcode != txtCode1.Text)
                                {
                                    rst = new NoteResult(NoteState.Error, txtCode2.Text, "当前" + this.scanType2 + "编码已绑定，对应" + this.scanType1 + "编码：" + bindcode);
                                }
                                else
                                {
                                    rst = new NoteResult(NoteState.OK);
                                }
                            }
                        }
                        else
                        {
                            // 非关键工序可扫描Code2的只有COG补扫
                            if (string.IsNullOrEmpty(bindcode))
                            {
                                // Code2查询不到FOG绑定信息，返回NG
                                rst = new NoteResult(NoteState.Error, txtCode2.Text, "获取FOG绑定信息失败");
                            }
                            else
                            {
                                // COG允许扫FPC进行补扫
                                txtCode1.Text = bindcode;
                                txtglassCode.Text = bindcode;
                                rst = new NoteResult(NoteState.OK);
                            }
                        }//if (this.isBind)
                    }//if匹配长度
                    else
                    {
                        IsScan = false;
                        return new NoteResult(NoteState.Error, DataString, DataString + "\n编码长度" + DataString.Length + "不符合规则");
                    }
                }//if匹配规则
            }//if (txtCode2.Visible)
            return rst;
        }
        string FanProduct = string.Empty;
        /// <summary>
        /// 判断是否合法的Code1
        /// </summary>
        /// <param name="DataString">扫描编码</param>
        /// <returns>NoteResult</returns>
        private NoteResult isValidCode1(string DataString)
        {
            txtglassCode.Text = "";
            this.glassInfo = null;
            this.isRepeat = false;

            #region 从HBase数据库查询过站信息
            try
            {
                //异步查询Code1过站信息
                CallWithTimeout(GetProcessData1, this.searchTimeout = 50000);
                //GetProcessData1();
                if (this.glassInfo == null)
                {
                    IsScan = false;
                    return new NoteResult(NoteState.Fail, DataString, "HBase数据库连接异常.");
                }
                #region 返修品

                if (this.glassInfo.Exception != null && this.glassInfo.Exception.Count > 0)
                {
                    FanProduct = GetExceptionContent(this.glassInfo.Exception);
                }

                if (this.glassInfo.GlassState.Repeat == "True"
                   || (this.glassInfo.GlassState.LCDState.Contains("复判") && this.glassInfo.GlassState.LCDState != "待复判")
                   || (this.glassInfo.GlassState.LCDState.Contains("重工") && this.glassInfo.GlassState.LCDState != "待重工")
                   || (this.glassInfo.Exception != null && this.glassInfo.Exception.Count > 0))
                {
                    WarnMessage warnMsg = new WarnMessage("返 修 品 ！" + FanProduct, MessageBoxButtons.OK, 3, "OK");
                    warnMsg.ShowDialog(this);
                    FanProduct = "";
                    //new NoteResult(NoteState.Fail, DataString, "返修品：" + FanProduct);
                }
                #endregion
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
                // 关键工序,已扫描Code1,
                if (this.isBind && !string.IsNullOrWhiteSpace(txtCode1.Text))
                {
                    // 不匹配Code1和Code2规则
                    if (DataString.Substring(0, DataString.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : DataString.Length) != txtRule1.Text && DataString.Substring(0, DataString.Length >= txtRule2.Text.Length ? txtRule2.Text.Length : DataString.Length) != txtRule2.Text)
                    {
                        IsScan = false;
                        return new NoteResult(NoteState.Error, DataString, DataString + "\n编码格式不符合规则");
                    }
                }

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
                if (DataString.Substring(0, DataString.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : DataString.Length) == txtRule1.Text)
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

            #region 返修品
            //if (this.glassInfo.GlassState.Repeat == "True"
            //   || (this.glassInfo.GlassState.LCDState.Contains("复判") && this.glassInfo.GlassState.LCDState != "待复判")
            //   || (this.glassInfo.GlassState.LCDState.Contains("重工") && this.glassInfo.GlassState.LCDState != "待重工"))
            //{
            //    IsScan = false;
            //    //WarnMessage warnMsg = new WarnMessage("返 修 品 ！", MessageBoxButtons.OK, 3, "OK");
            //    //warnMsg.ShowDialog(this);
            //    return new NoteResult(NoteState.OK, DataString, "返 修 品 ！");
            //}
            #endregion

            #region 重复扫描过滤
            if (!String.IsNullOrEmpty(this.LastCode1) && DataString == this.LastCode1)
            {
                IsScan = false;
                return new NoteResult(NoteState.Fail, DataString, "当前玻璃已扫描,\n请勿重复扫描");
            }
            #endregion 重复扫描过滤

            if (this.glassInfo.ProductInfo != null)
            {
                // 填充玻璃编码
                txtglassCode.Text = this.glassInfo.ProductInfo.LCDCode;//玻璃码

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
                        //// 获取最近时间的不良记录
                        //this.glassInfo.LastExceptionKey = null;
                        //foreach (string key in this.glassInfo.Exception.Keys)
                        //{
                        //    if (this.glassInfo.LastExceptionKey == null)
                        //    {
                        //        this.glassInfo.LastExceptionKey = key;
                        //    }
                        //    else
                        //    {
                        //        if (DateTime.Compare(DateTime.Parse(this.glassInfo.Exception[key].ScanTime), DateTime.Parse(this.glassInfo.Exception[this.glassInfo.LastExceptionKey].ScanTime)) > 0)
                        //        {
                        //            this.glassInfo.LastExceptionKey = key;
                        //        }
                        //    }
                        //}
                        //// 当前不良信息
                        //ExceptionInfo lastEx = this.glassInfo.Exception[glassInfo.LastExceptionKey];
                        //// 如果是FOG AOI提报的不良，需要自动复判成良品
                        //if (lastEx.ProcessCode == "009" && (lastEx.ExceptionState == "待复判"))
                        //{
                        //    GetHBaseDataClass.Instance.ExceptionJudge(ref this.glassInfo, "复判良品", "Admin", "", BaseUI.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"), txtProcessIP.Text);
                        //    //更新lcdState
                        //    this.glassInfo.GlassState.LCDState = "良品";
                        //    //更新Data_06
                        //    GetHBaseDataClass.Instance.UpdateData06(this.glassInfo.ProductInfo.LCDCode, this.glassInfo.GlassState);

                        //    break;
                        //}
                        //else
                        //{
                        IsScan = false;
                        return new NoteResult(NoteState.Error, DataString, "此玻璃已申报不良，\r\n请提交复判！");
                    //}

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
                            if (lastEx.ProcessCode == "027" && lastEx.ExceptionState == "复判良品")
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
                        if (t1 < t2)
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
                if (this.fixtureType == 1)
                {
                    this.qrCode = this.glassInfo.ProductInfo.QRCode;
                    if (string.IsNullOrEmpty(this.qrCode))
                    {
                        string msg = "该玻璃未绑定客户码!";
                        IsScan = false;
                        return new NoteResult(NoteState.NG, DataString, msg);
                    }
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
                            if (!string.IsNullOrWhiteSpace(this.warnType) && this.warnType.Substring(0, 1) == "1")
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

            // 不是绑定工序，扫了Code1直接提交
            if (!this.isBind)
            {
                return new NoteResult(NoteState.OK);
            }
            else
            {
                // 匹配成功，等待扫描第二个码
                return new NoteResult(NoteState.Success);
            }
        }

        private string GetExceptionContent(Dictionary<string, ExceptionInfo> Exception)
        {
            List<ExceptionInfo> Ents = new List<ExceptionInfo>();
            foreach (var item in Exception)
            {
                Ents.Add(item.Value);
            }
            var Ent = Ents.Max(t => t.ExceptionKey);
            var KeyEnt = Ents.Where(t => t.ExceptionKey == Ent).FirstOrDefault();


            //var EntException = Ents.Where(t => t.ExceptionKey == Ent).FirstOrDefault();
            //Ents.Where(t => t.ExceptionKey == Ent).FirstOrDefault().JudgeContent;
            return KeyEnt.JudgeContent + KeyEnt.ExceptionContent;
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
            string sendString = "";

            // 判断当前站点所属类型
            switch (this.siteType)
            {
                // 过站扫描点
                //RGJT{RGZD;E888;172.16.12.33;;LCD202101010001|LCD202101010001;admin;0;null}
                case SiteType.过站扫描点:
                    sendString = "RGJT{RGZD;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";" + txtglassCode.Text.Trim() + ";" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";" + NGState + ";" + NGDesc + "}";
                    break;

                // 关键扫描点
                case SiteType.关键扫描点:
                    sendString = "RGJT{RGZD;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";" + txtglassCode.Text.Trim() + ";" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";" + NGState + ";" + NGDesc + "}";
                    break;

                // 过站补扫点
                default:
                    // 扫描两个编码，补扫关键工序
                    if (this.isBind)
                    {
                        // 补扫TP贴合
                        if (txtProcessCode.Text == "018")//TP贴合
                        {
                            sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";TF;" + NGState + ";" + NGDesc + "}";
                        }
                        else if (txtProcessCode.Text == "032")//54位成品喷码补发
                        {
                            sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";CP;" + NGState + ";" + NGDesc + "}";
                        }
                        else
                        {
                            sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#;" + NGState + ";" + NGDesc + "}";
                        }

                    }
                    // 只扫描一个编码，补扫过站工序
                    else
                    {
                        //sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + "172.16.7.1" + ";;" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#;" + NGState + ";" + NGDesc + "}";
                        sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#;" + NGState + ";" + NGDesc + "}";
                    }
                    break;
            }
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

        ///// <summary>
        ///// 获取Code1的过站信息
        ///// </summary>
        //private void GetProcessData1()
        //{
        //    this.processData = GetHBaseDataClass.Instance.GetProductionRouteByCode(this.AnalysisCode);//Code1查询结果
        //}
        ///// <summary>
        ///// 获取Code2的过站信息
        ///// </summary>
        //private void GetProcessData2()
        //{
        //    this.processData2 = GetHBaseDataClass.Instance.GetProductionRouteByCode(this.AnalysisCode);//Code1查询结果
        //}
        /// <summary>
        /// 获取Code1对应的玻璃基本信息和当前状态
        /// </summary>
        private void GetProcessData1()
        {
            //this.glassInfo = GetHBaseDataClass.Instance.GetGlassState(this.AnalysisCode);//Code1查询结果
            this.glassInfo = GetHBaseDataClass.Instance.GetGlassInfo(this.AnalysisCode);//Code1查询结果
        }
        /// <summary>
        /// 获取Code2的过站信息
        /// </summary>
        private void GetProcessData2()
        {
            this.bindInfo = GetHBaseDataClass.Instance.GetProductInfo(this.AnalysisCode);//Code2查询结果
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
        #endregion 方法

        #region 不良申报
        /// <summary>
        /// 初始化不良项
        /// </summary>
        private void InitBNC()
        {
            txtDescribe.Clear();
            flpNG.Controls.Clear();
            // 添加不良项列表
            Dictionary<int, string> BNCList = BaseUI.GetDefaultBNCItem(BaseUI.UI_CurrentProcedureCode);
            //int width = 0;
            List<CheckBox> chklst = new List<CheckBox>();
            foreach (int key in BNCList.Keys)
            {
                CheckBox chk = new CheckBox()
                {
                    Appearance = System.Windows.Forms.Appearance.Button,
                    //AutoSize = true,
                    AutoSize = false,
                    Width = 206,
                    Height = 45,
                    AutoEllipsis = true,

                    Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                    ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85))))),
                    Location = new System.Drawing.Point(3, 3),
                    Margin = new System.Windows.Forms.Padding(3, 8, 3, 8),
                    UseVisualStyleBackColor = true,
                    Name = key.ToString(),
                    Text = BNCList[key],
                    TextAlign = ContentAlignment.MiddleCenter
                };
                //this.flpNG.Controls.Add(chk);
                chk.CheckStateChanged += this.BNC_CheckStateChanged;
                chklst.Add(chk);
                //if (chk.Width > width)
                //{
                //    width = chk.Width;
                //}
            }


            //bool isSelected = false;
            this.flpNG.Controls.AddRange(chklst.ToArray());

            if (this.flpNG.Controls.Count > 0)
            {
                (this.flpNG.Controls[0] as CheckBox).Checked = true;
            }
            //foreach (CheckBox chk in this.flpNG.Controls)
            //{
            //    int height = chk.Height;
            //    chk.AutoSize = false;
            //    chk.Width = width;
            //    chk.Height = height;
            //    if (!isSelected)
            //    {
            //        chk.Checked = true;
            //        isSelected = true;
            //    }
            //}

            this.Refresh();
            txtScanCode.SelectAll();
            txtScanCode.Focus();
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
            if (txtCode1.Text == "" || txtglassCode.Text == "")
            {
                string msg = "请添加不良玻璃编码！";
                this.failMsg = new FailMessage(msg, 3);
                this.failMsg.ShowDialog(this);
                return;
            }

            if (txtDescribe.Text == "")
            {
                string msg = "请选择不良项！";
                this.failMsg = new FailMessage(msg, 3);
                this.failMsg.ShowDialog(this);
                return;
            }

            string code = txtCode1.Text; //FPC码
            string lcd = txtglassCode.Text; //玻璃码

            btnOK.Enabled = false;
            btnCancel.Enabled = false;
            //txtError.Text = "正在提交，请稍候...";

            // 数据提交后台处理
            bgwWriteData.RunWorkerAsync(new object[] { code, lcd, txtDescribe.Tag.ToString(), txtDescribe.Text, txtProcessIP.Text, YJ.Common.TypeConverter.StrToInt(BaseUI.UI_BOOPID), txtOrder.Text });
        }

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
            btnOK.Enabled = true;
            btnCancel.Enabled = true;

            if (e.Result != null)
            {
                NoteResult rst = e.Result as NoteResult;
                ShowResult(rst);
                btnNG.Text = "不良";
                pnlNote.Visible = true;
                pnlNG.Visible = false;
                pnlNote.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnNG.Text = "不良";
            pnlNote.Visible = true;
            pnlNG.Visible = false;
            pnlNote.Dock = DockStyle.Fill;

            this.AnalysisCode = "";
            txtScanCode.Clear();
            txtglassCode.Clear();
            txtCode1.Clear();
            txtCode2.Clear();
            txtScanCode.Focus();
            // 清除扫码枪串口接收缓冲区
            //Program.ScanDevice.DiscardInBuffer();
            Program.ScanDevice.DiscardBuffer();
        }


        #endregion 不良申报

        #region 治具编码
        /// <summary>
        /// 获取治具编号
        /// </summary>
        private void GetMouldCode()
        {
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
            try
            {
                dlgMouldCode dlgMould = new dlgMouldCode(this.txtMouldCode.Text, txtProcessCode.Text);
                DialogResult rst = dlgMould.ShowDialog(this);
                if (rst == System.Windows.Forms.DialogResult.OK)
                {
                    // 设备编码
                    //txtMouldCode.Text = YJ.Common.Utils.GetAppConfig("mouldcode", "appSettings");
                    txtMouldCode.Text = dlgMould.MouldCode;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("治具编码获取失败!", ex);
            }
            finally
            {
                Program.ScanDevice.DataReceived += new Utils.DataReceivedEventHandler(ScanDevice_DataReceived);
                txtScanCode.Focus();
                txtScanCode.SelectAll();
            }
        }

        private void txtMouldCode_Click(object sender, EventArgs e)
        {
            GetMouldCode();
        }
        #endregion 治具编码
    }
}
