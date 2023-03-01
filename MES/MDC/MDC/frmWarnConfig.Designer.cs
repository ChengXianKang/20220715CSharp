namespace MDC
{
    partial class frmWarnConfig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWarnConfig));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeviceIP = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbLine = new System.Windows.Forms.ComboBox();
            this.pnlSplit1 = new System.Windows.Forms.Panel();
            this.dgwDevice = new Utils.Common.DataGridViewPlus(this.components);
            this.Tid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RwWarn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.JudgeWarn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PointWarn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ModelChangeEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Interval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDevice)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnRefresh, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDeviceIP, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbLine, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 33);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnRefresh.Location = new System.Drawing.Point(283, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(74, 27);
            this.btnRefresh.TabIndex = 118;
            this.btnRefresh.Text = "刷 新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 117;
            this.label1.Text = "生产产线:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDeviceIP
            // 
            this.txtDeviceIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDeviceIP.BackColor = System.Drawing.Color.White;
            this.txtDeviceIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeviceIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeviceIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtDeviceIP.Location = new System.Drawing.Point(443, 3);
            this.txtDeviceIP.Name = "txtDeviceIP";
            this.txtDeviceIP.Size = new System.Drawing.Size(194, 26);
            this.txtDeviceIP.TabIndex = 4;
            this.txtDeviceIP.Visible = false;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label12.Location = new System.Drawing.Point(364, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 20);
            this.label12.TabIndex = 116;
            this.label12.Text = "机台IP:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnSearch.Location = new System.Drawing.Point(643, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 27);
            this.btnSearch.TabIndex = 117;
            this.btnSearch.Text = "查 找";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            // 
            // cmbLine
            // 
            this.cmbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLine.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLine.FormattingEnabled = true;
            this.cmbLine.Location = new System.Drawing.Point(83, 3);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(194, 27);
            this.cmbLine.TabIndex = 119;
            this.cmbLine.SelectedIndexChanged += new System.EventHandler(this.cmbLine_SelectedIndexChanged);
            // 
            // pnlSplit1
            // 
            this.pnlSplit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit1.Location = new System.Drawing.Point(5, 38);
            this.pnlSplit1.Name = "pnlSplit1";
            this.pnlSplit1.Size = new System.Drawing.Size(784, 6);
            this.pnlSplit1.TabIndex = 109;
            // 
            // dgwDevice
            // 
            this.dgwDevice.AllowUserToAddRows = false;
            this.dgwDevice.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgwDevice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwDevice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwDevice.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwDevice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgwDevice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwDevice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tid,
            this.LineName,
            this.ProcessName,
            this.DeviceCode,
            this.DeviceIP,
            this.DeviceName,
            this.RwWarn,
            this.JudgeWarn,
            this.PointWarn,
            this.ModelChangeEnabled,
            this.Interval,
            this.Version});
            this.dgwDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwDevice.Location = new System.Drawing.Point(5, 44);
            this.dgwDevice.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgwDevice.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgwDevice.MergeColumnNames")));
            this.dgwDevice.MultiSelect = false;
            this.dgwDevice.Name = "dgwDevice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwDevice.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgwDevice.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgwDevice.RowTemplate.Height = 23;
            this.dgwDevice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwDevice.ShowLineNumber = true;
            this.dgwDevice.Size = new System.Drawing.Size(784, 482);
            this.dgwDevice.SortByColumnHeaderClick = false;
            this.dgwDevice.TabIndex = 110;
            this.dgwDevice.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwDevice_CellEndEdit);
            this.dgwDevice.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwDevice_CellValueChanged);
            this.dgwDevice.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgwDevice_CurrentCellDirtyStateChanged);
            // 
            // Tid
            // 
            this.Tid.DataPropertyName = "Tid";
            this.Tid.HeaderText = "ID";
            this.Tid.Name = "Tid";
            this.Tid.ReadOnly = true;
            this.Tid.Visible = false;
            // 
            // LineName
            // 
            this.LineName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LineName.DataPropertyName = "SPL_Name";
            this.LineName.HeaderText = "产线名称";
            this.LineName.Name = "LineName";
            this.LineName.ReadOnly = true;
            // 
            // ProcessName
            // 
            this.ProcessName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProcessName.DataPropertyName = "SGX_Name";
            this.ProcessName.HeaderText = "工序名称";
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.ReadOnly = true;
            // 
            // DeviceCode
            // 
            this.DeviceCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeviceCode.DataPropertyName = "STW_JobCode";
            this.DeviceCode.HeaderText = "机台编码";
            this.DeviceCode.Name = "DeviceCode";
            this.DeviceCode.ReadOnly = true;
            // 
            // DeviceIP
            // 
            this.DeviceIP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeviceIP.DataPropertyName = "STW_CardIP";
            this.DeviceIP.HeaderText = "机台IP";
            this.DeviceIP.Name = "DeviceIP";
            this.DeviceIP.ReadOnly = true;
            // 
            // DeviceName
            // 
            this.DeviceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeviceName.DataPropertyName = "STW_CardName";
            this.DeviceName.HeaderText = "机台名称";
            this.DeviceName.Name = "DeviceName";
            this.DeviceName.ReadOnly = true;
            // 
            // RwWarn
            // 
            this.RwWarn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RwWarn.DataPropertyName = "RwWarn";
            this.RwWarn.FalseValue = "0";
            this.RwWarn.HeaderText = "重工良品提醒";
            this.RwWarn.Name = "RwWarn";
            this.RwWarn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.RwWarn.TrueValue = "1";
            this.RwWarn.Width = 90;
            // 
            // JudgeWarn
            // 
            this.JudgeWarn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.JudgeWarn.DataPropertyName = "JudgeWarn";
            this.JudgeWarn.FalseValue = "0";
            this.JudgeWarn.HeaderText = "复判良品提醒";
            this.JudgeWarn.Name = "JudgeWarn";
            this.JudgeWarn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.JudgeWarn.TrueValue = "1";
            this.JudgeWarn.Width = 90;
            // 
            // PointWarn
            // 
            this.PointWarn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PointWarn.DataPropertyName = "PointWarn";
            this.PointWarn.FalseValue = "0";
            this.PointWarn.HeaderText = "点线不良提醒";
            this.PointWarn.Name = "PointWarn";
            this.PointWarn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PointWarn.TrueValue = "1";
            this.PointWarn.Width = 90;
            // 
            // ModelChangeEnabled
            // 
            this.ModelChangeEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ModelChangeEnabled.DataPropertyName = "ModelChangeEnabled";
            this.ModelChangeEnabled.FalseValue = "0";
            this.ModelChangeEnabled.HeaderText = "允许切换型号";
            this.ModelChangeEnabled.Name = "ModelChangeEnabled";
            this.ModelChangeEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ModelChangeEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ModelChangeEnabled.TrueValue = "1";
            this.ModelChangeEnabled.Width = 90;
            // 
            // Interval
            // 
            this.Interval.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Interval.DataPropertyName = "STW_Interval";
            this.Interval.HeaderText = "工序间隔时间(分钟)";
            this.Interval.Name = "Interval";
            // 
            // Version
            // 
            this.Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Version.DataPropertyName = "STW_Name";
            this.Version.HeaderText = "软件版本";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // frmWarnConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(794, 531);
            this.Controls.Add(this.dgwDevice);
            this.Controls.Add(this.pnlSplit1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmWarnConfig";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机台分拣提醒配置";
            this.Load += new System.EventHandler(this.frmWarnConfig_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDevice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtDeviceIP;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel pnlSplit1;
        private Utils.Common.DataGridViewPlus dgwDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tid;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RwWarn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn JudgeWarn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PointWarn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ModelChangeEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interval;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
    }
}