namespace WindowsFormsApplication3
{
    partial class Form3
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
            this.btSelFolder2 = new System.Windows.Forms.Button();
            this.btSelFolder1 = new System.Windows.Forms.Button();
            this.txtFolder2 = new System.Windows.Forms.TextBox();
            this.txtFolder1 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btCopy = new System.Windows.Forms.Button();
            this.btMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSelFolder2
            // 
            this.btSelFolder2.Location = new System.Drawing.Point(648, 47);
            this.btSelFolder2.Name = "btSelFolder2";
            this.btSelFolder2.Size = new System.Drawing.Size(109, 23);
            this.btSelFolder2.TabIndex = 9;
            this.btSelFolder2.Text = "选择文件夹二";
            this.btSelFolder2.UseVisualStyleBackColor = true;
            this.btSelFolder2.Click += new System.EventHandler(this.btSelFolder2_Click);
            // 
            // btSelFolder1
            // 
            this.btSelFolder1.Location = new System.Drawing.Point(648, 11);
            this.btSelFolder1.Name = "btSelFolder1";
            this.btSelFolder1.Size = new System.Drawing.Size(109, 23);
            this.btSelFolder1.TabIndex = 10;
            this.btSelFolder1.Text = "选择文件夹一";
            this.btSelFolder1.UseVisualStyleBackColor = true;
            this.btSelFolder1.Click += new System.EventHandler(this.btSelFolder1_Click);
            // 
            // txtFolder2
            // 
            this.txtFolder2.Location = new System.Drawing.Point(11, 49);
            this.txtFolder2.Name = "txtFolder2";
            this.txtFolder2.Size = new System.Drawing.Size(629, 21);
            this.txtFolder2.TabIndex = 7;
            // 
            // txtFolder1
            // 
            this.txtFolder1.Location = new System.Drawing.Point(12, 12);
            this.txtFolder1.Name = "txtFolder1";
            this.txtFolder1.Size = new System.Drawing.Size(629, 21);
            this.txtFolder1.TabIndex = 8;
            // 
            // btCopy
            // 
            this.btCopy.Location = new System.Drawing.Point(473, 88);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(75, 23);
            this.btCopy.TabIndex = 11;
            this.btCopy.Text = "复制文件夹";
            this.btCopy.UseVisualStyleBackColor = true;
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // btMove
            // 
            this.btMove.Location = new System.Drawing.Point(565, 88);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(75, 23);
            this.btMove.TabIndex = 11;
            this.btMove.Text = "移动文件夹";
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 129);
            this.Controls.Add(this.btMove);
            this.Controls.Add(this.btCopy);
            this.Controls.Add(this.btSelFolder2);
            this.Controls.Add(this.btSelFolder1);
            this.Controls.Add(this.txtFolder2);
            this.Controls.Add(this.txtFolder1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSelFolder2;
        private System.Windows.Forms.Button btSelFolder1;
        private System.Windows.Forms.TextBox txtFolder2;
        private System.Windows.Forms.TextBox txtFolder1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btCopy;
        private System.Windows.Forms.Button btMove;
    }
}