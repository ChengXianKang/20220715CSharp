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
    public partial class frmNGReview : Form
    {
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
        /// <summary>
        /// 工控机本地IP
        /// </summary>
        private string ipAddress;
        /// <summary>
        /// 当前工单的所有工序列表DataTable
        /// </summary>
        private DataTable dtProcessTable;
        /// <summary>
        /// 工序列表DataView
        /// </summary>
        private DataView dvProcess;
        ///// <summary>
        ///// 扫描码查询的已过工序信息表
        ///// </summary>
        //private DataTable processData;
        /// <summary>
        /// 玻璃信息
        /// </summary>
        private GlassInfo glassInfo;
        /// <summary>
        /// 不良玻璃信息表(玻璃码，GlassInfo对象)
        /// </summary>
        private Dictionary<string, GlassInfo> NGGlassList = new Dictionary<string, GlassInfo>();
        ///// <summary>
        ///// 不良玻璃信息表(玻璃码，TPL_ProcessFail_Main对象)
        ///// </summary>
        //private Dictionary<string, TPL_ProcessFail_Main> ProcessFailList = new Dictionary<string, TPL_ProcessFail_Main>();
        /// <summary>
        /// 不良描述
        /// </summary>
        public string NGDesc = "";
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;        //失败提示框
        /// <summary>
        /// 成功提示框
        /// </summary>
        private SuccessMessage successMsg;  //成功提示框

        private WarnMessage warnMsg;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 玻璃信息查询超时时间
        /// </summary>
        private int searchTimeout;
        /// <summary>
        /// 所有不良项数据表
        /// </summary>
        private DataTable dtBNC;
        /// <summary>
        /// 当前成品型号
        /// </summary>
        private string currentModel;
        /// <summary>
        /// 复测AOI不良图片列表
        /// </summary>
        private Dictionary<string, string> dicNGImageList;
        /// <summary>
        /// 玻璃码和NG图片对应列表
        /// </summary>
        private Dictionary<string, string> dicLCD_Path;
        /// <summary>
        /// NG图片路径
        /// </summary>
        private System.IO.DirectoryInfo NGPath;
        /// <summary>
        /// 未找到图片
        /// </summary>
        private Image imgNotFound;
        /// <summary>
        /// 正在加载图片
        /// </summary>
        private Image imgLoading;
        /// <summary>
        /// 是否展示不良图片
        /// </summary>
        private bool AOI_NG_Enabled = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmNGReview()
        {
            InitializeComponent();

            cmbSPLName.Dock = DockStyle.Fill;
            txtProcessIP.Dock = DockStyle.Fill;
            txtCurrentOpCode.Dock = DockStyle.Fill;
            btnRework.Dock = DockStyle.Fill;
            btnScrap.Dock = DockStyle.Fill;
            btnErrJudge.Dock = DockStyle.Fill;
            comProcess.Dock = DockStyle.Fill;
            txtDescribe.Dock = DockStyle.Fill;
            txtBNCCode.Dock = DockStyle.Fill;
            btnSearch.Dock = DockStyle.Fill;
            txtScanCode.Dock = DockStyle.Fill;
            txtLCD.Dock = DockStyle.Fill;
            txtProcess.Dock = DockStyle.Fill;
            txtOp.Dock = DockStyle.Fill;
            txtTime.Dock = DockStyle.Fill;
            txtProductOrder.Dock = DockStyle.Fill;
            txtFinishesModel.Dock = DockStyle.Fill;
            txtLineCode.Dock = DockStyle.Fill;
            txtNG.Dock = DockStyle.Fill;
            txtNGType.Dock = DockStyle.Fill;
            btnHome.Dock = DockStyle.Fill;
            btnForward.Dock = DockStyle.Fill;
            btnEnd.Dock = DockStyle.Fill;
            btnBack.Dock = DockStyle.Fill;
            btnDelete.Dock = DockStyle.Fill;
            btnCancel.Dock = DockStyle.Fill;
            btnOK.Dock = DockStyle.Fill;

            //txtCurrentLineName.Clear();
            txtProcessIP.Clear();
            txtCurrentOpCode.Clear();
            txtDescribe.Clear();
            txtDescribe.Tag = "";
            txtBNCCode.Clear();
            txtScanCode.Clear();
            txtLCD.Clear();
            txtProcess.Clear();
            txtOp.Clear();
            txtTime.Clear();
            txtProductOrder.Clear();
            txtFinishesModel.Clear();
            txtFinishesModel.Tag = null;
            txtLineCode.Clear();
            txtNG.Clear();
            txtNGType.Clear();
            txtIdx.Clear();
            comProcess.Items.Clear();
            cmbSPLName.Items.Clear();
            flpNG.Controls.Clear();

            lblType.Tag = null;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNGReview_Load(object sender, EventArgs e)
        {
            try
            {
                this.imgNotFound = picNG.BackgroundImage;
                this.imgLoading = picNG.InitialImage;
                picNG.BackgroundImage = null;
                picNG.InitialImage = null;
                comProcess.Enabled = false;

                // 当前程序版本
                this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
        private void frmNGReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnCancel.Enabled)
            {
                e.Cancel = true;
            }
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
            this.flpNG.Refresh();
            txtScanCode.SelectAll();
            txtScanCode.Focus();
        }


        private void frmNGReview_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            this.flpNG.Refresh();
            txtScanCode.SelectAll();
            txtScanCode.Focus();
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
                // 当前玻璃索引
                txtIdx.Text = "0";
                // 已添加玻璃数量
                lblCount.Text = "/ 0";

                // 登录人员账号
                txtCurrentOpCode.Text = BaseUI.UI_BOOLOGNAME;

                // 当前站点IP
                this.ipAddress = BaseUI.GetLocalIP();

#if DEBUG
                //this.ipAddress = "192.168.1.165";    //	机台补发	
#endif
                txtProcessIP.Text = this.ipAddress;

                // 初始化产线选择下拉框
                DataView dvLine = BaseUI.GetProcedureLine();
                cmbSPLName.DisplayMember = "SPL_Name";
                cmbSPLName.ValueMember = "SPL_JobCode";
                cmbSPLName.DataSource = dvLine;
                cmbSPLName.SelectedIndex = 0;

                ////初始化工序
                //BindProcessComboBox(BaseUI.UI_SPOMJobCode, BaseUI.UI_SPLJOBCODE);

                // 初始化不良项数据表
                this.dtBNC = BaseUI.GetBNCTable();

                // 设置程序标题
                this.Text = "不良复判 --【请选择产线】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";

                this.dicLCD_Path = new Dictionary<string, string>();
                //this.pgeImage.Parent = null;

                if (appConfig.ContainsKey("AOI_NG_Path"))
                {
                    string path = appConfig.GetConfigString("AOI_NG_Path");
                    if (!string.IsNullOrEmpty(path))
                    {
                        this.NGPath = new System.IO.DirectoryInfo(appConfig.GetConfigString("AOI_NG_Path"));
                    }
                    else
                    {
                        this.NGPath = null;
                    }
                }
                else
                {
                    appConfig.SetConfig("AOI_NG_Path", "");
                    this.NGPath = null;
                }

                if (appConfig.ContainsKey("AOI_NG_Enabled"))
                {
                    this.AOI_NG_Enabled = appConfig.GetConfigBool("AOI_NG_Enabled");
                }
                else
                {
                    this.AOI_NG_Enabled = true;
                    appConfig.SetConfig("AOI_NG_Enabled", "True");
                }

                if (this.AOI_NG_Enabled)
                {
                    cmbNGImageEnabled.SelectedIndex = 0;
                }
                else
                {
                    cmbNGImageEnabled.SelectedIndex = 1;
                }

                // 设置复判权限
                SetUserRight();
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 根据站点类别和人员ID，给型号/工序下拉框赋权
        /// </summary>
        private void SetUserRight()
        {
            try
            {
                // 允许复判良品
                btnErrJudge.Visible = !BaseUI.UI_RIGHT.ContainsKey("02120401");
                // 允许复判报废
                btnScrap.Visible = !BaseUI.UI_RIGHT.ContainsKey("02120402");
                // 允许复判重工
                btnRework.Visible = !BaseUI.UI_RIGHT.ContainsKey("02120403");
            }
            catch (Exception ex)
            {
                LogHelper.Error("获取当前账号操作权限失败", ex);
                ShowResult(NoteState.Error, "", "获取当前账号操作权限失败!" + ex.Message);
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
                //object GXCode = comProcess.SelectedValue;
                //获取工序链
                this.dtProcessTable = BaseUI.GetProduceRouteData(ProductionOrder, LineCode);
                comProcess.DisplayMember = "SGX_Name";  //工序名称
                comProcess.ValueMember = "SGX_JobCode"; //工序编码
                comProcess.DataSource = this.dtProcessTable;

                //if (GXCode == null)
                //{
                //    // 设置程序标题
                //    this.Text = "不良复判 --【请选择本站工序】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
                //    // 补扫站点，待选择
                //    comProcess.SelectedIndex = -1;
                //}
                //else
                //{
                    //// 选择当前工序
                    //try
                    //{
                    //    comProcess.SelectedValue = GXCode;
                    //}
                    //catch (Exception)
                    //{
                    //    // 设置程序标题
                    //    this.Text = "不良复判 --【请选择本站工序】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
                    //    // 补扫站点，待选择
                    //    comProcess.SelectedIndex = -1;
                    //    return;
                    //}
                    //// 初始化窗体信息
                    //PageInit();
                //}
            }
            catch (Exception exp)
            {
                LogHelper.Error("初始化工序链失败." + exp.Message, exp);
                ShowResult(NoteState.Error, "", "初始化工序链失败." + exp.Message);
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
            if (lblType.Tag == null)
            {
                // 设置程序标题
                this.Text = "不良复判 --【请选择复判结果】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
            }
            else
            {
                // 初始化窗体信息
                PageInit();
            }
        }

        /// <summary>
        /// 根据本站工序初始化窗体信息
        /// </summary>
        private void PageInit()
        {
            if (comProcess.SelectedValue == null)
            {
                txtDescribe.Clear();
                txtDescribe.Tag = "";
                flpNG.Controls.Clear();
                txtBNCCode.Clear();
                this.Refresh();
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }
            try
            {
                // 筛选当前选择的工序信息
                this.dvProcess = new DataView(dtProcessTable);
                this.dvProcess.RowFilter = "SGX_JobCode='" + comProcess.SelectedValue + "'";
                if (this.dvProcess.Count > 0)
                {
                    BaseUI.UI_CurrentProcedureNo = this.dvProcess[0]["PR_NoSeq"].ToString();          //当前工序序号
                    BaseUI.UI_CurrentProcedureCode = this.dvProcess[0]["SGX_JobCode"].ToString();      //当前工序编码

                    // 设置程序标题
                    this.Text = "不良复判 -- " + this.dvProcess[0]["SGX_Name"].ToString() + "   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";

                    //加载不良项列表
                    InitBNC();
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
        /// 初始化不良项
        /// </summary>
        private void InitBNC()
        {
            txtDescribe.Clear();
            txtDescribe.Tag = "";
            flpNG.Controls.Clear();
            txtBNCCode.Clear();

            // 添加不良项列表
            //int width = 0;
            DataView dv = new DataView(this.dtBNC);
            dv.RowFilter = string.Format("TPD_ProcessCode = '{0}' AND BNC_Flag = '{1}'", BaseUI.UI_CurrentProcedureCode, this.lblType.Tag);
            dv.Sort = "TPD_BNCName ASC";

            List<CheckBox> chklst = new List<CheckBox>();
            foreach (DataRowView row in dv)
            {
                CheckBox chk = new CheckBox()
                {
                    Appearance = System.Windows.Forms.Appearance.Button,
                    AutoSize = false,
                    Width = 206,
                    Height = 45,
                    AutoEllipsis = true,
                    Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                    ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85))))),
                    Location = new System.Drawing.Point(3, 3),
                    Margin = new System.Windows.Forms.Padding(3, 8, 3, 8),
                    UseVisualStyleBackColor = true,
                    Name = row["TPD_BNCTid"].ToString(),    //不良项ID
                    Text = row["TPD_BNCName"].ToString(),   //不良项名称
                    Tag = row["TPD_BNCCode"],               //不良项编码
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

            this.flpNG.Controls.AddRange(chklst.ToArray());
            //foreach (CheckBox chk in this.flpNG.Controls)
            //{
            //    int height = chk.Height;
            //    chk.AutoSize = false;
            //    chk.Width = width;
            //    chk.Height = height;
            //}

            this.Refresh();
            txtScanCode.SelectAll();
            txtScanCode.Focus();
        }

        /// <summary>
        /// 重工按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRework_Click(object sender, EventArgs e)
        {
            Set_TFM_Type("重工");
        }
        /// <summary>
        /// 报废按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScrap_Click(object sender, EventArgs e)
        {
            Set_TFM_Type("报废");
        }
        /// <summary>
        /// 误判按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrJudge_Click(object sender, EventArgs e)
        {
            Set_TFM_Type("误判");
        }

        /// <summary>
        /// 设置异常类型
        /// </summary>
        /// <param name="Type"></param>
        private void Set_TFM_Type(string Type)
        {
            switch (Type)
            {
                case "重工":
                    lblType.Tag = "重工";
                    btnRework.BackColor = Color.Red;
                    btnScrap.BackColor = Color.Transparent;
                    btnErrJudge.BackColor = Color.Transparent;

                    //comProcess.Enabled = true;
                    pgeNG.Enabled = true;
                    break;
                case "报废":
                    lblType.Tag = "报废";
                    btnRework.BackColor = Color.Transparent;
                    btnScrap.BackColor = Color.Red;
                    btnErrJudge.BackColor = Color.Transparent;

                    //comProcess.Enabled = true;
                    pgeNG.Enabled = true;
                    break;
                case "误判":
                    lblType.Tag = "误判";
                    btnRework.BackColor = Color.Transparent;
                    btnScrap.BackColor = Color.Transparent;
                    btnErrJudge.BackColor = Color.Red;

                    //comProcess.SelectedIndex = -1;
                    txtDescribe.Clear();
                    txtDescribe.Tag = "";
                    flpNG.Controls.Clear();
                    txtBNCCode.Clear();
                    //comProcess.Enabled = false;
                    pgeNG.Enabled = false;

                    break;
                default:
                    lblType.Tag = null;
                    btnRework.BackColor = Color.Transparent;
                    btnScrap.BackColor = Color.Transparent;
                    btnErrJudge.BackColor = Color.Transparent;

                    //comProcess.Enabled = true;
                    pgeNG.Enabled = true;

                    comProcess.SelectedIndex = -1;
                    txtDescribe.Clear();
                    txtDescribe.Tag = "";
                    flpNG.Controls.Clear();
                    txtBNCCode.Clear();
                    // 清空玻璃列表
                    this.NGGlassList.Clear();
                    //this.ProcessFailList.Clear();
                    this.dicLCD_Path.Clear();
                    // 当前玻璃索引
                    txtIdx.Text = "0";
                    // 已添加玻璃数量
                    lblCount.Text = "/ 0";

                    txtLCD.Clear();
                    txtProcess.Clear();
                    txtOp.Clear();
                    txtTime.Clear();
                    txtProductOrder.Clear();
                    txtFinishesModel.Clear();
                    txtFinishesModel.Tag = null;
                    txtLineCode.Clear();
                    txtNG.Clear();
                    txtNGType.Clear();
                    txtScanCode.Clear();
                    txtScanCode.Focus();
                    picNG.Image = null;
                    // 设置程序标题
                    this.Text = "不良复判 --【请选择复判结果】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
                    break;
            }

            if (comProcess.SelectedIndex != -1)
            {
                PageInit();
            }
            else
            {
                // 设置程序标题
                this.Text = "不良复判 --【请选择本站工序】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
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
                    switch (state)
                    {
                        case NoteState.Success:
                            break;
                        case NoteState.OK:
                            // 弹框提示
                            this.successMsg = new SuccessMessage("操作成功", 2);
                            this.successMsg.ShowDialog(this);
                            Set_TFM_Type("");
                            //初始化工序
                            BindProcessComboBox(BaseUI.UI_SPOMJobCode, BaseUI.UI_SPLJOBCODE);
                            break;

                        case NoteState.NG:
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 1000);
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
            if (e.KeyChar == (char)Keys.Enter && txtScanCode.Text != "")
            {
                // 分析处理数据
                ScanDataHandler(txtScanCode.Text.Trim());
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
                    // 第1步：站点基本信息检测
                    #region 第1步：站点基本信息检测
                    if (string.IsNullOrWhiteSpace(cmbSPLName.Text))
                    {
                        ShowResult(NoteState.Fail, "", "请选择当前产线");
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

                    #region 去重
                    if (this.NGGlassList.ContainsKey(this.glassInfo.ProductInfo.LCDCode))
                    {
                        ShowResult(NoteState.NG, "", "列表中已存在此片玻璃，\r\n请勿重复扫描!");
                        //// 当前玻璃成品型号
                        //this.currentModel = this.glassInfo.ProductInfo.FinishesModel;
                        // 显示玻璃信息
                        ShowGlassInfo(this.glassInfo.ProductInfo.LCDCode);
                        return;
                    }
                    #endregion 去重

                    #region 查询玻璃不良信息

                    //// 旧的不良信息查询方式，从SQL Server中查询不良信息
                    //Utils.Model.TPL_ProcessFail_Main ProcessFailInfo = null;
                    //try
                    //{
                    //    ProcessFailInfo = BaseUI.GetProcessFail_Main(this.glassInfo.ProductInfo.LCDCode);
                    //}
                    //catch (Exception exp)
                    //{
                    //    LogHelper.Error("SQL查询玻璃不良申报信息失败.", exp);
                    //    //ShowResult(NoteState.Error, "", "查询玻璃不良申报信息失败！" + exp.Message);
                    //    //return;
                    //}

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
                        ShowResult(NoteState.NG, "", "该玻璃未申报不良！");
                        return;
                    }

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

                    #endregion 查询玻璃是否已提交不良信息

                    #region 第一片玻璃需要初始化工单和工序

                    if (this.NGGlassList.Count > 0)
                    {
                        //if (!string.IsNullOrEmpty(this.glassInfo.ProductInfo.ProductionOrder) && this.glassInfo.ProductInfo.ProductionOrder != txtProductOrder.Text)
                        //{
                        //    ShowResult(NoteState.NG, "", "该玻璃生产工单与当前工单不一致，不能混合提交");
                        //    return;
                        //}
                    }
                    else
                    {
                        // 当前玻璃成品型号
                        this.currentModel = this.glassInfo.ProductInfo.FinishesModel;

                        // 第一片玻璃，初始化工序列表
                        BindProcessComboBox(this.glassInfo.ProductInfo.ProductionOrder, this.cmbSPLName.SelectedValue.ToString());
                        //// 新添加的第一片玻璃，自动选择本站工序,默认重工
                        //if (this.NGGlassList.Count == 1)
                        //{
                            // 选择当前工序
                            try
                            {
                                comProcess.SelectedValue = lastEx.ProcessCode;
                            }
                            catch (Exception)
                            {
                                // 待选择
                                comProcess.SelectedIndex = -1;
                            }
                            Set_TFM_Type("重工");
                        //}

                    }

                    #endregion 第一片玻璃需要初始化工单和工序
                    
                    // 显示玻璃不良信息
                    ShowGlassInfo(this.glassInfo);

                    // 工序不一致
                    if (comProcess.SelectedValue != null && comProcess.SelectedValue.ToString() != lastEx.ProcessCode)
                    {
                        ShowResult(NoteState.NG, "", "该玻璃不良申报站点与当前工序不一致！");
                        return;
                    }
                    //// 工单不一致
                    //if (this.glassInfo.ProductInfo.ProductionOrder != lastEx.ProductionOrder)
                    //{
                    //    ShowResult(NoteState.NG, "", "该玻璃生产工单与不良申报信息中的工单不一致！");
                    //    return;
                    //}
                    // 型号不一致
                    if (this.glassInfo.ProductInfo.FinishesModel != this.currentModel)
                    {
                        ShowResult(NoteState.NG, "", "该玻璃型号与当前型号不一致！");
                        return;
                    }
                    // 产线不一致
                    if (lastEx.ProductionLineCode != cmbSPLName.SelectedValue.ToString())
                    {
                        List<string> lstMixLine = BaseUI.GetMixLine(lastEx.FinishesCode);
                        if (lstMixLine.Count == 0)
                        {
                            ShowResult(NoteState.NG, "", string.Format("该玻璃不良申报产线与当前产线不一致！", lastEx.ProductionLineName));
                            return;
                        }
                        else if (!lstMixLine.Contains(cmbSPLName.SelectedValue.ToString()))
                        {
                            ShowResult(NoteState.NG, "", string.Format("该玻璃不良申报产线为{0},禁止在当前产线复判！", lastEx.ProductionLineName));
                            return;
                        }
                    }

                    // 判断是否已复判
                    if (lastEx.ExceptionState == "重工报废")
                    {
                        ShowResult(NoteState.NG, "", "该玻璃已" + lastEx.ExceptionState);
                        return;
                    }
                    else if (lastEx.ExceptionState == "复判良品" || lastEx.ExceptionState == "重工良品")
                    {
                        ShowResult(NoteState.NG, "", "该玻璃未申报不良！");
                        return;
                    }
                    else if (lastEx.ExceptionState == "复判重工" || lastEx.ExceptionState == "复判报废")
                    {
                        this.warnMsg = new WarnMessage("已" + lastEx.ExceptionState + "！\r\n是否改判？", MessageBoxButtons.OKCancel, 5, "Cancel");
                        if (this.warnMsg.ShowDialog(this) == DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    // 添加至玻璃列表
                    this.NGGlassList.Add(this.glassInfo.ProductInfo.LCDCode, this.glassInfo);
                    //this.ProcessFailList.Add(this.glassInfo.ProductInfo.LCDCode, ProcessFailInfo);

                    // 当前玻璃索引
                    txtIdx.Text = this.NGGlassList.Count.ToString();
                    // 已添加玻璃数量
                    lblCount.Text = string.Format("/ {0}", this.NGGlassList.Count);
                    
                    if(this.glassInfo.GlassState.Repeat == "True")
                    {
                        WarnMessage warnMsg = new WarnMessage("返 修 品 ！", MessageBoxButtons.OK, 5, "OK");
                        warnMsg.ShowDialog(this);
                    }

                    txtScanCode.Clear();
                    txtScanCode.Focus();
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
        /// 显示玻璃不良信息
        /// </summary>
        /// <param name="glassInfo"></param>
        private void ShowGlassInfo(GlassInfo glassInfo)
        {
            if (glassInfo == null || string.IsNullOrEmpty(glassInfo.LastExceptionKey)) return;
            // 填充数据
            ExceptionInfo exInfo = glassInfo.Exception[glassInfo.LastExceptionKey];
            // 玻璃码
            txtLCD.Text = glassInfo.ProductInfo.LCDCode;
            // 提报站点工序名
            txtProcess.Text = exInfo.processName;
            // 工单编码
            txtProductOrder.Text = exInfo.ProductionOrder;
            // 产线编码
            txtLineCode.Text = exInfo.ProductionLineCode;
            // 成品型号
            txtFinishesModel.Text = exInfo.FinishesModel;
            txtFinishesModel.Tag = exInfo.FinishesCode;
            // 申报/复判类型
            txtNGType.Text = exInfo.ExceptionState;
            // 判断是否已复判
            if (exInfo.ExceptionState == "待复判")
            {
                lblOP.Text = "申报人员:";
                lblTime.Text = "申报时间:";
                lblNG.Text = "申报不良:";
                lblNGType.Text = "申报状态:";
                lblDeviceIP.Text = "申 报 I P:";
                txtOp.Text = exInfo.ScanNumber;
                txtTime.Text = exInfo.ScanTime;
                // 异常描述
                txtNG.Text = exInfo.ExceptionContent;
                txtDeviceIP.Text = exInfo.DeviceIp;
            }
            else
            {
                lblOP.Text = "复判人员:";
                lblTime.Text = "复判时间:";
                lblNG.Text = "复判不良:";
                lblNGType.Text = "复判状态:";
                lblDeviceIP.Text = "复 判 I P:";
                txtOp.Text = exInfo.JudgeNumber;
                txtTime.Text = exInfo.JudgeTime;
                // 异常描述
                txtNG.Text = exInfo.JudgeContent;
                txtDeviceIP.Text = exInfo.JudgeIp;
            }

            txtIdx.Text = "";

            //if (exInfo.ProcessCode == "031" && this.AOI_NG_Enabled)//复测AOI获取不良图片
            //{
            //    // 先去图片列表中查找匹配图片
            //    if (this.dicNGImageList != null && this.dicNGImageList.Count > 0 && this.dicNGImageList.ContainsKey(this.txtScanCode.Text))
            //    {
            //        string path = this.dicNGImageList[this.txtScanCode.Text];
            //        if (!this.dicLCD_Path.ContainsKey(glassInfo.ProductInfo.LCDCode))
            //        {
            //            this.dicLCD_Path.Add(glassInfo.ProductInfo.LCDCode, path);
            //        }
            //        this.picNG.Load(path);
            //        this.picNG.SizeMode = PictureBoxSizeMode.Zoom;
            //    }
            //    else//无匹配图片
            //    {
            //        // 显示未找到图片
            //        this.picNG.Image = this.imgNotFound;
            //        this.picNG.SizeMode = PictureBoxSizeMode.CenterImage;

            //        // 检查图片文件夹路径是否配置
            //        if (this.NGPath == null)
            //        {
            //            failMsg = new FailMessage("请先选择不良图片文件夹", 9999);
            //            failMsg.ShowDialog(this);
            //            System.Windows.Forms.FolderBrowserDialog folder = new FolderBrowserDialog();
            //            folder.Description = "选择不良图片文件夹";
            //            if (folder.ShowDialog(this) == DialogResult.OK)
            //            {
            //                this.NGPath = new System.IO.DirectoryInfo(folder.SelectedPath);
            //                appConfig.SetConfig("AOI_NG_Path", folder.SelectedPath);
            //            }
            //        }


            //        if (this.NGPath != null && this.NGPath.Exists)
            //        {
            //            // 显示正在加载
            //            this.picNG.Image = this.imgLoading;
            //            this.picNG.SizeMode = PictureBoxSizeMode.CenterImage;

            //            // 重新加载不良图片列表
            //            this.dicNGImageList = GetFilesHelper.GetImageDic(this.NGPath);

            //            // 再次匹配不良图片
            //            if (this.dicNGImageList != null && this.dicNGImageList.Count > 0 && this.dicNGImageList.ContainsKey(this.txtScanCode.Text))
            //            {
            //                string path = this.dicNGImageList[this.txtScanCode.Text];
            //                if (!this.dicLCD_Path.ContainsKey(glassInfo.ProductInfo.LCDCode))
            //                {
            //                    this.dicLCD_Path.Add(glassInfo.ProductInfo.LCDCode, path);
            //                }
            //                this.picNG.Load(path);
            //                this.picNG.SizeMode = PictureBoxSizeMode.Zoom;
            //            }
            //            else
            //            {
            //                this.picNG.Image = this.imgNotFound;
            //                this.picNG.SizeMode = PictureBoxSizeMode.CenterImage;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    this.picNG.Image = null;
            //}
        }

        /// <summary>
        /// 显示玻璃不良信息
        /// </summary>
        /// <param name="ProcessFailInfo"></param>
        private void ShowGlassInfo(string glassCode)
        {
            if (string.IsNullOrEmpty(glassCode) || !this.NGGlassList.ContainsKey(glassCode))
            {
                // 玻璃码
                txtLCD.Text = "";
                // 提报站点工序名
                txtProcess.Text = "";
                // 工单编码
                txtProductOrder.Text = "";
                txtFinishesModel.Tag = null;
                txtFinishesModel.Text = "";
                // 产线编码
                txtLineCode.Text = "";
                // 异常描述
                txtNG.Text = "";
                lblOP.Text = "申报人员:";
                lblTime.Text = "申报时间:";
                lblNG.Text = "申报不良:";
                lblNGType.Text = "申报状态:";
                lblDeviceIP.Text = "申 报 I P:";
                txtOp.Text = "";
                txtTime.Text = "";
                txtNGType.Text = "";
                txtDeviceIP.Text = "";
                txtIdx.Text = "0";
                this.picNG.Image = null;
                return;
            }

            GlassInfo glassInfo = this.NGGlassList[glassCode];
            // 填充数据
            ExceptionInfo exInfo = glassInfo.Exception[glassInfo.LastExceptionKey];
            // 玻璃码
            txtLCD.Text = glassInfo.ProductInfo.LCDCode;
            // 提报站点工序名
            txtProcess.Text = exInfo.processName;
            // 工单编码
            txtProductOrder.Text = exInfo.ProductionOrder;
            // 产线编码
            txtLineCode.Text = exInfo.ProductionLineCode;
            // 成品型号
            txtFinishesModel.Text = exInfo.FinishesModel;
            txtFinishesModel.Tag = exInfo.FinishesCode;
            // 申报/复判类型
            txtNGType.Text = exInfo.ExceptionState;
            // 判断是否已复判
            if (exInfo.ExceptionState == "待复判")
            {
                lblOP.Text = "申报人员:";
                lblTime.Text = "申报时间:";
                lblNG.Text = "申报不良:";
                lblNGType.Text = "申报状态:";
                lblDeviceIP.Text = "申 报 I P:";
                txtOp.Text = exInfo.ScanNumber;
                txtTime.Text = exInfo.ScanTime;
                // 异常描述
                txtNG.Text = exInfo.ExceptionContent;
                txtDeviceIP.Text = exInfo.DeviceIp;
            }
            else
            {
                lblOP.Text = "复判人员:";
                lblTime.Text = "复判时间:";
                lblNG.Text = "复判不良:";
                lblNGType.Text = "复判状态:";
                lblDeviceIP.Text = "复 判 I P:";
                txtOp.Text = exInfo.JudgeNumber;
                txtTime.Text = exInfo.JudgeTime;
                // 异常描述
                txtNG.Text = exInfo.JudgeContent;
                txtDeviceIP.Text = exInfo.JudgeIp;
            }

            for (int i = 0; i < this.NGGlassList.Count; i++)
            {
                if (this.NGGlassList.ElementAt(i).Key == glassCode)
                {
                    txtIdx.Text = (i + 1).ToString();
                    break;
                }
            }

            //if (exInfo.ProcessCode == "031" && this.AOI_NG_Enabled)//复测AOI获取不良图片
            //{
            //    if (this.dicLCD_Path.ContainsKey(glassCode))
            //    {
            //        string path = this.dicLCD_Path[glassCode];
            //        this.picNG.Load(path);
            //        this.picNG.SizeMode = PictureBoxSizeMode.Zoom;
            //    }
            //    else
            //    {
            //        this.picNG.Image = this.imgNotFound;
            //        this.picNG.SizeMode = PictureBoxSizeMode.CenterImage;
            //    }
            //}
            //else
            //{
            //    this.picNG.Image = null;
            //}
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
        /// 第一片玻璃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, EventArgs e)
        {
            if (this.NGGlassList.Count > 0)
            {
                string lcd = this.NGGlassList.ElementAt(0).Key;
                ShowGlassInfo(lcd);
            }
        }
        /// <summary>
        /// 最后一片玻璃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (this.NGGlassList.Count > 0)
            {
                string lcd = this.NGGlassList.ElementAt(this.NGGlassList.Count - 1).Key;
                ShowGlassInfo(lcd);
            }
        }
        /// <summary>
        /// 上一片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.NGGlassList.Count == 0) return;

            int idx = 0;
            if (txtIdx.Text != "")
            {
                idx = Convert.ToInt32(txtIdx.Text) - 1;
            }
            if (idx > 0)
            {
                idx -= 1;
            }
            string lcd = this.NGGlassList.ElementAt(idx).Key;
            ShowGlassInfo(lcd);
        }
        /// <summary>
        /// 下一片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnForward_Click(object sender, EventArgs e)
        {
            if (this.NGGlassList.Count == 0) return;
            int idx = 0;
            if (txtIdx.Text != "")
            {
                idx = Convert.ToInt32(txtIdx.Text) - 1;
            }
            if (idx < this.NGGlassList.Count - 1)
            {
                idx += 1;
            }
            string lcd = this.NGGlassList.ElementAt(idx).Key;
            ShowGlassInfo(lcd);
        }
        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIdx_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.NGGlassList.Count == 0) return;

            int idx = 0;
            if (txtIdx.Text != "")
            {
                idx = Convert.ToInt32(txtIdx.Text) - 1;
            }
            if (idx < 0)
            {
                idx = 0;
            }
            else if (idx > this.NGGlassList.Count - 1)
            {
                idx = this.NGGlassList.Count - 1;
            }
            string lcd = this.NGGlassList.ElementAt(idx).Key;
            ShowGlassInfo(lcd);
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtLCD.Text == "" ||this.NGGlassList.Count == 0 || !this.NGGlassList.ContainsKey(txtLCD.Text)) return;
            // 当前玻璃索引
            int idx = 0;
            for (int i = 0; i < this.NGGlassList.Count; i++)
            {
                if (this.NGGlassList.ElementAt(i).Key == txtLCD.Text)
                {
                    idx = i;
                    break;
                }
            }

            // 获取玻璃码
            string lcd = txtLCD.Text;
            // 移除
            this.NGGlassList.Remove(lcd);
            //this.ProcessFailList.Remove(lcd);
            // 已添加玻璃数量
            lblCount.Text = string.Format("/ {0}", this.NGGlassList.Count);

            if (idx == this.NGGlassList.Count)
            {
                idx -= 1;
            }

            if (idx < 0 || this.NGGlassList.Count == 0)
            {
                ShowGlassInfo("");
            }
            else
            {
                lcd = this.NGGlassList.ElementAt(idx).Key;
                ShowGlassInfo(lcd);
            }
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
            txtDescribe.Tag = "";
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
            if (this.NGGlassList.Count == 0)
            {
                ShowResult(NoteState.NG, "", "请先扫描编码，\r\n获取不良申报信息！");
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }
            if (lblType.Tag == null)
            {
                ShowResult(NoteState.NG, "", "请先选择复判结果");
                return;
            }
            if (lblType.Tag.ToString() != "误判" && string.IsNullOrWhiteSpace(comProcess.Text))
            {
                ShowResult(NoteState.NG, "", "请先选择本站工序");
                return;
            }
            if (lblType.Tag.ToString() != "误判" && (txtDescribe.Tag == null || txtDescribe.Tag.ToString() == "" ))
            {
                ShowResult(NoteState.NG, "", "请判定不良项！");
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }

            string type = "重工";
            if (lblType.Tag != null)
            {
                type = lblType.Tag.ToString();
            }

            //string idList = "";
            //foreach (Utils.Model.TPL_ProcessFail_Main item in this.ProcessFailList.Values)
            //{
            //    if (item != null && item.TFM_Tid > 0)
            //    {
            //        idList += string.Format("{0}|", item.TFM_Tid);
            //    }
            //}
            //idList = idList.TrimEnd('|');

            //Dictionary<string,string> keyList = new Dictionary<string,string>();
            //foreach (GlassInfo glass in this.NGGlassList.Values)
            //{
            //    keyList.Add(glass.ProductInfo.LCDCode, glass.LastExceptionKey);
            //}

            btnOK.Enabled = false;
            btnCancel.Enabled = false;

            // 数据提交后台处理
            bgwWriteData.RunWorkerAsync(new object[] { "", type, txtDescribe.Tag.ToString(), txtDescribe.Text, YJ.Common.TypeConverter.StrToInt(BaseUI.UI_BOOPID), this.NGGlassList, txtCurrentOpCode.Text, txtProcessIP.Text });
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
                //string idList = val[0].ToString();
                string type = val[1].ToString();
                string bncid = val[2].ToString();
                string bncname = val[3].ToString();
                int pid = YJ.Common.TypeConverter.ObjectToInt(val[4]);
                Dictionary<string, GlassInfo> ngGlassList = (Dictionary<string, GlassInfo>)val[5];
                string pname = val[6].ToString();
                string ip = val[7].ToString();

                string state = "复判重工";
                if (type == "报废")
                {
                    state = "复判报废";
                }
                else if (type == "误判")
                {
                    state = "复判良品";
                }
                //// 调用存储过程，提交数据,将复判结果提交到SQL
                //if (!string.IsNullOrWhiteSpace(idList))
                //{
                //    bool rst = BaseUI.Check_ProcessFail(idList, type, bncid, bncname, pid);
                //    if (rst)
                //    {
                //        e.Result = null;
                //    }
                //    //else
                //    //{
                //    //    e.Result = new Exception("数据提交失败!");
                //    //}
                //}

                // 遍历每一片玻璃，提交复判结果到HBase
                foreach (GlassInfo glassInfo in ngGlassList.Values)
                {
                    GlassInfo obj = glassInfo;

                    string judgeRst = GetHBaseDataClass.Instance.ExceptionJudge(ref obj, state, pname, bncname, BaseUI.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"), ip);
                    if (judgeRst != "复判成功")
                    {
                        e.Result = new Exception(judgeRst);
                        return;
                    }
                }
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
            btnOK.Enabled = true;
            btnCancel.Enabled = true;

            if (e.Result == null)
            {
                //// 将不良参数添加到华为数据上传参数表HB_Product_Parameter
                //Utils.Model.HB_Product_Parameter para = new Utils.Model.HB_Product_Parameter()
                //{
                //    HPP_Guid = Guid.NewGuid().ToString(),
                //    HPP_Code = this.dicGlassInfo["ProductOrder"],                   //工单编码
                //    HPP_SMCode = this.dicGlassInfo["FinishesCode"],                 //产品编码
                //    HPP_SMSpec = this.dicGlassInfo["FinishesModel"],                //成品型号
                //    HPP_CusCode = this.dicGlassInfo["ClientProductNo"],             //客户料号
                //    HPP_SPLJobCode = this.dicGlassInfo["LineCode"],                 //产线编码
                //    HPP_SGXJobCode = this.dicGlassInfo["ProcessCode"],              //工序编码
                //    HPP_DeviceCode = "",                                            //机台编码
                //    HPP_CardIP = this.dicGlassInfo["DeviceIp"],                     //设备IP
                //    HPP_ScanIP = "",                                                //扫描IP
                //    HPP_ProductDate = this.dicGlassInfo["ProductDate"],             //生产日期
                //    HPP_LotCode = this.dicGlassInfo["LotNumber"],                   //批次号
                //    HPP_AddDate = Convert.ToDateTime(this.dicGlassInfo["DateTime"]),//过站时间
                //    HPP_LCD = this.dicGlassInfo["GlassCode"],                       //玻璃码
                //    HPP_FPC = this.dicGlassInfo["FPCCode"],                         //FPC码
                //    HPP_TP = this.dicGlassInfo["TPCode"],                           //TP码
                //    HPP_BL = this.dicGlassInfo["BLCode"],                           //背光码
                //    HPP_QR = this.dicGlassInfo["QRCode"],                           //成品码
                //    HPP_Parameter1 = bncname,                                       //测试参数
                //    HPP_Parameter2 = "--",                                          //测试参数
                //    HPP_Parameter3 = "--",                                          //测试参数
                //    HPP_Parameter4 = "--",                                          //测试参数
                //    HPP_Parameter5 = "--",                                          //测试参数
                //    HPP_Parameter6 = "--",                                          //测试参数
                //    HPP_Parameter7 = "--",                                          //测试参数
                //    HPP_Parameter8 = "--",                                          //测试参数
                //    HPP_Parameter9 = "--",                                          //测试参数
                //    HPP_Parameter10 = "--",                                         //测试参数
                //    HPP_TestResult = "FAIL",                                        //测试结果
                //    HPP_IsSend = false
                //};
                //BaseUI.Add_HB_Product_Parameter(para);

                ShowResult(NoteState.OK, "", "操作成功！");
            }
            else
            {
                Exception ex = e.Result as Exception;
                ShowResult(NoteState.NG, "", "提交失败，请重试！\r\n" + ex.Message);
                txtScanCode.SelectAll();
                txtScanCode.Focus();
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Set_TFM_Type("");
            //初始化工序
            BindProcessComboBox(BaseUI.UI_SPOMJobCode, BaseUI.UI_SPLJOBCODE);
            // 设置程序标题
            this.Text = "不良复判 --【请选择复判结果】   版本:" + this.versionName + "（技术支持：深圳市鼎立特科技有限公司）";
        }

        /// <summary>
        /// 不良编码按下回车键，查找不良项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBNCCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BNCCodeSearch(txtBNCCode.Text.Trim());
            }
        }
        /// <summary>
        /// 查找不良项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BNCCodeSearch(txtBNCCode.Text.Trim());
        }

        /// <summary>
        /// 查找指定的不良项编码
        /// </summary>
        /// <param name="code"></param>
        private void BNCCodeSearch(string code)
        {
            foreach (CheckBox chk in flpNG.Controls)
            {
                if (code == "")
                {
                    chk.Visible = true;
                }
                else
                {
                    if (chk.Tag.ToString().Contains(code.ToUpper()))
                    {
                        chk.Visible = true;
                    }
                    else
                    {
                        chk.Visible = false;
                    }
                }
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择不良图片文件夹";
            if (folder.ShowDialog(this) == DialogResult.OK)
            {
                this.NGPath = new System.IO.DirectoryInfo(folder.SelectedPath);
                appConfig.SetConfig("AOI_NG_Path", folder.SelectedPath);
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            Image img = picNG.Image;
            if (img != null)
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                picNG.Image = img;
            }
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            if (picNG.Image != null)
            {
                string path = dicLCD_Path[txtLCD.Text];
                dlgImageView imgView = new dlgImageView(path, picNG.Image);
                imgView.ShowDialog(this);
            }
        }
        /// <summary>
        /// 选择是否启用不良图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNGImageEnabled_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNGImageEnabled.SelectedIndex == 0)
            {
                this.AOI_NG_Enabled = true;
                appConfig.SetConfig("AOI_NG_Enabled", "True");
            }
            else
            {
                this.AOI_NG_Enabled = false;
                appConfig.SetConfig("AOI_NG_Enabled", "False");
                this.picNG.Image = null;
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            flpNG.VerticalScroll.Value = flpNG.VerticalScroll.Minimum;
            flpNG.VerticalScroll.Value = flpNG.VerticalScroll.Minimum;
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            flpNG.VerticalScroll.Value = flpNG.VerticalScroll.Maximum;
            flpNG.VerticalScroll.Value = flpNG.VerticalScroll.Maximum;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int pos = flpNG.VerticalScroll.Value - 183;
            if (pos < 0)
            {
                pos = 0;
            }
            flpNG.VerticalScroll.Value = pos;
            flpNG.VerticalScroll.Value = pos;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int pos = flpNG.VerticalScroll.Value + 183;
            if (pos > flpNG.VerticalScroll.Maximum)
            {
                pos = flpNG.VerticalScroll.Maximum;
            }
            flpNG.VerticalScroll.Value = pos;
            flpNG.VerticalScroll.Value = pos;
        }


    }
}
