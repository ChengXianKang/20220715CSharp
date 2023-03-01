using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils.HWDataPush;
using Utils.HWDataPush.Entity;

namespace MDC
{
    public partial class frmDataPushConfig : Form
    {
        public static string CONFIG_GROUP = YJ.Common.Utils.GetAppConfig("ConfigGroup", "appSettings");//配置组名称，暂时硬编码，需求有变动或需要测试时可以将此配置转移到配置文件

        public frmDataPushConfig()
        {
            InitializeComponent();
        }

        private void frmDataPushConfig_Load(object sender, EventArgs e)
        {
            try
            {
                var config = HuaweiDataSender.Current.Config;

                txtGroupName.Text = config.GroupName;
                txtDataType.Text = config.DataType;
                txtFactoryName.Text = config.FactoryName;
                txtSupplierName.Text = config.SupplierName;
                txtAppKey.Text = config.AppKey;
                txtTokenURL.Text = config.TokenURL;
                txtTokenKey.Text = config.TokenKey;
                txtTokenSecur.Text = config.TokenSecur;
                txtApiURL.Text = config.ApiURL;
                txtCustomerNumber.Text = config.CustomerNumber;
            }
            catch (HuaweiConfigExecption hex)
            {
                MessageBox.Show(hex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取配置发生错误！" + ex.Message);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            HuaweiPushDataConfig data = new HuaweiPushDataConfig();
            data.GroupName = txtGroupName.Text.Trim();
            data.DataType = txtDataType.Text.Trim();
            data.FactoryName = txtFactoryName.Text.Trim();
            data.SupplierName = txtSupplierName.Text.Trim();
            data.AppKey = txtAppKey.Text;
            data.TokenURL = txtTokenURL.Text;
            data.TokenKey = txtTokenKey.Text;
            data.TokenSecur = txtTokenSecur.Text;
            data.ApiURL = txtApiURL.Text;
            data.CustomerNumber = txtCustomerNumber.Text;
            try
            {
                HuaweiDataSender.Current.SaveConfig(data.GroupName, data);
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置发生错误！" + ex.Message);
            }
        }
    }
}
