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

using Newtonsoft.Json.Linq;

namespace MDC
{
    public partial class frmOrderException : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();

        /// <summary>
        /// 后台修改工单已投入玻璃的TP有码状态
        /// </summary>
        BackgroundWorker bgwTPState;
        /// <summary>
        /// 后台修改工单已投入玻璃的存货编码
        /// </summary>
        BackgroundWorker bgwProductNumber;
        /// <summary>
        /// 后台修改工单已投入玻璃的客户料号
        /// </summary>
        BackgroundWorker bgwGlassCusNumber;
        /// <summary>
        /// 后台修改工单已投入玻璃的产线编码
        /// </summary>
        BackgroundWorker bgwLineCode;
        /// <summary>
        /// 后台修改工单已投入玻璃的成品型号
        /// </summary>
        BackgroundWorker bgwModelCode;
        /// <summary>
        /// 导出工单已投入玻璃的信息
        /// </summary>
        BackgroundWorker bgwGlassList;
        /// <summary>
        /// 工单所有玻璃编码列表
        /// </summary>
        List<string> lstLCD;
        /// <summary>
        /// 本机IP
        /// </summary>
        private string DeviceIp = "";


        public frmOrderException()
        {
            InitializeComponent();
            grpCusCode.Visible = true;
            grpOrderCode.Visible = true;
            grpOrderInfo.Visible = true;
            grpGlassHbaseInfo.Visible = true;
            grpProcessHistory.Visible = true;
        }

        public frmOrderException(int WindowType)
        {
            InitializeComponent();
            switch (WindowType)
            {
                case 1:
                    grpCusCode.Visible = false;
                    grpOrderCode.Visible = false;
                    grpOrderInfo.Visible = false;
                    grpGlassHbaseInfo.Visible = false;
                    grpProcessHistory.Visible = true;
                    break;
                default:
                    grpCusCode.Visible = true;
                    grpOrderCode.Visible = true;
                    grpOrderInfo.Visible = true;
                    grpGlassHbaseInfo.Visible = true;
                    grpProcessHistory.Visible = true;
                    break;
            }
        }

        private void frmOrderException_Load(object sender, EventArgs e)
        {
            this.DeviceIp = BaseUI.GetLocalIP();
            this.Size = new Size(810, 569);
        }

        private void ClearAll()
        {
        }


        #region 修改工单已投入玻璃的TP有码状态
        /// <summary>
        /// 修改TP有码状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show(this, "此操作将批量修改工单所有已投产玻璃的TP有码状态\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst != DialogResult.OK) return;
   
            string order = txtOrder.Text;
            string tpstate = rdoTPState1.Checked ? "1" : "0";
            List<string> lstLCD = GetHBaseDataClass.Instance.GetLCDByOrder(order);
            pgsTPState.Maximum = lstLCD.Count;
            pgsTPState.Step = 1;
            pgsTPState.Value = 0;

            bgwTPState = new BackgroundWorker();
            bgwTPState.WorkerReportsProgress = true;
            bgwTPState.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgwTPState.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            bgwTPState.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            bgwTPState.RunWorkerAsync(new object[] { lstLCD, tpstate });
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] obj = e.Argument as object[];
            lstLCD = obj[0] as List<string>;
            string tpstate = obj[1].ToString();
            for (int i = 0; i < lstLCD.Count; i++)
            {
                if (lstLCD.Count != 0)
                {
                    bgwTPState.ReportProgress(100 * i / lstLCD.Count, i);
                }

                string lcd = lstLCD[i];
                try
                {
                    dynamic objLCD = GetHBaseDataClass.Instance.GetData01(lcd);
                    if ((objLCD.tpState ?? "*").ToString() != tpstate)
                    {
                        objLCD.tpState = tpstate;
                        GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", lcd, objLCD);
                    }

                    string fpc = objLCD.fpcCode;
                    if (!string.IsNullOrWhiteSpace(fpc))
                    {
                        dynamic objFPC = GetHBaseDataClass.Instance.GetData01(fpc);
                        if ((objFPC.tpState ?? "*").ToString() != tpstate)
                        {
                            objFPC.tpState = tpstate;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", fpc, objFPC);
                        }
                    }

                    string tp = objLCD.tpCode;
                    if (!string.IsNullOrWhiteSpace(tp))
                    {
                        dynamic objTP = GetHBaseDataClass.Instance.GetData01(tp);
                        if ((objTP.tpState ?? "*").ToString() != tpstate)
                        {
                            objTP.tpState = tpstate;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objTP);
                        }
                    }

                    string bl = objLCD.backCode;
                    if (!string.IsNullOrWhiteSpace(bl))
                    {
                        dynamic objBL = GetHBaseDataClass.Instance.GetData01(bl);
                        if ((objBL.tpState ?? "*").ToString() != tpstate)
                        {
                            objBL.tpState = tpstate;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", bl, objBL);
                        }
                    }

                    string qr = objLCD.intactCode;
                    if (!string.IsNullOrWhiteSpace(qr))
                    {
                        dynamic objQR = GetHBaseDataClass.Instance.GetData01(qr);
                        if ((objQR.tpState ?? "*").ToString() != tpstate)
                        {
                            objQR.tpState = tpstate;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", qr, objQR);
                        }
                    }
                    //productInfo.LCDCode = (jsonStr["glassCode"] ?? "").ToString(); //玻璃码
                    //productInfo.BLCode = (jsonStr["backCode"] ?? "").ToString();//BL编码
                    //productInfo.FPCCode = (jsonStr["fpcCode"] ?? "").ToString();//fpc编码
                    //productInfo.TPCode = (jsonStr["tpCode"] ?? "").ToString();//tp编码
                    //productInfo.ICCode = (jsonStr["icCode"] ?? "").ToString();
                    //productInfo.QRCode = (jsonStr["intactCode"] ?? "").ToString();//54位产品编码
                    //productInfo.TPState = (jsonStr["tpState"] ?? "0").ToString();//TP是否有码
                }
                catch (Exception ex)
                {
                    LogHelper.Error("玻璃" + lcd + "的TP有码类型修改失败", ex);
                    continue;
                }
            }

            e.Result = "ok";
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddLog("修改工单玻璃TP有码状态", "工单" + txtOrder.Text + "所有已投产玻璃的TP有码状态修改为" + (rdoTPState1.Checked ? "【有码】" : "【无码】"));
            MessageBox.Show("修改成功！");
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = Convert.ToInt32(e.UserState);
            lblTPState.Text = string.Format("{0} / {1}", i + 1, lstLCD.Count);
            pgsTPState.Value = i;
        }

        #endregion 修改工单已投入玻璃的TP有码状态

        /// <summary>
        /// 工单信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtOrder.Text.Trim() == "") return;
            txtOrderState.Clear();
            txtCusNumber.Clear();
            txtCusOrder.Clear();
            txtFinishesCode.Clear();
            string sql = string.Format("SELECT SPOM_Tid,SPOM_Status,SPOM_JobCode,SPOM_SMCode,SPOM_CustomerCode,SPOM_IsTP,SPOM_CustomerOrder FROM Store_ProduceOrder_Main WHERE SPOM_JobCode = '{0}'", txtOrder.Text);
            DataView dv = conn.ExecuteDataView(sql, "Base");
            if (dv.Count == 0)
            {
                MessageBox.Show(this, "未查询到该工单信息!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtOrderState.Text = (dv[0]["SPOM_Status"] ?? "").ToString();
            txtFinishesCode.Text = (dv[0]["SPOM_SMCode"] ?? "").ToString();
            txtCusNumber.Text = (dv[0]["SPOM_CustomerCode"] ?? "").ToString();
            txtCusOrder.Text = (dv[0]["SPOM_CustomerOrder"] ?? "").ToString();
            if ((dv[0]["SPOM_IsTP"] ?? "0").ToString() == "1")
            {
                rdoTPState1.Checked = true;
                rdoTPState0.Checked = false;
            }
            else
            {
                rdoTPState1.Checked = false;
                rdoTPState0.Checked = true;
            }
        }


        #region 修改工单信息
        /// <summary>
        /// 修改工单状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrderState_Click(object sender, EventArgs e)
        {
            if (cmbOrderState.SelectedIndex == -1)
            {
                MessageBox.Show(this, "请选择工单状态", "注意", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult rst = MessageBox.Show(this, "工单：" + txtOrder.Text + "\n原状态：" + txtOrderState.Text + "\n修改为：" + cmbOrderState.Text + "\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst == DialogResult.OK)
            {
                string sql = string.Format("UPDATE Store_ProduceOrder_Main SET SPOM_Status = '{0}' WHERE SPOM_JobCode = '{1}'", cmbOrderState.Text, txtOrder.Text);
                int n = conn.ExecuteAction(sql, "Base");
                if (n > 0)
                {
                    AddLog("修改工单状态", sql);
                    MessageBox.Show(this, "操作成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show(this, "修改失败，请重试!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// 修改产品料号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinishesCode_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show(this, "工单：" + txtOrder.Text + "\n原产品料号：" + txtFinishesCode.Text + "\n修改为：" + txtFinishesCodeNew.Text + "\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst == DialogResult.OK)
            {
                string sql = string.Format("UPDATE Store_ProduceOrder_Main SET SPOM_SMCode = '{0}' WHERE SPOM_JobCode = '{1}'", txtFinishesCodeNew.Text, txtOrder.Text);
                int n = conn.ExecuteAction(sql, "Base");
                if (n > 0)
                {
                    AddLog("修改产品料号", sql);
                    MessageBox.Show(this, "操作成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show(this, "修改失败，请重试!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// 修改客户料号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCusCode_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show(this, "工单：" + txtOrder.Text + "\n原客户料号：" + txtCusNumber.Text + "\n修改为：" + txtCusNumberNew.Text + "\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst == DialogResult.OK)
            {
                string sql = string.Format("UPDATE Store_ProduceOrder_Main SET SPOM_CustomerCode = '{0}' WHERE SPOM_JobCode = '{1}'", txtCusNumberNew.Text, txtOrder.Text);
                int n = conn.ExecuteAction(sql, "Base");
                if (n > 0)
                {
                    AddLog("修改客户料号", sql);
                    MessageBox.Show(this, "操作成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show(this, "修改失败，请重试!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        /// <summary>
        /// 修改客户订单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCusOrder_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show(this, "工单：" + txtOrder.Text + "\n原客户订单号：" + txtCusOrder.Text + "\n修改为：" + txtCusOrderNew.Text + "\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst == DialogResult.OK)
            {
                string sql = string.Format("UPDATE Store_ProduceOrder_Main SET SPOM_CustomerOrder = '{0}' WHERE SPOM_JobCode = '{1}'", txtCusOrderNew.Text, txtOrder.Text);
                int n = conn.ExecuteAction(sql, "Base");
                if (n > 0)
                {
                    AddLog("修改客户订单号", sql);
                    MessageBox.Show(this, "操作成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show(this, "修改失败，请重试!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        #endregion 修改工单信息

        /// <summary>
        /// 查询外箱的产品料号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOuterBoxSearch_Click(object sender, EventArgs e)
        {
            if (txtOuterBox.Text.Trim() == "") return;
            txtOuterBoxCode.Clear();
            string sql = string.Format("SELECT SM_nbCode FROM TPL_PackingOuterBox_Main JOIN Store_Material ON OBM_SMTid =SM_Tid WHERE OBM_JobCode = '{0}'", txtOuterBox.Text);
            object obj = conn.ExecuteScalar(sql, "Base");
            if (obj == null)
            {
                MessageBox.Show(this, "未查询到该外箱信息!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtOuterBoxCode.Text = (obj ?? "").ToString();
        }
        /// <summary>
        /// 修改单一外箱的产品料号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOuterBoxCode_Click(object sender, EventArgs e)
        {
            if (txtOuterBoxCode.Text.Trim() == "")
            {
                MessageBox.Show(this, "请先查询外箱信息", "注意", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtOuterBoxCodeNew.Text.Trim() == "")
            {
                MessageBox.Show(this, "请输入新产品料号", "注意", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult rst = MessageBox.Show(this, "外箱条码：" + txtOuterBox.Text + "\n原产品料号：" + txtOuterBoxCode.Text + "\n修改为：" + txtOuterBoxCodeNew.Text + "\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst == DialogResult.OK)
            {
                string sql = string.Format("SELECT SM_Tid FROM Store_Material WHERE SM_nbCode = '{0}'", txtOuterBoxCodeNew.Text);
                object obj = conn.ExecuteScalar(sql, "Base");
                if ((obj ?? "").ToString() == "")
                {
                    MessageBox.Show(this, "未查询到新料号对应的成品信息!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string id = (obj ?? "").ToString();

                sql = string.Format("UPDATE TPL_PackingOuterBox_Main SET OBM_SMTid = {0} WHERE OBM_JobCode = '{1}'", id, txtOuterBox.Text);
                int n = conn.ExecuteAction(sql, "Base");
                if (n > 0)
                {
                    AddLog("修改外箱产品料号", sql);
                    MessageBox.Show(this, "操作成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show(this, "修改失败，请重试!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// 添加异常处理操作日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        private bool AddLog(string type, string desc)
        {
            string op = BaseUI.UI_BOOLOGNAME;
            string ip = this.DeviceIp;
            return AddLog(type, op, ip, desc);
        }

        /// <summary>
        /// 添加异常处理操作日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="op"></param>
        /// <param name="ip"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        private bool AddLog(string type, string op, string ip, string desc)
        {
            if (desc.Contains("'"))
            {
                desc = desc.Replace("'", "''");
            }
            string sql = string.Format("INSERT INTO TPL_Operation_Log (TOL_Type,TOL_Op,TOL_Ip,TOL_Desc) VALUES ('{0}','{1}','{2}','{3}')", type, op, ip, desc);
            int n = conn.ExecuteAction(sql, "Base");
            return n > 0 ? true : false;
        }

        /// <summary>
        /// 过站信息查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcessSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtData02GlassCode.Text)) return;
            if (cmbTableName.SelectedIndex < 0) return;
            string code = txtData02GlassCode.Text;
            List<string> keys = new List<string>();
            switch (cmbTableName.Text)
            {
                case "data_01":
                    keys = GetHBaseDataClass.Instance.GetRowKeys(code, "data_01");
                    break;

                case "data_02":
                    //keys = GetHBaseDataClass.Instance.GetData02Keys(code);
                    keys = GetHBaseDataClass.Instance.GetRowKeys(code, "data_02");
                    break;

                case "data_06":
                    keys = GetHBaseDataClass.Instance.GetRowKeys(code, "data_06");
                    break;

                case "exception_01":
                    keys = GetHBaseDataClass.Instance.GetRowKeys(code, "exception_01");
                    break;
            }
            lstKeys.DataSource = keys;
        }

        /// <summary>
        /// 选中Key值后，获取Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTableName.SelectedIndex < 0) return;

            string key = lstKeys.SelectedItem.ToString();
            txtKey.Text = key;
            dynamic jobject = GetHBaseDataClass.Instance.GetDataValueByKey(key, cmbTableName.Text);
            if (jobject != null)
            {
                txtData02Value.Text = jobject.ToString();
            }
            else
            {
                txtData02Value.Text = "";
            }
        }

        /// <summary>
        /// 修改过站数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (cmbTableName.SelectedIndex < 0) return;
            string key = txtKey.Text;
            dynamic jobject = JValue.Parse(txtData02Value.Text);
            if (GetHBaseDataClass.Instance.UpdateHBaseKeyValue(cmbTableName.Text, key, jobject))
            {
                MessageBox.Show("修改成功！");
            }
            else
            {
                MessageBox.Show("修改失败，请重试！");
            }
        }

        /// <summary>
        /// 过站数据修改玻璃码输入框回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtData02GlassCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcessSearch.PerformClick();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (cmbTableName.SelectedIndex < 0) return;
            string key = lstKeys.SelectedItem.ToString();

            if (GetHBaseDataClass.Instance.DeleteRowKey(key, cmbTableName.Text))
            {
                MessageBox.Show("删除成功！");
            }
            else
            {
                MessageBox.Show("删除失败，请重试！");
            }
        }

        #region 修改工单所有已投入玻璃的存货编码
        private void btnProductNumber_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show(this, "此操作将批量修改工单所有已投产玻璃的存货编码\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst != DialogResult.OK) return;

            string order = txtOrder.Text;
            string productNo = txtProductNumber.Text;
            List<string> lstLCD = GetHBaseDataClass.Instance.GetLCDByOrder(order);
            pgsProductNumber.Maximum = lstLCD.Count;
            pgsProductNumber.Step = 1;
            pgsProductNumber.Value = 0;
            bgwProductNumber = new BackgroundWorker();
            bgwProductNumber.WorkerReportsProgress = true;
            bgwProductNumber.DoWork += new DoWorkEventHandler(bgwProductNumber_DoWork);
            bgwProductNumber.ProgressChanged += new ProgressChangedEventHandler(bgwProductNumber_ProgressChanged);
            bgwProductNumber.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwProductNumber_RunWorkerCompleted);
            bgwProductNumber.RunWorkerAsync(new object[] { lstLCD, productNo });
        }

        private void bgwProductNumber_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] obj = e.Argument as object[];
            lstLCD = obj[0] as List<string>;
            string productNo = obj[1].ToString();
            for (int i = 0; i < lstLCD.Count; i++)
            {
                if (lstLCD.Count != 0)
                {
                    bgwProductNumber.ReportProgress(100 * i / lstLCD.Count, i);
                }

                string lcd = lstLCD[i];
                try
                {
                    dynamic objLCD = GetHBaseDataClass.Instance.GetData01(lcd);
                    if ((objLCD.finishesCode ?? "*").ToString() != productNo)
                    {
                        objLCD.finishesCode = productNo;
                        GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", lcd, objLCD);
                    }

                    string fpc = objLCD.fpcCode;
                    if (!string.IsNullOrWhiteSpace(fpc))
                    {
                        dynamic objFPC = GetHBaseDataClass.Instance.GetData01(fpc);
                        if ((objFPC.finishesCode ?? "*").ToString() != productNo)
                        {
                            objFPC.finishesCode = productNo;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", fpc, objFPC);
                        }
                    }

                    string tp = objLCD.tpCode;
                    if (!string.IsNullOrWhiteSpace(tp))
                    {
                        dynamic objTP = GetHBaseDataClass.Instance.GetData01(tp);
                        if ((objTP.finishesCode ?? "*").ToString() != productNo)
                        {
                            objTP.finishesCode = productNo;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objTP);
                        }
                    }

                    string bl = objLCD.backCode;
                    if (!string.IsNullOrWhiteSpace(bl))
                    {
                        dynamic objBL = GetHBaseDataClass.Instance.GetData01(bl);
                        if ((objBL.finishesCode ?? "*").ToString() != productNo)
                        {
                            objBL.finishesCode = productNo;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", bl, objBL);
                        }
                    }

                    string qr = objLCD.intactCode;
                    if (!string.IsNullOrWhiteSpace(qr))
                    {
                        dynamic objQR = GetHBaseDataClass.Instance.GetData01(qr);
                        if ((objQR.finishesCode ?? "*").ToString() != productNo)
                        {
                            objQR.finishesCode = productNo;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", qr, objQR);
                        }
                    }
                    //productInfo.LCDCode = (jsonStr["glassCode"] ?? "").ToString(); //玻璃码
                    //productInfo.BLCode = (jsonStr["backCode"] ?? "").ToString();//BL编码
                    //productInfo.FPCCode = (jsonStr["fpcCode"] ?? "").ToString();//fpc编码
                    //productInfo.TPCode = (jsonStr["tpCode"] ?? "").ToString();//tp编码
                    //productInfo.ICCode = (jsonStr["icCode"] ?? "").ToString();
                    //productInfo.QRCode = (jsonStr["intactCode"] ?? "").ToString();//54位产品编码
                    //productInfo.TPState = (jsonStr["tpState"] ?? "0").ToString();//TP是否有码
                }
                catch (Exception ex)
                {
                    LogHelper.Error("玻璃" + lcd + "的存货编码修改失败", ex);
                    continue;
                }
            }

            e.Result = "ok";
        }

        private void bgwProductNumber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddLog("修改工单玻璃存货编码", "工单" + txtOrder.Text + "所有已投产玻璃的存货编码修改为" + txtProductNumber.Text);
            MessageBox.Show("修改成功！");
        }

        private void bgwProductNumber_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = Convert.ToInt32(e.UserState);
            lblProductNumber.Text = string.Format("{0} / {1}", i + 1, lstLCD.Count);
            pgsProductNumber.Value = i;
        }
        #endregion 修改工单所有已投入玻璃的存货编码

        #region 修改工单所有已投入玻璃的客户料号
        private void btnGlassCusNumber_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show(this, "此操作将批量修改工单所有已投产玻璃的客户料号\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst != DialogResult.OK) return;

            string order = txtOrder.Text;
            string cusNo = txtGlassCusNumber.Text;
            List<string> lstLCD = GetHBaseDataClass.Instance.GetLCDByOrder(order);
            pgsGlassCusNumber.Maximum = lstLCD.Count;
            pgsGlassCusNumber.Step = 1;
            pgsGlassCusNumber.Value = 0;
            bgwGlassCusNumber = new BackgroundWorker();
            bgwGlassCusNumber.WorkerReportsProgress = true;
            bgwGlassCusNumber.DoWork += new DoWorkEventHandler(bgwGlassCusNumber_DoWork);
            bgwGlassCusNumber.ProgressChanged += new ProgressChangedEventHandler(bgwGlassCusNumber_ProgressChanged);
            bgwGlassCusNumber.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwGlassCusNumber_RunWorkerCompleted);
            bgwGlassCusNumber.RunWorkerAsync(new object[] { lstLCD, cusNo });
        }

        private void bgwGlassCusNumber_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] obj = e.Argument as object[];
            lstLCD = obj[0] as List<string>;
            string cusNo = obj[1].ToString();
            for (int i = 0; i < lstLCD.Count; i++)
            {
                if (lstLCD.Count != 0)
                {
                    bgwGlassCusNumber.ReportProgress(100 * i / lstLCD.Count, i);
                }

                string lcd = lstLCD[i];
                try
                {
                    dynamic objLCD = GetHBaseDataClass.Instance.GetData01(lcd);
                    if ((objLCD.clientProductNo ?? "*").ToString() != cusNo)
                    {
                        objLCD.clientProductNo = cusNo;
                        GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", lcd, objLCD);
                    }

                    string fpc = objLCD.fpcCode;
                    if (!string.IsNullOrWhiteSpace(fpc))
                    {
                        dynamic objFPC = GetHBaseDataClass.Instance.GetData01(fpc);
                        if ((objFPC.clientProductNo ?? "*").ToString() != cusNo)
                        {
                            objFPC.clientProductNo = cusNo;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", fpc, objFPC);
                        }
                    }

                    string tp = objLCD.tpCode;
                    if (!string.IsNullOrWhiteSpace(tp))
                    {
                        dynamic objTP = GetHBaseDataClass.Instance.GetData01(tp);
                        if ((objTP.clientProductNo ?? "*").ToString() != cusNo)
                        {
                            objTP.clientProductNo = cusNo;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objTP);
                        }
                    }

                    string bl = objLCD.backCode;
                    if (!string.IsNullOrWhiteSpace(bl))
                    {
                        dynamic objBL = GetHBaseDataClass.Instance.GetData01(bl);
                        if ((objBL.clientProductNo ?? "*").ToString() != cusNo)
                        {
                            objBL.clientProductNo = cusNo;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", bl, objBL);
                        }
                    }

                    string qr = objLCD.intactCode;
                    if (!string.IsNullOrWhiteSpace(qr))
                    {
                        dynamic objQR = GetHBaseDataClass.Instance.GetData01(qr);
                        if ((objQR.clientProductNo ?? "*").ToString() != cusNo)
                        {
                            objQR.clientProductNo = cusNo;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", qr, objQR);
                        }
                    }
                    //productInfo.LCDCode = (jsonStr["glassCode"] ?? "").ToString(); //玻璃码
                    //productInfo.BLCode = (jsonStr["backCode"] ?? "").ToString();//BL编码
                    //productInfo.FPCCode = (jsonStr["fpcCode"] ?? "").ToString();//fpc编码
                    //productInfo.TPCode = (jsonStr["tpCode"] ?? "").ToString();//tp编码
                    //productInfo.ICCode = (jsonStr["icCode"] ?? "").ToString();
                    //productInfo.QRCode = (jsonStr["intactCode"] ?? "").ToString();//54位产品编码
                    //productInfo.TPState = (jsonStr["tpState"] ?? "0").ToString();//TP是否有码
                }
                catch (Exception ex)
                {
                    LogHelper.Error("玻璃" + lcd + "的客户料号修改失败", ex);
                    continue;
                }
            }

            e.Result = "ok";
        }

        private void bgwGlassCusNumber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddLog("修改工单玻璃客户料号", "工单" + txtOrder.Text + "所有已投产玻璃的客户料号修改为" + txtProductNumber.Text);
            MessageBox.Show("修改成功！");
        }

        private void bgwGlassCusNumber_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = Convert.ToInt32(e.UserState);
            lblGlassCusNumber.Text = string.Format("{0} / {1}", i + 1, lstLCD.Count);
            pgsGlassCusNumber.Value = i;
        }
        #endregion 修改工单所有已投入玻璃的客户料号

        #region 修改已投入玻璃的产线编码
        private List<string> GetLCDCode()
        {
            List<string> lstGlass = new List<string>();
            string lineCode = "2101";
            string processCode = "005";
            DateTime date = new DateTime(2020, 12, 31);
            for (int h = 1; h < 24; h++)
            {
                DateTime time = date.AddHours(h);
                //int part = 1;
                lstGlass.AddRange(GetHBaseDataClass.Instance.GetGlassCodeByTime(lineCode, processCode, time, 1));
                lstGlass.AddRange(GetHBaseDataClass.Instance.GetGlassCodeByTime(lineCode, processCode, time, 2));
            }
            return lstGlass;
        }

        private void btnLineCode_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show(this, "此操作将批量修改工单所有已投产玻璃的产线编码\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst != DialogResult.OK) return;



            string order = txtOrder.Text;
            string line = txtLineCode.Text;
            List<string> lstLCD = GetHBaseDataClass.Instance.GetLCDByOrder(order);

            //List<string> lstLCD = GetLCDCode();
            //if (lstLCD.Contains("NWC00MM20201231170039"))
            //{
            //    pgsLineCode.Step = 1;
            //}
            pgsLineCode.Maximum = lstLCD.Count;
            pgsLineCode.Step = 1;
            pgsLineCode.Value = 0;
            bgwLineCode = new BackgroundWorker();
            bgwLineCode.WorkerReportsProgress = true;
            bgwLineCode.DoWork += new DoWorkEventHandler(bgwLineCode_DoWork);
            bgwLineCode.ProgressChanged += new ProgressChangedEventHandler(bgwLineCode_ProgressChanged);
            bgwLineCode.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwLineCode_RunWorkerCompleted);
            bgwLineCode.RunWorkerAsync(new object[] { lstLCD, line });
        }
        private void bgwLineCode_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] obj = e.Argument as object[];
            lstLCD = obj[0] as List<string>;
            string line = obj[1].ToString();
            for (int i = 0; i < lstLCD.Count; i++)
            {
                if (lstLCD.Count != 0)
                {
                    bgwLineCode.ReportProgress(100 * i / lstLCD.Count, i);
                }

                string lcd = lstLCD[i];
                try
                {
                    dynamic objLCD = GetHBaseDataClass.Instance.GetData01(lcd);
                    if ((objLCD.productLineCode ?? "*").ToString() != line)
                    {
                        objLCD.productLineCode = line;
                        GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", lcd, objLCD);
                    }

                    string fpc = objLCD.fpcCode;
                    if (!string.IsNullOrWhiteSpace(fpc))
                    {
                        dynamic objFPC = GetHBaseDataClass.Instance.GetData01(fpc);
                        if ((objFPC.productLineCode ?? "*").ToString() != line)
                        {
                            objFPC.productLineCode = line;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", fpc, objFPC);
                        }
                    }

                    string tp = objLCD.tpCode;
                    if (!string.IsNullOrWhiteSpace(tp))
                    {
                        dynamic objTP = GetHBaseDataClass.Instance.GetData01(tp);
                        if ((objTP.productLineCode ?? "*").ToString() != line)
                        {
                            objTP.productLineCode = line;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objTP);
                        }
                    }

                    string bl = objLCD.backCode;
                    if (!string.IsNullOrWhiteSpace(bl))
                    {
                        dynamic objBL = GetHBaseDataClass.Instance.GetData01(bl);
                        if ((objBL.productLineCode ?? "*").ToString() != line)
                        {
                            objBL.productLineCode = line;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", bl, objBL);
                        }
                    }

                    string qr = objLCD.intactCode;
                    if (!string.IsNullOrWhiteSpace(qr))
                    {
                        dynamic objQR = GetHBaseDataClass.Instance.GetData01(qr);
                        if ((objQR.productLineCode ?? "*").ToString() != line)
                        {
                            objQR.productLineCode = line;
                            GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", qr, objQR);
                        }
                    }

                    Dictionary<string, dynamic> dicException = GetHBaseDataClass.Instance.GetExceptionDynamic(lcd);
                    if (dicException != null && dicException.Count > 0)
                    {
                        foreach (string key in dicException.Keys)
                        {
                            dynamic objEx = dicException[key];
                            if ((objEx.productLineCode ?? "*").ToString() != line)
                            {
                                objEx.productLineCode = line;
                                //objEx.productLineName = "TXD生产39线";
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("exception_01", key, objEx);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("玻璃" + lcd + "的产线编码修改失败", ex);
                    continue;
                }
            }

            e.Result = "ok";
        }

        private void bgwLineCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddLog("修改工单玻璃产线编码", "工单" + txtOrder.Text + "所有已投产玻璃的产线编码修改为" + txtLineCode.Text);
            MessageBox.Show("修改成功！");
        }

        private void bgwLineCode_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = Convert.ToInt32(e.UserState);
            lblLineCode.Text = string.Format("{0} / {1}", i + 1, lstLCD.Count);
            pgsLineCode.Value = i;
        }
        #endregion 修改已投入玻璃的产线编码

        #region 修改工单所有已投入玻璃的成品型号

        private void btnModelCode_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show(this, "此操作将批量修改工单所有已投产玻璃的成品型号\n\n确定修改吗？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (rst != DialogResult.OK) return;

            string order = txtOrder.Text;
            string model = txtModelCode.Text;
            List<string> lstLCD = GetHBaseDataClass.Instance.GetLCDByOrder(order);
            pgsModelCode.Maximum = lstLCD.Count;
            pgsModelCode.Step = 1;
            pgsModelCode.Value = 0;
            bgwModelCode = new BackgroundWorker();
            bgwModelCode.WorkerReportsProgress = true;
            bgwModelCode.DoWork += new DoWorkEventHandler(bgwModelCode_DoWork);
            bgwModelCode.ProgressChanged += new ProgressChangedEventHandler(bgwModelCode_ProgressChanged);
            bgwModelCode.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwModelCode_RunWorkerCompleted);
            bgwModelCode.RunWorkerAsync(new object[] { lstLCD, model });
        }

        private void bgwModelCode_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] obj = e.Argument as object[];
            lstLCD = obj[0] as List<string>;
            string model = obj[1].ToString();
            for (int i = 0; i < lstLCD.Count; i++)
            {
                if (lstLCD.Count != 0)
                {
                    bgwModelCode.ReportProgress(100 * i / lstLCD.Count, i);
                }

                string lcd = lstLCD[i];
                try
                {
                    //if (lcd == "NWC00MM20201221058120")
                    //{
                    //    lcd = lcd;
                    //}
                    dynamic objLCD = GetHBaseDataClass.Instance.GetData01(lcd);
                    if (objLCD == null) continue;

                    string fpc = objLCD.fpcCode;
                    string tp = objLCD.tpCode;
                    string bl = objLCD.backCode;
                    string qr = objLCD.intactCode;

                    dynamic data06 = GetHBaseDataClass.Instance.GetDataValueByKey(lcd, "data_06");
                    string newLogCode = data06.logCode;

                    //// 第三步：删除data_01中的绑定条码
                    //// --------------------------------------------------------------------------------------------
                    ////删除条码绑定
                    //try
                    //{
                    //    //不包含FOG需要解绑FPC、TP、背光、成品喷码
                    //    if (!newLogCode.Contains("008"))
                    //    {
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.FPCCode, "logValue:");
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.TPCode, "logValue:");
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.BLCode, "logValue:");
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                    //    } //不包含TP贴合需要解绑TP、背光、成品喷码
                    //    else if (!newLogCode.Contains("018"))
                    //    {
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.TPCode, "logValue:");
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.BLCode, "logValue:");
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                    //    }
                    //    //不包含背光绑定需要解绑背光、成品喷码
                    //    else if (!newLogCode.Contains("024"))
                    //    {
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.BLCode, "logValue:");
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                    //    }
                    //    //不包含成品喷码需要解绑成品喷码
                    //    else if (!newLogCode.Contains("032") && !newLogCode.Contains("039"))
                    //    {
                    //        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                    //    }
                    //}
                    //catch (Exception exp)
                    //{
                    //    LogHelper.Error("编码解绑失败|" + exp.Message, exp);
                    //    Reconnect();
                    //    return "编码解绑失败";
                    //}

                    objLCD.finishesModel = model;
                    //objLCD.finishesCode = model;
                    //objLCD.productLineCode = "2301";
                    //objLCD.clientProductNo = "715100593041";

                    if (!newLogCode.Contains("008"))
                    {
                        objLCD.fpcCode = "";
                    }
                    if (!newLogCode.Contains("018"))
                    {
                        objLCD.tpCode = "";
                    }
                    if (!newLogCode.Contains("024"))
                    {
                        objLCD.backCode = "";
                    }
                    if (!newLogCode.Contains("032") && !newLogCode.Contains("039"))
                    {
                        objLCD.intactCode = "";
                    }
                    try
                    {
                        GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", lcd, objLCD);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("玻璃" + lcd + "修改data_01失败", ex);
                        continue;
                    }

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(fpc))
                        {
                            if (!newLogCode.Contains("008"))
                            {
                                GetDataFromHBase.Instance.DeleteRowHBase("data_01", fpc, "logValue:");
                            }
                            else
                            {
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", fpc, objLCD);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("FPC码" + fpc + "修改data_01失败", ex);
                    }

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(tp))
                        {
                            if (!newLogCode.Contains("018"))
                            {
                                GetDataFromHBase.Instance.DeleteRowHBase("data_01", tp, "logValue:");
                            }
                            else
                            {
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objLCD);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("TP码" + tp + "修改data_01失败", ex);
                    }

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(bl))
                        {
                            if (!newLogCode.Contains("024"))
                            {
                                GetDataFromHBase.Instance.DeleteRowHBase("data_01", bl, "logValue:");
                            }
                            else
                            {
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", bl, objLCD);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("BL码" + bl + "修改data_01失败", ex);
                    }

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(qr))
                        {
                            if (!newLogCode.Contains("032") && !newLogCode.Contains("039"))
                            {
                                GetDataFromHBase.Instance.DeleteRowHBase("data_01", qr, "logValue:");
                            }
                            else
                            {
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", qr, objLCD);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("成品码" + qr + "修改data_01失败", ex);
                    }

                    //if (!string.IsNullOrWhiteSpace(tp))
                    //{
                    //    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objLCD);
                    //    //dynamic objTP = GetHBaseDataClass.Instance.GetData01(tp);
                    //    //if ((objTP.finishesModel ?? "*").ToString() != model)
                    //    //{
                    //    //    objTP.finishesModel = model;
                    //    //    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objTP);
                    //    //}
                    //}

                    
                    //if (!string.IsNullOrWhiteSpace(bl))
                    //{
                    //    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", bl, objLCD);
                    //    //dynamic objBL = GetHBaseDataClass.Instance.GetData01(bl);
                    //    //if ((objBL.finishesModel ?? "*").ToString() != model)
                    //    //{
                    //    //    objBL.finishesModel = model;
                    //    //    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", bl, objBL);
                    //    //}
                    //}

                    
                    //if (!string.IsNullOrWhiteSpace(qr))
                    //{
                    //    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", qr, objLCD);
                    //    //dynamic objQR = GetHBaseDataClass.Instance.GetData01(qr);
                    //    //if ((objQR.finishesModel ?? "*").ToString() != model)
                    //    //{
                    //    //    objQR.finishesModel = model;
                    //    //    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", qr, objQR);
                    //    //}
                    //}
                    ////productInfo.LCDCode = (jsonStr["glassCode"] ?? "").ToString(); //玻璃码
                    ////productInfo.BLCode = (jsonStr["backCode"] ?? "").ToString();//BL编码
                    ////productInfo.FPCCode = (jsonStr["fpcCode"] ?? "").ToString();//fpc编码
                    ////productInfo.TPCode = (jsonStr["tpCode"] ?? "").ToString();//tp编码
                    ////productInfo.ICCode = (jsonStr["icCode"] ?? "").ToString();
                    ////productInfo.QRCode = (jsonStr["intactCode"] ?? "").ToString();//54位产品编码
                    ////productInfo.TPState = (jsonStr["tpState"] ?? "0").ToString();//TP是否有码
                }
                catch (Exception ex)
                {
                    LogHelper.Error("玻璃" + lcd + "的成品型号修改失败", ex);
                    continue;
                }
            }

            e.Result = "ok";
        }

        private void bgwModelCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddLog("修改工单玻璃成品型号", "工单" + txtOrder.Text + "所有已投产玻璃的成品型号修改为" + txtModelCode.Text);
            MessageBox.Show("修改成功！");
        }

        private void bgwModelCode_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = Convert.ToInt32(e.UserState);
            lblModelCode.Text = string.Format("{0} / {1}", i + 1, lstLCD.Count);
            pgsModelCode.Value = i;
        }
        #endregion 修改工单所有已投入玻璃的成品型号

        private void btnGlassList_Click(object sender, EventArgs e)
        {
            string order = txtOrder.Text;
            List<string> lstLCD = GetHBaseDataClass.Instance.GetLCDByOrder(order);
            pgsGlassList.Maximum = lstLCD.Count;
            pgsGlassList.Step = 1;
            pgsGlassList.Value = 0;
            bgwGlassList = new BackgroundWorker();
            bgwGlassList.WorkerReportsProgress = true;
            bgwGlassList.DoWork += new DoWorkEventHandler(bgwGlassList_DoWork);
            bgwGlassList.ProgressChanged += new ProgressChangedEventHandler(bgwGlassList_ProgressChanged);
            bgwGlassList.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwGlassList_RunWorkerCompleted);
            bgwGlassList.RunWorkerAsync(lstLCD);
        }

        private void bgwGlassList_DoWork(object sender, DoWorkEventArgs e)
        {
            lstLCD = e.Argument as List<string>;
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("玻璃编码");
            dt.Columns.Add("FPC编码");
            dt.Columns.Add("TP编码");
            dt.Columns.Add("背光编码");
            dt.Columns.Add("成品编码");

            for (int i = 0; i < lstLCD.Count; i++)
            {
                if (lstLCD.Count != 0)
                {
                    bgwGlassList.ReportProgress(100 * i / lstLCD.Count, i);
                }

                string lcd = lstLCD[i];
                try
                {
                    dynamic objLCD = GetHBaseDataClass.Instance.GetData01(lcd);
                    string fpc = objLCD.fpcCode;
                    string tp = objLCD.tpCode;
                    string bl = objLCD.backCode;
                    string qr = objLCD.intactCode;
                    dt.Rows.Add(new string[] { (i + 1).ToString(), lcd, fpc, tp, bl, qr });
                }
                catch (Exception ex)
                {
                    LogHelper.Error(lcd, ex);
                    continue;
                }
            }

            //ExcelHelper.ExportExcel(dt, "工单玻璃明细");
            e.Result = dt;
        }

        private void bgwGlassList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                DataTable dt = e.Result as DataTable;
                ExcelHelper.ExportExcel(dt, "工单玻璃明细");
            }
        }

        private void bgwGlassList_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = Convert.ToInt32(e.UserState);
            lblGlassList.Text = string.Format("{0} / {1}", i + 1, lstLCD.Count);
            pgsGlassList.Value = i;
        }


    }
}
