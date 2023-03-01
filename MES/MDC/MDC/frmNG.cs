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

namespace MDC
{
    public partial class frmNG : Form
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
        /// <summary>
        /// 玻璃信息
        /// </summary>
        private Dictionary<string, string> dicGlassInfo;
        /// <summary>
        /// Code1扫码类型
        /// </summary>
        private string scanType1;
        /// <summary>
        /// 待提报不良玻璃列表(玻璃码，扫描码)
        /// </summary>
        public Dictionary<string, string> NGGlassList = new Dictionary<string, string>();
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
        /// 构造函数
        /// </summary>
        public frmNG()
        {
            InitializeComponent();

            txtSPLName.Dock = DockStyle.Fill;
            txtProcessIP.Dock = DockStyle.Fill;
            txtOpCode.Dock = DockStyle.Fill;
            txtRule1.Dock = DockStyle.Fill;
            txtDigit1.Dock = DockStyle.Fill;
            txtCode1.Dock = DockStyle.Fill;
            txtDescribe.Dock = DockStyle.Fill;
            txtBNCCode.Dock = DockStyle.Fill;
            txtOrder.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;
            comProcess.Dock = DockStyle.Fill;

            txtSPLName.Clear();
            txtProcessIP.Clear();
            txtOpCode.Clear();
            txtRule1.Clear();
            txtDigit1.Clear();
            txtCode1.Clear();
            txtDescribe.Clear();
            txtBNCCode.Clear();
            txtModel.Clear();
            txtOrder.Clear();
            lvwGlass.Items.Clear();
            flpNG.Controls.Clear();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNG_Load(object sender, EventArgs e)
        {
            try
            {
                // 当前程序版本
                this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                // 绑定串口数据接收事件处理程序
                Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;
                // 初始化窗体信息
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
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNG_FormClosing(object sender, FormClosingEventArgs e)
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
        private void frmNG_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
            this.flpNG.Refresh();
        }

        /// <summary>
        /// 程序初始化
        /// </summary>
        private void Initialize()
        {
            try
            {
                lvwGlass.Items.Clear();
                NGGlassList.Clear();
                NGDesc = "";
                // 登录人员账号
                txtOpCode.Text = BaseUI.UI_BOOLOGNAME;
                // 玻璃信息查询超时时间
                searchTimeout = appConfig.GetConfigInt("SearchTimeout");
                // 当前站点IP
                this.ipAddress = BaseUI.GetLocalIP();
                // 设备IP
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
                // 成品型号
                txtModel.Text = BaseUI.UI_SMSPEC;//成品规格
                // 工单编码
                txtOrder.Text = BaseUI.UI_SPOMJobCode;//工单编码
                // 产线名称
                txtSPLName.Text = BaseUI.UI_SPLNAME;

                //初始化工序
                BindProcessComboBox(BaseUI.UI_SPOMJobCode, BaseUI.UI_SPLJOBCODE);

                // 初始化不良项数据表
                this.dtBNC = BaseUI.GetBNCTable();
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
                throw ex;
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
                object GXCode = comProcess.SelectedValue;
                //获取工序链
                this.dtProcessTable = BaseUI.GetProduceRouteData(ProductionOrder, LineCode);
                comProcess.DisplayMember = "SGX_Name";  //工序名称
                comProcess.ValueMember = "SGX_JobCode"; //工序编码
                comProcess.DataSource = this.dtProcessTable;

                if (GXCode == null)
                {
                    // 设置程序标题
                    this.Text = "不良申报 --【请选择本站工序】   版本:" + this.versionName;
                    // 补扫站点，待选择
                    comProcess.SelectedIndex = -1;
                }
                else
                {
                    // 选择当前工序
                    try
                    {
                        comProcess.SelectedValue = GXCode;
                    }
                    catch (Exception)
                    {
                        // 设置程序标题
                        this.Text = "不良申报 --【请选择本站工序】   版本:" + this.versionName;
                        // 补扫站点，待选择
                        comProcess.SelectedIndex = -1;
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
        /// 选择工序后下拉框关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comProcess_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comProcess.SelectedIndex == -1) return;
            // 初始化窗体信息
            PageInit();
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
                    BaseUI.UI_CurrentProcedureNo = this.dvProcess[0]["PR_NoSeq"].ToString();          //当前工序序号
                    BaseUI.UI_CurrentProcedureCode = this.dvProcess[0]["SGX_JobCode"].ToString();      //当前工序编码
                    //当前站点IP地址,获取的工序对应IP
                    BaseUI.UI_EquCardIP = this.dvProcess[0]["STW_CardIP"].ToString();
                    txtProcessIP.Text = BaseUI.UI_EquCardIP;

                    // 扫描类型
                    if (String.IsNullOrEmpty(this.dvProcess[0]["PR_ScanType"].ToString()))
                    {
                        ShowResult(NoteState.NG, "", "请先在MES系统中配置工序【" + comProcess.Text + "】的扫描类型");
                        return;
                    }
                    string PR_ScanType = Enum.GetName(typeof(ScanType), Convert.ToInt32(this.dvProcess[0]["PR_ScanType"]));
                    string[] scanType = PR_ScanType.Split(new string[] { "_and_", "_or_" }, StringSplitOptions.RemoveEmptyEntries);
                    this.scanType1 = scanType[0];
                    lblCode1.Text = this.scanType1 + "编码:";
                    txtRule1.Text = this.dvProcess[0]["Rule_" + this.scanType1].ToString();
                    txtDigit1.Text = this.dvProcess[0]["Len_" + this.scanType1].ToString();
                    lvwGlass.Columns[1].Text = this.scanType1 + "编码";
                    // 设置程序标题
                    this.Text = "不良申报 -- " + this.dvProcess[0]["SGX_Name"].ToString() + "   版本:" + this.versionName;

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
                txtCode1.SelectAll();
                txtCode1.Focus();
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
            flpNG.Controls.Clear();
            txtBNCCode.Clear();

            // 添加不良项列表
            int width = 0;
            DataView dv = new DataView(this.dtBNC);
            dv.RowFilter = string.Format("TPD_ProcessCode = '{0}'", BaseUI.UI_CurrentProcedureCode);
            dv.Sort = "TPD_BNCName ASC";

            foreach (DataRowView row in dv)
            {
                CheckBox chk = new CheckBox()
                {
                    Appearance = System.Windows.Forms.Appearance.Button,
                    AutoSize = true,
                    Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                    ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85))))),
                    Location = new System.Drawing.Point(3, 3),
                    Margin = new System.Windows.Forms.Padding(3, 10, 3, 10),
                    UseVisualStyleBackColor = true,
                    Name = row["TPD_BNCTid"].ToString(),    //不良项ID
                    Text = row["TPD_BNCName"].ToString(),   //不良项名称
                    Tag = row["TPD_BNCCode"],               //不良项编码
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.flpNG.Controls.Add(chk);
                chk.CheckStateChanged += this.BNC_CheckStateChanged;

                if (chk.Width > width)
                {
                    width = chk.Width;
                }
            }

            bool isSelected = false;
            foreach (CheckBox chk in this.flpNG.Controls)
            {
                int height = chk.Height;
                chk.AutoSize = false;
                chk.Width = width;
                chk.Height = height;
                if (!isSelected)
                {
                    chk.Checked = true;
                    isSelected = true;
                }
            }

            this.Refresh();
            txtCode1.SelectAll();
            txtCode1.Focus();
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
                            PageInit();
                            lvwGlass.Items.Clear();
                            this.dicGlassInfo = null;
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
                    txtCode1.SelectAll();
                    txtCode1.Focus();
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
            if (e.KeyChar == (char)Keys.Enter && txtCode1.Text != "")
            {
                // 分析处理数据
                ScanDataHandler(txtCode1.Text.Trim());
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
                    if (string.IsNullOrWhiteSpace(txtSPLName.Text))
                    {
                        ShowResult(NoteState.Fail, "", "获取设备所属产线失败");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(comProcess.Text))
                    {
                        ShowResult(NoteState.Fail, "", "请先选择本站工序");
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
                    if (DataString.Contains("OK:") || DataString.Contains("ERROR:"))
                    {
                        ShowResult(NoteState.NG, "", "编码不符合规则，请确认扫描枪是否打开了测试模式");
                        return;
                    }

                    string[] ds = DataString.Split(':');
                    if (ds.Length == 0)
                    {
                        ShowResult(NoteState.NG, "", "编码格式不符合规则");
                        return;
                    }
                    string AnalysisCode = ds[0].ToString().Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");    //扫描数据解析

                    // 基本长度判断
                    if (AnalysisCode.Length <= 9)
                    {
                        ShowResult(NoteState.NG, "", "默认编码长度至少为10位以上，请确认条码长度");
                        return;
                    }
                    #endregion 第2步：解析扫描数据

                    // 第3步：查询玻璃过站信息
                    #region 第3步：查询玻璃过站信息
                    this.dicGlassInfo = GetHBaseDataClass.Instance.GetGlassProcessInfo(AnalysisCode, BaseUI.UI_CurrentProcedureCode);
                    // 判断是否获取到玻璃信息
                    if (dicGlassInfo == null)
                    {
                        ShowResult(NoteState.NG, "", "获取FOG绑定信息失败!");
                        return;
                    }

                    // 判断是否混线
                    if (!string.IsNullOrEmpty(dicGlassInfo["LineCode"]) && dicGlassInfo["LineCode"].Length >= 2 && dicGlassInfo["LineCode"] != BaseUI.UI_SPLJOBCODE)
                    {
                        string txtError = string.Format("该玻璃生产产线为{0}线,与当前产线不一致", dicGlassInfo["LineCode"].Substring(0, 2));
                        ShowResult(NoteState.NG, "", txtError);
                        return;
                    }

                    // 判断是否获取到过站信息
                    if (dicGlassInfo["DateTime"] == null)
                    {
                        ShowResult(NoteState.NG, "", "当前工序未扫描!\n请先过站再提报不良.");
                        return;
                    }
                    #endregion 第3步：查询玻璃过站信息

                    #region 去重
                    if (this.NGGlassList.ContainsKey(dicGlassInfo["GlassCode"]))
                    {
                        ShowResult(NoteState.NG, "", "列表中已存在此片玻璃，\r\n请勿重复扫描!");
                        return;
                    }
                    #endregion 去重

                    #region 第4步：判断是否切换工单
                    //工单编码
                    string ProductOrder = dicGlassInfo["ProductOrder"];
                    //成品型号
                    string FinishesModel = dicGlassInfo["FinishesModel"];

                    if (!string.IsNullOrEmpty(ProductOrder) && ProductOrder != txtOrder.Text)
                    {
                        if (this.NGGlassList.Count > 0)
                        {
                            ShowResult(NoteState.NG, "", "该玻璃生产工单与当前工单不一致，不能混合申报");
                            return;
                        }
                        else
                        {
                            // 初始化工序
                            BindProcessComboBox(ProductOrder, BaseUI.UI_SPLJOBCODE);
                            // 初始化页面
                            txtOrder.Text = ProductOrder;
                            txtModel.Text = FinishesModel;
                        }
                    }

                    #endregion 判断是否切换工单

                    #region Code1编码规则检查
                    if (txtRule1.Text == "")
                    {
                        ShowResult(NoteState.NG, "", "未设置" + this.scanType1 + "编码规则");
                        return;
                    }
                    if (txtDigit1.Text == "")
                    {
                        ShowResult(NoteState.NG, "", "未设置" + this.scanType1 + "编码长度");
                        return;
                    }

                    // 编码长度检查
                    if (txtDigit1.Text != "0" && AnalysisCode.Length != YJ.Common.Utils.StrToInt(txtDigit1.Text, 0))
                    {
                        ShowResult(NoteState.NG, "", "编码长度不符合规则，长度为" + DataString.Length);
                        return;
                    }
                    // 编码规则检查
                    if (AnalysisCode.Substring(0, txtRule1.Text.Length) != txtRule1.Text)
                    {
                        ShowResult(NoteState.NG, "", "编码格式不符合规则");
                        return;
                    }
                    // 填入文本框
                    txtCode1.Text = AnalysisCode;

                    #endregion Code1编码规则检查

                    #region 查询玻璃是否已提交不良信息
                    try
                    {
                        if (dicGlassInfo != null && !string.IsNullOrEmpty(dicGlassInfo["LCDState"]))
                        {
                            switch (dicGlassInfo["LCDState"])
                            {
                                case "待复判":
                                    ShowResult(NoteState.NG, "", "该玻璃已申报不良，\r\n请勿重复提交！");
                                    return;

                                case "复判重工":
                                    ShowResult(NoteState.NG, "", "此玻璃已复判重工，\r\n请提交重工组！");
                                    return;

                                case "复判报废":
                                    ShowResult(NoteState.NG, "", "此玻璃已复判报废！");
                                    return;

                                case "重工报废":
                                    ShowResult(NoteState.NG, "", "此玻璃已重工报废！");
                                    return;
                            }
                        }
                        else
                        {
                            FailReviewResult reviewResult = BaseUI.GetFailReviewResult(dicGlassInfo["GlassCode"]);
                            switch (reviewResult)
                            {
                                case FailReviewResult.已申报未复判:
                                    ShowResult(NoteState.NG, "", "该玻璃已申报不良，\r\n请勿重复提交！");
                                    return;

                                case FailReviewResult.复判重工:
                                    ShowResult(NoteState.NG, "", "此玻璃已复判重工，\r\n请提交重工组！");
                                    return;
                                case FailReviewResult.复判报废:
                                    ShowResult(NoteState.NG, "", "此玻璃已报废！");
                                    return;
                            }
                        }
                    }
                    catch (Exception exp)
                    {
                        ShowResult(NoteState.NG, "", "查询玻璃不良信息失败!\r\n" + exp.Message.ToString());
                        return;
                    }

                    #endregion 查询玻璃是否已提交不良信息

                    // 添加至玻璃列表
                    this.NGGlassList.Add(dicGlassInfo["GlassCode"], txtCode1.Text);

                    // 添加Note信息
                    ListViewItem item = this.lvwGlass.Items.Insert(0,(this.lvwGlass.Items.Count + 1).ToString());
                    item.SubItems.AddRange(new string[]
                    {
                        txtCode1.Text, //扫描码
                        dicGlassInfo["GlassCode"],   // 玻璃码
                        dicGlassInfo["DateTime"]    //过站时间
                    });
                    
                    txtCode1.Clear();
                    txtCode1.Focus();
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
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwGlass.SelectedItems.Count != 1) return;
            int idx = lvwGlass.SelectedIndices[0];
            // 获取玻璃码
            string lcd = lvwGlass.SelectedItems[0].SubItems[2].Text;
            // 移除
            this.NGGlassList.Remove(lcd);
            lvwGlass.Items.Remove(lvwGlass.SelectedItems[0]);
            // 重新编排序号（倒序）
            for (int i = 0; i < lvwGlass.Items.Count; i++)
            {
                lvwGlass.Items[i].Text = (lvwGlass.Items.Count - i).ToString();
            }

            // 选择下一行
            if (lvwGlass.Items.Count > idx)
            {
                lvwGlass.Items[idx].Selected = true;
                lvwGlass.Focus();
            }
            else
            {
                if (idx > 0)
                {
                    lvwGlass.Items[idx - 1].Selected = true;
                    lvwGlass.Focus();
                }
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
            if (lvwGlass.Items.Count == 0)
            {
                ShowResult(NoteState.NG, "", "请添加不良玻璃编码！");
                return;
            }

            if (txtDescribe.Text == "")
            {
                ShowResult(NoteState.NG, "", "请选择不良项！");
                return;
            }

            string fpc = ""; //FPC码
            string lcd = ""; //玻璃码
            string time = ""; //过站时间

            // 遍历每一片玻璃
            foreach (ListViewItem item in lvwGlass.Items)
            {
                fpc += item.SubItems[1].Text + "|"; //扫描码
                lcd += item.SubItems[2].Text + "|"; //玻璃码
                time += item.SubItems[3].Text + "|"; //过站时间
            }//foreach (ListViewItem item in lvwGlass.Items)

            fpc = fpc.TrimEnd('|');
            lcd = lcd.TrimEnd('|');
            time = time.TrimEnd('|');

            btnOK.Enabled = false;
            btnCancel.Enabled = false;

            // 数据提交后台处理
            bgwWriteData.RunWorkerAsync(new object[] { fpc, lcd, time, txtDescribe.Tag.ToString(), txtDescribe.Text, txtProcessIP.Text, YJ.Common.TypeConverter.StrToInt(BaseUI.UI_BOOPID), txtOrder.Text });
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
                string fpc = val[0].ToString();
                string lcd = val[1].ToString();
                string time = val[2].ToString();
                string bncid = val[3].ToString();
                string bncname = val[4].ToString();
                string ip = val[5].ToString();
                int pid = Convert.ToInt32(val[6]);
                string order = val[7].ToString(); 

                // 调用存储过程，提交数据
                bool rst = BaseUI.Add_ProcessFail("正操作", "重工", fpc, lcd, time, bncid, bncname, "01", ip, pid, order);
                if (rst)
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

                    e.Result = null;
                }
                else
                {
                    e.Result = new Exception("数据提交失败!");
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
                ShowResult(NoteState.OK, "", "不良申报成功！");
            }
            else
            {
                Exception ex = e.Result as Exception;
                LogHelper.Error("不良申报失败.", ex);
                ShowResult(NoteState.NG, "", "提交失败，请重试！\r\n" + ex.Message);
            }
            
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            PageInit();
            lvwGlass.Items.Clear();
            this.dicGlassInfo = null;
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
        ///// <summary>
        ///// 调用存储过程，添加不良申报信息
        ///// </summary>
        ///// <param name="fpc">FPC编码</param>
        ///// <param name="lcd">LCD编码</param>
        ///// <param name="time">过站时间</param>
        ///// <returns></returns>
        //private bool Add_ProcessFail(string fpc, string lcd, string time, string bncid, string bncname, string ip, int pid)
        //{
        //    object rst = BaseUI.Add_ProcessFail("正操作", "重工", fpc, lcd, time, bncid, bncname, "01", ip, pid); 
        //    return true;
        //}

        ///// <summary>
        ///// 通过SQL语句，添加不良申报信息
        ///// </summary>
        ///// <param name="fpc">FPC编码</param>
        ///// <param name="lcd">LCD编码</param>
        ///// <param name="time">过站时间</param>
        ///// <returns></returns>
        //private bool Add_ProcessFail_SQL(string fpc, string lcd, string time)
        //{
        //    // 创建传参对象并赋值
        //    Utils.Model.TPL_ProcessFail_Main TFM = new Utils.Model.TPL_ProcessFail_Main()
        //    {
        //        TFM_Status = "正操作",                       //提报状态
        //        TFM_Type = "重工",                           //提报类型
        //        TFM_Describe = "",                           //异常描述
        //        TFM_ProductTid = BaseUI.UI_SPOMID,           //工单ID
        //        TFM_ProductOrder = BaseUI.UI_SPOMJobCode,    //工单编码
        //        TFM_LineTid = BaseUI.UI_SPLID,               //产线ID
        //        TFM_LineCode = BaseUI.UI_SPLJOBCODE,         //产线编码
        //        TFM_ProcessTid = BaseUI.UI_SGXID,            //工序ID
        //        TFM_ProcessCode = BaseUI.UI_SGXJOBCODE,      //工序编码
        //        TFM_DeviceTid = BaseUI.UI_DeviceTid,         //设备ID
        //        TFM_DeviceIP = txtProcessIP.Text,            //设备IP地址
        //        TFM_ScanCode = fpc,                          //FPC码
        //        TFM_GlassCode = lcd,                         //玻璃码
        //        TFM_ScanTime = Convert.ToDateTime(time),                            //过站时间
        //        TFM_Num = 1,                                 //提报数量
        //        TFM_AddPid = YJ.Common.TypeConverter.StrToInt(BaseUI.UI_BOOPID), //用户ID
        //        TFM_AddDate = DateTime.Now                   //添加时间
        //    };
        //    // 将信息写入主表，并获取该行数据ID
        //    int idx = BaseUI.Add_ProcessFail_Main(TFM);

        //    if (idx == 0)
        //    {
        //        //txtError.Text = "数据提交失败！\r\n请重试";
        //        this.DialogResult = System.Windows.Forms.DialogResult.None;
        //        return false;
        //    }
        //    else    //主表写入成功，将不良项信息写入子表
        //    {
        //        string desc = "";   //不良项描述
        //        //遍历所有选中的不良项
        //        foreach (CheckBox chk in flpNG.Controls)
        //        {
        //            if (chk.Checked)
        //            {
        //                //创建传参对象并赋值
        //                Utils.Model.TPL_ProcessFail_Sub TFS = new Utils.Model.TPL_ProcessFail_Sub()
        //                {
        //                    TFS_TFMTid = idx,               //主表数据ID
        //                    TFS_BNCTid = YJ.Common.TypeConverter.StrToInt(chk.Name),    //不良项ID
        //                    TFS_BNCName = chk.Text,         //不良项名称
        //                    TFS_Type = "01",                //异常来源（默认为01产线提报判定）
        //                    TFS_AddPid = YJ.Common.TypeConverter.StrToInt(BaseUI.UI_BOOPID),    //用户ID
        //                    TFS_AddDate = DateTime.Now      //添加时间
        //                };
        //                // 将不良项信息写入子表
        //                int subidx = BaseUI.Add_ProcessFail_Sub(TFS);
        //                if (subidx == 0)
        //                {
        //                    //txtError.Text = "数据提交失败！\r\n请重试";
        //                    this.DialogResult = System.Windows.Forms.DialogResult.None;
        //                    break;
        //                }//if (subidx == 0)

        //                // 拼接异常描述内容
        //                desc += TFS.TFS_BNCName + "|";
        //            }//if (chk.Checked)
        //        }//foreach (CheckBox chk in flpNG.Controls)

        //        // 不良描述
        //        desc = desc.TrimEnd('|');

        //        // 回更主表异常描述内容
        //        BaseUI.Update_TFM_Describe(idx, desc);
        //    }//if (idx == 0)

        //    return true;
        //}





    }
}
