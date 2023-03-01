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
    public partial class FailMessage : Form
    {
        // 自动关闭的时间限制，如3为3秒后自动关闭
        private int second;
        // 计数器，用以判断当前窗口弹出后持续的时间
        private int counter;

        /// <summary>
        /// 失败对话框（自动关闭）
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="autoCloseMS">自动关闭时间（秒）</param>
        public FailMessage(string message, int autoClose)
        {
            InitializeComponent();
            // 激活并启动timer，设置timer的触发间隔为1000毫秒（1秒）
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Start();
            PlaySuccess("1");
            //PlayChinese(message);
            // 显示消息
            this.labelMessage.Text = message;
            // 获得时间限制
            this.second = autoClose;
            // 初始化计数器
            this.counter = 0;
            // 初始化按钮的文本
            if (this.second > 990)
            {
                this.buttonOK.Text = string.Format("确定", this.second - this.counter);
            }
            else
            {
                this.buttonOK.Text = string.Format("确定({0})", this.second - this.counter);
            }
        }

        /// <summary>
        /// 失败对话框（手动关闭）
        /// </summary>
        /// <param name="message">提示消息</param>
        public FailMessage(string message)
        {
            InitializeComponent();
            PlaySuccess("1");
            //PlayChinese(message);
            // 显示消息
            this.labelMessage.Text = message;
            // 初始化按钮的文本
            this.buttonOK.Text = "确定";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 每次进定时器后，计数自增1秒
            if (second > 990)
            {
                counter = 0;
            }
            else
            {
                this.counter++;
            }

            // 如果没有到达指定的时间限制
            if (this.counter < this.second)
            {
                // 刷新按钮的文本
                this.buttonOK.Text = string.Format("确定({0})", this.second - this.counter);
                this.Refresh();
            }
            // 如果到达时间限制
            else
            {
                // 关闭timer
                this.timer1.Enabled = false;
                this.timer1.Stop();
                // 关闭对话框
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

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
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\警告.wav";
                    p.Load();
                    p.Play();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("提示音播放失败|" + ex.Message, ex);
            }
        }

        private void buttonOK_MouseClick(object sender, MouseEventArgs e)
        {
            // 单击确定按钮，关闭对话框
            this.timer1.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
