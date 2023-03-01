namespace WindowsFormsApplication2
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
            this.btCopy = new System.Windows.Forms.Button();
            this.txtOpenFolder = new System.Windows.Forms.Button();
            this.btOpenFile = new System.Windows.Forms.Button();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(445, 66);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 23);
            this.btDelete.TabIndex = 7;
            this.btDelete.Text = "删除文件";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btMove
            // 
            this.btMove.Location = new System.Drawing.Point(364, 66);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(75, 23);
            this.btMove.TabIndex = 8;
            this.btMove.Text = "移动文件";
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // btCopy
            // 
            this.btCopy.Location = new System.Drawing.Point(283, 66);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(75, 23);
            this.btCopy.TabIndex = 9;
            this.btCopy.Text = "复制文件";
            this.btCopy.UseVisualStyleBackColor = true;
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // txtOpenFolder
            // 
            this.txtOpenFolder.Location = new System.Drawing.Point(530, 38);
            this.txtOpenFolder.Name = "txtOpenFolder";
            this.txtOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.txtOpenFolder.TabIndex = 5;
            this.txtOpenFolder.Text = "选择文件夹";
            this.txtOpenFolder.UseVisualStyleBackColor = true;
            this.txtOpenFolder.Click += new System.EventHandler(this.txtOpenFolder_Click);
            // 
            // btOpenFile
            // 
            this.btOpenFile.Location = new System.Drawing.Point(530, 11);
            this.btOpenFile.Name = "btOpenFile";
            this.btOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btOpenFile.TabIndex = 6;
            this.btOpenFile.Text = "选择文件";
            this.btOpenFile.UseVisualStyleBackColor = true;
            this.btOpenFile.Click += new System.EventHandler(this.btOpenFile_Click);
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(11, 39);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(511, 21);
            this.txtFolderName.TabIndex = 3;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(12, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(511, 21);
            this.txtFileName.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 105);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btMove);
            this.Controls.Add(this.btCopy);
            this.Controls.Add(this.txtOpenFolder);
            this.Controls.Add(this.btOpenFile);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.txtFileName);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Button btCopy;
        private System.Windows.Forms.Button txtOpenFolder;
        private System.Windows.Forms.Button btOpenFile;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}