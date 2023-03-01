using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Speech.Synthesis;
using System.Media;

namespace Utils
{
    public partial class WaitMessage : Form
    {
        private int second;     //等待超时时间（单位:s）
        private int counter;    //计数器
        private string msg;     //提示信息

        /// <summary>
        /// 等待提示框（自动关闭）
        /// </summary>
        /// <param name="message"></param>
        /// <param name="autoCloseMS"></param>
        public WaitMessage(string message, int autoCloseMS)
        {
            InitializeComponent();
            this.second = autoCloseMS;
            this.counter = 0;
            this.msg = message;
            // 显示消息
            this.labelMessage.Text = message;
            // 初始化按钮的文本
            this.buttonOK.Text = string.Format("确定({0})", this.second);
            // 激活并启动timer，设置timer的触发间隔为500毫秒
            this.timer1.Interval = 1000;
            this.timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.counter++;
            this.buttonOK.Text = string.Format("确定({0})", this.second - this.counter);

            if (this.counter >= this.second)
            {
                this.timer1.Stop();
                this.labelMessage.Text = "操作超时，请重新扫码！";
                PlaySuccess("1");
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
                }
                else if (result == "1" && playSuccess.Contains("Fail"))//失败 
                {
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\失败.wav";
                }
                else if (result == "2" && playSuccess.Contains("Warning"))//警告
                {
                    p.SoundLocation = Application.StartupPath + @"\\Pic\\警告.wav";
                }
                p.Load();
                p.Play();
            }
            catch (Exception ex)
            {
                LogHelper.Error("提示音播放失败|" + ex.Message, ex);
            }
        }

        private void buttonOK_MouseClick(object sender, MouseEventArgs e)
        {
            //if (this.counter < this.second)
            //{
            //    return;
            //}
            // 单击确定按钮，关闭对话框
            this.timer1.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        private void WaitMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();
        }
    }
}
