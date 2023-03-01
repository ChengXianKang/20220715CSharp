namespace WindowsFormsApplication3
{
    partial class Form2
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
            this.btDelete = new System.Windows.Forms.Button();
            this.btMove = new System.Windows.Forms.Button();
            this.btSelFolder2 = new System.Windows.Forms.Button();
            this.btSelFolder1 = new System.Windows.Forms.Button();
            this.txtFolder2 = new System.Windows.Forms.TextBox();
            this.txtFolder1 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(551, 86);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(90, 23);
            this.btDelete.TabIndex = 7;
            this.btDelete.Text = "删除文件夹";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btMove
            // 
            this.btMove.Location = new System.Drawing.Point(455, 86);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(90, 23);
            this.btMove.TabIndex = 8;
            this.btMove.Text = "移动文件夹";
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // btSelFolder2
            // 
            this.btSelFolder2.Location = new System.Drawing.Point(648, 47);
            this.btSelFolder2.Name = "btSelFolder2";
            this.btSelFolder2.Size = new System.Drawing.Size(109, 23);
            this.btSelFolder2.TabIndex = 5;
            this.btSelFolder2.Text = "选择文件夹二";
            this.btSelFolder2.UseVisualStyleBackColor = true;
            this.btSelFolder2.Click += new System.EventHandler(this.btSelFolder2_Click);
            // 
            // btSelFolder1
            // 
            this.btSelFolder1.Location = new System.Drawing.Point(648, 11);
            this.btSelFolder1.Name = "btSelFolder1";
            this.btSelFolder1.Size = new System.Drawing.Size(109, 23);
            this.btSelFolder1.TabIndex = 6;
            this.btSelFolder1.Text = "选择文件夹一";
            this.btSelFolder1.UseVisualStyleBackColor = true;
            this.btSelFolder1.Click += new System.EventHandler(this.btSelFolder1_Click);
            // 
            // txtFolder2
            // 
            this.txtFolder2.Location = new System.Drawing.Point(11, 49);
            this.txtFolder2.Name = "txtFolder2";
            this.txtFolder2.Size = new System.Drawing.Size(629, 21);
            this.txtFolder2.TabIndex = 3;
            // 
            // txtFolder1
            // 
            this.txtFolder1.Location = new System.Drawing.Point(12, 12);
            this.txtFolder1.Name = "txtFolder1";
            this.txtFolder1.Size = new System.Drawing.Size(629, 21);
            this.txtFolder1.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 126);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btMove);
            this.Controls.Add(this.btSelFolder2);
            this.Controls.Add(this.btSelFolder1);
            this.Controls.Add(this.txtFolder2);
            this.Controls.Add(this.txtFolder1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Button btSelFolder2;
        private System.Windows.Forms.Button btSelFolder1;
        private System.Windows.Forms.TextBox txtFolder2;
        private System.Windows.Forms.TextBox txtFolder1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}