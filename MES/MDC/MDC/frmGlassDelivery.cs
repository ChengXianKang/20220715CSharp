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
using Newtonsoft.Json.Linq;
using Utils;
using Utils.HBaseClass;

namespace MDC
{
    public partial class frmGlassDelivery : Form
    {
        #region 私有字段
        /// <summary>
        /// 站点类型（"Delivery":产线投料, "Receive":机台上料）
        /// </summary>
        private string siteType;
        /// <summary>
        /// 本机IP
        /// </summary>
        private string ipAddress = "";
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;        //失败提示框
        /// <summary>
        /// 成功提示框
        /// </summary>
        private SuccessMessage successMsg;  //成功提示框
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
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
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 工单所需物料清单编码列表
        /// </summary>
        private List<string> MaterialCodeList;
        /// <summary>
        /// 工单数量
        /// </summary>
        private int OrderNumber;
        /// <summary>
        /// Cell投料数量
        /// </summary>
        private int FeedNumber;
        /// <summary>
        /// 清洗投入数量
        /// </summary>
        private int InputNumber;
        /// <summary>
        /// 已扫描批数
        /// </summary>
        private int ScanLotCount;
        /// <summary>
        /// 产线生产工单信息表（生成计划，生产中，已完结，生产挂起）
        /// </summary>
        private DataTable dtProductionOrder;
        /// <summary>
        /// 切换工单接口地址
        /// </summary>
        private string orderSwitchUrl;
        /// <summary>
        /// 提交待产工单提醒次数
        /// </summary>
        private int remindTimes;

        private System.Windows.Forms.Timer tmrNumRefresh;
        /// <summary>
        /// 车间产线列表
        /// </summary>
        DataTable dtline;

        #endregion 私有字段

        #region 构造函数
        /// <summary>
        /// frmMain的构造函数
        /// </summary>
        public frmGlassDelivery(string SiteType)
        {
            InitializeComponent();

            txtLineName.Visible = false;
            txtLineName.Dock = DockStyle.Fill;
            txtIP.Dock = DockStyle.Fill;
            txtOp.Dock = DockStyle.Fill;
            txtOrder.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;
            txtScanCode.Dock = DockStyle.Fill;
            txtSN.Dock = DockStyle.Fill;
            txtProduct.Dock = DockStyle.Fill;
            txtCount.Dock = DockStyle.Fill;
            txtLine.Dock = DockStyle.Fill;
            txtProductionOrder.Dock = DockStyle.Fill;
            txtProductModel.Dock = DockStyle.Fill;
            txtOrderNumber.Dock = DockStyle.Fill;
            txtFeedNumber.Dock = DockStyle.Fill;
            txtInputNumber.Dock = DockStyle.Fill;
            btnRefresh.Dock = DockStyle.Fill;
            cmbNextOrder.Dock = DockStyle.Fill;
            btnSwitchOrder.Dock = DockStyle.Fill;
            cmbShopName.Dock = DockStyle.Fill;

            txtLineName.Clear();
            txtIP.Clear();
            txtOp.Clear();
            txtModel.Clear();
            txtOrder.Clear();
            txtScanCode.Clear();
            txtSN.Clear();
            txtProduct.Clear();
            txtCount.Clear();
            txtLine.Clear();
            txtProductionOrder.Clear();
            txtProductModel.Clear();
            txtOrderNumber.Clear();
            txtFeedNumber.Clear();
            txtInputNumber.Clear();
            cmbNextOrder.Items.Clear();
            cmbShopName.Items.Clear();
            lvwNote.LostFocus += new EventHandler(lvwNote_LostFocus);
            lvwNote.Columns[3].Width = 150;

            this.siteType = SiteType;
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
                // 绑定串口数据接收事件处理程序
                //Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;
                Program.ScanDevice.DataReceived += new Utils.DataReceivedEventHandler(ScanDevice_DataReceived);
                // 程序初始化
                Initialize();

                this.tmrNumRefresh = new System.Windows.Forms.Timer();
                this.tmrNumRefresh.Interval = 10000;
                this.tmrNumRefresh.Tick += new EventHandler(tmrNumRefresh_Tick);
                this.tmrNumRefresh.Start();
            }
            catch (Exception ex)
            {
                ShowResult(NoteState.Error, "", "程序初始化失败!\n" + ex.Message);
                this.Close();
                return;
            }
        }

        /// <summary>
        /// 定时刷新投料数和清洗投入数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrNumRefresh_Tick(object sender, EventArgs e)
        {
            // 投料数量和扫码数量
            BaseUI.GetFeedAndInputNumber(BaseUI.UI_SPOMJobCode, out this.FeedNumber, out this.InputNumber);
            txtFeedNumber.Text = this.FeedNumber.ToString();
            txtInputNumber.Text = this.InputNumber.ToString();
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmScan_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
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
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        /// <summary>
        /// 提交待产工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextOrder_Click(object sender, EventArgs e)
        {
            // 未选择工单
            if (cmbNextOrder.SelectedIndex == -1 || cmbNextOrder.SelectedValue == null)
            {
                ShowResult(NoteState.NG, "", "请选择待产工单后，再点击提交.");
                return;
            }
            // 上一完结工单有未完成退/补料单
            if (!isMaterialStartEnabled())
            {
                ShowResult(NoteState.NG, "", "上一完结工单有未完成退料单，\r\n暂时不能提交.");
                return;
            }

            WarnMessage warn = new WarnMessage("提交待产工单后，将不可更改。\r\n是否确定提交此工单？", MessageBoxButtons.OKCancel, 9999, "Cancel");
            if (warn.ShowDialog(this) == DialogResult.OK)
            {
                if (BaseUI.NextOrderCommit(BaseUI.UI_SPOMJobCode, (int)cmbNextOrder.SelectedValue))
                {
                    ShowResult(NoteState.OK, cmbNextOrder.Text.ToString(), "已提交待产工单，系统将开始配料.");
                    cmbNextOrder.Enabled = false;
                    btnNextOrder.Enabled = false;
                    return;
                }
                else
                {
                    ShowResult(NoteState.NG, cmbNextOrder.Text.ToString(), "待产工单提交失败，\r\n请重新提交！");
                    return;
                }
            }
        }

        /// <summary>
        /// 切换工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwitchOrder_Click(object sender, EventArgs e)
        {
            // 未选择工单
            if (cmbNextOrder.SelectedIndex == -1 || cmbNextOrder.SelectedValue == null)
            {
                ShowResult(NoteState.NG, "", "请先选择待产工单.");
                return;
            }

            // 刷新投料数量和清洗投入数量
            BaseUI.GetFeedAndInputNumber(BaseUI.UI_SPOMJobCode, out this.FeedNumber, out this.InputNumber);
            txtFeedNumber.Text = this.FeedNumber.ToString();
            txtInputNumber.Text = this.InputNumber.ToString();

            if (this.FeedNumber < this.OrderNumber)
            {
                WarnMessage warn = new WarnMessage("当前工单尚未完成投料，\r\n是否仍要切换工单？", MessageBoxButtons.OKCancel, 9999, "Cancel");
                if (warn.ShowDialog(this) != DialogResult.OK) return;            
            }

            if (this.InputNumber < this.FeedNumber)
            {
                string msg = string.Format("已投料玻璃中有{0}片未扫描，\r\n是否仍要切换工单？", this.FeedNumber - this.InputNumber);
                WarnMessage warn = new WarnMessage(msg, MessageBoxButtons.OKCancel, 9999, "Cancel");
                if (warn.ShowDialog(this) != DialogResult.OK) return;
            }

            // 工单完结对话框
            dlgOrder dlgOrder = new dlgOrder(txtProductionOrder.Text, cmbNextOrder.Text, this.OrderNumber, this.FeedNumber, this.InputNumber);
            if (dlgOrder.ShowDialog(this) == DialogResult.OK)
            {
                string orderToDo = dlgOrder.OrderToDo;
                string note = dlgOrder.Note;
                string codeType;
                if (orderToDo == "完结")
                {
                    codeType = "01";                    
                }
                else//工单挂起
                {
                    codeType = "02";      
                    //ShowResult(NoteState.OK, cmbNextOrder.Text, "工单切换失败！\r\n暂不支持工单挂起.");
                }

                string response = BaseUI.OrderSwitch(this.orderSwitchUrl, codeType, BaseUI.UI_SPLJOBCODE, BaseUI.UI_SPOMJobCode);
                //{"status":"0","msg":"error","result":"当前产线无下一工单"}
                dynamic reObj = JObject.Parse(response);
                string status = reObj.status;
                if (status == "1")
                {
                    // 设置工单完结信息
                    BaseUI.SetOrderOverInfo(txtProductionOrder.Text, note);

                    ShowResult(NoteState.OK, cmbNextOrder.Text, "工单切换成功.");

                    // 重新初始化
                    Initialize();
                }
                else if (status == "0")
                {
                    string msg = reObj.msg;
                    string result = reObj.result;
                    ShowResult(NoteState.NG, msg, result);
                    return;
                }
            }

        }

        private void cmbShopName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbShopName.SelectedIndex < 0) return;
            string shopName = cmbShopName.Text;
            DataView dvLine = new DataView(this.dtline);
            dvLine.RowFilter = string.Format("SPL_ShopName = '{0}'", shopName);
            dvLine.Sort = "SPL_Name ASC";
            // 初始化产线选择下拉框
            cmbLineName.DisplayMember = "SPL_Name";
            cmbLineName.ValueMember = "SPL_JobCode";
            cmbLineName.DataSource = dvLine;
            cmbLineName.SelectedIndex = 0;
        }

        #region 方法
        /// <summary>
        /// 是否允许工单开卡
        /// </summary>
        /// <returns></returns>
        private bool isMaterialStartEnabled()
        {
            int lastOrderId = -1;// 上一完结工单ID
            DataView dvOver = new DataView(this.dtProductionOrder);
            dvOver.RowFilter = "SPOM_Status = '已完结'";
            if (dvOver != null && dvOver.Count > 0)
            {
                lastOrderId = YJ.Common.Utils.StrToInt(dvOver[0]["SPOM_Tid"], -1);
            }
            if (lastOrderId == -1) return true;

            return BaseUI.IsProductNgBackOver(lastOrderId);
        }

        /// <summary>
        /// 程序初始化
        /// </summary>
        private void Initialize()
        {
            // 当前程序版本
            this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // 登录人员账号
            txtOp.Text = BaseUI.UI_BOOLOGNAME;
            // 玻璃信息查询超时时间
            searchTimeout = appConfig.GetConfigInt("SearchTimeout");
            // 最大日志行数
            maxLogCount = appConfig.GetConfigInt("MaxLogCount");
            // 是否记录程序运行日志
            isWriteLog = appConfig.GetConfigBool("WriteAppLog");

            // 初始化车间选择下拉框
            this.dtline = BaseUI.GetProcedureLine().Table;
            DataTable dtshop = new DataView(dtline).ToTable(true, new string[] { "SPL_ShopName" });
            cmbShopName.DisplayMember = "SPL_ShopName";
            cmbShopName.ValueMember = "SPL_ShopName";
            cmbShopName.DataSource = dtshop;
            cmbShopName.SelectedIndex = 1;

            // 初始化产线选择下拉框
            string shopName = cmbShopName.Text;
            DataView dvLine = new DataView(this.dtline);
            dvLine.RowFilter = string.Format("SPL_ShopName = '{0}'", shopName);
            dvLine.Sort = "SPL_Name ASC";
            cmbLineName.DisplayMember = "SPL_Name";
            cmbLineName.ValueMember = "SPL_JobCode";
            cmbLineName.DataSource = dvLine;
            cmbLineName.SelectedIndex = 0;

            // 当前站点IP
            this.ipAddress = BaseUI.GetLocalIP();
#if DEBUG
            //this.ipAddress = "172.16.7.21";
            //this.ipAddress = "172.16.28.86";
            //this.ipAddress = "172.16.1.101";
            //this.ipAddress = "172.16.3.133"; //C3车间收料IP
#endif
            txtIP.Text = this.ipAddress;

            if (this.siteType == "Delivery")//发料
            {
                this.Text = string.Format("Cell车间发料  {0}", this.versionName);
                txtLineName.Text = "同兴达Cell车间";
                lblShopName.Text = "发往车间:";
                lblLineName.Text = "发往产线:";
                //cmbShopName.SelectedIndex = -1;
                cmbShopName.Enabled = true;
                cmbLineName.Enabled = true;
                tlpOrder.Visible = false;
            }
            else if (this.siteType == "Check")//收料
            {
                try
                {
                    // 获取本机IP所在的产线、工序、产线在制品的型号
                    BaseUI.GetLineModelProcedure(this.ipAddress);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (!string.IsNullOrEmpty(BaseUI.UI_SPLJOBCODE))
                {
                    DataView dv = new DataView(this.dtline);
                    dv.RowFilter = string.Format("SPL_JobCode = '{0}'", BaseUI.UI_SPLJOBCODE);
                    string shop = dv[0]["SPL_ShopName"].ToString();
                    this.Text = string.Format("{0}接收  {1}", shop, this.versionName);
                    cmbShopName.SelectedValue = shop;
                }
                else
                {
                    cmbShopName.SelectedIndex = -1;
                    ShowResult(NoteState.Error, "", "未能获取本机IP所在产线!");
                }
              
                cmbShopName.Enabled = false;
                lblShopName.Text = "收料车间:";
                lblLineName.Text = "收料产线";
                cmbLineName.SelectedIndex = -1;
                cmbLineName.Enabled = false;
                
                tlpOrder.Visible = false;
            }
            else if (this.siteType == "Receive")//机台上料
            {
                try
                {
                    // 获取本机IP所在的产线、工序、产线在制品的型号
                    BaseUI.GetLineModelProcedure(this.ipAddress);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // 切换工单接口地址
                orderSwitchUrl = appConfig.GetConfigString("OrderSwitchUrl");
                lblShopName.Text = "投料车间:";
                lblLineName.Text = "投料产线:";
                cmbShopName.Enabled = false;
                cmbLineName.Enabled = false;
                this.Text = string.Format("产线投料  {0}", this.versionName);
                tlpOrder.Visible = true;

                // 产线名称
                txtLineName.Text = BaseUI.UI_SPLNAME;
                if (!string.IsNullOrEmpty(BaseUI.UI_SPLJOBCODE))
                {
                    // 确定投料车间
                    DataView dv = new DataView(this.dtline);
                    dv.RowFilter = string.Format("SPL_JobCode = '{0}'", BaseUI.UI_SPLJOBCODE);
                    string shop = dv[0]["SPL_ShopName"].ToString();
                    cmbShopName.SelectedValue = shop;
                    // 确定投料产线
                    DataView dv2 = new DataView(this.dtline);
                    dv2.RowFilter = string.Format("SPL_ShopName = '{0}'", shop);
                    dv2.Sort = "SPL_Name ASC";
                    cmbLineName.DisplayMember = "SPL_Name";
                    cmbLineName.ValueMember = "SPL_JobCode";
                    cmbLineName.DataSource = dv2;
                    cmbLineName.SelectedValue = BaseUI.UI_SPLJOBCODE;
                }
                else
                {
                    ShowResult(NoteState.Error, "", "未能获取本机IP所在产线!");
                }

                // 生产工单
                txtProductionOrder.Text = BaseUI.UI_SPOMJobCode;
                // 成品型号
                txtProductModel.Text = BaseUI.UI_SMSPEC;
                // 工单数量
                this.OrderNumber = BaseUI.GetOrderNumber(BaseUI.UI_SPOMJobCode);
                txtOrderNumber.Text = this.OrderNumber.ToString();
                // 投料数量和扫码数量
                BaseUI.GetFeedAndInputNumber(BaseUI.UI_SPOMJobCode, out this.FeedNumber, out this.InputNumber);
                txtFeedNumber.Text = this.FeedNumber.ToString();
                txtInputNumber.Text = this.InputNumber.ToString();
                // 已扫描批数
                this.ScanLotCount = 0;
                txtScanLotCount.Text = "0";
                // 提交待产工单提醒次数
                this.remindTimes = 0;
                // 获取物料清单
                GetMaterialCodeList();
                // 绑定待产工单选项
                this.dtProductionOrder = BaseUI.GetOrderTable(BaseUI.UI_SPLJOBCODE);
                DataView dvNextOrder = new DataView(this.dtProductionOrder);
                dvNextOrder.RowFilter = "SPOM_Status = '生成计划' OR SPOM_Status = '生产挂起'";
                cmbNextOrder.DisplayMember = "SPOM_JobCode";
                cmbNextOrder.ValueMember = "SPOM_Tid";
                cmbNextOrder.DataSource = dvNextOrder;

                // 确定是否已提交下一工单
                int nextOrderID = GetNextOrderID();
                if (nextOrderID != -1)
                {
                    cmbNextOrder.SelectedValue = nextOrderID;
                    cmbNextOrder.Enabled = false;
                    btnNextOrder.Enabled = false;
                }
                else
                {
                    cmbNextOrder.SelectedIndex = -1;
                    cmbNextOrder.Enabled = true;
                    btnNextOrder.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 获取下一工单ID
        /// </summary>
        /// <returns></returns>
        private int GetNextOrderID()
        {
            int id = -1;
            // 查询当前工单信息，查看是否已添加SPOM_NextOrder
            DataView dvMaterial = new DataView(this.dtProductionOrder);
            dvMaterial.RowFilter = string.Format("SPOM_JobCode = '{0}'", BaseUI.UI_SPOMJobCode);
            // 无当前工单信息，返回-1
            if (dvMaterial.Count == 0) return -1;
            // NextOrder ID
            id = YJ.Common.Utils.StrToInt(dvMaterial[0]["SPOM_NextOrder"], -1);

            if (id == -1) return id;

            // 查询下一工单的信息
            dvMaterial.RowFilter = string.Format("SPOM_Tid = {0}", id);
            // 无下一工单数据，返回-1
            if (dvMaterial.Count == 0) return -1;

            // 工单状态是否为生成计划或生产挂起
            string status = dvMaterial[0]["SPOM_Status"].ToString();
            if(status != "生成计划" && status != "生产挂起") return -1;

            // 配料状态是否为待配料，不是则更新配料状态
            string state = dvMaterial[0]["SPOM_MaterialState"].ToString();
            if (state != "待配料")
            {
                BaseUI.NextOrderCommit(BaseUI.UI_SPOMJobCode, id);
            }

            return id;
        }

        /// <summary>
        /// 获取物料清单
        /// </summary>
        private void GetMaterialCodeList()
        {
            // 获取物料列表
            DataView dv = BaseUI.GetMaterialTable(BaseUI.UI_SPOMJobCode);
            this.MaterialCodeList = new List<string>();
            foreach (DataRowView row in dv)
            {
                string code = row["SPOS_SMCode"].ToString();
                this.MaterialCodeList.Add(code);
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
                            break;

                        case NoteState.Error:
                            // 记录错误日志
                            LogHelper.Error(message, new Exception(message));
                            //Log4netProvider.Logger.Error(message);
                            // 弹框提示
                            // new FailMessage(message, 1000).ShowDialog(this);
                            this.failMsg = new FailMessage(message, 1000);
                            this.failMsg.ShowDialog(this);
                            break;
                    }
                    txtScanCode.Clear();
                    txtScanCode.Focus();
                    // 清除扫码枪串口接收缓冲区
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
                if (this.InvokeRequired)
                {
                    ScanDataHandlerCallback d = new ScanDataHandlerCallback(ScanDataHandler);
                    this.BeginInvoke(d, new object[] { DataString });
                }
                else
                {
                    // 第1步：站点基本信息检测
                    #region 第1步：站点基本信息检测
                    //if (string.IsNullOrWhiteSpace(txtLineName.Text))
                    //{
                    //    ShowResult(NoteState.NG, "", "获取设备所属产线失败");
                    //    return;
                    //}
                    if (string.IsNullOrWhiteSpace(txtOp.Text))
                    {
                        ShowResult(NoteState.NG, "", "获取扫描人员账号失败");
                        return;
                    }
                    if (this.siteType == "Delivery" && cmbShopName.SelectedIndex == -1)
                    {
                        ShowResult(NoteState.NG, "", "请先选择发往车间");
                        return;
                    }
                    #endregion 第1步：站点基本信息检测

                    // 第2步：解析扫描数据
                    #region 第2步：解析扫描数据
                    string AnalysisCode = DataString.Trim().Replace("\r", "").Replace("\n", "");  // 实际扫描数据
                    string[] ds = AnalysisCode.Split('|');
                    if (ds.Length != 6)
                    {
                        ShowResult(NoteState.NG, AnalysisCode, "请扫描正确的标签二维码！");
                        return;
                    }
                    txtScanCode.Text = AnalysisCode;

                    txtSN.Text = ds[0];
                    txtProduct.Text = ds[1];
                    txtModel.Text = ds[2];
                    txtOrder.Text = ds[3];
                    txtCount.Text = ds[4];
                    txtLine.Text = ds[5];

                    if (txtSN.Text.Length != 13)
                    {
                        ShowResult(NoteState.NG, txtSN.Text, "二维码数据有误，\n请重新扫描");
                        return;
                    }

                    #endregion 第2步：解析扫描数据


                    // 第3步：将数据保存至数据库
                    #region 第3步：将数据保存至数据库
                    // 检查批次状态
                    Utils.Model.TPL_Glass_Delivery sn = DAL.DAL_TPL_Glass_Delivery.GetModel(txtSN.Text);

                    // Cell车间发料
                    if (this.siteType == "Delivery")
                    {
                        //if (sn == null)
                        //{
                        //    ShowResult(NoteState.NG, txtSN.Text, "该批次状态查询失败.");
                        //    return;
                        //}

                        //string state = sn.GD_State;

                        //if (string.IsNullOrWhiteSpace(state))
                        //{
                        //    ShowResult(NoteState.NG, txtSN.Text, "该批次状态查询失败.");
                        //    return;
                        //}

                        if (sn != null)
                        {
                            // 序列号已存在，说明该批次已发料
                            ShowResult(NoteState.NG, txtSN.Text, "该批次已发料，请勿重复扫描。" + AnalysisCode);
                        }
                        else
                        {
                            // 添加发料信息
                            Utils.Model.TPL_Glass_Delivery lot = new Utils.Model.TPL_Glass_Delivery()
                            {
                                GD_SN = txtSN.Text,
                                GD_ProductCode = txtProduct.Text,
                                GD_ProductModel = txtModel.Text,
                                GD_ProductionOrder = txtOrder.Text,
                                GD_MaterialCount = YJ.Common.Utils.StrToInt(txtCount.Text,150),
                                GD_ProductionLine = txtLine.Text,
                                GD_State = "已发料",
                                GD_DeliveryShop = cmbShopName.Text,
                                GD_DeliveryLineCode = cmbLineName.SelectedValue.ToString(),
                                GD_DeliveryLine = cmbLineName.Text,
                                GD_DeliveryIP = txtIP.Text,
                                GD_DeliveryOP = txtOp.Text,
                                GD_DeliveryTime = DateTime.Now,
                                GD_CheckShop = "",
                                GD_CheckIP = "",
                                GD_CheckOP = "",
                                GD_CheckTime = new DateTime(1900,1,1),
                                GD_ReceiveDeviceIP = "",
                                GD_ReceiveShop = "",
                                GD_ReceiveLineCode = "",
                                GD_ReceiveLine = "",
                                GD_ReceiveOrder = "",
                                GD_ReceiveOP = "",
                                GD_ReceiveTime = new DateTime(1900, 1, 1)
                            };
                            if (DAL.DAL_TPL_Glass_Delivery.Add(lot))
                            {
                                // 更新计数
                                this.ScanLotCount += 1;
                                txtScanLotCount.Text = this.ScanLotCount.ToString();
                                ShowResult(NoteState.OK, txtSN.Text, "发料成功！" + AnalysisCode);
                            }
                            else
                            {
                                ShowResult(NoteState.NG, txtSN.Text, "发料失败，请重新扫描。" + AnalysisCode);
                            }
                        }
                    }
                    // 产线接收
                    else if (this.siteType == "Check")
                    {
                        string state = "未发料";
                        if (sn != null && !string.IsNullOrWhiteSpace(sn.GD_State))
                        {
                            state = sn.GD_State;
                        }

                        switch (state)
                        {
                            case "未发料":
                                // 漏录入投料信息，重新添加，投料人员为空
                                Utils.Model.TPL_Glass_Delivery lot = new Utils.Model.TPL_Glass_Delivery()
                                {
                                    GD_SN = txtSN.Text,
                                    GD_ProductCode = txtProduct.Text,
                                    GD_ProductModel = txtModel.Text,
                                    GD_ProductionOrder = txtOrder.Text,
                                    GD_MaterialCount = YJ.Common.Utils.StrToInt(txtCount.Text, 150),
                                    GD_ProductionLine = txtLine.Text,
                                    GD_State = "已收料",
                                    GD_DeliveryShop = cmbShopName.Text,
                                    GD_DeliveryLineCode = "",
                                    GD_DeliveryLine = "",
                                    GD_DeliveryOP = "",
                                    GD_DeliveryIP = "",
                                    GD_DeliveryTime = DateTime.Now,
                                    GD_CheckShop = cmbShopName.Text,
                                    GD_CheckOP = txtOp.Text,
                                    GD_CheckIP = txtIP.Text,
                                    GD_CheckTime = DateTime.Now,
                                    GD_ReceiveDeviceIP = "",
                                    GD_ReceiveOrder = "",
                                    GD_ReceiveShop = "",
                                    GD_ReceiveLineCode = "",
                                    GD_ReceiveLine = "",
                                    GD_ReceiveOP = "",
                                    GD_ReceiveTime = new DateTime(1900, 1, 1)
                                };
                                DAL.DAL_TPL_Glass_Delivery.Add(lot);
                                // 更新计数
                                this.ScanLotCount += 1;
                                txtScanLotCount.Text = this.ScanLotCount.ToString();
                                ShowResult(NoteState.OK, txtSN.Text, "收料成功！" + AnalysisCode);
                                break;
                            case "已发料":
                                // 发往车间与当前车间不一致
                                if (sn.GD_DeliveryShop != cmbShopName.Text)
                                {
                                    ShowResult(NoteState.Error, txtSN.Text, "该批次应发往" + sn.GD_DeliveryShop + ",\r\n收料后请联系Cell车间处理！");
                                }
                                // 有发料信息，更新收料信息
                                DAL.DAL_TPL_Glass_Delivery.CheckGlass(txtSN.Text, cmbShopName.Text, txtOp.Text, txtIP.Text);
                                // 更新计数
                                this.ScanLotCount += 1;
                                txtScanLotCount.Text = this.ScanLotCount.ToString();
                                ShowResult(NoteState.OK, txtSN.Text, "收料成功！" + AnalysisCode);
                                break;
                            default:
                                // 已收料
                                // 序列号已存在，说明该批次已投料
                                ShowResult(NoteState.NG, txtSN.Text, "该批次已收料，请勿重复扫描。" + AnalysisCode);
                                break;
                        }
                    }
                    // 机台投料
                    else if (this.siteType == "Receive")
                    {
                        #region 判断是否混料
                        if (!DAL.DAL_TPL_Glass_Delivery.MaterialCodeCheck(txtProduct.Text, txtProductionOrder.Text))
                        {
                            ShowResult(NoteState.NG, txtSN.Text, "该批次不属于当前工单物料,请确认是否混料！\r\n" + AnalysisCode);
                            return;
                        }
                        #endregion 判断是否混料

                        // 投料数量已到达工单数量
                        if (this.FeedNumber >= this.OrderNumber)
                        {
                            ShowResult(NoteState.NG, txtSN.Text, "当前工单投料已完成，请等待切换工单！");
                            return;
                        }
                        // 清洗投入数量已到达工单数量
                        else if (this.InputNumber >= this.OrderNumber)
                        {
                            ShowResult(NoteState.NG, txtSN.Text, "当前清洗投入数已达到工单数量，请切换至下一工单！");
                            return;
                        }
                        else if (YJ.Common.Utils.StrToInt(txtCount.Text, 0) > (this.OrderNumber - this.FeedNumber))
                        {
                            ShowResult(NoteState.NG, txtSN.Text, string.Format("当前工单仅需再投料{0}片！", (this.OrderNumber - this.FeedNumber)));
                            return;
                        }

                        //// 混线投料
                        //if (!string.IsNullOrEmpty(sn.GD_DeliveryLine) && sn.GD_DeliveryLine != this.cmbShopName.Text.ToString())
                        //{
                        //    DialogResult rst = MessageBox.Show(this, "该批次物料应发往 " + sn.GD_DeliveryLine + "，与当前产线不一致。\r\n是否继续投料？", "混线投料", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        //    if (rst == System.Windows.Forms.DialogResult.No)
                        //    {
                        //        return;
                        //    }
                        //}
                        string state = "未发料";
                        if (sn != null && !string.IsNullOrWhiteSpace(sn.GD_State))
                        {
                            state = sn.GD_State;
                        }

                        switch (state)
                        {
                            case "未发料":
                                // 漏录入发料信息，重新添加，发料收料人员为空
                                Utils.Model.TPL_Glass_Delivery lot = new Utils.Model.TPL_Glass_Delivery()
                                {
                                    GD_SN = txtSN.Text,
                                    GD_ProductCode = txtProduct.Text,
                                    GD_ProductModel = txtModel.Text,
                                    GD_ProductionOrder = txtOrder.Text,
                                    GD_MaterialCount = YJ.Common.Utils.StrToInt(txtCount.Text, 150),
                                    GD_ProductionLine = txtLine.Text,
                                    GD_State = "已投料",
                                    GD_DeliveryShop = cmbShopName.Text,
                                    GD_DeliveryLineCode = cmbLineName.SelectedValue.ToString(),
                                    GD_DeliveryLine = cmbLineName.Text,
                                    GD_DeliveryOP = "",
                                    GD_DeliveryIP = "",
                                    GD_DeliveryTime = DateTime.Now,
                                    GD_CheckShop = cmbShopName.Text,
                                    GD_CheckOP = "",
                                    GD_CheckIP = "",
                                    GD_CheckTime = DateTime.Now,
                                    GD_ReceiveDeviceIP = txtIP.Text,
                                    GD_ReceiveShop = cmbShopName.Text,
                                    GD_ReceiveLineCode = cmbLineName.SelectedValue.ToString(),
                                    GD_ReceiveLine = cmbLineName.Text,
                                    GD_ReceiveOrder = txtProductionOrder.Text,
                                    GD_ReceiveOP = txtOp.Text,
                                    GD_ReceiveTime = DateTime.Now
                                };
                                DAL.DAL_TPL_Glass_Delivery.Add(lot);
                                //// 更新投料数量
                                //DAL.DAL_TPL_Glass_Delivery.AddFeedNumber(lot.GD_ReceiveOrder, (int)lot.GD_MaterialCount, out this.FeedNumber, out this.InputNumber); 
                                //txtFeedNumber.Text = this.FeedNumber.ToString();
                                //txtInputNumber.Text = this.InputNumber.ToString();
                                //// 更新计数
                                //this.ScanLotCount += 1;
                                //txtScanLotCount.Text = this.ScanLotCount.ToString();
                                ShowResult(NoteState.OK, txtSN.Text, "投料成功！" + AnalysisCode);

                                break;

                            case "已发料":
                                if (sn.GD_DeliveryShop != cmbShopName.Text)
                                {
                                    ShowResult(NoteState.Error, txtSN.Text, "该批次应发往" + sn.GD_DeliveryShop + ",\r\n请联系Cell车间处理！");
                                    return;
                                }
                                // 有投料信息，更新收料信息和投料信息
                                DAL.DAL_TPL_Glass_Delivery.CheckAndReceiveGlass(txtSN.Text, txtOp.Text, cmbShopName.Text, cmbLineName.SelectedValue.ToString(), cmbLineName.Text, txtIP.Text, txtProductionOrder.Text);
                                //// 更新投料数量
                                //DAL.DAL_TPL_Glass_Delivery.AddFeedNumber(txtProductionOrder.Text, Convert.ToInt32(txtCount.Text), out this.FeedNumber, out this.InputNumber);
                                //txtFeedNumber.Text = this.FeedNumber.ToString();
                                //txtInputNumber.Text = this.InputNumber.ToString();
                                //// 更新计数
                                //this.ScanLotCount += 1;
                                //txtScanLotCount.Text = this.ScanLotCount.ToString();
                                ShowResult(NoteState.OK, txtSN.Text, "投料成功！" + AnalysisCode);
                                break;

                            case "已收料":
                                if (sn.GD_DeliveryShop != cmbShopName.Text)
                                {
                                    ShowResult(NoteState.Error, txtSN.Text, "该批次物料应发往" + sn.GD_DeliveryShop + ",\r\n请联系Cell车间处理！");
                                    return;
                                }
                                if (sn.GD_CheckShop != cmbShopName.Text)
                                {
                                    ShowResult(NoteState.Error, txtSN.Text, "该批次物料的收料车间是" + sn.GD_CheckShop + ",\r\n请联系Cell车间处理！");
                                    return;
                                }
                                // 有投料信息，更新收料信息和投料信息
                                DAL.DAL_TPL_Glass_Delivery.ReceiveGlass(txtSN.Text, txtOp.Text, cmbShopName.Text, cmbLineName.SelectedValue.ToString(), cmbLineName.Text, txtIP.Text, txtProductionOrder.Text);

                                ShowResult(NoteState.OK, txtSN.Text, "投料成功！" + AnalysisCode);
                                break;

                            default:
                                // 序列号已存在，说明该批次已投料
                                ShowResult(NoteState.NG, txtSN.Text, "该批次已投料，请勿重复扫描。" + AnalysisCode);
                                return;
                        }

                        // 更新投料数量
                        int count = YJ.Common.Utils.StrToInt(txtCount.Text, 0);
                        DAL.DAL_TPL_Glass_Delivery.AddFeedNumber(txtProductionOrder.Text, txtProduct.Text, count, out this.FeedNumber, out this.InputNumber);
                        txtFeedNumber.Text = this.FeedNumber.ToString();
                        txtInputNumber.Text = this.InputNumber.ToString();
                        // 更新计数
                        this.ScanLotCount += 1;
                        txtScanLotCount.Text = this.ScanLotCount.ToString();

                        if (this.FeedNumber >= this.OrderNumber * 0.5 && this.FeedNumber < this.OrderNumber * 0.8 && btnNextOrder.Enabled && this.remindTimes == 0)
                        {
                            WarnMessage remind = new WarnMessage("当前投料已过50%，\r\n请及时提交待产工单.", MessageBoxButtons.OK, 30, "OK");
                            remind.ShowDialog(this);
                            this.remindTimes = 1;
                        }
                        else if (this.FeedNumber >= this.OrderNumber * 0.8 && this.FeedNumber < this.OrderNumber && btnNextOrder.Enabled && this.remindTimes <= 1)
                        {
                            WarnMessage remind = new WarnMessage("当前投料已过80%，\r\n请及时提交待产工单.", MessageBoxButtons.OK, 30, "OK");
                            remind.ShowDialog(this);
                            this.remindTimes = 2;
                        }
                        else if (this.FeedNumber >= this.OrderNumber && btnNextOrder.Enabled && this.remindTimes <= 2)
                        {
                            WarnMessage remind = new WarnMessage("当前已完成投料，\r\n请及时提交待产工单.", MessageBoxButtons.OK, 30, "OK");
                            remind.ShowDialog(this);
                            this.remindTimes = 3;
                        }
                    }
                    #endregion 第3步：将数据保存至数据库
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据解析失败.", ex); 
                ShowResult(NoteState.Error, "", "数据解析失败." + ex.Message.ToString());
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
        #endregion 方法





    }
}
