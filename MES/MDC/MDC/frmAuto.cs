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
using Utils.HBaseClass;
using Utils.Model;

namespace MDC
{
    public partial class frmAuto : Form
    {
        #region 私有字段
        /// <summary>
        /// 数据上传客户端
        /// </summary>
        private Client client;
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
        private AutoSerialPort GlassDetector;
        /// <summary>
        /// 玻璃信息
        /// </summary>
        private GlassInfo glassInfo;
        /// <summary>
        /// 待绑定条码信息
        /// </summary>
        private ProductInfo bindInfo;
        /// <summary>
        /// Code1类型
        /// </summary>
        private string code1Type = "";
        /// <summary>
        /// Code2类型
        /// </summary>
        private string code2Type = "";
        /// <summary>
        /// Code1扫码器
        /// </summary>
        private ReaderAccessor Code1_Reader;
        /// <summary>
        /// Code2扫码器
        /// </summary>
        private ReaderAccessor Code2_Reader;
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
        /// 数据发送成功
        /// </summary>
        private bool sendSuccess = true;
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
        /// <summary>
        /// 扫码超时定时器
        /// </summary>
        private System.Windows.Forms.Timer tmrTimeOut;
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
        /// 上一片绑定成功的玻璃的Code2码
        /// </summary>
        private string lastCode2 = "";
        /// <summary>
        /// 当前扫描的玻璃的code1码
        /// </summary>
        private string ScanCode1 = "";
        /// <summary>
        /// 当前扫描的玻璃的Code2码
        /// </summary>
        private string ScanCode2 = "";        
        /// <summary>
        /// 最近若干片玻璃的绑定结果记录（字符串的每一位代表一片玻璃的绑定结果，0：OK，1：NG，2：混料）
        /// </summary>
        private string ScanNote = "";
        /// <summary>
        /// 最近50片扫描记录示意图块宽度
        /// </summary>
        private int lblWidth = 4;
        /// <summary>
        /// 自动重连定时器
        /// </summary>
        private System.Timers.Timer tmrAutoReconnect;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        ///// <summary>
        ///// 是否允许混线生产
        ///// </summary>
        //private bool isMixedLine = false;
        /// <summary>
        /// 标当前操作是否为返检
        /// </summary>
        private bool isRepeat = false;
        /// <summary>
        /// 当前工序是否允许返检
        /// </summary>
        private bool processRepeatEnabled = false;
        /// <summary>
        /// 工序路由列表
        /// </summary>
        private Dictionary<string, string> dicProcessRoute;
        /// <summary>
        /// 当前已过工序的Dictionary
        /// </summary>
        private Dictionary<string, string> dicProcess;

        /// <summary>
        /// 接收服务器IP
        /// </summary>
        private string ip = null;
        /// <summary>
        /// 接收服务器端口
        /// </summary>
        private string port = "-1";

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
        /// 成品码绑定的工控机IP地址
        /// </summary>
        private string ipQrBinding = "";

        /// <summary>
        /// 1#扫描枪心跳计数
        /// </summary>
        private int Reader1Heatbeat = 0;
        /// <summary>
        /// 2#扫描枪心跳计数
        /// </summary>
        private int Reader2Heatbeat = 0;

        #endregion 私有字段

        #region 构造函数
        /// <summary>
        /// frmMain的构造函数
        /// </summary>
        public frmAuto()
        {
            InitializeComponent();
            // 页面初始化
            txtSPLName.Dock = DockStyle.Fill;
            txtIPAddress.Dock = DockStyle.Fill;
            txtPort.Dock = DockStyle.Fill;
            txtProcess.Dock = DockStyle.Fill;
            txtProcessIP.Dock = DockStyle.Fill;
            txtOpCode.Dock = DockStyle.Fill;
            txtRule1.Dock = DockStyle.Fill;
            txtRule2.Dock = DockStyle.Fill;
            txtDigit1.Dock = DockStyle.Fill;
            txtDigit2.Dock = DockStyle.Fill;
            txtCode1.Dock = DockStyle.Fill;
            txtCode2.Dock = DockStyle.Fill;
            txtLCDCount.Dock = DockStyle.Fill;
            txtOKCount.Dock = DockStyle.Fill;
            txtNGCount.Dock = DockStyle.Fill;
            txtRuleCount.Dock = DockStyle.Fill;
            txtCode1ReaderIP.Dock = DockStyle.Fill;
            txtCode2ReaderIP.Dock = DockStyle.Fill;
            txtStartTime.Dock = DockStyle.Fill;
            txtOrder.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;
            flpScanNote.Dock = DockStyle.Fill;

            txtSPLName.Clear();
            txtIPAddress.Clear();
            txtPort.Clear();
            txtProcess.Clear();
            txtProcessIP.Clear();
            txtOpCode.Clear();
            txtCode1.Clear();
            txtCode2.Clear();
            txtCode1ReaderIP.Clear();
            txtCode2ReaderIP.Clear();
            txtStartTime.Clear();
            txtOrder.Clear();
            txtModel.Clear();
            txtLCDCount.Text = "0";
            txtOKCount.Text = "0";
            txtNGCount.Text = "0";
            txtRuleCount.Text = "0";
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

                // 连接服务器
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
            GetHBaseDataClass.Instance.Reconnect();
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
                sendSuccess = true;
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

                // 当前程序版本
                versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                // 登录人员账号
                txtOpCode.Text = BaseUI.UI_BOOLOGNAME;
                // 当前站点IP
                this.ipAddress = BaseUI.GetLocalIP();
#if DEBUG
                this.ipAddress = "172.20.23.61";
                this.ipAddress = "192.168.1.221";
#endif

                txtProcessIP.Text = this.ipAddress;

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

#if DEBUG
                //string[] ip = BaseUI.UI_SPLIPAddress.Split('.');
                //if (ip.Length == 4)
                //{
                //    txtIPAddress.Text = string.Format("192.168.41.{0}", ip[3]);
                //}
#endif

                // 上报服务器端口号
                txtPort.Text = BaseUI.UI_SPLPORT;

                this.ip = txtIPAddress.Text;
                this.port = txtPort.Text;
                
                // 产线名称
                txtSPLName.Text = BaseUI.UI_SPLNAME;
                // 本站工序
                txtProcess.Text = BaseUI.UI_SGXNAME;

                //获取工序链,初始化页面
                NoteResult rst = BindProcess(BaseUI.UI_SPOMJobCode, BaseUI.UI_SPLJOBCODE);
                if (rst.State != NoteState.Success)
                {
                    ShowResult(NoteState.Error, DateTime.Now, "", rst.Message);
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    return;
                }

                // 机台IP设置的弹框提醒类型
                this.warnType = BaseUI.GetDeviceWarnType(this.ipAddress);

                // 相邻工序过站间隔时间
                this.processInterval = BaseUI.GetProcessInterval(this.ipAddress);

                // 程序标题
                this.Text = string.Format("自动过站点--{0}  {1}", txtProcess.Text, this.versionName);

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
            this.lastCode2 = "";
            this.ScanNote = "";
            this.flpScanNote.Controls.Clear();

            txtCode1.Clear();
            txtCode2.Clear();
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
            this.lastCode2 = "";
            this.ScanNote = "";
            this.flpScanNote.Controls.Clear();
            txtCode1.Clear();
            txtCode2.Clear();
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

                this.sendSuccess = true;

                if (BaseUI.UI_IsBind)
                {
                    this.GlassDetector = new AutoSerialPort();
                    // 绑定串口数据接收事件处理程序
                    this.GlassDetector.DataReceived += new Utils.DataReceivedEventHandler(GlassDetector_DataReceived);

                    // 打开玻璃检测传感器串口
                    OpenDeviceCom();

                    // 初始化2个扫码枪
                    OpenReader(2);

                    this.tmrAutoReconnect = new System.Timers.Timer(1000);
                    this.tmrAutoReconnect.AutoReset = false;
                    this.tmrAutoReconnect.Elapsed += new System.Timers.ElapsedEventHandler(tmrAutoReconnect_Elapsed);
                }
                else
                {
                    // 初始化1个扫码枪
                    OpenReader(1);
                    // 开启扫码枪
                    ReaderTriggerOn(0, true);
                    this.isTriggerOn = true;
                }


                // 连接数据接收服务器
                if (string.IsNullOrEmpty(txtIPAddress.Text))
                {
                    ShowResult(NoteState.Error, DateTime.Now, "", "服务器IP地址不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(txtPort.Text))
                {
                    ShowResult(NoteState.Error, DateTime.Now, "", "连接端口不能为空！");
                    return;
                }
                if (client == null)
                {
                    client = new Client(ClientPrint, this.ip, this.port);
                }
                if (!client.connected)
                {
                    client.ipString = this.ip;
                    client.port = YJ.Common.Utils.StrToInt(this.port, 0);
                    client.start();
                }
                if (client != null)
                {
                    ShowResult(NoteState.Success, DateTime.Now, this.ip + "：" + this.port, "客户端 " + client.localIpPort);
                    if (!client.connected)
                    {
                        this.tmrAutoReconnect.Start();
                    }
                }

                this.txtStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, txtIPAddress.Text + "：" + txtPort.Text, "客户端启动监听失败！" + ex.Message.ToString());
                // 自动重连服务器
                if (this.tmrAutoReconnect != null && this.tmrAutoReconnect.Enabled == false)
                {
                    this.tmrAutoReconnect.Start();
                }
            }
            this.btnReset.Enabled = true;
        }
        /// <summary>
        /// 断开服务器
        /// </summary>
        private void ClientStop()
        {
            try
            {
                this.btnReset.Enabled = false;

                if (BaseUI.UI_IsBind)
                {
                    // 关闭玻璃检测传感器串口
                    CloseDeviceCom();
                    this.GlassDetector.Dispose();
                    this.GlassDetector = null;

                    // 关闭扫码枪
                    ReaderTriggerOn(2, false);
                    if (Code1_Reader != null)
                    {
                        ShowResult(NoteState.Success, DateTime.Now, Code1_Reader.IpAddress, this.code1Type + "扫码枪已停止扫描.");
                        this.Code1_Reader.Disconnect();
                        PublicSub.Delay(100);
                    }
                    if (Code2_Reader != null)
                    {
                        ShowResult(NoteState.Success, DateTime.Now, Code2_Reader.IpAddress, this.code2Type + "扫码枪已停止扫描.");
                        this.Code2_Reader.Disconnect();
                        PublicSub.Delay(100);
                    }
                }
                else
                {
                    // 关闭扫码枪
                    ReaderTriggerOn(0, false);
                    if (Code1_Reader != null)
                    {
                        ShowResult(NoteState.Success, DateTime.Now, Code1_Reader.IpAddress, this.code1Type + "扫码枪已停止扫描.");
                        this.Code1_Reader.Disconnect();
                    }
                    
                    PublicSub.Delay(100);
                }

                // 断开服务器连接
                if (client != null && client.connected)
                {
                    client.stop();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", "断开服务器失败." + ex.Message.ToString());
            }
            this.btnReset.Enabled = true;
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
                            ShowResult(NoteState.Error, DateTime.Now, "", info);
                        }
                        else if (info == "31313131")
                        {
                            ShowResult(NoteState.Success, DateTime.Now, txtIPAddress.Text + "：" + txtPort.Text, "长时间无操作，与服务器连接将自动断开");
                        }
                        else
                        {
                            ShowResult(NoteState.Success, DateTime.Now, "", info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", "客户端输出信息发生异常." + ex.Message.ToString());
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

                if (this.isRepeat)
                {
                    message = "返检：" + message;
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
                        PlaySuccess("1");
                        // 弹框提示
                        new FailMessage(message, 1).ShowDialog(this);
                        break;
                    case NoteState.Fail:
                        PlaySuccess("1");
                        // 弹框提示
                        new FailMessage(message, 1).ShowDialog(this);
                        break;
                    case NoteState.Error:
                        PlaySuccess("1");
                        // 弹框提示
                        new FailMessage(message, 5).ShowDialog(this);
                        //txtCode1.Clear();
                        //txtCode2.Clear();
                        //txtGlassCode.Clear();
                        // 清除串口缓冲区
                        if (this.GlassDetector != null)
                        {
                            this.GlassDetector.DiscardBuffer();
                        }
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
        /// 打开两个扫码枪
        /// </summary>
        /// <param name="ReaderNumber">扫码枪数量</param>
        private void OpenReader(int ReaderNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode1ReaderIP.Text))
                {
                    ShowResult(NoteState.Error, DateTime.Now, "", this.code1Type + "扫码枪IP地址不能为空！");
                    return;
                }
                this.Code1_Reader = new ReaderAccessor(txtCode1ReaderIP.Text);
                this.Code1_Reader.DataArrived += new EventHandler<TCPEventArgs>(Code1_Reader_DataArrived);
                this.Code1_Reader.ReaderConnected += new EventHandler<TCPEventArgs>(Code1_Reader_ReaderConnected);
                this.Code1_Reader.ReaderClosed += new EventHandler<TCPEventArgs>(Code1_Reader_ReaderClosed);
                if (!this.Code1_Reader.Connect())
                {
                    ShowResult(NoteState.Fail, DateTime.Now, txtCode1ReaderIP.Text, this.code1Type + "扫码枪连接失败！");
                }
                else
                {
                    //扫描枪已连接，开启心跳定时器
                    this.Reader1Heatbeat = 0;
                    this.tmrRead1Heatbeat.Start();
                }


                PublicSub.Delay(100);

                if (ReaderNumber > 1)
                {
                    if (string.IsNullOrEmpty(txtCode2ReaderIP.Text))
                    {
                        ShowResult(NoteState.Error, DateTime.Now, "", this.code2Type + "扫码枪IP地址不能为空！");
                        return;
                    }
                    this.Code2_Reader = new ReaderAccessor(txtCode2ReaderIP.Text);
                    this.Code2_Reader.DataArrived += new EventHandler<TCPEventArgs>(Code2_Reader_DataArrived);
                    this.Code2_Reader.ReaderConnected += new EventHandler<TCPEventArgs>(Code2_Reader_ReaderConnected);
                    this.Code2_Reader.ReaderClosed += new EventHandler<TCPEventArgs>(Code2_Reader_ReaderClosed);
                    if (!this.Code2_Reader.Connect())
                    {
                        ShowResult(NoteState.Fail, DateTime.Now, txtCode2ReaderIP.Text, this.code2Type + "扫码枪连接失败！");
                    }
                    else
                    {
                        //扫描枪已连接，开启心跳定时器
                        this.Reader2Heatbeat = 0;
                        this.tmrRead2Heatbeat.Start();
                    }
                    PublicSub.Delay(100);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", ex.Message);
            }
        }

        /// <summary>
        /// #1扫描枪已连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Code1_Reader_ReaderConnected(object sender, TCPEventArgs e)
        {
            ShowResult(NoteState.Success, DateTime.Now, txtCode1ReaderIP.Text, this.code1Type + "扫码枪已连接【" + txtCode1ReaderIP.Text + "】");
        }
        /// <summary>
        /// #1扫描枪已连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Code2_Reader_ReaderConnected(object sender, TCPEventArgs e)
        {
            ShowResult(NoteState.Success, DateTime.Now, txtCode2ReaderIP.Text, this.code2Type + "扫码枪已连接【" + txtCode2ReaderIP.Text + "】");
        }
        /// <summary>
        /// #1扫描枪已断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Code1_Reader_ReaderClosed(object sender, TCPEventArgs e)
        {
            ShowResult(NoteState.Fail, DateTime.Now, txtCode1ReaderIP.Text, this.code1Type + "扫码枪连接断开");
        }
        /// <summary>
        /// #2扫描枪已断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Code2_Reader_ReaderClosed(object sender, TCPEventArgs e)
        {
            ShowResult(NoteState.Fail, DateTime.Now, txtCode2ReaderIP.Text, this.code2Type + "扫码枪连接断开");
        }

        /// <summary>
        /// 触发两个扫码枪
        /// </summary>
        /// <param name="readerNum"></param>
        /// <param name="isOn"></param>
        private void ReaderTriggerOn(int readerNum, bool isOn)
        {
            try
            {
                if (isOn)
                {
                    //ExecCommand("command")is for sending a command and getting a command response.
                    if (readerNum == 0)
                    {
                        this.Code1_Reader.ExecCommand("LON");
                    }
                    else if (readerNum == 1)
                    {
                        this.Code2_Reader.ExecCommand("LON");
                    }
                    else
                    {
                        this.Code1_Reader.ExecCommand("LON");
                        this.Code2_Reader.ExecCommand("LON");
                    }
                }
                else
                {
                    //ExecCommand("command")is for sending a command and getting a command response.
                    if (readerNum == 0)
                    {
                        this.Code1_Reader.ExecCommand("LOFF");
                    }
                    else if (readerNum == 1)
                    {
                        this.Code2_Reader.ExecCommand("LOFF");
                    }
                    else
                    {
                        this.Code1_Reader.ExecCommand("LOFF");
                        this.Code2_Reader.ExecCommand("LOFF");
                    }
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
                Debug.WriteLine("收到" + this.code1Type + "扫描枪数据：" + DataString);

                //收到数据，心跳计数清零
                this.Reader1Heatbeat = 0;

                if (DataString.Contains("%KEYENCE"))
                {
                    ShowResult(NoteState.Success, DateTime.Now, DataString, "收到" + this.code1Type + "扫描枪数据：" + DataString);
                    return;
                }

                // 分析处理数据
                if (!string.IsNullOrEmpty(DataString))
                {
                    ScanDataHandler(DataString, txtCode1);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Code2扫码枪接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Code2_Reader_DataArrived(object sender, TCPEventArgs e)
        {
            try
            {
                byte[] data = e.DataBytes;
                string DataString = (new UTF8Encoding().GetString(data)).Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");
                Debug.WriteLine("收到" + this.code2Type + "扫描枪数据：" + DataString);

                //收到数据，心跳计数清零
                this.Reader2Heatbeat = 0;

                if (DataString.Contains("%KEYENCE"))
                {
                    ShowResult(NoteState.Success, DateTime.Now, DataString, "收到" + this.code2Type + "扫描枪数据：" + DataString);
                    return;
                }


                // 分析处理数据
                if (!string.IsNullOrEmpty(DataString) )
                {
                    ScanDataHandler(DataString, txtCode2);
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
                    NoteResult rst = new NoteResult(NoteState.Success);

                    // 解析扫描数据
                    #region 解析扫描数据
                    if (DataString.Contains("OK:") || DataString.Contains("ERROR:"))
                    {
                        rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, "编码不符合规则，请确认扫描枪是否打开了测试模式");
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

                            //// 小写字母检查
                            //if (!appConfig.GetConfigBool("LowerEnabled") && BaseUI.LowerCheck(DataString))
                            //{
                            //    rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, "条码中禁止包含小写字母");
                            //}
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

                    // 过滤无效数据
                    if (!this.isTriggerOn || DataString == "ERROR" || (DataString.Length >= 6 && DataString.Substring(1, 5) == "ERROR")) return;

                    #endregion 解析扫描数据

                    #region 分别处理Code1和Code2数据
                    if (rst.State == NoteState.Success)
                    {
                        // 扫描编码
                        if (textbox == txtCode1)
                        {
                            // 编码已填充到文本框，丢弃
                            if (DataString == textbox.Text) return;

                            // 判断是否重复扫描
                            if (DataString == this.lastCode1)//(this.lastCide1 != "" && DataString.Contains(this.lastCide1))
                            {
                                // 过站扫描点只扫一个编码，重复扫描数据直接丢弃
                                if (!BaseUI.UI_IsBind)
                                {
                                    return;
                                }
                                else
                                {
                                    // 关键工序，提示重复扫描
                                    rst = new NoteResult(NoteState.Fail, DateTime.Now, DataString, this.code1Type + "编码重复扫描！");
                                }//if (!BaseUI.UI_IsBind)
                            }
                            else
                            {
                                this.ScanCode1 = DataString;
                                // 检查在制工单是否切换
                                rst = ProductionRouteCheck(DataString);
                                if (rst.State == NoteState.Success)
                                {
                                    #region Code1编码规则检查
                                    //if (txtRule1.Text == "")
                                    //{
                                    //    IsScan = false;
                                    //    return new NoteResult(NoteState.Fail, "", "未设置" + this.scanType1 + "编码规则");
                                    //}
                                    //if (txtDigit1.Text == "")
                                    //{
                                    //    IsScan = false;
                                    //    return new NoteResult(NoteState.Fail, "", "未设置" + this.scanType1 + "编码长度");
                                    //}

                                    //判断编码长度是否符合规则
                                    if (txtDigit1.Text == "0" || DataString.Length == YJ.Common.Utils.StrToInt(txtDigit1.Text, 0))
                                    {
                                        // 判断是否符合code1编码规则
                                        if (txtRule1.Text != "" && DataString.Substring(0, DataString.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : DataString.Length) != txtRule1.Text)
                                        {
                                            rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, this.code1Type + "编码格式不符合规则！");
                                        }
                                        else
                                        {
                                            rst = new NoteResult(NoteState.Success, DateTime.Now, DataString, this.code1Type + "编码：" + DataString);
                                        }
                                    }
                                    else
                                    {
                                        rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, this.code1Type + "编码长度" + DataString.Length + "不符合规则！");
                                    }
                                    #endregion Code1编码规则检查

                                }//if (rst.State == NoteState.Success)
                            }//if (DataString == this.lastCide1)
                        }//if (textbox == txtCode1)
                        // Code2编码
                        else if (textbox == txtCode2)
                        {
                            if (DataString == textbox.Text) return;
                            // 判断是否重复扫描
                            if (DataString == this.lastCode2)
                            {
                                rst = new NoteResult(NoteState.Fail, DateTime.Now, DataString, this.code2Type + "编码重复扫描！");
                            }
                            //// 判断是否符合Code2编码规则   
                            //else if (txtRule2.Text != "" && DataString.Substring(0, DataString.Length >= txtRule2.Text.Length ? txtRule2.Text.Length : DataString.Length) != txtRule2.Text)
                            //{
                            //    rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, this.code2Type + "编码不符合规则！");
                            //}
                            //else if (txtDigit2.Text != "0" && DataString.Length != YJ.Common.Utils.StrToInt(txtDigit2.Text, 0))
                            //{
                            //    rst = new NoteResult(NoteState.NG, DateTime.Now, DataString, this.code2Type + "编码长度" + DataString.Length + "不符合规则！");
                            //}
                            else
                            {
                                this.ScanCode2 = DataString;
                                rst = new NoteResult(NoteState.Success, DateTime.Now, DataString, this.code2Type + "编码：" + DataString);
                            }
                        }//else if (textbox == txtCode2)
                    }//if (rst.State == NoteState.Success)

                    #endregion 分别处理Code1和Code2数据

                    // 添加Note
                    ShowResult(rst);

                    textbox.Text = DataString;

                    // 编码检测通过
                    if (rst.State == NoteState.Success)
                    {
                        if (!BaseUI.UI_IsBind)
                        {
                            this.LCD_Count += 1;
                            this.txtLCDCount.Text = this.LCD_Count.ToString();
                            // 校验数据并发送到服务器
                            CheckAndSendCode();
                        }//if (!BaseUI.UI_IsBind)
                        else
                        {
                            if (txtCode1.Text != "" && txtCode2.Text != "")
                            {
                                if (this.tmrTimeOut != null)
                                {
                                    this.tmrTimeOut.Enabled = false;
                                    this.tmrTimeOut.Dispose();
                                }
                                // 校验数据并发送到服务器
                                CheckAndSendCode();
                            }
                            else
                            {
                                this.tmrTimeOut = new System.Windows.Forms.Timer();
                                this.tmrTimeOut.Interval = this.tBindingTimeout;
                                this.tmrTimeOut.Tick += new EventHandler(tmrTimeOut_Tick);
                                this.tmrTimeOut.Start();
                            }
                        }//if (BaseUI.UI_IsBind)
                    }
                    // 编码不符合规则，计入混料
                    else if (rst.State == NoteState.NG)
                    {
                        if (!BaseUI.UI_IsBind)
                        {
                            string code1 = txtCode1.Text;
                            ShowResult(NoteState.NG, DateTime.Now, code1, this.code1Type + "编码绑定失败！ ");
                            this.lastCode1 = DataString;
                        }
                        else
                        {
                            // 停止扫码枪
                            ReaderTriggerOn(2, false);
                            if (this.tmrTimeOut != null)
                            {
                                this.tmrTimeOut.Enabled = false;
                                this.tmrTimeOut.Dispose();
                            }
                            string code1 = txtCode1.Text == "" ? "【" + this.code1Type + "漏扫】" : txtCode1.Text;
                            string code2 = txtCode2.Text == "" ? "【" + this.code2Type + "漏扫】" : txtCode2.Text;
                            ShowResult(NoteState.NG, DateTime.Now, code1 + " | " + code2, this.code1Type + "-" + this.code2Type + "编码绑定失败！ ");

                            this.isTriggerOn = false;
                        }
                        this.sendSuccess = true;
                        txtCode1.Text = "";
                        txtCode2.Text = "";
                        this.RuleError_Count += 1;
                        this.txtRuleCount.Text = this.RuleError_Count.ToString();
                        AddScanNote("2");
                        return;
                    }
                    // 编码重复扫描，不计数
                    else
                    {
                        if (!BaseUI.UI_IsBind)
                        {
                            //string code1 = txtCode1.Text;
                            //ShowResult(NoteState.NG, DateTime.Now, code1, this.code1Type + "编码绑定失败！ ");
                            this.lastCode1 = DataString;
                        }
                        else
                        {
                            // 停止扫码枪
                            ReaderTriggerOn(2, false);
                            if (this.tmrTimeOut != null)
                            {
                                this.tmrTimeOut.Enabled = false;
                                this.tmrTimeOut.Dispose();
                            }
                            string code1 = txtCode1.Text == "" ? "【" + this.code1Type + "漏扫】" : txtCode1.Text;
                            string tp = txtCode2.Text == "" ? "【" + this.code2Type + "漏扫】" : txtCode2.Text;
                            ShowResult(NoteState.NG, DateTime.Now, code1 + " | " + tp, "玻璃检测数据异常，已忽略！ ");

                            this.isTriggerOn = false;
                            this.LCD_Count -= 1;
                            this.txtLCDCount.Text = this.LCD_Count.ToString();
                        }
                        this.sendSuccess = true;
                        txtCode1.Text = "";
                        txtCode2.Text = "";
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

            //#region 局部变量
            //string productionOrder = "";//生产订单
            //string finishesModel = "";//成品规格型号
            //#endregion 局部变量

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

            //#region 混线生产检测
            //if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionLineCode) && this.glassInfo.ProductInfo.ProductionLineCode != BaseUI.UI_SPLJOBCODE)
            //{
            //    DataTable dtPro = BaseUI.GetOrderInfo(this.glassInfo.ProductInfo.ProductionOrder);
            //    string mixLine = "0";
            //    string lineName = "";
            //    if (dtPro != null && dtPro.Rows.Count > 0)
            //    {
            //        mixLine = (dtPro.Rows[0]["SPOM_MixedLine"] ?? "0").ToString();
            //        lineName = (dtPro.Rows[0]["SPL_Name"] ?? "").ToString();
            //    }

            //    // 工单不允许混线
            //    if (mixLine == "0")
            //    {
            //        return new NoteResult(NoteState.Fail, DataString, "该玻璃生产产线为" + lineName + ",与当前产线不一致。");
            //    }
            //    // 不同型号禁止混线
            //    else if (this.glassInfo.ProductInfo.FinishesModel != BaseUI.UI_SMSPEC)
            //    {
            //        return new NoteResult(NoteState.Fail, DataString, "该玻璃型号为" + this.glassInfo.ProductInfo.FinishesModel + ",与当前产线不一致。");
            //    }
            //    else
            //    {
            //        List<string> lstMixLine = BaseUI.GetMixLine(this.glassInfo.ProductInfo.ProductionOrder);
            //        if (lstMixLine.Count == 0)
            //        {
            //            return new NoteResult(NoteState.Fail, DataString, "该玻璃工单未配置可混产线。");
            //        }
            //        else if (!lstMixLine.Contains(BaseUI.UI_SPLJOBCODE))
            //        {
            //            return new NoteResult(NoteState.Fail, DataString, "该玻璃生产产线为" + lineName + ",禁止在当前产线生产。");
            //        }
            //    }
            //    //if (!this.isMixedLine)
            //    //{
            //    //    string LineName = BaseUI.GetLineName(this.glassInfo.ProductInfo.ProductionLineCode);
            //    //    return new NoteResult(NoteState.Fail, DataString, "该玻璃生产产线为" + LineName + ",与当前产线不一致。");
            //    //}
            //    //else if (this.glassInfo.ProductInfo.FinishesModel != BaseUI.UI_SMSPEC)
            //    //{
            //    //    return new NoteResult(NoteState.Fail, DataString, "该玻璃型号为" + this.glassInfo.ProductInfo.FinishesModel + ",与当前产线不一致。");
            //    //}
            //}
            //#endregion 混线生产检测

            #region 混线生产检测
            // 与当前产线不一致，判断是否允许混线
            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionLineCode) && this.glassInfo.ProductInfo.ProductionLineCode != BaseUI.UI_SPLJOBCODE)
            {
                List<string> lstMixLine = BaseUI.GetMixLine(this.glassInfo.ProductInfo.FinishesCode);
                if (lstMixLine.Count == 0)
                {
                    return new NoteResult(NoteState.Fail, DataString, "该玻璃产线为" + this.glassInfo.ProductInfo.ProductionLineCode + ",禁止混线。");
                }
                else if (!lstMixLine.Contains(BaseUI.UI_SPLJOBCODE))
                {
                    return new NoteResult(NoteState.Fail, DataString, "该玻璃产线为" + this.glassInfo.ProductInfo.ProductionLineCode + ",禁止在当前产线生产。");
                }
            }
            #endregion 混线生产检测

            #region 型号切换检测
            // 不同型号时检测是否允许切换型号
            if (this.glassInfo.ProductInfo != null && this.glassInfo.ProductInfo.FinishesModel != txtModel.Text)
            {
                if (!string.IsNullOrWhiteSpace(this.warnType) && this.warnType.Substring(3, 1) == "0")
                {
                    return new NoteResult(NoteState.Fail, DataString, "该玻璃型号为" + this.glassInfo.ProductInfo.FinishesModel + ",与当前产线不一致。");
                }
            }
            #endregion 型号切换检测

            #region 判断玻璃是否已包装
            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode) && BaseUI.isInPackage(this.glassInfo.ProductInfo.LCDCode))
            {
                return new NoteResult(NoteState.Fail, DataString, "该玻璃已包装，无法过站！");
            }
            #endregion 判断玻璃是否已包装

            #region 切换工单，重新初始化页面。LCD清洗投入不切换工单。
            if (BaseUI.UI_CurrentProcedureNo != "1" && this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionOrder) && this.glassInfo.ProductInfo.ProductionOrder != txtOrder.Text)
            {
                txtOrder.Text = this.glassInfo.ProductInfo.ProductionOrder;
                txtModel.Text = this.glassInfo.ProductInfo.FinishesModel;
                // 初始化工序
                if (BindProcess(this.glassInfo.ProductInfo.ProductionOrder, BaseUI.UI_SPLJOBCODE).State != NoteState.Success)
                {
                    return new NoteResult(rst.State, "", rst.Message);
                }
            }
            #endregion 切换工单，重新初始化页面

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
            //获取工序链列表
            this.dicProcessRoute = dtProcessTable.Rows.Cast<DataRow>().ToDictionary(x => x["SGX_JobCode"].ToString(), x => x["SGX_Name"].ToString());
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

                // 是否允许返检
                switch (dvProcess[0]["SGX_Rework"].ToString())
                {
                    case "02":
                        this.processRepeatEnabled = true;
                        break;
                    default:
                        this.processRepeatEnabled = false;
                        break;
                }

                ////如果是背光贴合，则获取成品码绑定的工控机IP地址
                //if (BaseUI.UI_CurrentProcedureCode == "024")
                //{
                //    // 筛选出成品码生成绑定工序032对应的设备IP地址
                //    DataView dvQr = new DataView(dtProcessTable);
                //    dvQr.RowFilter = "SGX_JobCode='032'";
                //    // 背光贴合的下一道工序是032成品码生成(喷码)
                //    if (dvQr.Count > 0 && YJ.Common.Utils.StrToInt(dvQr[0]["PR_NoSeq"], 0) == YJ.Common.Utils.StrToInt(BaseUI.UI_CurrentProcedureNo, 0) + 1)
                //    {
                //        this.ipQrBinding = dvQr[0]["STW_CardIP"].ToString();
                //    }
                //    else
                //    {
                //        this.ipQrBinding = "";
                //    }
                //}

                // 机台IP设置的弹框提醒类型
                this.warnType = BaseUI.GetDeviceWarnType(BaseUI.UI_EquCardIP);

                // 相邻工序过站间隔时间
                this.processInterval = BaseUI.GetProcessInterval(BaseUI.UI_EquCardIP);

                // 当前已过工序的列表
                this.dicProcess = BaseUI.GetProcedure(dtProcessTable);

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

                //txtCode1.Clear();
                //txtCode2.Clear();
                txtRule1.Clear();
                txtRule2.Clear();
                txtCode1ReaderIP.Clear();
                txtCode2ReaderIP.Clear();

                this.code1Type = scanType[0];
                if (scanType.Length > 1)
                {
                    this.code2Type = scanType[1];
                }
                if (BaseUI.UI_IsBind)
                {
                    lblCode2.Visible = true;
                    txtCode2.Visible = true;
                    txtRule2.Visible = true;
                    lblRule2.Visible = true;
                    txtDigit2.Visible = true;
                    lblCode1.Text = this.code1Type + "编码:";
                    txtRule1.Text = dvProcess[0]["Rule_" + this.code1Type].ToString();
                    txtDigit1.Text = dvProcess[0]["Len_" + this.code1Type].ToString();
                    lblCode2.Text = this.code2Type + "编码:";
                    txtRule2.Text = dvProcess[0]["Rule_" + this.code2Type].ToString();
                    txtDigit2.Text = dvProcess[0]["Len_" + this.code2Type].ToString();
                    // 1#扫描枪IP
                    lblCode1ReaderIP.Text = this.code1Type + "扫描枪:";
                    txtCode1ReaderIP.Text = ReaderIP[0];
                    // 2#扫描枪IP
                    lblCode2ReaderIP.Visible = true;
                    txtCode2ReaderIP.Visible = true;
                    lblCode2ReaderIP.Text = this.code2Type + "扫描枪:";
                    txtCode2ReaderIP.Text = ReaderIP[1];
                }
                else
                {
                    lblCode2.Visible = false;
                    txtCode2.Visible = false;
                    txtRule2.Visible = false;
                    lblRule2.Visible = false;
                    txtDigit2.Visible = false;
                    lblCode1.Text = this.code1Type + "编码:";
                    txtRule1.Text = dvProcess[0]["Rule_" + this.code1Type].ToString();
                    txtDigit1.Text = dvProcess[0]["Len_" + this.code1Type].ToString();
                    // 1#扫描枪IP
                    lblCode1ReaderIP.Text = this.code1Type + "扫描枪:";
                    txtCode1ReaderIP.Text = ReaderIP[0];
                    // 2#扫描枪IP
                    lblCode2ReaderIP.Visible = false;
                    txtCode2ReaderIP.Visible = false;

                }
                return new NoteResult(NoteState.Success);
            }
            else
            {
                return new NoteResult(NoteState.NG, DateTime.Now, ProductOrder, "当前工单不需要过此工序");
            }
        }

        /// <summary>
        /// 编码绑定超时定时器程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrTimeOut_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer tmr = sender as System.Windows.Forms.Timer;
            tmr.Enabled = false;
            // 停止扫码枪
            ReaderTriggerOn(2, false);

            if (txtCode1.Text == "")
            {
                ShowResult(NoteState.Fail, DateTime.Now, "", this.code1Type + "码漏扫！");
            }
            if (txtCode2.Text == "")
            {
                ShowResult(NoteState.Fail, DateTime.Now, "", this.code2Type + "码漏扫！");
            }
            string code1 = txtCode1.Text == "" ? "【" + this.code1Type + "漏扫】" : txtCode1.Text;
            string code2 = txtCode2.Text == "" ? "【" + this.code2Type + "漏扫】" : txtCode2.Text;
            ShowResult(NoteState.NG, DateTime.Now, code1 + " | " + code2, this.code1Type + "-" + this.code2Type + "编码绑定失败！ ");

            this.isTriggerOn = false;
            this.sendSuccess = true;
            txtCode1.Text = "";
            txtCode2.Text = "";
            this.NG_Count += 1;
            this.txtNGCount.Text = this.NG_Count.ToString();
            AddScanNote("1");
        }

        /// <summary>
        /// 打开玻璃检测传感器
        /// </summary>
        private void OpenDeviceCom()
        {
            try
            {
                //打开玻璃检测传感器
                this.GlassDetector.OpenPort();

//#if !DEBUG
                if (isGlassDetectorConnected())
                {
                    ShowResult(NoteState.Success, DateTime.Now, this.GlassDetector.PortName, "玻璃检测传感器(" + this.GlassDetector.PortName + ")已连接.");
                }
                else
                {
                    ShowResult(NoteState.Error, DateTime.Now, "", "玻璃检测传感器连接异常!");
                }
//#endif
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, "", "玻璃检测传感器连接异常!" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 关闭玻璃检测传感器串口
        /// </summary>
        private void CloseDeviceCom()
        {
            try
            {
                this.GlassDetector.DiscardBuffer();
                this.GlassDetector.ClosePort();
                ShowResult(NoteState.Success, DateTime.Now, this.GlassDetector.PortName, "玻璃检测传感器(" + this.GlassDetector.PortName + ")已关闭.");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, DateTime.Now, this.GlassDetector.PortName, "关闭玻璃检测传感器失败!" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 玻璃检测传感器连接测试
        /// </summary>
        /// <returns></returns>
        private bool isGlassDetectorConnected()
        {
            try
            {
                //薛工信息采集器心跳协议
                byte[] cmd = SerialPortUtil.HexToByte("57AB0101787C");
                byte[] rev = new byte[5];
                this.GlassDetector.SendCommand(cmd, ref rev, 500);
                if (byteToHexStr(rev) == "57AB810187")
                {
                    return true;
                }
                else
                {
                    //新版本信息采集器心跳协议
                    cmd = SerialPortUtil.HexToByte("AA5A00FC00FF");
                    rev = new byte[6];
                    this.GlassDetector.SendCommand(cmd, ref rev, 500);
                    if (byteToHexStr(rev) == "AA5A00FC0FFF")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// 玻璃检测传感器串口数据接收事件
        /// </summary>
        /// <param name="e"></param>
        private void GlassDetector_DataReceived(Utils.DataReceivedEventArgs e)
        {
            try
            {
                // 打印调试数据
                System.Diagnostics.Debug.Print(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  |   检测到玻璃：" + e.HexStringReceived + "\r\n");

                byte[] ReDatas = e.BytesReceived;

                // 分析处理数据
                if (ReDatas != null && ReDatas.Length > 0)
                {
                    NewGlassHandler(ReDatas);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Fail, DateTime.Now, this.GlassDetector.PortName, "玻璃检测传感器接收数据失败." + ex.Message.ToString());
            }
        }

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
                    if (Data.Length < 4)
                    {
                        ShowResult(NoteState.Fail, time, hexString, "玻璃检测数据异常.");
                        return;
                    }
                    else if(hexString.Contains("AA5A00FA0FFF") && !hexString.Contains("AA5A00FA8FFF"))
                    {
                        //忽略玻璃离开信号
                        return;
                    }
                    else if ((Data[0] != 53 && !hexString.Contains("AA5A00FA8FFF")))
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

                    // 若上一片玻璃绑定失败
                    if (!this.sendSuccess)
                    {
                        // 停止扫码枪
                        ReaderTriggerOn(2, false);

                        string code1 = txtCode1.Text == "" ? "【" + this.code1Type + "漏扫】" : txtCode1.Text;
                        string tp = txtCode2.Text == "" ? "【" + this.code2Type + "漏扫】" : txtCode2.Text;
                        ShowResult(NoteState.NG, time, code1 + " | " + tp, this.code1Type + "-" + this.code2Type + "编码绑定失败！ ");

                        this.isTriggerOn = false;
                        txtCode1.Text = "";
                        txtCode2.Text = "";
                        this.NG_Count += 1;
                        this.txtNGCount.Text = this.NG_Count.ToString();
                        AddScanNote("1");
                        //Utils.PublicSub.Delay(50);
                    }


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

                    //// 传感器检测玻璃序号
                    //int index = Data[1];

                    // 处理新玻璃数据

                    this.LCD_Count += 1;
                    this.txtLCDCount.Text = this.LCD_Count.ToString();

                    // 判断是否符合检测到玻璃的数据格式
                    if (Data.Length == 4 && Data[0] == 53 || hexString == "AA5A00FA8FFF")
                    {
                        //// 传感器检测玻璃序号
                        //int index = Data[1];
                        ShowResult(NoteState.Success, time, hexString, "检测到新玻璃");
                    }
                    else
                    {
                        ShowResult(NoteState.Fail, time, hexString, "检测到新玻璃,但数据异常！");
                    }

                    if (this.tmrTimeOut != null)
                    {
                        this.tmrTimeOut.Enabled = false;
                        this.tmrTimeOut.Dispose();
                    }
                    this.LastLCDTime = tick;
                    this.sendSuccess = false;
                    txtCode1.Text = "";
                    txtCode2.Text = "";
                    // 刷新显示
                    this.Refresh();
                    this.isTriggerOn = true;
                    // 开启扫码枪
                    ReaderTriggerOn(2, true);
                }
            }
            catch (Exception ex)
            {
                // 停止扫码枪
                ReaderTriggerOn(2, false);
                this.isTriggerOn = false;
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Fail, DateTime.Now, SerialPortUtil.ByteToHex(Data), "数据解析失败." + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        private void CheckAndSendCode()
        {
            NoteResult rst;

            // 已过工序和编码合法性检测
            rst = ProcessCheck();

            if (rst.State == NoteState.Success)
            {
                // 生成发送字符串，并发送至服务器
                rst = BuildAndSend();
            }

            // 判断编码检测和发送结果
            switch (rst.State)
            {
                case NoteState.OK:  // 发送成功
                    // 输出提示信息
                    ShowResult(rst);

                    this.OK_Count += 1;
                    this.txtOKCount.Text = this.OK_Count.ToString();
                    AddScanNote("0");

                    // 024背光贴合，若需生成并绑定成品码
                    if (BaseUI.UI_CurrentProcedureCode == "024" && !string.IsNullOrEmpty(this.ipQrBinding))
                    {
                        // 背光编码
                        string blcode = txtCode2.Text;
                        // 委托异步处理成品码绑定
                        QRCodeBinding(blcode);
                    }
                    break;

                case NoteState.NG:  // 编码不合法
                    // 输出提示信息
                    ShowResult(rst);

                    this.NG_Count += 1;
                    this.txtNGCount.Text = this.NG_Count.ToString();
                    AddScanNote("1");
                    break;

                case NoteState.Fail:    // 混料
                    // 输出提示信息
                    ShowResult(NoteState.NG, DateTime.Now, rst.Code, rst.Message);

                    this.RuleError_Count += 1;
                    this.txtRuleCount.Text = this.RuleError_Count.ToString();
                    AddScanNote("2");
                    break;

                case NoteState.Error:   // 服务器异常
                    // 输出提示信息
                    ShowResult(NoteState.NG, DateTime.Now, rst.Code, rst.Message);

                    //this.NG_Count += 1;
                    //this.txtNGCount.Text = this.NG_Count.ToString();
                    //AddScanNote("1");
                    this.RuleError_Count += 1;
                    this.txtRuleCount.Text = this.RuleError_Count.ToString();
                    AddScanNote("2");
                    // 自动重连服务器
                    if (this.tmrAutoReconnect != null && this.tmrAutoReconnect.Enabled == false)
                    {
                        this.tmrAutoReconnect.Start();
                    }
                    break;
            }

            this.lastCode1 = txtCode1.Text;
            txtCode1.Text = "";

            if (BaseUI.UI_IsBind)
            {
                this.lastCode2 = txtCode2.Text;
                txtCode2.Text = "";
                this.sendSuccess = true;

                ReaderTriggerOn(2, false);
                this.isTriggerOn = false;
            }
        }

        /// <summary>
        /// 自动重连服务器定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrAutoReconnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (client == null)
                {
                    client = new Client(ClientPrint, this.ip, this.port);
                }
                if (!client.connected)
                {
                    client.ipString = this.ip;
                    client.port = YJ.Common.Utils.StrToInt(this.port, 0);
                    client.start();
                }
                if (client != null)
                {
                    ShowResult(NoteState.Success, DateTime.Now, this.ip + "：" + this.port, "客户端 " + client.localIpPort);
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
            catch (Exception ex)
            {
                LogHelper.Error("自动重连接收服务器失败", ex);
                this.tmrAutoReconnect.Start();
            }
        }

        /// <summary>
        /// 漏工序和编码合法性检测
        /// </summary>
        /// <returns></returns>
        private NoteResult ProcessCheck()
        {
            #region 局部变量
            string code1 = "";//扫描码
            string code2 = "";//对应码
            string code = "";//绑定码
            #endregion 局部变量

            txtGlassCode.Clear();

            #region 从HBase数据库查询过站信息
            code1 = txtCode1.Text;
            code = code1;
            if (BaseUI.UI_IsBind)
            {
                code2 = txtCode2.Text;
                code = code1 + "|" + code2;
            }
            //code1Code查询结果
            if (this.glassInfo == null)
            {
                return new NoteResult(NoteState.NG, code, "HBase数据库连接异常.");
            }

            #endregion 从HBase数据库查询过站信息

            #region 编码规则检测
            // 判断是否符合code1编码规则
            if (txtRule1.Text != "" && code1.Substring(0, code1.Length >= txtRule1.Text.Length ? txtRule1.Text.Length : code1.Length) != txtRule1.Text)
            {
                return new NoteResult(NoteState.Fail, code1, this.code1Type + "编码格式不符合规则！");
            }
            else if (txtDigit1.Text != "0" && code1.Length != YJ.Common.Utils.StrToInt(txtDigit1.Text, 0))
            {
                return new NoteResult(NoteState.Fail, code1, this.code1Type + "编码长度" + code1.Length + "不符合规则！");
            }

            // 判断是否符合Code2编码规则
            if (BaseUI.UI_IsBind && txtRule2.Text != "" && code2.Substring(0, code2.Length >= txtRule2.Text.Length ? txtRule2.Text.Length : code2.Length ) != txtRule2.Text)
            {
                return new NoteResult(NoteState.Fail, code2, this.code2Type + "编码格式不符合规则！");
            }
            else if (BaseUI.UI_IsBind && txtDigit2.Text != "0" && code2.Length != YJ.Common.Utils.StrToInt(txtDigit2.Text, 0))
            {
                return new NoteResult(NoteState.Fail, code2, this.code2Type + "编码长度" + code2.Length + "不符合规则！");
            }
            #endregion 编码规则检测

            //#region 混线生产检测

            //if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionLineCode) & this.glassInfo.ProductInfo.ProductionLineCode != BaseUI.UI_SPLJOBCODE && !this.isMixedLine)
            //{
            //    string LineName = BaseUI.GetLineName(this.glassInfo.ProductInfo.ProductionLineCode);
            //    return new NoteResult(NoteState.Fail, code, "该玻璃生产产线为" + LineName + ",与当前产线不一致。");
            //}

            //#endregion 混线生产检测

            #region 混线生产检测
            // 与当前产线不一致，判断是否允许混线
            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionLineCode) && this.glassInfo.ProductInfo.ProductionLineCode != BaseUI.UI_SPLJOBCODE)
            {
                List<string> lstMixLine = BaseUI.GetMixLine(this.glassInfo.ProductInfo.FinishesCode);
                if (lstMixLine.Count == 0)
                {
                    return new NoteResult(NoteState.Fail, code, "该玻璃产线为" + this.glassInfo.ProductInfo.ProductionLineCode + ",禁止混线。");
                }
                else if (!lstMixLine.Contains(BaseUI.UI_SPLJOBCODE))
                {
                    return new NoteResult(NoteState.Fail, code, "该玻璃产线为" + this.glassInfo.ProductInfo.ProductionLineCode + ",禁止在当前产线生产。");
                }
            }
            #endregion 混线生产检测

            #region 型号切换检测
            // 不同型号时检测是否允许切换型号
            if (this.glassInfo.ProductInfo != null && this.glassInfo.ProductInfo.FinishesModel != txtModel.Text)
            {
                if (!string.IsNullOrWhiteSpace(this.warnType) && this.warnType.Substring(3, 1) == "0")
                {
                    return new NoteResult(NoteState.Fail, code, "该玻璃型号为" + this.glassInfo.ProductInfo.FinishesModel + ",与当前产线不一致。");
                }
            }
            #endregion 型号切换检测

            #region 判断玻璃是否已包装
            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode) && BaseUI.isInPackage(this.glassInfo.ProductInfo.LCDCode))
            {
                return new NoteResult(NoteState.Fail, code, "该玻璃已包装，无法过站！");
            }
            #endregion 判断玻璃是否已包装

            #region 已过工序检测
            this.isRepeat = false;
            if (this.glassInfo.GlassState != null)
            {
                bool Isrepeat = BaseUI.CheckProcedure(this.glassInfo.GlassState.LogCode);
                if (Isrepeat && !this.processRepeatEnabled)
                {
                    return new NoteResult(NoteState.NG, code, "当前工序已扫描,不能再次过站。");
                }
            }
            #endregion 已过工序检测

            #region 非法玻璃卡控
            if (this.glassInfo.ProductInfo != null)
            {
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
                        return new NoteResult(NoteState.NG, code, "此玻璃已申报不良，请提交复判！");

                    case "复判重工":
                        return new NoteResult(NoteState.NG, code, "此玻璃已复判重工，请提交重工组！");

                    case "复判报废":
                        return new NoteResult(NoteState.NG, code, "此玻璃已复判报废，不能过站！");

                    case "重工报废":
                        return new NoteResult(NoteState.NG, code, "此玻璃已重工报废，不能过站！");
                }
                #endregion 不良玻璃检测

                #region 漏工序检测
                if (this.glassInfo.GlassState != null)
                {
                    string Procedure = BaseUI.GetLeakProcedure(this.glassInfo.GlassState.LogCode, BaseUI.NowCode, dicProcess);
                    if (Procedure != "")
                    {
                        string msg = "检测到工序 " + Procedure + " 未扫描.";
                        return new NoteResult(NoteState.NG, code, msg);
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
                                return new NoteResult(NoteState.NG, code, msg);
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
                            string msg = "当前工序已过站,请勿重复扫描!";
                            return new NoteResult(NoteState.NG, code, msg);
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
                            return new NoteResult(NoteState.NG, code, msg);
                        }
                    }
                }
                #endregion 相邻工序过站间隔检测

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
                                        WarnMessage warnMsg = new WarnMessage("复 判 良 品 ！(点线类不良)", MessageBoxButtons.OK, 1, "OK");
                                        warnMsg.ShowDialog(this);
                                    }
                                    else
                                    {
                                        WarnMessage warnMsg = new WarnMessage("复 判 良 品 ！", MessageBoxButtons.OK, 1, "OK");
                                        warnMsg.ShowDialog(this);
                                    }
                                }
                            }

                            break;
                    }
                }
                #endregion 复判良品和重工良品检测

            }//if (this.glassInfo.ProductInfo != null)
            #endregion 非法玻璃卡控

            #region 编码合法性检测
            // 第一道工序，检查LCD编码是否已存在
            if (BaseUI.UI_SGXNO == "1" && this.glassInfo.ProductInfo != null && !string.IsNullOrWhiteSpace(this.glassInfo.ProductInfo.LCDCode))
            {
                return new NoteResult(NoteState.NG, code, "当前玻璃码已绑定，请勿多次绑定");
            }

            // 其他工序，检查是否获取玻璃信息
            if (BaseUI.UI_SGXNO != "1" && (this.glassInfo.ProductInfo == null || string.IsNullOrWhiteSpace(this.glassInfo.ProductInfo.LCDCode)))
            {
                return new NoteResult(NoteState.Error, code, "获取FOG绑定信息失败");
            }

            if (this.glassInfo.ProductInfo != null && !string.IsNullOrEmpty(this.glassInfo.ProductInfo.LCDCode))
            {
                txtGlassCode.Text = this.glassInfo.ProductInfo.LCDCode;
            }

            // 关键扫描点，检查对应码Code2是否已绑定
            if (BaseUI.UI_IsBind)
            {
                // 查询Code2绑定信息
                try
                {
                    this.bindInfo = null;
                    //this.processData2 = GHDC.GetProductionRouteByCode(code2);
                    CallWithTimeout(GetProcessData2, this.searchTimeout);
                    if (this.bindInfo == null)
                    {
                        return new NoteResult(NoteState.NG, code, "内部查询玻璃绑定信息失败,如多次出现此错误，请关掉程序重新打开");
                    }
                }
                catch (TimeoutException tex)
                {
                    GetHBaseDataClass.Instance.Reconnect();
                    return new NoteResult(NoteState.NG, code, "HBase数据库连接超时." + tex.Message.ToString());
                }
                catch (Exception exp)
                {
                    return new NoteResult(NoteState.NG, code, "查询" + this.code2Type + "编码绑定信息失败." + exp.Message.ToString());
                }
                string bindcode = "";// Code2已绑定的编码
                switch (this.code1Type)
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

                if (!string.IsNullOrEmpty(bindcode) && bindcode != code1)
                {
                    return new NoteResult(NoteState.NG, code, "当前" + this.code2Type + "编码已绑定，对应" + this.code1Type + "编码：" + bindcode);
                }
            }//if (BaseUI.UI_IsBind)
            #endregion 编码合法性检测

            // 以上检测项全部通过，返回OK
            return new NoteResult(NoteState.Success, code, "检测通过");
        }

        /// <summary>
        /// 生成并发送字符串
        /// </summary>
        private NoteResult BuildAndSend()
        {
            string sendString = "";
            string code = "";
            // 判断当前站点所属类型
            // 过站扫描点
            if (!BaseUI.UI_IsBind)
            {
                sendString = "RGJT{RGZD;" + this.mouldcode + ";" + txtProcessIP.Text + ";" + txtGlassCode.Text.Trim() + ";" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + "}";
                code = txtCode1.Text.Trim();
            }
            else
            {
                // 关键扫描点
                sendString = "RGJT{RGZD;" + this.mouldcode + ";" + txtProcessIP.Text + ";" + txtGlassCode.Text.Trim() + ";" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + "}";
                code = txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim();
            }

            try
            {
                if (client != null && client.connected)
                {
                    client.Send(client.StringToHexString(sendString, System.Text.Encoding.GetEncoding("GBK")));

                    return new NoteResult(NoteState.OK, DateTime.Now, code, "发送成功：" + sendString);
                }
                else
                {
                    return new NoteResult(NoteState.Error, DateTime.Now, code, "服务器已断开连接");
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
                return new NoteResult(NoteState.Error, DateTime.Now, code, "发送信息到服务器发生异常." + exp.Message.ToString());
            }
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

        ///// <summary>
        ///// 获取Code1的过站信息
        ///// </summary>
        //private void GetProcessData1()
        //{
        //    this.processData = GetHBaseDataClass.Instance.GetProductionRouteByCode(this.ScanCode1);//Code1查询结果
        //}

        ///// <summary>
        ///// 获取Code2的过站信息
        ///// </summary>
        //private void GetProcessData2()
        //{
        //    this.processData2 = GetHBaseDataClass.Instance.GetProductionRouteByCode(this.ScanCode2);//Code2查询结果
        //}
        /// <summary>
        /// 获取Code1对应的玻璃基本信息和当前状态
        /// </summary>
        private void GetProcessData1()
        {
            this.glassInfo = GetHBaseDataClass.Instance.GetGlassState(this.ScanCode1);//Code1查询结果
        }
        /// <summary>
        /// 获取Code2的过站信息
        /// </summary>
        private void GetProcessData2()
        {
            this.bindInfo = GetHBaseDataClass.Instance.GetProductInfo(this.ScanCode2);//Code2查询结果
        }

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
            string code = blCode + "|" + qrCode;
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
                    string sendString = "RGJT{RGZD;" + this.mouldcode + ";" + this.ipQrBinding + ";" + lcdCode + ";" + blCode + "|" + qrCode + ";" + txtOpCode.Text.Trim() + ";0;null}";
                    
                    // 发送数据到服务器
                    try
                    {
                        if (client != null && client.connected)
                        {
                            client.Send(client.StringToHexString(sendString, System.Text.Encoding.GetEncoding("GBK")));

                            ShowResult(NoteState.OK, DateTime.Now, code, "发送成功：" + sendString);
                        }
                        else
                        {
                            ShowResult(NoteState.Fail, DateTime.Now, code, "服务器已断开连接");
                        }
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error(exp.Message, exp);
                        ShowResult(NoteState.Fail, DateTime.Now, code, "发送信息到服务器发生异常." + exp.Message.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("成品码绑定失败", ex);
                ShowResult(NoteState.Fail, DateTime.Now, code, "成品码绑定失败." + ex.Message.ToString());
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
            ReaderTriggerOn(2, false);
        }

        /// <summary>
        /// 测试串口数据接收处理
        /// </summary>
        /// <param name="e"></param>
        private void TestCom_DataReceived(Utils.DataReceivedEventArgs e)
        {
            //byte[] rev = e.BytesReceived;
            //if (byteToHexStr(rev) == "57AB0101787C")
            //{
            //    byte[] cmd = SerialPortUtil.HexToByte("57AB810187");
            //    this.TestCom.WriteData(cmd);
            //}
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
            ScanDataHandler(txtFPC.Text, txtCode1);
        }
        /// <summary>
        /// 发送2#编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTP_Click(object sender, EventArgs e)
        {
            ScanDataHandler(txtTP.Text, txtCode2);
        }

        #endregion 测试程序
        /// <summary>
        /// 扫描枪1#心跳检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRead1Heatbeat_Tick(object sender, EventArgs e)
        {
            this.Reader1Heatbeat += 1;

            if (this.Reader1Heatbeat == 3 )
            {
                this.Code1_Reader.ExecCommand("%KEYENCE");
                ShowResult(NoteState.Success, DateTime.Now, "%KEYENCE", this.code1Type + "扫码枪通信检测");
            }
            else if (this.Reader1Heatbeat >= 4)
            {
                this.Reader1Heatbeat = 0;
                this.tmrRead1Heatbeat.Stop();
                btnReset.PerformClick();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRead2Heatbeat_Tick(object sender, EventArgs e)
        {
            this.Reader2Heatbeat += 1;

            if (this.Reader2Heatbeat == 3)
            {
                this.Code2_Reader.ExecCommand("%KEYENCE");
                ShowResult(NoteState.Success, DateTime.Now, "%KEYENCE", this.code2Type + "扫码枪通信检测");
            }
            else if (this.Reader2Heatbeat >= 4)
            {
                this.Reader2Heatbeat = 0;
                this.tmrRead2Heatbeat.Stop();
                btnReset.PerformClick();
            }

        }
    }
}
