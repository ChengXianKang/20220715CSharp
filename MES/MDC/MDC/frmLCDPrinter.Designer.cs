namespace MDC
{
    partial class frmLCDPrinter
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

        #region  Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLCDPrinter));
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.txtSerialPort = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtSPLName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOpCode = new System.Windows.Forms.TextBox();
            this.lblReaderAddress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProcessIP = new System.Windows.Forms.TextBox();
            this.lblPrinterIP = new System.Windows.Forms.Label();
            this.txtReaderAddress = new System.Windows.Forms.TextBox();
            this.tlpCode = new System.Windows.Forms.TableLayoutPanel();
            this.txtRule1 = new System.Windows.Forms.TextBox();
            this.tlpDebug = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblYield = new System.Windows.Forms.Label();
            this.txtYield = new System.Windows.Forms.TextBox();
            this.flpScanNote = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtRuleCount = new System.Windows.Forms.TextBox();
            this.txtNGCount = new System.Windows.Forms.TextBox();
            this.txtOKCount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLCDCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCode1 = new System.Windows.Forms.Label();
            this.txtCodeLCD = new System.Windows.Forms.TextBox();
            this.lblCode2 = new System.Windows.Forms.Label();
            this.txtCodePrint = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlNote = new System.Windows.Forms.Panel();
            this.lvwNote = new Utils.ListViewNote(this.components);
            this.pnlDebug = new System.Windows.Forms.Panel();
            this.txtGlassCode = new System.Windows.Forms.TextBox();
            this.btnCode2 = new System.Windows.Forms.Button();
            this.btnCode1 = new System.Windows.Forms.Button();
            this.txtTP = new System.Windows.Forms.TextBox();
            this.txtFPC = new System.Windows.Forms.TextBox();
            this.btnGlass = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlSplit1 = new System.Windows.Forms.Panel();
            this.tlpInfo.SuspendLayout();
            this.tlpCode.SuspendLayout();
            this.tlpDebug.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlNote.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpInfo
            // 
            this.tlpInfo.AutoSize = true;
            this.tlpInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpInfo.ColumnCount = 7;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tlpInfo.Controls.Add(this.txtSerialPort, 3, 1);
            this.tlpInfo.Controls.Add(this.btnReset, 6, 0);
            this.tlpInfo.Controls.Add(this.txtSPLName, 1, 0);
            this.tlpInfo.Controls.Add(this.label12, 0, 0);
            this.tlpInfo.Controls.Add(this.label4, 2, 0);
            this.tlpInfo.Controls.Add(this.txtProcess, 3, 0);
            this.tlpInfo.Controls.Add(this.label13, 4, 0);
            this.tlpInfo.Controls.Add(this.txtOpCode, 5, 0);
            this.tlpInfo.Controls.Add(this.lblReaderAddress, 4, 1);
            this.tlpInfo.Controls.Add(this.label1, 0, 1);
            this.tlpInfo.Controls.Add(this.txtProcessIP, 1, 1);
            this.tlpInfo.Controls.Add(this.lblPrinterIP, 2, 1);
            this.tlpInfo.Controls.Add(this.txtReaderAddress, 5, 1);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpInfo.Location = new System.Drawing.Point(5, 5);
            this.tlpInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 2;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInfo.Size = new System.Drawing.Size(784, 64);
            this.tlpInfo.TabIndex = 100;
            // 
            // txtSerialPort
            // 
            this.txtSerialPort.BackColor = System.Drawing.Color.Lavender;
            this.txtSerialPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerialPort.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSerialPort.ForeColor = System.Drawing.Color.Black;
            this.txtSerialPort.Location = new System.Drawing.Point(324, 35);
            this.txtSerialPort.Name = "txtSerialPort";
            this.txtSerialPort.ReadOnly = true;
            this.txtSerialPort.Size = new System.Drawing.Size(122, 26);
            this.txtSerialPort.TabIndex = 131;
            this.txtSerialPort.TabStop = false;
            this.txtSerialPort.Tag = "";
            this.txtSerialPort.Text = "COM1";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnReset.BackColor = System.Drawing.SystemColors.Control;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnReset.FlatAppearance.BorderSize = 2;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnReset.Location = new System.Drawing.Point(701, 5);
            this.btnReset.Name = "btnReset";
            this.tlpInfo.SetRowSpan(this.btnReset, 2);
            this.btnReset.Size = new System.Drawing.Size(80, 53);
            this.btnReset.TabIndex = 2;
            this.btnReset.Tag = "Start";
            this.btnReset.Text = "复 位";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtSPLName
            // 
            this.txtSPLName.BackColor = System.Drawing.Color.Lavender;
            this.txtSPLName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSPLName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSPLName.ForeColor = System.Drawing.Color.Black;
            this.txtSPLName.Location = new System.Drawing.Point(93, 3);
            this.txtSPLName.Name = "txtSPLName";
            this.txtSPLName.ReadOnly = true;
            this.txtSPLName.Size = new System.Drawing.Size(122, 26);
            this.txtSPLName.TabIndex = 10;
            this.txtSPLName.TabStop = false;
            this.txtSPLName.Text = "产线名称";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label12.Location = new System.Drawing.Point(3, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 20);
            this.label12.TabIndex = 115;
            this.label12.Text = "产线名称:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(234, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 110;
            this.label4.Text = "本站工序:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProcess
            // 
            this.txtProcess.BackColor = System.Drawing.Color.Lavender;
            this.txtProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcess.ForeColor = System.Drawing.Color.Black;
            this.txtProcess.Location = new System.Drawing.Point(324, 3);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(122, 26);
            this.txtProcess.TabIndex = 13;
            this.txtProcess.TabStop = false;
            this.txtProcess.Tag = "EC888";
            this.txtProcess.Text = "TP贴合";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label13.Location = new System.Drawing.Point(465, 6);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 20);
            this.label13.TabIndex = 116;
            this.label13.Text = "员工账号:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOpCode
            // 
            this.txtOpCode.BackColor = System.Drawing.Color.Lavender;
            this.txtOpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOpCode.ForeColor = System.Drawing.Color.Black;
            this.txtOpCode.Location = new System.Drawing.Point(555, 3);
            this.txtOpCode.Name = "txtOpCode";
            this.txtOpCode.ReadOnly = true;
            this.txtOpCode.Size = new System.Drawing.Size(134, 26);
            this.txtOpCode.TabIndex = 15;
            this.txtOpCode.TabStop = false;
            this.txtOpCode.Text = "8888";
            // 
            // lblReaderAddress
            // 
            this.lblReaderAddress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblReaderAddress.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReaderAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblReaderAddress.Location = new System.Drawing.Point(465, 38);
            this.lblReaderAddress.Margin = new System.Windows.Forms.Padding(3);
            this.lblReaderAddress.Name = "lblReaderAddress";
            this.lblReaderAddress.Size = new System.Drawing.Size(84, 20);
            this.lblReaderAddress.TabIndex = 104;
            this.lblReaderAddress.Text = "扫码枪IP:";
            this.lblReaderAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 121;
            this.label1.Text = "本  机 IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProcessIP
            // 
            this.txtProcessIP.BackColor = System.Drawing.Color.Lavender;
            this.txtProcessIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcessIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcessIP.ForeColor = System.Drawing.Color.Black;
            this.txtProcessIP.Location = new System.Drawing.Point(93, 35);
            this.txtProcessIP.Name = "txtProcessIP";
            this.txtProcessIP.ReadOnly = true;
            this.txtProcessIP.Size = new System.Drawing.Size(122, 26);
            this.txtProcessIP.TabIndex = 14;
            this.txtProcessIP.TabStop = false;
            this.txtProcessIP.Text = "当前站点IP";
            // 
            // lblPrinterIP
            // 
            this.lblPrinterIP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPrinterIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrinterIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblPrinterIP.Location = new System.Drawing.Point(234, 38);
            this.lblPrinterIP.Margin = new System.Windows.Forms.Padding(3);
            this.lblPrinterIP.Name = "lblPrinterIP";
            this.lblPrinterIP.Size = new System.Drawing.Size(84, 20);
            this.lblPrinterIP.TabIndex = 119;
            this.lblPrinterIP.Text = "喷码机串口:";
            this.lblPrinterIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReaderAddress
            // 
            this.txtReaderAddress.BackColor = System.Drawing.Color.Lavender;
            this.txtReaderAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReaderAddress.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReaderAddress.ForeColor = System.Drawing.Color.Black;
            this.txtReaderAddress.Location = new System.Drawing.Point(555, 35);
            this.txtReaderAddress.Name = "txtReaderAddress";
            this.txtReaderAddress.ReadOnly = true;
            this.txtReaderAddress.Size = new System.Drawing.Size(134, 26);
            this.txtReaderAddress.TabIndex = 123;
            this.txtReaderAddress.TabStop = false;
            this.txtReaderAddress.Tag = "";
            this.txtReaderAddress.Text = "172.16.8.37";
            // 
            // tlpCode
            // 
            this.tlpCode.AutoSize = true;
            this.tlpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpCode.ColumnCount = 8;
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tlpCode.Controls.Add(this.txtRule1, 7, 0);
            this.tlpCode.Controls.Add(this.tlpDebug, 4, 2);
            this.tlpCode.Controls.Add(this.flpScanNote, 6, 2);
            this.tlpCode.Controls.Add(this.tableLayoutPanel1, 1, 2);
            this.tlpCode.Controls.Add(this.label5, 0, 2);
            this.tlpCode.Controls.Add(this.lblCode1, 0, 0);
            this.tlpCode.Controls.Add(this.txtCodeLCD, 1, 0);
            this.tlpCode.Controls.Add(this.lblCode2, 0, 1);
            this.tlpCode.Controls.Add(this.txtCodePrint, 1, 1);
            this.tlpCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCode.Location = new System.Drawing.Point(5, 75);
            this.tlpCode.Name = "tlpCode";
            this.tlpCode.RowCount = 3;
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCode.Size = new System.Drawing.Size(784, 96);
            this.tlpCode.TabIndex = 100;
            // 
            // txtRule1
            // 
            this.txtRule1.BackColor = System.Drawing.Color.Lavender;
            this.txtRule1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRule1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRule1.ForeColor = System.Drawing.Color.Black;
            this.txtRule1.Location = new System.Drawing.Point(681, 3);
            this.txtRule1.Name = "txtRule1";
            this.txtRule1.ReadOnly = true;
            this.txtRule1.Size = new System.Drawing.Size(88, 26);
            this.txtRule1.TabIndex = 131;
            this.txtRule1.TabStop = false;
            this.txtRule1.Tag = "";
            // 
            // tlpDebug
            // 
            this.tlpDebug.ColumnCount = 3;
            this.tlpCode.SetColumnSpan(this.tlpDebug, 2);
            this.tlpDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tlpDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tlpDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDebug.Controls.Add(this.label9, 2, 0);
            this.tlpDebug.Controls.Add(this.lblYield, 0, 0);
            this.tlpDebug.Controls.Add(this.txtYield, 1, 0);
            this.tlpDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDebug.Location = new System.Drawing.Point(452, 64);
            this.tlpDebug.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDebug.Name = "tlpDebug";
            this.tlpDebug.RowCount = 1;
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDebug.Size = new System.Drawing.Size(158, 32);
            this.tlpDebug.TabIndex = 125;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label9.Location = new System.Drawing.Point(138, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 20);
            this.label9.TabIndex = 126;
            this.label9.Text = "%";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblYield
            // 
            this.lblYield.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblYield.BackColor = System.Drawing.Color.Transparent;
            this.lblYield.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYield.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblYield.Location = new System.Drawing.Point(5, 6);
            this.lblYield.Margin = new System.Windows.Forms.Padding(3);
            this.lblYield.Name = "lblYield";
            this.lblYield.Size = new System.Drawing.Size(55, 20);
            this.lblYield.TabIndex = 125;
            this.lblYield.Text = "不良率:";
            this.lblYield.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtYield
            // 
            this.txtYield.BackColor = System.Drawing.Color.Lavender;
            this.txtYield.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYield.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtYield.ForeColor = System.Drawing.Color.Black;
            this.txtYield.Location = new System.Drawing.Point(66, 3);
            this.txtYield.Name = "txtYield";
            this.txtYield.Size = new System.Drawing.Size(66, 26);
            this.txtYield.TabIndex = 125;
            this.txtYield.Text = "0.00";
            this.txtYield.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // flpScanNote
            // 
            this.flpScanNote.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flpScanNote.BackColor = System.Drawing.Color.Lavender;
            this.flpScanNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.flpScanNote, 2);
            this.flpScanNote.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpScanNote.Location = new System.Drawing.Point(616, 66);
            this.flpScanNote.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
            this.flpScanNote.Name = "flpScanNote";
            this.flpScanNote.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.flpScanNote.Size = new System.Drawing.Size(160, 27);
            this.flpScanNote.TabIndex = 128;
            this.flpScanNote.SizeChanged += new System.EventHandler(this.flpScanNote_SizeChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tlpCode.SetColumnSpan(this.tableLayoutPanel1, 3);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Controls.Add(this.txtRuleCount, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNGCount, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtOKCount, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label15, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtLCDCount, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(90, 64);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(362, 32);
            this.tableLayoutPanel1.TabIndex = 122;
            // 
            // txtRuleCount
            // 
            this.txtRuleCount.BackColor = System.Drawing.Color.Lavender;
            this.txtRuleCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRuleCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRuleCount.ForeColor = System.Drawing.Color.Black;
            this.txtRuleCount.Location = new System.Drawing.Point(309, 3);
            this.txtRuleCount.Name = "txtRuleCount";
            this.txtRuleCount.ReadOnly = true;
            this.txtRuleCount.Size = new System.Drawing.Size(46, 26);
            this.txtRuleCount.TabIndex = 119;
            this.txtRuleCount.Text = "0";
            this.txtRuleCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // txtNGCount
            // 
            this.txtNGCount.BackColor = System.Drawing.Color.Lavender;
            this.txtNGCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNGCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNGCount.ForeColor = System.Drawing.Color.Black;
            this.txtNGCount.Location = new System.Drawing.Point(207, 3);
            this.txtNGCount.Name = "txtNGCount";
            this.txtNGCount.ReadOnly = true;
            this.txtNGCount.Size = new System.Drawing.Size(43, 26);
            this.txtNGCount.TabIndex = 119;
            this.txtNGCount.Text = "0";
            this.txtNGCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // txtOKCount
            // 
            this.txtOKCount.BackColor = System.Drawing.Color.Lavender;
            this.txtOKCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOKCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOKCount.ForeColor = System.Drawing.Color.Black;
            this.txtOKCount.Location = new System.Drawing.Point(105, 3);
            this.txtOKCount.Name = "txtOKCount";
            this.txtOKCount.ReadOnly = true;
            this.txtOKCount.Size = new System.Drawing.Size(43, 26);
            this.txtOKCount.TabIndex = 120;
            this.txtOKCount.Text = "0";
            this.txtOKCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.DarkOrange;
            this.label15.Location = new System.Drawing.Point(260, 6);
            this.label15.Margin = new System.Windows.Forms.Padding(3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 20);
            this.label15.TabIndex = 121;
            this.label15.Text = "异常:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(158, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 20);
            this.label7.TabIndex = 121;
            this.label7.Text = "NG:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLCDCount
            // 
            this.txtLCDCount.BackColor = System.Drawing.Color.Lavender;
            this.txtLCDCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLCDCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLCDCount.ForeColor = System.Drawing.Color.Black;
            this.txtLCDCount.Location = new System.Drawing.Point(3, 3);
            this.txtLCDCount.Name = "txtLCDCount";
            this.txtLCDCount.ReadOnly = true;
            this.txtLCDCount.Size = new System.Drawing.Size(43, 26);
            this.txtLCDCount.TabIndex = 118;
            this.txtLCDCount.Text = "0";
            this.txtLCDCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(56, 6);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 20);
            this.label8.TabIndex = 122;
            this.label8.Text = "OK:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(3, 70);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 120;
            this.label5.Text = "玻璃计数:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode1
            // 
            this.lblCode1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCode1.BackColor = System.Drawing.Color.Transparent;
            this.lblCode1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblCode1.Location = new System.Drawing.Point(3, 6);
            this.lblCode1.Margin = new System.Windows.Forms.Padding(3);
            this.lblCode1.Name = "lblCode1";
            this.lblCode1.Size = new System.Drawing.Size(84, 20);
            this.lblCode1.TabIndex = 113;
            this.lblCode1.Text = "扫描编码:";
            this.lblCode1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodeLCD
            // 
            this.txtCodeLCD.BackColor = System.Drawing.Color.White;
            this.txtCodeLCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtCodeLCD, 6);
            this.txtCodeLCD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCodeLCD.ForeColor = System.Drawing.Color.Black;
            this.txtCodeLCD.Location = new System.Drawing.Point(93, 3);
            this.txtCodeLCD.Name = "txtCodeLCD";
            this.txtCodeLCD.ReadOnly = true;
            this.txtCodeLCD.Size = new System.Drawing.Size(582, 26);
            this.txtCodeLCD.TabIndex = 6;
            // 
            // lblCode2
            // 
            this.lblCode2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCode2.BackColor = System.Drawing.Color.Transparent;
            this.lblCode2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblCode2.Location = new System.Drawing.Point(3, 38);
            this.lblCode2.Margin = new System.Windows.Forms.Padding(3);
            this.lblCode2.Name = "lblCode2";
            this.lblCode2.Size = new System.Drawing.Size(84, 20);
            this.lblCode2.TabIndex = 115;
            this.lblCode2.Text = "转喷编码:";
            this.lblCode2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodePrint
            // 
            this.txtCodePrint.BackColor = System.Drawing.Color.Lavender;
            this.txtCodePrint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtCodePrint, 6);
            this.txtCodePrint.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCodePrint.ForeColor = System.Drawing.Color.Black;
            this.txtCodePrint.Location = new System.Drawing.Point(93, 35);
            this.txtCodePrint.Name = "txtCodePrint";
            this.txtCodePrint.ReadOnly = true;
            this.txtCodePrint.Size = new System.Drawing.Size(582, 26);
            this.txtCodePrint.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnClear.FlatAppearance.BorderSize = 2;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnClear.Location = new System.Drawing.Point(598, 33);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 26);
            this.btnClear.TabIndex = 123;
            this.btnClear.Tag = "";
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnCountClear_Click);
            // 
            // pnlNote
            // 
            this.pnlNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.pnlNote.Controls.Add(this.lvwNote);
            this.pnlNote.Controls.Add(this.pnlDebug);
            this.pnlNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNote.Location = new System.Drawing.Point(5, 177);
            this.pnlNote.Name = "pnlNote";
            this.pnlNote.Padding = new System.Windows.Forms.Padding(3);
            this.pnlNote.Size = new System.Drawing.Size(784, 349);
            this.pnlNote.TabIndex = 105;
            // 
            // lvwNote
            // 
            this.lvwNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.lvwNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwNote.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvwNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lvwNote.FullRowSelect = true;
            this.lvwNote.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwNote.HideSelection = false;
            this.lvwNote.Location = new System.Drawing.Point(3, 3);
            this.lvwNote.Name = "lvwNote";
            this.lvwNote.ShowItemToolTips = true;
            this.lvwNote.Size = new System.Drawing.Size(778, 284);
            this.lvwNote.TabIndex = 9;
            this.lvwNote.UseCompatibleStateImageBehavior = false;
            this.lvwNote.View = System.Windows.Forms.View.Details;
            // 
            // pnlDebug
            // 
            this.pnlDebug.Controls.Add(this.txtGlassCode);
            this.pnlDebug.Controls.Add(this.btnCode2);
            this.pnlDebug.Controls.Add(this.btnCode1);
            this.pnlDebug.Controls.Add(this.txtTP);
            this.pnlDebug.Controls.Add(this.txtFPC);
            this.pnlDebug.Controls.Add(this.btnGlass);
            this.pnlDebug.Controls.Add(this.btnClear);
            this.pnlDebug.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDebug.Location = new System.Drawing.Point(3, 287);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(778, 59);
            this.pnlDebug.TabIndex = 126;
            this.pnlDebug.Visible = false;
            // 
            // txtGlassCode
            // 
            this.txtGlassCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtGlassCode.BackColor = System.Drawing.Color.White;
            this.txtGlassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGlassCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGlassCode.ForeColor = System.Drawing.Color.Black;
            this.txtGlassCode.Location = new System.Drawing.Point(440, 4);
            this.txtGlassCode.Name = "txtGlassCode";
            this.txtGlassCode.Size = new System.Drawing.Size(228, 26);
            this.txtGlassCode.TabIndex = 130;
            this.txtGlassCode.Text = "玻璃码";
            // 
            // btnCode2
            // 
            this.btnCode2.Location = new System.Drawing.Point(350, 29);
            this.btnCode2.Name = "btnCode2";
            this.btnCode2.Size = new System.Drawing.Size(68, 26);
            this.btnCode2.TabIndex = 129;
            this.btnCode2.Text = "Code2";
            this.btnCode2.UseVisualStyleBackColor = true;
            this.btnCode2.Click += new System.EventHandler(this.btnTP_Click);
            // 
            // btnCode1
            // 
            this.btnCode1.Location = new System.Drawing.Point(350, 3);
            this.btnCode1.Name = "btnCode1";
            this.btnCode1.Size = new System.Drawing.Size(68, 26);
            this.btnCode1.TabIndex = 128;
            this.btnCode1.Text = "Code1";
            this.btnCode1.UseVisualStyleBackColor = true;
            this.btnCode1.Click += new System.EventHandler(this.btnFPC_Click);
            // 
            // txtTP
            // 
            this.txtTP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTP.BackColor = System.Drawing.Color.White;
            this.txtTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTP.ForeColor = System.Drawing.Color.Black;
            this.txtTP.Location = new System.Drawing.Point(72, 30);
            this.txtTP.Name = "txtTP";
            this.txtTP.Size = new System.Drawing.Size(272, 26);
            this.txtTP.TabIndex = 127;
            // 
            // txtFPC
            // 
            this.txtFPC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtFPC.BackColor = System.Drawing.Color.White;
            this.txtFPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFPC.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFPC.ForeColor = System.Drawing.Color.Black;
            this.txtFPC.Location = new System.Drawing.Point(72, 3);
            this.txtFPC.Name = "txtFPC";
            this.txtFPC.Size = new System.Drawing.Size(272, 26);
            this.txtFPC.TabIndex = 126;
            // 
            // btnGlass
            // 
            this.btnGlass.Location = new System.Drawing.Point(4, 2);
            this.btnGlass.Name = "btnGlass";
            this.btnGlass.Size = new System.Drawing.Size(62, 56);
            this.btnGlass.TabIndex = 125;
            this.btnGlass.Text = "新玻璃";
            this.btnGlass.UseVisualStyleBackColor = true;
            this.btnGlass.Click += new System.EventHandler(this.btnGlass_Click);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(5, 171);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(784, 6);
            this.panel4.TabIndex = 106;
            // 
            // pnlSplit1
            // 
            this.pnlSplit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit1.Location = new System.Drawing.Point(5, 69);
            this.pnlSplit1.Name = "pnlSplit1";
            this.pnlSplit1.Size = new System.Drawing.Size(784, 6);
            this.pnlSplit1.TabIndex = 108;
            // 
            // frmLCDPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(794, 531);
            this.Controls.Add(this.pnlNote);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.tlpCode);
            this.Controls.Add(this.pnlSplit1);
            this.Controls.Add(this.tlpInfo);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmLCDPrinter";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动读转喷";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
            this.tlpCode.ResumeLayout(false);
            this.tlpCode.PerformLayout();
            this.tlpDebug.ResumeLayout(false);
            this.tlpDebug.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlNote.ResumeLayout(false);
            this.pnlDebug.ResumeLayout(false);
            this.pnlDebug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtSPLName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblPrinterIP;
        private System.Windows.Forms.Label lblReaderAddress;
        private System.Windows.Forms.TextBox txtProcessIP;
        private System.Windows.Forms.TableLayoutPanel tlpCode;
        private System.Windows.Forms.TextBox txtCodePrint;
        private System.Windows.Forms.Label lblCode2;
        private System.Windows.Forms.TextBox txtCodeLCD;
        private System.Windows.Forms.Label lblCode1;
        private System.Windows.Forms.Panel pnlNote;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlSplit1;
        private Utils.ListViewNote lvwNote;
        private System.Windows.Forms.TextBox txtOpCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtReaderAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLCDCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNGCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtOKCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tlpDebug;
        private System.Windows.Forms.TextBox txtYield;
        private System.Windows.Forms.Label lblYield;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlDebug;
        private System.Windows.Forms.TextBox txtRuleCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.FlowLayoutPanel flpScanNote;
        private System.Windows.Forms.Button btnGlass;
        private System.Windows.Forms.Button btnCode2;
        private System.Windows.Forms.Button btnCode1;
        private System.Windows.Forms.TextBox txtTP;
        private System.Windows.Forms.TextBox txtFPC;
        private System.Windows.Forms.TextBox txtGlassCode;
        private System.Windows.Forms.TextBox txtSerialPort;
        private System.Windows.Forms.TextBox txtRule1;

    }
}