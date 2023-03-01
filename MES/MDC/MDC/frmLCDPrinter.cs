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
using System.Media;
using Utils;
using Utils.Common;
using Utils.HBaseClass;
using Utils.Model;
using Cognex.DataMan.SDK;
//using Cognex.DataMan.SDK.Discovery;

namespace MDC
{
    public partial class frmLCDPrinter : Form
    {
        #region 私有字段
        /// <summary>
        /// 本机IP
        /// </summary>
        private string ipAddress = "";
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
        /// <summary>
        /// 玻璃检测传感器
        /// </summary>
        private SerialPortUtil GlassDetector;
        /// <summary>
        /// 玻璃信息
        /// </summary>
        private GlassInfo glassInfo;
        /// <summary>
        /// Code1扫码器
        /// </summary>
        private ReaderAccessor Code1_Reader;
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
        /// 已初始化玻璃检测
        /// </summary>
        private bool isInitialized = false;
        /// <summary>
        /// 机台编码
        /// </summary>
        private string mouldcode;
        /// <summary>
        /// 玻璃信息查询超时时间
        /// </summary>
        private int searchTimeout;
        /// <summary>
        /// 是否记录程序运行日志
        /// </summary>
        private bool isWriteLog;
        ///// <summary>
        ///// 扫码超时定时器
        ///// </summary>
        //private System.Windows.Forms.Timer tmrTimeOut;
        /// <summary>
        /// 扫码绑定超时时间
        /// </summary>
        private int tBindingTimeout;
        /// <summary>
        /// 开启扫码
        /// </summary>
        private bool isTriggerOn = false;
        /// <summary>
        /// 最大日志行数
        /// </summary>
        private int maxLogCount;
        /// <summary>
        /// 不良报警阈值（单位:片，近50片玻璃中的不良数大于此设定值将播放报警音
        /// </summary>
        private int warningValue;
        /// <summary>
        /// 上一片绑定成功的玻璃的检测信号到达时间（毫秒）
        /// </summary>
        private int LastLCDTime = -1;
        /// <summary>
        /// 玻璃检测信号过滤时间(毫秒，与上一片玻璃检测信号的时间间隔少于此设定值，将被丢弃）
        /// </summary>
        private int LCDFilter;
        /// <summary>
        /// 上一片绑定成功的玻璃的code1码
        /// </summary>
        private string lastCode1 = "";
        /// <summary>
        /// 当前扫描的玻璃的code1码
        /// </summary>
        private string ScanCode1 = "";
        /// <summary>
        /// 最近若干片玻璃的绑定结果记录（字符串的每一位代表一片玻璃的绑定结果，0：OK，1：NG，2：混料）
        /// </summary>
        private string ScanNote = "";
        /// <summary>
        /// 最近50片扫描记录示意图块宽度
        /// </summary>
        private int lblWidth = 4;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 喷码机串口通讯类
        /// </summary>
        private System.IO.Ports.SerialPort PortPrinter;
        /// <summary>
        /// 是否启用玻璃检测串口
        /// </summary>
        private bool GlassDetectorEnabled;
        /// <summary>
        /// 康耐视读头串口通讯类
        /// </summary>
        private Utils.SerialPortUtil PortReader;
        /// <summary>
        /// 扫码头类型：1=基恩士网口连接，2=康耐视网口连接，3=康耐视串口连接
        /// </summary>
        private int ReaderType;
        /// <summary>
        /// 康耐视读头类
        /// </summary>
        //private EthSystemDiscoverer ethSystemDiscoverer;
        //private EthSystemConnector ethSystemConnector;

        //private DataManSystem dataManSystem;


        private Utils.SerialPortUtil TestPrinter;

        private Utils.SerialPortUtil TestCom;

        /// <summary>
        /// 上一片玻璃读取时间
        /// </summary>
        private DateTime lastGlassTime = DateTime.Now;

        #endregion 私有字段

        #region 构造函数
        /// <summary>
        /// frmMain的构造函数
        /// </summary>
        public frmLCDPrinter()
        {
            InitializeComponent();
            // 页面初始化
            txtSPLName.Dock = DockStyle.Fill;
            txtSerialPort.Dock = DockStyle.Fill;
            txtProcess.Dock = DockStyle.Fill;
            txtProcessIP.Dock = DockStyle.Fill;
            txtOpCode.Dock = DockStyle.Fill;
            txtCodeLCD.Dock = DockStyle.Fill;
            txtCodePrint.Dock = DockStyle.Fill;
            txtLCDCount.Dock = DockStyle.Fill;
            txtOKCount.Dock = DockStyle.Fill;
            txtNGCount.Dock = DockStyle.Fill;
            txtRuleCount.Dock = DockStyle.Fill;
            txtReaderAddress.Dock = DockStyle.Fill;
            flpScanNote.Dock = DockStyle.Fill;
            txtYield.Dock = DockStyle.Fill;

            txtSPLName.Clear();
            txtSerialPort.Clear();
            txtProcess.Clear();
            txtProcessIP.Clear();
            txtOpCode.Clear();
            txtCodeLCD.Clear();
            txtCodePrint.Clear();
            txtReaderAddress.Clear();
            txtLCDCount.Text = "0";
            txtOKCount.Text = "0";
            txtNGCount.Text = "0";
            txtRuleCount.Text = "0";
            txtYield.Text = "0.00";
            lvwNote.LostFocus += new EventHandler(lvwNote_LostFocus);

#if DEBUG   // 调试模式，显示调试面板
            this.pnlDebug.Visible = true;
#else
            this.pnlDebug.Visible = false;
#endif
        }

        #endregion 构造函数

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                int width = (flpScanNote.Width - 3) / 25 - 1;
                if (width <= 0)
                {
                    width = 1;
                }
                this.lblWidth = width;

                // 关闭手持扫描枪串口
                Program.CloseDeviceCom();
                // 页面初始化
                Initialize();

                if (this.DialogResult == DialogResult.Cancel)
                {
                    return;
                }


#if DEBUG
                this.TestPrinter = new SerialPortUtil("COM4", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);
                this.TestPrinter.DataReceived += new Utils.DataReceivedEventHandler(TestPrinter_DataReceived);

                this.TestCom = new SerialPortUtil("COM5", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);
                this.TestCom.DataReceived += new Utils.DataReceivedEventHandler(TestCom_DataReceived);
#endif




                // 启动服务
                ClientStart();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", "程序初始化失败!\n" + ex.Message);
                this.Close();
                return;
            }
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.Hide();
                // 停止
                ClientStop();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                // 关闭程序
                Application.Exit();
                System.Environment.Exit(0);
            }
            finally
            {
                //打开手持扫描枪串口
                Program.OpenDeviceCom();
            }
        }

        /// <summary>
        /// 窗体尺寸变化时，刷新显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_SizeChanged(object sender, EventArgs e)
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
            // 停止服务
            ClientStop();
            // 立即清除
            //ClearAll();
            Clear();
            // 页面初始化
            Initialize();
            // 启动服务
            ClientStart();
            // 连接HBase服务器
            //GetHBaseDataClass.Instance.Reconnect();
        }

        /// <summary>
        /// 计算良率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            if (txtLCDCount.Text != "" && txtLCDCount.Text != "0" && txtNGCount.Text != "" && txtRuleCount.Text != "")
            {
                double yield = 100 * (Convert.ToDouble(txtNGCount.Text) + Convert.ToDouble(txtRuleCount.Text)) / Convert.ToDouble(txtLCDCount.Text);
                txtYield.Text = yield.ToString("0.00");
                //if (yield > warningValue)
                //{
                //    PlaySuccess("1");
                //}
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
        /// 初始化窗体信息
        /// </summary>
        private void Initialize()
        {
            try
            {
                // 已初始化玻璃检测
                isInitialized = false;
                // 数据发送成功
                //sendSuccess = true;
                // 玻璃信息查询超时时间
                searchTimeout = appConfig.GetConfigInt("SearchTimeout");
                // 机台编码
                mouldcode = appConfig.GetConfigString("mouldcode");
                // 是否记录程序运行日志
                isWriteLog = appConfig.GetConfigBool("WriteAppLog");
                // 扫码绑定超时时间
                tBindingTimeout = appConfig.GetConfigInt("BindingTimeout");
                // 开启扫码
                isTriggerOn = false;
                // 最大日志行数
                maxLogCount = appConfig.GetConfigInt("MaxLogCount");
                // 不良报警阈值（单位:片，近50片玻璃中的不良数大于此设定值将播放报警音
                warningValue = appConfig.GetConfigInt("WarningValue");
                // 上一片绑定成功的玻璃的检测信号到达时间（毫秒）
                LastLCDTime = -1;
                // 玻璃检测信号过滤时间(毫秒，与上一片玻璃检测信号的时间间隔少于此设定值，将被丢弃）
                LCDFilter = appConfig.GetConfigInt("LCDFilter");
                // 是否启用玻璃检测串口
                GlassDetectorEnabled = appConfig.GetConfigBool("GlassDetectorEnabled");
                // 扫码头类型
                ReaderType = appConfig.GetConfigInt("ReaderType");


                // 当前程序版本
                versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                // 登录人员账号
                txtOpCode.Text = BaseUI.UI_BOOLOGNAME;
                // 当前站点IP
                this.ipAddress = BaseUI.GetLocalIP();
#if DEBUG
                this.ipAddress = "172.20.21.178";
#endif
                
                txtProcessIP.Text = this.ipAddress;


#if !DEBUG
                try
                {
                    // 获取本机IP所在的产线、工序、产线在制品的型号
                    BaseUI.GetLineModelProcedure(this.ipAddress);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex.Message, ex);
                    throw ex;
                }
               
                // 产线名称
                txtSPLName.Text = BaseUI.UI_SPLNAME;
                // 本站工序
                txtProcess.Text = BaseUI.UI_SGXNAME;

                // 获取扫描枪IP
                string[] ReaderIP = BaseUI.GetCodeReaderIP(this.ipAddress);
                if (ReaderIP == null)
                {
                    ShowResult(NoteState.NG, DateTime.Now, this.ipAddress, "本机IP不在自动过站配置表中");
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    return;
                }

                txtRule1.Clear();
                txtReaderAddress.Clear();

                //this.code1Type = scanType[0];
                //txtRule1.Text = dvProcess[0]["Rule_" + this.code1Type].ToString();
                // 1#扫描枪IP
                txtReaderAddress.Text = ReaderIP[0];
                if (ReaderIP[0].Contains("COM"))
                {
                    lblReaderAddress.Text = "扫码枪串口:";
                }

                ////获取工序链,初始化页面
                //NoteResult rst = BindProcess(BaseUI.UI_SPOMJobCode, BaseUI.UI_SPLJOBCODE);
                //if (rst.State != NoteState.Success)
                //{
                //    ShowResult(NoteState.Error, DateTime.Now, "", rst.Message);
                //    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                //    this.Close();
                //    return;
                //}
#else
                //lblReaderAddress.Text = "扫码枪串口:";
                //txtReaderAddress.Text = "COM5";

                lblReaderAddress.Text = "扫码枪IP:";
                txtReaderAddress.Text = "172.20.23.100";
#endif
                //喷码机串口
                txtSerialPort.Text = appConfig.GetConfigString("KGKSerialPort");

                // 程序标题
                this.Text = string.Format("自动读转喷--{0}  {1}（技术支持：深圳市鼎立特科技有限公司）", txtProcess.Text, this.versionName);

                this.Refresh();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", "初始化数据失败." + ex.Message.ToString());
            }

        }
        /// <summary>
        /// 清除所有信息
        /// </summary>
        private void ClearAll()
        {
            this.LCD_Count = 0;
            this.OK_Count = 0;
            this.NG_Count = 0;
            this.RuleError_Count = 0;
            txtLCDCount.Text = "0";
            txtOKCount.Text = "0";
            txtNGCount.Text = "0";
            txtRuleCount.Text = "0";
            txtYield.Text = "0.00";
            this.isInitialized = false;
            // 清空日志
            this.lvwNote.Items.Clear();

            this.lastCode1 = "";
            this.flpScanNote.Controls.Clear();

            txtCodeLCD.Clear();
            txtCodePrint.Clear();
            txtGlassCode.Clear();
        }

        /// <summary>
        /// 清除信息
        /// </summary>
        private void Clear()
        {
            this.isInitialized = false;
            // 清空日志
            this.lvwNote.Items.Clear();

            this.lastCode1 = "";
            this.ScanNote = "";
            this.flpScanNote.Controls.Clear();
            txtCodeLCD.Clear();
            txtCodePrint.Clear();
            txtGlassCode.Clear();
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        private void ClientStart()
        {
            try
            {
                this.btnReset.Enabled = false;

                //this.sendSuccess = true;

                //this.GlassDetector = new AutoSerialPort();
                //// 绑定串口数据接收事件处理程序
                //this.GlassDetector.DataReceived += new Utils.DataReceivedEventHandler(GlassDetector_DataReceived);

                //// 打开玻璃检测传感器串口
                //OpenDeviceCom();

#if DEBUG 
                this.TestCom.OpenPort();
                this.TestPrinter.OpenPort();
#endif


                // 初始化扫码枪
                OpenReader(this.ReaderType);


                // 连接喷码机串口
                ConnectPrinter();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", "系统初始化失败！" + ex.Message.ToString());
            }
            this.btnReset.Enabled = true;
        }


        /// <summary>
        /// 关闭客户端
        /// </summary>
        private void ClientStop()
        {
            try
            {
                this.btnReset.Enabled = false;

                // 关闭玻璃检测传感器串口
                //CloseDeviceCom();
                //this.GlassDetector.Dispose();
                //this.GlassDetector = null;

                // 断开喷码机
                DisconnectPrinter();

                //断开扫码枪
                CloseReader();

#if DEBUG
                this.TestCom.ClosePort();
                this.TestPrinter.ClosePort();
#endif
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", "关闭客户端失败." + ex.Message.ToString());
            }
            this.btnReset.Enabled = true;
        }

        /// <summary>
        /// 连接喷码机
        /// </summary>
        private void ConnectPrinter()
        {
            try
            {
                if (txtSerialPort.Text.Equals(""))
                {
                    ShowResult(NoteState.Error, DateTime.Now, "", "请在config文件中设置喷码机串口号");
                }
                else
                {
                    if (PortPrinter == null)
                    {
                        PortPrinter = new SerialPort(txtSerialPort.Text, 9600, Parity.None, 8, StopBits.One);
                    }
                    else
                    {
                        Utils.Common.CCS3000.ClosePort(ref PortPrinter);//打开前先初始化
                        PortPrinter.PortName = txtSerialPort.Text;
                        PortPrinter.BaudRate = 9600;
                    }
                    if (!PortPrinter.IsOpen)
                    {
                        PortPrinter.Open();
                    }

                    string cmd = "SRC:0:1:1:";
                    if (Utils.Common.CCS3000.SetCmd(cmd, PortPrinter) == Utils.Common.CCS3000.RESULT_ACK)
                    {
                        ShowResult(NoteState.Success, DateTime.Now, txtSerialPort.Text, "喷码机已连接【" + txtSerialPort.Text + "】");
                        LogHelper.Info("喷码机已连接【" + txtSerialPort.Text + "】");
                    }
                    else
                    {
                        ShowResult(NoteState.Error, DateTime.Now, txtSerialPort.Text, "连接喷码机" + txtSerialPort.Text + "失败");
                        LogHelper.Info("连接喷码机" + txtSerialPort.Text + "失败");
                    }
                }
            }
            catch (Exception exp)
            {
                ShowResult(NoteState.Error, DateTime.Now, txtSerialPort.Text, "连接喷码机" + txtSerialPort.Text + "失败." + exp.Message);
                LogHelper.Error("连接喷码机" + txtSerialPort.Text + "失败", exp);
            }
        }

        /// <summary>
        /// 断开喷码机
        /// </summary>
        private void DisconnectPrinter()
        {
            try
            {
                Utils.Common.CCS3000.ClosePort(ref PortPrinter);
            }
            catch (Exception exp)
            {
                ShowResult(NoteState.Error, DateTime.Now, txtSerialPort.Text, "断开喷码机" + txtSerialPort.Text + "失败." + exp.Message);
                LogHelper.Error("断开喷码机" + txtSerialPort.Text + "失败", exp);
            }
        }
        /// <summary>
        /// 跨线程输出结果的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ShowResultCallback(NoteState state, DateTime time, string code, string message);

        /// <summary>
        /// 跨线程输出结果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        private void ShowResult(NoteState state, DateTime time, string code, string message)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                ShowResultCallback d = new ShowResultCallback(ShowResult);
                this.BeginInvoke(d, new object[] { state, time, code, message });
            }
            else
            {
                if (lvwNote.Items.Count >= this.maxLogCount)
                {
                    lvwNote.Items.Clear();
                }
                // 添加Note信息
                lvwNote.AddNote(state, time, code, message);
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
                        break;
                    case NoteState.NG:
                        break;
                    case NoteState.Fail:
                        break;
                    case NoteState.Error:
                        // 弹框提示
                        new FailMessage(message, 5).ShowDialog(this);
                        //txtCode1.Clear();
                        //txtCode2.Clear();
                        //txtGlassCode.Clear();
                        //// 清除串口缓冲区
                        //if (this.GlassDetector != null)
                        //{
                        //    this.GlassDetector.DiscardBuffer();
                        //}
                        break;
                }

            }
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        /// <param name="result">提示结果对象</param>
        private void ShowResult(NoteResult result)
        {
            ShowResult(result.State, result.Time, result.Code, result.Message);
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
                LogHelper.Info(msg);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 初始化扫码枪
        /// </summary>
        /// <param name="ReaderType">扫码枪类型：1=基恩士网口，2=康耐视网口，3=康耐视串口</param>
        private void OpenReader(int ReaderType)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReaderAddress.Text))
                {
                    ShowResult(NoteState.Error, DateTime.Now, "", "扫码枪地址不能为空！");
                    return;
                }
                switch (ReaderType)
                {
                    case 1:
                        if (txtReaderAddress.Text.Contains("COM"))
                        {
                            ShowResult(NoteState.Error, DateTime.Now, txtReaderAddress.Text, "扫码枪类型或地址自动过站配置错误！");
                            return;
                        }
                        //初始化基恩士读头TCP
                        this.Code1_Reader = new ReaderAccessor(txtReaderAddress.Text);
                        this.Code1_Reader.DataArrived += new EventHandler<TCPEventArgs>(Code1_Reader_DataArrived);
                        this.Code1_Reader.ReaderConnected += new EventHandler<TCPEventArgs>(Code1_Reader_ReaderConnected);
                        this.Code1_Reader.ReaderClosed += new EventHandler<TCPEventArgs>(Code1_Reader_ReaderClosed);
                        try
                        {
                            if (!this.Code1_Reader.Connect())
                            {
                                ShowResult(NoteState.Fail, DateTime.Now, txtReaderAddress.Text, "扫码枪连接失败！");
                            }
                            else
                            {
                                //无电眼直接开启扫码枪
                                // 开启扫码枪
                                ReaderTriggerOn(true, this.ReaderType);
                                this.isTriggerOn = true;
                                ShowResult(NoteState.Success, DateTime.Now, txtReaderAddress.Text, "扫码头已开启扫码.");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex.Message, ex);
                            ShowResult(NoteState.Error, DateTime.Now, "扫码枪连接失败！", ex.Message);
                            return;
                        }
                        break;

                    case 2:
                        //if (txtReaderAddress.Text.Contains("COM"))
                        //{
                        //    ShowResult(NoteState.Error, DateTime.Now, txtReaderAddress.Text, "扫码枪类型或地址自动过站配置错误！");
                        //    return;
                        //}
                        //this.ethSystemConnector = new EthSystemConnector(System.Net.IPAddress.Parse(txtReaderAddress.Text));
                        //this.dataManSystem = new DataManSystem(this.ethSystemConnector);
                        //this.dataManSystem.ReadStringArrived += new ReadStringArrivedHandler(dataManSystem_ReadStringArrived);
                        //this.dataManSystem.XmlResultArrived += new XmlResultArrivedHandler(dataManSystem_XmlResultArrived);
                        //this.dataManSystem.SystemConnected += new SystemConnectedHandler(dataManSystem_SystemConnected);
                        //this.dataManSystem.SystemDisconnected += new SystemDisconnectedHandler(dataManSystem_SystemDisconnected);
                        //this.dataManSystem.Connect();
                        if (txtReaderAddress.Text.Contains("COM"))
                        {
                            ShowResult(NoteState.Error, DateTime.Now, txtReaderAddress.Text, "扫码枪类型或地址自动过站配置错误！");
                            return;
                        }
                        //初始化康耐视读头TCP
                        this.Code1_Reader = new ReaderAccessor(txtReaderAddress.Text, 23);
                        this.Code1_Reader.DataArrived += new EventHandler<TCPEventArgs>(Code1_Reader_DataArrived);
                        this.Code1_Reader.ReaderConnected += new EventHandler<TCPEventArgs>(Code1_Reader_ReaderConnected);
                        this.Code1_Reader.ReaderClosed += new EventHandler<TCPEventArgs>(Code1_Reader_ReaderClosed);
                        try
                        {
                            if (!this.Code1_Reader.Connect())
                            {
                                ShowResult(NoteState.Fail, DateTime.Now, txtReaderAddress.Text, "扫码枪连接失败！");
                            }
                            else
                            {
                                //无电眼直接开启扫码枪
                                // 开启扫码枪,康耐视262采用外部触发，自动开启扫码
                                //ReaderTriggerOn(true, this.ReaderType);
                                this.isTriggerOn = true;
                                ShowResult(NoteState.Success, DateTime.Now, txtReaderAddress.Text, "扫码头已开启扫码.");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex.Message, ex);
                            ShowResult(NoteState.Error, DateTime.Now, "扫码枪连接失败！", ex.Message);
                            return;
                        }
                        break;

                    case 3:
                        if (!txtReaderAddress.Text.Contains("COM"))
                        {
                            ShowResult(NoteState.Error, DateTime.Now, txtReaderAddress.Text, "扫码枪类型或地址自动过站配置错误！");
                            return;
                        }
                        //初始化康耐视读头串口
                        this.PortReader = new SerialPortUtil(txtReaderAddress.Text);
                        this.PortReader.DtrEnable = true;
                        this.PortReader.DataReceived += this.PortReader_DataReceived;
                        this.PortReader.Error += new SerialErrorReceivedEventHandler(PortReader_Error);
                        try
                        {
                            this.PortReader.OpenPort();
                            if (!this.PortReader.IsOpen)
                            {
                                ShowResult(NoteState.Fail, DateTime.Now, txtReaderAddress.Text, "扫码枪连接失败！");
                            }
                            else
                            {
                                //无电眼直接开启扫码枪
                                // 开启扫码枪
                                ReaderTriggerOn(true, this.ReaderType);
                                this.isTriggerOn = true;
                                ShowResult(NoteState.Success, DateTime.Now, txtReaderAddress.Text, "扫码头已开启扫码.");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex.Message, ex);
                            ShowResult(NoteState.Error, DateTime.Now, "扫码枪连接失败！", ex.Message);
                            return;
                        }
                        break;
                }
                
                PublicSub.Delay(100);

                ////无电眼直接开启扫码枪
                //// 开启扫码枪
                //ReaderTriggerOn(true, this.ReaderType);
                //this.isTriggerOn = true;
                //ShowResult(NoteState.Success, DateTime.Now, txtReaderAddress.Text, "扫码头已开启扫码.");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", ex.Message);
            }
        }

        //private void dataManSystem_XmlResultArrived(object sender, XmlResultArrivedEventArgs args)
        //{
        //    string data = args.XmlResult;
        //}

        //private void dataManSystem_SystemDisconnected(object sender, EventArgs args)
        //{
        //    ShowResult(NoteState.Fail, DateTime.Now, txtReaderAddress.Text, "扫码枪连接断开");
        //}

        //private void dataManSystem_SystemConnected(object sender, EventArgs args)
        //{
        //    ShowResult(NoteState.Success, DateTime.Now, txtReaderAddress.Text, "扫码枪已连接【" + txtReaderAddress.Text + "】");
        //}

        //private void dataManSystem_ReadStringArrived(object sender, ReadStringArrivedEventArgs args)
        //{
        //    string data = args.ReadString;
        //    //ShowResult(NoteState.Fail, DateTime.Now, txtReaderAddress.Text, "扫码枪连接断开");
        //}

        /// <summary>
        /// 断开扫码枪
        /// </summary>
        private void CloseReader()
        {
            // 关闭扫码枪
            ReaderTriggerOn(false, this.ReaderType);

            switch (ReaderType)
            {
                case 1:
                    if (Code1_Reader != null)
                    {
                        ShowResult(NoteState.Success, DateTime.Now, Code1_Reader.IpAddress, "扫码枪已停止扫描.");
                        this.Code1_Reader.Disconnect();
                        PublicSub.Delay(100);
                    }
                    break;

                case 2:
                    if (Code1_Reader != null)
                    {
                        ShowResult(NoteState.Success, DateTime.Now, Code1_Reader.IpAddress, "扫码枪已停止扫描.");
                        this.Code1_Reader.Disconnect();
                        PublicSub.Delay(100);
                    }
                    //if (this.dataManSystem != null && this.ethSystemConnector != null)
                    //{
                    //    ShowResult(NoteState.Success, DateTime.Now, Code1_Reader.IpAddress, "扫码枪已停止扫描.");
                    //    this.dataManSystem.Disconnect();
                    //    PublicSub.Delay(100);
                    //}
                    break;

                case 3:
                    try
                    {
                        this.PortReader.ClosePort();
                        ShowResult(NoteState.Success, DateTime.Now, Code1_Reader.IpAddress, "扫码枪已停止扫描.");
                        PublicSub.Delay(100);
                    }
                    catch (Exception exp)
                    {
                        ShowResult(NoteState.Error, DateTime.Now, txtSerialPort.Text, "断开扫码枪" + txtReaderAddress.Text + "失败." + exp.Message);
                        LogHelper.Error("断开扫码枪" + txtReaderAddress.Text + "失败", exp);
                    }
                    break;
            }
        }

        /// <summary>
        /// 康耐视扫码枪串口数据错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortReader_Error(object sender, SerialErrorReceivedEventArgs e)
        {
            LogHelper.Error("串口数据接收错误", new Exception(e.EventType.ToString()));
            ShowResult(NoteState.Error, DateTime.Now, txtReaderAddress.Text, "扫码枪" + txtReaderAddress.Text + "数据接收错误.类型：" + e.EventType.ToString()); ;
        }

        /// <summary>
        /// 康耐视扫码枪串口数据接收处理
        /// </summary>
        /// <param name="e"></param>
        private void PortReader_DataReceived(Utils.DataReceivedEventArgs e)
        {
            try
            {
                byte[] data = e.BytesReceived;
                string DataString = (new UTF8Encoding().GetString(data)).Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("|", "/").Replace(";", "；"); ;
                Debug.WriteLine("收到" + "扫描枪数据：" + DataString);

                // 分析处理数据
                if (!string.IsNullOrEmpty(DataString))
                {
                    ScanDataHandler(DataString, txtCodeLCD);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// #1扫描枪已连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Code1_Reader_ReaderConnected(object sender, TCPEventArgs e)
        {
            ShowResult(NoteState.Success, DateTime.Now, txtReaderAddress.Text, "扫码枪已连接【" + txtReaderAddress.Text + "】");
        }

        /// <summary>
        /// #1扫描枪已断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Code1_Reader_ReaderClosed(object sender, TCPEventArgs e)
        {
            ShowResult(NoteState.Fail, DateTime.Now, txtReaderAddress.Text, "扫码枪连接断开");
        }


        /// <summary>
        /// 触发两个扫码枪
        /// </summary>
        /// <param name="readerNum"></param>
        /// <param name="isOn"></param>
        private void ReaderTriggerOn(bool isOn, int readerType)
        {
            try
            {
                switch (readerType)
                {
                    case 1:
                        if (isOn)
                        {
                            this.Code1_Reader.ExecCommand("LON");
                        }
                        else
                        {
                            this.Code1_Reader.ExecCommand("LOFF");
                        }
                        break;

                    case 2:
                        if (isOn)
                        {
                            this.Code1_Reader.SendCommand("+");
                        }
                        else
                        {
                            this.Code1_Reader.SendCommand("-");
                        }
                        break;

                    case 3:

                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Code1扫码枪接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Code1_Reader_DataArrived(object sender, TCPEventArgs e)
        {
            try
            {
                byte[] data = e.DataBytes;
                string DataString = (new UTF8Encoding().GetString(data)).Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");
                Debug.WriteLine("收到" + "扫描枪数据：" + DataString);

                // 分析处理数据
                if (!string.IsNullOrEmpty(DataString))
                {
                    ScanDataHandler(DataString, txtCodeLCD);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 分析处理扫描数据的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ScanDataHandlerCallback(string DataString, TextBox textbox);
        /// <summary>
        /// 分析处理扫描数据
        /// </summary>
        /// <param name="DataString">扫描枪接收字符串</param>
        /// <param name="textbox">填充字符串的TextBox控件</param>
        private void ScanDataHandler(string DataString, TextBox textbox)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (textbox.InvokeRequired)
                {
                    ScanDataHandlerCallback d = new ScanDataHandlerCallback(ScanDataHandler);
                    this.BeginInvoke(d, new object[] { DataString, textbox });
                }
                else
                {
                    // 玻璃检测的当前系统毫秒数
                    int tick = Environment.TickCount;
                    // 玻璃扫描时间
                    DateTime glassTime = DateTime.Now;

                    //if (this.LastLCDTime < 0)
                    //{
                    //    this.LastLCDTime = tick;
                    //}
                    // 玻璃检测信号到达时间
                    DateTime time = DateTime.Now;

                    NoteResult rst = new NoteResult(NoteState.Success);

                    // 解析扫描数据
                    #region 解析扫描数据
                    if (DataString.Contains("OK:") || DataString.Contains("ERROR:"))
                    {
                        rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, "编码不符合规则，请确认扫描枪是否打开了测试模式");
                    }
                    else if(DataString == "ERROR" || DataString.Length >= 5 && DataString.Substring(0,5) == "ERROR")
                    {
                        if(glassTime <= this.lastGlassTime.AddMilliseconds(this.LCDFilter))
                        {
                            rst = new NoteResult(NoteState.Fail, time, DataString, "已忽略!与上片玻璃间隔" + (glassTime - this.lastGlassTime).TotalSeconds + "S");
                        }
                        else
                        {
                            rst = new NoteResult(NoteState.NG, time, DataString, "扫码枪读取失败!");
                        }
                    }
                    else
                    {
                        string[] ds = DataString.Split(':');
                        if (ds.Length == 0)
                        {
                            rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, "编码格式不符合规则");
                        }
                        else
                        {
                            DataString = ds[0].ToString().Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");    //扫描数据解析

                            // YFPCA-SKI65101B18S|F|S1|201121|01F68          YFPCA-SKI65101B18S/F/S1/201121/01F68
                            DataString = DataString.Replace("|", "/").Replace(";", "；");


                            // 基本长度判断
                            if (DataString.Length <= 9)
                            {
                                rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, "默认编码长度至少为10位以上，请确认条码长度");
                            }

                            // 小写字母检查
                            if (BaseUI.LowerCheck(DataString))
                            {
                                if (appConfig.ContainsKey("LowerEnabled"))
                                {
                                    if (!appConfig.GetConfigBool("LowerEnabled"))
                                    {
                                        rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, "条码中禁止包含小写字母");
                                    }
                                }
                                else
                                {
                                    rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, "条码中禁止包含小写字母");
                                }
                            }
                        }//if (ds.Length == 0)
                    }//if (DataString.Contains("OK:") || DataString.Contains("ERROR:"))

                    //// 过滤无效数据
                    //if (!this.isTriggerOn || DataString == "ERROR" || (DataString.Length >= 6 && DataString.Substring(1, 5) == "ERROR")) return;

                    #endregion 解析扫描数据

                    this.lastGlassTime = glassTime;

                    #region 分别处理Code1和Code2数据
                    if (rst.State == NoteState.Success)
                    {
                        // 扫描编码
                        if (textbox == txtCodeLCD)
                        {
                            // 编码已填充到文本框，丢弃
                            if (DataString == textbox.Text) return;

                            // 判断是否重复扫描
                            if (DataString == this.lastCode1)//(this.lastCide1 != "" && DataString.Contains(this.lastCide1))
                            {
                                // 过站扫描点只扫一个编码，重复扫描数据直接丢弃
                                return;
                            }
                            else
                            {
                                this.ScanCode1 = DataString;
                                // 判断是否符合code1编码规则
                                if (txtRule1.Text != "" && DataString.Substring(0, DataString.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : DataString.Length) != txtRule1.Text)
                                {
                                    rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, "编码不符合规则！");
                                }
                                else
                                {
                                    rst = new NoteResult(NoteState.Success, DateTime.Now, DataString, "编码：" + DataString);
                                }
                            }//if (DataString == this.lastCide1)
                        }//if (textbox == txtCode1)
                     }//if (rst.State == NoteState.Success)

                    #endregion 分别处理Code1和Code2数据

                    // 添加Note
                    ShowResult(rst);
                    this.LastLCDTime = tick;
                    textbox.Text = DataString;

                    // 编码检测通过
                    if (rst.State == NoteState.Success)
                    {
                        this.LCD_Count += 1;
                        this.txtLCDCount.Text = this.LCD_Count.ToString();
                        // 校验数据并发送到服务器
                        //CheckAndSendCode();

                        try
                        {
                            this.txtCodePrint.Text = DataString;
                            string cmdSLM1 = string.Format("S2M:2:1:::::1:0:1{0}:", DataString);

                            if (SendMsg(cmdSLM1))
                            {
                                ShowResult(NoteState.OK, DateTime.Now, DataString, string.Format("发送数据到喷码机成功：{0}", cmdSLM1));
                                this.OK_Count += 1;
                                this.txtOKCount.Text = this.OK_Count.ToString();
                                AddScanNote("0");

                            }
                            else
                            {
                                ShowResult(NoteState.Error, DateTime.Now, DataString, string.Format("发送数据到喷码机失败：{0}", cmdSLM1));
                                this.NG_Count += 1;
                                this.txtNGCount.Text = this.NG_Count.ToString();
                                AddScanNote("1");

                            }

                            this.lastCode1 = txtCodeLCD.Text; ;
                            txtCodeLCD.Clear();

                            this.txtCodePrint.Clear();

                            //if (BaseUI.UI_IsBind)
                            //{
                            //    this.lastCode2 = txtCode2.Text;
                            //    txtCode2.Text = "";
                            //    this.sendSuccess = true;

                            //    ReaderTriggerOn(2, false);
                            //    this.isTriggerOn = false;
                            //}
                        }
                        catch (Exception ex)
                        {
                            ShowResult(NoteState.NG, DateTime.Now, DataString, string.Format("发送数据到喷码机失败：{0}", ex.Message));
                            LogHelper.Error("发送数据到喷码机失败.", ex);
                        }
                    }
                    // 编码不符合规则，计入混料
                    else if (rst.State == NoteState.NG)
                    {
                        //if (!BaseUI.UI_IsBind)
                        //{
                            string code1 = txtCodeLCD.Text;
                            ShowResult(NoteState.NG, DateTime.Now, code1, "编码读转喷失败！ ");
                            this.lastCode1 = DataString;
                        //}
                        //else
                        //{
                        //    // 停止扫码枪
                        //    ReaderTriggerOn(2, false);
                        //    if (this.tmrTimeOut != null)
                        //    {
                        //        this.tmrTimeOut.Enabled = false;
                        //        this.tmrTimeOut.Dispose();
                        //    }
                        //    string code1 = txtCodeLCD.Text == "" ? "【" + this.code1Type + "漏扫】" : txtCodeLCD.Text;
                        //    string code2 = txtCodePrint.Text == "" ? "【" + this.code2Type + "漏扫】" : txtCodePrint.Text;
                        //    ShowResult(NoteState.NG, DateTime.Now, code1 + " | " + code2, this.code1Type + "-" + this.code2Type + "编码绑定失败！ ");

                        //    this.isTriggerOn = false;
                        //}
                        //this.sendSuccess = true;
                        txtCodeLCD.Text = "";
                        txtCodePrint.Text = "";

                        this.LCD_Count += 1;
                        this.txtLCDCount.Text = this.LCD_Count.ToString();
                        this.NG_Count += 1;
                        this.txtNGCount.Text = this.NG_Count.ToString();
                        AddScanNote("1");
                        //this.RuleError_Count += 1;
                        //this.txtRuleCount.Text = this.RuleError_Count.ToString();
                        //AddScanNote("2");
                        return;
                    }
                    // 编码重复扫描，不计数
                    else
                    {
                        //if (!BaseUI.UI_IsBind)
                        //{
                            //string code1 = txtCode1.Text;
                            //ShowResult(NoteState.NG, DateTime.Now, code1, this.code1Type + "编码绑定失败！ ");
                            this.lastCode1 = DataString;
                        //}
                        //else
                        //{
                        //    // 停止扫码枪
                        //    ReaderTriggerOn(2, false);
                        //    if (this.tmrTimeOut != null)
                        //    {
                        //        this.tmrTimeOut.Enabled = false;
                        //        this.tmrTimeOut.Dispose();
                        //    }
                        //    string code1 = txtCodeLCD.Text == "" ? "【" + this.code1Type + "漏扫】" : txtCodeLCD.Text;
                        //    string tp = txtCodePrint.Text == "" ? "【" + this.code2Type + "漏扫】" : txtCodePrint.Text;
                        //    ShowResult(NoteState.NG, DateTime.Now, code1 + " | " + tp, "玻璃检测数据异常，已忽略！ ");

                        //    this.isTriggerOn = false;
                        //    this.LCD_Count -= 1;
                        //    this.txtLCDCount.Text = this.LCD_Count.ToString();
                        //}
                        //this.sendSuccess = true;
                        txtCodeLCD.Text = "";
                        txtCodePrint.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 检测当前在制工单是否切换
        /// </summary>
        /// <param name="DataString">扫描编码</param>
        /// <returns>NoteResult</returns>
        private NoteResult ProductionRouteCheck(string DataString)
        {
            NoteResult rst = new NoteResult(NoteState.Success); ;

            #region 从HBase数据库查询过站信息
            try
            {
                this.glassInfo = null;
                CallWithTimeout(GetProcessData1, this.searchTimeout);

                if (this.glassInfo == null)
                {
                    return new NoteResult(NoteState.NG, DataString, "HBase数据库连接异常.");
                }
            }
            catch (TimeoutException tex)
            {
                GetHBaseDataClass.Instance.Reconnect();
                return new NoteResult(NoteState.NG, DataString, "HBase数据库连接超时." + tex.Message.ToString());
            }
            catch (Exception exp)
            {
                return new NoteResult(NoteState.NG, DataString, "查询玻璃绑定信息失败." + exp.Message.ToString());
            }
            #endregion 从HBase数据库查询过站信息

            #region 判断玻璃是否已包装
            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode) && BaseUI.isInPackage(this.glassInfo.ProductInfo.LCDCode))
            {
                return new NoteResult(NoteState.Fail, DataString, "该玻璃已包装，无法过站！");
            }
            #endregion 判断玻璃是否已包装

            return rst;
        }

        /// <summary>
        /// 初始化工序链
        /// </summary>
        /// <param name="ProductOrder">工单编码</param>
        /// <param name="LineCode">产线编码</param>
        private NoteResult BindProcess(string ProductOrder, string LineCode)
        {
            //获取工序链
            DataTable dtProcessTable = BaseUI.GetProduceRouteData(ProductOrder, LineCode);
            // 筛选当前选择的工序信息
            DataView dvProcess = new DataView(dtProcessTable);
            dvProcess.RowFilter = "SGX_JobCode='" + BaseUI.UI_SGXJOBCODE + "'";
            if (dvProcess.Count > 0)
            {
                BaseUI.UI_SGXNO = dvProcess[0]["PR_NoSeq"].ToString();                      //当前工序序号
                BaseUI.UI_CurrentProcedureNo = dvProcess[0]["PR_NoSeq"].ToString();          //当前工序序号
                BaseUI.UI_CurrentProcedureCode = dvProcess[0]["SGX_JobCode"].ToString();      //当前工序编码

                //// 是否允许跨线生产
                //switch (YJ.Common.Utils.StrToInt(dvProcess[0]["SPOM_MixedLine"], 0))
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

                //// 是否允许返检
                //switch (dvProcess[0]["SGX_Rework"].ToString())
                //{
                //    case "02":
                //        this.processRepeatEnabled = true;
                //        break;
                //    default:
                //        this.processRepeatEnabled = false;
                //        break;
                //}

                // 扫描类型
                if (String.IsNullOrEmpty(dvProcess[0]["PR_ScanType"].ToString()))
                {
                    return new NoteResult(NoteState.NG, DateTime.Now, ProductOrder, "请先在MES系统中配置工序【" + txtProcess.Text + "】的扫描类型");
                }
                string PR_ScanType = Enum.GetName(typeof(ScanType), Convert.ToInt32(dvProcess[0]["PR_ScanType"]));
                string[] scanType = PR_ScanType.Split(new string[] { "_and_", "_or_" }, StringSplitOptions.RemoveEmptyEntries);

                // 获取扫描枪IP
                string[] ReaderIP = BaseUI.GetCodeReaderIP(this.ipAddress);
                if (ReaderIP == null)
                {
                    return new NoteResult(NoteState.NG, DateTime.Now, this.ipAddress, "本机IP不在自动过站配置表中");
                }

                txtRule1.Clear();
                txtReaderAddress.Clear();

                //this.code1Type = scanType[0];
                //txtRule1.Text = dvProcess[0]["Rule_" + this.code1Type].ToString();
                // 1#扫描枪IP
                txtReaderAddress.Text = ReaderIP[0];
                return new NoteResult(NoteState.Success);
            }
            else
            {
                return new NoteResult(NoteState.NG, DateTime.Now, ProductOrder, "当前工单不需要过此工序");
            }
        }

        ///// <summary>
        ///// 编码绑定超时定时器程序
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tmrTimeOut_Tick(object sender, EventArgs e)
        //{
        //    System.Windows.Forms.Timer tmr = sender as System.Windows.Forms.Timer;
        //    tmr.Enabled = false;
        //    // 停止扫码枪
        //    ReaderTriggerOn(2, false);

        //    if (txtCodeLCD.Text == "")
        //    {
        //        ShowResult(NoteState.Fail, DateTime.Now, "", this.code1Type + "码漏扫！");
        //    }
        //    if (txtCodePrint.Text == "")
        //    {
        //        ShowResult(NoteState.Fail, DateTime.Now, "", this.code2Type + "码漏扫！");
        //    }
        //    string code1 = txtCodeLCD.Text == "" ? "【" + this.code1Type + "漏扫】" : txtCodeLCD.Text;
        //    string code2 = txtCodePrint.Text == "" ? "【" + this.code2Type + "漏扫】" : txtCodePrint.Text;
        //    ShowResult(NoteState.NG, DateTime.Now, code1 + " | " + code2, this.code1Type + "-" + this.code2Type + "编码绑定失败！ ");

        //    this.isTriggerOn = false;
        //    this.sendSuccess = true;
        //    txtCodeLCD.Text = "";
        //    txtCodePrint.Text = "";
        //    this.NG_Count += 1;
        //    this.txtNGCount.Text = this.NG_Count.ToString();
        //    AddScanNote("1");
        //}

//        /// <summary>
//        /// 打开玻璃检测传感器
//        /// </summary>
//        private void OpenDeviceCom()
//        {
//            try
//            {
//                //打开玻璃检测传感器
//                this.GlassDetector.OpenPort();

////#if !DEBUG
//                if (isGlassDetectorConnected())
//                {
//                    ShowResult(NoteState.Success, DateTime.Now, this.GlassDetector.PortName, "玻璃检测传感器(" + this.GlassDetector.PortName + ")已连接.");
//                }
//                else
//                {
//                    ShowResult(NoteState.Error, DateTime.Now, "", "玻璃检测传感器连接异常!");
//                }
////#endif
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Error(ex.Message, ex);
//                ShowResult(NoteState.Error, DateTime.Now, "", "玻璃检测传感器连接异常!" + ex.Message.ToString());
//            }
//        }
        ///// <summary>
        ///// 关闭玻璃检测传感器串口
        ///// </summary>
        //private void CloseDeviceCom()
        //{
        //    try
        //    {
        //        this.GlassDetector.DiscardBuffer();
        //        this.GlassDetector.ClosePort();
        //        ShowResult(NoteState.Success, DateTime.Now, this.GlassDetector.PortName, "玻璃检测传感器(" + this.GlassDetector.PortName + ")已关闭.");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex.Message, ex);
        //        ShowResult(NoteState.Error, DateTime.Now, this.GlassDetector.PortName, "关闭玻璃检测传感器失败!" + ex.Message.ToString());
        //    }
        //}

        ///// <summary>
        ///// 玻璃检测传感器连接测试
        ///// </summary>
        ///// <returns></returns>
        //private bool isGlassDetectorConnected()
        //{
        //    try
        //    {
        //        byte[] cmd = SerialPortUtil.HexToByte("57AB0101787C");
        //        byte[] rev = new byte[5];
        //        this.GlassDetector.SendCommand(cmd, ref rev, 500);
        //        if (byteToHexStr(rev) == "57AB810187")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex.Message, ex);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 玻璃检测传感器串口数据接收事件
        ///// </summary>
        ///// <param name="e"></param>
        //private void GlassDetector_DataReceived(Utils.DataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        // 打印调试数据
        //        System.Diagnostics.Debug.Print(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  |   检测到玻璃：" + e.HexStringReceived + "\r\n");

        //        byte[] ReDatas = e.BytesReceived;

        //        // 分析处理数据
        //        if (ReDatas != null && ReDatas.Length > 0)
        //        {
        //            NewGlassHandler(ReDatas);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex.Message, ex);
        //        ShowResult(NoteState.Fail, DateTime.Now, this.GlassDetector.PortName, "玻璃检测传感器接收数据失败." + ex.Message.ToString());
        //    }
        //}

        /// <summary>
        /// 分析处理新玻璃信息的委托
        /// </summary>
        /// <param name="Data">玻璃检测数据</param>
        private delegate void NewGlassHandlerCallback(byte[] Data);
        /// <summary>
        /// 分析处理新玻璃信息
        /// </summary>
        /// <param name="Data">玻璃检测数据</param>
        private void NewGlassHandler(byte[] Data)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    NewGlassHandlerCallback d = new NewGlassHandlerCallback(NewGlassHandler);
                    //this.Invoke(d, new object[] { Data });
                    this.BeginInvoke(d, new object[] { Data });
                }
                else
                {
                    // 玻璃检测的当前系统毫秒数
                    int tick = Environment.TickCount;
                    // 玻璃检测信号到达时间
                    DateTime time = DateTime.Now;
                    // 玻璃检测数据字符串
                    string hexString = byteToHexStr(Data);
                    // 不是玻璃检测数据，直接丢弃
                    if (Data.Length < 4 || Data[0] != 53)
                    {
                        ShowResult(NoteState.Fail, time, hexString, "玻璃检测数据异常.");
                        return;
                    }

                    // 玻璃检测数据过滤，间隔少于设定值将被丢弃
                    if (this.LastLCDTime < 0)
                    {
                        this.LastLCDTime = tick;
                    }
                    else
                    {
                        int span = Math.Abs(tick - this.LastLCDTime);
                        if (span < this.LCDFilter)
                        {
                            ShowResult(NoteState.Fail, time, hexString, "与上片玻璃间隔" + span.ToString() + "ms，已忽略！");
                            return;
                        }
                    }

                    //// 若上一片玻璃绑定失败
                    //if (!this.sendSuccess)
                    //{
                    //    // 停止扫码枪
                    //    ReaderTriggerOn(2, false);

                    //    string code1 = txtCodeLCD.Text == "" ? "【" + this.code1Type + "漏扫】" : txtCodeLCD.Text;
                    //    string tp = txtCodePrint.Text == "" ? "【" + this.code2Type + "漏扫】" : txtCodePrint.Text;
                    //    ShowResult(NoteState.NG, time, code1 + " | " + tp, this.code1Type + "-" + this.code2Type + "编码绑定失败！ ");

                    //    this.isTriggerOn = false;
                    //    txtCodeLCD.Text = "";
                    //    txtCodePrint.Text = "";
                    //    this.NG_Count += 1;
                    //    this.txtNGCount.Text = this.NG_Count.ToString();
                    //    AddScanNote("1");
                    //    //Utils.PublicSub.Delay(50);
                    //}

                    // 传感器检测玻璃序号
                    int index = Data[1];

                    // 处理新玻璃数据
                    // 程序刚刚启动，未初始化
                    if (!this.isInitialized)
                    {
                        this.LCD_Count = 0;
                        this.OK_Count = 0;
                        this.NG_Count = 0;
                        this.RuleError_Count = 0;
                        this.txtLCDCount.Text = "0";
                        this.txtOKCount.Text = "0";
                        this.txtNGCount.Text = "0";
                        this.txtRuleCount.Text = "0";
                        this.isInitialized = true;
                    }

                    this.LCD_Count += 1;
                    this.txtLCDCount.Text = this.LCD_Count.ToString();

                    // 判断是否符合检测到玻璃的数据格式
                    if (Data.Length == 4)
                    {
                        ShowResult(NoteState.Success, time, hexString, "检测到新玻璃【" + index.ToString("000") + "】");
                    }
                    else
                    {
                        ShowResult(NoteState.Fail, time, hexString, "检测到新玻璃【" + index.ToString("000") + "】,数据异常！");
                    }

                    //if (this.tmrTimeOut != null)
                    //{
                    //    this.tmrTimeOut.Enabled = false;
                    //    this.tmrTimeOut.Dispose();
                    //}
                    this.LastLCDTime = tick;
                    //this.sendSuccess = false;
                    txtCodeLCD.Text = "";
                    txtCodePrint.Text = "";
                    // 刷新显示
                    this.Refresh();
                    this.isTriggerOn = true;
                    // 开启扫码枪
                    ReaderTriggerOn(true, this.ReaderType);
                }
            }
            catch (Exception ex)
            {
                // 停止扫码枪
                ReaderTriggerOn(false, this.ReaderType);
                this.isTriggerOn = false;
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Fail, DateTime.Now, SerialPortUtil.ByteToHex(Data), "数据解析失败." + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 发送数据到喷码机
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool SendMsg(string cmd)
        {
            try
            {
                if (Utils.Common.CCS3000.SetCmd(cmd, PortPrinter) == Utils.Common.CCS3000.RESULT_ACK)
                {
                    //ShowResult(NoteState.Success, DateTime.Now, cmd, string.Format("发送数据到喷码机成功：{0}", cmd));
                    LogHelper.Info(string.Format("发送数据到喷码机成功：{0}", cmd));
                    return true;
                }
                else
                {
                    //ShowResult(NoteState.NG, DateTime.Now, cmd, string.Format("发送数据到喷码机失败：{0}", cmd));
                    LogHelper.Info(string.Format("发送数据到喷码机失败：{0}", cmd));
                    return false;
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error(string.Format("发送数据到喷码机失败：{0}", cmd),exp);
                return false;
            }
        }

        ///// <summary>
        ///// 发送数据
        ///// </summary>
        //private void CheckAndSendCode()
        //{
        //    NoteResult rst;

        //    // 已过工序和编码合法性检测
        //    rst = ProcessCheck();

        //    if (rst.State == NoteState.Success)
        //    {
        //        // 生成发送字符串，并发送至服务器
        //        rst = BuildAndSend();
        //    }

        //    // 判断编码检测和发送结果
        //    switch (rst.State)
        //    {
        //        case NoteState.OK:  // 发送成功
        //            // 输出提示信息
        //            ShowResult(rst);

        //            this.OK_Count += 1;
        //            this.txtOKCount.Text = this.OK_Count.ToString();
        //            AddScanNote("0");

        //            // 024背光贴合，若需生成并绑定成品码
        //            if (BaseUI.UI_CurrentProcedureCode == "024" && !string.IsNullOrEmpty(this.ipQrBinding))
        //            {
        //                // 背光编码
        //                string blcode = txtCodePrint.Text;
        //                // 委托异步处理成品码绑定
        //                QRCodeBinding(blcode);
        //            }
        //            break;

        //        case NoteState.NG:  // 编码不合法
        //            // 输出提示信息
        //            ShowResult(rst);

        //            this.NG_Count += 1;
        //            this.txtNGCount.Text = this.NG_Count.ToString();
        //            AddScanNote("1");
        //            break;

        //        case NoteState.Fail:    // 混料
        //            // 输出提示信息
        //            ShowResult(NoteState.NG, DateTime.Now, rst.Code, rst.Message);

        //            this.RuleError_Count += 1;
        //            this.txtRuleCount.Text = this.RuleError_Count.ToString();
        //            AddScanNote("2");
        //            break;

        //        case NoteState.Error:   // 服务器异常
        //            // 输出提示信息
        //            ShowResult(NoteState.NG, DateTime.Now, rst.Code, rst.Message);

        //            this.NG_Count += 1;
        //            this.txtNGCount.Text = this.NG_Count.ToString();
        //            AddScanNote("1");
        //            break;
        //    }

        //    this.lastCode1 = txtCodeLCD.Text;
        //    txtCodeLCD.Text = "";

        //    if (BaseUI.UI_IsBind)
        //    {
        //        this.lastCode2 = txtCodePrint.Text;
        //        txtCodePrint.Text = "";
        //        this.sendSuccess = true;

        //        ReaderTriggerOn(2, false);
        //        this.isTriggerOn = false;
        //    }
        //}

        ///// <summary>
        ///// 漏工序和编码合法性检测
        ///// </summary>
        ///// <returns></returns>
        //private NoteResult ProcessCheck()
        //{
        //    #region 局部变量
        //    string code1 = "";//扫描码
        //    string code2 = "";//对应码
        //    string code = "";//绑定码
        //    #endregion 局部变量

        //    txtGlassCode.Clear();

        //    #region 从HBase数据库查询过站信息
        //    code1 = txtCodeLCD.Text;
        //    code = code1;
        //    if (BaseUI.UI_IsBind)
        //    {
        //        code2 = txtCodePrint.Text;
        //        code = code1 + "|" + code2;
        //    }
        //    //code1Code查询结果
        //    if (this.glassInfo == null)
        //    {
        //        return new NoteResult(NoteState.NG, code, "HBase数据库连接异常.");
        //    }

        //    #endregion 从HBase数据库查询过站信息

        //    #region 编码规则检测
        //    // 判断是否符合code1编码规则
        //    if (txtRule1.Text != "" && code1.Substring(0, txtRule1.Text.Length) != txtRule1.Text)
        //    {
        //        return new NoteResult(NoteState.Fail, code1, this.code1Type + "编码不符合规则！");
        //    }
        //    // 判断是否符合Code2编码规则
        //    if (BaseUI.UI_IsBind && txtRule2.Text != "" && code2.Substring(0, txtRule2.Text.Length) != txtRule2.Text)
        //    {
        //        return new NoteResult(NoteState.Fail, code2, this.code2Type + "编码不符合规则！");
        //    }
        //    #endregion 编码规则检测
 
        //    //#region 混线生产检测

        //    //if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionLineCode) & this.glassInfo.ProductInfo.ProductionLineCode != BaseUI.UI_SPLJOBCODE && !this.isMixedLine)
        //    //{
        //    //    string LineName = BaseUI.GetLineName(this.glassInfo.ProductInfo.ProductionLineCode);
        //    //    return new NoteResult(NoteState.Fail, code, "该玻璃生产产线为" + LineName + ",与当前产线不一致。");
        //    //}

        //    //#endregion 混线生产检测

        //    #region 混线生产检测
        //    // 与当前产线不一致，判断是否允许混线
        //    if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionLineCode) && this.glassInfo.ProductInfo.ProductionLineCode != BaseUI.UI_SPLJOBCODE)
        //    {
        //        List<string> lstMixLine = BaseUI.GetMixLine(this.glassInfo.ProductInfo.FinishesCode);
        //        if (lstMixLine.Count == 0)
        //        {
        //            return new NoteResult(NoteState.Fail, code, "该玻璃产线为" + this.glassInfo.ProductInfo.ProductionLineCode + ",禁止混线。");
        //        }
        //        else if (!lstMixLine.Contains(BaseUI.UI_SPLJOBCODE))
        //        {
        //            return new NoteResult(NoteState.Fail, code, "该玻璃产线为" + this.glassInfo.ProductInfo.ProductionLineCode + ",禁止在当前产线生产。");
        //        }
        //    }
        //    #endregion 混线生产检测

        //    #region 型号切换检测
        //    // 不同型号时检测是否允许切换型号
        //    if (this.glassInfo.ProductInfo != null && this.glassInfo.ProductInfo.FinishesModel != txtModel.Text)
        //    {
        //        if (!string.IsNullOrWhiteSpace(this.warnType) && this.warnType.Substring(3, 1) == "0")
        //        {
        //            return new NoteResult(NoteState.Fail, code, "该玻璃型号为" + this.glassInfo.ProductInfo.FinishesModel + ",与当前产线不一致。");
        //        }
        //    }
        //    #endregion 型号切换检测

        //    #region 判断玻璃是否已包装
        //    if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode) && BaseUI.isInPackage(this.glassInfo.ProductInfo.LCDCode))
        //    {
        //        return new NoteResult(NoteState.Fail, code, "该玻璃已包装，无法过站！");
        //    }
        //    #endregion 判断玻璃是否已包装

        //    #region 已过工序检测
        //    if (this.glassInfo.GlassState != null)
        //    {
        //        bool Isrepeat = BaseUI.CheckProcedure(this.glassInfo.GlassState.LogCode);
        //        if (Isrepeat && !this.processRepeatEnabled)
        //        {
        //            return new NoteResult(NoteState.NG, code, "当前工序已扫描,不能再次过站。");
        //        }
        //    }
        //    #endregion 已过工序检测

        //    #region 编码合法性检测
        //    // 第一道工序，检查LCD编码是否已存在
        //    if (BaseUI.UI_SGXNO == "1" && this.glassInfo.ProductInfo != null && !string.IsNullOrWhiteSpace(this.glassInfo.ProductInfo.LCDCode))
        //    {
        //        return new NoteResult(NoteState.NG, code, "当前玻璃码已绑定，请勿多次绑定");
        //    }

        //    // 其他工序，检查是否获取玻璃信息
        //    if (BaseUI.UI_SGXNO != "1" && (this.glassInfo.ProductInfo == null || string.IsNullOrWhiteSpace(this.glassInfo.ProductInfo.LCDCode)))
        //    {
        //        return new NoteResult(NoteState.Error, code, "获取FOG绑定信息失败");
        //    }

        //    if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode))
        //    {
        //        txtGlassCode.Text = this.glassInfo.ProductInfo.LCDCode;
        //    }

        //    // 关键扫描点，检查对应码Code2是否已绑定
        //    if (BaseUI.UI_IsBind)
        //    {
        //        // 查询Code2绑定信息
        //        try
        //        {
        //            this.bindInfo = null;
        //            //this.processData2 = GHDC.GetProductionRouteByCode(code2);
        //            CallWithTimeout(GetProcessData2, this.searchTimeout);
        //            if (this.bindInfo == null)
        //            {
        //                return new NoteResult(NoteState.NG, code, "内部查询玻璃绑定信息失败,如多次出现此错误，请关掉程序重新打开");
        //            }
        //        }
        //        catch (TimeoutException tex)
        //        {
        //            GetHBaseDataClass.Instance.Reconnect();
        //            return new NoteResult(NoteState.NG, code, "HBase数据库连接超时." + tex.Message.ToString());
        //        }
        //        catch (Exception exp)
        //        {
        //            return new NoteResult(NoteState.NG, code, "查询" + this.code2Type + "编码绑定信息失败." + exp.Message.ToString());
        //        }
        //        string bindcode = "";// Code2已绑定的编码
        //        switch (this.code1Type)
        //        {
        //            case "LCD":
        //                bindcode = this.bindInfo.LCDCode;
        //                break;
        //            case "FPC":
        //                bindcode = this.bindInfo.FPCCode;
        //                break;
        //            case "BL":
        //                bindcode = this.bindInfo.BLCode;
        //                break;
        //        }

        //        if (!string.IsNullOrEmpty(bindcode) && bindcode != code1)
        //        {
        //            return new NoteResult(NoteState.NG, code, "当前" + this.code2Type + "编码已绑定，对应" + this.code1Type + "编码：" + bindcode);
        //        }
        //    }//if (BaseUI.UI_IsBind)
        //    #endregion 编码合法性检测

        //    // 以上检测项全部通过，返回OK
        //    return new NoteResult(NoteState.Success, code, "检测通过");
        //}

        ///// <summary>
        ///// 生成并发送字符串
        ///// </summary>
        //private NoteResult BuildAndSend()
        //{
        //    string sendString = "";
        //    string code = "";
        //    // 判断当前站点所属类型
        //    // 过站扫描点
        //    if (!BaseUI.UI_IsBind)
        //    {
        //        sendString = "RGJT{RGZD;" + this.mouldcode + ";" + txtProcessIP.Text + ";" + txtGlassCode.Text.Trim() + ";" + txtCodeLCD.Text.Trim() + "|" + txtCodeLCD.Text.Trim() + ";" + txtOpCode.Text.Trim() + "}";
        //        code = txtCodeLCD.Text.Trim();
        //    }
        //    else
        //    {
        //        // 关键扫描点
        //        sendString = "RGJT{RGZD;" + this.mouldcode + ";" + txtProcessIP.Text + ";" + txtGlassCode.Text.Trim() + ";" + txtCodeLCD.Text.Trim() + "|" + txtCodePrint.Text.Trim() + ";" + txtOpCode.Text.Trim() + "}";
        //        code = txtCodeLCD.Text.Trim() + "|" + txtCodePrint.Text.Trim();
        //    }

        //    try
        //    {
        //        if (client != null && client.connected)
        //        {
        //            client.Send(client.StringToHexString(sendString, System.Text.Encoding.GetEncoding("GBK")));

        //            return new NoteResult(NoteState.OK, DateTime.Now, code, "发送成功：" + sendString);
        //        }
        //        else
        //        {
        //            return new NoteResult(NoteState.Error, DateTime.Now, code, "服务器已断开连接");
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        LogHelper.Error(exp.Message, exp);
        //        return new NoteResult(NoteState.Error, DateTime.Now, code, "发送信息到服务器发生异常." + exp.Message.ToString());
        //    }
        //}

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

            // 混料立即报警
            if (State == "2")
            {
                PlaySuccess("1");
            }
            // 连续10片NG或混料，播放报警提示音
            else if (this.ScanNote.Contains("1111111111") || this.ScanNote.Contains("2222222222"))
            {
                PlaySuccess("1");
                // 强制复位
                btnReset.PerformClick();
            }
            //近50片玻璃中的不良数大于不良报警阈值播放报警音
            else if (PublicSub.SubStringCount(this.ScanNote, "1") + PublicSub.SubStringCount(this.ScanNote, "2") >= this.warningValue)
            {
                PlaySuccess("1");
            }
        }

        /// <summary>
        /// 播放提示音
        /// </summary>
        /// <param name="result"></param>
        private void PlaySuccess(string result)
        {
            try
            {
                SoundPlayer p = new SoundPlayer();
                if (result == "0")//成功
                {
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\成功.wav";
                }
                else if (result == "1")//失败 
                {
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\失败.wav";
                }
                else if (result == "2")//警告
                {
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\警告.wav";
                }
                p.Load();
                p.Play();
            }
            catch (Exception ex)
            {
                LogHelper.Error("提示音播放失败|" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
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
            this.glassInfo = GetHBaseDataClass.Instance.GetGlassState(this.ScanCode1);//Code1查询结果
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern uint GetTickCount();

        ///// <summary>
        ///// 检测编码是否已绑定
        ///// </summary>
        ///// <returns></returns>
        //private NoteResult isCodeBinded(string bindCode, out ProductInfo BindData)
        //{
        //    ProductInfo bindData = null;
        //    bool istimeout = false;  // 读取超时
        //    uint start = GetTickCount();  // 记录开始时间
        //    while (!istimeout)
        //    {
        //        #region 从HBase数据库查询过站信息
        //        try
        //        {
        //            bindData = GetHBaseDataClass.Instance.GetProductInfo(bindCode);//Code2查询结果
        //            if (bindData == null)
        //            {
        //                BindData = null;
        //                return new NoteResult(NoteState.Fail, bindCode, "编码绑定失败.");
        //            }
        //            else if (!string.IsNullOrWhiteSpace(bindData.LCDCode))
        //            {
        //                BindData = bindData;
        //                return new NoteResult(NoteState.OK);
        //            }
        //        }
        //        catch (Exception exp)
        //        {
        //            BindData = null;
        //            return new NoteResult(NoteState.Fail, bindCode, "编码绑定失败." + exp.Message.ToString());
        //        }
        //        #endregion 从HBase数据库查询过站信息

        //        if (GetTickCount() - start > 10000)
        //        {
        //            istimeout = true;
        //        }
        //        Application.DoEvents();
        //    }//while (!found && !istimeout)
        //    BindData = null;
        //    return new NoteResult(NoteState.Fail, bindCode, "编码绑定失败.");
        //}

        #endregion 方法


        #region 测试程序
        /// <summary>
        /// 计数清零按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCountClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            ReaderTriggerOn(false, this.ReaderType);
        }

        /// <summary>
        /// 测试串口数据接收处理
        /// </summary>
        /// <param name="e"></param>
        private void TestCom_DataReceived(Utils.DataReceivedEventArgs e)
        {
            byte[] rev = e.BytesReceived;
            if (byteToHexStr(rev) == "57AB0101787C")
            {
                byte[] cmd = SerialPortUtil.HexToByte("57AB810187");
                this.TestCom.WriteData(cmd);
            }
        }

        private void TestPrinter_DataReceived(Utils.DataReceivedEventArgs e)
        {
            byte[] rev = e.BytesReceived;
            if (byteToHexStr(rev) == "2SRC:0:1:1:3")
            {
                //byte[] cmd = SerialPortUtil.HexToByte("57AB810187");
                //this.TestCom.WriteData(cmd);
            }
            byte[] cmd = new byte[1];
            cmd[0] = 0x06;
            this.TestPrinter.WriteData(cmd);

        }

        /// <summary>
        /// 检测到新玻璃信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGlass_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[4];
            data[0] = 53;
            data[1] = 00;
            data[2] = 13;
            data[3] = 10;
            //this.TestCom.WriteData(data);
            NewGlassHandler(data);
        }
        /// <summary>
        /// 发送1#编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFPC_Click(object sender, EventArgs e)
        {
            ScanDataHandler(txtFPC.Text, txtCodeLCD);
        }
        /// <summary>
        /// 发送2#编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTP_Click(object sender, EventArgs e)
        {
            ScanDataHandler(txtTP.Text, txtCodePrint);
        }

        #endregion 测试程序

    }
}
