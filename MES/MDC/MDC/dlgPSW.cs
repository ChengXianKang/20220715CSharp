using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace MDC
{
    public partial class dlgPSW : Form
    {
        public dlgPSW()
        {
            InitializeComponent();
        }

        private void dlgPSW_Load(object sender, EventArgs e)
        {
            txtAccount.Text = BaseUI.UI_BOOLOGNAME;
            txtName.Text = BaseUI.UI_BOOPNAME;
            txtPswOld.SelectAll();
            txtPswOld.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (txtPswOld.Text.Trim() == "")
            {
                lblError.Text = "请输入原密码";
                txtPswOld.Focus();
                txtPswOld.SelectAll();
                return;
            }
            if (txtPswNew.Text.Trim() == "")
            {
                lblError.Text = "请输入新密码";
                txtPswNew.Focus();
                txtPswNew.SelectAll();
                return;
            }
            if (txtPswNew2.Text.Trim() == "")
            {
                lblError.Text = "请重复新密码";
                txtPswNew2.Focus();
                txtPswNew2.SelectAll();
                return;
            }
            if (txtPswNew.Text != txtPswNew2.Text)
            {
                lblError.Text = "两次输入不一致";
                txtPswNew2.Focus();
                txtPswNew2.SelectAll();
                return;
            }
           
            // 验证原密码
            LoginState state = BaseUI.ChangePassword(txtAccount.Text.Trim(), txtPswOld.Text, txtPswNew.Text);

            if (state == LoginState.登录成功)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                lblError.Text = state.ToString();
                txtPswOld.Focus();
                txtPswOld.SelectAll();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void txtPswOld_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPswNew.Focus();
                txtPswNew.SelectAll();
            }
        }

        private void txtPswNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPswNew2.Focus();
                txtPswNew2.SelectAll();
            }
        }

        private void txtPswNew2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }


    }
}
