namespace MDC
{
    partial class frmMaterialConfig
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaterialConfig));
            this.dgwMaterial = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSpec2 = new System.Windows.Forms.TextBox();
            this.txtName2 = new System.Windows.Forms.TextBox();
            this.txtName1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCode1 = new System.Windows.Forms.TextBox();
            this.txtSpec1 = new System.Windows.Forms.TextBox();
            this.txtCode2 = new System.Windows.Forms.TextBox();
            this.lblTid1 = new System.Windows.Forms.Label();
            this.lblTid2 = new System.Windows.Forms.Label();
            this.btnSearch1 = new System.Windows.Forms.Button();
            this.btnSearch2 = new System.Windows.Forms.Button();
            this.tXD_Material_ConfigBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Tid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SM_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SM_Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMB_Name_Bonded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMB_Spec_Bonded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMTid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMTid_Bonded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMCode_Bonded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgwMaterial)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tXD_Material_ConfigBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwMaterial
            // 
            this.dgwMaterial.AllowUserToAddRows = false;
            this.dgwMaterial.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgwMaterial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwMaterial.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwMaterial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgwMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwMaterial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tid,
            this.SM_Name,
            this.SM_Spec,
            this.SMB_Name_Bonded,
            this.SMB_Spec_Bonded,
            this.SMTid,
            this.SMCode,
            this.SMTid_Bonded,
            this.SMCode_Bonded,
            this.Pid,
            this.AddDate});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwMaterial.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgwMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwMaterial.Location = new System.Drawing.Point(0, 212);
            this.dgwMaterial.Name = "dgwMaterial";
            this.dgwMaterial.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwMaterial.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgwMaterial.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgwMaterial.RowTemplate.Height = 23;
            this.dgwMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwMaterial.Size = new System.Drawing.Size(794, 319);
            this.dgwMaterial.TabIndex = 1;
            this.dgwMaterial.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgwMaterial_RowPostPaint);
            this.dgwMaterial.Click += new System.EventHandler(this.dgwMaterial_Click);
            this.dgwMaterial.DoubleClick += new System.EventHandler(this.dgwMaterial_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.toolStripSeparator2,
            this.btnSave,
            this.btnAdd,
            this.btnEdit,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(794, 39);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(78, 36);
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 36);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 36);
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(78, 36);
            this.btnEdit.Text = "修改";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(78, 36);
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.groupBox1.Size = new System.Drawing.Size(794, 173);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "详细信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Controls.Add(this.txtSpec2, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtName2, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtName1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCode1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSpec1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCode2, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTid2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch2, 5, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 145);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtSpec2
            // 
            this.txtSpec2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSpec2.BackColor = System.Drawing.Color.White;
            this.txtSpec2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtSpec2.Location = new System.Drawing.Point(523, 112);
            this.txtSpec2.Name = "txtSpec2";
            this.txtSpec2.ReadOnly = true;
            this.txtSpec2.Size = new System.Drawing.Size(204, 29);
            this.txtSpec2.TabIndex = 11;
            // 
            // txtName2
            // 
            this.txtName2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtName2.BackColor = System.Drawing.Color.White;
            this.txtName2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtName2.Location = new System.Drawing.Point(523, 75);
            this.txtName2.Name = "txtName2";
            this.txtName2.ReadOnly = true;
            this.txtName2.Size = new System.Drawing.Size(204, 29);
            this.txtName2.TabIndex = 9;
            // 
            // txtName1
            // 
            this.txtName1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtName1.BackColor = System.Drawing.Color.White;
            this.txtName1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtName1.Location = new System.Drawing.Point(113, 75);
            this.txtName1.Name = "txtName1";
            this.txtName1.ReadOnly = true;
            this.txtName1.Size = new System.Drawing.Size(204, 29);
            this.txtName1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(28, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "成品编码:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(438, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "成品编码:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label3.Location = new System.Drawing.Point(28, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "物料名称:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(438, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "物料名称:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(28, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "规格型号:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label6.Location = new System.Drawing.Point(438, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 22);
            this.label6.TabIndex = 5;
            this.label6.Text = "规格型号:";
            // 
            // txtCode1
            // 
            this.txtCode1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCode1.BackColor = System.Drawing.Color.White;
            this.txtCode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtCode1.Location = new System.Drawing.Point(113, 39);
            this.txtCode1.Name = "txtCode1";
            this.txtCode1.ReadOnly = true;
            this.txtCode1.Size = new System.Drawing.Size(204, 29);
            this.txtCode1.TabIndex = 6;
            // 
            // txtSpec1
            // 
            this.txtSpec1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSpec1.BackColor = System.Drawing.Color.White;
            this.txtSpec1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtSpec1.Location = new System.Drawing.Point(113, 112);
            this.txtSpec1.Name = "txtSpec1";
            this.txtSpec1.ReadOnly = true;
            this.txtSpec1.Size = new System.Drawing.Size(204, 29);
            this.txtSpec1.TabIndex = 10;
            // 
            // txtCode2
            // 
            this.txtCode2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCode2.BackColor = System.Drawing.Color.White;
            this.txtCode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtCode2.Location = new System.Drawing.Point(523, 39);
            this.txtCode2.Name = "txtCode2";
            this.txtCode2.ReadOnly = true;
            this.txtCode2.Size = new System.Drawing.Size(204, 29);
            this.txtCode2.TabIndex = 7;
            // 
            // lblTid1
            // 
            this.lblTid1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTid1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTid1, 3);
            this.lblTid1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblTid1.Location = new System.Drawing.Point(156, 7);
            this.lblTid1.Name = "lblTid1";
            this.lblTid1.Size = new System.Drawing.Size(58, 22);
            this.lblTid1.TabIndex = 12;
            this.lblTid1.Text = "非保税";
            // 
            // lblTid2
            // 
            this.lblTid2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTid2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTid2, 3);
            this.lblTid2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblTid2.Location = new System.Drawing.Point(554, 7);
            this.lblTid2.Name = "lblTid2";
            this.lblTid2.Size = new System.Drawing.Size(42, 22);
            this.lblTid2.TabIndex = 13;
            this.lblTid2.Text = "保税";
            // 
            // btnSearch1
            // 
            this.btnSearch1.Location = new System.Drawing.Point(323, 39);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(33, 30);
            this.btnSearch1.TabIndex = 14;
            this.btnSearch1.Text = "...";
            this.btnSearch1.UseVisualStyleBackColor = true;
            this.btnSearch1.Click += new System.EventHandler(this.btnSearch1_Click);
            // 
            // btnSearch2
            // 
            this.btnSearch2.Location = new System.Drawing.Point(733, 39);
            this.btnSearch2.Name = "btnSearch2";
            this.btnSearch2.Size = new System.Drawing.Size(33, 30);
            this.btnSearch2.TabIndex = 15;
            this.btnSearch2.Text = "...";
            this.btnSearch2.UseVisualStyleBackColor = true;
            this.btnSearch2.Click += new System.EventHandler(this.btnSearch2_Click);
            // 
            // Tid
            // 
            this.Tid.DataPropertyName = "SMB_Tid";
            this.Tid.HeaderText = "Tid";
            this.Tid.Name = "Tid";
            this.Tid.ReadOnly = true;
            this.Tid.Visible = false;
            // 
            // SM_Name
            // 
            this.SM_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SM_Name.DataPropertyName = "SMB_Name";
            this.SM_Name.FillWeight = 10F;
            this.SM_Name.HeaderText = "物料名称";
            this.SM_Name.Name = "SM_Name";
            this.SM_Name.ReadOnly = true;
            this.SM_Name.Width = 99;
            // 
            // SM_Spec
            // 
            this.SM_Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SM_Spec.DataPropertyName = "SMB_Spec";
            this.SM_Spec.FillWeight = 10F;
            this.SM_Spec.HeaderText = "规格型号";
            this.SM_Spec.Name = "SM_Spec";
            this.SM_Spec.ReadOnly = true;
            this.SM_Spec.Width = 99;
            // 
            // SMB_Name_Bonded
            // 
            this.SMB_Name_Bonded.HeaderText = "保税物料名称";
            this.SMB_Name_Bonded.Name = "SMB_Name_Bonded";
            this.SMB_Name_Bonded.ReadOnly = true;
            this.SMB_Name_Bonded.Visible = false;
            // 
            // SMB_Spec_Bonded
            // 
            this.SMB_Spec_Bonded.HeaderText = "保税型号规格";
            this.SMB_Spec_Bonded.Name = "SMB_Spec_Bonded";
            this.SMB_Spec_Bonded.ReadOnly = true;
            this.SMB_Spec_Bonded.Visible = false;
            // 
            // SMTid
            // 
            this.SMTid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SMTid.DataPropertyName = "SMB_SMTid";
            this.SMTid.FillWeight = 20F;
            this.SMTid.HeaderText = "非保税型号主键";
            this.SMTid.Name = "SMTid";
            this.SMTid.ReadOnly = true;
            this.SMTid.Visible = false;
            // 
            // SMCode
            // 
            this.SMCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SMCode.DataPropertyName = "SMB_SMCode";
            this.SMCode.FillWeight = 20F;
            this.SMCode.HeaderText = "非保税型号编码";
            this.SMCode.Name = "SMCode";
            this.SMCode.ReadOnly = true;
            this.SMCode.Width = 147;
            // 
            // SMTid_Bonded
            // 
            this.SMTid_Bonded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SMTid_Bonded.DataPropertyName = "SMB_SMTid_Bonded";
            this.SMTid_Bonded.FillWeight = 20F;
            this.SMTid_Bonded.HeaderText = "保税型号主键";
            this.SMTid_Bonded.Name = "SMTid_Bonded";
            this.SMTid_Bonded.ReadOnly = true;
            this.SMTid_Bonded.Visible = false;
            // 
            // SMCode_Bonded
            // 
            this.SMCode_Bonded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SMCode_Bonded.DataPropertyName = "SMB_SMCode_Bonded";
            this.SMCode_Bonded.FillWeight = 20F;
            this.SMCode_Bonded.HeaderText = "保税型号编码";
            this.SMCode_Bonded.Name = "SMCode_Bonded";
            this.SMCode_Bonded.ReadOnly = true;
            this.SMCode_Bonded.Width = 131;
            // 
            // Pid
            // 
            this.Pid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Pid.DataPropertyName = "BOO_LogName";
            this.Pid.HeaderText = "添加人";
            this.Pid.Name = "Pid";
            this.Pid.ReadOnly = true;
            this.Pid.Width = 83;
            // 
            // AddDate
            // 
            this.AddDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.AddDate.DataPropertyName = "SMB_AddDate";
            this.AddDate.HeaderText = "添加时间";
            this.AddDate.Name = "AddDate";
            this.AddDate.ReadOnly = true;
            this.AddDate.Width = 99;
            // 
            // frmMaterialConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 531);
            this.Controls.Add(this.dgwMaterial);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMaterialConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保税型号对照表";
            this.Load += new System.EventHandler(this.frmMaterialConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwMaterial)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tXD_Material_ConfigBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource tXD_Material_ConfigBindingSource;
        private System.Windows.Forms.DataGridView dgwMaterial;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtSpec2;
        private System.Windows.Forms.TextBox txtName2;
        private System.Windows.Forms.TextBox txtName1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCode1;
        private System.Windows.Forms.TextBox txtSpec1;
        private System.Windows.Forms.TextBox txtCode2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Label lblTid1;
        private System.Windows.Forms.Label lblTid2;
        private System.Windows.Forms.Button btnSearch1;
        private System.Windows.Forms.Button btnSearch2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SM_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn SM_Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMB_Name_Bonded;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMB_Spec_Bonded;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMTid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMTid_Bonded;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMCode_Bonded;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddDate;
    }
}

