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
using Utils;
using Utils.HBaseClass;
using Utils.Model;

namespace MDC
{
    public partial class frmLcdReInput : Form
    {
        public frmLcdReInput()
        {
            InitializeComponent();
        }

        private void txtScanCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtScanCode.Text != "")
            {
                // 分析处理数据
                ScanDataHandler(txtScanCode.Text.Trim());
            }
        }

        private void frmLcdReInput_Load(object sender, EventArgs e)
        {
            txtInfo.ReadOnly = true;
            // 绑定串口数据接收事件处理程序
            Program.ScanDevice.DataReceived += new Utils.DataReceivedEventHandler(ScanDevice_DataReceived);
            txtScanCode.Focus();
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
                //ShowResult(NoteState.Error, Program.ScanDevice.PortName, "扫描设备接收数据失败." + exp.Message.ToString());
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
                if (true)
                {
                    if (this.InvokeRequired)
                    {
                        ScanDataHandlerCallback d = new ScanDataHandlerCallback(ScanDataHandler);
                        this.BeginInvoke(d, new object[] { DataString });
                    }
                    else
                    {
                        txtInfo.Clear();
                        string code = txtScanCode.Text;
                        code = code.Replace('|', '/');
                        txtInfo.Text += "扫描码：" + code + "\r\n";
                        dynamic glassInfo = GetHBaseDataClass.Instance.GetDataValueByKey(code, "data_01");
                        if(glassInfo == null)
                        {
                            txtInfo.Text += "未找到玻璃信息！\r\n";
                            return;
                        }
                        code = glassInfo.glassCode;
                        txtInfo.Text += "玻璃码：" + code + "\r\n";

                        List<string> keys01 = new List<string>();
                        List<string> keys02 = new List<string>();
                        List<string> keys06 = new List<string>();

                        keys01 = GetHBaseDataClass.Instance.GetRowKeys(code, "data_01");
                        keys02 = GetHBaseDataClass.Instance.GetRowKeys(code, "data_02");
                        keys06 = GetHBaseDataClass.Instance.GetRowKeys(code, "data_06");

                        //第一种情况，data_01有数据，data_02和data_06没数据
                        if (keys01.Count > 0 && (keys02.Count == 0 || keys06.Count == 0))
                        {
                            if (GetHBaseDataClass.Instance.DeleteRowKey(code, "data_01"))
                            {
                                txtInfo.Text += "已删除玻璃信息，请重新补扫LCD投入！\r\n";
                            }
                            else
                            {
                                txtInfo.Text += "处理失败，请记录该玻璃码，联系MES组员！";
                            }
                        }
                        else if(keys01.Count == 1 && keys02.Count > 0 && keys06.Count == 1)
                        {
                            dynamic jobject = GetHBaseDataClass.Instance.GetDataValueByKey(code, "data_06");
                            if (jobject != null)
                            {
                                txtInfo.Text += jobject.ToString() + "\r\n";
                            }
                            else
                            {
                                txtInfo.Text += "未找到该玻璃信息。\r\n";
                                return;
                            }

                            string logCode = jobject.logCode;
                            if (!logCode.Contains("005"))
                            {
                                jobject.logCode = "005," + logCode.TrimStart(',');
                            }

                            string fpc = jobject.fpcCode;
                            if (fpc != "" && !logCode.Contains("008"))
                            {
                                jobject.logCode = logCode.TrimEnd(',') + ",008";
                            }

                            string state = jobject.lcdState;
                            if (state == "")
                            {
                                jobject.lcdState = "良品";
                            }

                            if (GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_06", code, jobject))
                            {
                                txtInfo.Text += jobject.ToString();
                                txtInfo.Text += "\r\n修改成功！\r\n";
                            }
                            else
                            {
                                txtInfo.Text += "修改失败，请重试！\r\n";
                            }
                        }
                        txtScanCode.Clear();
                        txtScanCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据解析失败", ex);
                //ShowResult(NoteState.Error, "", "数据解析失败." + ex.Message.ToString());
            }
        }

        private void frmLcdReInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtScanCode2.Text != "")
            {
                // 分析处理数据
                ScanDataHandler2(txtScanCode2.Text.Trim());
            }
        }


        /// <summary>
        /// 分析处理扫描数据的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ScanDataHandlerCallback2(string DataString);
        /// <summary>
        /// 分析处理扫描数据
        /// </summary>
        /// <param name="DataString">扫描枪接收字符串</param>
        private void ScanDataHandler2(string DataString)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (true)
                {
                    if (this.InvokeRequired)
                    {
                        ScanDataHandlerCallback2 d = new ScanDataHandlerCallback2(ScanDataHandler2);
                        this.BeginInvoke(d, new object[] { DataString });
                    }
                    else
                    {
                        txtInfo2.Clear();
                        string code = txtScanCode2.Text;
                        code = code.Replace('|', '/');
                        txtInfo2.Text += "扫描码：" + code + "\r\n";
                        dynamic glassInfo = GetHBaseDataClass.Instance.GetDataValueByKey(code, "data_01");
                        if (glassInfo == null)
                        {
                            txtInfo2.Text += "未找到玻璃信息！\r\n";
                            return;
                        }

                        code = glassInfo.glassCode;
                        txtInfo2.Text += "玻璃码：" + code + "\r\n";

                        glassInfo = GetHBaseDataClass.Instance.GetDataValueByKey(code, "data_01");
                        if (glassInfo == null)
                        {
                            txtInfo2.Text += "未找到玻璃信息！\r\n";
                            return;
                        }

                        dynamic jobject = GetHBaseDataClass.Instance.GetDataValueByKey(code, "data_06");
                        if (jobject != null)
                        {
                            txtInfo2.Text += jobject.ToString() + "\r\n";
                        }
                        else
                        {
                            txtInfo2.Text += "未找到该玻璃信息。\r\n";
                            return;
                        }

                        string logCode = jobject.logCode;
                        if (!logCode.Contains("018"))
                        {
                            jobject.logCode = logCode.TrimEnd(',') + ",018";
                        }

                        string bl = glassInfo.backCode;
                        if (bl != "" && !logCode.Contains("024"))
                        {
                            jobject.logCode = logCode.TrimEnd(',') + ",024";
                        }
                        else if(bl == "" && logCode.Contains("024"))
                        {
                            jobject.logCode = logCode.Replace(",024", "");
                        }

                        string qr = glassInfo.intactCode;
                        if (qr != "" && !logCode.Contains("039"))
                        {
                            jobject.logCode = logCode.TrimEnd(',') + ",039";
                        }
                        else if (qr == "" && logCode.Contains("039"))
                        {
                            jobject.logCode = logCode.Replace(",039", "");
                        }

                        if (GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_06", code, jobject))
                        {
                            txtInfo2.Text += jobject.ToString();
                            txtInfo2.Text += "\r\n修改成功！\r\n";
                        }
                        else
                        {
                            txtInfo2.Text += "\r\n修改失败，请重试！\r\n";
                        }
                        txtScanCode2.Clear();
                        txtScanCode2.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据解析失败", ex);
                //ShowResult(NoteState.Error, "", "数据解析失败." + ex.Message.ToString());
            }
        }
    }
}
