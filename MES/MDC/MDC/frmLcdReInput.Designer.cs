namespace MDC
{
    partial class frmLcdReInput
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
            this.txtScanCode = new System.Windows.Forms.TextBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInfo2 = new System.Windows.Forms.TextBox();
            this.txtScanCode2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtScanCode
            // 
            this.txtScanCode.BackColor = System.Drawing.Color.White;
            this.txtScanCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtScanCode.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtScanCode.Location = new System.Drawing.Point(3, 98);
            this.txtScanCode.Name = "txtScanCode";
            this.txtScanCode.Size = new System.Drawing.Size(570, 33);
            this.txtScanCode.TabIndex = 4;
            this.txtScanCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScanCode_KeyPress);
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.Color.Black;
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInfo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtInfo.Location = new System.Drawing.Point(3, 131);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo.Size = new System.Drawing.Size(570, 401);
            this.txtInfo.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(584, 561);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtInfo);
            this.tabPage1.Controls.Add(this.txtScanCode);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "补LCD投入";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtInfo2);
            this.tabPage2.Controls.Add(this.txtScanCode2);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(559, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "补后段信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 95);
            this.label1.TabIndex = 6;
            this.label1.Text = "仅用于前段：\r\n补扫后提示成功或玻璃码已绑定，但仍然无法过后面站点，\r\n扫码后可自动补齐LCD投入或FPC绑定玻璃过站信息\r\n有的玻璃会删除玻璃码信息，需要从头重" +
    "新补扫LCD投入\r\n根据程序提示进行下一步处理。";
            // 
            // txtInfo2
            // 
            this.txtInfo2.BackColor = System.Drawing.Color.Black;
            this.txtInfo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInfo2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtInfo2.Location = new System.Drawing.Point(3, 93);
            this.txtInfo2.Multiline = true;
            this.txtInfo2.Name = "txtInfo2";
            this.txtInfo2.ReadOnly = true;
            this.txtInfo2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo2.Size = new System.Drawing.Size(553, 240);
            this.txtInfo2.TabIndex = 8;
            // 
            // txtScanCode2
            // 
            this.txtScanCode2.BackColor = System.Drawing.Color.White;
            this.txtScanCode2.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtScanCode2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanCode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.txtScanCode2.Location = new System.Drawing.Point(3, 60);
            this.txtScanCode2.Name = "txtScanCode2";
            this.txtScanCode2.Size = new System.Drawing.Size(553, 33);
            this.txtScanCode2.TabIndex = 7;
            this.txtScanCode2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(411, 57);
            this.label2.TabIndex = 9;
            this.label2.Text = "仅用于后段：\r\n自动站点有过站记录，但是仍然提示漏扫\r\n自动补齐贴合上料，BL关联玻璃，客户码绑定";
            // 
            // frmLcdReInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmLcdReInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "异常玻璃处理20211214";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLcdReInput_FormClosing);
            this.Load += new System.EventHandler(this.frmLcdReInput_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtScanCode;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtInfo2;
        private System.Windows.Forms.TextBox txtScanCode2;
        private System.Windows.Forms.Label label2;
    }
}