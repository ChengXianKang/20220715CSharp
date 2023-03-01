using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Reflection;
using MDCAutoUpdate;
using System.Collections;
using System.IO;
using IWshRuntimeLibrary;
using Utils;
using Newtonsoft.Json.Linq;

namespace MDC
{
    static class Program
    {
        #region 全局变量
        /// <summary>
        /// 登录窗口
        /// </summary>
        public static frmLogin LoginForm;
        /// <summary>
        /// 主窗口
        /// </summary>
        public static frmMain MainForm;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private static AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 扫码枪串口(串口名为空，自动识别串口号)
        /// </summary>
        public static AutoSerialPort ScanDevice = new AutoSerialPort();
        /// <summary>
        /// 标识程序是注销还是关闭
        /// </summary>
        public static bool isLogout = true;
        /// <summary>
        /// 程序第一次运行
        /// </summary>
        public static bool isFirstRun = true;
        #endregion 全局变量

        /// <summary>
        /// 该函数设置由不同线程产生的窗口的显示状态。
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //string path = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            //string ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + ".config";

            //System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new System.Configuration.ExeConfigurationFileMap() { ExeConfigFilename = path }, System.Configuration.ConfigurationUserLevel.None);
            //string strReturn = config.AppSettings.Settings["HBase_DataServer_IP"].Value;
            dynamic obj = new JObject();
            obj.name = "123";
            string str = obj.ToString();

            try
            {
                // 获取正在运行的实例
                Process instance = RunningInstance();
                if (instance == null)
                {
                    /**
                    * 当前用户是管理员的时候，直接启动应用程序
                    * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
                    */
                    //获得当前登录的Windows用户标示
                    System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
#if !DEBUG
                    //判断当前登录用户是否为管理员
                    if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                    {
#endif
                    // 如果是管理员，则直接运行
                    //注册捕捉异常事件

                    //处理未捕获的异常
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    ////处理UI线程异常
                    Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                    //处理非UI线程异常
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    //初始化程序配置
                    InitAppConfig();

                    //创建快捷方式到桌面
                    CreateShortcutOnDesktop();
                    //删除30天之前的日志
                    DeleteFile();
                    // 打开扫码枪串口
                    OpenDeviceCom();

#if DEBUG
                    //// 关闭扫码枪串口
                    //CloseDeviceCom();

                    ////// 打开主界面
                    ////Application.Run(new frmWarnConfig());
                    ////Application.Run(new frmHBase());

                    ////异常玻璃处理工具
                    //Application.Run(new frmLcdReInput());
                    //// 关闭程序
                    //Application.Exit();
                    //System.Environment.Exit(0);
#endif

                    while (isLogout)//如果是注销用户，则返回登录界面，否则关闭程序
                    {

                        // 打开登录窗体
                        LoginForm = new frmLogin();

                        DialogResult rst = LoginForm.ShowDialog();
                        isFirstRun = false;
                        if (rst == DialogResult.OK)
                        {
                            // 打开主界面
                            MainForm = new frmMain();
                            Application.Run(MainForm);

                            // 删除帐号登录IP地址
                            BaseUI.ChangeLoginState(BaseUI.UI_BOOLOGNAME, "");
                        }
                        else
                        {
                            isLogout = false;
                        }
                    }
                    // 关闭扫码枪串口
                    CloseDeviceCom();

                    Utils.HBaseClass.GetDataFromHBase.Instance.CloseConnect();

                    // 关闭程序
                    Application.Exit();
                    System.Environment.Exit(0);
#if !DEBUG
                    }
                    else
                    {
                        // 创建启动对象
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            UseShellExecute = true,
                            WorkingDirectory = Environment.CurrentDirectory,
                            FileName = Application.ExecutablePath,
                            // 设置启动动作,确保以管理员身份运行
                            Verb = "runas"
                        };

                        System.Diagnostics.Process.Start(startInfo);
                    }
                }
                else
                {
                    // 显示已运行的程序。
                    HandleRunningInstance(instance);
                    Application.Exit();    // 退出程序 
                    System.Environment.Exit(0);
                    return;
#endif
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                //退出
                Application.Exit();
                System.Environment.Exit(0);

            }
        }

        /// <summary>
        /// 获取正在运行的实例，没有运行的实例返回null;
        /// </summary>
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 显示已运行的程序。
        /// </summary>
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        }

        /// <summary>
        /// 处理UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.Exception;
                //记录异常信息操作
                LogHelper.Error(ex.Message, ex);
            }
            catch (Exception exc)
            {
                LogHelper.Error(exc.Message, exc);
            }
        }

        /// <summary>
        /// 处理非UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                //记录异常信息操作
                LogHelper.Error(ex.Message, ex);
            }
            catch (Exception exc)
            {
                LogHelper.Error(exc.Message, exc);
            }
        }

        /// <summary>
        /// 打开扫码枪串口
        /// </summary>
        public static void OpenDeviceCom()
        {
            try
            {
                //打开扫描设备
                ScanDevice.OpenPort();
            }
            catch (Exception exp)
            {
                FailMessage failMsg = new FailMessage("打开扫描设备串口失败!", 1000);
                failMsg.ShowDialog();
                LogHelper.Error("打开扫描设备失败!", exp);
            }
        }
        /// <summary>
        /// 关闭扫码枪串口
        /// </summary>
        public static void CloseDeviceCom()
        {
            try
            {
                ScanDevice.DiscardBuffer();
                ScanDevice.ClosePort();
            }
            catch (Exception exp)
            {
                FailMessage failMsg = new FailMessage("关闭扫描设备失败!", 1000);
                failMsg.ShowDialog();
                LogHelper.Error("关闭扫描设备失败!" + exp.Message, exp);
                //Log4netProvider.Logger.Error("关闭扫描设备失败!" + exp);
            }
        }

        //删除30天之前的Log文件
        private static void DeleteFile()
        {
            try
            {
                //文件夹路径
                string strFolderPath = Application.StartupPath + @"\Log";
                DirectoryInfo dyInfo = new DirectoryInfo(strFolderPath);
                //获取文件夹下所有的文件
                foreach (DirectoryInfo subDy in dyInfo.GetDirectories())
                {
                    foreach (FileInfo feInfo in subDy.GetFiles())
                    {
                        //判断文件日期是否小于今天，是则删除
                        try
                        {
                            if (feInfo.CreationTime < DateTime.Today.AddDays(-30))
                            {
                                feInfo.Delete();
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 创建快捷方式到桌面
        /// </summary>
        private static void CreateShortcutOnDesktop()
        {
            //添加引用 (com->Windows Script Host Object Model)，using IWshRuntimeLibrary;
            String shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "生产数据采集.lnk");
            if (!System.IO.File.Exists(shortcutPath))
            {
                // 获取当前应用程序目录地址
                String exePath = Process.GetCurrentProcess().MainModule.FileName;
                IWshShell shell = new WshShell();
                //// 确定是否已经创建的快捷键被改名了
                //foreach (var item in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "*.lnk"))
                //{
                //    WshShortcut tempShortcut = (WshShortcut)shell.CreateShortcut(item);
                //    if (tempShortcut.TargetPath == exePath)
                //    {
                //        return;
                //    }
                //}
                WshShortcut shortcut = (WshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = exePath;
                shortcut.Arguments = "";// 参数  
                //shortcut.Description = "Manufacturing Data Collection";
                shortcut.Description = "生产数据采集";
                shortcut.WorkingDirectory = Environment.CurrentDirectory;//程序所在文件夹，在快捷方式图标点击右键可以看到此属性  
                shortcut.IconLocation = exePath;//图标，该图标是应用程序的资源文件  
                //shortcut.Hotkey = "CTRL+SHIFT+W";//热键，发现没作用，大概需要注册一下  
                shortcut.WindowStyle = 1;
                shortcut.Save();
            }
        }

        /// <summary>
        /// 初始化程序配置文件
        /// </summary>
        private static void InitAppConfig()
        {
            //// 程序配置列表
            //Dictionary<string, string> configs = new Dictionary<string, string>
            //{
            //    //<!--****************************************通用配置********************************************-->
            //    { "DbLog", ",SqlLog,WorkLog,ErrorLog,LoginLog,HttpErrorLog," },
            //    //<!--操作完成时,不同的状态是否提示音乐，Success Fail Warning 三种状态可选，不同状态间用逗号","分隔-->
            //    { "PlaySuccess", "Fail,Warning"},
            //    //<!--设备编码-->
            //    { "mouldcode", "EC888"},
            //    ////<!--数据上传服务器IP地址-->
            //    //{ "HBase_Interface_IP", ""},
            //    ////<!--{ "HBase_Interface_IP", "192.168.23.159"},-->
            //    ////<!--HBase数据库IP地址-->
            //    //{ "HBase_DataServer_IP", ""},
            //    ////<!--{ "HBase_Interface_IP", "192.168.23.159"},-->
            //    //<!--玻璃信息查询超时时间-->
            //    { "SearchTimeout", "5000"},
            //    //<!--最大日志行数（超过将自动清空）-->
            //    { "MaxLogCount", "200"},
            //    //<!--是否记录程序运行日志(True/False)-->
            //    { "WriteAPPLog", "True"},

            //    //<!--************************************自动过站程序配置****************************************-->
            //    //<!--编码绑定超时时间（毫秒）-->
            //    { "BindingTimeout", "5000"},
            //    //<!--玻璃检测信号过滤时间(毫秒，与上一片玻璃检测信号的时间间隔少于此设定值，将被丢弃）-->
            //    { "LCDFilter", "1300"},
            //    //<!--不良报警阈值（单位:片，近50片玻璃中的不良数大于此设定值将播放报警音）-->
            //    { "WarningValue", "10"},
            //    //<!--HBase数据查询重试次数-->
            //    { "RetryCount", "3" },

            //    ////<!--************************************不良复判程序配置****************************************-->
            //    ////<!--复测AOI不良图片路径-->
            //    //{ "AOI_NG_Path", ""},
            //    ////<!--是否展示复测AOI不良图片-->
            //    //{ "AOI_NG_Enabled", "True"},

            //    ////<!--************************************自动包装程序配置****************************************-->
            //    ////<!--包装串口名称-->
            //    //{ "PackagePort", "COM2"},

            //    ////<!--************************************产线投料程序配置****************************************-->
            //    ////<!--切换工单接口地址-->
            //    //{ "OrderSwitchUrl", "http://192.168.41.2:6605/Service/ServiceHandler.ashx"},

            //    //<!--************************************物流看板程序配置****************************************-->
            //    //<!--数据刷新周期（单位：毫秒）-->
            //    { "RefreshTime", "60000"},
            //    //<!--图表轮播间隔时间（单位：毫秒）-->
            //    { "PlayInterval", "30000"},

            //    //<!--************************************华为上传程序配置****************************************-->
            //    //<!--配置参数组-->
            //    { "ConfigGroup", "official"},
            //    //<!--华为客户料号-->
            //    { "HWCusNumber", "23020712,23020730,23020737,23020909"}
            //};
            //appConfig.SetConfig(configs);

            //// 获取华为客户料号
            //if (!appConfig.ContainsKey("KGKSerialPort"))
            //{
            //    appConfig.SetConfig("KGKSerialPort", "COM1");
            //}
            //// 默认关闭读转喷玻璃检测
            //appConfig.SetConfig("GlassDetectorEnabled", "False");


        }

    }
}
