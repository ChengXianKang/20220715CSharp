namespace MDC
{
    partial class frmNGReworkScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNGReworkScan));
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtScanCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGlassCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblNGType = new System.Windows.Forms.Label();
            this.txtExceptionState = new System.Windows.Forms.TextBox();
            this.txtProductOrder = new System.Windows.Forms.TextBox();
            this.txtFinishesModel = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblNG = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtExceptionContent = new System.Windows.Forms.TextBox();
            this.lblOP = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtOp = new System.Windows.Forms.TextBox();
            this.txtLineName = new System.Windows.Forms.TextBox();
            this.lblDeviceIP = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDeviceIP = new System.Windows.Forms.TextBox();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCurrentOpCode = new System.Windows.Forms.TextBox();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.cmbProcess = new System.Windows.Forms.ComboBox();
            this.txtProcessIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlSplit1 = new System.Windows.Forms.Panel();
            this.bgwWriteData = new System.ComponentModel.BackgroundWorker();
            this.imglNG = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvwProcess = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lvwNote = new Utils.ListViewNote(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtScanCode, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label10, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtGlassCode, 5, 0);
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label14, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblNGType, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtExceptionState, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtProductOrder, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtFinishesModel, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblTime, 4, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblNG, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtTime, 5, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtExceptionContent, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblOP, 4, 2);
            this.tableLayoutPanel5.Controls.Add(this.label17, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtOp, 5, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtLineName, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblDeviceIP, 4, 3);
            this.tableLayoutPanel5.Controls.Add(this.label15, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtDeviceIP, 5, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtProcess, 3, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(8, 47);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(778, 129);
            this.tableLayoutPanel5.TabIndex = 122;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label9.Location = new System.Drawing.Point(9, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 19);
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
            this.txtScanCode.Location = new System.Drawing.Point(83, 3);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.Size = new System.Drawing.Size(430, 26);
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
            this.label10.Location = new System.Drawing.Point(527, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 19);
            this.label10.TabIndex = 127;
            this.label10.Text = "LCD编码:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGlassCode
            // 
            this.txtGlassCode.BackColor = System.Drawing.Color.Lavender;
            this.txtGlassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGlassCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGlassCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtGlassCode.Location = new System.Drawing.Point(601, 3);
            this.txtGlassCode.Name = "txtGlassCode";
            this.txtGlassCode.ReadOnly = true;
            this.txtGlassCode.Size = new System.Drawing.Size(172, 26);
            this.txtGlassCode.TabIndex = 126;
            this.txtGlassCode.Text = "LCD编码";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label3.Location = new System.Drawing.Point(9, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 131;
            this.label3.Text = "成品型号:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label14.Location = new System.Drawing.Point(9, 70);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 19);
            this.label14.TabIndex = 130;
            this.label14.Text = "生产工单:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNGType
            // 
            this.lblNGType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNGType.BackColor = System.Drawing.Color.Transparent;
            this.lblNGType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNGType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblNGType.Location = new System.Drawing.Point(9, 103);
            this.lblNGType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNGType.Name = "lblNGType";
            this.lblNGType.Size = new System.Drawing.Size(69, 19);
            this.lblNGType.TabIndex = 130;
            this.lblNGType.Text = "当前状态:";
            this.lblNGType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExceptionState
            // 
            this.txtExceptionState.BackColor = System.Drawing.Color.Lavender;
            this.txtExceptionState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExceptionState.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtExceptionState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtExceptionState.Location = new System.Drawing.Point(83, 99);
            this.txtExceptionState.Name = "txtExceptionState";
            this.txtExceptionState.ReadOnly = true;
            this.txtExceptionState.Size = new System.Drawing.Size(172, 26);
            this.txtExceptionState.TabIndex = 138;
            this.txtExceptionState.TabStop = false;
            this.txtExceptionState.Text = "重工/报废/良品";
            // 
            // txtProductOrder
            // 
            this.txtProductOrder.BackColor = System.Drawing.Color.Lavender;
            this.txtProductOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductOrder.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProductOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProductOrder.Location = new System.Drawing.Point(83, 67);
            this.txtProductOrder.Name = "txtProductOrder";
            this.txtProductOrder.ReadOnly = true;
            this.txtProductOrder.Size = new System.Drawing.Size(172, 26);
            this.txtProductOrder.TabIndex = 134;
            this.txtProductOrder.TabStop = false;
            this.txtProductOrder.Text = "工单编码";
            // 
            // txtFinishesModel
            // 
            this.txtFinishesModel.BackColor = System.Drawing.Color.Lavender;
            this.txtFinishesModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFinishesModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFinishesModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtFinishesModel.Location = new System.Drawing.Point(83, 35);
            this.txtFinishesModel.Name = "txtFinishesModel";
            this.txtFinishesModel.ReadOnly = true;
            this.txtFinishesModel.Size = new System.Drawing.Size(172, 26);
            this.txtFinishesModel.TabIndex = 135;
            this.txtFinishesModel.TabStop = false;
            this.txtFinishesModel.Text = "成品型号";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblTime.Location = new System.Drawing.Point(527, 38);
            this.lblTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(69, 19);
            this.lblTime.TabIndex = 129;
            this.lblTime.Text = "复判时间:";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNG
            // 
            this.lblNG.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNG.BackColor = System.Drawing.Color.Transparent;
            this.lblNG.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblNG.Location = new System.Drawing.Point(263, 38);
            this.lblNG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(72, 20);
            this.lblNG.TabIndex = 116;
            this.lblNG.Text = "复判不良:";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.Lavender;
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtTime.Location = new System.Drawing.Point(601, 35);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(174, 26);
            this.txtTime.TabIndex = 137;
            this.txtTime.TabStop = false;
            this.txtTime.Text = "复判时间";
            // 
            // txtExceptionContent
            // 
            this.txtExceptionContent.BackColor = System.Drawing.Color.Lavender;
            this.txtExceptionContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExceptionContent.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtExceptionContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtExceptionContent.Location = new System.Drawing.Point(342, 35);
            this.txtExceptionContent.Name = "txtExceptionContent";
            this.txtExceptionContent.ReadOnly = true;
            this.txtExceptionContent.Size = new System.Drawing.Size(172, 26);
            this.txtExceptionContent.TabIndex = 9;
            this.txtExceptionContent.Text = "不良描述字符串";
            // 
            // lblOP
            // 
            this.lblOP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOP.BackColor = System.Drawing.Color.Transparent;
            this.lblOP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblOP.Location = new System.Drawing.Point(527, 70);
            this.lblOP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(69, 19);
            this.lblOP.TabIndex = 138;
            this.lblOP.Text = "复判人员:";
            this.lblOP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label17.Location = new System.Drawing.Point(268, 70);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 19);
            this.label17.TabIndex = 132;
            this.label17.Text = "申报产线:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOp
            // 
            this.txtOp.BackColor = System.Drawing.Color.Lavender;
            this.txtOp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOp.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtOp.Location = new System.Drawing.Point(601, 67);
            this.txtOp.Name = "txtOp";
            this.txtOp.ReadOnly = true;
            this.txtOp.Size = new System.Drawing.Size(174, 26);
            this.txtOp.TabIndex = 139;
            this.txtOp.TabStop = false;
            this.txtOp.Text = "复判人员账号";
            // 
            // txtLineName
            // 
            this.txtLineName.BackColor = System.Drawing.Color.Lavender;
            this.txtLineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLineName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLineName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtLineName.Location = new System.Drawing.Point(342, 67);
            this.txtLineName.Name = "txtLineName";
            this.txtLineName.ReadOnly = true;
            this.txtLineName.Size = new System.Drawing.Size(172, 26);
            this.txtLineName.TabIndex = 136;
            this.txtLineName.TabStop = false;
            this.txtLineName.Text = "产线编码";
            // 
            // lblDeviceIP
            // 
            this.lblDeviceIP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDeviceIP.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDeviceIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblDeviceIP.Location = new System.Drawing.Point(527, 103);
            this.lblDeviceIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDeviceIP.Name = "lblDeviceIP";
            this.lblDeviceIP.Size = new System.Drawing.Size(69, 19);
            this.lblDeviceIP.TabIndex = 130;
            this.lblDeviceIP.Text = "复 判 I P:";
            this.lblDeviceIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label15.Location = new System.Drawing.Point(268, 103);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 19);
            this.label15.TabIndex = 131;
            this.label15.Text = "申报工序:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDeviceIP
            // 
            this.txtDeviceIP.BackColor = System.Drawing.Color.Lavender;
            this.txtDeviceIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeviceIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeviceIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtDeviceIP.Location = new System.Drawing.Point(601, 99);
            this.txtDeviceIP.Name = "txtDeviceIP";
            this.txtDeviceIP.ReadOnly = true;
            this.txtDeviceIP.Size = new System.Drawing.Size(174, 26);
            this.txtDeviceIP.TabIndex = 138;
            this.txtDeviceIP.TabStop = false;
            this.txtDeviceIP.Text = "复判IP";
            // 
            // txtProcess
            // 
            this.txtProcess.BackColor = System.Drawing.Color.Lavender;
            this.txtProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProcess.Location = new System.Drawing.Point(342, 99);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(173, 26);
            this.txtProcess.TabIndex = 144;
            this.txtProcess.TabStop = false;
            this.txtProcess.Text = "复判站点工序名称";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label13.Location = new System.Drawing.Point(524, 6);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 20);
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
            this.txtCurrentOpCode.Location = new System.Drawing.Point(601, 3);
            this.txtCurrentOpCode.Name = "txtCurrentOpCode";
            this.txtCurrentOpCode.ReadOnly = true;
            this.txtCurrentOpCode.Size = new System.Drawing.Size(174, 26);
            this.txtCurrentOpCode.TabIndex = 15;
            this.txtCurrentOpCode.TabStop = false;
            this.txtCurrentOpCode.Text = "登录账号";
            // 
            // tlpInfo
            // 
            this.tlpInfo.AutoSize = true;
            this.tlpInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tlpInfo.ColumnCount = 6;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpInfo.Controls.Add(this.cmbProcess, 0, 0);
            this.tlpInfo.Controls.Add(this.txtProcessIP, 3, 0);
            this.tlpInfo.Controls.Add(this.label1, 2, 0);
            this.tlpInfo.Controls.Add(this.label12, 0, 0);
            this.tlpInfo.Controls.Add(this.label13, 4, 0);
            this.tlpInfo.Controls.Add(this.txtCurrentOpCode, 5, 0);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpInfo.Location = new System.Drawing.Point(8, 8);
            this.tlpInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 1;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpInfo.Size = new System.Drawing.Size(778, 33);
            this.tlpInfo.TabIndex = 109;
            // 
            // cmbProcess
            // 
            this.cmbProcess.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.cmbProcess.FormattingEnabled = true;
            this.cmbProcess.Location = new System.Drawing.Point(83, 3);
            this.cmbProcess.Name = "cmbProcess";
            this.cmbProcess.Size = new System.Drawing.Size(173, 27);
            this.cmbProcess.TabIndex = 132;
            this.cmbProcess.SelectedIndexChanged += new System.EventHandler(this.cmbProcess_SelectedIndexChanged);
            // 
            // txtProcessIP
            // 
            this.txtProcessIP.BackColor = System.Drawing.Color.Lavender;
            this.txtProcessIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcessIP.Enabled = false;
            this.txtProcessIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcessIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtProcessIP.Location = new System.Drawing.Point(342, 3);
            this.txtProcessIP.Name = "txtProcessIP";
            this.txtProcessIP.ReadOnly = true;
            this.txtProcessIP.Size = new System.Drawing.Size(173, 26);
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
            this.label1.Location = new System.Drawing.Point(265, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
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
            this.label12.Location = new System.Drawing.Point(4, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 20);
            this.label12.TabIndex = 131;
            this.label12.Text = "返修岗位:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSplit1
            // 
            this.pnlSplit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit1.Location = new System.Drawing.Point(8, 41);
            this.pnlSplit1.Name = "pnlSplit1";
            this.pnlSplit1.Size = new System.Drawing.Size(778, 6);
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
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(8, 176);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(778, 6);
            this.panel2.TabIndex = 124;
            // 
            // lvwProcess
            // 
            this.lvwProcess.AllowColumnReorder = true;
            this.lvwProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.lvwProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.tableLayoutPanel4.SetColumnSpan(this.lvwProcess, 2);
            this.lvwProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwProcess.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvwProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lvwProcess.FullRowSelect = true;
            this.lvwProcess.Location = new System.Drawing.Point(3, 23);
            this.lvwProcess.Name = "lvwProcess";
            this.lvwProcess.ShowItemToolTips = true;
            this.lvwProcess.Size = new System.Drawing.Size(253, 315);
            this.lvwProcess.TabIndex = 125;
            this.lvwProcess.TabStop = false;
            this.lvwProcess.UseCompatibleStateImageBehavior = false;
            this.lvwProcess.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 44;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "工序名称";
            this.columnHeader3.Width = 180;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.TabIndex = 132;
            this.label5.Text = "已过工序:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.lvwNote, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.lvwProcess, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(8, 182);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(778, 341);
            this.tableLayoutPanel4.TabIndex = 123;
            // 
            // lvwNote
            // 
            this.lvwNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.lvwNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel4.SetColumnSpan(this.lvwNote, 4);
            this.lvwNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwNote.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvwNote.FullRowSelect = true;
            this.lvwNote.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwNote.HideSelection = false;
            this.lvwNote.Location = new System.Drawing.Point(262, 23);
            this.lvwNote.Name = "lvwNote";
            this.lvwNote.ShowItemToolTips = true;
            this.lvwNote.Size = new System.Drawing.Size(513, 315);
            this.lvwNote.TabIndex = 133;
            this.lvwNote.UseCompatibleStateImageBehavior = false;
            this.lvwNote.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(263, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 134;
            this.label2.Text = "即时消息:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmNGReworkScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(170)))), ((int)(((byte)(193)))));
            this.ClientSize = new System.Drawing.Size(794, 531);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.pnlSplit1);
            this.Controls.Add(this.tlpInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNGReworkScan";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "返修过站";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNGReworkScan_FormClosing);
            this.Load += new System.EventHandler(this.frmNGReworkScan_Load);
            this.SizeChanged += new System.EventHandler(this.frmNGReview_SizeChanged);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
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
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.TextBox txtFinishesModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtScanCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtGlassCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtProductOrder;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.TextBox txtOp;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtLineName;
        private System.Windows.Forms.TextBox txtExceptionContent;
        private System.Windows.Forms.Label lblNG;
        private System.Windows.Forms.TextBox txtExceptionState;
        private System.Windows.Forms.Label lblNGType;
        private System.Windows.Forms.ImageList imglNG;
        private System.Windows.Forms.TextBox txtDeviceIP;
        private System.Windows.Forms.Label lblDeviceIP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbProcess;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lvwProcess;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private Utils.ListViewNote lvwNote;
        private System.Windows.Forms.Label label2;
    }
}