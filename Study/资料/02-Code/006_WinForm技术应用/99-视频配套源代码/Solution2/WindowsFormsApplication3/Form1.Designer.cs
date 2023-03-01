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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.商品管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报表系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品出库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.月度报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.年度报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.商品管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.报表系统ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.商品入库ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.商品出库ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.月度报表ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.年度报表ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.商品查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.商品查询ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.商品盘点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品管理ToolStripMenuItem,
            this.报表系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 商品管理ToolStripMenuItem
            // 
            this.商品管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品入库ToolStripMenuItem,
            this.商品出库ToolStripMenuItem,
            this.toolStripSeparator2,
            this.商品查询ToolStripMenuItem1,
            this.商品盘点ToolStripMenuItem});
            this.商品管理ToolStripMenuItem.Name = "商品管理ToolStripMenuItem";
            this.商品管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.商品管理ToolStripMenuItem.Text = "商品管理";
            // 
            // 报表系统ToolStripMenuItem
            // 
            this.报表系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.月度报表ToolStripMenuItem,
            this.年度报表ToolStripMenuItem});
            this.报表系统ToolStripMenuItem.Name = "报表系统ToolStripMenuItem";
            this.报表系统ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.报表系统ToolStripMenuItem.Text = "报表系统";
            // 
            // 商品入库ToolStripMenuItem
            // 
            this.商品入库ToolStripMenuItem.Name = "商品入库ToolStripMenuItem";
            this.商品入库ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.商品入库ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.商品入库ToolStripMenuItem.Text = "商品入库";
            this.商品入库ToolStripMenuItem.Click += new System.EventHandler(this.商品入库ToolStripMenuItem_Click);
            // 
            // 商品出库ToolStripMenuItem
            // 
            this.商品出库ToolStripMenuItem.Name = "商品出库ToolStripMenuItem";
            this.商品出库ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.商品出库ToolStripMenuItem.Text = "商品出库";
            // 
            // 月度报表ToolStripMenuItem
            // 
            this.月度报表ToolStripMenuItem.Name = "月度报表ToolStripMenuItem";
            this.月度报表ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.月度报表ToolStripMenuItem.Text = "月度报表";
            // 
            // 年度报表ToolStripMenuItem
            // 
            this.年度报表ToolStripMenuItem.Name = "年度报表ToolStripMenuItem";
            this.年度报表ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.年度报表ToolStripMenuItem.Text = "年度报表";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品管理ToolStripMenuItem1,
            this.报表系统ToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // 商品管理ToolStripMenuItem1
            // 
            this.商品管理ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品入库ToolStripMenuItem1,
            this.商品出库ToolStripMenuItem1,
            this.商品查询ToolStripMenuItem});
            this.商品管理ToolStripMenuItem1.Name = "商品管理ToolStripMenuItem1";
            this.商品管理ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.商品管理ToolStripMenuItem1.Text = "商品管理";
            // 
            // 报表系统ToolStripMenuItem1
            // 
            this.报表系统ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.月度报表ToolStripMenuItem1,
            this.年度报表ToolStripMenuItem1});
            this.报表系统ToolStripMenuItem1.Name = "报表系统ToolStripMenuItem1";
            this.报表系统ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.报表系统ToolStripMenuItem1.Text = "报表系统";
            // 
            // 商品入库ToolStripMenuItem1
            // 
            this.商品入库ToolStripMenuItem1.Name = "商品入库ToolStripMenuItem1";
            this.商品入库ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.商品入库ToolStripMenuItem1.Text = "商品入库";
            this.商品入库ToolStripMenuItem1.Click += new System.EventHandler(this.商品入库ToolStripMenuItem_Click);
            // 
            // 商品出库ToolStripMenuItem1
            // 
            this.商品出库ToolStripMenuItem1.Name = "商品出库ToolStripMenuItem1";
            this.商品出库ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.商品出库ToolStripMenuItem1.Text = "商品出库";
            // 
            // 月度报表ToolStripMenuItem1
            // 
            this.月度报表ToolStripMenuItem1.Name = "月度报表ToolStripMenuItem1";
            this.月度报表ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.月度报表ToolStripMenuItem1.Text = "月度报表";
            // 
            // 年度报表ToolStripMenuItem1
            // 
            this.年度报表ToolStripMenuItem1.Name = "年度报表ToolStripMenuItem1";
            this.年度报表ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.年度报表ToolStripMenuItem1.Text = "年度报表";
            // 
            // 商品查询ToolStripMenuItem
            // 
            this.商品查询ToolStripMenuItem.Name = "商品查询ToolStripMenuItem";
            this.商品查询ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.商品查询ToolStripMenuItem.Text = "商品查询";
            this.商品查询ToolStripMenuItem.Click += new System.EventHandler(this.商品查询ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(823, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "商品入库";
            this.toolStripButton1.Click += new System.EventHandler(this.商品入库ToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton2.Text = "商品出库";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton3.Text = "月度报表";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // 商品查询ToolStripMenuItem1
            // 
            this.商品查询ToolStripMenuItem1.Name = "商品查询ToolStripMenuItem1";
            this.商品查询ToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.商品查询ToolStripMenuItem1.Text = "商品查询";
            // 
            // 商品盘点ToolStripMenuItem
            // 
            this.商品盘点ToolStripMenuItem.Name = "商品盘点ToolStripMenuItem";
            this.商品盘点ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.商品盘点ToolStripMenuItem.Text = "商品盘点";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 508);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(823, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel1.Text = "      ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 530);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 商品管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品出库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报表系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 月度报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 年度报表ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 商品管理ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 商品入库ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 商品出库ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 报表系统ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 月度报表ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 年度报表ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 商品查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 商品查询ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 商品盘点ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

