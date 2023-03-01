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
using Utils;
using Utils.HBaseClass;

namespace MDC
{
    public partial class frmReview : Form
    {
        #region 私有字段
        ///// <summary>
        ///// HBase数据库操作类
        ///// </summary>
        //private GetHBaseDataClass GHDC;
        /// <summary>
        /// 扫描码查询的已过工序信息表
        /// </summary>
        private DataTable processData;
        /// <summary>
        /// 玻璃信息查询超时时间
        /// </summary>
        private int searchTimeout;
        /// <summary>
        /// 本机IP
        /// </summary>
        private string ipAddress = "";
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;   
        /// <summary>
        /// 成功提示框
        /// </summary>
        private SuccessMessage successMsg;
        /// <summary>
        /// 玻璃不良申报信息对象
        /// </summary>
        Utils.Model.TPL_ProcessFail_Main ProcessFailInfo = null;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        #endregion 私有字段

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmReview()
        {
            InitializeComponent();

            txtProcessIP.Dock = DockStyle.Fill;
            txtCurrentLineName.Dock = DockStyle.Fill;
            txtCurrentOpCode.Dock = DockStyle.Fill;
            txtScanCode.Dock = DockStyle.Fill;
            txtLCD.Dock = DockStyle.Fill;
            txtProcess.Dock = DockStyle.Fill;
            txtOp.Dock = DockStyle.Fill;
            txtTime.Dock = DockStyle.Fill;
            txtProductOrder.Dock = DockStyle.Fill;
            txtFinishesModel.Dock = DockStyle.Fill;
            txtLineCode.Dock = DockStyle.Fill;
            txtDescribe.Dock = DockStyle.Fill;
            txtError.Dock = DockStyle.Fill;

            txtProcessIP.Clear();
            txtCurrentLineName.Clear();
            txtCurrentOpCode.Clear();
            txtScanCode.Clear();
            txtFinishesModel.Clear();
            txtLCD.Clear();
            txtProcess.Clear();
            txtOp.Clear();
            txtTime.Clear();
            txtProductOrder.Clear();
            txtLineCode.Clear();
            txtDescribe.Clear();
            txtError.Clear();

            lblType.Tag = "重工";

            flpNG.Controls.Clear();

        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmReview_Load(object sender, EventArgs e)
        {
            try
            {
                // 当前程序版本
                this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                // 设置程序标题
                this.Text = string.Format("不良复判   版本:{0}", this.versionName);
                //// 打开扫描枪串口
                //OpenDeviceCom();
                // 绑定串口数据接收事件处理程序
                Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;
                // 程序初始化
                Initialize();
            }
            catch (Exception ex)
            {
                ShowResult(NoteState.Error, "", "程序初始化失败!" + ex.Message);
            }
        }
        private void frmReview_Shown(object sender, EventArgs e)
        {
            txtScanCode.Focus();
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmReview_FormClosing(object sender, FormClosingEventArgs e)
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
        private void frmReview_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
            this.flpNG.Refresh();
        }

        /// <summary>
        /// 程序初始化
        /// </summary>
        private void Initialize()
        {
            // 玻璃信息查询超时时间
            searchTimeout = appConfig.GetConfigInt("SearchTimeout");

            // 登录人员账号
            txtCurrentOpCode.Text = BaseUI.UI_BOOLOGNAME;

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
                ShowResult(NoteState.Error, "", ex.Message.ToString());
            }

            // 产线名称
            txtCurrentLineName.Text = BaseUI.UI_SPLNAME;
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
                            break;

                        case NoteState.NG:
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 1000);
                            this.failMsg.ShowDialog(this);
                            break;

                        case NoteState.Error:
                            // 记录错误日志
                            LogHelper.Error(message,new Exception(message));
                            // 弹框提示
                            this.failMsg = new FailMessage(message, 1000);
                            this.failMsg.ShowDialog(this);
                            break;
                    }
                    txtScanCode.Clear();
                    txtLCD.Clear();
                    txtProcess.Clear();
                    txtOp.Clear();
                    txtTime.Clear();
                    txtProductOrder.Clear();
                    txtFinishesModel.Clear();
                    txtLineCode.Clear();
                    txtDescribe.Clear();
                    txtError.Clear();
                    flpNG.Controls.Clear();

                    lblType.Tag = "重工";
                    btnRework.BackColor = Color.Transparent;
                    btnScrap.BackColor = Color.Transparent;
                    btnErrJudge.BackColor = Color.Transparent;

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
        //    catch (Exception ex)
        //    {
        //        ShowResult(NoteState.Error, "", "串口" + Program.ScanDevice.PortName + "打开失败!" + ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 扫描枪接收数据
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ScanDevice_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
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
        //    catch (Exception ex)
        //    {
        //        ShowResult(NoteState.Error, Program.ScanDevice.PortName, "扫描设备接收数据失败." + ex.Message);
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
                ShowResult(NoteState.Error, Program.ScanDevice.PortName, "扫描设备接收数据失败." + exp.Message);
            }
        }
        /// <summary>
        /// 实际扫描数据文本框按下回车键，开始处理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFPC_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtError.Text = "";

                    #region 扫描数据解析
                    //编码规则解析并填充文本框
                    txtScanCode.Text = DataString.Trim();  // 实际扫描数据

                    if (DataString.Contains("OK:") || DataString.Contains("ERROR:"))
                    {
                        ShowResult(NoteState.Error, "", "编码不符合规则，请确认扫描枪是否打开了测试模式");
                        return;
                    }

                    string[] ds = DataString.Split(':');
                    if (ds.Length == 0)
                    {
                        ShowResult(NoteState.Error, "", "编码不符合规则");
                        return;
                    }
                    string AnalysisCode = ds[0].ToString().Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");    //扫描数据解析
                    if (ds[0].Length <= 9)
                    {
                        ShowResult(NoteState.Error, "", "默认编码长度至少为10位以上，请确认条码长度");
                        return;
                    }

                    string scanCode = AnalysisCode.Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");
                    txtScanCode.Text = scanCode;
                    #endregion 扫描数据解析

                    #region 查询玻璃信息
                    // 局部变量
                    string productionOrder = "";//生产订单
                    string lineCode = "";//产线编码
                    string glassCode = "";//玻璃码
                    string finishesModel = "";//成品规格型号
                    this.processData = null;

                    try
                    {
                        //this.processData = GHDC.GetProductionRouteByCode(DataString);//Code1查询结果
                        this.processData = null;
                        CallWithTimeout(GetProcessData1, this.searchTimeout);

                        if (this.processData == null)
                        {
                            ShowResult(NoteState.NG, DataString, "内部查询玻璃绑定信息失败,如多次出现此错误，请关掉程序重新打开");
                            return;
                        }

                        if (this.processData.DefaultView.Count > 0)
                        {
                            DataRowView rv = this.processData.DefaultView[0];
                            if (rv["Process_LCDCode"].ToString() != "")
                            {
                                productionOrder = rv["Process_productionOrder"].ToString();//生产订单
                                lineCode = rv["Process_productLineCode"].ToString();//产线编码
                                glassCode = rv["Process_LCDCode"].ToString();//玻璃码
                                finishesModel = rv["Process_finishesModel"].ToString();//成品规格型号
                            }
                            rv = null;
                        }
                        else
                        {
                            ShowResult(NoteState.Error, DataString, "未能查询到玻璃信息.");
                            return;
                        }
                    }
                    catch (Exception exp)
                    {
                        ShowResult(NoteState.Error, DataString, "查询玻璃绑定信息失败." + exp.Message);
                        return;
                    }

                    #endregion 查询玻璃信息

                    #region 查询玻璃不良信息
                    try
                    {
                        ProcessFailInfo = BaseUI.GetProcessFail_Main(glassCode);
                    }
                    catch (Exception exp)
                    {
                        ShowResult(NoteState.Error, "", "查询玻璃不良申报信息失败！" + exp.Message);
                        return;
                    }
                    // 判断是否已申报不良
                    if (ProcessFailInfo == null)
                    {
                        ShowResult(NoteState.Error, "", "该玻璃未申报不良！");
                        return;
                    }
                    // 判断是否混线，与当前产线不一致
                    if (ProcessFailInfo.TFM_LineCode != null && ProcessFailInfo.TFM_LineCode.Length >= 2 && ProcessFailInfo.TFM_LineCode != BaseUI.UI_SPLJOBCODE)
                    {
                        ShowResult(NoteState.Error, "", "该玻璃生产产线为" + ProcessFailInfo.TFM_LineCode.Substring(0,2) + "线，与当前产线不一致！");
                        return;
                    }
                    // 工单不一致
                    if (productionOrder != this.ProcessFailInfo.TFM_ProductOrder)
                    {
                        ShowResult(NoteState.Error, "", "该玻璃生产工单与不良申报信息中的工单不一致！");
                        return;
                    }
                    // 产线不一致
                    if (lineCode != this.ProcessFailInfo.TFM_LineCode)
                    {
                        ShowResult(NoteState.Error, "", "该玻璃生产产线与不良申报信息中的产线不一致！");
                        return;
                    }
                    // 填充数据
                    // 玻璃码
                    txtLCD.Text = this.ProcessFailInfo.TFM_GlassCode;
                    // 提报站点工序名
                    txtProcess.Text = BaseUI.GetProcessName(this.ProcessFailInfo.TFM_ProcessCode);
                    // 工单编码
                    txtProductOrder.Text = this.ProcessFailInfo.TFM_ProductOrder;
                    // 成品型号
                    txtFinishesModel.Text = finishesModel;
                    // 产线编码
                    txtLineCode.Text = this.ProcessFailInfo.TFM_LineCode;
                    // 异常描述
                    txtDescribe.Text = this.ProcessFailInfo.TFM_Describe;

                    // 判断是否已复判
                    if (this.ProcessFailInfo.TFM_Status == "正操作")
                    {
                        lblOP.Text = "提报人员:";
                        lblTime.Text = "提报时间:";
                        string opName = BaseUI.GetOpName(this.ProcessFailInfo.TFM_AddPid == null ? null : this.ProcessFailInfo.TFM_AddPid.ToString());
                        txtOp.Text = opName;
                        txtTime.Text = ((DateTime)this.ProcessFailInfo.TFM_AddDate).ToString("yyyy-MM-dd HH:mm:ss");
                        txtError.Text = "";
                        //txtFPC.SelectAll();
                        //txtFPC.Focus();
                    }
                    else
                    {
                        lblOP.Text = "复判人员:";
                        lblTime.Text = "复判时间:";
                        string opName = BaseUI.GetOpName(this.ProcessFailInfo.TFM_CheckPid == null ? null : this.ProcessFailInfo.TFM_CheckPid.ToString());
                        txtOp.Text = opName;
                        txtTime.Text = ((DateTime)this.ProcessFailInfo.TFM_CheckDate).ToString("yyyy-MM-dd HH:mm:ss");

                        txtError.Text = "此玻璃已复判！";
                        //txtFPC.SelectAll();
                        //txtFPC.Focus();
                        //return;
                    }

                    // 设置异常类型
                    switch (this.ProcessFailInfo.TFM_Type)
                    {
                        case "重工":
                            lblType.Tag = "重工";
                            btnRework.BackColor = Color.Yellow;
                            btnScrap.BackColor = Color.Transparent;
                            btnErrJudge.BackColor = Color.Transparent;
                            break;
                        case "报废":
                            lblType.Tag = "报废";
                            btnRework.BackColor = Color.Transparent;
                            btnScrap.BackColor = Color.Yellow;
                            btnErrJudge.BackColor = Color.Transparent;
                            break;
                        case "误判":
                            lblType.Tag = "误判";
                            btnRework.BackColor = Color.Transparent;
                            btnScrap.BackColor = Color.Transparent;
                            btnErrJudge.BackColor = Color.Yellow;
                            break;
                    }
                    btnRework.Tag = btnRework.BackColor;
                    btnScrap.Tag = btnScrap.BackColor;
                    btnErrJudge.Tag = btnErrJudge.BackColor;

                    //Set_TFM_Type(this.ProcessFailInfo.TFM_Type);

                    #endregion 查询玻璃是否已提交不良信息


                    #region 添加不良项列表
                    this.flpNG.Controls.Clear();
                    Dictionary<int, string> BNCList = BaseUI.GetBNCList(this.ProcessFailInfo.TFM_ProcessCode);
                    int width = 0;
                    foreach (int key in BNCList.Keys)
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
                            Name = key.ToString(),
                            Text = BNCList[key],
                            TextAlign = ContentAlignment.MiddleCenter 
                        };
                        this.flpNG.Controls.Add(chk);
                        chk.CheckStateChanged += this.BNC_CheckStateChanged;
                        if (chk.Width > width)
                        {
                            width = chk.Width;
                        }
                    }
                    foreach (CheckBox chk in this.flpNG.Controls)
                    {
                        int height = chk.Height;
                        chk.AutoSize = false;
                        chk.Width = width;
                        chk.Height = height;
                    }
                    // 标记提报的不良子项
                    Dictionary<int, string> FailSub = BaseUI.GetProcessFail_Sub(this.ProcessFailInfo.TFM_Tid);
                    int idx = 0;
                    foreach (int key in FailSub.Keys)
                    {
                        if(this.flpNG.Controls.ContainsKey(key.ToString()))
                        {
                            CheckBox chk = this.flpNG.Controls[key.ToString()] as CheckBox;
                            chk.BackColor = Color.Yellow;
                            this.flpNG.Controls.SetChildIndex(chk, idx);
                            idx++;
                        }
                    }

                    #endregion 添加不良项列表

                    txtScanCode.SelectAll();
                    txtScanCode.Focus();
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
                txtError.Text = ex.Message;
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
                item.Tag = item.BackColor;
                item.BackColor = Color.Red;
            }
            else
            {
                if (item.Tag != null)
                {
                    item.BackColor = (Color)item.Tag;
                }
                else
                {
                    item.BackColor = Color.Transparent;
                }
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
                    btnScrap.BackColor = (Color)btnScrap.Tag;
                    btnErrJudge.BackColor = (Color)btnErrJudge.Tag;
                    break;
                case "报废":
                    lblType.Tag = "报废";
                    btnRework.BackColor = (Color)btnRework.Tag;
                    btnScrap.BackColor = Color.Red;
                    btnErrJudge.BackColor = (Color)btnErrJudge.Tag;
                    break;
                case "误判":
                    lblType.Tag = "误判";
                    btnRework.BackColor = (Color)btnRework.Tag;
                    btnScrap.BackColor = (Color)btnScrap.Tag;
                    btnErrJudge.BackColor = Color.Red;
                    break;
            }
        }


        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            txtError.Clear();
            if (txtLCD.Text == "")
            {
                txtError.Text = "请先扫描编码，\r\n获取不良申报信息！";
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }

            if (txtDescribe.Tag == null)
            {
                txtError.Text = "请判定不良项！";
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                txtScanCode.SelectAll();
                txtScanCode.Focus();
                return;
            }

            if (this.ProcessFailInfo.TFM_Status == "已复判")
            {
                ShowResult(NoteState.NG, "", "此玻璃已复判，\r\n请勿重复提交！");
                return;
            }

            string type = "重工";
            if (lblType.Tag != null)
            {
                type = lblType.Tag.ToString();
            }
            btnOK.Enabled = false;
            btnCancel.Enabled = false;
            txtError.Text = "正在提交，请稍候...";

            // 数据提交后台处理
            bgwWriteData.RunWorkerAsync(new object[] { this.ProcessFailInfo.TFM_Tid, type, txtDescribe.Tag.ToString(), txtDescribe.Text, YJ.Common.TypeConverter.StrToInt(BaseUI.UI_BOOPID) });
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
                int tfmtid = YJ.Common.TypeConverter.ObjectToInt(val[0]);
                string type = val[1].ToString();
                string bncid = val[2].ToString();
                string bncname = val[3].ToString();
                int pid = YJ.Common.TypeConverter.ObjectToInt(val[4]);

                // 调用存储过程，提交数据
                bool rst = BaseUI.Check_ProcessFail(tfmtid, type, bncid, bncname, pid);
                if (rst)
                {
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
                ShowResult(NoteState.OK, "", "操作成功！");
                //this.NGDesc = txtDescribe.Text;
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                Exception ex = e.Result as Exception;
                txtError.Text = "提交失败，请重试！\r\n" + ex.Message;
                txtScanCode.SelectAll();
                txtScanCode.Focus();
            }
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtScanCode.Clear();
            txtLCD.Clear();
            txtProcess.Clear();
            txtOp.Clear();
            txtTime.Clear();
            txtProductOrder.Clear();
            txtLineCode.Clear();
            txtDescribe.Clear();
            txtError.Clear();
            flpNG.Controls.Clear();

            lblType.Tag = "重工";
            btnRework.BackColor = Color.Transparent;
            btnScrap.BackColor = Color.Transparent;
            btnErrJudge.BackColor = Color.Transparent;
            txtScanCode.Focus();

            // 清除扫码枪串口接收缓冲区
            Program.ScanDevice.DiscardBuffer();
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
            this.processData = GetHBaseDataClass.Instance.GetProductionRouteByCode(txtScanCode.Text);//Code1查询结果
        }

    }
}
