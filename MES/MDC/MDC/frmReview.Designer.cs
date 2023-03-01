namespace MDC
{
    partial class frmReview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReview));
            this.flpNG = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDescribe = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnErrJudge = new System.Windows.Forms.Button();
            this.btnScrap = new System.Windows.Forms.Button();
            this.btnRework = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.txtFinishesModel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCurrentOpCode = new System.Windows.Forms.TextBox();
            this.txtProcessIP = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlSplit1 = new System.Windows.Forms.Panel();
            this.txtCurrentLineName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtScanCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLCD = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtProductOrder = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.lblOP = new System.Windows.Forms.Label();
            this.txtOp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLineCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtError = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bgwWriteData = new System.ComponentModel.BackgroundWorker();
            this.flpNG.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpNG
            // 
            this.flpNG.AutoScroll = true;
            this.flpNG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.flpNG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel5.SetColumnSpan(this.flpNG, 6);
            this.flpNG.Controls.Add(this.checkBox1);
            this.flpNG.Controls.Add(this.checkBox2);
            this.flpNG.Controls.Add(this.checkBox3);
            this.flpNG.Controls.Add(this.checkBox4);
            this.flpNG.Controls.Add(this.tableLayoutPanel3);
            this.flpNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpNG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.flpNG.Location = new System.Drawing.Point(3, 194);
            this.flpNG.Name = "flpNG";
            this.flpNG.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.flpNG.Size = new System.Drawing.Size(772, 190);
            this.flpNG.TabIndex = 114;
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
            this.checkBox1.Location = new System.Drawing.Point(13, 10);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(186, 45);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "LCD显示不良";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.BNC_CheckStateChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.checkBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.checkBox2.Location = new System.Drawing.Point(205, 10);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(132, 45);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "LCD暗点";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.checkBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.checkBox3.Location = new System.Drawing.Point(343, 10);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(132, 45);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "LCD亮点";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.checkBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.checkBox4.Location = new System.Drawing.Point(481, 10);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(186, 45);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "LCD显示不良";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Location = new System.Drawing.Point(13, 65);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(300, 0);
            this.tableLayoutPanel3.TabIndex = 119;
            // 
            // txtDescribe
            // 
            this.txtDescribe.BackColor = System.Drawing.Color.Lavender;
            this.txtDescribe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel5.SetColumnSpan(this.txtDescribe, 3);
            this.txtDescribe.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescribe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtDescribe.Location = new System.Drawing.Point(78, 137);
            this.txtDescribe.Name = "txtDescribe";
            this.txtDescribe.ReadOnly = true;
            this.txtDescribe.Size = new System.Drawing.Size(437, 26);
            this.txtDescribe.TabIndex = 8;
            this.txtDescribe.Text = "不良描述字符串";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.btnErrJudge, 5, 10);
            this.tableLayoutPanel5.Controls.Add(this.btnScrap, 3, 10);
            this.tableLayoutPanel5.Controls.Add(this.btnRework, 1, 10);
            this.tableLayoutPanel5.Controls.Add(this.lblType, 0, 10);
            this.tableLayoutPanel5.Controls.Add(this.txtProcess, 5, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtFinishesModel, 3, 4);
            this.tableLayoutPanel5.Controls.Add(this.label3, 2, 4);
            this.tableLayoutPanel5.Controls.Add(this.panel3, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.panel2, 0, 9);
            this.tableLayoutPanel5.Controls.Add(this.txtCurrentOpCode, 5, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtProcessIP, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label13, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.pnlSplit1, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtCurrentLineName, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtScanCode, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtLCD, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.txtProductOrder, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.label10, 4, 3);
            this.tableLayoutPanel5.Controls.Add(this.lblTime, 4, 5);
            this.tableLayoutPanel5.Controls.Add(this.txtTime, 5, 5);
            this.tableLayoutPanel5.Controls.Add(this.lblOP, 4, 4);
            this.tableLayoutPanel5.Controls.Add(this.txtOp, 5, 4);
            this.tableLayoutPanel5.Controls.Add(this.flpNG, 0, 8);
            this.tableLayoutPanel5.Controls.Add(this.txtDescribe, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.label12, 4, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtLineCode, 5, 2);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 11;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(778, 451);
            this.tableLayoutPanel5.TabIndex = 121;
            // 
            // btnErrJudge
            // 
            this.btnErrJudge.BackColor = System.Drawing.SystemColors.Control;
            this.btnErrJudge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnErrJudge.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnErrJudge.FlatAppearance.BorderSize = 2;
            this.btnErrJudge.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnErrJudge.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.btnErrJudge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnErrJudge.Image = ((System.Drawing.Image)(resources.GetObject("btnErrJudge.Image")));
            this.btnErrJudge.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnErrJudge.Location = new System.Drawing.Point(596, 396);
            this.btnErrJudge.Name = "btnErrJudge";
            this.btnErrJudge.Size = new System.Drawing.Size(179, 52);
            this.btnErrJudge.TabIndex = 150;
            this.btnErrJudge.Tag = "";
            this.btnErrJudge.Text = "良  品";
            this.btnErrJudge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnErrJudge.UseVisualStyleBackColor = true;
            this.btnErrJudge.Click += new System.EventHandler(this.btnErrJudge_Click);
            // 
            // btnScrap
            // 
            this.btnScrap.BackColor = System.Drawing.SystemColors.Control;
            this.btnScrap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScrap.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnScrap.FlatAppearance.BorderSize = 2;
            this.btnScrap.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnScrap.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.btnScrap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnScrap.Image = ((System.Drawing.Image)(resources.GetObject("btnScrap.Image")));
            this.btnScrap.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnScrap.Location = new System.Drawing.Point(337, 396);
            this.btnScrap.Name = "btnScrap";
            this.btnScrap.Size = new System.Drawing.Size(178, 52);
            this.btnScrap.TabIndex = 148;
            this.btnScrap.Tag = "";
            this.btnScrap.Text = "报  废";
            this.btnScrap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnScrap.UseVisualStyleBackColor = true;
            this.btnScrap.Click += new System.EventHandler(this.btnScrap_Click);
            // 
            // btnRework
            // 
            this.btnRework.BackColor = System.Drawing.SystemColors.Control;
            this.btnRework.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRework.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnRework.FlatAppearance.BorderSize = 2;
            this.btnRework.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnRework.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.btnRework.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btnRework.Image = ((System.Drawing.Image)(resources.GetObject("btnRework.Image")));
            this.btnRework.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRework.Location = new System.Drawing.Point(78, 396);
            this.btnRework.Name = "btnRework";
            this.btnRework.Size = new System.Drawing.Size(178, 52);
            this.btnRework.TabIndex = 147;
            this.btnRework.Tag = "";
            this.btnRework.Text = "重  工";
            this.btnRework.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRework.UseVisualStyleBackColor = true;
            this.btnRework.Click += new System.EventHandler(this.btnRework_Click);
            // 
            // lblType
            // 
            this.lblType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblType.BackColor = System.Drawing.Color.Transparent;
            this.lblType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblType.Location = new System.Drawing.Point(4, 412);
            this.lblType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(69, 19);
            this.lblType.TabIndex = 145;
            this.lblType.Text = "异常类型:";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProcess
            // 
            this.txtProcess.BackColor = System.Drawing.Color.Lavender;
            this.txtProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProcess.Location = new System.Drawing.Point(596, 73);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(179, 26);
            this.txtProcess.TabIndex = 144;
            this.txtProcess.TabStop = false;
            this.txtProcess.Text = "提报站点工序名称";
            // 
            // txtFinishesModel
            // 
            this.txtFinishesModel.BackColor = System.Drawing.Color.Lavender;
            this.txtFinishesModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFinishesModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFinishesModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtFinishesModel.Location = new System.Drawing.Point(337, 105);
            this.txtFinishesModel.Name = "txtFinishesModel";
            this.txtFinishesModel.ReadOnly = true;
            this.txtFinishesModel.Size = new System.Drawing.Size(178, 26);
            this.txtFinishesModel.TabIndex = 135;
            this.txtFinishesModel.TabStop = false;
            this.txtFinishesModel.Text = "成品型号";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label3.Location = new System.Drawing.Point(263, 108);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 131;
            this.label3.Text = "成品型号:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.tableLayoutPanel5.SetColumnSpan(this.panel3, 6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 166);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(778, 6);
            this.panel3.TabIndex = 121;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.tableLayoutPanel5.SetColumnSpan(this.panel2, 6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 387);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(778, 6);
            this.panel2.TabIndex = 120;
            // 
            // txtCurrentOpCode
            // 
            this.txtCurrentOpCode.BackColor = System.Drawing.Color.Lavender;
            this.txtCurrentOpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentOpCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrentOpCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtCurrentOpCode.Location = new System.Drawing.Point(596, 3);
            this.txtCurrentOpCode.Name = "txtCurrentOpCode";
            this.txtCurrentOpCode.ReadOnly = true;
            this.txtCurrentOpCode.Size = new System.Drawing.Size(179, 26);
            this.txtCurrentOpCode.TabIndex = 15;
            this.txtCurrentOpCode.TabStop = false;
            this.txtCurrentOpCode.Text = "当前登录账号";
            // 
            // txtProcessIP
            // 
            this.txtProcessIP.BackColor = System.Drawing.Color.Lavender;
            this.txtProcessIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcessIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcessIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProcessIP.Location = new System.Drawing.Point(78, 3);
            this.txtProcessIP.Name = "txtProcessIP";
            this.txtProcessIP.ReadOnly = true;
            this.txtProcessIP.Size = new System.Drawing.Size(178, 26);
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
            this.label13.Location = new System.Drawing.Point(521, 6);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 20);
            this.label13.TabIndex = 116;
            this.label13.Text = "复判人员:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSplit1
            // 
            this.pnlSplit1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.tableLayoutPanel5.SetColumnSpan(this.pnlSplit1, 6);
            this.pnlSplit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplit1.Location = new System.Drawing.Point(0, 32);
            this.pnlSplit1.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSplit1.Name = "pnlSplit1";
            this.pnlSplit1.Size = new System.Drawing.Size(778, 6);
            this.pnlSplit1.TabIndex = 113;
            // 
            // txtCurrentLineName
            // 
            this.txtCurrentLineName.BackColor = System.Drawing.Color.Lavender;
            this.txtCurrentLineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentLineName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrentLineName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtCurrentLineName.Location = new System.Drawing.Point(337, 3);
            this.txtCurrentLineName.Name = "txtCurrentLineName";
            this.txtCurrentLineName.ReadOnly = true;
            this.txtCurrentLineName.Size = new System.Drawing.Size(178, 26);
            this.txtCurrentLineName.TabIndex = 123;
            this.txtCurrentLineName.TabStop = false;
            this.txtCurrentLineName.Text = "产线名称";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(262, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 122;
            this.label2.Text = "产线名称:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 121;
            this.label1.Text = "站  点 IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(4, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 124;
            this.label4.Text = "扫描编码:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtScanCode
            // 
            this.txtScanCode.BackColor = System.Drawing.Color.White;
            this.txtScanCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel5.SetColumnSpan(this.txtScanCode, 3);
            this.txtScanCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtScanCode.Location = new System.Drawing.Point(78, 41);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.Size = new System.Drawing.Size(437, 26);
            this.txtScanCode.TabIndex = 125;
            this.txtScanCode.Text = "FPC编码";
            this.txtScanCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFPC_KeyPress);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(4, 76);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 127;
            this.label5.Text = "LCD编码:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLCD
            // 
            this.txtLCD.BackColor = System.Drawing.Color.Lavender;
            this.txtLCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel5.SetColumnSpan(this.txtLCD, 3);
            this.txtLCD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLCD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtLCD.Location = new System.Drawing.Point(78, 73);
            this.txtLCD.Name = "txtLCD";
            this.txtLCD.ReadOnly = true;
            this.txtLCD.Size = new System.Drawing.Size(437, 26);
            this.txtLCD.TabIndex = 126;
            this.txtLCD.Text = "LCD编码";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label9.Location = new System.Drawing.Point(4, 108);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 19);
            this.label9.TabIndex = 130;
            this.label9.Text = "工单编码:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductOrder
            // 
            this.txtProductOrder.BackColor = System.Drawing.Color.Lavender;
            this.txtProductOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductOrder.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProductOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProductOrder.Location = new System.Drawing.Point(78, 105);
            this.txtProductOrder.Name = "txtProductOrder";
            this.txtProductOrder.ReadOnly = true;
            this.txtProductOrder.Size = new System.Drawing.Size(178, 26);
            this.txtProductOrder.TabIndex = 134;
            this.txtProductOrder.TabStop = false;
            this.txtProductOrder.Text = "工单编码";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label10.Location = new System.Drawing.Point(522, 76);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 19);
            this.label10.TabIndex = 131;
            this.label10.Text = "提报站点:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblTime.Location = new System.Drawing.Point(522, 140);
            this.lblTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(69, 19);
            this.lblTime.TabIndex = 129;
            this.lblTime.Text = "提报时间:";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.Lavender;
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtTime.Location = new System.Drawing.Point(596, 137);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(179, 26);
            this.txtTime.TabIndex = 137;
            this.txtTime.TabStop = false;
            this.txtTime.Text = "提报时间";
            // 
            // lblOP
            // 
            this.lblOP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOP.BackColor = System.Drawing.Color.Transparent;
            this.lblOP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblOP.Location = new System.Drawing.Point(522, 108);
            this.lblOP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(69, 19);
            this.lblOP.TabIndex = 138;
            this.lblOP.Text = "提报人员:";
            this.lblOP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOp
            // 
            this.txtOp.BackColor = System.Drawing.Color.Lavender;
            this.txtOp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOp.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtOp.Location = new System.Drawing.Point(596, 105);
            this.txtOp.Name = "txtOp";
            this.txtOp.ReadOnly = true;
            this.txtOp.Size = new System.Drawing.Size(179, 26);
            this.txtOp.TabIndex = 139;
            this.txtOp.TabStop = false;
            this.txtOp.Text = "提报人员账号";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label8.Location = new System.Drawing.Point(4, 140);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 19);
            this.label8.TabIndex = 142;
            this.label8.Text = "异常描述:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label12.Location = new System.Drawing.Point(522, 44);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 19);
            this.label12.TabIndex = 132;
            this.label12.Text = "产线编码:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLineCode
            // 
            this.txtLineCode.BackColor = System.Drawing.Color.Lavender;
            this.txtLineCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLineCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLineCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtLineCode.Location = new System.Drawing.Point(596, 41);
            this.txtLineCode.Name = "txtLineCode";
            this.txtLineCode.ReadOnly = true;
            this.txtLineCode.Size = new System.Drawing.Size(178, 26);
            this.txtLineCode.TabIndex = 136;
            this.txtLineCode.TabStop = false;
            this.txtLineCode.Text = "产线编码";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label6.Location = new System.Drawing.Point(4, 172);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 19);
            this.label6.TabIndex = 143;
            this.label6.Text = "异常明细:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtError, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 465);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(778, 58);
            this.tableLayoutPanel2.TabIndex = 118;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(596, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 52);
            this.btnCancel.TabIndex = 76;
            this.btnCancel.Tag = "";
            this.btnCancel.Text = "取 消";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnOK.FlatAppearance.BorderSize = 2;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.Green;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(337, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(178, 52);
            this.btnOK.TabIndex = 75;
            this.btnOK.Tag = "";
            this.btnOK.Text = "提 交";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtError
            // 
            this.txtError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.txtError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.txtError, 3);
            this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtError.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtError.ForeColor = System.Drawing.Color.Red;
            this.txtError.Location = new System.Drawing.Point(10, 5);
            this.txtError.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(314, 48);
            this.txtError.TabIndex = 77;
            this.txtError.Text = "错误信息";
            this.txtError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(8, 459);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 6);
            this.panel1.TabIndex = 120;
            // 
            // bgwWriteData
            // 
            this.bgwWriteData.WorkerReportsProgress = true;
            this.bgwWriteData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWriteData_DoWork);
            this.bgwWriteData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWriteData_RunWorkerCompleted);
            // 
            // frmReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(794, 531);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReview";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重工复判";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReview_FormClosing);
            this.Load += new System.EventHandler(this.frmReview_Load);
            this.Shown += new System.EventHandler(this.frmReview_Shown);
            this.SizeChanged += new System.EventHandler(this.frmReview_SizeChanged);
            this.flpNG.ResumeLayout(false);
            this.flpNG.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescribe;
        private System.Windows.Forms.TextBox txtCurrentOpCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProcessIP;
        private System.Windows.Forms.Panel pnlSplit1;
        private System.Windows.Forms.FlowLayoutPanel flpNG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.ComponentModel.BackgroundWorker bgwWriteData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurrentLineName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtScanCode;
        private System.Windows.Forms.TextBox txtLCD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox txtProductOrder;
        private System.Windows.Forms.TextBox txtLineCode;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.TextBox txtOp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnErrJudge;
        private System.Windows.Forms.Button btnScrap;
        private System.Windows.Forms.Button btnRework;
        private System.Windows.Forms.TextBox txtFinishesModel;
        private System.Windows.Forms.Label label3;
    }
}