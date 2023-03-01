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

namespace Utils
{
    public partial class frmLogin : Form
    {
        #region 字段
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        #endregion 字段

        #region 构造函数
        public frmLogin()
        {
            InitializeComponent();
            this.picHead.Image = imageList1.Images["user"];
        }
        #endregion 构造函数

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtAccount.Focus();
            txtAccount.Focus();
        }
        /// <summary>
        /// 账号输入框按下回车,跳转到密码输入框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // 分析处理数据
                AddScanData(txtAccount.Text.Trim().Replace("\r", "").Replace("\n", ""));
                btnLogin.PerformClick();
            }
        }
        /// <summary>
        /// 密码输入框按下回车键，执行登录操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // 分析处理数据
                AddScanData(txtAccount.Text.Trim().Replace("\r", "").Replace("\n", ""));
                btnLogin.PerformClick();
            }
        }

        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtAccount.Text == "")
            {
                lblMsg.Text = "请输入账号";
                txtAccount.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if (txtPassword.Text == "")
            {
                lblMsg.Text = "请输入密码";
                txtPassword.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
        }

        /// <summary>
        /// 界面关闭串口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //CloseDeviceCom();
        }

        /// <summary>
        /// 分析处理扫描枪数据
        /// </summary>
        /// <param name="DataString">扫描枪接收字符串</param>
        private void AddScanData(string DataString)
        {
            try
            {
                string[] ds = DataString.Split('|');
                if (ds.Length > 0)
                {
                    // 如果输入字符串是人员信息，则解析出账号密码并填充文本框
                    if (ds[0].Replace("\r", "").Replace("\n", "") == "Person")
                    {
                        txtAccount.Text = ds[1].ToString().Replace("\r", "").Replace("\n", "");
                        txtPassword.Text = YJ.Common.AES.Decode(ds[2].ToString().Replace("\r", "").Replace("\n", ""), "yj888128");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
