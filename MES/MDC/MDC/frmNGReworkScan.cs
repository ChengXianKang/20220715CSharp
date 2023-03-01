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
    public partial class frmNGReworkScan : Form
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
        /// 构造函数
        /// </summary>
        public frmNGReworkScan()
        {
            InitializeComponent();

            txtProcessIP.Dock = DockStyle.Fill;
            txtCurrentOpCode.Dock = DockStyle.Fill;
            txtScanCode.Dock = DockStyle.Fill;
            txtGlassCode.Dock = DockStyle.Fill;
            txtProcess.Dock = DockStyle.Fill;
            txtOp.Dock = DockStyle.Fill;
            txtTime.Dock = DockStyle.Fill;
            txtDeviceIP.Dock = DockStyle.Fill;
            txtProductOrder.Dock = DockStyle.Fill;
            txtFinishesModel.Dock = DockStyle.Fill;
            txtLineName.Dock = DockStyle.Fill;
            txtExceptionContent.Dock = DockStyle.Fill;
            txtExceptionState.Dock = DockStyle.Fill;
            cmbProcess.Dock = DockStyle.Fill;

            txtProcessIP.Clear();
            txtCurrentOpCode.Clear();
            txtScanCode.Clear();
            txtGlassCode.Clear();
            txtProcess.Clear();
            txtOp.Clear();
            txtTime.Clear();
            txtDeviceIP.Clear();
            txtProductOrder.Clear();
            txtFinishesModel.Clear();
            txtFinishesModel.Tag = null;
            txtLineName.Clear();
            txtExceptionContent.Clear();
            txtExceptionState.Clear();
            cmbProcess.Items.Clear();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNGReworkScan_Load(object sender, EventArgs e)
        {
            try
            {
                // 当前程序版本
                this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                this.Text = string.Format("返修过站   {0}   （技术支持：深圳市鼎立特科技有限公司）", versionName);

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
#if DEBUG
                this.ipAddress = "172.20.21.36";
#endif
                txtProcessIP.Text = this.ipAddress;

                // 初始化工序
                this.dtProcessRoute = GetProcessRoute();
                if(dtProcessRoute == null || dtProcessRoute.Rows.Count ==0)
                {
                    ShowResult(NoteState.Error, "", "获取返修工序路由失败!" );
                }
                BindingReworkProcess();

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
                                  FROM [Store_GongXuLian_Sub]
                                  JOIN [Store_GongXuLian_Main] ON [SGXLS_SGXLTid] = [SGXL_Tid]
                                  JOIN [Store_GongXu_Main] ON [SGXLS_SGXTid] = [SGX_Tid]
                                  WHERE [SGXL_Name] = '返修工序路由'
                                  ORDER BY [SGXLS_No]";
            DataTable dt = conn.ExecuteDataTable(sql, "Base");
            return dt;
        }

        /// <summary>
        /// 初始化工序
        /// </summary>
        private void BindingReworkProcess()
        {
            try
            {
                string sql = string.Format(@"select rcode, note,SGX_Name from VW_Base_Op_Right_IPC 
                                                              LEFT JOIN Store_GongXu_Main ON note = SGX_JobCode
                                                              where rcode like '021220%' and not isnull(note,'')='' 
                                                              and pid = {0} 
                                                              ORDER BY rcode", BaseUI.UI_BOOPID);
                DataTable dt = conn.ExecuteDataTable(sql, "Base");
                cmbProcess.DisplayMember = "SGX_Name";
                cmbProcess.ValueMember = "note";
                cmbProcess.DataSource = dt;
                if (cmbProcess.Items.Count > 0)
                {
                    cmbProcess.SelectedIndex = 0;
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
            if (cmbProcess.SelectedIndex == -1) return;
            string processCode = cmbProcess.SelectedValue.ToString();
            if(this.dtProcessRoute != null && this.dtProcessRoute.Rows.Count >0)
            {
                DataView dv = new DataView(this.dtProcessRoute);
                dv.RowFilter = string.Format("SGX_JobCode = '{0}'", processCode);
                if(dv.Count ==0)
                {
                    ShowResult(NoteState.Error, "", "返修工序路由中不存在当前返修岗位"+cmbProcess.Text);
                    cmbProcess.Tag = null;
                }
                else
                {
                    cmbProcess.Tag = dv[0]["SGXLS_No"];
                }
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
                    txtScanCode.Enabled = false;

                    // 第1步：站点基本信息检测
#region 第1步：站点基本信息检测
                    if (string.IsNullOrWhiteSpace(cmbProcess.Text))
                    {
                        ShowResult(NoteState.Fail, "", "当前账号未配置返修过站权限");
                        return;
                    }
                    if(cmbProcess.Tag == null)
                    {
                        ShowResult(NoteState.Fail, "", "返修工序路由中不存在当前返修岗位" + cmbProcess.Text);
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
                        txtGlassCode.Clear();
                        txtProcess.Clear();
                        txtOp.Clear();
                        txtTime.Clear();
                        txtProductOrder.Clear();
                        txtFinishesModel.Clear();
                        txtFinishesModel.Tag = null;
                        txtLineName.Clear();
                        txtExceptionContent.Clear();
                        txtExceptionState.Clear();
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
                        ShowResult(NoteState.NG, "", "当前玻璃未申报不良，无法过站");
                        return;
                    }

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

                    // 显示玻璃不良信息
                    ShowGlassInfo(this.glassInfo);

                    // 检查是否已包装
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

#region 不良信息
                    if (lastEx.ExceptionState == "待复判")
                    {
                        ShowResult(NoteState.NG, "", "当前玻璃尚未复判，无法过站");
                        return;
                    }
                    //else if (lastEx.ExceptionState == "复判良品" || lastEx.ExceptionState == "重工良品")
                    else if (lastEx.ExceptionState == "复判良品")//2020-04-10 重工良品可再次更新为重工报废
                    {
                        ShowResult(NoteState.NG, "", "当前玻璃为" + lastEx.ExceptionState + "，尚未申报不良");
                        return;
                    }
#endregion 不良信息

#endregion 查询玻璃是否已提交不良信息

                    sql = string.Format(@"SELECT [PRM_Tid]
		                                                        ,[PRS_ProcessCode]
		                                                        ,[PRS_ProcessName]
                                                          FROM [TPL_ProductRepair_Main]
                                                          LEFT JOIN [TPL_ProductRepair_Sub]
                                                          ON [PRM_Tid] = [PRS_PRMTid]
                                                          WHERE [PRM_ExceptionKey] = '{0}'", glassInfo.LastExceptionKey);
                    DataTable dtRework = conn.ExecuteDataTable(sql, "Base");

                    int PRMTid = 0;
                    int processNum = Convert.ToInt32(cmbProcess.Tag);
                    if (processNum == 1)
                    {
                        //返修进站，需创建主表数据
                        if (dtRework == null || dtRework.Rows.Count == 0)
                        {
                            sql = string.Format(@"INSERT INTO [TPL_ProductRepair_Main](
	                                                               [PRM_ProductionOrder]
                                                                  ,[PRM_FinishesModel]
                                                                  ,[PRM_ProductLineCode]
                                                                  ,[PRM_GlassCode]
                                                                  ,[PRM_ExceptionKey]
                                                                  ,[PRM_ExceptionState]
                                                                  ,[PRM_JudgeContent]
                                                                  ,[PRM_ProcessCode]
                                                                  ,[PRM_ProcessName]
                                                                  ,[PRM_IsOutput]
                                                                  ,[PRM_AddPid]
                                                                  ,[PRM_AddDate]
                                                              ) VALUES(
                                                                '{0}'
                                                                ,'{1}'
                                                                ,'{2}'
                                                                ,'{3}'
                                                                ,'{4}'
                                                                ,'{5}'
                                                                ,'{6}'
                                                                ,'{7}'
                                                                ,'{8}'
                                                                ,0
                                                                ,{9}
                                                                ,GETDATE() );  SELECT @@identity",
                                                                lastEx.ProductionOrder
                                                                , lastEx.FinishesModel
                                                                , lastEx.ProductionLineCode
                                                                , lastEx.GlassCode
                                                                , lastEx.ExceptionKey
                                                                , lastEx.ExceptionState
                                                                , lastEx.JudgeContent
                                                                , lastEx.ProcessCode
                                                                , lastEx.processName
                                                                , BaseUI.UI_BOOPID
                                                                );
                            object obj = conn.ExecuteScalar(sql, "Base");
                            if (obj != null)
                            {
                                PRMTid = Convert.ToInt32(obj);
                            }
                            else
                            {
                                ShowResult(NoteState.NG, txtScanCode.Text, "过站信息保存失败，请重试！");
                                return;
                            }
                        }//if (dtRework == null || dtRework.Rows.Count ==0)
                        //else
                        //{
                        //    ShowResult(NoteState.NG, txtScanCode.Text, "当前工序已过站，请勿重复扫描！");
                        //    return;
                        //}
                    }//if(processNum == 1)
                    else  //不是第一道工序，检测漏工序，获取主表ID
                    {
                        if (dtRework == null || dtRework.Rows.Count == 0)
                        {
                            ShowResult(NoteState.NG, txtScanCode.Text, string.Format("检测到工序 {0} 未扫描！", this.dtProcessRoute.Rows[0]["SGXLS_SGXName"].ToString()));
                            return;
                        }
                        else
                        {
                            PRMTid = Convert.ToInt32(dtRework.Rows[0]["PRM_Tid"]);
                        }
                    }

                    //判断是否重复过站
                    DataView dvRework = new DataView(dtRework);
                    dvRework.RowFilter = string.Format("PRS_ProcessCode = '{0}'", cmbProcess.SelectedValue);
                    if(dvRework.Count > 0)
                    {
                        ShowResult(NoteState.NG, txtScanCode.Text, "当前工序已过站，请勿重复扫描！");
                        return;
                    }

                    //写入子表
                    sql = string.Format(@"INSERT INTO [TPL_ProductRepair_Sub](
	                                                       [PRS_PRMTid]
                                                          ,[PRS_ProcessCode]
                                                          ,[PRS_ProcessName]
                                                          ,[PRS_ScanCode]
                                                          ,[PRS_AddPid]
                                                          ,[PRS_AddDate]
                                                      ) VALUES ({0}, '{1}', '{2}', '{3}', {4}, GETDATE())",
                                                      PRMTid, cmbProcess.SelectedValue.ToString(), cmbProcess.Text, txtScanCode.Text, BaseUI.UI_BOOPID);
                    int n = conn.ExecuteAction(sql, "Base");
                    if(n != 1)
                    {
                        ShowResult(NoteState.NG, txtScanCode.Text, "过站信息保存失败，请重试！");
                        return;
                    }

                    //最后一道工序，回更主表
                    if(processNum == this.dtProcessRoute.Rows.Count)
                    {
                        sql = string.Format(@"UPDATE [TPL_ProductRepair_Main] 
                                                            SET [PRM_IsOutput] = 1, [PRM_OutputDate] = GETDATE() 
                                                            WHERE [PRM_Tid] = {0}", PRMTid);
                        n = conn.ExecuteAction(sql, "Base");
                        if (n != 1)
                        {
                            ShowResult(NoteState.NG, txtScanCode.Text, "过站信息保存失败，请重试！");
                            return;
                        }
                    }

                    ShowResult(NoteState.OK, txtScanCode.Text, "操作成功！");

                    RefreshProcessLog();
                }//if (this.InvokeRequired)
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, "", ex.Message);
            }
            finally
            {
                txtScanCode.Clear();
                txtScanCode.Enabled = true;
                txtScanCode.Focus();
                txtScanCode.SelectAll();
            }
        }

        /// <summary>
        /// 显示玻璃不良信息
        /// </summary>
        /// <param name="glassInfo"></param>
        private void ShowGlassInfo(GlassInfo glassInfo)
        {
            if (glassInfo == null) return;
            // 玻璃码
            txtGlassCode.Text = glassInfo.ProductInfo.LCDCode;
            // 工单编码
            txtProductOrder.Text = glassInfo.ProductInfo.ProductionOrder;
            //// 产线编码
            //txtLineName.Text = glassInfo.ProductInfo.ProductionLineCode;
            // 成品型号
            txtFinishesModel.Text = glassInfo.ProductInfo.FinishesModel;
            txtFinishesModel.Tag = glassInfo.ProductInfo.FinishesCode;

            if (!string.IsNullOrEmpty(glassInfo.LastExceptionKey))
            {
                // 填充数据
                ExceptionInfo exInfo = glassInfo.Exception[glassInfo.LastExceptionKey];

                //复判产线
                txtLineName.Text = exInfo.ProductionLineName;
                txtLineName.Tag = exInfo.ProductionLineCode;
                // 提报站点工序名
                txtProcess.Text = exInfo.processName;
                txtProcess.Tag = exInfo.ProcessCode;

                // 申报/复判类型
                txtExceptionState.Text = exInfo.ExceptionState;
                // 判断是否已复判
                if (exInfo.ExceptionState == "待复判")
                {
                    lblOP.Text = "申报人员:";
                    lblTime.Text = "申报时间:";
                    lblNG.Text = "申报不良:";
                    //lblNGType.Text = "申报状态:";
                    lblDeviceIP.Text = "申 报 I P:";
                    txtOp.Text = exInfo.ScanNumber;
                    txtTime.Text = exInfo.ScanTime;
                    // 异常描述
                    txtExceptionContent.Text = exInfo.ExceptionContent;
                    txtDeviceIP.Text = exInfo.DeviceIp;
                }
                else if (exInfo.ExceptionState.Contains("复判"))
                {
                    lblOP.Text = "复判人员:";
                    lblTime.Text = "复判时间:";
                    lblNG.Text = "复判不良:";
                    //lblNGType.Text = "复判状态:";
                    lblDeviceIP.Text = "复 判 I P:";
                    txtOp.Text = exInfo.JudgeNumber;
                    txtTime.Text = exInfo.JudgeTime;
                    // 异常描述
                    txtExceptionContent.Text = exInfo.JudgeContent;
                    txtDeviceIP.Text = exInfo.JudgeIp;
                }
                else
                {
                    lblOP.Text = "解绑人员:";
                    lblTime.Text = "解绑时间:";
                    lblNG.Text = "重工不良:";
                    //lblNGType.Text = "重工状态:";
                    lblDeviceIP.Text = "解 绑 I P:";
                    txtOp.Text = exInfo.ReworkNumber;
                    txtTime.Text = exInfo.ReworkTime;
                    // 异常描述
                    txtExceptionContent.Text = exInfo.ReworkContent;
                    txtDeviceIP.Text = exInfo.ReworkIp;
                }
            }

            RefreshProcessLog();
        }

        private void RefreshProcessLog()
        {
            // 获取工序过站信息
            Dictionary<string, string> dicProcessLog = GetProcessLogData(glassInfo.LastExceptionKey);
            if (dicProcessLog.Count > 0)
            {
                int i = 1;
                foreach (string value in dicProcessLog.Values)
                {
                    ListViewItem item = lvwProcess.Items.Add((i++).ToString());
                    item.SubItems.Add(value);
                }
            }

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
                                                        FROM [TPL_ProductRepair_Main]
                                                        JOIN [TPL_ProductRepair_Sub]
                                                        ON [PRM_Tid] = [PRS_PRMTid]
                                                        WHERE [PRM_ExceptionKey] = '{0}'
                                                        ORDER BY [PRS_AddDate]", exceptionKey);
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

    }
}
