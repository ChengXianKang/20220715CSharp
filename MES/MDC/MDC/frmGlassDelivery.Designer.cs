namespace MDC
{
    partial class frmGlassDelivery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGlassDelivery));
            this.tlpOrder = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnNextOrder = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.lblProductionOrder = new System.Windows.Forms.Label();
            this.lblProductModel = new System.Windows.Forms.Label();
            this.lblFeedNumber = new System.Windows.Forms.Label();
            this.lblInputNumber = new System.Windows.Forms.Label();
            this.txtProductionOrder = new System.Windows.Forms.TextBox();
            this.txtProductModel = new System.Windows.Forms.TextBox();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.txtFeedNumber = new System.Windows.Forms.TextBox();
            this.txtInputNumber = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbNextOrder = new System.Windows.Forms.ComboBox();
            this.btnSwitchOrder = new System.Windows.Forms.Button();
            this.txtOp = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtLineName = new System.Windows.Forms.TextBox();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tlpCode = new System.Windows.Forms.TableLayoutPanel();
            this.txtScanLotCount = new System.Windows.Forms.TextBox();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtScanCode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlNote = new System.Windows.Forms.Panel();
            this.lvwNote = new Utils.ListViewNote(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlSplit1 = new System.Windows.Forms.Panel();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblShopName = new System.Windows.Forms.Label();
            this.cmbShopName = new System.Windows.Forms.ComboBox();
            this.cmbLineName = new System.Windows.Forms.ComboBox();
            this.lblLineName = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpOrder.SuspendLayout();
            this.tlpCode.SuspendLayout();
            this.pnlNote.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpOrder
            // 
            this.tlpOrder.AutoSize = true;
            this.tlpOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpOrder.ColumnCount = 6;
            this.tlpOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpOrder.Controls.Add(this.label6, 3, 2);
            this.tlpOrder.Controls.Add(this.btnNextOrder, 2, 2);
            this.tlpOrder.Controls.Add(this.label5, 0, 2);
            this.tlpOrder.Controls.Add(this.lblOrderNumber, 0, 1);
            this.tlpOrder.Controls.Add(this.lblProductionOrder, 0, 0);
            this.tlpOrder.Controls.Add(this.lblProductModel, 2, 0);
            this.tlpOrder.Controls.Add(this.lblFeedNumber, 2, 1);
            this.tlpOrder.Controls.Add(this.lblInputNumber, 4, 1);
            this.tlpOrder.Controls.Add(this.txtProductionOrder, 1, 0);
            this.tlpOrder.Controls.Add(this.txtProductModel, 3, 0);
            this.tlpOrder.Controls.Add(this.txtOrderNumber, 1, 1);
            this.tlpOrder.Controls.Add(this.txtFeedNumber, 3, 1);
            this.tlpOrder.Controls.Add(this.txtInputNumber, 5, 1);
            this.tlpOrder.Controls.Add(this.btnRefresh, 4, 0);
            this.tlpOrder.Controls.Add(this.cmbNextOrder, 1, 2);
            this.tlpOrder.Controls.Add(this.btnSwitchOrder, 4, 2);
            this.tlpOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpOrder.Location = new System.Drawing.Point(8, 82);
            this.tlpOrder.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpOrder.Name = "tlpOrder";
            this.tlpOrder.RowCount = 3;
            this.tlpOrder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpOrder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpOrder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpOrder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpOrder.Size = new System.Drawing.Size(778, 102);
            this.tlpOrder.TabIndex = 100;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label6.Location = new System.Drawing.Point(395, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(183, 20);
            this.label6.TabIndex = 136;
            this.label6.Text = "(待产工单提交后不可更改)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnNextOrder
            // 
            this.btnNextOrder.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNextOrder.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNextOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnNextOrder.Location = new System.Drawing.Point(294, 69);
            this.btnNextOrder.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnNextOrder.Name = "btnNextOrder";
            this.btnNextOrder.Size = new System.Drawing.Size(94, 31);
            this.btnNextOrder.TabIndex = 133;
            this.btnNextOrder.Text = "提  交";
            this.btnNextOrder.UseVisualStyleBackColor = true;
            this.btnNextOrder.Click += new System.EventHandler(this.btnNextOrder_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(14, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.TabIndex = 133;
            this.label5.Text = "待产工单:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOrderNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblOrderNumber.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOrderNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblOrderNumber.Location = new System.Drawing.Point(14, 41);
            this.lblOrderNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(82, 20);
            this.lblOrderNumber.TabIndex = 124;
            this.lblOrderNumber.Text = "工单数量:";
            this.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProductionOrder
            // 
            this.lblProductionOrder.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProductionOrder.BackColor = System.Drawing.Color.Transparent;
            this.lblProductionOrder.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductionOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblProductionOrder.Location = new System.Drawing.Point(14, 7);
            this.lblProductionOrder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductionOrder.Name = "lblProductionOrder";
            this.lblProductionOrder.Size = new System.Drawing.Size(82, 20);
            this.lblProductionOrder.TabIndex = 122;
            this.lblProductionOrder.Text = "生产工单:";
            this.lblProductionOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProductModel
            // 
            this.lblProductModel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProductModel.BackColor = System.Drawing.Color.Transparent;
            this.lblProductModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblProductModel.Location = new System.Drawing.Point(305, 7);
            this.lblProductModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductModel.Name = "lblProductModel";
            this.lblProductModel.Size = new System.Drawing.Size(82, 20);
            this.lblProductModel.TabIndex = 123;
            this.lblProductModel.Text = "成品型号:";
            this.lblProductModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFeedNumber
            // 
            this.lblFeedNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFeedNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblFeedNumber.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFeedNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblFeedNumber.Location = new System.Drawing.Point(305, 41);
            this.lblFeedNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFeedNumber.Name = "lblFeedNumber";
            this.lblFeedNumber.Size = new System.Drawing.Size(82, 20);
            this.lblFeedNumber.TabIndex = 125;
            this.lblFeedNumber.Text = "Cell投料数:";
            this.lblFeedNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInputNumber
            // 
            this.lblInputNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInputNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblInputNumber.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInputNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblInputNumber.Location = new System.Drawing.Point(591, 41);
            this.lblInputNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInputNumber.Name = "lblInputNumber";
            this.lblInputNumber.Size = new System.Drawing.Size(87, 20);
            this.lblInputNumber.TabIndex = 126;
            this.lblInputNumber.Text = "清洗投入数:";
            this.lblInputNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductionOrder
            // 
            this.txtProductionOrder.BackColor = System.Drawing.Color.Lavender;
            this.txtProductionOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductionOrder.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProductionOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProductionOrder.Location = new System.Drawing.Point(103, 3);
            this.txtProductionOrder.Name = "txtProductionOrder";
            this.txtProductionOrder.ReadOnly = true;
            this.txtProductionOrder.Size = new System.Drawing.Size(185, 26);
            this.txtProductionOrder.TabIndex = 127;
            this.txtProductionOrder.TabStop = false;
            this.txtProductionOrder.Text = "工单编码";
            // 
            // txtProductModel
            // 
            this.txtProductModel.BackColor = System.Drawing.Color.Lavender;
            this.txtProductModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProductModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProductModel.Location = new System.Drawing.Point(394, 3);
            this.txtProductModel.Name = "txtProductModel";
            this.txtProductModel.ReadOnly = true;
            this.txtProductModel.Size = new System.Drawing.Size(185, 26);
            this.txtProductModel.TabIndex = 128;
            this.txtProductModel.TabStop = false;
            this.txtProductModel.Text = "成品型号";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.BackColor = System.Drawing.Color.Lavender;
            this.txtOrderNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrderNumber.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOrderNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtOrderNumber.Location = new System.Drawing.Point(103, 37);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.ReadOnly = true;
            this.txtOrderNumber.Size = new System.Drawing.Size(185, 26);
            this.txtOrderNumber.TabIndex = 129;
            this.txtOrderNumber.TabStop = false;
            // 
            // txtFeedNumber
            // 
            this.txtFeedNumber.BackColor = System.Drawing.Color.Lavender;
            this.txtFeedNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFeedNumber.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFeedNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtFeedNumber.Location = new System.Drawing.Point(394, 37);
            this.txtFeedNumber.Name = "txtFeedNumber";
            this.txtFeedNumber.ReadOnly = true;
            this.txtFeedNumber.Size = new System.Drawing.Size(185, 26);
            this.txtFeedNumber.TabIndex = 130;
            this.txtFeedNumber.TabStop = false;
            // 
            // txtInputNumber
            // 
            this.txtInputNumber.BackColor = System.Drawing.Color.Lavender;
            this.txtInputNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInputNumber.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInputNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtInputNumber.Location = new System.Drawing.Point(685, 37);
            this.txtInputNumber.Name = "txtInputNumber";
            this.txtInputNumber.ReadOnly = true;
            this.txtInputNumber.Size = new System.Drawing.Size(90, 26);
            this.txtInputNumber.TabIndex = 131;
            this.txtInputNumber.TabStop = false;
            // 
            // btnRefresh
            // 
            this.tlpOrder.SetColumnSpan(this.btnRefresh, 2);
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnRefresh.Location = new System.Drawing.Point(585, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(190, 31);
            this.btnRefresh.TabIndex = 132;
            this.btnRefresh.Text = "刷      新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbNextOrder
            // 
            this.cmbNextOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNextOrder.FormattingEnabled = true;
            this.cmbNextOrder.Location = new System.Drawing.Point(103, 71);
            this.cmbNextOrder.Name = "cmbNextOrder";
            this.cmbNextOrder.Size = new System.Drawing.Size(185, 28);
            this.cmbNextOrder.TabIndex = 134;
            // 
            // btnSwitchOrder
            // 
            this.tlpOrder.SetColumnSpan(this.btnSwitchOrder, 2);
            this.btnSwitchOrder.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSwitchOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnSwitchOrder.Location = new System.Drawing.Point(585, 68);
            this.btnSwitchOrder.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSwitchOrder.Name = "btnSwitchOrder";
            this.btnSwitchOrder.Size = new System.Drawing.Size(190, 34);
            this.btnSwitchOrder.TabIndex = 135;
            this.btnSwitchOrder.Text = "切换工单";
            this.btnSwitchOrder.UseVisualStyleBackColor = true;
            this.btnSwitchOrder.Click += new System.EventHandler(this.btnSwitchOrder_Click);
            // 
            // txtOp
            // 
            this.txtOp.BackColor = System.Drawing.Color.Lavender;
            this.txtOp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOp.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtOp.Location = new System.Drawing.Point(492, 37);
            this.txtOp.Name = "txtOp";
            this.txtOp.ReadOnly = true;
            this.txtOp.Size = new System.Drawing.Size(283, 26);
            this.txtOp.TabIndex = 15;
            this.txtOp.TabStop = false;
            this.txtOp.Text = "扫描人员工号";
            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.Color.Lavender;
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtIP.Location = new System.Drawing.Point(103, 37);
            this.txtIP.Name = "txtIP";
            this.txtIP.ReadOnly = true;
            this.txtIP.Size = new System.Drawing.Size(283, 26);
            this.txtIP.TabIndex = 14;
            this.txtIP.TabStop = false;
            this.txtIP.Text = "当前站点IP";
            // 
            // txtLineName
            // 
            this.txtLineName.BackColor = System.Drawing.Color.Lavender;
            this.txtLineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLineName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLineName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtLineName.Location = new System.Drawing.Point(79, 76);
            this.txtLineName.Name = "txtLineName";
            this.txtLineName.ReadOnly = true;
            this.txtLineName.Size = new System.Drawing.Size(185, 26);
            this.txtLineName.TabIndex = 10;
            this.txtLineName.TabStop = false;
            this.txtLineName.Text = "产线名称";
            // 
            // txtOrder
            // 
            this.txtOrder.BackColor = System.Drawing.Color.Lavender;
            this.txtOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrder.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtOrder.Location = new System.Drawing.Point(394, 67);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.ReadOnly = true;
            this.txtOrder.Size = new System.Drawing.Size(185, 26);
            this.txtOrder.TabIndex = 124;
            this.txtOrder.TabStop = false;
            this.txtOrder.Tag = "EC888";
            // 
            // txtModel
            // 
            this.txtModel.BackColor = System.Drawing.Color.Lavender;
            this.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtModel.Location = new System.Drawing.Point(394, 35);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(185, 26);
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
            this.label2.Location = new System.Drawing.Point(608, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 122;
            this.label2.Text = "线       别:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label14.Location = new System.Drawing.Point(305, 70);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 20);
            this.label14.TabIndex = 119;
            this.label14.Text = "工       单:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tlpCode
            // 
            this.tlpCode.AutoSize = true;
            this.tlpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpCode.ColumnCount = 6;
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpCode.Controls.Add(this.txtScanLotCount, 5, 0);
            this.tlpCode.Controls.Add(this.txtLine, 5, 2);
            this.tlpCode.Controls.Add(this.txtCount, 5, 1);
            this.tlpCode.Controls.Add(this.txtProduct, 1, 2);
            this.tlpCode.Controls.Add(this.txtSN, 1, 1);
            this.tlpCode.Controls.Add(this.txtModel, 3, 1);
            this.tlpCode.Controls.Add(this.label4, 4, 1);
            this.tlpCode.Controls.Add(this.txtOrder, 3, 2);
            this.tlpCode.Controls.Add(this.label8, 0, 0);
            this.tlpCode.Controls.Add(this.label3, 0, 2);
            this.tlpCode.Controls.Add(this.label9, 0, 1);
            this.tlpCode.Controls.Add(this.label14, 2, 2);
            this.tlpCode.Controls.Add(this.txtScanCode, 1, 0);
            this.tlpCode.Controls.Add(this.label2, 4, 2);
            this.tlpCode.Controls.Add(this.label15, 2, 1);
            this.tlpCode.Controls.Add(this.label1, 4, 0);
            this.tlpCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCode.Location = new System.Drawing.Point(8, 190);
            this.tlpCode.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpCode.Name = "tlpCode";
            this.tlpCode.RowCount = 4;
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCode.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCode.Size = new System.Drawing.Size(778, 96);
            this.tlpCode.TabIndex = 100;
            // 
            // txtScanLotCount
            // 
            this.txtScanLotCount.BackColor = System.Drawing.Color.Lavender;
            this.txtScanLotCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScanLotCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanLotCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtScanLotCount.Location = new System.Drawing.Point(685, 3);
            this.txtScanLotCount.Name = "txtScanLotCount";
            this.txtScanLotCount.ReadOnly = true;
            this.txtScanLotCount.Size = new System.Drawing.Size(90, 26);
            this.txtScanLotCount.TabIndex = 127;
            this.txtScanLotCount.TabStop = false;
            this.txtScanLotCount.Tag = "EC888";
            // 
            // txtLine
            // 
            this.txtLine.BackColor = System.Drawing.Color.Lavender;
            this.txtLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLine.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtLine.Location = new System.Drawing.Point(685, 67);
            this.txtLine.Name = "txtLine";
            this.txtLine.ReadOnly = true;
            this.txtLine.Size = new System.Drawing.Size(90, 26);
            this.txtLine.TabIndex = 125;
            this.txtLine.TabStop = false;
            this.txtLine.Tag = "EC888";
            // 
            // txtCount
            // 
            this.txtCount.BackColor = System.Drawing.Color.Lavender;
            this.txtCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtCount.Location = new System.Drawing.Point(685, 35);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(90, 26);
            this.txtCount.TabIndex = 126;
            this.txtCount.TabStop = false;
            this.txtCount.Tag = "EC888";
            // 
            // txtProduct
            // 
            this.txtProduct.BackColor = System.Drawing.Color.Lavender;
            this.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProduct.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProduct.Location = new System.Drawing.Point(103, 67);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.ReadOnly = true;
            this.txtProduct.Size = new System.Drawing.Size(185, 26);
            this.txtProduct.TabIndex = 127;
            this.txtProduct.TabStop = false;
            this.txtProduct.Tag = "EC888";
            // 
            // txtSN
            // 
            this.txtSN.BackColor = System.Drawing.Color.Lavender;
            this.txtSN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtSN.Location = new System.Drawing.Point(103, 35);
            this.txtSN.Name = "txtSN";
            this.txtSN.ReadOnly = true;
            this.txtSN.Size = new System.Drawing.Size(185, 26);
            this.txtSN.TabIndex = 128;
            this.txtSN.TabStop = false;
            this.txtSN.Tag = "EC888";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(596, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 126;
            this.label4.Text = "数       量:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label8.Location = new System.Drawing.Point(14, 6);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 20);
            this.label8.TabIndex = 109;
            this.label8.Text = "实时扫描:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label3.Location = new System.Drawing.Point(14, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 125;
            this.label3.Text = "产品编码:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label9.Location = new System.Drawing.Point(14, 38);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 20);
            this.label9.TabIndex = 111;
            this.label9.Text = "序  列  号:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtScanCode
            // 
            this.txtScanCode.BackColor = System.Drawing.Color.White;
            this.txtScanCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpCode.SetColumnSpan(this.txtScanCode, 3);
            this.txtScanCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtScanCode.Location = new System.Drawing.Point(103, 3);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.Size = new System.Drawing.Size(476, 26);
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
            this.label15.Location = new System.Drawing.Point(305, 38);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 20);
            this.label15.TabIndex = 119;
            this.label15.Text = "规格型号:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(591, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 129;
            this.label1.Text = "已扫批数:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlNote
            // 
            this.pnlNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.pnlNote.Controls.Add(this.txtLineName);
            this.pnlNote.Controls.Add(this.lvwNote);
            this.pnlNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNote.Location = new System.Drawing.Point(8, 292);
            this.pnlNote.Name = "pnlNote";
            this.pnlNote.Padding = new System.Windows.Forms.Padding(3);
            this.pnlNote.Size = new System.Drawing.Size(778, 231);
            this.pnlNote.TabIndex = 105;
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
            this.lvwNote.Size = new System.Drawing.Size(772, 225);
            this.lvwNote.TabIndex = 9;
            this.lvwNote.UseCompatibleStateImageBehavior = false;
            this.lvwNote.View = System.Windows.Forms.View.Details;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(8, 286);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(778, 6);
            this.panel4.TabIndex = 106;
            // 
            // pnlSplit1
            // 
            this.pnlSplit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit1.Location = new System.Drawing.Point(8, 184);
            this.pnlSplit1.Name = "pnlSplit1";
            this.pnlSplit1.Size = new System.Drawing.Size(778, 6);
            this.pnlSplit1.TabIndex = 108;
            // 
            // tlpInfo
            // 
            this.tlpInfo.AutoSize = true;
            this.tlpInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpInfo.ColumnCount = 4;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.Controls.Add(this.lblShopName, 0, 0);
            this.tlpInfo.Controls.Add(this.cmbShopName, 1, 0);
            this.tlpInfo.Controls.Add(this.cmbLineName, 3, 0);
            this.tlpInfo.Controls.Add(this.lblLineName, 2, 0);
            this.tlpInfo.Controls.Add(this.label16, 0, 1);
            this.tlpInfo.Controls.Add(this.txtIP, 1, 1);
            this.tlpInfo.Controls.Add(this.label17, 2, 1);
            this.tlpInfo.Controls.Add(this.txtOp, 3, 1);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpInfo.Location = new System.Drawing.Point(8, 8);
            this.tlpInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 2;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInfo.Size = new System.Drawing.Size(778, 68);
            this.tlpInfo.TabIndex = 109;
            // 
            // lblShopName
            // 
            this.lblShopName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblShopName.BackColor = System.Drawing.Color.Transparent;
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShopName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblShopName.Location = new System.Drawing.Point(14, 7);
            this.lblShopName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(82, 20);
            this.lblShopName.TabIndex = 115;
            this.lblShopName.Text = "选择车间:";
            this.lblShopName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbShopName
            // 
            this.cmbShopName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShopName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.cmbShopName.FormattingEnabled = true;
            this.cmbShopName.Location = new System.Drawing.Point(103, 3);
            this.cmbShopName.Name = "cmbShopName";
            this.cmbShopName.Size = new System.Drawing.Size(283, 28);
            this.cmbShopName.TabIndex = 122;
            this.cmbShopName.SelectionChangeCommitted += new System.EventHandler(this.cmbShopName_SelectionChangeCommitted);
            // 
            // cmbLineName
            // 
            this.cmbLineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.cmbLineName.FormattingEnabled = true;
            this.cmbLineName.Location = new System.Drawing.Point(492, 3);
            this.cmbLineName.Name = "cmbLineName";
            this.cmbLineName.Size = new System.Drawing.Size(283, 28);
            this.cmbLineName.TabIndex = 123;
            // 
            // lblLineName
            // 
            this.lblLineName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLineName.BackColor = System.Drawing.Color.Transparent;
            this.lblLineName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLineName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblLineName.Location = new System.Drawing.Point(403, 7);
            this.lblLineName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(82, 20);
            this.lblLineName.TabIndex = 116;
            this.lblLineName.Text = "选择产线:";
            this.lblLineName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label16.Location = new System.Drawing.Point(26, 41);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 20);
            this.label16.TabIndex = 121;
            this.label16.Text = "设  备 IP:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label17.Location = new System.Drawing.Point(415, 41);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 20);
            this.label17.TabIndex = 116;
            this.label17.Text = "员工账号:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(8, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 6);
            this.panel1.TabIndex = 110;
            // 
            // frmGlassDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(794, 531);
            this.Controls.Add(this.pnlNote);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.tlpCode);
            this.Controls.Add(this.pnlSplit1);
            this.Controls.Add(this.tlpOrder);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tlpInfo);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmGlassDelivery";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产线投料（正在初始化...）";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmScan_FormClosed);
            this.Load += new System.EventHandler(this.frmScan_Load);
            this.SizeChanged += new System.EventHandler(this.frmScan_SizeChanged);
            this.tlpOrder.ResumeLayout(false);
            this.tlpOrder.PerformLayout();
            this.tlpCode.ResumeLayout(false);
            this.tlpCode.PerformLayout();
            this.pnlNote.ResumeLayout(false);
            this.pnlNote.PerformLayout();
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpOrder;
        private System.Windows.Forms.TextBox txtLineName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtOp;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TableLayoutPanel tlpCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtScanCode;
        private System.Windows.Forms.Panel pnlNote;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlSplit1;
        private Utils.ListViewNote lvwNote;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Label lblProductionOrder;
        private System.Windows.Forms.Label lblProductModel;
        private System.Windows.Forms.Label lblFeedNumber;
        private System.Windows.Forms.Label lblInputNumber;
        private System.Windows.Forms.TextBox txtProductionOrder;
        private System.Windows.Forms.TextBox txtProductModel;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.TextBox txtFeedNumber;
        private System.Windows.Forms.TextBox txtInputNumber;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnNextOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbNextOrder;
        private System.Windows.Forms.Button btnSwitchOrder;
        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtScanLotCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbShopName;
        private System.Windows.Forms.ComboBox cmbLineName;
        private System.Windows.Forms.Label lblLineName;

    }
}

