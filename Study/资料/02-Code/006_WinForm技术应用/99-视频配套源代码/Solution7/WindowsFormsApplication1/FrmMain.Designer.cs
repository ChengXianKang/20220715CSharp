﻿namespace WindowsFormsApplication1
{
    partial class FrmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.普通模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单例模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.普通模式ToolStripMenuItem,
            this.单例模式ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(809, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 普通模式ToolStripMenuItem
            // 
            this.普通模式ToolStripMenuItem.Name = "普通模式ToolStripMenuItem";
            this.普通模式ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.普通模式ToolStripMenuItem.Text = "普通模式";
            this.普通模式ToolStripMenuItem.Click += new System.EventHandler(this.普通模式ToolStripMenuItem_Click);
            // 
            // 单例模式ToolStripMenuItem
            // 
            this.单例模式ToolStripMenuItem.Name = "单例模式ToolStripMenuItem";
            this.单例模式ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.单例模式ToolStripMenuItem.Text = "单例模式";
            this.单例模式ToolStripMenuItem.Click += new System.EventHandler(this.单例模式ToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 543);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 普通模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单例模式ToolStripMenuItem;
    }
}

