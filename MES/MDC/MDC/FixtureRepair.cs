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
    public partial class FixtureRepair : Form
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
        private string ipAddress { get; set; } = "172.20.21.221";
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
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 最大日志行数
        /// </summary>
        private int maxLogCount;
        /// <summary>
        /// 是否记录程序运行日志
        /// </summary>
        private bool isWriteLog;

        /// <summary>
        /// 最近50片扫描记录示意图块宽度
        /// </summary>
        private int lblWidth = 4;

        /// <summary>
        /// 最近若干片玻璃的绑定结果记录（字符串的每一位代表一片玻璃的绑定结果，0：OK，1：NG，2：混料）
        /// </summary>
        private string ScanNote = "";
        private string UserId = "";//用户编码

        private string[] WorkDArray = new string[] { "CELL", "CFOG", "DB", "ASSY", "OQC", "返修" };//工段

        /// <summary>
        /// 构造函数
        /// </summary>
        public FixtureRepair()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixtureRepair_Load(object sender, EventArgs e)
        {
            try
            {
                // 当前程序版本
                this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                this.Text = string.Format("治具维修  {0}", versionName);

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

        private void FormUiDock()
        {
            label10.Dock = DockStyle.Fill;
            comDeviceType.Dock = DockStyle.Fill;
            label12.Dock = DockStyle.Fill;
            txtSendRepairer_Name.Dock = DockStyle.Fill;
            lblTime.Dock = DockStyle.Fill;
            comLines.Dock = DockStyle.Fill;

            label4.Dock = DockStyle.Fill;
            comByB.Dock = DockStyle.Fill;
            label1.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;
            lblNG.Dock = DockStyle.Fill;

            txtVersion.Dock = DockStyle.Fill;
            comByB.Dock = DockStyle.Fill;
            label1.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;
            lblNG.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 程序初始化
        /// </summary>
        private void Initialize()
        {
            try
            {
                FormUiDock();
                // 最大日志行数
                maxLogCount = appConfig.GetConfigInt("MaxLogCount");
                // 是否记录程序运行日志
                isWriteLog = appConfig.GetConfigBool("WriteAppLog");
                // 登录人员账号
                // 当前站点IP
                this.ipAddress = BaseUI.GetLocalIP();
                txtSendRepairer_Name.Text = BaseUI.UI_BOOPNAME;//用户名
                UserId = BaseUI.UI_BOOPID;//用户ID
                BindingReworkProcess();//产线 过站名称 下拉绑定
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
        /// 初始化 产线 过站名称 下拉绑定
        /// </summary>
        private void BindingReworkProcess()
        {
            try
            {
                //产线绑定
                DataTable dt = BaseUI.GetPLineList();
                comLines.DisplayMember = "SPL_Name";
                comLines.ValueMember = "SPL_JobCode";
                comLines.DataSource = dt;
                if (comLines.Items.Count > 0)
                {
                    comLines.SelectedIndex = 0;
                }
                //白夜班绑定
                var BybList = BaseUI.GetByB();
                comByB.DisplayMember = "Name";
                comByB.ValueMember = "Name";
                comByB.DataSource = BybList;
                if (comByB.Items.Count > 0)
                {
                    comByB.SelectedIndex = 0;
                }
                //工段
                var WorkD = BaseUI.GetWorkD();
                comWorkD.DisplayMember = "Name";
                comWorkD.ValueMember = "Name";
                comWorkD.DataSource = WorkD;
                if (comWorkD.Items.Count > 0)
                {
                    comWorkD.SelectedIndex = 0;
                }
                //设备类型
                var DeviceLIst = BaseUI.GetDeviceType();
                comDeviceType.DisplayMember = "Name";
                comDeviceType.ValueMember = "Name";
                comDeviceType.DataSource = DeviceLIst;
                if (comDeviceType.Items.Count > 0)
                {
                    comDeviceType.SelectedIndex = 0;
                }
                //comByB.SelectedIndex = -1;
                //comLines.SelectedIndex = -1;
            }
            catch (Exception exp)
            {
                LogHelper.Error("初始化失败." + exp.Message, exp);
                ShowResult(NoteState.Error, "", "初始化失败." + exp.Message);
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

        private string FixtureStatus = "待维修";

        /// <summary>
        /// 分析处理扫描数据的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ScanDataHandlerCallback(string DataString);

        ScanDataHandlerCallback d;
        /// <summary>
        /// 分析处理扫描数据
        /// </summary>
        /// <param name="DataString">扫描枪接收字符串</param>
        private void ScanDataHandler(string DataString)
        {
            string ResCode = DataString.Trim();

            try
            {
                if (this.InvokeRequired)
                {
                    d = new ScanDataHandlerCallback(ScanDataHandler);
                    this.BeginInvoke(d, new object[] { ResCode });
                }
                else
                {
                    // 第2步：解析扫描数据
                    #region 第1步：解析扫描数据
                    //编码规则解析并填充文本框
                    if (string.IsNullOrEmpty(ResCode))
                    {
                        this.failMsg = new FailMessage("请扫码！", 2);
                        this.failMsg.ShowDialog(this);
                        return;
                    }
                    else
                    {
                        string[] ds = ResCode.Split('-');
                        if (ds == null || ds.Length == 0)
                        {
                            ShowResult(NoteState.NG, "", "编码不符合规则");
                            return;
                        }
                        else if (ds.Length == 3)
                        {
                            txtHostCode.Text = ResCode;//主机编码
                        }
                        else if (ds.Length == 4)
                        {
                            txtScanCode.Text = ResCode;//支架编码
                            txtModel.Text = ds[0];//型号
                            comWorkD.SelectedValue = ds[1];//工段
                            txtVersion.Text = ds[2];//版本
                            var SingleGlass = BaseUI.GetSingleFixtureRepair(txtScanCode.Text);
                            if (SingleGlass != null && SingleGlass.Rows.Count != 0)
                            {
                                comByB.SelectedValue = SingleGlass.Rows[0]["ByB"].ToString();//白夜班
                                FixtureStatus = SingleGlass.Rows[0]["FixtureStatus"].ToString();//支架维修状态
                                string Line = SingleGlass.Rows[0]["Line"].ToString();//长线
                                comLines.SelectedValue = SingleGlass.Rows[0]["Line"].ToString();//长线
                                //PostId,FaultId
                                string PostId = SingleGlass.Rows[0]["PostId"].ToString();
                                string FaultId = SingleGlass.Rows[0]["FaultId"].ToString();
                                comPost.SelectedValue = PostId;// BaseUI.GetFaultItemById(PostId);//岗位
                                comFaultName.SelectedValue = FaultId;// BaseUI.GetFaultItemById(FaultId);//故障
                                comDeviceType.SelectedValue = SingleGlass.Rows[0]["DeviceType"].ToString();//类型
                                txtHostCode.Text = SingleGlass.Rows[0]["HostCode"].ToString();//主机编码

                            }
                            else
                            {
                                CreanItem2();
                            }
                        }
                        else if (ds.Length == 5)
                        {
                            txtScanCode.Text = ResCode;//支架编码
                            txtModel.Text = ds[0] + "-" + ds[1];//型号
                            comWorkD.SelectedValue = ds[2];//工段
                            txtVersion.Text = ds[3];//版本
                            var SingleGlass = BaseUI.GetSingleFixtureRepair(txtScanCode.Text);
                            if (SingleGlass != null && SingleGlass.Rows.Count != 0)
                            {
                                txtHostCode.Text = SingleGlass.Rows[0]["HostCode"].ToString();//主机编码
                                comByB.SelectedValue = SingleGlass.Rows[0]["ByB"].ToString();//白夜班
                                FixtureStatus = SingleGlass.Rows[0]["FixtureStatus"].ToString();//支架维修状态
                                string Line = SingleGlass.Rows[0]["Line"].ToString();//长线
                                comLines.SelectedValue = SingleGlass.Rows[0]["Line"].ToString();//长线
                                                                                                   //PostId,FaultId
                                comPost.SelectedValue = SingleGlass.Rows[0]["PostId"].ToString();//岗位
                                comFaultName.SelectedValue = SingleGlass.Rows[0]["FaultId"].ToString();//故障
                            }
                            else
                            {
                                CreanItem2();
                            }
                        }
                    }
                    #endregion 第2步：解析扫描数据
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
                ShowResult(NoteState.Error, "", ex.Message);
            }
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
        /// <summary>
        /// 添加绑定记录
        /// </summary>
        /// <param name="State"></param>
        private void AddScanNote(string State)
        {
            if (this.ScanNote.Length >= 50)
            {
                this.ScanNote = this.ScanNote.Substring(1);
                //this.flpScanNote.Controls.RemoveAt(0);
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
        }
        public void CreanItem()
        {
            txtModel.Text = "";
            comWorkD.SelectedIndex = -1;
            txtHostCode.Text = "";
            txtVersion.Text = "";
            txtScanCode.Text = "";
            comPost.SelectedIndex = -1;
            comFaultName.SelectedIndex = -1;
            comByB.SelectedIndex = -1;
            comLines.SelectedIndex = -1;
        }
        public void CreanItem2()
        {
            comByB.SelectedIndex = -1;
            comLines.SelectedIndex = -1;
            comPost.SelectedIndex = -1;
            comFaultName.SelectedIndex = -1;
        }
        /// <summary>
        /// 岗位下拉获取故障名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comPost_SelectedValueChanged(object sender, EventArgs e)
        {
            string WorkDStr = comPost.SelectedValue.ToString();
            comFaultName.DataSource = BaseUI.GetFault_Dt(WorkDStr);
            comFaultName.DisplayMember = "ItemName";
            comFaultName.ValueMember = "Id";
            if (comFaultName.Items.Count > 0)
            {
                comFaultName.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            #region 保存维修数据
            try
            {
                string ScanCode = txtScanCode.Text;
                string HostCode = txtHostCode.Text;
                string[] HostCodeArr = HostCode.Split('-');
                if (string.IsNullOrEmpty(ScanCode))
                {
                    this.failMsg = new FailMessage("请扫治具编码！", 2);
                    this.failMsg.ShowDialog(this);
                    return;
                }
                if (string.IsNullOrEmpty(HostCode))
                {
                    this.failMsg = new FailMessage("请扫主机编码！", 2);
                    this.failMsg.ShowDialog(this);
                    return;
                }
                if (HostCodeArr.Length != 3)
                {
                    this.failMsg = new FailMessage("主机编码格式错误！", 2);
                    this.failMsg.ShowDialog(this);
                    return;
                }
                //查询维修支架信息
                var SingleGlass = BaseUI.GetSingleFixtureRepair(ScanCode);
                string line = comLines.SelectedValue.ToString();//产线
                string PostId = comPost.SelectedValue.ToString();//岗位Id
                string FaultId = comFaultName.SelectedValue.ToString();//故障Id
                string DeviceType = comDeviceType.SelectedValue.ToString();//设备类型
                string SendRepairer_Remark = comWorkD.Text.Trim() + "，" + comPost.Text.ToString() + "，" + comFaultName.Text.ToString();
                if (SingleGlass != null && SingleGlass.Rows.Count != 0)
                {
                    //更新
                    BaseUI.UpdateFixtureRepair(comByB.SelectedValue.ToString(), comWorkD.Text.Trim(), txtHostCode.Text, txtModel.Text, txtSendRepairer_Name.Text,
                       SendRepairer_Remark, line, FixtureStatus, txtScanCode.Text, PostId, FaultId, DeviceType);
                    ShowResult(NoteState.OK, txtScanCode.Text, "操作成功！");
                    CreanItem();//清空数据项
                }
                else
                {
                    string GuidId = System.Guid.NewGuid().ToString("N");
                    string DateTimeNow = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string InsertSql = string.Format("  insert into FixtureRepair(Id,ByB,WorkD,HostCode,FixtureCode,model,SendRepairer_Id,SendRepairer_Name,SendRepairer_Remark,FixtureStatus,CreateTime,Line,PostId,FaultId,DeviceType)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}') ", GuidId, comByB.SelectedValue.ToString(), comWorkD.Text.Trim(), txtHostCode.Text, txtScanCode.Text, txtModel.Text, UserId, txtSendRepairer_Name.Text,
                       SendRepairer_Remark, FixtureStatus, DateTimeNow, line, PostId, FaultId, DeviceType);
                    var Flag = BaseUI.InsertOldInStation(InsertSql);
                    if (Flag)
                    {
                        AddScanNote("0");
                        ShowResult(NoteState.OK, txtScanCode.Text, "操作成功！");
                        CreanItem();//清空数据项
                    }
                    else
                    {
                        ShowResult(NoteState.Fail, txtScanCode.Text, "数据保存失败.");
                    }
                }
            }
            catch (TimeoutException tex)
            {
                GetHBaseDataClass.Instance.Reconnect();
                ShowResult(NoteState.Error, txtScanCode.Text, "HBase数据库连接超时." + tex.Message.ToString());
                return;
            }
            catch (Exception exp)
            {
                ShowResult(NoteState.Error, txtScanCode.Text, "查询玻璃绑定信息失败." + exp.Message);
                return;
            }
            #endregion
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh1_Click(object sender, EventArgs e)
        {
            CreanItem();
            CreanItem2();
        }
        /// <summary>
        /// 工段选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comWorkD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string WorkDStr = comWorkD.SelectedValue.ToString().ToUpper();
            if (WorkDStr == "CFOGTP")
            {
                WorkDStr = "CFOG";
            }
            else if (WorkDStr == "ASSYTP" || WorkDStr == "ASSYOTP")
            {
                WorkDStr = "ASSY";
            }
            if (WorkDArray.Contains(WorkDStr))
            {
                comPost.DataSource = BaseUI.GetWorkPost(WorkDStr);
                comPost.DisplayMember = "ItemName";
                comPost.ValueMember = "Id";
                if (comPost.Items.Count > 0)
                {
                    comPost.SelectedIndex = 0;
                }
            }
        }
    }
}
