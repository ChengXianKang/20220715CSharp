namespace MDC
{
    partial class frmOlded
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOlded));
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblNG = new System.Windows.Forms.Label();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.lblOP = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.lblDeviceIP = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtProductOrder = new System.Windows.Forms.TextBox();
            this.cmbSiteName = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtScanCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLCDCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCurrentOpCode = new System.Windows.Forms.TextBox();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.cmbLineName = new System.Windows.Forms.ComboBox();
            this.txtProcessIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlSplit1 = new System.Windows.Forms.Panel();
            this.bgwWriteData = new System.ComponentModel.BackgroundWorker();
            this.imglNG = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.flpScanNote = new System.Windows.Forms.FlowLayoutPanel();
            this.txtLCDCount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBadRate = new System.Windows.Forms.TextBox();
            this.txtOKCount = new System.Windows.Forms.TextBox();
            this.lblYield = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNGCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwNote = new Utils.ListViewNote(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.lblTime, 4, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblNG, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtStartDate, 5, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblOP, 4, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtEndDate, 5, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblDeviceIP, 4, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtDuration, 5, 3);
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtModel, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtProductOrder, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.cmbSiteName, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtScanCode, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label10, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtLCDCode, 3, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(11, 58);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(996, 126);
            this.tableLayoutPanel5.TabIndex = 122;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblTime.Location = new System.Drawing.Point(676, 8);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(92, 24);
            this.lblTime.TabIndex = 129;
            this.lblTime.Text = "开始时间:";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNG
            // 
            this.lblNG.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNG.BackColor = System.Drawing.Color.Transparent;
            this.lblNG.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblNG.Location = new System.Drawing.Point(338, 7);
            this.lblNG.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(96, 25);
            this.lblNG.TabIndex = 116;
            this.lblNG.Text = "工序名称:";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartDate
            // 
            this.txtStartDate.BackColor = System.Drawing.Color.Lavender;
            this.txtStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartDate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStartDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtStartDate.Location = new System.Drawing.Point(775, 4);
            this.txtStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.ReadOnly = true;
            this.txtStartDate.Size = new System.Drawing.Size(209, 31);
            this.txtStartDate.TabIndex = 137;
            this.txtStartDate.TabStop = false;
            this.txtStartDate.Text = "0000-00-00 00:00";
            // 
            // lblOP
            // 
            this.lblOP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOP.BackColor = System.Drawing.Color.Transparent;
            this.lblOP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblOP.Location = new System.Drawing.Point(676, 47);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(92, 24);
            this.lblOP.TabIndex = 138;
            this.lblOP.Text = "结束时间:";
            this.lblOP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndDate
            // 
            this.txtEndDate.BackColor = System.Drawing.Color.Lavender;
            this.txtEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEndDate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtEndDate.Location = new System.Drawing.Point(775, 44);
            this.txtEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.ReadOnly = true;
            this.txtEndDate.Size = new System.Drawing.Size(209, 31);
            this.txtEndDate.TabIndex = 139;
            this.txtEndDate.TabStop = false;
            this.txtEndDate.Text = "0000-00-00 00:00";
            // 
            // lblDeviceIP
            // 
            this.lblDeviceIP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDeviceIP.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDeviceIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblDeviceIP.Location = new System.Drawing.Point(676, 88);
            this.lblDeviceIP.Name = "lblDeviceIP";
            this.lblDeviceIP.Size = new System.Drawing.Size(92, 24);
            this.lblDeviceIP.TabIndex = 130;
            this.lblDeviceIP.Text = "老化时长:";
            this.lblDeviceIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDuration
            // 
            this.txtDuration.BackColor = System.Drawing.Color.Lavender;
            this.txtDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDuration.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtDuration.Location = new System.Drawing.Point(775, 83);
            this.txtDuration.Margin = new System.Windows.Forms.Padding(4);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.ReadOnly = true;
            this.txtDuration.Size = new System.Drawing.Size(209, 31);
            this.txtDuration.TabIndex = 138;
            this.txtDuration.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 24);
            this.label3.TabIndex = 131;
            this.label3.Text = "成品型号:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtModel
            // 
            this.txtModel.BackColor = System.Drawing.Color.Lavender;
            this.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtModel.Location = new System.Drawing.Point(111, 44);
            this.txtModel.Margin = new System.Windows.Forms.Padding(4);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(208, 31);
            this.txtModel.TabIndex = 135;
            this.txtModel.TabStop = false;
            this.txtModel.Text = "成品型号";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label14.Location = new System.Drawing.Point(12, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 24);
            this.label14.TabIndex = 130;
            this.label14.Text = "生产工单:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductOrder
            // 
            this.txtProductOrder.BackColor = System.Drawing.Color.Lavender;
            this.txtProductOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductOrder.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProductOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProductOrder.Location = new System.Drawing.Point(111, 4);
            this.txtProductOrder.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductOrder.Name = "txtProductOrder";
            this.txtProductOrder.ReadOnly = true;
            this.txtProductOrder.Size = new System.Drawing.Size(208, 31);
            this.txtProductOrder.TabIndex = 134;
            this.txtProductOrder.TabStop = false;
            this.txtProductOrder.Text = "工单编码";
            // 
            // cmbSiteName
            // 
            this.cmbSiteName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbSiteName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSiteName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSiteName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.cmbSiteName.FormattingEnabled = true;
            this.cmbSiteName.Location = new System.Drawing.Point(443, 4);
            this.cmbSiteName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSiteName.Name = "cmbSiteName";
            this.cmbSiteName.Size = new System.Drawing.Size(208, 32);
            this.cmbSiteName.TabIndex = 151;
            this.cmbSiteName.SelectedIndexChanged += new System.EventHandler(this.cmbSiteName_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label9.Location = new System.Drawing.Point(12, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 24);
            this.label9.TabIndex = 124;
            this.label9.Text = "扫描编码:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtScanCode
            // 
            this.txtScanCode.BackColor = System.Drawing.Color.White;
            this.txtScanCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel5.SetColumnSpan(this.txtScanCode, 3);
            this.txtScanCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtScanCode.Location = new System.Drawing.Point(111, 83);
            this.txtScanCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.Size = new System.Drawing.Size(537, 31);
            this.txtScanCode.TabIndex = 125;
            this.txtScanCode.Text = "FPC编码";
            this.txtScanCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScanCode_KeyPress);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label10.Location = new System.Drawing.Point(344, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 24);
            this.label10.TabIndex = 127;
            this.label10.Text = "LCD编码:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLCDCode
            // 
            this.txtLCDCode.BackColor = System.Drawing.Color.Lavender;
            this.txtLCDCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLCDCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLCDCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtLCDCode.Location = new System.Drawing.Point(443, 44);
            this.txtLCDCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtLCDCode.Name = "txtLCDCode";
            this.txtLCDCode.ReadOnly = true;
            this.txtLCDCode.Size = new System.Drawing.Size(209, 31);
            this.txtLCDCode.TabIndex = 126;
            this.txtLCDCode.Text = "LCD编码";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label13.Location = new System.Drawing.Point(673, 7);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 25);
            this.label13.TabIndex = 116;
            this.label13.Text = "返修人员:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCurrentOpCode
            // 
            this.txtCurrentOpCode.BackColor = System.Drawing.Color.Lavender;
            this.txtCurrentOpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentOpCode.Enabled = false;
            this.txtCurrentOpCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrentOpCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtCurrentOpCode.Location = new System.Drawing.Point(775, 4);
            this.txtCurrentOpCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCurrentOpCode.Name = "txtCurrentOpCode";
            this.txtCurrentOpCode.ReadOnly = true;
            this.txtCurrentOpCode.Size = new System.Drawing.Size(209, 31);
            this.txtCurrentOpCode.TabIndex = 15;
            this.txtCurrentOpCode.TabStop = false;
            this.txtCurrentOpCode.Text = "登录账号";
            // 
            // tlpInfo
            // 
            this.tlpInfo.AutoSize = true;
            this.tlpInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpInfo.ColumnCount = 6;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.Controls.Add(this.cmbLineName, 0, 0);
            this.tlpInfo.Controls.Add(this.txtProcessIP, 3, 0);
            this.tlpInfo.Controls.Add(this.label1, 2, 0);
            this.tlpInfo.Controls.Add(this.label12, 0, 0);
            this.tlpInfo.Controls.Add(this.label13, 4, 0);
            this.tlpInfo.Controls.Add(this.txtCurrentOpCode, 5, 0);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpInfo.Location = new System.Drawing.Point(11, 10);
            this.tlpInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 1;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tlpInfo.Size = new System.Drawing.Size(996, 40);
            this.tlpInfo.TabIndex = 109;
            // 
            // cmbLineName
            // 
            this.cmbLineName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLineName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.cmbLineName.FormattingEnabled = true;
            this.cmbLineName.Location = new System.Drawing.Point(111, 4);
            this.cmbLineName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLineName.Name = "cmbLineName";
            this.cmbLineName.Size = new System.Drawing.Size(208, 32);
            this.cmbLineName.TabIndex = 132;
            this.cmbLineName.SelectedIndexChanged += new System.EventHandler(this.cmbProcess_SelectedIndexChanged);
            // 
            // txtProcessIP
            // 
            this.txtProcessIP.BackColor = System.Drawing.Color.Lavender;
            this.txtProcessIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcessIP.Enabled = false;
            this.txtProcessIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcessIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProcessIP.Location = new System.Drawing.Point(443, 4);
            this.txtProcessIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtProcessIP.Name = "txtProcessIP";
            this.txtProcessIP.ReadOnly = true;
            this.txtProcessIP.Size = new System.Drawing.Size(208, 31);
            this.txtProcessIP.TabIndex = 14;
            this.txtProcessIP.TabStop = false;
            this.txtProcessIP.Text = "172.16.13.111";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(341, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 25);
            this.label1.TabIndex = 121;
            this.label1.Text = "站  点 IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label12.Location = new System.Drawing.Point(6, 7);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 25);
            this.label12.TabIndex = 131;
            this.label12.Text = "产线名称:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSplit1
            // 
            this.pnlSplit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit1.Location = new System.Drawing.Point(11, 50);
            this.pnlSplit1.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSplit1.Name = "pnlSplit1";
            this.pnlSplit1.Size = new System.Drawing.Size(996, 8);
            this.pnlSplit1.TabIndex = 113;
            // 
            // bgwWriteData
            // 
            this.bgwWriteData.WorkerReportsProgress = true;
            this.bgwWriteData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWriteData_DoWork);
            this.bgwWriteData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWriteData_RunWorkerCompleted);
            // 
            // imglNG
            // 
            this.imglNG.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imglNG.ImageSize = new System.Drawing.Size(16, 16);
            this.imglNG.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(11, 184);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(996, 47);
            this.panel2.TabIndex = 124;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 255F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flpScanNote, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtLCDCount, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtBadRate, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtOKCount, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblYield, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label16, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNGCount, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(996, 39);
            this.tableLayoutPanel1.TabIndex = 162;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label7.Location = new System.Drawing.Point(4, 4);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 31);
            this.label7.TabIndex = 152;
            this.label7.Text = "玻璃计数:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flpScanNote
            // 
            this.flpScanNote.BackColor = System.Drawing.Color.Lavender;
            this.flpScanNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpScanNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpScanNote.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpScanNote.Location = new System.Drawing.Point(742, 1);
            this.flpScanNote.Margin = new System.Windows.Forms.Padding(1, 1, 4, 1);
            this.flpScanNote.Name = "flpScanNote";
            this.flpScanNote.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.flpScanNote.Size = new System.Drawing.Size(250, 37);
            this.flpScanNote.TabIndex = 161;
            this.flpScanNote.SizeChanged += new System.EventHandler(this.flpScanNote_SizeChanged);
            // 
            // txtLCDCount
            // 
            this.txtLCDCount.BackColor = System.Drawing.Color.Lavender;
            this.txtLCDCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLCDCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLCDCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLCDCount.ForeColor = System.Drawing.Color.Black;
            this.txtLCDCount.Location = new System.Drawing.Point(111, 4);
            this.txtLCDCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtLCDCount.Name = "txtLCDCount";
            this.txtLCDCount.ReadOnly = true;
            this.txtLCDCount.Size = new System.Drawing.Size(95, 31);
            this.txtLCDCount.TabIndex = 153;
            this.txtLCDCount.Text = "0";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label11.Location = new System.Drawing.Point(706, 4);
            this.label11.Margin = new System.Windows.Forms.Padding(4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 31);
            this.label11.TabIndex = 160;
            this.label11.Text = "%";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(214, 4);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 31);
            this.label8.TabIndex = 154;
            this.label8.Text = "OK:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBadRate
            // 
            this.txtBadRate.BackColor = System.Drawing.Color.Lavender;
            this.txtBadRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBadRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBadRate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBadRate.ForeColor = System.Drawing.Color.Black;
            this.txtBadRate.Location = new System.Drawing.Point(625, 4);
            this.txtBadRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtBadRate.Name = "txtBadRate";
            this.txtBadRate.ReadOnly = true;
            this.txtBadRate.Size = new System.Drawing.Size(73, 31);
            this.txtBadRate.TabIndex = 159;
            this.txtBadRate.Text = "0";
            // 
            // txtOKCount
            // 
            this.txtOKCount.BackColor = System.Drawing.Color.Lavender;
            this.txtOKCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOKCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOKCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOKCount.ForeColor = System.Drawing.Color.Black;
            this.txtOKCount.Location = new System.Drawing.Point(271, 4);
            this.txtOKCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtOKCount.Name = "txtOKCount";
            this.txtOKCount.ReadOnly = true;
            this.txtOKCount.Size = new System.Drawing.Size(95, 31);
            this.txtOKCount.TabIndex = 151;
            this.txtOKCount.Text = "0";
            // 
            // lblYield
            // 
            this.lblYield.BackColor = System.Drawing.Color.Transparent;
            this.lblYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYield.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYield.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblYield.Location = new System.Drawing.Point(546, 4);
            this.lblYield.Margin = new System.Windows.Forms.Padding(4);
            this.lblYield.Name = "lblYield";
            this.lblYield.Size = new System.Drawing.Size(71, 31);
            this.lblYield.TabIndex = 158;
            this.lblYield.Text = "不良率:";
            this.lblYield.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.DarkOrange;
            this.label16.Location = new System.Drawing.Point(374, 4);
            this.label16.Margin = new System.Windows.Forms.Padding(4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 31);
            this.label16.TabIndex = 157;
            this.label16.Text = "异常:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNGCount
            // 
            this.txtNGCount.BackColor = System.Drawing.Color.Lavender;
            this.txtNGCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNGCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNGCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNGCount.ForeColor = System.Drawing.Color.Black;
            this.txtNGCount.Location = new System.Drawing.Point(443, 4);
            this.txtNGCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtNGCount.Name = "txtNGCount";
            this.txtNGCount.ReadOnly = true;
            this.txtNGCount.Size = new System.Drawing.Size(95, 31);
            this.txtNGCount.TabIndex = 155;
            this.txtNGCount.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(6, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 25);
            this.label2.TabIndex = 134;
            this.label2.Text = "即时消息:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lvwNote
            // 
            this.lvwNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.lvwNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel4.SetColumnSpan(this.lvwNote, 2);
            this.lvwNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwNote.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvwNote.FullRowSelect = true;
            this.lvwNote.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwNote.HideSelection = false;
            this.lvwNote.Location = new System.Drawing.Point(4, 29);
            this.lvwNote.Margin = new System.Windows.Forms.Padding(4);
            this.lvwNote.Name = "lvwNote";
            this.lvwNote.ShowItemToolTips = true;
            this.lvwNote.Size = new System.Drawing.Size(988, 390);
            this.lvwNote.TabIndex = 133;
            this.lvwNote.UseCompatibleStateImageBehavior = false;
            this.lvwNote.View = System.Windows.Forms.View.Details;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.lvwNote, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(11, 231);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(996, 423);
            this.tableLayoutPanel4.TabIndex = 123;
            // 
            // frmOlded
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(1018, 664);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.pnlSplit1);
            this.Controls.Add(this.tlpInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmOlded";
            this.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "老化过站";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNGReworkScan_FormClosing);
            this.Load += new System.EventHandler(this.frmOlded_Load);
            this.SizeChanged += new System.EventHandler(this.frmNGReview_SizeChanged);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.TextBox txtCurrentOpCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProcessIP;
        private System.Windows.Forms.Panel pnlSplit1;
        private System.ComponentModel.BackgroundWorker bgwWriteData;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLCDCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtProductOrder;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox txtStartDate;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.TextBox txtEndDate;
        private System.Windows.Forms.Label lblNG;
        private System.Windows.Forms.ImageList imglNG;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbLineName;
        private System.Windows.Forms.TextBox txtOKCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLCDCount;
        private System.Windows.Forms.TextBox txtNGCount;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtBadRate;
        private System.Windows.Forms.Label lblYield;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FlowLayoutPanel flpScanNote;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbSiteName;
        private System.Windows.Forms.Label lblDeviceIP;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtScanCode;
        private System.Windows.Forms.Label label2;
        private Utils.ListViewNote lvwNote;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}