namespace MDC
{
    partial class frmStorage
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
            this.flpStorage = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flpSetting = new System.Windows.Forms.FlowLayoutPanel();
            this.rdoArea1 = new System.Windows.Forms.RadioButton();
            this.rdoArea2 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblIdle = new System.Windows.Forms.Label();
            this.lblUnlock = new System.Windows.Forms.Label();
            this.lblLock = new System.Windows.Forms.Label();
            this.lblDel = new System.Windows.Forms.Label();
            this.lbl30Day = new System.Windows.Forms.Label();
            this.lbl60Day = new System.Windows.Forms.Label();
            this.lblNearExpired = new System.Windows.Forms.Label();
            this.lblExpired = new System.Windows.Forms.Label();
            this.bgwShow = new System.ComponentModel.BackgroundWorker();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.flpSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpStorage
            // 
            this.flpStorage.AutoScroll = true;
            this.flpStorage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(20)))), ((int)(((byte)(50)))));
            this.flpStorage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpStorage.Location = new System.Drawing.Point(5, 57);
            this.flpStorage.Name = "flpStorage";
            this.flpStorage.Size = new System.Drawing.Size(1894, 980);
            this.flpStorage.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(20)))), ((int)(((byte)(50)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flpSetting, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1894, 52);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.LightYellow;
            this.lblTime.Location = new System.Drawing.Point(252, 24);
            this.lblTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 7);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(131, 19);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "更新时间:00:00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "智慧仓储管理看板";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1894, 2);
            this.panel2.TabIndex = 3;
            // 
            // flpSetting
            // 
            this.flpSetting.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.flpSetting.Controls.Add(this.rdoArea1);
            this.flpSetting.Controls.Add(this.rdoArea2);
            this.flpSetting.Controls.Add(this.label6);
            this.flpSetting.Controls.Add(this.comboBox1);
            this.flpSetting.Controls.Add(this.label4);
            this.flpSetting.Controls.Add(this.cmbPage);
            this.flpSetting.Controls.Add(this.label5);
            this.flpSetting.Controls.Add(this.cmbUnit);
            this.flpSetting.Controls.Add(this.label3);
            this.flpSetting.Controls.Add(this.lblIdle);
            this.flpSetting.Controls.Add(this.lblUnlock);
            this.flpSetting.Controls.Add(this.lblLock);
            this.flpSetting.Controls.Add(this.lblDel);
            this.flpSetting.Controls.Add(this.lbl30Day);
            this.flpSetting.Controls.Add(this.lbl60Day);
            this.flpSetting.Controls.Add(this.lblNearExpired);
            this.flpSetting.Controls.Add(this.lblExpired);
            this.flpSetting.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flpSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.flpSetting.Location = new System.Drawing.Point(432, 9);
            this.flpSetting.Name = "flpSetting";
            this.flpSetting.Size = new System.Drawing.Size(1415, 38);
            this.flpSetting.TabIndex = 2;
            // 
            // rdoArea1
            // 
            this.rdoArea1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoArea1.Checked = true;
            this.rdoArea1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.rdoArea1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.rdoArea1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoArea1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.rdoArea1.Location = new System.Drawing.Point(3, 3);
            this.rdoArea1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.rdoArea1.Name = "rdoArea1";
            this.rdoArea1.Size = new System.Drawing.Size(80, 30);
            this.rdoArea1.TabIndex = 18;
            this.rdoArea1.TabStop = true;
            this.rdoArea1.Text = "大库区";
            this.rdoArea1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoArea1.UseVisualStyleBackColor = true;
            this.rdoArea1.CheckedChanged += new System.EventHandler(this.rdoArea1_CheckedChanged);
            // 
            // rdoArea2
            // 
            this.rdoArea2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoArea2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.rdoArea2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.rdoArea2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoArea2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.rdoArea2.Location = new System.Drawing.Point(83, 3);
            this.rdoArea2.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.rdoArea2.Name = "rdoArea2";
            this.rdoArea2.Size = new System.Drawing.Size(80, 30);
            this.rdoArea2.TabIndex = 19;
            this.rdoArea2.Text = "小库区";
            this.rdoArea2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoArea2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(169, 4);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 4, 0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 26);
            this.label6.TabIndex = 16;
            this.label6.Text = "区域";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "小库区",
            "大库区"});
            this.comboBox1.Location = new System.Drawing.Point(219, 3);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(56, 30);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(298, 4);
            this.label4.Margin = new System.Windows.Forms.Padding(20, 4, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 26);
            this.label4.TabIndex = 4;
            this.label4.Text = "第";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbPage
            // 
            this.cmbPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.cmbPage.FormattingEnabled = true;
            this.cmbPage.Location = new System.Drawing.Point(329, 3);
            this.cmbPage.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.cmbPage.Name = "cmbPage";
            this.cmbPage.Size = new System.Drawing.Size(50, 30);
            this.cmbPage.TabIndex = 7;
            this.cmbPage.SelectionChangeCommitted += new System.EventHandler(this.cmbPage_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(379, 4);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 26);
            this.label5.TabIndex = 6;
            this.label5.Text = "页";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbUnit.Location = new System.Drawing.Point(430, 3);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(20, 3, 0, 3);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(50, 30);
            this.cmbUnit.TabIndex = 3;
            this.cmbUnit.SelectionChangeCommitted += new System.EventHandler(this.cmbUnit_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(480, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 4, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "排/页";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIdle
            // 
            this.lblIdle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIdle.BackColor = System.Drawing.Color.White;
            this.lblIdle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIdle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.lblIdle.Location = new System.Drawing.Point(562, 3);
            this.lblIdle.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblIdle.Name = "lblIdle";
            this.lblIdle.Size = new System.Drawing.Size(106, 30);
            this.lblIdle.TabIndex = 8;
            this.lblIdle.Text = "空闲";
            this.lblIdle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnlock
            // 
            this.lblUnlock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUnlock.BackColor = System.Drawing.Color.LightGreen;
            this.lblUnlock.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUnlock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.lblUnlock.Location = new System.Drawing.Point(668, 3);
            this.lblUnlock.Margin = new System.Windows.Forms.Padding(0);
            this.lblUnlock.Name = "lblUnlock";
            this.lblUnlock.Size = new System.Drawing.Size(106, 30);
            this.lblUnlock.TabIndex = 9;
            this.lblUnlock.Text = "解锁";
            this.lblUnlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLock
            // 
            this.lblLock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLock.BackColor = System.Drawing.Color.LightCyan;
            this.lblLock.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.lblLock.Location = new System.Drawing.Point(774, 3);
            this.lblLock.Margin = new System.Windows.Forms.Padding(0);
            this.lblLock.Name = "lblLock";
            this.lblLock.Size = new System.Drawing.Size(106, 30);
            this.lblLock.TabIndex = 11;
            this.lblLock.Text = "锁定";
            this.lblLock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDel
            // 
            this.lblDel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDel.BackColor = System.Drawing.Color.DarkGray;
            this.lblDel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.lblDel.Location = new System.Drawing.Point(880, 3);
            this.lblDel.Margin = new System.Windows.Forms.Padding(0);
            this.lblDel.Name = "lblDel";
            this.lblDel.Size = new System.Drawing.Size(106, 30);
            this.lblDel.TabIndex = 10;
            this.lblDel.Text = "禁用";
            this.lblDel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl30Day
            // 
            this.lbl30Day.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl30Day.BackColor = System.Drawing.Color.Yellow;
            this.lbl30Day.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl30Day.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.lbl30Day.Location = new System.Drawing.Point(986, 3);
            this.lbl30Day.Margin = new System.Windows.Forms.Padding(0);
            this.lbl30Day.Name = "lbl30Day";
            this.lbl30Day.Size = new System.Drawing.Size(106, 30);
            this.lbl30Day.TabIndex = 14;
            this.lbl30Day.Text = "超30天";
            this.lbl30Day.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl60Day
            // 
            this.lbl60Day.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl60Day.BackColor = System.Drawing.Color.Orange;
            this.lbl60Day.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl60Day.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.lbl60Day.Location = new System.Drawing.Point(1092, 3);
            this.lbl60Day.Margin = new System.Windows.Forms.Padding(0);
            this.lbl60Day.Name = "lbl60Day";
            this.lbl60Day.Size = new System.Drawing.Size(106, 30);
            this.lbl60Day.TabIndex = 15;
            this.lbl60Day.Text = "超60天";
            this.lbl60Day.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNearExpired
            // 
            this.lblNearExpired.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNearExpired.BackColor = System.Drawing.Color.LightPink;
            this.lblNearExpired.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNearExpired.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.lblNearExpired.Location = new System.Drawing.Point(1198, 3);
            this.lblNearExpired.Margin = new System.Windows.Forms.Padding(0);
            this.lblNearExpired.Name = "lblNearExpired";
            this.lblNearExpired.Size = new System.Drawing.Size(106, 30);
            this.lblNearExpired.TabIndex = 12;
            this.lblNearExpired.Text = "15天过期";
            this.lblNearExpired.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblExpired
            // 
            this.lblExpired.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblExpired.BackColor = System.Drawing.Color.Red;
            this.lblExpired.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExpired.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(70)))));
            this.lblExpired.Location = new System.Drawing.Point(1304, 3);
            this.lblExpired.Margin = new System.Windows.Forms.Padding(0);
            this.lblExpired.Name = "lblExpired";
            this.lblExpired.Size = new System.Drawing.Size(106, 30);
            this.lblExpired.TabIndex = 13;
            this.lblExpired.Text = "已过期";
            this.lblExpired.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgwShow
            // 
            this.bgwShow.WorkerReportsProgress = true;
            this.bgwShow.WorkerSupportsCancellation = true;
            this.bgwShow.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwShow_DoWork);
            this.bgwShow.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwShow_ProgressChanged);
            this.bgwShow.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwShow_RunWorkerCompleted);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 15000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // frmStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1904, 1042);
            this.Controls.Add(this.flpStorage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmStorage";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智慧仓储管理看板";
            this.Load += new System.EventHandler(this.frmStorage_Load);
            this.Shown += new System.EventHandler(this.frmStorage_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flpSetting.ResumeLayout(false);
            this.flpSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpStorage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbPage;
        private System.ComponentModel.BackgroundWorker bgwShow;
        private System.Windows.Forms.Label lblIdle;
        private System.Windows.Forms.Label lblUnlock;
        private System.Windows.Forms.Label lblDel;
        private System.Windows.Forms.Label lblLock;
        private System.Windows.Forms.Label lblNearExpired;
        private System.Windows.Forms.Label lblExpired;
        private System.Windows.Forms.Label lbl30Day;
        private System.Windows.Forms.Label lbl60Day;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton rdoArea1;
        private System.Windows.Forms.RadioButton rdoArea2;
    }
}