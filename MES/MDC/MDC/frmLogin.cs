using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using Utils;
using MDCAutoUpdate;

namespace MDC
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    public partial class frmLogin : Form
    {
        #region 私有字段
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 本机IP地址
        /// </summary>
        private string ip = "";
        #endregion 私有字段

        #region 构造函数
        /// <summary>
        /// frmLogin的构造函数
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
        }
        #endregion 构造函数

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {

            //txtAccount.Text = "SLC24718";
            //txtPassword.Text = "123456";
            // 当前程序版本
            lblVersion.Text = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ////// 打开串口
            ////OpenDeviceCom();
            //// 绑定串口数据接收事件
            //Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;

            this.BackgroundImage = Image.FromFile("img/Login.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            this.ip = BaseUI.GetLocalIP();
            this.lblIP.Text = string.Format("本机IP：{0}", this.ip);

            txtAccount.Focus();
            if (Program.isFirstRun)
            {
                lblMsg.Text = "正在检查更新...";
                lblMsg.ForeColor = Color.WhiteSmoke;
                bgwUpdate.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
            ////CloseDeviceCom();
        }

        #region 自动更新
        /// <summary>
        /// 自动更新后台处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
                string serverXmlFile = string.Empty;
                XmlFiles updaterXmlFiles = null;
                string updateUrl = "", tempUpdatePath = "";
                try
                {
                    //从本地读取更新配置文件信息
                    updaterXmlFiles = new XmlFiles(localXmlFile);
                }
                catch
                {
                    MessageBox.Show("配置文件出错!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Result = null;
                    return;
                }
                //获取服务器地址
                updateUrl = updaterXmlFiles.GetNodeValue("//Url");
//#if DEBUG
//                updateUrl = "http://192.168.41.2:6605/FormAutoUpdate/MDCAutoUpdate/";
//                //updateUrl = "http://192.168.41.2:6605/FormAutoUpdate/MDC";
//#endif
                MDCAutoUpdate.AppUpdater appUpdater = new MDCAutoUpdate.AppUpdater();
                appUpdater.UpdaterUrl = updateUrl + "/UpdateList.xml";

                //与服务器连接,下载更新配置文件
                try
                {
                    tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
                    if (System.IO.File.Exists(tempUpdatePath + "\\UpdateList.xml"))
                    {
                        System.IO.File.Delete(tempUpdatePath + "\\UpdateList.xml");
                    }
                    appUpdater.DownAutoUpdateFile(tempUpdatePath);
                }
                catch
                {
                    MessageBox.Show("与服务器连接失败,操作超时!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Result = null;
                    return;
                }

                serverXmlFile = tempUpdatePath + "\\UpdateList.xml";
                if (System.IO.File.Exists(serverXmlFile))
                {
                    Hashtable updateFileList = null;
                    appUpdater.CheckForUpdate(serverXmlFile, localXmlFile, out updateFileList);
                    if (updateFileList.Count > 0)
                    {
                        e.Result = true;
                        return;
                    }
                    else
                    {
                        e.Result = false;
                        return;
                    }
                }
                else
                {
                    e.Result = null;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("失败！" + exp.Message.ToString());
                e.Result = null;
            }
        }

        /// <summary>
        /// 自动更新检查完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                lblMsg.Text = "连接服务器超时，检查更新失败！";
                lblMsg.ForeColor = Color.Red;
                return;
            }

            bool newVersion = Convert.ToBoolean(e.Result);
            if (newVersion)
            {
                this.Hide();
                System.Diagnostics.Process.Start(Application.StartupPath + @"\MDCAutoUpdate.exe");
            }
            else
            {
                lblMsg.Text = "当前已是最新版本";
                lblMsg.ForeColor = Color.WhiteSmoke;
                // 将程序版本写入数据库
                BaseUI.DeviceVersionUpdate();
            }

        }

        #endregion 自动更新


        /// <summary>
        /// 账号输入框按下回车,分析处理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // 分析处理数据
                AddScanData(txtAccount.Text.Trim().Replace("\r", "").Replace("\n", ""));
                Login(txtAccount.Text, txtPassword.Text);
            }
        }
        /// <summary>
        /// 密码输入框按下回车键，分析处理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // 分析处理数据
                AddScanData(txtPassword.Text.Trim().Replace("\r", "").Replace("\n", ""));

                // 执行登录操作
                Login(txtAccount.Text, txtPassword.Text);
            }
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            btnClose.ForeColor = Color.Red;
        }
        private void btnClose_MouseUp(object sender, MouseEventArgs e)
        {
            btnClose.ForeColor = Color.White;
        }

        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login(txtAccount.Text, txtPassword.Text);
        }
        private void btnLogin_MouseDown(object sender, MouseEventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(67, 85, 143);
        }

        private void btnLogin_MouseUp(object sender, MouseEventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(89, 116, 200);
        }

        #region 鼠标按下时移动窗体
        //需添加using System.Runtime.InteropServices;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        //......
        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            // 鼠标左键单击
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture(); //释放鼠标捕捉
                //发送左键点击的消息至该窗体(标题栏)
                SendMessage(Handle, 0xA1, 0x02, 0);
            }
        }
        #endregion


        #region 方法
        ///// <summary>
        ///// 打开扫码枪串口
        ///// </summary>
        //private void OpenDeviceCom()
        //{
        //    try
        //    {
        //        //打开扫描设备
        //        if (!Program.ScanDevice.IsOpen)
        //        {
        //            Program.ScanDevice.Open();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        FailMessage failMsg = new FailMessage("打开扫描设备失败!\n" + exp.Message.ToString(), 1000);
        //        failMsg.ShowDialog();
        //        LogHelper.Error("打开扫描设备失败!" + exp.Message.ToString(),exp);
        //        //Log4netProvider.Logger.Error("打开扫描设备失败!" + exp);
        //    }
        //}
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
        //        if (!DataString.Contains("OK:") && !DataString.Contains("ERROR:"))
        //        {
        //            // 分析处理数据
        //            AddScanData(DataString);
        //        }

        //    }
        //    catch (Exception exp)
        //    {
        //        FailMessage failMsg = new FailMessage("扫描设备接收数据失败!\n" + exp.Message.ToString(), 1000);
        //        failMsg.ShowDialog(this);
        //        txtAccount.Text = "";
        //        txtPassword.Text = "";
        //        txtAccount.SelectAll();
        //        txtAccount.Focus();
        //        Program.ScanDevice.DiscardInBuffer();
        //        LogHelper.Error("扫描设备接收数据失败!" + exp.Message, exp);
        //        //Log4netProvider.Logger.Error("扫描设备接收数据失败!" + exp);
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
                if (!DataString.Contains("OK:") && !DataString.Contains("ERROR:"))
                {
                    // 分析处理数据
                    AddScanData(DataString);
                }
            }
            catch (Exception exp)
            {
                FailMessage failMsg = new FailMessage("扫描设备接收数据失败!\n" + exp.Message.ToString(), 1000);
                failMsg.ShowDialog(this);
                txtAccount.Text = "";
                txtPassword.Text = "";
                txtAccount.SelectAll();
                txtAccount.Focus();
                Program.ScanDevice.DiscardBuffer();
                LogHelper.Error("扫描设备接收数据失败!" + exp.Message, exp);
                //Log4netProvider.Logger.Error("扫描设备接收数据失败!" + exp);
            }
        }
        /// <summary>
        /// 分析处理扫描枪数据的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void AddScanDataCallback(string DataString);
        /// <summary>
        /// 分析处理扫描枪数据
        /// </summary>
        /// <param name="DataString">扫描枪接收字符串</param>
        private void AddScanData(string DataString)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    AddScanDataCallback d = new AddScanDataCallback(AddScanData);
                    this.BeginInvoke(d, new object[] { DataString });
                }
                else
                {
                    string[] ds = DataString.Split('|');
                    if (ds.Length > 0)
                    {
                        // 如果输入字符串是人员信息，则解析出账号密码并填充文本框
                        if (ds[0].Replace("\r", "").Replace("\n", "") == "Person")
                        {
                            string account = ds[1].Replace("\r", "").Replace("\n", "");
                            string password = YJ.Common.AES.Decode(ds[2].Replace("\r", "").Replace("\n", ""), "yj888128");
                            txtAccount.Text = account;
                            txtPassword.Text = password;
                            Login(account, password);
                        }
                        else
                        {
                            if (txtAccount.Focused)
                            {
                                txtAccount.Text = ds[0];
                            }
                            if (txtPassword.Focused)
                            {
                                txtPassword.Text = ds[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FailMessage failMsg = new FailMessage("扫描设备解析数据失败!\n" + ex.Message.ToString(), 1000);
                failMsg.ShowDialog(this);
                txtAccount.SelectAll();
                txtAccount.Focus();
                LogHelper.Error("扫描设备解析数据失败!" + ex.Message.ToString(), ex);
                //Log4netProvider.Logger.Error("扫描设备解析数据失败!" + ex);
                //Program.ScanDevice.DiscardInBuffer();
                Program.ScanDevice.DiscardBuffer();
            }
        }
        #endregion 方法

        #region 登录程序
        /// <summary>
        /// 登录程序
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        private void Login(string account, string password)
        {
            if (account.Trim() == "")
            {
                lblMsg.Text = "请输入账号";
                lblMsg.ForeColor = Color.Red;
                txtAccount.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if (password.Trim() == "")
            {
                lblMsg.Text = "请输入密码";
                lblMsg.ForeColor = Color.Red;
                txtPassword.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            //记录登录帐号
            lblMsg.Text = "正在验证账号密码...";
            lblMsg.ForeColor = Color.WhiteSmoke;
            bgwLogin.RunWorkerAsync(new string[] { account, password });
        }
        

        /// <summary>
        /// 登录后台处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] data;
            string account;
            string password;
            try
            {

                if (e.Argument != null)
                {
                    data = e.Argument as string[];
                    account = data[0];
                    password = data[1];
//#if DEBUG
//                    LoginState state = LoginState.登录成功;
//#else
                    LoginState state = BaseUI.Login(account, password);
//#endif
                    e.Result = state;
                }
            }
            catch (Exception)
            {
                e.Result = null;
            }
        }
        /// <summary>
        /// 登录后台处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoginState state;
            if (e.Result == null)
            {
                lblMsg.Text = "程序异常，请重启软件";
                lblMsg.ForeColor = Color.Red;
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                txtPassword.Focus();
                txtPassword.SelectAll();
                return;
            }
            state = (LoginState)e.Result;
            switch (state)
            {
                case LoginState.登录成功:
                    // 获取帐号登录IP地址
                    string loginIp = BaseUI.GetLoginIP(BaseUI.UI_BOOLOGNAME);
                    if(string.IsNullOrEmpty(loginIp) || loginIp == this.ip) //帐号未登录
                    {
                        //更新帐号登录IP地址
                        bool islogin = BaseUI.ChangeLoginState(BaseUI.UI_BOOLOGNAME, this.ip);
                        if(islogin)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            LogHelper.Info("用户【" + BaseUI.UI_BOOLOGNAME + "】登录成功");
                        }
                        else
                        {
                            lblMsg.Text = "帐号登录状态更新失败，请重试！";
                            lblMsg.ForeColor = Color.Red;
                            this.DialogResult = System.Windows.Forms.DialogResult.None;
                            btnLogin.Focus();
                        }
                    }
                    else//帐号已登录
                    {
                        lblMsg.Text = string.Format("帐号已在【{0}】登录！", loginIp);
                        lblMsg.ForeColor = Color.Red;
                        this.DialogResult = System.Windows.Forms.DialogResult.None;
                        txtPassword.Clear();
                        txtAccount.Focus();
                        txtAccount.SelectAll();
                    }
                    break;
                case LoginState.密码错误:
                    lblMsg.Text = state.ToString();
                    lblMsg.ForeColor = Color.Red;
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                    break;
                default:
                    lblMsg.Text = state.ToString();
                    lblMsg.ForeColor = Color.Red;
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    txtAccount.Focus();
                    txtAccount.SelectAll();
                    break;
            }
        }
        #endregion 登录程序

        #region 设置窗体的边框阴影效果
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect, // x-coordinate of upper-left corner
                int nTopRect, // y-coordinate of upper-left corner
                int nRightRect, // x-coordinate of lower-right corner
                int nBottomRect, // y-coordinate of lower-right corner
                int nWidthEllipse, // height of ellipse
                int nHeightEllipse // width of ellipse
             );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;

        }

        #endregion 设置窗体的边框阴影效果

        private void frmLogin_Shown(object sender, EventArgs e)
        {
#if DEBUG
            //txtAccount.Text = "wangm";
            //txtPassword.Text = "Skyworth.2020";

            //txtAccount.Text = "SLC24718";
            //txtPassword.Text = "123456";
            btnLogin.PerformClick();
#endif
        }
    }
}
