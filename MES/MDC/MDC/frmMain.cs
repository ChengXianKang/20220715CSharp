using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Utils;

namespace MDC
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 当前程序版本
        /// </summary>
        private string versionName;
        /// <summary>
        /// 是否注销登录
        /// </summary>
        private bool isLogout = false;
        /// <summary>
        /// 条码绑定模式
        /// </summary>
        private string bindingType = null;
        /// <summary>
        /// 人工站点窗口
        /// </summary>
        //public static frmScan Scan;
        public static frmManualScan Scan;
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private static AppConfig appConfig = new AppConfig();


        /// <summary>
        /// 构造函数
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            llblOP.Text = "Hi! " + BaseUI.UI_BOOLOGNAME ?? "";
            Init();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("img/Main.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.ScrollControlIntoView(flowLayoutPanel1.Controls[0]);
            }
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
        //        return cp;
        //    }
        //}
        /// <summary>
        /// 窗口初始化
        /// </summary>
        private void Init()
        {
            // 当前程序版本
            this.versionName = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // 设置程序标题
            this.Text = string.Format("生产数据采集管理系统  {0}", this.versionName);

            // 人工过站点，过站补扫点
            if (BaseUI.UI_RIGHT.ContainsKey("021202") || BaseUI.UI_RIGHT.ContainsKey("021203"))
            {
                btnManualScan.Visible = true;
            }
            else
            {
                btnManualScan.Visible = false;
            }
            // 自动包装
            if (BaseUI.UI_RIGHT.ContainsKey("021221"))
            {
                btnAutoPackage.Visible = true;
            }
            else
            {
                btnAutoPackage.Visible = false;
            }

            // 自动过站点
            if (BaseUI.UI_RIGHT.ContainsKey("021201"))
            {
                btnAutoScan.Visible = true;
            }
            else
            {
                btnAutoScan.Visible = false;
            }
            // 单片追溯
            if (BaseUI.UI_RIGHT.ContainsKey("021206"))
            {
                btnInfo.Visible = true;
            }
            else
            {
                btnInfo.Visible = false;
            }
            // 不良申报
            if (BaseUI.UI_RIGHT.ContainsKey("021212"))
            {
                btnNG.Visible = true;
            }
            else
            {
                btnNG.Visible = false;
            }
            // 复判站点
            if (BaseUI.UI_RIGHT.ContainsKey("021204"))
            {
                btnReview.Visible = true;
            }
            else
            {
                btnReview.Visible = false;
            }
            // 重工解绑
            if (BaseUI.UI_RIGHT.ContainsKey("021205"))
            {
                btnRework.Visible = true;
            }
            else
            {
                btnRework.Visible = false;
            }
            // 配料看板
            if (BaseUI.UI_RIGHT.ContainsKey("021210"))
            {
                btnDeliveryBoard.Visible = true;
            }
            else
            {
                btnDeliveryBoard.Visible = false;
            }
            // Cell车间发料
            if (BaseUI.UI_RIGHT.ContainsKey("021208"))
            {
                btnGlassDelivery.Visible = true;
            }
            else
            {
                btnGlassDelivery.Visible = false;
            }
            // 车间收料
            if (BaseUI.UI_RIGHT.ContainsKey("021211"))
            {
                btnDeliveryCheck.Visible = true;
            }
            else
            {
                btnDeliveryCheck.Visible = false;
            }
            // 产线投料
            if (BaseUI.UI_RIGHT.ContainsKey("021209"))
            {
                btnGlassReceive.Visible = true;
            }
            else
            {
                btnGlassReceive.Visible = false;
            }
            // 华为上传
            if (BaseUI.UI_RIGHT.ContainsKey("021207"))
            {
                btnDataPush.Visible = true;
            }
            else
            {
                btnDataPush.Visible = false;
            }
            // 物流看板
            if (BaseUI.UI_RIGHT.ContainsKey("021213"))
            {
                btnMaterialProgress.Visible = true;
            }
            else
            {
                btnMaterialProgress.Visible = false;
            }
            // 过站明细
            if (BaseUI.UI_RIGHT.ContainsKey("021214"))
            {
                btnReport.Visible = true;
            }
            else
            {
                btnReport.Visible = false;
            }
            // 工单异常处理
            if (BaseUI.UI_RIGHT.ContainsKey("021215"))
            {
                btnOrderException.Visible = true;
            }
            else
            {
                btnOrderException.Visible = false;
            }
            // 自动过站配置
            if (BaseUI.UI_RIGHT.ContainsKey("021216"))
            {
                btnAutoScanConfig.Visible = true;
            }
            else
            {
                btnAutoScanConfig.Visible = false;
            }
            // 机台参数配置
            if (BaseUI.UI_RIGHT.ContainsKey("021222"))
            {
                btnWarnConfig.Visible = true;
            }
            else
            {
                btnWarnConfig.Visible = false;
            }
            // 智慧仓储看板
            if (BaseUI.UI_RIGHT.ContainsKey("021217"))
            {
                btnStorage.Visible = true;
                btnOrderStock.Visible = true;
            }
            else
            {
                btnStorage.Visible = false;
                btnOrderStock.Visible = false;
            }

            // 客退重工
            if (BaseUI.UI_RIGHT.ContainsKey("021218"))
            {
                btnReworkInput.Visible = true;
            }
            else
            {
                btnReworkInput.Visible = false;
            }

            // 读转喷
            if (BaseUI.UI_RIGHT.ContainsKey("021219"))
            {
                btnPrinter.Visible = true;
            }
            else
            {
                btnPrinter.Visible = false;
            }

            // 返修过站
            if (BaseUI.UI_RIGHT.ContainsKey("021220"))
            {
                btnReworkScan.Visible = true;
            }
            else
            {
                btnReworkScan.Visible = false;
            }

            // TP测试
            if (BaseUI.UI_RIGHT.ContainsKey("021224"))
            {
                btnTpTest.Visible = true;
            }
            else
            {
                btnTpTest.Visible = false;
            }

            // 烧录电测
            if (BaseUI.UI_RIGHT.ContainsKey("021225"))
            {
                btnOTP.Visible = true;
            }
            else
            {
                btnOTP.Visible = false;
            }

            // 老化过站
            if (BaseUI.UI_RIGHT.ContainsKey("021223"))
            {
                btn_OldStation.Visible = true;
            }
            else
            {
                btn_OldStation.Visible = false;
            }
            //治具维修
            if (BaseUI.UI_RIGHT.ContainsKey("021226"))
            {
               btnFixtureRepair.Visible = true;
            }
            else
            {
                btnFixtureRepair.Visible = false;
            }
        }

        #region 按钮事件

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.isLogout = true;
            this.Close();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.isLogout = isLogout;
        }


        #endregion

        #region MouseDown MouseUp
        /// <summary>
        /// 鼠标按下时，切换按钮图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string tagRight = ((Button)sender).Tag.ToString();
                ((Button)sender).ImageKey = tagRight + "1";
                ((Button)sender).FlatAppearance.BorderColor = Color.FromArgb(41, 57, 85);
            }
            catch (Exception exp)
            {
                LogHelper.Error("失败.|" + exp.Message, exp);
            }
        }

        /// <summary>
        /// 鼠标弹起时，还原按钮图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string tagRight = ((Button)sender).Tag.ToString();
                ((Button)sender).ImageKey = tagRight + "0";
                ((Button)sender).FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
            }
            catch (Exception exp)
            {
                LogHelper.Error("失败.|" + exp.Message, exp);
            }
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderColor = Color.OrangeRed;
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        #endregion

        /// <summary>
        /// 人工过站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScan_Click(object sender, EventArgs e)
        {
            //Scan = new frmScan();
            Scan = new frmManualScan();
            this.Hide();
            Scan.ShowDialog();
            this.Show();
            btnManualScan.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 自动包装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoPackage_Click(object sender, EventArgs e)
        {
            frmAutoPackage autoPackage = new frmAutoPackage();
            this.Hide();
            autoPackage.ShowDialog();
            this.Show();
            btnAutoPackage.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 不良申报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNG_Click(object sender, EventArgs e)
        {
            frmNGScan NG = new frmNGScan();
            this.Hide();
            NG.ShowDialog();
            this.Show();
            btnNG.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 重工复判
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReview_Click(object sender, EventArgs e)
        {
            frmNGReview Review = new frmNGReview();
            this.Hide();
            Review.ShowDialog();
            this.Show();
            btnReview.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 重工解绑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRework_Click(object sender, EventArgs e)
        {
            frmNGRework Rework = new frmNGRework();
            this.Hide();
            Rework.ShowDialog();
            this.Show();
            btnRework.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 自动过站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoScan_Click(object sender, EventArgs e)
        {
            if(this.bindingType == null)
            {
                if (appConfig.ContainsKey("BindingType"))
                {
                    this.bindingType = appConfig.GetConfigString("BindingType");
                }
            }
            if(this.bindingType == "1")
            {
                frmBinding Binding = new frmBinding();
                this.Hide();
                Binding.ShowDialog();
            }
            else
            {
                frmAuto AutoScan = new frmAuto();
                this.Hide();
                AutoScan.ShowDialog();
            }
            this.Show();
            btnAutoScan.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 单片追溯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            frmInfo Info = new frmInfo();
            this.Hide();
            Info.ShowDialog();
            this.Show();
            btnInfo.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 华为上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataPush_Click(object sender, EventArgs e)
        {
            frmDataPush DataPush = new frmDataPush();
            this.Hide();
            DataPush.ShowDialog();
            this.Show();
            btnDataPush.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 产线投料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGlassDelivery_Click(object sender, EventArgs e)
        {
            frmGlassDelivery GlassDelivery = new frmGlassDelivery("Delivery");
            this.Hide();
            GlassDelivery.ShowDialog();
            this.Show();
            btnGlassDelivery.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 机台上料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGlassReceive_Click(object sender, EventArgs e)
        {
            frmGlassDelivery GlassReceive = new frmGlassDelivery("Receive");
            this.Hide();
            GlassReceive.ShowDialog();
            this.Show();
            btnGlassReceive.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 投料看板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeliveryBoard_Click(object sender, EventArgs e)
        {
            frmDeliveryBoard DeliveryBoard = new frmDeliveryBoard();
            this.Hide();
            DeliveryBoard.ShowDialog();
            this.Show();
            btnDeliveryBoard.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 产线接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeliveryCheck_Click(object sender, EventArgs e)
        {
            frmGlassDelivery DeliveryCheck = new frmGlassDelivery("Check");
            this.Hide();
            DeliveryCheck.ShowDialog();
            this.Show();
            btnDeliveryCheck.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 点击用户名，修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblOP_Click(object sender, EventArgs e)
        {
            new dlgPSW().ShowDialog(this);
        }
        /// <summary>
        /// 修改密码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPsw_Click(object sender, EventArgs e)
        {
            new dlgPSW().ShowDialog(this);
        }

        private void btnMaterialProgress_Click(object sender, EventArgs e)
        {
            frmMaterialProgress MaterialProgress = new frmMaterialProgress();
            this.Hide();
            MaterialProgress.ShowDialog();
            this.Show();
            btnMaterialProgress.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 过站明细按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmMonitor Report = new frmMonitor();
            this.Hide();
            Report.ShowDialog();
            this.Show();
            btnReport.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 工单异常处理按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrderException_Click(object sender, EventArgs e)
        {
            frmOrderException OrderException = new frmOrderException();
            this.Hide();
            OrderException.ShowDialog();
            this.Show();
            btnOrderException.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 分拣配置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWarnConfig_Click(object sender, EventArgs e)
        {
            frmWarnConfig WarnConfig = new frmWarnConfig();
            this.Hide();
            WarnConfig.ShowDialog();
            this.Show();
            btnWarnConfig.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 自动过站配置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoScanConfig_Click(object sender, EventArgs e)
        {
            frmAutoScanConfig AutoScanConfig = new frmAutoScanConfig();
            this.Hide();
            AutoScanConfig.ShowDialog();
            this.Show();
            btnAutoScanConfig.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 仓储看板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStorage_Click(object sender, EventArgs e)
        {
            frmStorage Storage = new frmStorage();
            this.Hide();
            Storage.ShowDialog();
            this.Show();
            btnStorage.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 客退重工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReworkInput_Click(object sender, EventArgs e)
        {
            frmReworkInput ReworkInput = new frmReworkInput();
            this.Hide();
            ReworkInput.ShowDialog();
            this.Show();
            btnReworkInput.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 库存工单管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrderStock_Click(object sender, EventArgs e)
        {
            frmOrderStock OrderStock = new frmOrderStock();
            this.Hide();
            OrderStock.ShowDialog();
            this.Show();
            btnOrderStock.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        private void btnPrinter_Click(object sender, EventArgs e)
        {
            frmLCDPrinter LCDPrinter = new frmLCDPrinter();
            this.Hide();
            LCDPrinter.ShowDialog();
            this.Show();
            btnPrinter.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        /// <summary>
        /// 返修过站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReworkScan_Click(object sender, EventArgs e)
        {
            frmNGReworkScan ReworkScan = new frmNGReworkScan();
            this.Hide();
            ReworkScan.ShowDialog();
            this.Show();
            btnReworkScan.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// TP测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTpTest_Click(object sender, EventArgs e)
        {
            frmTpTest TpTest = new frmTpTest();
            this.Hide();
            TpTest.ShowDialog();
            this.Show();
            btnTpTest.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }

        private void btnOTP_Click(object sender, EventArgs e)
        {
            frmOTP OTP = new frmOTP();
            this.Hide();
            OTP.ShowDialog();
            this.Show();
            btnOTP.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 老化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OldStation_Click(object sender, EventArgs e)
        {
            frmOlded old = new frmOlded();
            this.Hide();
            old.ShowDialog();
            this.Show();
            btn_OldStation.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
        /// <summary>
        /// 治具维修
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFixtureRepair_Click(object sender, EventArgs e)
        {
            FixtureRepair fix = new FixtureRepair();
            this.Hide();
            fix.ShowDialog();
            this.Show();
            btnFixtureRepair.FlatAppearance.BorderColor = Color.FromArgb(188, 199, 216);
        }
    }
}
