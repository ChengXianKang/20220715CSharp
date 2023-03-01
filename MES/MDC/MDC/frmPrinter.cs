using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using CCS_3000Demo.BaseClass;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace CCS_3000Demo
{
    public partial class frmPrinter : Form
    {
        //异步处理扫码数据
        private BackgroundWorker bgw = new BackgroundWorker();
        private BackgroundWorker bgwPrint = new BackgroundWorker();
        //等待提示框
        private dlgWaiting WaitingBox = new dlgWaiting("", false);
        //扫描编码
        private string scanCode = "";
        //是否重喷新码
        private bool isRepeat = false;
        //查询结果
        private JObject jsonStr = null;

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern uint GetTickCount();

        public frmPrinter()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }

        private void frmPrinter_Load(object sender, EventArgs e)
        {
            try
            {
                TextBox.CheckForIllegalCrossThreadCalls = false;
                // 上报服务器IP地址
                string interfaceIP = null;
                try
                {
                    interfaceIP = YJ.Common.Utils.GetAppConfig("Receive_Server_IP", "appSettings");
                }
                catch (Exception)
                {
                    // APP.config中无HBase_Interface_IP字段
                }
                if (string.IsNullOrEmpty(interfaceIP))
                {
                    // 未配置服务器地址HBase_Interface_IP，则使用数据库获取的上报服务器IP地址
                    txtReciveServerIP.Text = CheckResult.LINE_IP;//"192.168.41.3";
                }
                else
                {
                    txtReciveServerIP.Text = interfaceIP;
                }

                txtReciveServerPort.Text = CheckResult.LINE_PORT;
                txtScanRule.Text = CheckResult.FPCRule;
                txtScan710ServerIP.Text = YJ.Common.Utils.GetAppConfig("Scan710ServerIP", "appSettings");
                txtScan710ServerPort.Text = YJ.Common.Utils.GetAppConfig("Scan710ServerPort", "appSettings");
                this.Text = "同兴达MES喷码系统  本机IP:" + CheckResult.LOCAL_IP + "   本机所属产线:" + CheckResult.LINE_CODE + "   当前版本号：" + CheckResult.VERNNAME;


                bgw.WorkerSupportsCancellation = true;
                bgw.DoWork += bgw_DoWork;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;

                bgwPrint.WorkerReportsProgress = true;
                bgwPrint.WorkerSupportsCancellation = true;
                bgwPrint.DoWork += new DoWorkEventHandler(bgwPrint_DoWork);
                bgwPrint.ProgressChanged += new ProgressChangedEventHandler(bgwPrint_ProgressChanged);
                bgwPrint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwPrint_RunWorkerCompleted);

                string[] portnames = SerialPort.GetPortNames();
                foreach (string name in portnames)
                {
                    cmbPrinterPorts.Items.Add(name);
                }
                txtScanCode.Enabled = false;
                cmbPrinterBaudRate.SelectedIndex = 0;//默认波特率9600
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error("喷码机页面加载失败" + exp.Message.ToString());
            }
        }


        private void btn710Scan_Click(object sender, EventArgs e)
        {
            try
            {
                if (chk710Scan.Checked)
                {
                    CheckPassWord dlg = new CheckPassWord();
                    dlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    dlg.ShowDialog();
                    if (CheckResult.CHECK_PASSWORD_RESULT == "True") //显示返回的值
                    {
                        chk710Scan.Checked = false;
                        lbtxtScanRule.Text = "FPC";
                        txtScanRule.Text = CheckResult.FPCRule;
                        lbtxtScanCode.Text = "FPC编码:";

                        lbtxtScan710ServerIP.Visible = false;
                        txtScan710ServerIP.Visible = false;
                        lbtxtScan710ServerPort.Visible = false;
                        txtScan710ServerPort.Visible = false;

                        if (scan710Client != null && scan710Client.connected)
                        {
                            //关闭扫码枪4C 4F 46 46 0D 0A
                            scan710Client.SendMessageToServer("4C 4F 46 46 0D 0A");
                            scan710Client.ClientStockClose();
                            scan710Client.socket.Close();
                            scan710Client.socket.Dispose();
                        }
                    }
                    else if (CheckResult.CHECK_PASSWORD_RESULT == "False")
                    {
                        MessageBox.Show("密码不正确或权限不足");
                    }
                }
                else
                {
                    CheckPassWord dlg = new CheckPassWord();
                    dlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    dlg.ShowDialog();
                    if (CheckResult.CHECK_PASSWORD_RESULT == "True") //显示返回的值
                    {
                        chk710Scan.Checked = true;
                        lbtxtScanRule.Text = "B/L";
                        txtScanRule.Text = CheckResult.BLRule;
                        lbtxtScanCode.Text = "B/L编码:";

                        lbtxtScan710ServerIP.Visible = true;
                        txtScan710ServerIP.Visible = true;
                        txtScan710ServerIP.Enabled = false;
                        lbtxtScan710ServerPort.Visible = true;
                        txtScan710ServerPort.Visible = true;
                        txtScan710ServerPort.Enabled = false;


                        ConnectScan710Server();//启动基恩士710服务器连接
                    }
                    else if (CheckResult.CHECK_PASSWORD_RESULT == "False")
                    {
                        MessageBox.Show("密码不正确或权限不足");
                    }
                }
                txtScanCode.Text = "";
                txtScanCode.Focus();
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPrinterPorts.Text.Equals("") || cmbPrinterBaudRate.Text.Equals(""))
                {
                    txt_ErrNote.Text = "请设置串口号和波特率!";
                    txtScanCode.Enabled = false;
                }
                else
                {
                    txtScanCode.Enabled = true;
                    txt_ErrNote.Text = "";
                    ConnectReciveServer();//启动数据接收服务器连接
                    if (chk710Scan.Checked)
                    {
                        ConnectScan710Server();//启动基恩士710服务器连接
                    }
                    if (PortPrinter == null)
                    {
                        PortPrinter = new SerialPort(cmbPrinterPorts.Text, Convert.ToInt32(cmbPrinterBaudRate.Text), Parity.None, 8, StopBits.One);
                    }
                    else
                    {
                        CCS3000.ClosePort(ref PortPrinter);//打开前先初始化
                        PortPrinter.PortName = cmbPrinterPorts.Text;
                        PortPrinter.BaudRate = Convert.ToInt32(cmbPrinterBaudRate.Text);
                    }
                    if (!PortPrinter.IsOpen)
                    {
                        PortPrinter.Open();
                    }

                    string cmd = "SRC:0:1:1:";
                    if (CCS3000.SetCmd(cmd, PortPrinter) == CCS3000.RESULT_ACK)
                    {
                        txt_ErrNote.AppendText("连接喷码机成功!\r\n");
                        btnConnect.Enabled = false;
                        btnDisconnect.Enabled = true;
                        txtSendCode.Enabled = true;
                        cmbPrinterPorts.Enabled = false;
                        cmbPrinterBaudRate.Enabled = false;
                        txtReciveServerIP.Enabled = false;
                        txtReciveServerPort.Enabled = false;
                        txtScanCode.Focus();
                        btnRepeat.Enabled = true;
                        btn710Scan.Enabled = true;
                    }
                    else
                    {
                        txt_ErrNote.AppendText("连接喷码机失败，请检查喷码机是否在运行状态，连接线是否固定在COM口上。");
                        txt_ErrNote.AppendText("\r\n 检测所选COM口与实际线路连接一致。--" + DateTime.Now.ToString() + "\r\n");
                        //写日志
                        YJ.Log.FileLog.log.Error("连接喷码机失败，请检查喷码机是否在运行状态，连接线是否固定在COM口上。");
                    }
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
                txt_ErrNote.AppendText("发生未知异常：\r\n" + exp.ToString());
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                CCS3000.ClosePort(ref PortPrinter);
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                QrPanel.Enabled = false;
                cmbPrinterPorts.Enabled = true;
                cmbPrinterBaudRate.Enabled = true;
                SocketClientShurtDown();
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }


        private bool SendQrCodeToPrinter(string command)
        {
            try
            {
                if (CCS3000.SetCmd(command, PortPrinter) == CCS3000.RESULT_ACK)
                {
                    YJ.Log.FileLog.log.Info(string.Format("发送数据到喷码机成功：{0}", command));
                    return true;
                }
                else
                {
                    YJ.Log.FileLog.log.Info(string.Format("发送数据到喷码机失败：{0}", command));
                    return false;
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
                return false;
            }
        }

        #region 启动reciveServer监听
        private Client reciveClient;
        private Client scan710Client;
        private void ConnectReciveServer()
        {
            try
            {
                //重新初始化工序链信息
                Program.InitLocalIP();
                //检测数据接收服务器监听地址与端口号
                if (string.IsNullOrEmpty(txtReciveServerIP.Text.ToString()))
                {
                    MessageBox.Show("客户端监听hBase服务器地址不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(txtReciveServerPort.Text.ToString()))
                {
                    MessageBox.Show("客户端监听端口不能为空！");
                    return;
                }
                //创建数据接收服务器监听客户端
                if (reciveClient == null)
                {
                    reciveClient = new Client(ReciveClientOutPutMessage, txtReciveServerIP.Text, txtReciveServerPort.Text);
                }
                //启动连接
                if (!reciveClient.connected)
                {
                    reciveClient.ipString = txtReciveServerIP.Text.Trim();
                    reciveClient.port = Convert.ToInt16(txtReciveServerPort.Text.Trim());
                    //添加进度响应条
                    frmWaitingBox f = new frmWaitingBox((obj, args) =>
                    {
                        reciveClient.ClientStockStart(false);
                    }, "客户端连接数据接收服务器中.....");
                    f.ShowDialog(this);
                }
                //返回连接结果
                if (reciveClient.connected)
                {
                    MethodInvoker p = delegate
                       {
                           txt_ErrNote.AppendText("本机： " + reciveClient.localIpPort + "客户端成功连接数据接收服务器\r\n");
                       };
                    txt_ErrNote.Invoke(new MethodInvoker(p));
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
                txt_ErrNote.AppendText("连接到数据接收服务器失败!\r\n" + exp.ToString());
            }
        }

        private void ConnectScan710Server()
        {
            try
            {
                //检测基恩士710服务器监听地址与端口号
                if (string.IsNullOrEmpty(txtScan710ServerIP.Text.ToString()))
                {
                    MessageBox.Show("客户端启动基恩士710监听，获取IP失败");
                    return;
                }
                if (string.IsNullOrEmpty(txtScan710ServerPort.Text.ToString()))
                {
                    MessageBox.Show("客户端监听端口不能为空！");
                    return;
                }
                //创建基恩士710服务器监听客户端
                if (scan710Client == null)
                {
                    scan710Client = new Client(Scan710ClientOutPutMessage, txtScan710ServerIP.Text, txtScan710ServerPort.Text);
                }
                //启动连接
                if (!scan710Client.connected)
                {
                    scan710Client.ipString = txtScan710ServerIP.Text.Trim();
                    scan710Client.port = Convert.ToInt16(txtScan710ServerPort.Text.Trim());
                    //添加进度响应条
                    frmWaitingBox f = new frmWaitingBox((obj, args) =>
                    {
                        scan710Client.ClientStockStart(false);
                    }, "客户端连接基恩士710服务器中.....");
                    f.ShowDialog(this);
                }
                //返回连接结果
                if (scan710Client.connected)
                {
                    MethodInvoker p = delegate
                    {
                        txt_ErrNote.AppendText("本机： " + scan710Client.localIpPort + "客户端成功连接基恩士710服务器\r\n");
                        //打开扫码枪
                        scan710Client.SendMessageToServer("4C 4F 4E 0D 0A");
                    };
                    txt_ErrNote.Invoke(new MethodInvoker(p));
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
                txt_ErrNote.AppendText("连接到基恩士710服务器失败!\r\n" + exp.ToString());
            }
        }
        // Hbase客户端输出服务端返回的消息
        private void ReciveClientOutPutMessage(string hbasemsg)
        {
            try
            {
                if (txt_ErrNote.InvokeRequired)
                {
                    Client.Print F = new Client.Print(ReciveClientOutPutMessage);
                    this.Invoke(F, new object[] { hbasemsg });
                }
                else
                {
                    if (hbasemsg != null)
                    {
                        txt_ErrNote.ForeColor = Color.Green;
                        txt_ErrNote.AppendText(hbasemsg + "--" + DateTime.Now.ToString("HH:mm:ss"));
                        txt_ErrNote.AppendText("\r\n");
                        txt_ErrNote.ScrollToCaret();
                    }
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }

        // 基恩士710客户端输出服务端返回的消息
        private void Scan710ClientOutPutMessage(string scan710msg)
        {
            try
            {
                if (txt_ErrNote.InvokeRequired)
                {
                    Client.Print F = new Client.Print(Scan710ClientOutPutMessage);
                    this.Invoke(F, new object[] { scan710msg });
                }
                else
                {
                    if (scan710msg != null)
                    {
                        try
                        {
                            txtScanCode.Text = new UTF8Encoding().GetString(scan710Client.strToHexByte(scan710msg)).Replace("\r", "").Replace("\n", "").Replace(" ", "");
                            txtScanCodeKeyPress();
                        }
                        catch { }
                    }
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }

        // 发送信息到数据接收服务器
        private string ReciveClientSendMsgToHbaseServer(string sendstring)
        {
            try
            {
                if (reciveClient != null && reciveClient.connected)
                {
                    reciveClient.SendMessageToServer(reciveClient.StringToHexString(sendstring, System.Text.Encoding.GetEncoding("GBK")));
                    return "True";
                }
                else
                {
                    return "服务器已断开连接";
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
                return "发送信息到数据接收服务器发生异常" + exp.Message.ToString();
            }
        }

        private void SocketClientShurtDown()
        {
            try
            {
                if (reciveClient != null && reciveClient.connected)
                {
                    reciveClient.ClientStockClose();
                    reciveClient.socket.Close();
                    reciveClient.socket.Dispose();
                }
                if (chk710Scan.Checked)
                {
                    if (scan710Client != null && scan710Client.connected)
                    {
                        //关闭扫码枪4C 4F 46 46 0D 0A
                        scan710Client.SendMessageToServer("4C 4F 46 46 0D 0A");
                        scan710Client.ClientStockClose();
                        scan710Client.socket.Close();
                        scan710Client.socket.Dispose();
                    }
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }

        #endregion

        #region 扫码后数据处理
        PromptMessageBox pmb;
        private void txtScanCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtScanCodeKeyPress();
            }
        }

        private void txtScanCodeKeyPress()
        {
            try
            {
                if (txtScanCode.Text.Trim().Length < txtScanRule.Text.Trim().Length)
                {
                    txt_ErrNote.Text = "扫码条码长度小于" + lbtxtScanRule.Text + "检测位数，请重新扫描" + lbtxtScanRule.Text + "条码！";
                    txt_ErrNote.AppendText("\r\n");
                    txtScanCode.SelectAll();
                    txtQrCode.Text = "";
                    txtLCDCode.Text = "";
                    txtSendCode.Text = "";
                    return;
                }
                if (txtScanCode.Text.Trim().Substring(0, txtScanRule.Text.Trim().Length).ToString() != txtScanRule.Text.Trim())
                {
                    txt_ErrNote.Text = "扫描条码与" + lbtxtScanRule.Text + "检测位不匹配，请重新扫描" + lbtxtScanRule.Text + "条码！";
                    txt_ErrNote.AppendText("\r\n");
                    txtScanCode.SelectAll();
                    txtQrCode.Text = "";
                    txtLCDCode.Text = "";
                    txtSendCode.Text = "";
                    return;
                }
                try
                {
                    //string ControlUrl = "";
                    //// 后台处理程序
                    //bgw.RunWorkerAsync(ControlUrl);
                    //pmb = new PromptMessageBox("正在获取成品码", 3000);
                    //pmb.ShowDialog(this);
                    //if (bgw.IsBusy)
                    //{
                    //    bgw.CancelAsync();
                    //}

                    object[] para = new object[]
                    {
                        txtScanCode.Text.Trim().Replace(" ", ""),
                        chkRepeat.Checked
                    };
                    if (bgwPrint == null)
                    {
                        bgwPrint = new BackgroundWorker();
                        bgwPrint.WorkerReportsProgress = true;
                        bgwPrint.WorkerSupportsCancellation = true;
                        bgwPrint.DoWork += new DoWorkEventHandler(bgwPrint_DoWork);
                        bgwPrint.ProgressChanged += new ProgressChangedEventHandler(bgwPrint_ProgressChanged);
                        bgwPrint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwPrint_RunWorkerCompleted);
                    }

                    bool istimeout = false;  // 读取超时
                    uint start = GetTickCount();  // 记录开始时间
                    while (!istimeout && bgwPrint.IsBusy)
                    {
                        if (GetTickCount() - start > 20000)
                        {
                            istimeout = true;
                        }
                        Application.DoEvents();
                    }
                    if (!istimeout)
                    {
                        bgwPrint.RunWorkerAsync(para);
                    }
                    else
                    {
                        YJ.Log.FileLog.log.Error("txtScanCode_KeyPress():  bgwPrint后台处理执行超时>20S");
                        if (!chk710Scan.Checked)
                        {
                            MessageBox.Show("txtScanCode_KeyPress():  bgwPrint后台处理执行超时>20S");
                        }
                        txt_ErrNote.AppendText("txtScanCode_KeyPress():  bgwPrint后台处理执行超时>20S");
                    }
                }
                catch (Exception exp)
                {
                    YJ.Log.FileLog.log.Error(exp.Message.ToString());
                    if (!chk710Scan.Checked)
                    {
                        MessageBox.Show(exp.Message);
                    }
                    txt_ErrNote.AppendText("txtScanCode_KeyPress  " + exp.ToString());
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }

        /// <summary>
        /// 喷码后台处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            //***********获取传参***********
            if (e.Argument != null)
            {
                object[] para = e.Argument as object[];
                this.scanCode = para[0].ToString();
                this.isRepeat = Convert.ToBoolean(para[1]);
            }
            else
            {
                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "参数错误，请重试！");
                JObject obj = JObject.Parse(result);
                e.Result = obj;
                return;
            }

            // ***********获取成品码***********
            // 提示进度
            bgwPrint.ReportProgress(10);//正在获取成品码...
            this.jsonStr = null;
            #region 获取成品码
            try
            {
                //异步查询Code1过站信息
                CallWithTimeout(GetQR, 5000);
            }
            catch (TimeoutException tex)
            {
                YJ.Log.FileLog.log.Error(tex.Message, tex);
                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "成品码获取超时");
                JObject obj = JObject.Parse(result);
                e.Result = obj;
                return;
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message, exp);
                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "成品码获取失败." + exp.Message);
                JObject obj = JObject.Parse(result);
                e.Result = obj;
                return;
            }

            if (this.jsonStr == null)
            {
                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "成品码获取失败.");
                JObject obj = JObject.Parse(result);
                e.Result = obj;
                return;
            }
            #endregion 获取成品码

            // ***********发送过站数据至接收服务器***********
            try
            {
                if (jsonStr["msg"].ToString() == "error")
                {
                    e.Result = jsonStr;
                    return;
                }
                else if (jsonStr["msg"].ToString() == "success")
                {
                    string QrCode = jsonStr["result"].ToString();
                    string LcdCode = jsonStr["lcdcode"].ToString();
                    string[] para = new string[] { QrCode, LcdCode };
                    // 提示进度
                    bgwPrint.ReportProgress(20, para); //已获取成品码
                    YJ.Log.FileLog.log.Info(string.Format("已获取成品码。LCD：{0}，QR：{1}", LcdCode, QrCode));

                    string SendCode = "";
                    if (CheckResult.PROCESS_CODE == "032")
                    {
                        SendCode = "CPPM{CPM;" + YJ.Common.Utils.GetAppConfig("mouldcode", "appSettings") + ";" + CheckResult.LOCAL_IP + ";" + QrCode.Trim() + "||" + this.scanCode.Trim() + ";1|1|0;0;;0;" + DateTime.Now.ToString("yyyyMMddHHmmss") + "}";
                    }
                    else if (CheckResult.PROCESS_CODE == "041")
                    {
                        if (this.isRepeat)
                        {
                            if(string.IsNullOrEmpty(CheckResult.QRBINDING_IP))
                            {
                                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "当前产线未配置工序032成品码生成(喷码)的机台IP");
                                JObject obj = JObject.Parse(result);
                                e.Result = obj;
                                return;
                            }
                            // 重新绑定新的成品编码
                            string qrbinding = "CPPM{CPM;" + YJ.Common.Utils.GetAppConfig("mouldcode", "appSettings") + ";" + CheckResult.QRBINDING_IP + ";" + QrCode.Trim() + "||" + this.scanCode.Trim() + ";1|1|0;0;;0;" + DateTime.Now.ToString("yyyyMMddHHmmss") + "}";
                            // 提示进度
                            bgwPrint.ReportProgress(25, qrbinding); //发送数据到接收服务器

                            string qrbindingResult = ReciveClientSendMsgToHbaseServer(qrbinding);
                            //string SendResultString = "True";
                            if (qrbindingResult != "True")
                            {
                                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "成品码绑定失败");
                                JObject obj = JObject.Parse(result);
                                e.Result = obj;
                                return;
                            }

                            YJ.Log.FileLog.log.Info(string.Format("已发送数据到接收服务器：{0}", qrbinding));
                        }
                        SendCode = "RGJT{RGZD;" + YJ.Common.Utils.GetAppConfig("mouldcode", "appSettings") + ";" + CheckResult.LOCAL_IP + ";" + LcdCode + ";" + this.scanCode.Trim() + "|" + this.scanCode.Trim() + ";#}";
                    }

                    // 提示进度
                    bgwPrint.ReportProgress(30, SendCode); //发送数据到接收服务器

                    string SendResultString = ReciveClientSendMsgToHbaseServer(SendCode);
                    //string SendResultString = "True";
                    if (SendResultString != "True")
                    {
                        string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", SendResultString);
                        JObject obj = JObject.Parse(result);
                        e.Result = obj;
                        return;
                    }

                    YJ.Log.FileLog.log.Info(string.Format("已发送数据到接收服务器：{0}", SendCode));

                    // ***********发送数据至喷码机***********
                    try
                    {
                        string FirstCommand = "", SecondCommand = "", ThirdCommand = "", FourthCommand = "";
                        if (QrCode.Length == 54)
                        {
                            #region 如果条码的长度为54位，发送给喷码机的指令需要分割为四部分
                            //第一部分 二维码的整体条码（54位）
                            if (!QrCode.Equals(""))
                            {
                                FirstCommand = string.Format("S2M:2:2:::::1:0:1{0}:", QrCode.Trim());
                            }
                            else
                            {
                                FirstCommand = string.Format("S2M:2:2:::::1:0:1 :");
                            }
                            //第二部分 二维码的前18位条码（18位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                SecondCommand = string.Format("SLM:0:1::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(0, 18)));
                            }
                            else
                            {
                                SecondCommand = string.Format("SLM:0:2::1 :");
                            }
                            //第三部分 二维码的中间18位条码（18位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                ThirdCommand = string.Format("SLM:0:2::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(18, 18)));
                            }
                            else
                            {
                                ThirdCommand = string.Format("SLM:0:2::1 :");
                            }
                            //第四部分 二维码的后18位条码（18位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                FourthCommand = string.Format("SLM:0:3::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(36, 18)));
                            }
                            else
                            {
                                FourthCommand = string.Format("SLM:0:2::1 :");
                            }

                            // 提示进度
                            bgwPrint.ReportProgress(40);

                            //准备发送到喷码机
                            if (SendQrCodeToPrinter(FirstCommand) && SendQrCodeToPrinter(SecondCommand) && SendQrCodeToPrinter(ThirdCommand) && SendQrCodeToPrinter(FourthCommand))
                            {
                                // 提示进度
                                bgwPrint.ReportProgress(50);
                            }
                            else
                            {
                                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "发送54位条码至喷码设备失败，请喷码完再进行扫码！");
                                JObject obj = JObject.Parse(result);
                                e.Result = obj;
                                return;
                            }
                            #endregion
                        }
                        else if (QrCode.Trim().Length == 47)
                        {
                            #region 如果条码的长度为47位，发送给喷码机的指令需要分割为四部分
                            //第一部分 二维码的整体条码（47位）
                            if (!QrCode.Equals(""))
                            {
                                FirstCommand = string.Format("S2M:2:2:::::1:0:1{0}:", QrCode.Trim());
                            }
                            else
                            {
                                FirstCommand = string.Format("S2M:2:2:::::1:0:1 :");
                            }
                            //第二部分 二维码的前16位条码（16位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                SecondCommand = string.Format("SLM:0:1::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(0, 16)));
                            }
                            else
                            {
                                SecondCommand = string.Format("SLM:0:2::1 :");
                            }
                            //第三部分 二维码的后16位条码（16位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                ThirdCommand = string.Format("SLM:0:2::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(16, 16)));
                            }
                            else
                            {
                                ThirdCommand = string.Format("SLM:0:2::1 :");
                            }
                            //第四部分 二维码的后15位条码（15位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                FourthCommand = string.Format("SLM:0:3::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(32, 15)));
                            }
                            else
                            {
                                FourthCommand = string.Format("SLM:0:2::1 :");
                            }

                            // 提示进度
                            bgwPrint.ReportProgress(40);

                            //准备发送到喷码机
                            if (SendQrCodeToPrinter(FirstCommand) && SendQrCodeToPrinter(SecondCommand) && SendQrCodeToPrinter(ThirdCommand) && SendQrCodeToPrinter(FourthCommand))
                            {
                                // 提示进度
                                bgwPrint.ReportProgress(50);
                            }
                            else
                            {
                                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "发送47位条码至喷码设备失败，请喷码完再进行扫码！");
                                JObject obj = JObject.Parse(result);
                                e.Result = obj;
                                return;
                            }
                            #endregion
                        }
                        else if (QrCode.Trim().Length == 40)
                        {
                            #region 如果条码的长度为40位，发送给喷码机的指令需要分割为三部分
                            //第一部分 二维码的整体条码（40位）
                            if (!txtQrCode.Text.Equals(""))
                            {
                                FirstCommand = string.Format("S2M:2:2:::::1:0:1{0}:", QrCode.Trim());
                            }
                            else
                            {
                                FirstCommand = string.Format("S2M:2:2:::::1:0:1 :");
                            }
                            //第二部分 二维码的前20位条码（20位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                SecondCommand = string.Format("SLM:0:1::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(0, 20)));
                            }
                            else
                            {
                                SecondCommand = string.Format("SLM:0:2::1 :");
                            }
                            //第三部分 二维码的后20位条码（20位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                ThirdCommand = string.Format("SLM:0:2::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(20, 20)));
                            }
                            else
                            {
                                ThirdCommand = string.Format("SLM:0:2::1 :");
                            }

                            // 提示进度
                            bgwPrint.ReportProgress(40);

                            //准备发送到喷码机
                            if (SendQrCodeToPrinter(FirstCommand) && SendQrCodeToPrinter(SecondCommand) && SendQrCodeToPrinter(ThirdCommand))
                            {
                                // 提示进度
                                bgwPrint.ReportProgress(50);
                            }
                            else
                            {
                                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "发送40位条码至喷码设备失败，请喷码完再进行扫码！");
                                JObject obj = JObject.Parse(result);
                                e.Result = obj;
                                return;
                            }
                            #endregion
                        }
                        else if (QrCode.Trim().Length == 21)
                        {
                            #region 如果条码的长度为21位，发送给喷码机的指令需要分割为三部分
                            //第一部分 二维码的整体条码（21位）
                            if (!txtQrCode.Text.Equals(""))
                            {
                                FirstCommand = string.Format("S2M:2:2:::::1:0:1{0}:", QrCode.Trim());
                            }
                            else
                            {
                                FirstCommand = string.Format("S2M:2:2:::::1:0:1 :");
                            }
                            //第二部分 二维码的前11位条码（11位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                SecondCommand = string.Format("SLM:0:1::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(0, 11)));
                            }
                            else
                            {
                                SecondCommand = string.Format("SLM:0:2::1 :");
                            }
                            //第三部分 二维码的后10位条码（10位）特殊字符（‘:’,'?','\'等需要转码）
                            if (!QrCode.Equals(""))
                            {
                                ThirdCommand = string.Format("SLM:0:2::4{0}:", UnicodeTransion.StringToUnicode(QrCode.Trim().Substring(11, 10)));
                            }
                            else
                            {
                                ThirdCommand = string.Format("SLM:0:2::1 :");
                            }

                            // 提示进度
                            bgwPrint.ReportProgress(40);

                            //准备发送到喷码机
                            if (SendQrCodeToPrinter(FirstCommand) && SendQrCodeToPrinter(SecondCommand) && SendQrCodeToPrinter(ThirdCommand))
                            {
                                // 提示进度
                                bgwPrint.ReportProgress(50);
                            }
                            else
                            {
                                string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "发送21位条码至喷码设备失败，请喷码完再进行扫码！");
                                JObject obj = JObject.Parse(result);
                                e.Result = obj;
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "二维码长度异常,当前长度" + QrCode.Trim().Length + "（正常长度为21位、40位、47位、54位）！");
                            JObject obj = JObject.Parse(result);
                            e.Result = obj;
                            return;
                        }

                    }
                    catch (Exception exp)
                    {
                        YJ.Log.FileLog.log.Error(exp.Message.ToString());
                        string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "发送信息失败!\r\n" + exp.Message);
                        JObject obj = JObject.Parse(result);
                        e.Result = obj;
                        return;
                    }

                    // ***********确认喷码完成信号***********
                    bool istimeout = false;  // 读取超时
                    bool isend = false; //喷码完成
                    uint start = GetTickCount();  // 记录开始时间
                    while (!isend && !istimeout)
                    {
                        isend = CCS3000.RecvPrintedEndSignal(PortPrinter);

                        if (GetTickCount() - start > 30000)
                        {
                            istimeout = true;
                        }

                        Application.DoEvents();
                    }//while (!isend && !istimeout)

                    if (isend)
                    {
                        string result = string.Format("{{\"status\":\"1\",\"msg\":\"success\",\"result\":\"{0}\"}}", "喷码成功!");
                        JObject obj = JObject.Parse(result);
                        e.Result = obj;
                        return;
                    }
                    else
                    {
                        string result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "喷码超时，请确认！");
                        JObject obj = JObject.Parse(result);
                        e.Result = obj;
                        return;
                    }
                }

            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }
        /// <summary>
        /// 喷码后台处理进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPrint_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            switch (progress)
            {
                case 10:
                    txtScanCode.Enabled = false;
                    if (this.WaitingBox == null)
                    {
                        this.WaitingBox = new dlgWaiting("正在获取成品码...", false);
                    }
                    this.WaitingBox.SetMsg("正在获取成品码...");
                    this.WaitingBox.ShowDialog(this);
                    txtScanCode.SelectAll();
                    txtScanCode.Focus();
                    break;

                case 20:
                    this.WaitingBox.SetMsg("已获取成品码");
                    string[] code = e.UserState as string[];
                    txtQrCode.Text = code[0];
                    txtLCDCode.Text = code[1];
                    break;

                case 25:
                    this.WaitingBox.SetMsg("重新生成绑定成品码");
                    txtSendCode.Text = e.UserState.ToString();
                    break;

                case 30:
                    this.WaitingBox.SetMsg("发送数据至接收服务器...");
                    txtSendCode.Text = e.UserState.ToString();
                    break;

                case 40:
                    this.WaitingBox.SetMsg("发送数据至喷码机...");
                    break;

                case 50:
                    this.WaitingBox.SetMsg("数据成功发送至喷码机,\r\n正在喷码...");
                    break;

                case 100:
                    break;
            }
        }
        /// <summary>
        /// 喷码后台处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            JObject jobj = null;
            if (e.Result != null)
            {
                jobj = e.Result as JObject;
            }
            else
            {
                this.WaitingBox.PlaySound("Error");
                this.WaitingBox.SetMsg("参数错误，请重试！", 1000);
                
                txt_ErrNote.Text = "参数错误，请重试！";
                txt_ErrNote.AppendText("\r\n");

                txtQrCode.Text = "";
                txtLCDCode.Text = "";
                txtSendCode.Text = "";
                txtScanCode.Enabled = true;
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }

            if (jobj["msg"].ToString() == "error")
            {
                this.WaitingBox.PlaySound("Error");
                this.WaitingBox.SetMsg(jobj["result"].ToString(), 1000);
                
                txt_ErrNote.Text = jobj["result"].ToString();
                txt_ErrNote.AppendText("\r\n");

                txtQrCode.Text = "";
                txtLCDCode.Text = "";
                txtSendCode.Text = "";
                txtScanCode.Enabled = true;
                txtScanCode.SelectAll();
                txtScanCode.Focus();
            }
            else
            {
                this.WaitingBox.PlaySound("OK");
                this.WaitingBox.SetMsg(jobj["result"].ToString(), 100);
                txt_ErrNote.Text = "喷码成功！";
                txt_ErrNote.AppendText("\r\n");
                txt_ErrNote.AppendText("成品码：" + txtQrCode.Text);
                txtScanCode.Enabled = true;
                txtScanCode.Text = "";
                txtQrCode.Text = "";
                txtLCDCode.Text = "";
                txtSendCode.Text = "";
                txtScanCode.Focus();
            }
        }

        /// <summary>
        /// 获取成品码
        /// </summary>
        private void GetQR()
        {
            try
            {
                string ClientResult = HuaWeiBarCode.Get_MaterialSNCode(this.scanCode, this.isRepeat);
                JObject obj = JObject.Parse(ClientResult);
                this.jsonStr = obj;
            }
            catch (Exception ex)
            {
                YJ.Log.FileLog.log.Error(ex.Message, ex);
                this.jsonStr = null;
            }
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


        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string ControlUrl = e.Argument as string;
                string ClientResult = HuaWeiBarCode.Get_MaterialSNCode(txtScanCode.Text.Trim().Replace(" ", ""), chkRepeat.Checked);
                JObject obj = JObject.Parse(ClientResult);
                e.Result = obj;
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
                e.Result = null;
            }
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                pmb.promptMessage = "获取成品码失败！";
                return;
            }
            pmb.DialogResult = System.Windows.Forms.DialogResult.OK;
            pmb.Close();
            try
            {
                this.jsonStr = e.Result as JObject;
                if (jsonStr["msg"].ToString() == "success")
                {
                    txtQrCode.Text = jsonStr["result"].ToString();
                    string LcdCode = jsonStr["lcdcode"].ToString();
                    txtLCDCode.Text = LcdCode;

                    if (CheckResult.PROCESS_CODE == "032")
                    {
                        txtSendCode.Text = "CPPM{CPM;" + YJ.Common.Utils.GetAppConfig("mouldcode", "appSettings") + ";" + CheckResult.LOCAL_IP + ";" + txtQrCode.Text.Trim() + "||" + txtScanCode.Text.Trim() + ";1|1|0;0;;0;" + DateTime.Now.ToString("yyyyMMddHHmmss") + "}";
                    }
                    else if (CheckResult.PROCESS_CODE == "041")
                    {
                        txtSendCode.Text = "RGJT{RGZD;" + YJ.Common.Utils.GetAppConfig("mouldcode", "appSettings") + ";" + CheckResult.LOCAL_IP + ";" + LcdCode + ";" + txtScanCode.Text.Trim() + "|" + txtScanCode.Text.Trim() + ";#}";
                    }
                    string SendResultString = ReciveClientSendMsgToHbaseServer(txtSendCode.Text);
                    //string SendResultString = "True";
                    if (SendResultString == "True")
                    {
                        try
                        {
                            string FirstCommand = "", SecondCommand = "", ThirdCommand = "", FourthCommand = "";
                            if (txtQrCode.Text.Trim().Length == 54)
                            {
                                #region 如果条码的长度为54位，发送给喷码机的指令需要分割为四部分
                                //第一部分 二维码的整体条码（54位）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    FirstCommand = string.Format("S2M:2:2:::::1:0:1{0}:", txtQrCode.Text);
                                }
                                else
                                {
                                    FirstCommand = string.Format("S2M:2:2:::::1:0:1 :");
                                }
                                //第二部分 二维码的前18位条码（18位）特殊字符（‘:’,'?','\'等需要转码）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    SecondCommand = string.Format("SLM:0:1::4{0}:", UnicodeTransion.StringToUnicode(txtQrCode.Text.Trim().Substring(0, 18)));
                                }
                                else
                                {
                                    SecondCommand = string.Format("SLM:0:2::1 :");
                                }
                                //第三部分 二维码的中间18位条码（18位）特殊字符（‘:’,'?','\'等需要转码）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    ThirdCommand = string.Format("SLM:0:2::4{0}:", UnicodeTransion.StringToUnicode(txtQrCode.Text.Trim().Substring(18, 18)));
                                }
                                else
                                {
                                    ThirdCommand = string.Format("SLM:0:2::1 :");
                                }
                                //第四部分 二维码的后18位条码（18位）特殊字符（‘:’,'?','\'等需要转码）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    FourthCommand = string.Format("SLM:0:3::4{0}:", UnicodeTransion.StringToUnicode(txtQrCode.Text.Trim().Substring(36, 18)));
                                }
                                else
                                {
                                    FourthCommand = string.Format("SLM:0:2::1 :");
                                }
                                //准备发送到喷码机
                                if (SendQrCodeToPrinter(FirstCommand) && SendQrCodeToPrinter(SecondCommand) && SendQrCodeToPrinter(ThirdCommand) && SendQrCodeToPrinter(FourthCommand))
                                {
                                    txt_ErrNote.Text = "发送至喷码机成功！";
                                    txt_ErrNote.AppendText("\r\n");
                                    txtScanCode.Enabled = false;
                                    int autoclose = Convert.ToInt32(YJ.Common.Utils.GetAppConfig("autoClose", "appSettings"));
                                    pmb = new PromptMessageBox("发送至喷码机成功！", 0, autoclose);
                                    pmb.ShowDialog(this);
                                    txtScanCode.Enabled = true;
                                    txtScanCode.Text = "";
                                    txtQrCode.Text = "";
                                    txtLCDCode.Text = "";
                                    txtSendCode.Text = "";
                                    txtScanCode.Focus();
                                }
                                else
                                {
                                    txt_ErrNote.Text = "发送54位条码至喷码设备失败，请喷码完再进行扫码！";
                                    txt_ErrNote.AppendText("\r\n");
                                    txtQrCode.Text = "";
                                    txtLCDCode.Text = "";
                                    txtSendCode.Text = "";
                                }
                                #endregion
                            }
                            else if (txtQrCode.Text.Trim().Length == 47)
                            {
                                #region 如果条码的长度为47位，发送给喷码机的指令需要分割为三部分
                                //第一部分 二维码的整体条码（47位）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    FirstCommand = string.Format("S2M:2:2:::::1:0:1{0}:", txtQrCode.Text);
                                }
                                else
                                {
                                    FirstCommand = string.Format("S2M:2:2:::::1:0:1 :");
                                }
                                //第二部分 二维码的前16位条码（16位）特殊字符（‘:’,'?','\'等需要转码）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    SecondCommand = string.Format("SLM:0:1::4{0}:", UnicodeTransion.StringToUnicode(txtQrCode.Text.Trim().Substring(0, 16)));
                                }
                                else
                                {
                                    SecondCommand = string.Format("SLM:0:2::1 :");
                                }
                                //第三部分 二维码的后16位条码（16位）特殊字符（‘:’,'?','\'等需要转码）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    ThirdCommand = string.Format("SLM:0:2::4{0}:", UnicodeTransion.StringToUnicode(txtQrCode.Text.Trim().Substring(16, 16)));
                                }
                                else
                                {
                                    ThirdCommand = string.Format("SLM:0:2::1 :");
                                }
                                //第四部分 二维码的后15位条码（15位）特殊字符（‘:’,'?','\'等需要转码）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    FourthCommand = string.Format("SLM:0:3::4{0}:", UnicodeTransion.StringToUnicode(txtQrCode.Text.Trim().Substring(32, 15)));
                                }
                                else
                                {
                                    FourthCommand = string.Format("SLM:0:3::1 :");
                                }
                                //准备发送到喷码机
                                if (SendQrCodeToPrinter(FirstCommand) && SendQrCodeToPrinter(SecondCommand) && SendQrCodeToPrinter(ThirdCommand) && SendQrCodeToPrinter(FourthCommand))
                                {
                                    txt_ErrNote.Text = "发送至喷码机成功！";
                                    txt_ErrNote.AppendText("\r\n");
                                    txtScanCode.Enabled = false;
                                    int autoclose = Convert.ToInt32(YJ.Common.Utils.GetAppConfig("autoClose", "appSettings"));
                                    pmb = new PromptMessageBox("发送至喷码机成功！", 0, autoclose);
                                    pmb.ShowDialog(this);
                                    txtScanCode.Enabled = true;
                                    txtScanCode.Text = "";
                                    txtQrCode.Text = "";
                                    txtLCDCode.Text = "";
                                    txtSendCode.Text = "";
                                    txtScanCode.Focus();
                                }
                                else
                                {
                                    txt_ErrNote.Text = "发送47位条码至喷码设备失败，请喷码完再进行扫码！";
                                    txt_ErrNote.AppendText("\r\n");
                                    txtQrCode.Text = "";
                                    txtLCDCode.Text = "";
                                    txtSendCode.Text = "";
                                }
                                #endregion
                            }
                            else if (txtQrCode.Text.Trim().Length == 40)
                            {
                                #region 如果条码的长度为40位，发送给喷码机的指令需要分割为三部分
                                //第一部分 二维码的整体条码（40位）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    FirstCommand = string.Format("S2M:2:2:::::1:0:1{0}:", txtQrCode.Text);
                                }
                                else
                                {
                                    FirstCommand = string.Format("S2M:2:2:::::1:0:1 :");
                                }
                                //第二部分 二维码的前20位条码（20位）特殊字符（‘:’,'?','\'等需要转码）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    SecondCommand = string.Format("SLM:0:1::4{0}:", UnicodeTransion.StringToUnicode(txtQrCode.Text.Trim().Substring(0, 20)));
                                }
                                else
                                {
                                    SecondCommand = string.Format("SLM:0:2::1 :");
                                }
                                //第三部分 二维码的后20位条码（20位）特殊字符（‘:’,'?','\'等需要转码）
                                if (!txtQrCode.Text.Equals(""))
                                {
                                    ThirdCommand = string.Format("SLM:0:2::4{0}:", UnicodeTransion.StringToUnicode(txtQrCode.Text.Trim().Substring(20, 20)));
                                }
                                else
                                {
                                    ThirdCommand = string.Format("SLM:0:2::1 :");
                                }
                                //准备发送到喷码机
                                if (SendQrCodeToPrinter(FirstCommand) && SendQrCodeToPrinter(SecondCommand) && SendQrCodeToPrinter(ThirdCommand))
                                {
                                    txt_ErrNote.Text = "发送至喷码机成功！";
                                    txt_ErrNote.AppendText("\r\n");
                                    txtScanCode.Enabled = false;
                                    int autoclose = Convert.ToInt32(YJ.Common.Utils.GetAppConfig("autoClose", "appSettings"));
                                    pmb = new PromptMessageBox("发送至喷码机成功！", 0, autoclose);
                                    pmb.ShowDialog(this);
                                    txtScanCode.Enabled = true;
                                    txtScanCode.Text = "";
                                    txtQrCode.Text = "";
                                    txtLCDCode.Text = "";
                                    txtSendCode.Text = "";
                                    txtScanCode.Focus();
                                }
                                else
                                {
                                    txt_ErrNote.Text = "发送40位条码至喷码设备失败，请喷码完再进行扫码！";
                                    txt_ErrNote.AppendText("\r\n");
                                    txtQrCode.Text = "";
                                    txtLCDCode.Text = "";
                                    txtSendCode.Text = "";
                                }
                                #endregion
                            }
                            else
                            {
                                txt_ErrNote.Text = "二维码长度异常,当前长度" + txtQrCode.Text.Trim().Length + "（正常长度为40位、47位、54位）！";
                                txt_ErrNote.AppendText("\r\n");
                                txtScanCode.SelectAll();
                                txtQrCode.Text = "";
                                txtLCDCode.Text = "";
                                txtSendCode.Text = "";
                            }

                        }
                        catch (Exception exp)
                        {
                            YJ.Log.FileLog.log.Error(exp.Message.ToString());
                            MessageBox.Show("发送信息失败!\r\n" + exp.Message);
                        }
                    }
                    else
                    {
                        txt_ErrNote.Text = SendResultString;
                        txt_ErrNote.AppendText("\r\n");
                        txtQrCode.Text = "";
                        txtLCDCode.Text = "";
                        txtSendCode.Text = "";
                    }
                }
                else
                {
                    txt_ErrNote.Text = jsonStr["result"].ToString();
                    txt_ErrNote.AppendText("\r\n");
                    txtScanCode.SelectAll();
                    txtQrCode.Text = "";
                    txtLCDCode.Text = "";
                    txtSendCode.Text = "";
                }
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }
        #endregion


        private void frmPrinter_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                btnDisconnect.PerformClick();
                System.Environment.Exit(0);
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkRepeat.Checked)
                {
                    CheckPassWord dlg = new CheckPassWord();
                    dlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    dlg.ShowDialog();
                    if (CheckResult.CHECK_PASSWORD_RESULT == "True") //显示返回的值
                    {
                        chkRepeat.Checked = false;
                    }
                    else if (CheckResult.CHECK_PASSWORD_RESULT == "False")
                    {
                        MessageBox.Show("密码不正确或权限不足");
                    }
                }
                else
                {
                    CheckPassWord dlg = new CheckPassWord();
                    dlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    dlg.ShowDialog();
                    if (CheckResult.CHECK_PASSWORD_RESULT == "True") //显示返回的值
                    {
                        chkRepeat.Checked = true;
                    }
                    else if (CheckResult.CHECK_PASSWORD_RESULT == "False")
                    {
                        MessageBox.Show("密码不正确或权限不足");
                    }
                }
                txtScanCode.Text = "";
                txtScanCode.Focus();
            }
            catch (Exception exp)
            {
                YJ.Log.FileLog.log.Error(exp.Message.ToString());
            }
        }

        private void chkRepeat_CheckedChanged(object sender, EventArgs e)
        {
            txtScanCode.Text = "";
            txtScanCode.Focus();
        }
    }
}

