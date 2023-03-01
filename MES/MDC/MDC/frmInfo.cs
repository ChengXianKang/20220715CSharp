using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Utils;
using Utils.HBaseClass;

namespace MDC
{
    public partial class frmInfo : Form
    {
        ///// <summary>
        ///// HBase数据查询类
        ///// </summary>
        //GetHBaseDataClass GHDC;
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;
        /// <summary>
        /// 成功提示框
        /// </summary>
        private SuccessMessage successMsg;

        public frmInfo()
        {
            InitializeComponent();

            txtCode.Dock = DockStyle.Fill;
            txtLCDState.Dock = DockStyle.Fill;
            txtRepeat.Dock = DockStyle.Fill;
            txtOrder.Dock = DockStyle.Fill;
            txtModel.Dock = DockStyle.Fill;
            txtLine.Dock = DockStyle.Fill;
            txtFinishesCode.Dock = DockStyle.Fill;
            txtCusCode.Dock = DockStyle.Fill;
            txtProductDate.Dock = DockStyle.Fill;
            txtTPState.Dock = DockStyle.Fill;
            txtIC.Dock = DockStyle.Fill;
            txtLCD.Dock = DockStyle.Fill;
            txtFPC.Dock = DockStyle.Fill;
            txtTP.Dock = DockStyle.Fill;
            txtBL.Dock = DockStyle.Fill;
            txtQR.Dock = DockStyle.Fill;
            lvwProcess.Dock = DockStyle.Fill;
            txtState.Dock = DockStyle.Fill;
            txtQRCode.Dock = DockStyle.Fill;
            txtSubJobCode.Dock = DockStyle.Fill;
            txtSubDate.Dock = DockStyle.Fill;
            txtInnerJobCode.Dock = DockStyle.Fill;
            txtInnerDate.Dock = DockStyle.Fill;
            txtOuterJobCode.Dock = DockStyle.Fill;
            txtOuterDate.Dock = DockStyle.Fill;
            txtPalletJobCode.Dock = DockStyle.Fill;
            txtPalletDate.Dock = DockStyle.Fill;

            ClearAll();

            // 当前程序版本
            string versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = string.Format("单片信息追溯   {0}", versionName);
        }

        private void frmInfo_Load(object sender, EventArgs e)
        {
            // 绑定串口数据接收事件处理程序
            Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            txtCode.Focus();
            txtCode.SelectAll();
        }

        private void frmInfo_FormClosing(object sender, FormClosingEventArgs e)
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
            if (e.KeyChar != (char)Keys.Enter || txtCode.Text == "") return;
            // 分析处理数据
            ScanDataHandler(txtCode.Text);
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "") return;
            // 分析处理数据
            ScanDataHandler(txtCode.Text);
        }

        /// <summary>
        /// 清除所有数据
        /// </summary>
        private void ClearAll()
        {
            txtCode.Clear();
            txtLCDState.Clear();
            txtRepeat.Clear();
            txtOrder.Clear();
            txtModel.Clear();
            txtLine.Clear();
            txtFinishesCode.Clear();
            txtCusCode.Clear();
            txtProductDate.Clear();
            txtTPState.Clear();
            txtIC.Clear();
            txtLCD.Clear();
            txtFPC.Clear();
            txtTP.Clear();
            txtBL.Clear();
            txtQR.Clear();
            txtState.Clear();
            txtQRCode.Clear();
            txtSubJobCode.Clear();
            txtSubDate.Clear();
            txtInnerJobCode.Clear();
            txtInnerDate.Clear();
            txtOuterJobCode.Clear();
            txtOuterDate.Clear();
            txtPalletJobCode.Clear();
            txtPalletDate.Clear();

            lvwProcess.Items.Clear();
            dgwException.DataSource = null;
            dgwMaterial.DataSource = null;
            dgwProcess.DataSource = null;
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
                    #region 扫描数据解析
                    //编码规则解析并填充文本框
                    txtCode.Text = DataString.Trim();  // 实际扫描数据

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

                    // YFPCA-SKI65101B18S|F|S1|201121|01F68          YFPCA-SKI65101B18S/F/S1/201121/01F68
                    AnalysisCode = AnalysisCode.Replace("|", "/").Replace(";", "；");

                    if (AnalysisCode.Length <= 9)
                    {
                        ShowResult(NoteState.Error, "", "默认编码长度至少为10位以上，请确认条码长度");
                        return;
                    }

                    txtCode.Text = AnalysisCode;

                    #endregion 扫描数据解析

                    #region 查询玻璃信息

                    ClearAll();
                    txtCode.Text = AnalysisCode;

                    // 异步处理编码重码检测
                    Thread thread = new Thread(new ParameterizedThreadStart(GetGlassData));
                    thread.IsBackground = true;
                    thread.Start(AnalysisCode);
                    #endregion 查询玻璃信息
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志
                LogHelper.Error(ex.Message, ex);
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

                    ClearAll();

                    txtCode.Focus();

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
        /// 从HBase数据库查询编码对应的玻璃信息
        /// </summary>
        /// <param name="code">查询编码</param>
        private void GetGlassData(object code)
        {
            DataSet ds = null;
            Utils.Model.GlassInfo glassInfo = null;
            string searchCode = "";

            try
            {
                if (code != null)
                {
                    // 待查询玻璃编码
                    searchCode = code.ToString();

                    // 查询data_01和data_06,获取玻璃基本信息
                    glassInfo = GetHBaseDataClass.Instance.GetGlassState(searchCode);

                    if (glassInfo != null && glassInfo.ProductInfo != null && !string.IsNullOrWhiteSpace(glassInfo.ProductInfo.LCDCode))
                    {
                        //用玻璃码查询data_02过站数据和物料信息
                        try
                        {
                            //物料上料工序对照表
                            string sqlRackProcess = string.Format("SELECT DISTINCT SPOS_SMCode,SPOS_RackProcess FROM Store_ProduceOrder_Sub LEFT JOIN Store_ProduceOrder_Main ON SPOS_SPOMTid=SPOM_Tid WHERE  SPOM_JobCode='{0}'  AND ISNULL(SPOS_RackProcess,'')<>''", glassInfo.ProductInfo.ProductionOrder);
                            DataTable dtRackProcess = conn.ExecuteDataTable(sqlRackProcess, "Base");
                            Dictionary<string, string> dicRackProcess = dtRackProcess.Rows.Cast<DataRow>().ToDictionary(x => x["SPOS_SMCode"].ToString(), x => x["SPOS_RackProcess"].ToString());

                            ds = GetHBaseDataClass.Instance.GetData02(glassInfo.ProductInfo.LCDCode, dicRackProcess);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("查询data_02过站数据和物料信息失败", ex);
                        }

                        //查询玻璃不良履历
                        DataTable dtException = new DataTable();
                        try
                        {
                            DataTable dt = GetHBaseDataClass.Instance.GetExceptionInfo(glassInfo.ProductInfo.LCDCode);
                            if (dt != null)
                            {
                                dtException = dt.Copy();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("查询玻璃不良履历失败", ex);
                        }
                        dtException.TableName = "exceptionTable";
                        ds.Tables.Add(dtException);

                        // 成品包装信息
                        DataTable dtPackage = new DataTable();
                        try
                        {
                            //包装信息
                            string sql = string.Format("EXEC Store_ChengPin_List_Pro '{0}'", glassInfo.ProductInfo.LCDCode);
                            DataTable dt = conn.ExecuteDataTable(sql, "Base");
                            if (dt != null)
                            {
                                dtPackage = dt.Copy();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("查询玻璃包装信息失败", ex);
                        }
                        dtPackage.TableName = "packageTable";
                        ds.Tables.Add(dtPackage);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
            finally
            {
                ShowInfo(glassInfo, ds);
            }
        }

        /// <summary>
        /// 跨线程展示玻璃信息的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ShowInfoCallback(Utils.Model.GlassInfo glassInfo, DataSet dsGlass);
        /// <summary>
        /// 展示玻璃信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="glassData"></param>
        private void ShowInfo(Utils.Model.GlassInfo glassInfo, DataSet dsGlass)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                ShowInfoCallback d = new ShowInfoCallback(ShowInfo);
                this.Invoke(d, new object[] { glassInfo, dsGlass });
            }
            else
            {
                try
                {
                    if (glassInfo != null)
                    {
                        //玻璃状态信息data_06
                        if (glassInfo.GlassState != null)
                        {
                            // 玻璃状态
                            txtLCDState.Text = glassInfo.GlassState.LCDState;
                            // 是否重工
                            txtRepeat.Text = glassInfo.GlassState.Repeat == "True" ? "是" : "否";
                        }

                        //玻璃基本信息data_01
                        if (glassInfo.ProductInfo != null)
                        {
                            // 生产工单
                            txtOrder.Text = glassInfo.ProductInfo.ProductionOrder;
                            // 成品型号
                            txtModel.Text = glassInfo.ProductInfo.FinishesModel;
                            // 产线编码
                            txtLine.Text = glassInfo.ProductInfo.ProductionLineCode;
                            //存货编码
                            txtFinishesCode.Text = glassInfo.ProductInfo.FinishesCode;
                            //客户料号
                            txtCusCode.Text = glassInfo.ProductInfo.CustomerNumber;
                            //生产日期
                            txtProductDate.Text = glassInfo.ProductInfo.ProductionDate;
                            //TP类型
                            txtTPState.Text = glassInfo.ProductInfo.TPState == "1" ? "有码" : "无码";
                            //IC
                            txtIC.Text = glassInfo.ProductInfo.ICCode;
                            //LCD
                            txtLCD.Text = glassInfo.ProductInfo.LCDCode;
                            //FPC
                            txtFPC.Text = glassInfo.ProductInfo.FPCCode;
                            //TP
                            txtTP.Text = glassInfo.ProductInfo.TPCode;
                            //BL
                            txtBL.Text = glassInfo.ProductInfo.BLCode;
                            //QR
                            txtQR.Text = glassInfo.ProductInfo.QRCode;
                        }

                        if (dsGlass != null && dsGlass.Tables.Count > 0)
                        {
                            // 工序过站信息
                            DataView dvParameter = dsGlass.Tables["parameterTable"].DefaultView;
                            dvParameter.Sort = "parameter_DateTime Asc, parameter_ProcessNumber Asc";
                            DataTable dtParameter = dvParameter.ToTable(true, new string[] { "parameter_ProcessNumber", "parameter_LineCode", "parameter_ProcessName", "parameter_DateTime", "parameter_CardIP", "parameter_trackNumber", "parameter_oppositeNumber", "parameter_bfFlag", "parameter_exceptionState", "parameter_ScanOP" });
                            dgwProcess.AutoGenerateColumns = false;
                            dgwProcess.DataSource = dtParameter;
                            //dvParameter = dtParameter.DefaultView;
                            //if (dvParameter != null && dvParameter.Count > 0)
                            //{
                            //    foreach (DataRowView row in dvParameter)
                            //    {
                            //        ListViewItem item = lvwProcess.Items.Add(row["parameter_ProcessNumber"].ToString());
                            //        item.SubItems.AddRange(new string[]{
                            //            row["parameter_LineCode"].ToString(), 
                            //            row["parameter_ProcessName"].ToString(), 
                            //            row["parameter_DateTime"].ToString(), 
                            //            row["parameter_CardIP"].ToString(), 
                            //            row["parameter_bfFlag"].ToString(),
                            //            row["parameter_ScanNumber"].ToString(),
                            //            row["parameter_trackNumber"].ToString(), 
                            //            row["parameter_oppositeNumber"].ToString() 
                            //        });
                            //    }

                            //    for (int i = 0; i < lvwProcess.Columns.Count; i++)
                            //    {
                            //        if (i <= 1)
                            //        {
                            //            lvwProcess.Columns[i].Width = -2;
                            //        }
                            //        else
                            //        {
                            //            lvwProcess.Columns[i].Width = -1;
                            //        }
                            //    }
                            //    lvwProcess.Columns[5].Width = 45;
                            //}

                            //包装信息
                            DataView dvPackage = dsGlass.Tables["packageTable"].DefaultView;
                            if (dvPackage != null && dvPackage.Count > 0)
                            {
                                txtState.Text = dvPackage[0]["Now_State"].ToString();
                                txtQRCode.Text = dvPackage[0]["QrCode"].ToString();
                                txtSubJobCode.Text = dvPackage[0]["Sub_JobCode"].ToString();
                                txtSubOP.Text = dvPackage[0]["Sub_Op"].ToString();
                                txtSubDate.Text = dvPackage[0]["Sub_Date"].ToString();
                                txtSubIP.Text = dvPackage[0]["Sub_CardIp"].ToString();
                                txtInnerJobCode.Text = dvPackage[0]["Inner_JobCode"].ToString();
                                txtInnerOP.Text = dvPackage[0]["Inner_Op"].ToString();
                                txtInnerDate.Text = dvPackage[0]["Inner_Date"].ToString();
                                txtInnerIP.Text = dvPackage[0]["Inner_CardIp"].ToString();
                                txtOuterJobCode.Text = dvPackage[0]["Outer_JobCode"].ToString();
                                txtOuterOP.Text = dvPackage[0]["Outer_Op"].ToString();
                                txtOuterDate.Text = dvPackage[0]["Outer_Date"].ToString();
                                txtOuterIP.Text = dvPackage[0]["Outer_CardIp"].ToString();
                                txtPalletJobCode.Text = dvPackage[0]["Pallet_JobCode"].ToString();
                                txtPalletOP.Text = dvPackage[0]["Pallet_Op"].ToString();
                                txtPalletDate.Text = dvPackage[0]["Pallet_Date"].ToString();
                                txtPalletIP.Text = dvPackage[0]["Pallet_CardIp"].ToString();
                            }

                            //不良信息
                            DataTable dtException = dsGlass.Tables["exceptionTable"];
                            foreach (DataRow row in dtException.Rows)
                            {
                                string rwProcessCode = (row["ex_reworkProcess"] ?? "").ToString();
                                if (!string.IsNullOrWhiteSpace(rwProcessCode))
                                {
                                    string reProcessName = BaseUI.GetProcessName(rwProcessCode);
                                    row["ex_reworkProcess"] = reProcessName;
                                }
                            }
                            dgwException.AutoGenerateColumns = false;
                            dgwException.DataSource = dtException;

                            //物料批次信息
                            DataView dvMaterial = new DataView(dsGlass.Tables["materialTable"]);
                            dvMaterial.RowFilter = "material_ProcessCode IN ('005','008','018','024')";
                            DataTable dtm = dvMaterial.ToTable(true, new string[] { "material_Code", "material_Name", "material_Spec", "material_Vonder", "material_SCDate", "material_Batch", "material_FeedingOP" });
                            foreach (DataRow row in dtm.Rows)
                            {
                                string Feeder = (row["material_FeedingOP"] ?? "").ToString();
                                if (Feeder == "隆晓燕")
                                {
                                    Feeder = "物流公共账号";
                                    row["material_FeedingOP"] = Feeder;
                                }
                            }
                            dgwMaterial.AutoGenerateColumns = false;
                            dgwMaterial.DataSource = dtm;
                        }//if (dsGlass != null && dsGlass.Tables.Count > 0)
                    }//if (glassInfo != null)
                }//try
                catch (Exception ex)
                {
                    LogHelper.Error(ex.Message, ex);
                    //ExceptionResult("获取玻璃信息失败。" + ex.Message.ToString());
                }
                finally
                {
                    txtCode.Enabled = true;
                    txtCode.Focus();
                    txtCode.SelectAll();
                }

                //Dictionary<string, dynamic> dic = GetHBaseDataClass.Instance.GetAllData(txtCode.Text);
            }
 
        }

        /// <summary>
        /// 右键菜单-复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopy_Click(object sender, EventArgs e)
        {
            if (this.lvwProcess.SelectedItems.Count == 0) return;
            string content = "";
            foreach (ListViewItem item in this.lvwProcess.SelectedItems)
            {
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    content += item.SubItems[i].Text + "    ";
                }
                content += "\r\n";
            }
            Clipboard.SetDataObject(content);
        }

        /// <summary>
        /// 右键菜单-复制扫描码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyCode1_Click(object sender, EventArgs e)
        {
            if (this.lvwProcess.SelectedItems.Count == 0) return;
            string content = "";
            foreach (ListViewItem item in this.lvwProcess.SelectedItems)
            {
                content += item.SubItems[6].Text + "\r\n";
            }
            Clipboard.SetDataObject(content);
        }

        /// <summary>
        /// 右键菜单-复制对应码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyCode2_Click(object sender, EventArgs e)
        {
            if (this.lvwProcess.SelectedItems.Count == 0) return;
            string content = "";
            foreach (ListViewItem item in this.lvwProcess.SelectedItems)
            {
                content += item.SubItems[7].Text + "\r\n";
            }
            Clipboard.SetDataObject(content);
        }

        /// <summary>
        /// 窗体尺寸变化时刷新界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInfo_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
            txtCode.Focus();
            txtCode.SelectAll();
        }



    }
}
