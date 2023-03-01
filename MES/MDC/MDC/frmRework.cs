using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Utils.HBaseClass;
using Utils;

namespace MDC
{
    public partial class frmRework : Form
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
        /// 产线编码
        /// </summary>
        private string lineCode;
        /// <summary>
        /// 错误提示
        /// </summary>
        FailMessage failMsg;
        /// <summary>
        /// 成功提示
        /// </summary>
        SuccessMessage successMsg;
        /// <summary>
        /// 玻璃信息DataSet
        /// </summary>
        DataSet dsGlassInfo = null;
        /// <summary>
        /// 玻璃编码
        /// </summary>
        private string GlassCode;

        public frmRework()
        {
            InitializeComponent();
            txtCode.Dock = DockStyle.Fill;
            btnSearch.Dock = DockStyle.Fill;
            tlpInfo.Dock = DockStyle.Fill;
            txtSPLName.Dock = DockStyle.Fill;
            txtIPAddress.Dock = DockStyle.Fill;
            txtOpCode.Dock = DockStyle.Fill;
            btnOK.Dock = DockStyle.Fill;
            txtOrderCode.Dock = DockStyle.Fill;
            txtModelCode.Dock = DockStyle.Fill;
            comProcess.Dock = DockStyle.Fill;
            btnSearch.Dock = DockStyle.Fill;

            txtOrderCode.Clear();
            txtModelCode.Clear();
            txtSPLName.Clear();
            lvwProcess.Items.Clear();
            txtOpCode.Clear();
            comProcess.Items.Clear();
            txtIPAddress.Clear();

            btnOK.Enabled = false;
        }

        private void frmRework_Load(object sender, EventArgs e)
        {
            try
            {
                // 当前程序版本
                this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                this.Text = string.Format("重工解绑   {0}", versionName);

                // 登录人员账号
                txtOpCode.Text = BaseUI.UI_BOOLOGNAME;
                // 当前站点IP
                string ipAddress = BaseUI.GetLocalIP();

                txtIPAddress.Text = ipAddress;

                // 初始化产线选择下拉框
                DataView dvLine = BaseUI.GetProcedureLine();
                cmbSPLName.DisplayMember = "SPL_Name";
                cmbSPLName.ValueMember = "SPL_JobCode";
                cmbSPLName.DataSource = dvLine;

                // 绑定串口数据接收事件处理程序
                Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;

                this.Refresh();
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
            }
        }

        private void frmRework_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 解除绑定串口数据接收事件处理程序
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
        }

        /// <summary>
        /// 编码输入框按下回车键，执行查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter || txtCode.Text.Trim() == "") return;
            string DataString = txtCode.Text.Trim().Replace("\r", "").Replace("\n", "");
            // 分析处理数据
            ScanDataHandler(DataString);
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() == "") return;
            string DataString = txtCode.Text.Trim().Replace("\r", "").Replace("\n", "");
            // 分析处理数据
            ScanDataHandler(DataString);
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
                // 分析处理数据
                ScanDataHandler(DataString);
            }
            catch (Exception exp)
            {
                LogHelper.Error(exp.Message, exp);
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
                    btnOK.Enabled = false;
                    btnSearch.Enabled = false;
                    txtCode.Enabled = false;

                    #region 扫描数据解析
                    //编码规则解析并填充文本框
                    txtCode.Text = DataString.Trim();  // 实际扫描数据

                    if (DataString.Contains("OK:") || DataString.Contains("ERROR:"))
                    {
                        failMsg = new FailMessage("编码不符合规则，请确认扫描枪是否打开了测试模式", 999999);
                        failMsg.ShowDialog(this);
                        return;
                    }

                    string[] ds = DataString.Split(':');
                    if (ds.Length == 0)
                    {
                        //ShowResult(NoteState.Error, "", "编码不符合规则");
                        //return;
                        failMsg = new FailMessage("编码不符合规则", 999999);
                        failMsg.ShowDialog(this);
                        return;
                    }
                    string AnalysisCode = ds[0].ToString().Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");    //扫描数据解析
                    if (AnalysisCode.Length <= 9)
                    {
                        //ShowResult(NoteState.Error, "", "默认编码长度至少为10位以上，请确认条码长度");
                        //return;
                        failMsg = new FailMessage("默认编码长度至少为10位以上，请确认条码长度", 999999);
                        failMsg.ShowDialog(this);
                        return;
                    }

                    txtCode.Text = AnalysisCode;

                    #endregion 扫描数据解析

                    #region 查询玻璃信息
                    try
                    {
                        txtOrderCode.Clear();
                        txtModelCode.Clear();
                        lvwProcess.Items.Clear();
                        //异步查询txtCode过站信息
                        CallWithTimeout(GetProcessData, 1000);
                        //查询玻璃信息
                        GetGlassData();

                        // 混线生产
                        if (lineCode != "" && lineCode.Length >= 2 && lineCode != cmbSPLName.SelectedValue.ToString())
                        {
                            string LineName = BaseUI.UI_SPLNAME;
                            failMsg = new FailMessage("玻璃生产产线为" + lineCode.Substring(0, 2) + "线，与当前产线不一致。", 999999);
                            failMsg.ShowDialog(this);
                            return;
                        }

                        string sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Sub WITH (NOLOCK) WHERE IBS_GlassCode='{0}'", GlassCode);
                        DataView dv = conn.ExecuteDataView(sql, "Base");
                        if (dv.Count > 0)
                        {
                            failMsg = new FailMessage("当前玻璃已包装，无法重工解绑", 999999);
                            failMsg.ShowDialog(this);
                            return;
                        }
                        sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Small_Sub WITH (NOLOCK) WHERE ISS_GlassCode='{0}'", GlassCode);
                        dv = conn.ExecuteDataView(sql, "Base");
                        if (dv.Count > 0)
                        {
                            failMsg = new FailMessage("当前玻璃已包装，无法重工解绑", 999999);
                            failMsg.ShowDialog(this);
                            return;
                        }

                        #region 不良信息

                        string strSql = string.Format("SELECT TFM_GlassCode, TFM_Status, TFM_Type, BNC_RepairSGX FROM TPL_ProcessFail_Detail_View WITH (NOLOCK) WHERE (TFM_ScanCode = '{0}' OR TFM_GlassCode = '{0}') ORDER BY TFM_AddDate DESC, BNC_RepairSGX ASC", GlassCode);
                        DataView dvNG = conn.ExecuteDataView(strSql, "Base");
                        if (dvNG == null || dvNG.Count == 0)
                        {
                            failMsg = new FailMessage("当前玻璃未申报不良，无法重工解绑", 999999);
                            failMsg.ShowDialog(this);
                        }
                        else if (dvNG[0]["TFM_Status"].ToString() == "正操作")
                        {
                            failMsg = new FailMessage("当前玻璃尚未不良复判，无法重工解绑", 999999);
                            failMsg.ShowDialog(this);
                        }
                        else
                        {
                            if (dvNG[0]["TFM_Type"].ToString() == "误判")
                            {
                                failMsg = new FailMessage("当前玻璃复判为良品，无法重工解绑", 999999);
                                failMsg.ShowDialog(this);
                            }
                            //-------------------------------------------------------------------------
                            //2019-07-03 V1.4.2019.703 复判报废品允许解绑，以回收TP，背光等部件
                            //-------------------------------------------------------------------------
                            //else if (dvNG[0]["TFM_Type"].ToString() == "报废")
                            //{
                            //    failMsg = new FailMessage("当前玻璃复判为报废，无法重工解绑", 999999);
                            //    failMsg.ShowDialog(this);
                            //}
                            else
                            {
                                string glassCode = dvNG[0]["TFM_GlassCode"].ToString();
                                strSql = string.Format("SELECT TOP 1 * FROM HB_Product_RepeatLog WITH(NOLOCK) WHERE PRL_GlassCode = '{0}' ORDER BY PRL_Tid DESC", glassCode);
                                DataView dvRework = conn.ExecuteDataView(strSql, "Base");
                                if (dvRework.Count > 0)
                                {
                                    failMsg = new FailMessage("当前玻璃已解绑，请勿重复扫描", 999999);
                                    failMsg.ShowDialog(this);
                                }
                                else
                                {
                                    string reProcess = dvNG[0]["BNC_RepairSGX"].ToString().PadLeft(3, '0');
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(reProcess))
                                        {
                                            comProcess.SelectedValue = reProcess;
                                            if (comProcess.SelectedIndex != -1)
                                            {
                                                comProcess.Enabled = false;
                                            }
                                            else
                                            {
                                                comProcess.Enabled = true;
                                            }
                                        }
                                        else
                                        {
                                            comProcess.Enabled = true;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        comProcess.Enabled = true;
                                    }
                                    finally
                                    {
                                        btnOK.Enabled = true;
                                    }
                                }
                            }
                        }

                        #endregion 不良信息

                    }
                    catch (TimeoutException tex)
                    {
                        GetHBaseDataClass.Instance.Reconnect();
                        LogHelper.Error("HBase数据库连接超时.|" + tex.Message, tex);
                    }
                    #endregion 查询玻璃信息
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
            }
            finally
            {
                //btnOK.Enabled = true;
                btnSearch.Enabled = true;
                txtCode.Enabled = true;
                txtCode.Focus();
                txtCode.SelectAll();
            }
        }

        /// <summary>
        /// 获取Code1的过站信息
        /// </summary>
        private void GetProcessData()
        {
            dsGlassInfo = null;
            dsGlassInfo = GetHBaseDataClass.Instance.GetDataTableBySingleGlass(txtCode.Text.Trim());//Code1查询结果
        }
        /// <summary>
        /// 从HBase数据库查询编码对应的玻璃信息
        /// </summary>
        /// <param name="code">查询编码</param>
        private void GetGlassData()
        {
           
            DataView dvProduct = null;
            DataView dvParameter = null;
            DataView dvChengPin = null;
            //string str = "";

            try
            {
                //ds = GHDC.GetDataTableBySingleGlass(str);//Code查询结果
                if (dsGlassInfo != null && dsGlassInfo.Tables.Count > 0)
                {
                    // 产线工单信息
                    dvProduct = dsGlassInfo.Tables["productTable"].DefaultView;

                    // 工序过站信息
                    DataView dv = dsGlassInfo.Tables["parameterTable"].DefaultView;
                    dv.Sort = "parameter_ProcessNumber Asc";
                    DataTable dt = dv.ToTable(true, new string[] { "parameter_ProcessNumber", "parameter_LineCode", "parameter_ProcessName", "parameter_DateTime", "parameter_CardIP", "parameter_trackNumber", "parameter_oppositeNumber", "parameter_bfFlag" });
                    dvParameter = dt.DefaultView;
                    GlassCode = dvParameter[0]["parameter_trackNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("获取玻璃信息失败.|" + ex.Message, ex);
                //YJ.Log.FileLog.log.Error("获取玻璃信息失败.|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + code + "|" + ex.Message.ToString());
            }
            finally
            {
                ShowInfo(dvProduct, dvParameter, dvChengPin);
            }
        }

        /// <summary>
        /// 跨线程展示玻璃信息的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ShowInfoCallback(DataView dvProduct, DataView dvParameter, DataView dvChengPin);
        /// <summary>
        /// 展示玻璃信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="glassData"></param>
        private void ShowInfo(DataView dvProduct, DataView dvParameter, DataView dvChengPin)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                ShowInfoCallback d = new ShowInfoCallback(ShowInfo);
                this.Invoke(d, new object[] { dvProduct, dvParameter, dvChengPin });
            }
            else
            {
                try
                {
                    //产线、生产订单等信息
                    if (dvProduct != null && dvProduct.Count > 0)
                    {
                        // 生产工单
                        txtOrderCode.Text = dvProduct[0]["product_ProductOrder"].ToString();
                        // 成品型号
                        txtModelCode.Text = dvProduct[0]["product_finishesModel"].ToString();

                        lineCode = dvProduct[0]["product_LineCode"].ToString();

                        BindComboBoxData(dvProduct[0]["product_ProductOrder"].ToString(), dvProduct[0]["product_LineCode"].ToString());
                    }

                    //工序过站信息
                    if (dvParameter != null && dvParameter.Count > 0)
                    {
                        foreach (DataRowView row in dvParameter)
                        {
                            ListViewItem item = lvwProcess.Items.Add(row["parameter_ProcessNumber"].ToString());
                            item.SubItems.AddRange(new string[]{
                                row["parameter_LineCode"].ToString(), 
                                row["parameter_ProcessName"].ToString(), 
                                row["parameter_DateTime"].ToString(), 
                                row["parameter_CardIP"].ToString(), 
                                row["parameter_bfFlag"].ToString(),
                                row["parameter_trackNumber"].ToString(), 
                                row["parameter_oppositeNumber"].ToString() 
                            });
                        }

                        for (int i = 0; i < lvwProcess.Columns.Count; i++)
                        {
                            if (i <= 1)
                            {
                                lvwProcess.Columns[i].Width = -2;
                            }
                            else
                            {
                                lvwProcess.Columns[i].Width = -1;
                            }
                        }
                        lvwProcess.Columns[5].Width = 45;
                    }


                }
                catch (Exception ex)
                {
                    LogHelper.Error("获取玻璃信息失败.|" + ex.Message, ex);
                }
                finally
                {
                    txtCode.Enabled = true;
                    txtCode.Focus();
                    txtCode.SelectAll();
                }
            }

        }
        private void BindComboBoxData(string ProductOrder, string LineCode)
        {
            string sql = string.Format("SELECT SGX_Name, SGX_JobCode FROM ProduceOrder_Route_CarIP_VW WHERE  SPOM_JobCode='{0}' AND SPL_JobCode ='{1}' AND SGX_JobCode IN('005', '011', '018', '024', '029', '032', '034') ORDER BY  PR_NoSeq ASC", ProductOrder, LineCode);
            DataView dv = conn.ExecuteDataView(sql, "Base");
            comProcess.DisplayMember = "SGX_Name";
            comProcess.ValueMember = "SGX_JobCode";
            comProcess.DataSource = dv;
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            if (lvwProcess.SelectedItems.Count == 0) return;
            ListViewItem item = lvwProcess.SelectedItems[0];
            string content = "";
            for (int i = 0; i < item.SubItems.Count; i++)
            {
                content += item.SubItems[i].Text + "    ";
            }
            Clipboard.SetDataObject(content);
        }

        private void frmRework_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
            txtCode.Focus();
            txtCode.SelectAll();
        }
        private void frmRework_Shown(object sender, EventArgs e)
        {
            txtCode.Focus();
            txtCode.SelectAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //// 混线生产
            //if (lineCode != "" && lineCode.Length >= 2 && lineCode != cmbSPLName.SelectedValue.ToString())
            //{
            //    string LineName = BaseUI.UI_SPLNAME;
            //    failMsg = new FailMessage("玻璃生产产线为" + lineCode.Substring(0,2) + "线，与当前产线不一致。", 999999);
            //    failMsg.ShowDialog(this);
            //    return;
            //}

            //#region 不良玻璃检测
            //FailReviewResult reviewResult = BaseUI.GetFailReviewResult(GlassCode);
            //switch (reviewResult)
            //{
            //    case FailReviewResult.未申报不良:
            //        failMsg = new FailMessage("当前玻璃未申报不良，无法重工解绑", 999999);
            //        failMsg.ShowDialog(this);
            //        return;

            //    case FailReviewResult.已申报未复判:
            //        failMsg = new FailMessage("当前玻璃尚未不良复判，无法重工解绑", 999999);
            //        failMsg.ShowDialog(this);
            //        return;

            //    //case FailReviewResult.复判报废:
            //    //    failMsg = new FailMessage("当前玻璃已复判报废，无法重工解绑", 999999);
            //    //    failMsg.ShowDialog(this);
            //    //    return;

            //    case FailReviewResult.复判良品:
            //        failMsg = new FailMessage("当前玻璃复判为良品，无法重工解绑", 999999);
            //        failMsg.ShowDialog(this);
            //        return;

            //    default:
            //        break;
            //}

            //#endregion 不良玻璃检测

            ////string sql = string.Format("SELECT   *  FROM   TPL_ProcessFail_Main  WHERE   TFM_GlassCode='{0}'", GlassCode);
            ////DataView dv = conn.ExecuteDataView(sql, "Base");
            ////if (dv.Count == 0)
            ////{
            ////    failMsg = new FailMessage("当前玻璃尚未不良提报，无法重工解绑", 999999);
            ////    failMsg.ShowDialog(this);
            ////    return;
            ////}
            //string sql = string.Format("SELECT   *  FROM   TPL_PackingInnerBox_Sub  WHERE IBS_GlassCode='{0}'", GlassCode);
            //DataView dv = conn.ExecuteDataView(sql, "Base");
            //if (dv.Count > 0)
            //{
            //    failMsg = new FailMessage("当前玻璃已包装，无法重工解绑", 999999);
            //    failMsg.ShowDialog(this);
            //    return;
            //}
            //sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Small_Sub WHERE ISS_GlassCode='{0}'", GlassCode);
            //dv = conn.ExecuteDataView(sql, "Base");
            //if (dv.Count > 0)
            //{
            //    failMsg = new FailMessage("当前玻璃已包装，无法重工解绑", 999999);
            //    failMsg.ShowDialog(this);
            //    return;
            //}
            try
            {
                string Result = GetHBaseDataClass.Instance.RepeatProcessFromHbase(txtCode.Text.Trim(), comProcess.SelectedValue.ToString());
                if (Result == "")
                {
                    this.successMsg = new SuccessMessage("操作成功", 2);
                    this.successMsg.ShowDialog(this);
                    txtOrderCode.Clear();
                    txtModelCode.Clear();
                    lvwProcess.Items.Clear();
                    comProcess.SelectedIndex = -1;
                    txtSPLName.Text = "";
                    btnOK.Enabled = false;
                }
                else
                {
                    failMsg = new FailMessage(Result, 999999);
                    failMsg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
                failMsg = new FailMessage("解绑失败！\r\n" + ex.Message, 999999);
                failMsg.ShowDialog(this);
            }
            finally
            {
                //btnOK.Enabled = true;
                btnSearch.Enabled = true;
                txtCode.Enabled = true;
                txtCode.Focus();
                txtCode.SelectAll();
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

        /// <summary>
        /// 选择生产产线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSPLName_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

    }
}