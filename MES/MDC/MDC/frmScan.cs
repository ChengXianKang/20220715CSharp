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

namespace MDC
{
    public partial class frmScan : Form
    {
        #region 私有字段
        ///// <summary>
        ///// HBase数据库操作类
        ///// </summary>
        //private GetHBaseDataClass GHDC;
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
        /// 当前已过工序的Dictionary
        /// </summary>
        private Dictionary<string, string> dicProcess;

        /// <summary>
        /// 扫描码查询的已过工序信息表
        /// </summary>
        private DataTable processData;
        /// <summary>
        /// 对应码查询的已过工序信息表
        /// </summary>
        private DataTable processData2;
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
        /// 当前站点的类别（关键、过站、补扫）
        /// </summary>
        private SiteType siteType;
        /// <summary>
        /// 当前工序是否关键工序
        /// </summary>
        private bool isBind;
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
        #endregion 私有字段

        #region 构造函数
        /// <summary>
        /// frmMain的构造函数
        /// </summary>
        public frmScan()
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
            txtAnalysisCode.Dock = DockStyle.Fill;
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
            flpScanNote.Dock = DockStyle.Fill;

            txtSPLName.Clear();
            txtIPAddress.Clear();
            txtPort.Clear();
            txtProcessIP.Clear();
            txtOpCode.Clear();
            txtEarlyProcess.Clear();
            txtModel.Clear();
            txtOrder.Clear();
            txtScanCode.Clear();
            txtAnalysisCode.Clear();
            txtglassCode.Clear();
            txtRule1.Clear();
            txtRule2.Clear();
            txtCode1.Clear();
            txtCode2.Clear();
            txtDigit1.Clear();
            txtDigit2.Clear();
            txtProcessNumber.Clear();
            txtProcessCode.Clear();
            txtLCDCount.Text = "0";
            txtOKCount.Text = "0";
            txtNGCount.Text = "0";
            txtErrorCount.Text = "0";
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
        private void frmScan_Load(object sender, EventArgs e)
        {
            try
            {
                int width = (flpScanNote.Width - 3) / 25 - 1;
                if (width <= 0)
                {
                    width = 1;
                }
                this.lblWidth = width;

                // 绑定串口数据接收事件处理程序
                //Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;
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

        /// <summary>
        /// 窗体显示时，连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_Shown(object sender, EventArgs e)
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
        private void frmScan_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
            //CloseDeviceCom();
        }

        /// <summary>
        /// 窗体尺寸变化时，刷新显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_SizeChanged(object sender, EventArgs e)
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
                // 断开服务器
                ClientStop();

                // 关闭串口
                Program.CloseDeviceCom();

                // 清空数据
                txtScanCode.Clear();
                txtAnalysisCode.Clear();
                txtglassCode.Clear();
                txtCode1.Clear();
                txtCode2.Clear();
                comProcess.SelectedIndex = -1;
                this.LCD_Count = 0;
                this.OK_Count = 0;
                this.NG_Count = 0;
                this.Error_Count = 0;
                txtLCDCount.Text = "0";
                txtOKCount.Text = "0";
                txtNGCount.Text = "0";
                txtErrorCount.Text = "0";
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

                //// 初始化选择生产型号列表
                //BindMaterialComboBox();
            }
            catch (Exception ex)
            {
                LogHelper.Error("复位失败", ex);
                ShowResult(NoteState.Error, "", "程序初始化失败!" + ex.Message);
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
                if ((BaseUI.UI_NoAccessProcess != null && BaseUI.UI_NoAccessProcess.Contains(processCode)) || (BaseUI.UI_OnlyAccessProcess != null && !BaseUI.UI_OnlyAccessProcess.Contains(processCode)))
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
            if (comProcess.SelectedIndex == -1)
            {
                ShowResult(NoteState.NG, "", "未选择本站工序.");
                return;
            }
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
            frmNG FormNG = new frmNG();
            DialogResult rst = FormNG.ShowDialog(this);
            if (rst == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fpc in FormNG.NGGlassList.Keys)
                {
                    ShowResult(NoteState.Success, fpc, "不良申报成功，不良描述：" + FormNG.NGDesc);
                }
            }
            Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;
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
            // 当前程序版本
            this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // 登录人员账号
            txtOpCode.Text = BaseUI.UI_BOOLOGNAME;
            // 玻璃信息查询超时时间
            searchTimeout = appConfig.GetConfigInt("SearchTimeout");
            // 设备编码
            txtMouldCode.Text = YJ.Common.Utils.GetAppConfig("mouldcode", "appSettings");
            // 当前站点IP
            this.ipAddress = BaseUI.GetLocalIP();
            txtProcessIP.Text = this.ipAddress;

            try
            {
                // 获取本机IP所在的产线、工序、产线在制品的型号
                BaseUI.GetLineModelProcedure(this.ipAddress);
            }
            catch (Exception ex)
            {
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

//#if DEBUG
//                string[] ip = BaseUI.UI_SPLIPAddress.Split('.');
//                if (ip.Length == 4)
//                {
//                    txtIPAddress.Text = string.Format("192.168.41.{0}", ip[3]);
//                }
//#endif

            // 上报服务器端口号
            txtPort.Text = BaseUI.UI_SPLPORT;
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
            }
            else
            {
                this.siteType = SiteType.过站扫描点;
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
                txtAnalysisCode.BorderStyle = BorderStyle.FixedSingle;
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

                // 判断本机IP对应的工序编码是补扫还是正常工序
                if (this.siteType == SiteType.过站补扫点)
                {
                    if (GXCode == null)
                    {
                        // 设置程序标题
                        this.Text = this.siteType.ToString() + "--【请选择本站工序】   版本:" + this.versionName;
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
                            this.Text = this.siteType.ToString() + "--【请选择本站工序】   版本:" + this.versionName;
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
                            // new SuccessMessage("操作成功", 2).ShowDialog(this);
                            this.successMsg = new SuccessMessage("操作成功", 2);
                            this.successMsg.ShowDialog(this);
                            break;

                        case NoteState.NG:
                            // 弹框提示
                            //new FailMessage(message, 1000).ShowDialog(this);
                            this.failMsg = new FailMessage(message, 1000);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;

                        case NoteState.Fail:
                            // 弹框提示
                            //new FailMessage(message, 1000).ShowDialog(this);
                            this.failMsg = new FailMessage(message, 1000);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;

                        case NoteState.Error:
                            //// 记录错误日志
                            //LogHelper.Error(message, new Exception(message));
                            //Log4netProvider.Logger.Error(message);
                            // 弹框提示
                            // new FailMessage(message, 1000).ShowDialog(this);
                            this.failMsg = new FailMessage(message, 1000);
                            this.failMsg.ShowDialog(this);
                            IsScan = true;
                            break;
                    }
                    txtScanCode.Clear();
                    txtAnalysisCode.Clear();
                    txtglassCode.Clear();
                    txtCode1.Clear();
                    txtCode2.Clear();
                    txtScanCode.Focus();
                    // 清除扫码枪串口接收缓冲区
                    //Program.ScanDevice.DiscardInBuffer();
                    Program.ScanDevice.DiscardBuffer();
                }
            }
            catch (Exception)
            {

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
                ////型号下拉框权限
                //comMaterial.Enabled = BaseUI.UI_RIGHT.ContainsKey(RCodeMaterial);
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
                // 筛选当前选择的工序信息
                this.dvProcess = new DataView(dtProcessTable);
                this.dvProcess.RowFilter = "SGX_JobCode='" + comProcess.SelectedValue + "'";
                if (this.dvProcess.Count > 0)
                {
                    //txtSPLName.Text = this.dvProcess[0]["lineName"].ToString();             //产线编码
                    txtProcessNumber.Text = this.dvProcess[0]["PR_NoSeq"].ToString();          //当前工序序号
                    txtProcessCode.Text = this.dvProcess[0]["SGX_JobCode"].ToString();      //当前工序编码
                    //txtEarlyProcess.Text = sqlView[0]["EarlyProcessName"].ToString();   //前置工序
                    BaseUI.UI_CurrentProcedureNo = this.dvProcess[0]["PR_NoSeq"].ToString();          //当前工序序号
                    BaseUI.UI_CurrentProcedureCode = this.dvProcess[0]["SGX_JobCode"].ToString();      //当前工序编码
                    //当前站点IP地址,补扫是获取的工序对应IP，正常工序为本机IP
                    BaseUI.UI_EquCardIP = this.siteType == SiteType.过站补扫点 ? this.dvProcess[0]["STW_CardIP"].ToString() : this.ipAddress;
                    txtProcessIP.Text = BaseUI.UI_EquCardIP;

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
                    txtAnalysisCode.Clear();
                    txtglassCode.Clear();
                    txtCode1.Clear();
                    txtCode2.Clear();

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
                    }
                    else
                    {
                        // 是否关键工序
                        this.isBind = Convert.ToBoolean(this.dvProcess[0]["SGX_IsBind"]);//是否关键工序

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
                        }
                        else
                        {
                            if (scanType.Length != 1)
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
                        }
                    }
                    // 设置程序标题
                    this.Text = this.siteType.ToString() + "--" + this.dvProcess[0]["SGX_Name"].ToString() + "   版本:" + this.versionName;
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
        ///// <summary>
        ///// 接收扫描枪数据
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void ScanDevice_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        if (!Program.ScanDevice.IsOpen)
        //        {
        //            OpenDeviceCom();
        //        }
        //        Thread.Sleep(200);
        //        byte[] ReDatas = new byte[Program.ScanDevice.BytesToRead];
        //        Program.ScanDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据
        //        string DataString = (new UTF8Encoding().GetString(ReDatas)).Replace("\r", "").Replace("\n", "");
        //        Debug.WriteLine("收到串口数据：" + DataString);
        //        // 分析处理数据
        //        ScanDataHandler(DataString);
        //    }
        //    catch (Exception exp)
        //    {
        //        LogHelper.Error(exp.Message, exp);
        //        ShowResult(NoteState.Error, Program.ScanDevice.PortName, "扫描设备接收数据失败." + exp.Message.ToString());
        //    }
        //}

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
                            }
                            else
                            {
                                msg = "获取当前站点工序失败.";
                            }
                            ShowResult(NoteState.Fail, "", msg);
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
                        txtAnalysisCode.Text = ds[0].ToString().Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");    //扫描数据解析

                        // 基本长度判断
                        if (txtAnalysisCode.Text.Length <= 9)
                        {
                            ShowResult(NoteState.Fail, DataString, "默认编码长度至少为10位以上，请确认条码长度");
                            return;
                        }
                        #endregion 第2步：解析扫描数据

                        NoteResult rst = null;

                        // 第3步：判断是否合法的Code2（如果有）
                        #region 第3步：判断是否合法的Code2（如果有）
                        if (txtCode2.Visible)
                        {
                            // 判断是否合法的Code2
                            rst = isValidCode2(txtAnalysisCode.Text);
                        }
                        #endregion 第3步：判断是否合法的Code2（如果有）

                        // 第4步：判断是否合法的Code1
                        #region 第4步：判断是否合法的Code1
                        if (rst == null)
                        {
                            // 判断是否合法的Code1
                            rst = isValidCode1(txtAnalysisCode.Text);
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
                                txtAnalysisCode.Clear();
                                txtScanCode.Clear();
                                txtScanCode.Focus();
                                break;
                            case NoteState.OK:
                                // 生成发送字符串
                                string sendCode = GetSendString();
                                if (sendCode == "")
                                {
                                    ShowResult(NoteState.NG, "", "生成发送字符串失败!");
                                    return;
                                }

                                // 发送数据到服务器
                                rst = ClientSend(sendCode.Replace("\r", "").Replace("\n", ""));
                                if (rst.State == NoteState.OK)
                                {
                                    this.LCD_Count += 1;
                                    this.OK_Count += 1;
                                    txtLCDCount.Text = this.LCD_Count.ToString();
                                    txtOKCount.Text = this.OK_Count.ToString();
                                    AddScanNote("0");
                                }
                                ShowResult(rst);
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
        /// 判断是否合法的Code2
        /// </summary>
        /// <param name="DataString">扫描编码</param>
        /// <returns>NoteResult</returns>
        private NoteResult isValidCode2(string DataString)
        {
            NoteResult rst = null;
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
                if (txtDigit2.Text == "0" || txtAnalysisCode.Text.Length == YJ.Common.Utils.StrToInt(txtDigit2.Text, 0))
                {
                    // 不匹配Code1规则，匹配Code2规则
                    if (txtAnalysisCode.Text.Substring(0, txtRule1.Text.Length) != txtRule1.Text && txtAnalysisCode.Text.Substring(0, txtRule2.Text.Length) == txtRule2.Text)
                    {
                        txtCode2.Text = txtAnalysisCode.Text;

                        // 关键工序必须先扫描Code1
                        if (this.isBind && string.IsNullOrWhiteSpace(txtCode1.Text))
                        {
                            IsScan = false;
                            return new NoteResult(NoteState.Fail, DataString, "请先扫描" + this.scanType1 + "编码");
                        }

                        // 查询Code2绑定信息
                        try
                        {
                            //this.processData2 = GHDC.GetProductionRouteByCode(txtCode2.Text);
                            //异步查询Code1过站信息
                            CallWithTimeout(GetProcessData2, this.searchTimeout);
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
                        if (processData2 != null && processData2.DefaultView.Count > 0)
                        {
                            bindcode = processData2.DefaultView[0]["Process_" + this.scanType1 + "Code"].ToString();
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
                                rst = new NoteResult(NoteState.Error, txtCode2.Text, "当前" + this.scanType2 + "编码已绑定，对应" + this.scanType1 + "编码：" + bindcode);
                            }
                        }
                        else
                        {
                            // 非关键工序可扫描Code2的只有COG补扫
                            if (string.IsNullOrEmpty(bindcode))
                            {
                                // Code2查询不到FOG绑定信息，返回NG
                                rst = new NoteResult(NoteState.NG, txtCode2.Text, "获取FOG绑定信息失败");
                            }
                            else
                            {
                                // COG允许扫FPC进行补扫
                                txtCode1.Text = bindcode;
                                txtglassCode.Text = bindcode;
                                rst = new NoteResult(NoteState.OK);
                            }
                        }//if (this.isBind)
                    }//if匹配规则
                }//if匹配长度
            }//if (txtCode2.Visible)
            return rst;
        }

        /// <summary>
        /// 判断是否合法的Code1
        /// </summary>
        /// <param name="DataString">扫描编码</param>
        /// <returns>NoteResult</returns>
        private NoteResult isValidCode1(string DataString)
        {
            NoteResult rst = null;
            txtglassCode.Text = "";

            #region 局部变量
            string productionOrder = "";//生产订单
            string productDate = "";//生产日期
            string lineCode = "";//产线编码
            string blCode = "";//背光编码
            string glassCode = "";//玻璃码
            string fpcCode = "";//FPC编码
            string tpCode = "";//TP编码
            string intactCode = "";//54位成品码
            string icCode = "";//ic编码
            string finishesCode = "";//成品编码
            string finishesModel = "";//成品规格型号
            string clientProductNo = "";//客户料号
            string logNumber = "";//产品已过工序序号
            string logCode = "";//产品已过工序编码
            this.processData = null;
            #endregion 局部变量

            #region 从HBase数据库查询过站信息
            try
            {
                //this.processData = GHDC.GetProductionRouteByCode(DataString);//Code1查询结果
                //异步查询Code1过站信息
                CallWithTimeout(GetProcessData1, this.searchTimeout);
                if (this.processData == null)
                {
                    IsScan = false;
                    return new NoteResult(NoteState.Fail, DataString, "内部查询玻璃绑定信息失败,如多次出现此错误，请关掉程序重新打开");
                }

                if (this.processData.DefaultView.Count > 0)
                {
                    DataRowView rv = this.processData.DefaultView[0];
                    if (rv["Process_LCDCode"].ToString() != "")
                    {
                        //txtglassCode.Text = rv["Process_LCDCode"].ToString();//玻璃码  
                        productionOrder = rv["Process_productionOrder"].ToString();//生产订单
                        productDate = rv["Process_productDate"].ToString();//生产日期
                        lineCode = rv["Process_productLineCode"].ToString();//产线编码
                        blCode = rv["Process_BLCode"].ToString();//背光编码
                        glassCode = rv["Process_LCDCode"].ToString();//玻璃码
                        fpcCode = rv["Process_FPCCode"].ToString();//FPC编码
                        tpCode = rv["Process_TPCode"].ToString();//TP编码
                        intactCode = rv["Process_QRCode"].ToString();//54位成品码
                        icCode = rv["Process_icCode"].ToString();//ic编码
                        finishesCode = rv["Process_finishesCode"].ToString();//成品编码
                        finishesModel = rv["Process_finishesModel"].ToString();//成品规格型号
                        clientProductNo = rv["Process_clientProductNo"].ToString();//客户料号
                        logNumber = rv["Process_logNumber"].ToString();//产品已过工序序号
                        logCode = rv["Process_logCode"].ToString();//产品已过工序编码
                    }
                    rv = null;
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

            #region 混线生产检测

            if (lineCode != "" & lineCode != BaseUI.UI_SPLJOBCODE)
            {
                string LineName = BaseUI.GetLineName(lineCode);
                IsScan = false;
                return new NoteResult(NoteState.Error, DataString, "该玻璃生产产线为" + LineName + ",与当前产线不一致。");
            }

            #endregion 混线生产检测

            #region 切换工单，重新初始化页面
            if (!string.IsNullOrEmpty(productionOrder) && productionOrder != txtOrder.Text)
            {
                // 初始化工序
                BindProcessComboBox(productionOrder, lineCode);
                //// 初始化页面
                //PageInit();
                txtOrder.Text = productionOrder;
                txtModel.Text = finishesModel;
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
                if (DataString.Substring(0, txtRule1.Text.Length) == txtRule1.Text)
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

            txtglassCode.Text = glassCode;//玻璃码

            #endregion Code1编码规则检查

            #region 不良玻璃检测
            FailReviewResult reviewResult = BaseUI.GetFailReviewResult(txtglassCode.Text);
            switch (reviewResult)
            {
                case FailReviewResult.已申报未复判:
                    IsScan = false;
                    return new NoteResult(NoteState.Error, DataString, "此玻璃已申报不良，\r\n请提交复判！");

                case FailReviewResult.复判报废:
                    IsScan = false;
                    return new NoteResult(NoteState.Error, DataString, "此玻璃已报废，\r\n不能过站！");
            }

            #endregion 不良玻璃检测

            #region 漏工序检测

            string Procedure = BaseUI.GetLeakProcedure(logCode, BaseUI.NowCode, dicProcess);
            if (Procedure != "")
            {
                string msg = "检测到工序 " + Procedure + " 未扫描.";
                IsScan = false;
                return new NoteResult(NoteState.NG, DataString, msg);
            }
            bool Isrepeat = BaseUI.CheckProcedure(logCode);
            if (Isrepeat)
            {
                IsScan = false;
                return new NoteResult(NoteState.Error, DataString, "检测到当前工序已扫描,\n请勿重复扫描。");
            }

            #endregion 漏工序检测

            #region 编码合法性检测
            // 第一道工序，检查LCD编码是否已存在
            if (txtProcessNumber.Text == "1" && !string.IsNullOrWhiteSpace(txtglassCode.Text))
            {
                IsScan = false;
                return new NoteResult(NoteState.Error, DataString, "当前玻璃码已绑定，请勿多次绑定");
            }
            // 其他工序，检查是否获取玻璃信息
            if (txtProcessNumber.Text != "1" && string.IsNullOrWhiteSpace(txtglassCode.Text))
            {
                IsScan = false;
                return new NoteResult(NoteState.NG, DataString, "获取FOG绑定信息失败");
            }
            #endregion 编码合法性检测
            
            // 过站工序，扫了Code1直接提交
            if (!this.isBind)
            {
                rst = new NoteResult(NoteState.OK);
            }
            // 关键工序，等待扫Code2
            else
            {
                // 匹配成功，等待扫描第二个码
                return new NoteResult(NoteState.Success);
            }

            return rst;
        }

        /// <summary>
        /// 生成发送字符串
        /// </summary>
        /// <returns>string</returns>
        private string GetSendString()
        {
            string sendString = "";

            // 判断当前站点所属类型
            switch (this.siteType)
            {
                // 过站扫描点
                case SiteType.过站扫描点:
                    sendString = "RGJT{RGZD;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";" + txtglassCode.Text.Trim() + ";" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + "}";
                    break;

                // 关键扫描点
                case SiteType.关键扫描点:
                    sendString = "RGJT{RGZD;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";" + txtglassCode.Text.Trim() + ";" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + "}";
                    break;

                // 过站补扫点
                default:
                    // 扫描两个编码，补扫关键工序
                    if (this.isBind)
                    {
                        // 补扫TP贴合
                        if (txtProcessCode.Text == "018")//TP贴合
                        {
                            sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";TF}";
                        }
                        else if (txtProcessCode.Text == "032")//54位成品喷码补发
                        {
                            sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";CP}";
                            //sendString = "CPPM{CPM;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";" + txtCode2.Text.Trim() + "||" + txtCode1.Text.Trim() + ";1|1|0;0;;0;" + DateTime.Now.ToString("yyyyMMddHHmmss") + "}";
                        }
                        else
                        {
                            sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode2.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#}";
                        }

                    }
                    // 只扫描一个编码，补扫过站工序
                    else
                    {
                        sendString = "RGJT{JTBF;" + txtMouldCode.Text + ";" + txtProcessIP.Text + ";;" + txtCode1.Text.Trim() + "|" + txtCode1.Text.Trim() + ";" + txtOpCode.Text.Trim() + ";#}";
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

        /// <summary>
        /// 获取Code1的过站信息
        /// </summary>
        private void GetProcessData1()
        {
            this.processData = GetHBaseDataClass.Instance.GetProductionRouteByCode(txtAnalysisCode.Text);//Code1查询结果
        }

        /// <summary>
        /// 获取Code2的过站信息
        /// </summary>
        private void GetProcessData2()
        {
            this.processData2 = GetHBaseDataClass.Instance.GetProductionRouteByCode(txtAnalysisCode.Text);//Code1查询结果
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
    }
}
