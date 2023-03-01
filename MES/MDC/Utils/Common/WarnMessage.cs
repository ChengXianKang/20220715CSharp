using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Utils
{
    public partial class WarnMessage : Form
    {
        // 自动关闭的时间限制，如3为3秒后自动关闭
        private int second;
        // 计数器，用以判断当前窗口弹出后持续的时间
        private int counter;
        private string DefaultButtonName;

        /// <summary>
        /// 提示框构造函数
        /// </summary>
        /// <param name="message">提示内容文本</param>
        /// <param name="buttons">按钮类型（MessageBoxButtons.OK/MessageBoxButtons.OKCancel）</param>
        /// <param name="autoCloseSecond">自动关闭时间（秒）</param>
        /// <param name="defaultButtonName">自动关闭时的默认执行按钮类别（OK/Cancel）</param>
        public WarnMessage(string message, MessageBoxButtons buttons, int autoCloseSecond, string defaultButtonName)
        {
            InitializeComponent();

            if (buttons == MessageBoxButtons.OK)
            {
                btnCancel.Visible = false;
                DefaultButtonName = "OK";
            }
            else
            {
                btnCancel.Visible = true;
                this.DefaultButtonName = defaultButtonName;
            }

            // 激活并启动timer，设置timer的触发间隔为1000毫秒（1秒）
            this.timer1.Interval = 1000;
            this.timer1.Start();
            PlaySuccess("2");
            //PlayChinese(message);
            // 显示消息
            this.labelMessage.Text = message;
            // 获得时间限制
            this.second = autoCloseSecond;
            // 初始化计数器
            this.counter = 0;

            if (DefaultButtonName == "OK")
            {
                // 初始化按钮的文本
                this.btnOK.Text = string.Format("确定({0})", this.second - this.counter);
            }
            else
            {
                // 初始化按钮的文本
                this.btnCancel.Text = string.Format("取消({0})", this.second - this.counter);
            }
        }

        /// <summary>
        /// 提示框构造函数
        /// </summary>
        /// <param name="message">提示内容文本</param>
        /// <param name="buttons">按钮类型（MessageBoxButtons.OK/MessageBoxButtons.OKCancel）</param>
        public WarnMessage(string message, MessageBoxButtons buttons)
        {
            InitializeComponent();
            // 显示消息
            this.labelMessage.Text = message;
            if (buttons == MessageBoxButtons.OK)
            {
                btnCancel.Visible = false;
            }
            else
            {
                btnCancel.Visible = true;
            }
            PlaySuccess("2");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 计数器自增
            this.counter++;
            // 如果没有到达指定的时间限制
            if (this.counter < this.second)
            {
                // 刷新按钮的文本
                if (DefaultButtonName == "OK")
                {
                    // 初始化按钮的文本
                    this.btnOK.Text = string.Format("确定({0})", this.second - this.counter);
                }
                else
                {
                    // 初始化按钮的文本
                    this.btnCancel.Text = string.Format("取消({0})", this.second - this.counter);
                }
                this.Refresh();
            }
            // 如果到达时间限制
            else
            {
                // 关闭timer
                this.timer1.Enabled = false;
                this.timer1.Stop();
                // 关闭对话框
                if (DefaultButtonName == "OK")
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 关闭timer
            this.timer1.Enabled = false;
            this.timer1.Stop();
            // 单击确定按钮，关闭对话框
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭timer
            this.timer1.Enabled = false;
            this.timer1.Stop();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #region MouseDown  MouseUp

        //播放提示音
        private void PlaySuccess(string result)
        {
            try
            {
                string playSuccess = YJ.Common.Utils.GetAppConfig("PlaySuccess", "appSettings");
                SoundPlayer p = new SoundPlayer();
                if (result == "0" && playSuccess.Contains("Success"))//成功
                {
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\成功.wav";
                    p.Load();
                    p.Play();
                }
                else if (result == "1" && playSuccess.Contains("Fail"))//失败 
                {
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\失败.wav";
                    p.Load();
                    p.Play();
                }
                else if (result == "2" && playSuccess.Contains("Warning"))//警告
                {
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\失败.wav";
                    p.Load();
                    p.Play();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("提示音播放失败|" + ex.Message, ex);
            }
        }
        #endregion
    }
}
