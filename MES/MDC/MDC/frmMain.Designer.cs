namespace MDC
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnReview = new System.Windows.Forms.Button();
            this.imgListButton = new System.Windows.Forms.ImageList(this.components);
            this.btnExit = new System.Windows.Forms.Label();
            this.btnRework = new System.Windows.Forms.Button();
            this.btnManualScan = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAutoScan = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnNG = new System.Windows.Forms.Button();
            this.btnDeliveryBoard = new System.Windows.Forms.Button();
            this.btnGlassDelivery = new System.Windows.Forms.Button();
            this.btnDeliveryCheck = new System.Windows.Forms.Button();
            this.btnGlassReceive = new System.Windows.Forms.Button();
            this.btnDataPush = new System.Windows.Forms.Button();
            this.btnAutoPackage = new System.Windows.Forms.Button();
            this.btnMaterialProgress = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnOrderException = new System.Windows.Forms.Button();
            this.btnWarnConfig = new System.Windows.Forms.Button();
            this.btnAutoScanConfig = new System.Windows.Forms.Button();
            this.btnStorage = new System.Windows.Forms.Button();
            this.btnReworkInput = new System.Windows.Forms.Button();
            this.btnOrderStock = new System.Windows.Forms.Button();
            this.btnPrinter = new System.Windows.Forms.Button();
            this.btnReworkScan = new System.Windows.Forms.Button();
            this.btnTpTest = new System.Windows.Forms.Button();
            this.btnOTP = new System.Windows.Forms.Button();
            this.btn_OldStation = new System.Windows.Forms.Button();
            this.btnFixtureRepair = new System.Windows.Forms.Button();
            this.llblOP = new System.Windows.Forms.LinkLabel();
            this.btnPsw = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReview
            // 
            this.btnReview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReview.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnReview.FlatAppearance.BorderSize = 3;
            this.btnReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReview.ImageKey = "Review0";
            this.btnReview.ImageList = this.imgListButton;
            this.btnReview.Location = new System.Drawing.Point(75, 547);
            this.btnReview.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(216, 216);
            this.btnReview.TabIndex = 33;
            this.btnReview.Tag = "Review";
            this.btnReview.UseVisualStyleBackColor = true;
            this.btnReview.Visible = false;
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
            this.btnReview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnReview.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnReview.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnReview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // imgListButton
            // 
            this.imgListButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListButton.ImageStream")));
            this.imgListButton.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListButton.Images.SetKeyName(0, "ManualScan0");
            this.imgListButton.Images.SetKeyName(1, "ManualScan1");
            this.imgListButton.Images.SetKeyName(2, "AutoScan0");
            this.imgListButton.Images.SetKeyName(3, "AutoScan1");
            this.imgListButton.Images.SetKeyName(4, "Info0");
            this.imgListButton.Images.SetKeyName(5, "Info1");
            this.imgListButton.Images.SetKeyName(6, "Review0");
            this.imgListButton.Images.SetKeyName(7, "Review1");
            this.imgListButton.Images.SetKeyName(8, "Rework0");
            this.imgListButton.Images.SetKeyName(9, "Rework1");
            this.imgListButton.Images.SetKeyName(10, "DataPush0");
            this.imgListButton.Images.SetKeyName(11, "DataPush1");
            this.imgListButton.Images.SetKeyName(12, "DeliveryBoard0");
            this.imgListButton.Images.SetKeyName(13, "DeliveryBoard1");
            this.imgListButton.Images.SetKeyName(14, "GlassDelivery0");
            this.imgListButton.Images.SetKeyName(15, "GlassDelivery1");
            this.imgListButton.Images.SetKeyName(16, "GlassReceive0");
            this.imgListButton.Images.SetKeyName(17, "GlassReceive1");
            this.imgListButton.Images.SetKeyName(18, "DeliveryCheck0");
            this.imgListButton.Images.SetKeyName(19, "DeliveryCheck1");
            this.imgListButton.Images.SetKeyName(20, "NG0");
            this.imgListButton.Images.SetKeyName(21, "NG1");
            this.imgListButton.Images.SetKeyName(22, "AutoPackage0");
            this.imgListButton.Images.SetKeyName(23, "AutoPackage1");
            this.imgListButton.Images.SetKeyName(24, "MaterialProgress0");
            this.imgListButton.Images.SetKeyName(25, "MaterialProgress1");
            this.imgListButton.Images.SetKeyName(26, "Report0");
            this.imgListButton.Images.SetKeyName(27, "Report1");
            this.imgListButton.Images.SetKeyName(28, "OrderException0");
            this.imgListButton.Images.SetKeyName(29, "OrderException1");
            this.imgListButton.Images.SetKeyName(30, "WarnConfig0");
            this.imgListButton.Images.SetKeyName(31, "WarnConfig1");
            this.imgListButton.Images.SetKeyName(32, "AutoScanConfig0");
            this.imgListButton.Images.SetKeyName(33, "AutoScanConfig1");
            this.imgListButton.Images.SetKeyName(34, "Return0");
            this.imgListButton.Images.SetKeyName(35, "Return1");
            this.imgListButton.Images.SetKeyName(36, "Stock0");
            this.imgListButton.Images.SetKeyName(37, "Stock1");
            this.imgListButton.Images.SetKeyName(38, "Storage0");
            this.imgListButton.Images.SetKeyName(39, "Storage1");
            this.imgListButton.Images.SetKeyName(40, "Printer0");
            this.imgListButton.Images.SetKeyName(41, "Printer1");
            this.imgListButton.Images.SetKeyName(42, "ReworkScan0");
            this.imgListButton.Images.SetKeyName(43, "ReworkScan1");
            this.imgListButton.Images.SetKeyName(44, "TpTest0");
            this.imgListButton.Images.SetKeyName(45, "TpTest1");
            this.imgListButton.Images.SetKeyName(46, "OTP0");
            this.imgListButton.Images.SetKeyName(47, "OTP1");
            this.imgListButton.Images.SetKeyName(48, "fixre.png");
            this.imgListButton.Images.SetKeyName(49, "oldha.png");
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(107, 499);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(68, 43);
            this.btnExit.TabIndex = 39;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRework
            // 
            this.btnRework.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRework.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnRework.FlatAppearance.BorderSize = 3;
            this.btnRework.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRework.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRework.ImageKey = "Rework0";
            this.btnRework.ImageList = this.imgListButton;
            this.btnRework.Location = new System.Drawing.Point(321, 547);
            this.btnRework.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnRework.Name = "btnRework";
            this.btnRework.Size = new System.Drawing.Size(216, 216);
            this.btnRework.TabIndex = 46;
            this.btnRework.Tag = "Rework";
            this.btnRework.UseVisualStyleBackColor = true;
            this.btnRework.Visible = false;
            this.btnRework.Click += new System.EventHandler(this.btnRework_Click);
            this.btnRework.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnRework.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnRework.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnRework.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnManualScan
            // 
            this.btnManualScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManualScan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnManualScan.FlatAppearance.BorderSize = 3;
            this.btnManualScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManualScan.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnManualScan.ImageKey = "ManualScan0";
            this.btnManualScan.ImageList = this.imgListButton;
            this.btnManualScan.Location = new System.Drawing.Point(75, 25);
            this.btnManualScan.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnManualScan.Name = "btnManualScan";
            this.btnManualScan.Size = new System.Drawing.Size(216, 216);
            this.btnManualScan.TabIndex = 45;
            this.btnManualScan.Tag = "ManualScan";
            this.btnManualScan.UseVisualStyleBackColor = true;
            this.btnManualScan.Visible = false;
            this.btnManualScan.Click += new System.EventHandler(this.btnScan_Click);
            this.btnManualScan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnManualScan.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnManualScan.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnManualScan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.flowLayoutPanel1.Controls.Add(this.btnManualScan);
            this.flowLayoutPanel1.Controls.Add(this.btnAutoScan);
            this.flowLayoutPanel1.Controls.Add(this.btnInfo);
            this.flowLayoutPanel1.Controls.Add(this.btnNG);
            this.flowLayoutPanel1.Controls.Add(this.btnReview);
            this.flowLayoutPanel1.Controls.Add(this.btnRework);
            this.flowLayoutPanel1.Controls.Add(this.btnDeliveryBoard);
            this.flowLayoutPanel1.Controls.Add(this.btnGlassDelivery);
            this.flowLayoutPanel1.Controls.Add(this.btnDeliveryCheck);
            this.flowLayoutPanel1.Controls.Add(this.btnGlassReceive);
            this.flowLayoutPanel1.Controls.Add(this.btnDataPush);
            this.flowLayoutPanel1.Controls.Add(this.btnAutoPackage);
            this.flowLayoutPanel1.Controls.Add(this.btnMaterialProgress);
            this.flowLayoutPanel1.Controls.Add(this.btnReport);
            this.flowLayoutPanel1.Controls.Add(this.btnOrderException);
            this.flowLayoutPanel1.Controls.Add(this.btnWarnConfig);
            this.flowLayoutPanel1.Controls.Add(this.btnAutoScanConfig);
            this.flowLayoutPanel1.Controls.Add(this.btnStorage);
            this.flowLayoutPanel1.Controls.Add(this.btnReworkInput);
            this.flowLayoutPanel1.Controls.Add(this.btnOrderStock);
            this.flowLayoutPanel1.Controls.Add(this.btnPrinter);
            this.flowLayoutPanel1.Controls.Add(this.btnReworkScan);
            this.flowLayoutPanel1.Controls.Add(this.btnTpTest);
            this.flowLayoutPanel1.Controls.Add(this.btnOTP);
            this.flowLayoutPanel1.Controls.Add(this.btn_OldStation);
            this.flowLayoutPanel1.Controls.Add(this.btnFixtureRepair);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(191, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(60, 0, 20, 60);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(613, 541);
            this.flowLayoutPanel1.TabIndex = 41;
            // 
            // btnAutoScan
            // 
            this.btnAutoScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoScan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnAutoScan.FlatAppearance.BorderSize = 3;
            this.btnAutoScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoScan.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAutoScan.ImageKey = "AutoScan0";
            this.btnAutoScan.ImageList = this.imgListButton;
            this.btnAutoScan.Location = new System.Drawing.Point(321, 25);
            this.btnAutoScan.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnAutoScan.Name = "btnAutoScan";
            this.btnAutoScan.Size = new System.Drawing.Size(216, 216);
            this.btnAutoScan.TabIndex = 47;
            this.btnAutoScan.Tag = "AutoScan";
            this.btnAutoScan.UseVisualStyleBackColor = true;
            this.btnAutoScan.Visible = false;
            this.btnAutoScan.Click += new System.EventHandler(this.btnAutoScan_Click);
            this.btnAutoScan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnAutoScan.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnAutoScan.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnAutoScan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnInfo
            // 
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnInfo.FlatAppearance.BorderSize = 3;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInfo.ImageKey = "Info0";
            this.btnInfo.ImageList = this.imgListButton;
            this.btnInfo.Location = new System.Drawing.Point(75, 286);
            this.btnInfo.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(216, 216);
            this.btnInfo.TabIndex = 48;
            this.btnInfo.Tag = "Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Visible = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            this.btnInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnInfo.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnInfo.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNG
            // 
            this.btnNG.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNG.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnNG.FlatAppearance.BorderSize = 3;
            this.btnNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNG.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNG.ImageKey = "NG0";
            this.btnNG.ImageList = this.imgListButton;
            this.btnNG.Location = new System.Drawing.Point(321, 286);
            this.btnNG.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnNG.Name = "btnNG";
            this.btnNG.Size = new System.Drawing.Size(216, 216);
            this.btnNG.TabIndex = 54;
            this.btnNG.Tag = "NG";
            this.btnNG.UseVisualStyleBackColor = true;
            this.btnNG.Click += new System.EventHandler(this.btnNG_Click);
            this.btnNG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNG.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnNG.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnNG.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnDeliveryBoard
            // 
            this.btnDeliveryBoard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeliveryBoard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnDeliveryBoard.FlatAppearance.BorderSize = 3;
            this.btnDeliveryBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeliveryBoard.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeliveryBoard.ImageKey = "DeliveryBoard0";
            this.btnDeliveryBoard.ImageList = this.imgListButton;
            this.btnDeliveryBoard.Location = new System.Drawing.Point(75, 808);
            this.btnDeliveryBoard.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnDeliveryBoard.Name = "btnDeliveryBoard";
            this.btnDeliveryBoard.Size = new System.Drawing.Size(216, 216);
            this.btnDeliveryBoard.TabIndex = 52;
            this.btnDeliveryBoard.Tag = "DeliveryBoard";
            this.btnDeliveryBoard.UseVisualStyleBackColor = true;
            this.btnDeliveryBoard.Visible = false;
            this.btnDeliveryBoard.Click += new System.EventHandler(this.btnDeliveryBoard_Click);
            this.btnDeliveryBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnDeliveryBoard.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnDeliveryBoard.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnDeliveryBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnGlassDelivery
            // 
            this.btnGlassDelivery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGlassDelivery.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnGlassDelivery.FlatAppearance.BorderSize = 3;
            this.btnGlassDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGlassDelivery.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGlassDelivery.ImageKey = "GlassDelivery0";
            this.btnGlassDelivery.ImageList = this.imgListButton;
            this.btnGlassDelivery.Location = new System.Drawing.Point(321, 808);
            this.btnGlassDelivery.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnGlassDelivery.Name = "btnGlassDelivery";
            this.btnGlassDelivery.Size = new System.Drawing.Size(216, 216);
            this.btnGlassDelivery.TabIndex = 50;
            this.btnGlassDelivery.Tag = "GlassDelivery";
            this.btnGlassDelivery.UseVisualStyleBackColor = true;
            this.btnGlassDelivery.Visible = false;
            this.btnGlassDelivery.Click += new System.EventHandler(this.btnGlassDelivery_Click);
            this.btnGlassDelivery.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnGlassDelivery.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnGlassDelivery.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnGlassDelivery.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnDeliveryCheck
            // 
            this.btnDeliveryCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeliveryCheck.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnDeliveryCheck.FlatAppearance.BorderSize = 3;
            this.btnDeliveryCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeliveryCheck.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeliveryCheck.ImageKey = "DeliveryCheck0";
            this.btnDeliveryCheck.ImageList = this.imgListButton;
            this.btnDeliveryCheck.Location = new System.Drawing.Point(75, 1069);
            this.btnDeliveryCheck.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnDeliveryCheck.Name = "btnDeliveryCheck";
            this.btnDeliveryCheck.Size = new System.Drawing.Size(216, 216);
            this.btnDeliveryCheck.TabIndex = 53;
            this.btnDeliveryCheck.Tag = "DeliveryCheck";
            this.btnDeliveryCheck.UseVisualStyleBackColor = true;
            this.btnDeliveryCheck.Visible = false;
            this.btnDeliveryCheck.Click += new System.EventHandler(this.btnDeliveryCheck_Click);
            this.btnDeliveryCheck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnDeliveryCheck.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnDeliveryCheck.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnDeliveryCheck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnGlassReceive
            // 
            this.btnGlassReceive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGlassReceive.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnGlassReceive.FlatAppearance.BorderSize = 3;
            this.btnGlassReceive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGlassReceive.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGlassReceive.ImageKey = "GlassReceive0";
            this.btnGlassReceive.ImageList = this.imgListButton;
            this.btnGlassReceive.Location = new System.Drawing.Point(321, 1069);
            this.btnGlassReceive.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnGlassReceive.Name = "btnGlassReceive";
            this.btnGlassReceive.Size = new System.Drawing.Size(216, 216);
            this.btnGlassReceive.TabIndex = 51;
            this.btnGlassReceive.Tag = "GlassReceive";
            this.btnGlassReceive.UseVisualStyleBackColor = true;
            this.btnGlassReceive.Visible = false;
            this.btnGlassReceive.Click += new System.EventHandler(this.btnGlassReceive_Click);
            this.btnGlassReceive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnGlassReceive.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnGlassReceive.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnGlassReceive.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnDataPush
            // 
            this.btnDataPush.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDataPush.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnDataPush.FlatAppearance.BorderSize = 3;
            this.btnDataPush.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataPush.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDataPush.ImageKey = "DataPush0";
            this.btnDataPush.ImageList = this.imgListButton;
            this.btnDataPush.Location = new System.Drawing.Point(75, 1330);
            this.btnDataPush.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnDataPush.Name = "btnDataPush";
            this.btnDataPush.Size = new System.Drawing.Size(216, 216);
            this.btnDataPush.TabIndex = 49;
            this.btnDataPush.Tag = "DataPush";
            this.btnDataPush.UseVisualStyleBackColor = true;
            this.btnDataPush.Visible = false;
            this.btnDataPush.Click += new System.EventHandler(this.btnDataPush_Click);
            this.btnDataPush.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnDataPush.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnDataPush.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnDataPush.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnAutoPackage
            // 
            this.btnAutoPackage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoPackage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnAutoPackage.FlatAppearance.BorderSize = 3;
            this.btnAutoPackage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoPackage.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAutoPackage.ImageKey = "AutoPackage0";
            this.btnAutoPackage.ImageList = this.imgListButton;
            this.btnAutoPackage.Location = new System.Drawing.Point(321, 1330);
            this.btnAutoPackage.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnAutoPackage.Name = "btnAutoPackage";
            this.btnAutoPackage.Size = new System.Drawing.Size(216, 216);
            this.btnAutoPackage.TabIndex = 55;
            this.btnAutoPackage.Tag = "AutoPackage";
            this.btnAutoPackage.UseVisualStyleBackColor = true;
            this.btnAutoPackage.Visible = false;
            this.btnAutoPackage.Click += new System.EventHandler(this.btnAutoPackage_Click);
            this.btnAutoPackage.Enter += new System.EventHandler(this.button_MouseEnter);
            this.btnAutoPackage.Leave += new System.EventHandler(this.button_MouseLeave);
            this.btnAutoPackage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnAutoPackage.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnAutoPackage.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnAutoPackage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnMaterialProgress
            // 
            this.btnMaterialProgress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaterialProgress.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnMaterialProgress.FlatAppearance.BorderSize = 3;
            this.btnMaterialProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterialProgress.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMaterialProgress.ImageKey = "MaterialProgress0";
            this.btnMaterialProgress.ImageList = this.imgListButton;
            this.btnMaterialProgress.Location = new System.Drawing.Point(75, 1591);
            this.btnMaterialProgress.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnMaterialProgress.Name = "btnMaterialProgress";
            this.btnMaterialProgress.Size = new System.Drawing.Size(216, 216);
            this.btnMaterialProgress.TabIndex = 56;
            this.btnMaterialProgress.Tag = "MaterialProgress";
            this.btnMaterialProgress.UseVisualStyleBackColor = true;
            this.btnMaterialProgress.Visible = false;
            this.btnMaterialProgress.Click += new System.EventHandler(this.btnMaterialProgress_Click);
            this.btnMaterialProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnMaterialProgress.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnMaterialProgress.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnMaterialProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnReport
            // 
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnReport.FlatAppearance.BorderSize = 3;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReport.ImageKey = "Report0";
            this.btnReport.ImageList = this.imgListButton;
            this.btnReport.Location = new System.Drawing.Point(321, 1591);
            this.btnReport.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(216, 216);
            this.btnReport.TabIndex = 57;
            this.btnReport.Tag = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Visible = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            this.btnReport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnReport.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnReport.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnReport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnOrderException
            // 
            this.btnOrderException.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrderException.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnOrderException.FlatAppearance.BorderSize = 3;
            this.btnOrderException.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderException.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOrderException.ImageKey = "OrderException0";
            this.btnOrderException.ImageList = this.imgListButton;
            this.btnOrderException.Location = new System.Drawing.Point(75, 1852);
            this.btnOrderException.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnOrderException.Name = "btnOrderException";
            this.btnOrderException.Size = new System.Drawing.Size(216, 216);
            this.btnOrderException.TabIndex = 58;
            this.btnOrderException.Tag = "OrderException";
            this.btnOrderException.UseVisualStyleBackColor = true;
            this.btnOrderException.Visible = false;
            this.btnOrderException.Click += new System.EventHandler(this.btnOrderException_Click);
            this.btnOrderException.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnOrderException.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOrderException.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.btnOrderException.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnWarnConfig
            // 
            this.btnWarnConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWarnConfig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnWarnConfig.FlatAppearance.BorderSize = 3;
            this.btnWarnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWarnConfig.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWarnConfig.ImageKey = "WarnConfig0";
            this.btnWarnConfig.ImageList = this.imgListButton;
            this.btnWarnConfig.Location = new System.Drawing.Point(321, 1852);
            this.btnWarnConfig.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnWarnConfig.Name = "btnWarnConfig";
            this.btnWarnConfig.Size = new System.Drawing.Size(216, 216);
            this.btnWarnConfig.TabIndex = 59;
            this.btnWarnConfig.Tag = "WarnConfig";
            this.btnWarnConfig.UseVisualStyleBackColor = true;
            this.btnWarnConfig.Visible = false;
            this.btnWarnConfig.Click += new System.EventHandler(this.btnWarnConfig_Click);
            // 
            // btnAutoScanConfig
            // 
            this.btnAutoScanConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoScanConfig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnAutoScanConfig.FlatAppearance.BorderSize = 3;
            this.btnAutoScanConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoScanConfig.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAutoScanConfig.ImageKey = "AutoScanConfig0";
            this.btnAutoScanConfig.ImageList = this.imgListButton;
            this.btnAutoScanConfig.Location = new System.Drawing.Point(75, 2113);
            this.btnAutoScanConfig.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnAutoScanConfig.Name = "btnAutoScanConfig";
            this.btnAutoScanConfig.Size = new System.Drawing.Size(216, 216);
            this.btnAutoScanConfig.TabIndex = 60;
            this.btnAutoScanConfig.Tag = "AutoScanConfig";
            this.btnAutoScanConfig.UseVisualStyleBackColor = true;
            this.btnAutoScanConfig.Click += new System.EventHandler(this.btnAutoScanConfig_Click);
            // 
            // btnStorage
            // 
            this.btnStorage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStorage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnStorage.FlatAppearance.BorderSize = 3;
            this.btnStorage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStorage.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStorage.ImageKey = "Storage0";
            this.btnStorage.ImageList = this.imgListButton;
            this.btnStorage.Location = new System.Drawing.Point(321, 2113);
            this.btnStorage.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnStorage.Name = "btnStorage";
            this.btnStorage.Size = new System.Drawing.Size(216, 216);
            this.btnStorage.TabIndex = 61;
            this.btnStorage.Tag = "Storage";
            this.btnStorage.UseVisualStyleBackColor = true;
            this.btnStorage.Click += new System.EventHandler(this.btnStorage_Click);
            // 
            // btnReworkInput
            // 
            this.btnReworkInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReworkInput.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnReworkInput.FlatAppearance.BorderSize = 3;
            this.btnReworkInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReworkInput.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReworkInput.ImageKey = "Return0";
            this.btnReworkInput.ImageList = this.imgListButton;
            this.btnReworkInput.Location = new System.Drawing.Point(75, 2374);
            this.btnReworkInput.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnReworkInput.Name = "btnReworkInput";
            this.btnReworkInput.Size = new System.Drawing.Size(216, 216);
            this.btnReworkInput.TabIndex = 62;
            this.btnReworkInput.Tag = "Return";
            this.btnReworkInput.UseVisualStyleBackColor = true;
            this.btnReworkInput.Visible = false;
            this.btnReworkInput.Click += new System.EventHandler(this.btnReworkInput_Click);
            // 
            // btnOrderStock
            // 
            this.btnOrderStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrderStock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnOrderStock.FlatAppearance.BorderSize = 3;
            this.btnOrderStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderStock.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOrderStock.ImageKey = "Stock0";
            this.btnOrderStock.ImageList = this.imgListButton;
            this.btnOrderStock.Location = new System.Drawing.Point(321, 2374);
            this.btnOrderStock.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnOrderStock.Name = "btnOrderStock";
            this.btnOrderStock.Size = new System.Drawing.Size(216, 216);
            this.btnOrderStock.TabIndex = 64;
            this.btnOrderStock.Tag = "Stock";
            this.btnOrderStock.UseVisualStyleBackColor = true;
            this.btnOrderStock.Click += new System.EventHandler(this.btnOrderStock_Click);
            // 
            // btnPrinter
            // 
            this.btnPrinter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrinter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnPrinter.FlatAppearance.BorderSize = 3;
            this.btnPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrinter.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrinter.ImageKey = "Printer0";
            this.btnPrinter.ImageList = this.imgListButton;
            this.btnPrinter.Location = new System.Drawing.Point(75, 2635);
            this.btnPrinter.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnPrinter.Name = "btnPrinter";
            this.btnPrinter.Size = new System.Drawing.Size(216, 216);
            this.btnPrinter.TabIndex = 65;
            this.btnPrinter.Tag = "Printer";
            this.btnPrinter.UseVisualStyleBackColor = true;
            this.btnPrinter.Visible = false;
            this.btnPrinter.Click += new System.EventHandler(this.btnPrinter_Click);
            // 
            // btnReworkScan
            // 
            this.btnReworkScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReworkScan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnReworkScan.FlatAppearance.BorderSize = 3;
            this.btnReworkScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReworkScan.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReworkScan.ImageKey = "ReworkScan0";
            this.btnReworkScan.ImageList = this.imgListButton;
            this.btnReworkScan.Location = new System.Drawing.Point(321, 2635);
            this.btnReworkScan.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnReworkScan.Name = "btnReworkScan";
            this.btnReworkScan.Size = new System.Drawing.Size(216, 216);
            this.btnReworkScan.TabIndex = 66;
            this.btnReworkScan.Tag = "ReworkScan";
            this.btnReworkScan.UseVisualStyleBackColor = true;
            this.btnReworkScan.Visible = false;
            this.btnReworkScan.Click += new System.EventHandler(this.btnReworkScan_Click);
            // 
            // btnTpTest
            // 
            this.btnTpTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTpTest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnTpTest.FlatAppearance.BorderSize = 3;
            this.btnTpTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTpTest.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTpTest.ImageKey = "TpTest0";
            this.btnTpTest.ImageList = this.imgListButton;
            this.btnTpTest.Location = new System.Drawing.Point(75, 2896);
            this.btnTpTest.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnTpTest.Name = "btnTpTest";
            this.btnTpTest.Size = new System.Drawing.Size(216, 216);
            this.btnTpTest.TabIndex = 67;
            this.btnTpTest.Tag = "TpTest";
            this.btnTpTest.UseVisualStyleBackColor = true;
            this.btnTpTest.Visible = false;
            this.btnTpTest.Click += new System.EventHandler(this.btnTpTest_Click);
            // 
            // btnOTP
            // 
            this.btnOTP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOTP.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnOTP.FlatAppearance.BorderSize = 3;
            this.btnOTP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOTP.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOTP.ImageKey = "OTP0";
            this.btnOTP.ImageList = this.imgListButton;
            this.btnOTP.Location = new System.Drawing.Point(321, 2896);
            this.btnOTP.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnOTP.Name = "btnOTP";
            this.btnOTP.Size = new System.Drawing.Size(216, 216);
            this.btnOTP.TabIndex = 68;
            this.btnOTP.Tag = "OTP";
            this.btnOTP.UseVisualStyleBackColor = true;
            this.btnOTP.Visible = false;
            this.btnOTP.Click += new System.EventHandler(this.btnOTP_Click);
            // 
            // btn_OldStation
            // 
            this.btn_OldStation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_OldStation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btn_OldStation.FlatAppearance.BorderSize = 3;
            this.btn_OldStation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OldStation.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OldStation.ImageIndex = 49;
            this.btn_OldStation.ImageList = this.imgListButton;
            this.btn_OldStation.Location = new System.Drawing.Point(75, 3157);
            this.btn_OldStation.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btn_OldStation.Name = "btn_OldStation";
            this.btn_OldStation.Size = new System.Drawing.Size(216, 216);
            this.btn_OldStation.TabIndex = 69;
            this.btn_OldStation.Tag = "Printer";
            this.btn_OldStation.UseVisualStyleBackColor = true;
            this.btn_OldStation.Visible = false;
            this.btn_OldStation.Click += new System.EventHandler(this.btn_OldStation_Click);
            // 
            // btnFixtureRepair
            // 
            this.btnFixtureRepair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFixtureRepair.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.btnFixtureRepair.FlatAppearance.BorderSize = 3;
            this.btnFixtureRepair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFixtureRepair.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFixtureRepair.ImageIndex = 48;
            this.btnFixtureRepair.ImageList = this.imgListButton;
            this.btnFixtureRepair.Location = new System.Drawing.Point(321, 3157);
            this.btnFixtureRepair.Margin = new System.Windows.Forms.Padding(15, 25, 15, 20);
            this.btnFixtureRepair.Name = "btnFixtureRepair";
            this.btnFixtureRepair.Size = new System.Drawing.Size(216, 216);
            this.btnFixtureRepair.TabIndex = 70;
            this.btnFixtureRepair.Tag = "Return";
            this.btnFixtureRepair.UseVisualStyleBackColor = true;
            this.btnFixtureRepair.Visible = false;
            this.btnFixtureRepair.Click += new System.EventHandler(this.btnFixtureRepair_Click);
            // 
            // llblOP
            // 
            this.llblOP.AutoSize = true;
            this.llblOP.BackColor = System.Drawing.Color.Transparent;
            this.llblOP.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.llblOP.ForeColor = System.Drawing.Color.Transparent;
            this.llblOP.LinkColor = System.Drawing.Color.Transparent;
            this.llblOP.Location = new System.Drawing.Point(15, 475);
            this.llblOP.Name = "llblOP";
            this.llblOP.Size = new System.Drawing.Size(134, 27);
            this.llblOP.TabIndex = 42;
            this.llblOP.TabStop = true;
            this.llblOP.Text = "Hi! wangmm";
            this.llblOP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llblOP.Click += new System.EventHandler(this.llblOP_Click);
            // 
            // btnPsw
            // 
            this.btnPsw.BackColor = System.Drawing.Color.Transparent;
            this.btnPsw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPsw.FlatAppearance.BorderSize = 0;
            this.btnPsw.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPsw.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPsw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPsw.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPsw.ForeColor = System.Drawing.Color.White;
            this.btnPsw.Location = new System.Drawing.Point(0, 501);
            this.btnPsw.Name = "btnPsw";
            this.btnPsw.Size = new System.Drawing.Size(107, 38);
            this.btnPsw.TabIndex = 43;
            this.btnPsw.Text = "修改密码";
            this.btnPsw.UseVisualStyleBackColor = false;
            this.btnPsw.Click += new System.EventHandler(this.btnPsw_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(55, 365);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 44;
            this.label3.Text = "技术支持：\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(8, 383);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 24);
            this.label1.TabIndex = 45;
            this.label1.Text = "深圳市鼎立特科技有限公司";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(804, 541);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnPsw);
            this.Controls.Add(this.llblOP);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生产数据采集管理系统 - 功能选择";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReview;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Button btnManualScan;
        private System.Windows.Forms.ImageList imgListButton;
        private System.Windows.Forms.Button btnRework;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAutoScan;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnDataPush;
        private System.Windows.Forms.Button btnGlassDelivery;
        private System.Windows.Forms.Button btnGlassReceive;
        private System.Windows.Forms.Button btnDeliveryBoard;
        private System.Windows.Forms.Button btnDeliveryCheck;
        private System.Windows.Forms.LinkLabel llblOP;
        private System.Windows.Forms.Button btnPsw;
        private System.Windows.Forms.Button btnNG;
        private System.Windows.Forms.Button btnAutoPackage;
        private System.Windows.Forms.Button btnMaterialProgress;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnOrderException;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWarnConfig;
        private System.Windows.Forms.Button btnAutoScanConfig;
        private System.Windows.Forms.Button btnStorage;
        private System.Windows.Forms.Button btnReworkInput;
        private System.Windows.Forms.Button btnOrderStock;
        private System.Windows.Forms.Button btnPrinter;
        private System.Windows.Forms.Button btnReworkScan;
        private System.Windows.Forms.Button btnTpTest;
        private System.Windows.Forms.Button btnOTP;
        private System.Windows.Forms.Button btn_OldStation;
        private System.Windows.Forms.Button btnFixtureRepair;
    }
}