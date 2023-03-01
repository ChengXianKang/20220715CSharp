using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using Utils;
using MDCAutoUpdate;
using System.Text.RegularExpressions;

namespace MDC
{
    public partial class dlgMouldCode : Form
    {
        public string MouldCode;
        private string processCode;

        public dlgMouldCode(string mouldCode, string processCode)
        {
            InitializeComponent();
            this.txtMouldCode.Text = mouldCode;
            this.MouldCode = mouldCode;
            this.processCode = processCode;
        }


        private void dlgMouldCode_Load(object sender, EventArgs e)
        {
            // 绑定串口数据接收事件
            //Program.ScanDevice.DataReceived += this.ScanDevice_DataReceived;
            //Program.ScanDevice.DataReceived += new Utils.DataReceivedEventHandler(ScanDevice_DataReceived);

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
                if (!DataString.Contains("OK:") && !DataString.Contains("ERROR:"))
                {
                    // 分析处理数据
                    AddScanData(DataString);
                }
            }
            catch (Exception exp)
            {
                FailMessage failMsg = new FailMessage("扫描设备接收数据失败!\n" + exp.Message.ToString(), 1000);
                failMsg.ShowDialog(this);
                txtMouldCode.Text = "";
                txtMouldCode.Focus();
                Program.ScanDevice.DiscardBuffer();
                LogHelper.Error("扫描设备接收数据失败!" + exp.Message, exp);
                //Log4netProvider.Logger.Error("扫描设备接收数据失败!" + exp);
            }
        }

        /// <summary>
        /// 分析处理扫描枪数据的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void AddScanDataCallback(string DataString);
        /// <summary>
        /// 分析处理扫描枪数据
        /// </summary>
        /// <param name="DataString">扫描枪接收字符串</param>
        private void AddScanData(string DataString)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.InvokeRequired)
                {
                    AddScanDataCallback d = new AddScanDataCallback(AddScanData);
                    this.BeginInvoke(d, new object[] { DataString });
                }
                else
                {
                    txtMouldCode.Text = DataString;
                    //正则表达式：不能以-开头结尾，至少一个大写字母、数字、横杠、斜杠
                    Regex regex = new Regex("^(?!-)(?!.*?-$)[A-Z0-9-/]+$");
                    if (regex.IsMatch(DataString))
                    {
                        if(this.processCode == "011" && !DataString.Contains("-CFOG-")
                            || this.processCode == "012" && !DataString.Contains("-CFOG/TP-")
                            || this.processCode == "015" && !DataString.Contains("-CFOGTP-")
                            || this.processCode == "027" && !DataString.Contains("-V")
                            || this.processCode == "029" && !DataString.Contains("-ASSY-")
                            || this.processCode == "56" && !DataString.Contains("-ASSYTP-")
                            || this.processCode == "66" && !DataString.Contains("-ASSYOTP-")
                            || this.processCode == "060" && !DataString.Contains("-ASSY-")
                            || this.processCode == "59" && !DataString.Contains("-ASSYTP-"))
                        {
                            txtMouldCode.Focus();
                            // 将光标定位到文本框的最后
                            this.txtMouldCode.SelectionStart = this.txtMouldCode.Text.Length;
                            txtMouldCode.SelectAll();
                            this.lblWarning.Visible = true;
                        }
                        else
                        {
                            this.lblWarning.Visible = false;
                            this.MouldCode = DataString;
                            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                    }
                    else
                    {
                        txtMouldCode.Focus();
                        // 将光标定位到文本框的最后
                        this.txtMouldCode.SelectionStart = this.txtMouldCode.Text.Length;
                        txtMouldCode.SelectAll();
                        this.lblWarning.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                FailMessage failMsg = new FailMessage("扫描设备解析数据失败!\n" + ex.Message.ToString(), 1000);
                failMsg.ShowDialog(this);
                txtMouldCode.Text = "";
                txtMouldCode.Focus();
                LogHelper.Error("扫描设备解析数据失败!" + ex.Message.ToString(), ex);
                //Log4netProvider.Logger.Error("扫描设备解析数据失败!" + ex);
                //Program.ScanDevice.DiscardInBuffer();
                Program.ScanDevice.DiscardBuffer();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AddScanData(txtMouldCode.Text.Trim().Replace("\r", "").Replace("\n", ""));

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void txtMouldCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void dlgMouldCode_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.ScanDevice.DataReceived -= this.ScanDevice_DataReceived;
            //CloseDeviceCom();
        }

        private void dlgMouldCode_Shown(object sender, EventArgs e)
        {
            Program.ScanDevice.DataReceived += new Utils.DataReceivedEventHandler(ScanDevice_DataReceived);
        }
    }
}
