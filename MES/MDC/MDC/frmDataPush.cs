using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils.HBaseClass;
using Utils;
using Utils.HWDataPush;
using Utils.HWDataPush.Entity;

namespace MDC
{
    public partial class frmDataPush : Form
    {
        ///// <summary>
        ///// HBase数据库操作类
        ///// </summary>
        //private GetHBaseDataClass GHDC;
        ///// <summary>
        ///// 玻璃信息
        ///// </summary>
        //private Dictionary<string, string> dicGlassInfo;
        /// <summary>
        /// 最大日志行数
        /// </summary>
        private int maxLogCount;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmDataPush()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDataPush_Load(object sender, EventArgs e)
        {
            // 最大日志行数
            maxLogCount = appConfig.GetConfigInt("MaxLogCount");
        }

        /// <summary>
        /// 单次发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManualSend_Click(object sender, EventArgs e)
        {
            if (cmbProcess.SelectedIndex == -1)
            {
                MessageBox.Show("请选择发送哪个工序的数据。");
                return;
            }
            if (txtCount.Text == "")
            {
                MessageBox.Show("请设置单次发送的玻璃数量。");
                return;
            }
            string processCode = "";
            int count = 1;
            switch (cmbProcess.SelectedIndex)
            {
                case 0:
                    processCode = "006";
                    break;
                case 1:
                    processCode = "008";
                    break;
                case 2:
                    processCode = "009";
                    break;
                case 3:
                    processCode = "011";
                    break;
                case 4:
                    processCode = "024";
                    break;
                case 5:
                    processCode = "031";
                    break;
                case 6:
                    processCode = "034";
                    break;
                case 7:
                    processCode = "080";
                    break;
                case 8:
                    processCode = "";
                    break;
            }
            count = Convert.ToInt32(txtCount.Text);
            if (!bgwManualSend.IsBusy)
            {
                bgwManualSend.RunWorkerAsync(new object[] { count, processCode });
            }
        }

        /// <summary>
        /// 单次发送后台处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwManualSend_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int count = 1;
                string process = "";
                if (e.Argument != null)
                {
                    object[] obj = e.Argument as object[];
                    count = Convert.ToInt32(obj[0]);
                    process = obj[1].ToString();
                }

                var res = HuaweiDataSender.Current.PushData(count, process);
                if (res.ResultCode != 0)
                {
                    e.Result = "数据错误        Error: " + res.ResultMessage;
                }
                else
                {
                    e.Result = string.Format("发送成功        本次发送了{0}条数据", res.SuccessedCount);
                }
            }
            catch (Exception ex)
            {
                e.Result = "发送失败        Error: " + ex.Message;
            }

        }

        /// <summary>
        /// 单次发送后台处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwManualSend_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null) return;
            string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "        " + e.Result.ToString() + "\r\n";
            txtNote.AppendText(msg);
        }

        /// <summary>
        /// 循环发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoSend_Click(object sender, EventArgs e)
        {
            if (btnAutoSend.Tag.ToString() == "start")
            {
                if (cmbProcess.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择发送哪个工序的数据。");
                    return;
                }
                if (txtCount.Text == "")
                {
                    MessageBox.Show("请设置单次发送的玻璃数量。");
                    return;
                }
                if (txtTime.Text == "")
                {
                    MessageBox.Show("请设置循环发送的间隔时间。");
                    return;
                }
                
                btnAutoSend.Tag = "stop";
                btnAutoSend.Text = "停止发送";


                tmrAutoSend.Interval = 1000 * Convert.ToInt32(txtTime.Text);
                tmrAutoSend.Start();
            }
            else
            {
                btnAutoSend.Tag = "start";
                btnAutoSend.Text = "开始发送";
            }
        }

        /// <summary>
        /// 循环发送定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrAutoSend_Tick(object sender, EventArgs e)
        {
            tmrAutoSend.Stop();
            if (btnAutoSend.Tag.ToString() == "start") return;
            string processCode = "";
            int count = 1;
            switch (cmbProcess.SelectedIndex)
            {
                case 0:
                    processCode = "006";
                    break;
                case 1:
                    processCode = "008";
                    break;
                case 2:
                    processCode = "009";
                    break;
                case 3:
                    processCode = "011";
                    break;
                case 4:
                    processCode = "024";
                    break;
                case 5:
                    processCode = "031";
                    break;
                case 6:
                    processCode = "034";
                    break;
                case 7:
                    processCode = "080";
                    break;
                case 8:
                    processCode = "";
                    break;
            }
            count = Convert.ToInt32(txtCount.Text);
            if (!bgwAutoSend.IsBusy)
            {
                bgwAutoSend.RunWorkerAsync(new object[] { count, processCode });
            }
        }



        /// <summary>
        /// 获取设备洁净度数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetAirData_Click(object sender, EventArgs e)
        {
            int count = DAL_HuaweiUpdate.Current.GetAirParticleTest();
            string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "        获取数据        获取到" + count.ToString() + "行设备内洁净度测试数据\r\n";
            txtNote.AppendText(msg);
        }

        /// <summary>
        /// 获取机台参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetGlue_Click(object sender, EventArgs e)
        {
            int count = DAL_HuaweiUpdate.Current.GetGlueTest();
            string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "        获取数据        获取到" + count.ToString() + "行机台检测数据\r\n";
            txtNote.AppendText(msg);
        }
        /// <summary>
        /// 获取复判不良数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetProcessFail_Click(object sender, EventArgs e)
        {
            int count = DAL_HuaweiUpdate.Current.GetProcessFail();
            string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "        获取数据        获取到" + count.ToString() + "行复判不良数据\r\n";
            txtNote.AppendText(msg);
        }

        /// <summary>
        /// 查询发送记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                DataTable dt = DAL_HuaweiUpdate.Current.GetUpdateHistory(date);
                dgvHistory.DataSource = dt;
                //dgvHistory.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                LogHelper.Error("上传记录查询失败", ex);
            }
        }

        private void bgwAutoSend_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 1;
            string process = "";
            if (e.Argument != null)
            {
                object[] obj = e.Argument as object[];
                count = Convert.ToInt32(obj[0]);
                process = obj[1].ToString();
            }

            // 获取设备洁净度数据
            int m = DAL_HuaweiUpdate.Current.GetAirParticleTest();
            if (m > 0)
            {
                bgwAutoSend.ReportProgress(0, string.Format("获取数据        获取到{0}行设备内洁净度测试数据", m));
            }

            // 获取机台参数
            int n = DAL_HuaweiUpdate.Current.GetGlueTest();
            if (n > 0)
            {
                bgwAutoSend.ReportProgress(0, string.Format("获取数据        获取到{0}行机台检测数据", n));
            }

            // 获取机台参数
             int k = DAL_HuaweiUpdate.Current.GetProcessFail();
            if (k > 0)
            {
                bgwAutoSend.ReportProgress(0, string.Format("获取数据        获取到{0}行复判不良数据", k));
            }

            try
            {
                // 上传数据
                var res = HuaweiDataSender.Current.PushData(count, process);
                if (res.ResultCode != 0)
                {
                    //MessageBox.Show("Error: " + res.ResultMessage, "发送数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bgwAutoSend.ReportProgress(0, "数据错误        Error: " + res.ResultMessage);
                    //Utils.PublicSub.Delay(10000);
                }
                else
                {
                    //MessageBox.Show(string.Format("发送成功！\n共发送了{0}条数据，共进行了{1}次发送。", res.SuccessedCount, res.SentTimes));
                    bgwAutoSend.ReportProgress(0, string.Format("发送成功        本次发送了{0}条数据", res.SuccessedCount));
                    //RefreshData();
                }

                //Delay(100);
            }
            catch (Exception ex)
            {
                LogHelper.Error("华为数据上传失败", ex);
                bgwAutoSend.ReportProgress(0, "发送失败        Error: " + ex.Message);
            }

            e.Result = "OK";
        }

        private void bgwAutoSend_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;
            string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "        " + e.UserState.ToString() + "\r\n";
            txtNote.AppendText(msg);
            if (this.txtNote.Lines.Length > this.maxLogCount)
            {
                string[] newlines = new string[this.maxLogCount];
                Array.Copy(this.txtNote.Lines, this.txtNote.Lines.Length - this.maxLogCount, newlines, 0, this.maxLogCount);
                this.txtNote.Lines = newlines;
                // 光标移至最后
                this.txtNote.Select(this.txtNote.TextLength, 0);
                //滚动到光标处
                this.txtNote.ScrollToCaret();    //自动显示至最后行
            }
        }

        private void bgwAutoSend_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (btnAutoSend.Tag.ToString() == "stop")
            {
                tmrAutoSend.Start();
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmDataPushConfig config = new frmDataPushConfig();
            config.ShowDialog(this);
        }



    }
}
