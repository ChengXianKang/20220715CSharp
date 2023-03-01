namespace CCS_3000Demo
{
    partial class frmPrinter
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrinter));
            this.ParameterGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbcmbPrinterPorts = new System.Windows.Forms.Label();
            this.lbtxtReciveServerIP = new System.Windows.Forms.Label();
            this.lbtxtScan710ServerIP = new System.Windows.Forms.Label();
            this.txtScan710ServerPort = new System.Windows.Forms.TextBox();
            this.cmbPrinterPorts = new System.Windows.Forms.ComboBox();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.lbtxtScan710ServerPort = new System.Windows.Forms.Label();
            this.txtReciveServerIP = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtScan710ServerIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lbcmbPrinterBaudRate = new System.Windows.Forms.Label();
            this.txtReciveServerPort = new System.Windows.Forms.TextBox();
            this.lbtxtReciveServerPort = new System.Windows.Forms.Label();
            this.cmbPrinterBaudRate = new System.Windows.Forms.ComboBox();
            this.chkRepeat = new System.Windows.Forms.CheckBox();
            this.chk710Scan = new System.Windows.Forms.CheckBox();
            this.btn710Scan = new System.Windows.Forms.Button();
            this.txtScanRule = new System.Windows.Forms.TextBox();
            this.lbtxtScanRule = new System.Windows.Forms.Label();
            this.lbtxtSendCode = new System.Windows.Forms.Label();
            this.txtSendCode = new System.Windows.Forms.TextBox();
            this.lbtxtPlainCode = new System.Windows.Forms.Label();
            this.txtLCDCode = new System.Windows.Forms.TextBox();
            this.txtQrCode = new System.Windows.Forms.TextBox();
            this.lbtxtQrCode = new System.Windows.Forms.Label();
            this.txtScanCode = new System.Windows.Forms.TextBox();
            this.txt_ErrNote = new System.Windows.Forms.TextBox();
            this.lbtxtScanCode = new System.Windows.Forms.Label();
            this.PortPrinter = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.QrPanel = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ParameterGroupBox.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.QrPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParameterGroupBox
            // 
            this.ParameterGroupBox.AutoSize = true;
            this.ParameterGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.ParameterGroupBox.Controls.Add(this.tableLayoutPanel3);
            this.ParameterGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ParameterGroupBox.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ParameterGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.ParameterGroupBox.Location = new System.Drawing.Point(10, 10);
            this.ParameterGroupBox.Name = "ParameterGroupBox";
            this.ParameterGroupBox.Size = new System.Drawing.Size(764, 155);
            this.ParameterGroupBox.TabIndex = 0;
            this.ParameterGroupBox.TabStop = false;
            this.ParameterGroupBox.Text = "通信参数";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 8;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.lbcmbPrinterPorts, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbtxtReciveServerIP, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbtxtScan710ServerIP, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtScan710ServerPort, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.cmbPrinterPorts, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRepeat, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbtxtScan710ServerPort, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtReciveServerIP, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnDisconnect, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtScan710ServerIP, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.btnConnect, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbcmbPrinterBaudRate, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtReciveServerPort, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbtxtReciveServerPort, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.cmbPrinterBaudRate, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkRepeat, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.chk710Scan, 6, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn710Scan, 7, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(758, 122);
            this.tableLayoutPanel3.TabIndex = 22;
            // 
            // lbcmbPrinterPorts
            // 
            this.lbcmbPrinterPorts.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbcmbPrinterPorts.AutoSize = true;
            this.lbcmbPrinterPorts.Location = new System.Drawing.Point(19, 7);
            this.lbcmbPrinterPorts.Name = "lbcmbPrinterPorts";
            this.lbcmbPrinterPorts.Size = new System.Drawing.Size(98, 27);
            this.lbcmbPrinterPorts.TabIndex = 0;
            this.lbcmbPrinterPorts.Text = "喷码端口:";
            // 
            // lbtxtReciveServerIP
            // 
            this.lbtxtReciveServerIP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtReciveServerIP.AutoSize = true;
            this.lbtxtReciveServerIP.Location = new System.Drawing.Point(19, 48);
            this.lbtxtReciveServerIP.Name = "lbtxtReciveServerIP";
            this.lbtxtReciveServerIP.Size = new System.Drawing.Size(98, 27);
            this.lbtxtReciveServerIP.TabIndex = 8;
            this.lbtxtReciveServerIP.Text = "服务器IP:";
            // 
            // lbtxtScan710ServerIP
            // 
            this.lbtxtScan710ServerIP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtScan710ServerIP.AutoSize = true;
            this.lbtxtScan710ServerIP.Location = new System.Drawing.Point(3, 88);
            this.lbtxtScan710ServerIP.Name = "lbtxtScan710ServerIP";
            this.lbtxtScan710ServerIP.Size = new System.Drawing.Size(114, 27);
            this.lbtxtScan710ServerIP.TabIndex = 16;
            this.lbtxtScan710ServerIP.Text = "710服务IP:";
            this.lbtxtScan710ServerIP.Visible = false;
            // 
            // txtScan710ServerPort
            // 
            this.txtScan710ServerPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScan710ServerPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScan710ServerPort.Enabled = false;
            this.txtScan710ServerPort.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScan710ServerPort.Location = new System.Drawing.Point(377, 85);
            this.txtScan710ServerPort.Name = "txtScan710ServerPort";
            this.txtScan710ServerPort.Size = new System.Drawing.Size(108, 34);
            this.txtScan710ServerPort.TabIndex = 19;
            this.txtScan710ServerPort.Text = "710端口9004";
            this.txtScan710ServerPort.Visible = false;
            // 
            // cmbPrinterPorts
            // 
            this.cmbPrinterPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPrinterPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinterPorts.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPrinterPorts.FormattingEnabled = true;
            this.cmbPrinterPorts.Location = new System.Drawing.Point(123, 3);
            this.cmbPrinterPorts.Name = "cmbPrinterPorts";
            this.cmbPrinterPorts.Size = new System.Drawing.Size(108, 35);
            this.cmbPrinterPorts.TabIndex = 6;
            // 
            // btnRepeat
            // 
            this.btnRepeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRepeat.Enabled = false;
            this.btnRepeat.Location = new System.Drawing.Point(511, 44);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(108, 35);
            this.btnRepeat.TabIndex = 13;
            this.btnRepeat.Text = "重新喷印";
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // lbtxtScan710ServerPort
            // 
            this.lbtxtScan710ServerPort.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtScan710ServerPort.AutoSize = true;
            this.lbtxtScan710ServerPort.Location = new System.Drawing.Point(237, 88);
            this.lbtxtScan710ServerPort.Name = "lbtxtScan710ServerPort";
            this.lbtxtScan710ServerPort.Size = new System.Drawing.Size(134, 27);
            this.lbtxtScan710ServerPort.TabIndex = 18;
            this.lbtxtScan710ServerPort.Text = "710服务端口:";
            this.lbtxtScan710ServerPort.Visible = false;
            // 
            // txtReciveServerIP
            // 
            this.txtReciveServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReciveServerIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReciveServerIP.Enabled = false;
            this.txtReciveServerIP.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReciveServerIP.ForeColor = System.Drawing.Color.Black;
            this.txtReciveServerIP.Location = new System.Drawing.Point(123, 44);
            this.txtReciveServerIP.Name = "txtReciveServerIP";
            this.txtReciveServerIP.Size = new System.Drawing.Size(108, 34);
            this.txtReciveServerIP.TabIndex = 9;
            this.txtReciveServerIP.Text = "数据接收IP";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(645, 3);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(110, 35);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "断开连接";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtScan710ServerIP
            // 
            this.txtScan710ServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScan710ServerIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScan710ServerIP.Enabled = false;
            this.txtScan710ServerIP.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScan710ServerIP.Location = new System.Drawing.Point(123, 85);
            this.txtScan710ServerIP.Name = "txtScan710ServerIP";
            this.txtScan710ServerIP.Size = new System.Drawing.Size(108, 34);
            this.txtScan710ServerIP.TabIndex = 17;
            this.txtScan710ServerIP.Text = "710服务器IP";
            this.txtScan710ServerIP.Visible = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Location = new System.Drawing.Point(511, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(108, 35);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "连接喷码机";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lbcmbPrinterBaudRate
            // 
            this.lbcmbPrinterBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbcmbPrinterBaudRate.AutoSize = true;
            this.lbcmbPrinterBaudRate.Location = new System.Drawing.Point(253, 7);
            this.lbcmbPrinterBaudRate.Name = "lbcmbPrinterBaudRate";
            this.lbcmbPrinterBaudRate.Size = new System.Drawing.Size(118, 27);
            this.lbcmbPrinterBaudRate.TabIndex = 1;
            this.lbcmbPrinterBaudRate.Text = "喷码波特率:";
            // 
            // txtReciveServerPort
            // 
            this.txtReciveServerPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReciveServerPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReciveServerPort.Enabled = false;
            this.txtReciveServerPort.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReciveServerPort.Location = new System.Drawing.Point(377, 44);
            this.txtReciveServerPort.Name = "txtReciveServerPort";
            this.txtReciveServerPort.Size = new System.Drawing.Size(108, 34);
            this.txtReciveServerPort.TabIndex = 11;
            this.txtReciveServerPort.Text = "数据接收端口";
            // 
            // lbtxtReciveServerPort
            // 
            this.lbtxtReciveServerPort.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtReciveServerPort.AutoSize = true;
            this.lbtxtReciveServerPort.Location = new System.Drawing.Point(253, 48);
            this.lbtxtReciveServerPort.Name = "lbtxtReciveServerPort";
            this.lbtxtReciveServerPort.Size = new System.Drawing.Size(118, 27);
            this.lbtxtReciveServerPort.TabIndex = 10;
            this.lbtxtReciveServerPort.Text = "服务器端口:";
            // 
            // cmbPrinterBaudRate
            // 
            this.cmbPrinterBaudRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPrinterBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinterBaudRate.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPrinterBaudRate.FormattingEnabled = true;
            this.cmbPrinterBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbPrinterBaudRate.Location = new System.Drawing.Point(377, 3);
            this.cmbPrinterBaudRate.Name = "cmbPrinterBaudRate";
            this.cmbPrinterBaudRate.Size = new System.Drawing.Size(108, 35);
            this.cmbPrinterBaudRate.TabIndex = 7;
            // 
            // chkRepeat
            // 
            this.chkRepeat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkRepeat.AutoSize = true;
            this.chkRepeat.Enabled = false;
            this.chkRepeat.Location = new System.Drawing.Point(491, 54);
            this.chkRepeat.Name = "chkRepeat";
            this.chkRepeat.Size = new System.Drawing.Size(14, 14);
            this.chkRepeat.TabIndex = 12;
            this.chkRepeat.UseVisualStyleBackColor = true;
            this.chkRepeat.CheckedChanged += new System.EventHandler(this.chkRepeat_CheckedChanged);
            // 
            // chk710Scan
            // 
            this.chk710Scan.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chk710Scan.AutoSize = true;
            this.chk710Scan.Enabled = false;
            this.chk710Scan.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk710Scan.Location = new System.Drawing.Point(625, 54);
            this.chk710Scan.Name = "chk710Scan";
            this.chk710Scan.Size = new System.Drawing.Size(14, 14);
            this.chk710Scan.TabIndex = 21;
            this.chk710Scan.UseVisualStyleBackColor = true;
            // 
            // btn710Scan
            // 
            this.btn710Scan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn710Scan.Enabled = false;
            this.btn710Scan.Location = new System.Drawing.Point(645, 44);
            this.btn710Scan.Name = "btn710Scan";
            this.btn710Scan.Size = new System.Drawing.Size(110, 35);
            this.btn710Scan.TabIndex = 20;
            this.btn710Scan.Text = "自动扫描";
            this.btn710Scan.UseVisualStyleBackColor = true;
            this.btn710Scan.Click += new System.EventHandler(this.btn710Scan_Click);
            // 
            // txtScanRule
            // 
            this.txtScanRule.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtScanRule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScanRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScanRule.Enabled = false;
            this.txtScanRule.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.txtScanRule.Location = new System.Drawing.Point(645, 3);
            this.txtScanRule.Name = "txtScanRule";
            this.txtScanRule.ReadOnly = true;
            this.txtScanRule.Size = new System.Drawing.Size(110, 39);
            this.txtScanRule.TabIndex = 15;
            // 
            // lbtxtScanRule
            // 
            this.lbtxtScanRule.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtScanRule.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lbtxtScanRule, 2);
            this.lbtxtScanRule.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.lbtxtScanRule.Location = new System.Drawing.Point(581, 9);
            this.lbtxtScanRule.Name = "lbtxtScanRule";
            this.lbtxtScanRule.Size = new System.Drawing.Size(58, 27);
            this.lbtxtScanRule.TabIndex = 14;
            this.lbtxtScanRule.Text = "规则:";
            // 
            // lbtxtSendCode
            // 
            this.lbtxtSendCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtSendCode.AutoSize = true;
            this.lbtxtSendCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.lbtxtSendCode.Location = new System.Drawing.Point(19, 89);
            this.lbtxtSendCode.Name = "lbtxtSendCode";
            this.lbtxtSendCode.Size = new System.Drawing.Size(98, 27);
            this.lbtxtSendCode.TabIndex = 15;
            this.lbtxtSendCode.Text = "发送字符:";
            // 
            // txtSendCode
            // 
            this.txtSendCode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSendCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSendCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSendCode.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSendCode.Location = new System.Drawing.Point(123, 85);
            this.txtSendCode.Name = "txtSendCode";
            this.txtSendCode.ReadOnly = true;
            this.txtSendCode.Size = new System.Drawing.Size(632, 35);
            this.txtSendCode.TabIndex = 14;
            // 
            // lbtxtPlainCode
            // 
            this.lbtxtPlainCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtPlainCode.AutoSize = true;
            this.lbtxtPlainCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.lbtxtPlainCode.Location = new System.Drawing.Point(19, 48);
            this.lbtxtPlainCode.Name = "lbtxtPlainCode";
            this.lbtxtPlainCode.Size = new System.Drawing.Size(98, 27);
            this.lbtxtPlainCode.TabIndex = 13;
            this.lbtxtPlainCode.Text = "玻璃编码:";
            // 
            // txtLCDCode
            // 
            this.txtLCDCode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLCDCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLCDCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLCDCode.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLCDCode.Location = new System.Drawing.Point(123, 44);
            this.txtLCDCode.Name = "txtLCDCode";
            this.txtLCDCode.ReadOnly = true;
            this.txtLCDCode.Size = new System.Drawing.Size(632, 35);
            this.txtLCDCode.TabIndex = 6;
            // 
            // txtQrCode
            // 
            this.txtQrCode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtQrCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQrCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQrCode.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQrCode.Location = new System.Drawing.Point(123, 3);
            this.txtQrCode.Name = "txtQrCode";
            this.txtQrCode.ReadOnly = true;
            this.txtQrCode.Size = new System.Drawing.Size(632, 35);
            this.txtQrCode.TabIndex = 4;
            // 
            // lbtxtQrCode
            // 
            this.lbtxtQrCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtQrCode.AutoSize = true;
            this.lbtxtQrCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.lbtxtQrCode.Location = new System.Drawing.Point(19, 7);
            this.lbtxtQrCode.Name = "lbtxtQrCode";
            this.lbtxtQrCode.Size = new System.Drawing.Size(98, 27);
            this.lbtxtQrCode.TabIndex = 0;
            this.lbtxtQrCode.Text = "成品编码:";
            // 
            // txtScanCode
            // 
            this.txtScanCode.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.SetColumnSpan(this.txtScanCode, 5);
            this.txtScanCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScanCode.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtScanCode.Location = new System.Drawing.Point(123, 3);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.Size = new System.Drawing.Size(439, 39);
            this.txtScanCode.TabIndex = 0;
            this.txtScanCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScanCode_KeyPress);
            // 
            // txt_ErrNote
            // 
            this.txt_ErrNote.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_ErrNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ErrNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ErrNote.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ErrNote.ForeColor = System.Drawing.Color.Red;
            this.txt_ErrNote.Location = new System.Drawing.Point(10, 429);
            this.txt_ErrNote.Multiline = true;
            this.txt_ErrNote.Name = "txt_ErrNote";
            this.txt_ErrNote.ReadOnly = true;
            this.txt_ErrNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_ErrNote.Size = new System.Drawing.Size(764, 123);
            this.txt_ErrNote.TabIndex = 11;
            this.txt_ErrNote.TabStop = false;
            // 
            // lbtxtScanCode
            // 
            this.lbtxtScanCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbtxtScanCode.AutoSize = true;
            this.lbtxtScanCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.lbtxtScanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lbtxtScanCode.Location = new System.Drawing.Point(22, 9);
            this.lbtxtScanCode.Name = "lbtxtScanCode";
            this.lbtxtScanCode.Size = new System.Drawing.Size(95, 27);
            this.lbtxtScanCode.TabIndex = 9;
            this.lbtxtScanCode.Text = "FPC编码:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(10, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(764, 75);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.lbtxtScanCode, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtScanCode, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtScanRule, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbtxtScanRule, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(758, 45);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // QrPanel
            // 
            this.QrPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.QrPanel.Controls.Add(this.tableLayoutPanel1);
            this.QrPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.QrPanel.Location = new System.Drawing.Point(10, 260);
            this.QrPanel.Name = "QrPanel";
            this.QrPanel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.QrPanel.Size = new System.Drawing.Size(764, 159);
            this.QrPanel.TabIndex = 13;
            this.QrPanel.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtSendCode, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbtxtSendCode, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbtxtQrCode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtQrCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbtxtPlainCode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtLCDCode, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 32);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(758, 124);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 165);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 10);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 250);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(764, 10);
            this.panel2.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 419);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(764, 10);
            this.panel3.TabIndex = 16;
            // 
            // frmPrinter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.txt_ErrNote);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.QrPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ParameterGroupBox);
            this.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrinter";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CCS-3000系列机器通信喷码";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrinter_FormClosed);
            this.Load += new System.EventHandler(this.frmPrinter_Load);
            this.ParameterGroupBox.ResumeLayout(false);
            this.ParameterGroupBox.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.QrPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ParameterGroupBox;
        private System.Windows.Forms.Label lbcmbPrinterBaudRate;
        private System.Windows.Forms.Label lbcmbPrinterPorts;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtQrCode;
        private System.Windows.Forms.Label lbtxtQrCode;
        private System.Windows.Forms.TextBox txtLCDCode;
        private System.Windows.Forms.ComboBox cmbPrinterBaudRate;
        private System.Windows.Forms.ComboBox cmbPrinterPorts;
        private System.Windows.Forms.Label lbtxtPlainCode;
        private System.Windows.Forms.TextBox txtScanCode;
        private System.Windows.Forms.TextBox txt_ErrNote;
        private System.Windows.Forms.Label lbtxtScanCode;
        private System.Windows.Forms.TextBox txtReciveServerPort;
        private System.Windows.Forms.Label lbtxtReciveServerPort;
        private System.Windows.Forms.TextBox txtReciveServerIP;
        private System.Windows.Forms.Label lbtxtReciveServerIP;
        private System.Windows.Forms.TextBox txtSendCode;
        private System.Windows.Forms.Label lbtxtSendCode;
        private System.IO.Ports.SerialPort PortPrinter;
        private System.Windows.Forms.CheckBox chkRepeat;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.TextBox txtScanRule;
        private System.Windows.Forms.Label lbtxtScanRule;
        private System.Windows.Forms.TextBox txtScan710ServerPort;
        private System.Windows.Forms.Label lbtxtScan710ServerPort;
        private System.Windows.Forms.TextBox txtScan710ServerIP;
        private System.Windows.Forms.Label lbtxtScan710ServerIP;
        private System.Windows.Forms.Button btn710Scan;
        private System.Windows.Forms.CheckBox chk710Scan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox QrPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}

