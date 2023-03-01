namespace MDC
{
    partial class frmDataPush
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataPush));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbProcess = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnManualSend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txtTime = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.btnAutoSend = new System.Windows.Forms.ToolStripButton();
            this.btnGetAirData = new System.Windows.Forms.ToolStripButton();
            this.btnGetGlue = new System.Windows.Forms.ToolStripButton();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.CustomerNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QcTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestSubItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlUpperLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlLowerLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestCharResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsVerification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RawDataID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bgwManualSend = new System.ComponentModel.BackgroundWorker();
            this.bgwAutoSend = new System.ComponentModel.BackgroundWorker();
            this.tmrAutoSend = new System.Windows.Forms.Timer(this.components);
            this.btnGetProcessFail = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConfig,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmbProcess,
            this.toolStripLabel2,
            this.txtCount,
            this.toolStripLabel3,
            this.toolStripSeparator2,
            this.btnManualSend,
            this.toolStripSeparator3,
            this.toolStripLabel4,
            this.txtTime,
            this.toolStripLabel5,
            this.btnAutoSend,
            this.btnGetAirData,
            this.btnGetGlue,
            this.btnGetProcessFail});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(794, 34);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnConfig
            // 
            this.btnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnConfig.Image")));
            this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(60, 31);
            this.btnConfig.Text = "修改配置";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 31);
            this.toolStripLabel1.Text = "选择工序";
            // 
            // cmbProcess
            // 
            this.cmbProcess.Items.AddRange(new object[] {
            "006-COG",
            "008-FOG",
            "009-FOG-AOI",
            "011-LCD前测不良",
            "024-背光组装",
            "031-复测AOI不良",
            "034-FV2不良",
            "080-点胶外观检测",
            "ALL-全部工序"});
            this.cmbProcess.Name = "cmbProcess";
            this.cmbProcess.Size = new System.Drawing.Size(80, 34);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(32, 31);
            this.toolStripLabel2.Text = "每次";
            // 
            // txtCount
            // 
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(30, 34);
            this.txtCount.Text = "50";
            this.txtCount.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(20, 31);
            this.toolStripLabel3.Text = "片";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // btnManualSend
            // 
            this.btnManualSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnManualSend.Image = ((System.Drawing.Image)(resources.GetObject("btnManualSend.Image")));
            this.btnManualSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnManualSend.Name = "btnManualSend";
            this.btnManualSend.Size = new System.Drawing.Size(60, 31);
            this.btnManualSend.Text = "单次发送";
            this.btnManualSend.Click += new System.EventHandler(this.btnManualSend_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(56, 31);
            this.toolStripLabel4.Text = "间隔时间";
            // 
            // txtTime
            // 
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(30, 34);
            this.txtTime.Text = "5";
            this.txtTime.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(20, 31);
            this.toolStripLabel5.Text = "秒";
            // 
            // btnAutoSend
            // 
            this.btnAutoSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAutoSend.Image = ((System.Drawing.Image)(resources.GetObject("btnAutoSend.Image")));
            this.btnAutoSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoSend.Name = "btnAutoSend";
            this.btnAutoSend.Size = new System.Drawing.Size(60, 31);
            this.btnAutoSend.Tag = "start";
            this.btnAutoSend.Text = "循环发送";
            this.btnAutoSend.Click += new System.EventHandler(this.btnAutoSend_Click);
            // 
            // btnGetAirData
            // 
            this.btnGetAirData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGetAirData.Image = ((System.Drawing.Image)(resources.GetObject("btnGetAirData.Image")));
            this.btnGetAirData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetAirData.Name = "btnGetAirData";
            this.btnGetAirData.Size = new System.Drawing.Size(120, 31);
            this.btnGetAirData.Text = "获取设备洁净度数据";
            this.btnGetAirData.Click += new System.EventHandler(this.btnGetAirData_Click);
            // 
            // btnGetGlue
            // 
            this.btnGetGlue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGetGlue.Image = ((System.Drawing.Image)(resources.GetObject("btnGetGlue.Image")));
            this.btnGetGlue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetGlue.Name = "btnGetGlue";
            this.btnGetGlue.Size = new System.Drawing.Size(84, 31);
            this.btnGetGlue.Text = "获取机台数据";
            this.btnGetGlue.Click += new System.EventHandler(this.btnGetGlue_Click);
            // 
            // grpHistory
            // 
            this.grpHistory.Controls.Add(this.dgvHistory);
            this.grpHistory.Controls.Add(this.panel1);
            this.grpHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistory.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpHistory.Location = new System.Drawing.Point(0, 0);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Padding = new System.Windows.Forms.Padding(5);
            this.grpHistory.Size = new System.Drawing.Size(794, 191);
            this.grpHistory.TabIndex = 11;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = "历史记录";
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomerNumber,
            this.SupplierName,
            this.FactoryName,
            this.CustomerModel,
            this.LotNumber,
            this.SerialNumber,
            this.ProductNumber,
            this.Line,
            this.QcTime,
            this.TestStation,
            this.TestItemName,
            this.TestSubItemName,
            this.ControlUpperLimit,
            this.ControlLowerLimit,
            this.TestUnit,
            this.TestResult,
            this.TestCharResult,
            this.IsVerification,
            this.RawDataID,
            this.Remark,
            this.UpdateTime,
            this.Guid});
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(5, 51);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowTemplate.Height = 23;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(784, 135);
            this.dgvHistory.TabIndex = 2;
            // 
            // CustomerNumber
            // 
            this.CustomerNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CustomerNumber.DataPropertyName = "HW_CustomerNumber";
            this.CustomerNumber.HeaderText = "华为编码";
            this.CustomerNumber.Name = "CustomerNumber";
            this.CustomerNumber.ReadOnly = true;
            this.CustomerNumber.Width = 81;
            // 
            // SupplierName
            // 
            this.SupplierName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SupplierName.DataPropertyName = "HW_SupplierName";
            this.SupplierName.HeaderText = "供应商";
            this.SupplierName.Name = "SupplierName";
            this.SupplierName.ReadOnly = true;
            this.SupplierName.Width = 69;
            // 
            // FactoryName
            // 
            this.FactoryName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactoryName.DataPropertyName = "HW_FactoryName";
            this.FactoryName.HeaderText = "厂别";
            this.FactoryName.Name = "FactoryName";
            this.FactoryName.ReadOnly = true;
            this.FactoryName.Width = 57;
            // 
            // CustomerModel
            // 
            this.CustomerModel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CustomerModel.DataPropertyName = "HW_CustomerModel";
            this.CustomerModel.HeaderText = "客户型号";
            this.CustomerModel.Name = "CustomerModel";
            this.CustomerModel.ReadOnly = true;
            this.CustomerModel.Width = 81;
            // 
            // LotNumber
            // 
            this.LotNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LotNumber.DataPropertyName = "HW_LotNumber";
            this.LotNumber.HeaderText = "Lot号";
            this.LotNumber.Name = "LotNumber";
            this.LotNumber.ReadOnly = true;
            this.LotNumber.Width = 120;
            // 
            // SerialNumber
            // 
            this.SerialNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SerialNumber.DataPropertyName = "HW_SerialNumber";
            this.SerialNumber.HeaderText = "序列号";
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.ReadOnly = true;
            this.SerialNumber.Width = 150;
            // 
            // ProductNumber
            // 
            this.ProductNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProductNumber.DataPropertyName = "HW_ProductNumber";
            this.ProductNumber.HeaderText = "供应商编码";
            this.ProductNumber.Name = "ProductNumber";
            this.ProductNumber.ReadOnly = true;
            this.ProductNumber.Width = 93;
            // 
            // Line
            // 
            this.Line.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Line.DataPropertyName = "HW_Line";
            this.Line.HeaderText = "生产线";
            this.Line.Name = "Line";
            this.Line.ReadOnly = true;
            this.Line.Width = 69;
            // 
            // QcTime
            // 
            this.QcTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.QcTime.DataPropertyName = "HW_QcTime";
            this.QcTime.HeaderText = "测试时间";
            this.QcTime.Name = "QcTime";
            this.QcTime.ReadOnly = true;
            this.QcTime.Width = 120;
            // 
            // TestStation
            // 
            this.TestStation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestStation.DataPropertyName = "HW_TestStation";
            this.TestStation.HeaderText = "测试工站";
            this.TestStation.Name = "TestStation";
            this.TestStation.ReadOnly = true;
            // 
            // TestItemName
            // 
            this.TestItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestItemName.DataPropertyName = "HW_TestItemName";
            this.TestItemName.HeaderText = "测试项目";
            this.TestItemName.Name = "TestItemName";
            this.TestItemName.ReadOnly = true;
            this.TestItemName.Width = 120;
            // 
            // TestSubItemName
            // 
            this.TestSubItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestSubItemName.DataPropertyName = "HW_TestSubItemName";
            this.TestSubItemName.HeaderText = "测试子项";
            this.TestSubItemName.Name = "TestSubItemName";
            this.TestSubItemName.ReadOnly = true;
            this.TestSubItemName.Width = 120;
            // 
            // ControlUpperLimit
            // 
            this.ControlUpperLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ControlUpperLimit.DataPropertyName = "HW_ControlUpperLimit";
            this.ControlUpperLimit.HeaderText = "控制上限";
            this.ControlUpperLimit.Name = "ControlUpperLimit";
            this.ControlUpperLimit.ReadOnly = true;
            this.ControlUpperLimit.Width = 81;
            // 
            // ControlLowerLimit
            // 
            this.ControlLowerLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ControlLowerLimit.DataPropertyName = "HW_ControlLowerLimit";
            this.ControlLowerLimit.HeaderText = "控制下限";
            this.ControlLowerLimit.Name = "ControlLowerLimit";
            this.ControlLowerLimit.ReadOnly = true;
            this.ControlLowerLimit.Width = 81;
            // 
            // TestUnit
            // 
            this.TestUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestUnit.DataPropertyName = "HW_TestUnit";
            this.TestUnit.HeaderText = "测试单位";
            this.TestUnit.Name = "TestUnit";
            this.TestUnit.ReadOnly = true;
            this.TestUnit.Width = 81;
            // 
            // TestResult
            // 
            this.TestResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestResult.DataPropertyName = "HW_TestResult";
            this.TestResult.HeaderText = "测试值";
            this.TestResult.Name = "TestResult";
            this.TestResult.ReadOnly = true;
            this.TestResult.Width = 69;
            // 
            // TestCharResult
            // 
            this.TestCharResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestCharResult.DataPropertyName = "HW_TestCharResult";
            this.TestCharResult.HeaderText = "测试结果";
            this.TestCharResult.Name = "TestCharResult";
            this.TestCharResult.ReadOnly = true;
            this.TestCharResult.Width = 81;
            // 
            // IsVerification
            // 
            this.IsVerification.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsVerification.DataPropertyName = "HW_IsVerification";
            this.IsVerification.HeaderText = "是否为验证模式";
            this.IsVerification.Name = "IsVerification";
            this.IsVerification.ReadOnly = true;
            this.IsVerification.Width = 120;
            // 
            // RawDataID
            // 
            this.RawDataID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RawDataID.DataPropertyName = "HW_RawDataID";
            this.RawDataID.HeaderText = "测试组ID";
            this.RawDataID.Name = "RawDataID";
            this.RawDataID.ReadOnly = true;
            this.RawDataID.Width = 300;
            // 
            // Remark
            // 
            this.Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Remark.DataPropertyName = "HW_Remark";
            this.Remark.HeaderText = "备注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            this.Remark.Width = 57;
            // 
            // UpdateTime
            // 
            this.UpdateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.UpdateTime.DataPropertyName = "HW_UpdateTime";
            this.UpdateTime.HeaderText = "上传时间";
            this.UpdateTime.Name = "UpdateTime";
            this.UpdateTime.ReadOnly = true;
            this.UpdateTime.Width = 120;
            // 
            // Guid
            // 
            this.Guid.DataPropertyName = "HW_Guid";
            this.Guid.HeaderText = "Guid";
            this.Guid.Name = "Guid";
            this.Guid.ReadOnly = true;
            this.Guid.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 30);
            this.panel1.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(139, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(130, 23);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // txtNote
            // 
            this.txtNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.txtNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNote.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtNote.Location = new System.Drawing.Point(5, 21);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNote.Size = new System.Drawing.Size(784, 276);
            this.txtNote.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(794, 302);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送日志";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpHistory);
            this.splitContainer1.Size = new System.Drawing.Size(794, 497);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 13;
            // 
            // bgwManualSend
            // 
            this.bgwManualSend.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwManualSend_DoWork);
            this.bgwManualSend.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwManualSend_RunWorkerCompleted);
            // 
            // bgwAutoSend
            // 
            this.bgwAutoSend.WorkerReportsProgress = true;
            this.bgwAutoSend.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAutoSend_DoWork);
            this.bgwAutoSend.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwAutoSend_ProgressChanged);
            this.bgwAutoSend.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAutoSend_RunWorkerCompleted);
            // 
            // tmrAutoSend
            // 
            this.tmrAutoSend.Tick += new System.EventHandler(this.tmrAutoSend_Tick);
            // 
            // btnGetProcessFail
            // 
            this.btnGetProcessFail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGetProcessFail.Image = ((System.Drawing.Image)(resources.GetObject("btnGetProcessFail.Image")));
            this.btnGetProcessFail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetProcessFail.Name = "btnGetProcessFail";
            this.btnGetProcessFail.Size = new System.Drawing.Size(84, 21);
            this.btnGetProcessFail.Text = "获取不良数据";
            this.btnGetProcessFail.Click += new System.EventHandler(this.btnGetProcessFail_Click);
            // 
            // frmDataPush
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(794, 531);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmDataPush";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "华为数据上传";
            this.Load += new System.EventHandler(this.frmDataPush_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grpHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnConfig;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbProcess;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txtCount;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnManualSend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox txtTime;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton btnAutoSend;
        private System.ComponentModel.BackgroundWorker bgwManualSend;
        private System.Windows.Forms.ToolStripButton btnGetAirData;
        private System.Windows.Forms.ToolStripButton btnGetGlue;
        private System.ComponentModel.BackgroundWorker bgwAutoSend;
        private System.Windows.Forms.Timer tmrAutoSend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn QcTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestSubItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlUpperLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlLowerLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestCharResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsVerification;
        private System.Windows.Forms.DataGridViewTextBoxColumn RawDataID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Guid;
        private System.Windows.Forms.ToolStripButton btnGetProcessFail;
    }
}