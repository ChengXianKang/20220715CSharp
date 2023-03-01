namespace Utils
{
    partial class ListViewNote
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListViewNote));
            this.imglNote = new System.Windows.Forms.ImageList(this.components);
            this.mnuNote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.colIdx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuCopyCode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglNote
            // 
            this.imglNote.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglNote.ImageStream")));
            this.imglNote.TransparentColor = System.Drawing.Color.Transparent;
            this.imglNote.Images.SetKeyName(0, "Error");
            this.imglNote.Images.SetKeyName(1, "NG");
            this.imglNote.Images.SetKeyName(2, "OK");
            this.imglNote.Images.SetKeyName(3, "Fail");
            this.imglNote.Images.SetKeyName(4, "Success");
            // 
            // mnuNote
            // 
            this.mnuNote.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mnuNote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopyCode,
            this.mnuCopy,
            this.toolStripSeparator1,
            this.mnuClear});
            this.mnuNote.Name = "mnuNote";
            this.mnuNote.Size = new System.Drawing.Size(135, 82);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(134, 24);
            this.mnuCopy.Text = "复制整行";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // mnuClear
            // 
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(134, 24);
            this.mnuClear.Text = "全部清除";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // colIdx
            // 
            this.colIdx.Text = "序号";
            this.colIdx.Width = -2;
            // 
            // colState
            // 
            this.colState.Text = "状态";
            this.colState.Width = -2;
            // 
            // colTime
            // 
            this.colTime.Text = "时间";
            this.colTime.Width = -2;
            // 
            // colCode
            // 
            this.colCode.Text = "数据";
            this.colCode.Width = 300;
            // 
            // colMessage
            // 
            this.colMessage.Text = "提示信息";
            this.colMessage.Width = -2;
            // 
            // mnuCopyCode
            // 
            this.mnuCopyCode.Name = "mnuCopyCode";
            this.mnuCopyCode.Size = new System.Drawing.Size(134, 24);
            this.mnuCopyCode.Text = "复制编码";
            this.mnuCopyCode.Click += new System.EventHandler(this.mnuCopyCode_Click);
            // 
            // ListViewNote
            // 
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdx,
            this.colState,
            this.colTime,
            this.colCode,
            this.colMessage});
            this.ContextMenuStrip = this.mnuNote;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FullRowSelect = true;
            this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.HideSelection = false;
            this.ShowItemToolTips = true;
            this.SmallImageList = this.imglNote;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.mnuNote.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imglNote;
        private System.Windows.Forms.ContextMenuStrip mnuNote;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuClear;
        private System.Windows.Forms.ColumnHeader colIdx;
        private System.Windows.Forms.ColumnHeader colState;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyCode;
    }
}
