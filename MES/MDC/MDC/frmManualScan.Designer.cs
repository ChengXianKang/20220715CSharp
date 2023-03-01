namespace MDC
{
    partial class frmManualScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManualScan));
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSPLName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnNG = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comProcess = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProcessIP = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOpCode = new System.Windows.Forms.TextBox();
            this.lblMouldCode = new System.Windows.Forms.Label();
            this.txtMouldCode = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtEarlyProcess = new System.Windows.Forms.TextBox();
            this.tlpCode = new System.Windows.Forms.TableLayoutPanel();
            this.label17 = new System.Windows.Forms.Label();
            this.flpScanNote = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtErrorCount = new System.Windows.Forms.TextBox();
            this.txtNGCount = new System.Windows.Forms.TextBox();
            this.txtOKCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLCDCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtScanCode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtglassCode = new System.Windows.Forms.TextBox();
            this.lblRule1 = new System.Windows.Forms.Label();
            this.txtRule1 = new System.Windows.Forms.TextBox();
            this.txtRule2 = new System.Windows.Forms.TextBox();
            this.lblRule2 = new System.Windows.Forms.Label();
            this.txtCode1 = new System.Windows.Forms.TextBox();
            this.lblCode1 = new System.Windows.Forms.Label();
            this.lblCode2 = new System.Windows.Forms.Label();
            this.txtCode2 = new System.Windows.Forms.TextBox();
            this.txtDigit1 = new System.Windows.Forms.TextBox();
            this.txtDigit2 = new System.Windows.Forms.TextBox();
            this.pnlNote = new System.Windows.Forms.Panel();
            this.pnlTest = new System.Windows.Forms.Panel();
            this.txtProcessNumber = new System.Windows.Forms.TextBox();
            this.txtProcessCode = new System.Windows.Forms.TextBox();
            this.lvwNote = new Utils.ListViewNote(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlSplit1 = new System.Windows.Forms.Panel();
            this.pnlNG = new System.Windows.Forms.Panel();
            this.flpNG = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescribe = new System.Windows.Forms.TextBox();
            this.bgwWriteData = new System.ComponentModel.BackgroundWorker();
            this.tlpInfo.SuspendLayout();
            this.tlpCode.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlNote.SuspendLayout();
            this.pnlTest.SuspendLayout();
            this.pnlNG.SuspendLayout();
            this.flpNG.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpInfo
            // 
            this.tlpInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpInfo.ColumnCount = 7;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tlpInfo.Controls.Add(this.txtOrder, 1, 2);
            this.tlpInfo.Controls.Add(this.txtModel, 3, 2);
            this.tlpInfo.Controls.Add(this.label2, 2, 2);
            this.tlpInfo.Controls.Add(this.txtSPLName, 1, 0);
            this.tlpInfo.Controls.Add(this.label14, 0, 2);
            this.tlpInfo.Controls.Add(this.label12, 0, 0);
            this.tlpInfo.Controls.Add(this.btnNG, 6, 0);
            this.tlpInfo.Controls.Add(this.btnReset, 6, 2);
            this.tlpInfo.Controls.Add(this.label6, 0, 1);
            this.tlpInfo.Controls.Add(this.comProcess, 1, 1);
            this.tlpInfo.Controls.Add(this.label1, 2, 0);
            this.tlpInfo.Controls.Add(this.txtProcessIP, 3, 0);
            this.tlpInfo.Controls.Add(this.label13, 4, 0);
            this.tlpInfo.Controls.Add(this.txtOpCode, 5, 0);
            this.tlpInfo.Controls.Add(this.lblMouldCode, 2, 1);
            this.tlpInfo.Controls.Add(this.txtMouldCode, 3, 1);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpInfo.Location = new System.Drawing.Point(5, 5);
            this.tlpInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 3;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.Size = new System.Drawing.Size(784, 102);
            this.tlpInfo.TabIndex = 100;
            // 
            // txtOrder
            // 
            this.txtOrder.BackColor = System.Drawing.Color.Lavender;
            this.txtOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrder.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtOrder.Location = new System.Drawing.Point(93, 71);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.ReadOnly = true;
            this.txtOrder.Size = new System.Drawing.Size(162, 31);
            this.txtOrder.TabIndex = 124;
            this.txtOrder.TabStop = false;
            this.txtOrder.Tag = "EC888";
            // 
            // txtModel
            // 
            this.txtModel.BackColor = System.Drawing.Color.Lavender;
            this.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpInfo.SetColumnSpan(this.txtModel, 3);
            this.txtModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtModel.Location = new System.Drawing.Point(351, 71);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(336, 31);
            this.txtModel.TabIndex = 123;
            this.txtModel.TabStop = false;
            this.txtModel.Tag = "EC888";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(274, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 122;
            this.label2.Text = "成品型号:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSPLName
            // 
            this.txtSPLName.BackColor = System.Drawing.Color.Lavender;
            this.txtSPLName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSPLName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSPLName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtSPLName.Location = new System.Drawing.Point(93, 3);
            this.txtSPLName.Name = "txtSPLName";
            this.txtSPLName.ReadOnly = true;
            this.txtSPLName.Size = new System.Drawing.Size(162, 31);
            this.txtSPLName.TabIndex = 10;
            this.txtSPLName.TabStop = false;
            this.txtSPLName.Text = "产线名称";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label14.Location = new System.Drawing.Point(4, 75);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 20);
            this.label14.TabIndex = 119;
            this.label14.Text = "生产工单:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label12.Location = new System.Drawing.Point(4, 7);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 20);
            this.label12.TabIndex = 115;
            this.label12.Text = "产线名称:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnNG
            // 
            this.btnNG.BackColor = System.Drawing.SystemColors.Control;
            this.btnNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNG.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnNG.FlatAppearance.BorderSize = 2;
            this.btnNG.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnNG.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNG.ForeColor = System.Drawing.Color.DarkRed;
            this.btnNG.Location = new System.Drawing.Point(693, 3);
            this.btnNG.Name = "btnNG";
            this.tlpInfo.SetRowSpan(this.btnNG, 2);
            this.btnNG.Size = new System.Drawing.Size(88, 62);
            this.btnNG.TabIndex = 74;
            this.btnNG.Tag = "";
            this.btnNG.Text = "不良";
            this.btnNG.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNG.UseVisualStyleBackColor = true;
            this.btnNG.Click += new System.EventHandler(this.btnNG_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.Control;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnReset.FlatAppearance.BorderSize = 2;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnReset.Location = new System.Drawing.Point(693, 71);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(88, 28);
            this.btnReset.TabIndex = 2;
            this.btnReset.Tag = "";
            this.btnReset.Text = "复 位";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label6.Location = new System.Drawing.Point(4, 41);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 111;
            this.label6.Text = "本站工序:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comProcess
            // 
            this.comProcess.BackColor = System.Drawing.Color.White;
            this.comProcess.DropDownHeight = 200;
            this.comProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comProcess.DropDownWidth = 200;
            this.comProcess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comProcess.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comProcess.FormattingEnabled = true;
            this.comProcess.IntegralHeight = false;
            this.comProcess.Location = new System.Drawing.Point(93, 37);
            this.comProcess.Name = "comProcess";
            this.comProcess.Size = new System.Drawing.Size(162, 31);
            this.comProcess.TabIndex = 1;
            this.comProcess.SelectionChangeCommitted += new System.EventHandler(this.comProcess_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(274, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 121;
            this.label1.Text = "设  备 IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProcessIP
            // 
            this.txtProcessIP.BackColor = System.Drawing.Color.Lavender;
            this.txtProcessIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcessIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcessIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProcessIP.Location = new System.Drawing.Point(351, 3);
            this.txtProcessIP.Name = "txtProcessIP";
            this.txtProcessIP.ReadOnly = true;
            this.txtProcessIP.Size = new System.Drawing.Size(120, 31);
            this.txtProcessIP.TabIndex = 14;
            this.txtProcessIP.TabStop = false;
            this.txtProcessIP.Text = "当前站点IP";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label13.Location = new System.Drawing.Point(490, 7);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 20);
            this.label13.TabIndex = 116;
            this.label13.Text = "员工账号:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOpCode
            // 
            this.txtOpCode.BackColor = System.Drawing.Color.Lavender;
            this.txtOpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOpCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtOpCode.Location = new System.Drawing.Point(567, 3);
            this.txtOpCode.Name = "txtOpCode";
            this.txtOpCode.ReadOnly = true;
            this.txtOpCode.Size = new System.Drawing.Size(120, 31);
            this.txtOpCode.TabIndex = 15;
            this.txtOpCode.TabStop = false;
            this.txtOpCode.Text = "扫描人员工号";
            // 
            // lblMouldCode
            // 
            this.lblMouldCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMouldCode.BackColor = System.Drawing.Color.Transparent;
            this.lblMouldCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMouldCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblMouldCode.Location = new System.Drawing.Point(274, 41);
            this.lblMouldCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMouldCode.Name = "lblMouldCode";
            this.lblMouldCode.Size = new System.Drawing.Size(70, 20);
            this.lblMouldCode.TabIndex = 125;
            this.lblMouldCode.Text = "治具编码:";
            this.lblMouldCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMouldCode
            // 
            this.txtMouldCode.BackColor = System.Drawing.Color.Lavender;
            this.txtMouldCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpInfo.SetColumnSpan(this.txtMouldCode, 3);
            this.txtMouldCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtMouldCode.Location = new System.Drawing.Point(351, 37);
            this.txtMouldCode.Name = "txtMouldCode";
            this.txtMouldCode.ReadOnly = true;
            this.txtMouldCode.Size = new System.Drawing.Size(336, 31);
            this.txtMouldCode.TabIndex = 126;
            this.txtMouldCode.Text = "设备编码";
            this.txtMouldCode.Click += new System.EventHandler(this.txtMouldCode_Click);
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.Lavender;
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtPort.Location = new System.Drawing.Point(140, 96);
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(75, 31);
            this.txtPort.TabIndex = 12;
            this.txtPort.TabStop = false;
            this.txtPort.Tag = "9053";
            this.txtPort.Text = "9053";
            this.txtPort.Visible = false;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label11.Location = new System.Drawing.Point(141, 11);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 20);
            this.label11.TabIndex = 104;
            this.label11.Text = "服务器IP:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Visible = false;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label10.Location = new System.Drawing.Point(141, 73);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 20);
            this.label10.TabIndex = 107;
            this.label10.Text = "连接端口:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Visible = false;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.BackColor = System.Drawing.Color.Lavender;
            this.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIPAddress.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIPAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtIPAddress.Location = new System.Drawing.Point(140, 39);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.ReadOnly = true;
            this.txtIPAddress.Size = new System.Drawing.Size(168, 31);
            this.txtIPAddress.TabIndex = 11;
            this.txtIPAddress.TabStop = false;
            this.txtIPAddress.Tag = "172.16.1.3";
            this.txtIPAddress.Text = "172.16.1.3";
            this.txtIPAddress.Visible = false;
            // 
            // txtEarlyProcess
            // 
            this.txtEarlyProcess.BackColor = System.Drawing.Color.White;
            this.txtEarlyProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEarlyProcess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEarlyProcess.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtEarlyProcess.Location = new System.Drawing.Point(4, 75);
            this.txtEarlyProcess.Name = "txtEarlyProcess";
            this.txtEarlyProcess.ReadOnly = true;
            this.txtEarlyProcess.Size = new System.Drawing.Size(129, 31);
            this.txtEarlyProcess.TabIndex = 16;
            this.txtEarlyProcess.TabStop = false;
            this.txtEarlyProcess.Text = "上一站点工序";
            this.txtEarlyProcess.Visible = false;
            // 
            // tlpCode
            // 
            this.tlpCode.AutoSize = true;
            this.tlpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpCode.ColumnCount = 8;
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpCode.Controls.Add(this.label17, 4, 3);
            this.tlpCode.Controls.Add(this.flpScanNote, 5, 3);
            this.tlpCode.Controls.Add(this.label5, 0, 3);
            this.tlpCode.Controls.Add(this.tableLayoutPanel1, 1, 3);
            this.tlpCode.Controls.Add(this.label8, 0, 0);
            this.tlpCode.Controls.Add(this.txtScanCode, 1, 0);
            this.tlpCode.Controls.Add(this.label15, 4, 0);
            this.tlpCode.Controls.Add(this.txtglassCode, 5, 0);
            this.tlpCode.Controls.Add(this.lblRule1, 4, 1);
            this.tlpCode.Controls.Add(this.txtRule1, 5, 1);
            this.tlpCode.Controls.Add(this.txtRule2, 5, 2);
            this.tlpCode.Controls.Add(this.lblRule2, 4, 2);
            this.tlpCode.Controls.Add(this.txtCode1, 1, 1);
            this.tlpCode.Controls.Add(this.lblCode1, 0, 1);
            this.tlpCode.Controls.Add(this.lblCode2, 0, 2);
            this.tlpCode.Controls.Add(this.txtCode2, 1, 2);
            this.tlpCode.Controls.Add(this.txtDigit1, 7, 1);
            this.tlpCode.Controls.Add(this.txtDigit2, 7, 2);
            this.tlpCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCode.Location = new System.Drawing.Point(5, 113);
            this.tlpCode.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpCode.Name = "tlpCode";
            this.tlpCode.RowCount = 4;
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33443F));
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33445F));
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33112F));
            this.tlpCode.Size = new System.Drawing.Size(784, 148);
            this.tlpCode.TabIndex = 100;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label17.Location = new System.Drawing.Point(478, 119);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 20);
            this.label17.TabIndex = 127;
            this.label17.Text = "过站状态:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flpScanNote
            // 
            this.flpScanNote.BackColor = System.Drawing.Color.Lavender;
            this.flpScanNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.flpScanNote, 3);
            this.flpScanNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpScanNote.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpScanNote.Location = new System.Drawing.Point(565, 112);
            this.flpScanNote.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
            this.flpScanNote.Name = "flpScanNote";
            this.flpScanNote.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.flpScanNote.Size = new System.Drawing.Size(216, 35);
            this.flpScanNote.TabIndex = 129;
            this.flpScanNote.SizeChanged += new System.EventHandler(this.flpScanNote_SizeChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(3, 119);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 127;
            this.label5.Text = "玻璃计数:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tlpCode.SetColumnSpan(this.tableLayoutPanel1, 3);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.txtErrorCount, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNGCount, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtOKCount, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtLCDCount, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(90, 111);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 37);
            this.tableLayoutPanel1.TabIndex = 123;
            // 
            // txtErrorCount
            // 
            this.txtErrorCount.BackColor = System.Drawing.Color.Lavender;
            this.txtErrorCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtErrorCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtErrorCount.ForeColor = System.Drawing.Color.Black;
            this.txtErrorCount.Location = new System.Drawing.Point(339, 3);
            this.txtErrorCount.Name = "txtErrorCount";
            this.txtErrorCount.ReadOnly = true;
            this.txtErrorCount.Size = new System.Drawing.Size(42, 31);
            this.txtErrorCount.TabIndex = 119;
            this.txtErrorCount.Text = "0";
            // 
            // txtNGCount
            // 
            this.txtNGCount.BackColor = System.Drawing.Color.Lavender;
            this.txtNGCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNGCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNGCount.ForeColor = System.Drawing.Color.Black;
            this.txtNGCount.Location = new System.Drawing.Point(227, 3);
            this.txtNGCount.Name = "txtNGCount";
            this.txtNGCount.ReadOnly = true;
            this.txtNGCount.Size = new System.Drawing.Size(41, 31);
            this.txtNGCount.TabIndex = 119;
            this.txtNGCount.Text = "0";
            // 
            // txtOKCount
            // 
            this.txtOKCount.BackColor = System.Drawing.Color.Lavender;
            this.txtOKCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOKCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOKCount.ForeColor = System.Drawing.Color.Black;
            this.txtOKCount.Location = new System.Drawing.Point(115, 3);
            this.txtOKCount.Name = "txtOKCount";
            this.txtOKCount.ReadOnly = true;
            this.txtOKCount.Size = new System.Drawing.Size(41, 31);
            this.txtOKCount.TabIndex = 120;
            this.txtOKCount.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.DarkOrange;
            this.label3.Location = new System.Drawing.Point(290, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 121;
            this.label3.Text = "异常:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(165, 5);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 26);
            this.label7.TabIndex = 121;
            this.label7.Text = "漏工序:";
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
            this.txtLCDCount.Size = new System.Drawing.Size(41, 31);
            this.txtLCDCount.TabIndex = 118;
            this.txtLCDCount.Text = "0";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(66, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 20);
            this.label4.TabIndex = 122;
            this.label4.Text = "OK:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label8.Location = new System.Drawing.Point(4, 8);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 20);
            this.label8.TabIndex = 109;
            this.label8.Text = "实时扫描:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtScanCode
            // 
            this.txtScanCode.BackColor = System.Drawing.Color.White;
            this.txtScanCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtScanCode, 3);
            this.txtScanCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtScanCode.Location = new System.Drawing.Point(93, 3);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.Size = new System.Drawing.Size(378, 31);
            this.txtScanCode.TabIndex = 3;
            this.txtScanCode.Text = "实际扫描数据";
            this.txtScanCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScanCode_KeyPress);
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label15.Location = new System.Drawing.Point(478, 8);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 20);
            this.label15.TabIndex = 119;
            this.label15.Text = "LCD 编码:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtglassCode
            // 
            this.txtglassCode.BackColor = System.Drawing.Color.Lavender;
            this.txtglassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtglassCode, 3);
            this.txtglassCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtglassCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtglassCode.Location = new System.Drawing.Point(567, 3);
            this.txtglassCode.Name = "txtglassCode";
            this.txtglassCode.ReadOnly = true;
            this.txtglassCode.Size = new System.Drawing.Size(212, 31);
            this.txtglassCode.TabIndex = 5;
            this.txtglassCode.Text = "玻璃码";
            // 
            // lblRule1
            // 
            this.lblRule1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRule1.BackColor = System.Drawing.Color.Transparent;
            this.lblRule1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRule1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblRule1.Location = new System.Drawing.Point(478, 45);
            this.lblRule1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRule1.Name = "lblRule1";
            this.lblRule1.Size = new System.Drawing.Size(82, 20);
            this.lblRule1.TabIndex = 121;
            this.lblRule1.Text = "规则&&长度:";
            this.lblRule1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRule1
            // 
            this.txtRule1.BackColor = System.Drawing.Color.Lavender;
            this.txtRule1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtRule1, 2);
            this.txtRule1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRule1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtRule1.Location = new System.Drawing.Point(567, 40);
            this.txtRule1.Name = "txtRule1";
            this.txtRule1.ReadOnly = true;
            this.txtRule1.Size = new System.Drawing.Size(165, 31);
            this.txtRule1.TabIndex = 124;
            // 
            // txtRule2
            // 
            this.txtRule2.BackColor = System.Drawing.Color.Lavender;
            this.txtRule2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtRule2, 2);
            this.txtRule2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRule2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtRule2.Location = new System.Drawing.Point(567, 77);
            this.txtRule2.Name = "txtRule2";
            this.txtRule2.ReadOnly = true;
            this.txtRule2.Size = new System.Drawing.Size(165, 31);
            this.txtRule2.TabIndex = 125;
            // 
            // lblRule2
            // 
            this.lblRule2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRule2.BackColor = System.Drawing.Color.Transparent;
            this.lblRule2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRule2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblRule2.Location = new System.Drawing.Point(478, 82);
            this.lblRule2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRule2.Name = "lblRule2";
            this.lblRule2.Size = new System.Drawing.Size(82, 20);
            this.lblRule2.TabIndex = 123;
            this.lblRule2.Text = "规则&&长度:";
            this.lblRule2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode1
            // 
            this.txtCode1.BackColor = System.Drawing.Color.Lavender;
            this.txtCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtCode1, 3);
            this.txtCode1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtCode1.Location = new System.Drawing.Point(93, 40);
            this.txtCode1.Name = "txtCode1";
            this.txtCode1.ReadOnly = true;
            this.txtCode1.Size = new System.Drawing.Size(378, 31);
            this.txtCode1.TabIndex = 6;
            // 
            // lblCode1
            // 
            this.lblCode1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCode1.BackColor = System.Drawing.Color.Transparent;
            this.lblCode1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblCode1.Location = new System.Drawing.Point(4, 45);
            this.lblCode1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCode1.Name = "lblCode1";
            this.lblCode1.Size = new System.Drawing.Size(82, 20);
            this.lblCode1.TabIndex = 113;
            this.lblCode1.Text = "1#编码:";
            this.lblCode1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode2
            // 
            this.lblCode2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCode2.BackColor = System.Drawing.Color.Transparent;
            this.lblCode2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblCode2.Location = new System.Drawing.Point(4, 82);
            this.lblCode2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCode2.Name = "lblCode2";
            this.lblCode2.Size = new System.Drawing.Size(82, 20);
            this.lblCode2.TabIndex = 115;
            this.lblCode2.Text = "2#编码:";
            this.lblCode2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode2
            // 
            this.txtCode2.BackColor = System.Drawing.Color.Lavender;
            this.txtCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtCode2, 3);
            this.txtCode2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtCode2.Location = new System.Drawing.Point(93, 77);
            this.txtCode2.Name = "txtCode2";
            this.txtCode2.ReadOnly = true;
            this.txtCode2.Size = new System.Drawing.Size(378, 31);
            this.txtCode2.TabIndex = 7;
            // 
            // txtDigit1
            // 
            this.txtDigit1.BackColor = System.Drawing.Color.Lavender;
            this.txtDigit1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDigit1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDigit1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtDigit1.Location = new System.Drawing.Point(738, 40);
            this.txtDigit1.Name = "txtDigit1";
            this.txtDigit1.ReadOnly = true;
            this.txtDigit1.Size = new System.Drawing.Size(40, 31);
            this.txtDigit1.TabIndex = 125;
            // 
            // txtDigit2
            // 
            this.txtDigit2.BackColor = System.Drawing.Color.Lavender;
            this.txtDigit2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDigit2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDigit2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtDigit2.Location = new System.Drawing.Point(738, 77);
            this.txtDigit2.Name = "txtDigit2";
            this.txtDigit2.ReadOnly = true;
            this.txtDigit2.Size = new System.Drawing.Size(40, 31);
            this.txtDigit2.TabIndex = 126;
            // 
            // pnlNote
            // 
            this.pnlNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.pnlNote.Controls.Add(this.pnlTest);
            this.pnlNote.Controls.Add(this.lvwNote);
            this.pnlNote.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNote.Location = new System.Drawing.Point(5, 267);
            this.pnlNote.Name = "pnlNote";
            this.pnlNote.Padding = new System.Windows.Forms.Padding(3);
            this.pnlNote.Size = new System.Drawing.Size(331, 259);
            this.pnlNote.TabIndex = 105;
            // 
            // pnlTest
            // 
            this.pnlTest.Controls.Add(this.txtProcessNumber);
            this.pnlTest.Controls.Add(this.txtProcessCode);
            this.pnlTest.Controls.Add(this.txtEarlyProcess);
            this.pnlTest.Controls.Add(this.label11);
            this.pnlTest.Controls.Add(this.txtIPAddress);
            this.pnlTest.Controls.Add(this.label10);
            this.pnlTest.Controls.Add(this.txtPort);
            this.pnlTest.Location = new System.Drawing.Point(8, 84);
            this.pnlTest.Name = "pnlTest";
            this.pnlTest.Size = new System.Drawing.Size(323, 151);
            this.pnlTest.TabIndex = 75;
            this.pnlTest.Visible = false;
            // 
            // txtProcessNumber
            // 
            this.txtProcessNumber.Enabled = false;
            this.txtProcessNumber.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcessNumber.Location = new System.Drawing.Point(4, 5);
            this.txtProcessNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProcessNumber.Name = "txtProcessNumber";
            this.txtProcessNumber.ReadOnly = true;
            this.txtProcessNumber.Size = new System.Drawing.Size(129, 31);
            this.txtProcessNumber.TabIndex = 69;
            this.txtProcessNumber.Text = "当前站点序号";
            this.txtProcessNumber.Visible = false;
            // 
            // txtProcessCode
            // 
            this.txtProcessCode.Enabled = false;
            this.txtProcessCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcessCode.Location = new System.Drawing.Point(4, 41);
            this.txtProcessCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProcessCode.Name = "txtProcessCode";
            this.txtProcessCode.ReadOnly = true;
            this.txtProcessCode.Size = new System.Drawing.Size(129, 31);
            this.txtProcessCode.TabIndex = 73;
            this.txtProcessCode.Text = "当前站点编码";
            this.txtProcessCode.Visible = false;
            // 
            // lvwNote
            // 
            this.lvwNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.lvwNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwNote.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvwNote.FullRowSelect = true;
            this.lvwNote.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwNote.HideSelection = false;
            this.lvwNote.Location = new System.Drawing.Point(3, 3);
            this.lvwNote.Name = "lvwNote";
            this.lvwNote.ShowItemToolTips = true;
            this.lvwNote.Size = new System.Drawing.Size(325, 253);
            this.lvwNote.TabIndex = 9;
            this.lvwNote.UseCompatibleStateImageBehavior = false;
            this.lvwNote.View = System.Windows.Forms.View.Details;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(5, 261);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(784, 6);
            this.panel4.TabIndex = 106;
            // 
            // pnlSplit1
            // 
            this.pnlSplit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit1.Location = new System.Drawing.Point(5, 107);
            this.pnlSplit1.Name = "pnlSplit1";
            this.pnlSplit1.Size = new System.Drawing.Size(784, 6);
            this.pnlSplit1.TabIndex = 108;
            // 
            // pnlNG
            // 
            this.pnlNG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.pnlNG.Controls.Add(this.flpNG);
            this.pnlNG.Controls.Add(this.label16);
            this.pnlNG.Controls.Add(this.tableLayoutPanel2);
            this.pnlNG.Controls.Add(this.tableLayoutPanel3);
            this.pnlNG.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNG.Location = new System.Drawing.Point(349, 267);
            this.pnlNG.Name = "pnlNG";
            this.pnlNG.Size = new System.Drawing.Size(440, 259);
            this.pnlNG.TabIndex = 109;
            // 
            // flpNG
            // 
            this.flpNG.AutoScroll = true;
            this.flpNG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.flpNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpNG.Controls.Add(this.checkBox1);
            this.flpNG.Controls.Add(this.checkBox4);
            this.flpNG.Controls.Add(this.checkBox2);
            this.flpNG.Controls.Add(this.checkBox3);
            this.flpNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpNG.Location = new System.Drawing.Point(0, 37);
            this.flpNG.Name = "flpNG";
            this.flpNG.Size = new System.Drawing.Size(440, 100);
            this.flpNG.TabIndex = 121;
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Red;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.checkBox1.Location = new System.Drawing.Point(3, 10);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(232, 55);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "LCD显示不良";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // checkBox4
            // 
            this.checkBox4.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.checkBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.checkBox4.Location = new System.Drawing.Point(3, 85);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(232, 55);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "LCD显示不良";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.checkBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.checkBox2.Location = new System.Drawing.Point(3, 160);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(232, 55);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "LCD显示不良";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.checkBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.checkBox3.Location = new System.Drawing.Point(3, 235);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(232, 55);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "LCD显示不良";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.DarkRed;
            this.label16.Location = new System.Drawing.Point(0, 137);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(440, 80);
            this.label16.TabIndex = 123;
            this.label16.Text = "不 良 申 报 ！";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 217);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(440, 42);
            this.tableLayoutPanel2.TabIndex = 122;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(343, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 36);
            this.btnCancel.TabIndex = 76;
            this.btnCancel.Tag = "";
            this.btnCancel.Text = "取 消";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnOK.FlatAppearance.BorderSize = 2;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.Green;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(193, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 36);
            this.btnOK.TabIndex = 75;
            this.btnOK.Tag = "";
            this.btnOK.Text = "提 交";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtDescribe, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(440, 37);
            this.tableLayoutPanel3.TabIndex = 120;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label9.Location = new System.Drawing.Point(7, 3);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 31);
            this.label9.TabIndex = 115;
            this.label9.Text = "异常描述:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescribe
            // 
            this.txtDescribe.BackColor = System.Drawing.Color.Lavender;
            this.txtDescribe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescribe.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescribe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtDescribe.Location = new System.Drawing.Point(93, 3);
            this.txtDescribe.Name = "txtDescribe";
            this.txtDescribe.ReadOnly = true;
            this.txtDescribe.Size = new System.Drawing.Size(344, 31);
            this.txtDescribe.TabIndex = 8;
            this.txtDescribe.Text = "不良描述字符串";
            // 
            // bgwWriteData
            // 
            this.bgwWriteData.WorkerReportsProgress = true;
            this.bgwWriteData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWriteData_DoWork);
            this.bgwWriteData.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwWriteData_ProgressChanged);
            this.bgwWriteData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWriteData_RunWorkerCompleted);
            // 
            // frmManualScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(794, 531);
            this.Controls.Add(this.pnlNG);
            this.Controls.Add(this.pnlNote);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.tlpCode);
            this.Controls.Add(this.pnlSplit1);
            this.Controls.Add(this.tlpInfo);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmManualScan";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人工扫描站点（正在初始化...）";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManualScan_FormClosed);
            this.Load += new System.EventHandler(this.frmManualScan_Load);
            this.Shown += new System.EventHandler(this.frmManualScan_Shown);
            this.SizeChanged += new System.EventHandler(this.frmManualScan_SizeChanged);
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
            this.tlpCode.ResumeLayout(false);
            this.tlpCode.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlNote.ResumeLayout(false);
            this.pnlTest.ResumeLayout(false);
            this.pnlTest.PerformLayout();
            this.pnlNG.ResumeLayout(false);
            this.pnlNG.PerformLayout();
            this.flpNG.ResumeLayout(false);
            this.flpNG.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtSPLName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ComboBox comProcess;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtOpCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtEarlyProcess;
        private System.Windows.Forms.TextBox txtProcessIP;
        private System.Windows.Forms.TableLayoutPanel tlpCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCode2;
        private System.Windows.Forms.TextBox txtScanCode;
        private System.Windows.Forms.Label lblCode2;
        private System.Windows.Forms.TextBox txtCode1;
        private System.Windows.Forms.Label lblCode1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblRule2;
        private System.Windows.Forms.Label lblRule1;
        private System.Windows.Forms.TextBox txtglassCode;
        private System.Windows.Forms.Panel pnlNote;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlSplit1;
        private Utils.ListViewNote lvwNote;
        private System.Windows.Forms.TextBox txtProcessCode;
        private System.Windows.Forms.TextBox txtProcessNumber;
        private System.Windows.Forms.TextBox txtRule1;
        private System.Windows.Forms.TextBox txtRule2;
        private System.Windows.Forms.TextBox txtDigit2;
        private System.Windows.Forms.TextBox txtDigit1;
        private System.Windows.Forms.Button btnNG;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlTest;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtErrorCount;
        private System.Windows.Forms.TextBox txtNGCount;
        private System.Windows.Forms.TextBox txtOKCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLCDCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.FlowLayoutPanel flpScanNote;
        private System.Windows.Forms.Panel pnlNG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDescribe;
        private System.Windows.Forms.FlowLayoutPanel flpNG;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.ComponentModel.BackgroundWorker bgwWriteData;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblMouldCode;
        private System.Windows.Forms.TextBox txtMouldCode;
    }
}