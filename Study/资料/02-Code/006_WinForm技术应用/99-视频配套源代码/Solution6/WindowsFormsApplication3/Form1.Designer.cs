namespace WindowsFormsApplication3
{
    partial class Form1
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
            this.txtFolder1 = new System.Windows.Forms.TextBox();
            this.txtFolder2 = new System.Windows.Forms.TextBox();
            this.btSelFolder1 = new System.Windows.Forms.Button();
            this.btSelFolder2 = new System.Windows.Forms.Button();
            this.btMove = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // txtFolder1
            // 
            this.txtFolder1.Location = new System.Drawing.Point(13, 13);
            this.txtFolder1.Name = "txtFolder1";
            this.txtFolder1.Size = new System.Drawing.Size(629, 21);
            this.txtFolder1.TabIndex = 0;
            // 
            // txtFolder2
            // 
            this.txtFolder2.Location = new System.Drawing.Point(12, 50);
            this.txtFolder2.Name = "txtFolder2";
            this.txtFolder2.Size = new System.Drawing.Size(629, 21);
            this.txtFolder2.TabIndex = 0;
            // 
            // btSelFolder1
            // 
            this.btSelFolder1.Location = new System.Drawing.Point(649, 12);
            this.btSelFolder1.Name = "btSelFolder1";
            this.btSelFolder1.Size = new System.Drawing.Size(109, 23);
            this.btSelFolder1.TabIndex = 1;
            this.btSelFolder1.Text = "选择文件夹一";
            this.btSelFolder1.UseVisualStyleBackColor = true;
            this.btSelFolder1.Click += new System.EventHandler(this.btSelFolder1_Click);
            // 
            // btSelFolder2
            // 
            this.btSelFolder2.Location = new System.Drawing.Point(649, 48);
            this.btSelFolder2.Name = "btSelFolder2";
            this.btSelFolder2.Size = new System.Drawing.Size(109, 23);
            this.btSelFolder2.TabIndex = 1;
            this.btSelFolder2.Text = "选择文件夹二";
            this.btSelFolder2.UseVisualStyleBackColor = true;
            this.btSelFolder2.Click += new System.EventHandler(this.btSelFolder2_Click);
            // 
            // btMove
            // 
            this.btMove.Location = new System.Drawing.Point(456, 87);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(90, 23);
            this.btMove.TabIndex = 2;
            this.btMove.Text = "移动文件夹";
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(552, 87);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(90, 23);
            this.btDelete.TabIndex = 2;
            this.btDelete.Text = "删除文件夹";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 132);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btMove);
            this.Controls.Add(this.btSelFolder2);
            this.Controls.Add(this.btSelFolder1);
            this.Controls.Add(this.txtFolder2);
            this.Controls.Add(this.txtFolder1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolder1;
        private System.Windows.Forms.TextBox txtFolder2;
        private System.Windows.Forms.Button btSelFolder1;
        private System.Windows.Forms.Button btSelFolder2;
        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

