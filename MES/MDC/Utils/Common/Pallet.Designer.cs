namespace Utils.Common
{
    partial class Pallet
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
            this.tlpStore = new System.Windows.Forms.TableLayoutPanel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.tlpStore.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpStore
            // 
            this.tlpStore.BackColor = System.Drawing.Color.Transparent;
            this.tlpStore.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpStore.ColumnCount = 2;
            this.tlpStore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpStore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStore.Controls.Add(this.lblCaption, 0, 0);
            this.tlpStore.Controls.Add(this.lblState, 0, 0);
            this.tlpStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStore.Location = new System.Drawing.Point(0, 0);
            this.tlpStore.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStore.Name = "tlpStore";
            this.tlpStore.RowCount = 1;
            this.tlpStore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStore.Size = new System.Drawing.Size(100, 20);
            this.tlpStore.TabIndex = 1;
            // 
            // lblCaption
            // 
            this.lblCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaption.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblCaption.Location = new System.Drawing.Point(10, 1);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(89, 18);
            this.lblCaption.TabIndex = 6;
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblState
            // 
            this.lblState.BackColor = System.Drawing.Color.Transparent;
            this.lblState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblState.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.lblState.ImageKey = "(无)";
            this.lblState.Location = new System.Drawing.Point(1, 1);
            this.lblState.Margin = new System.Windows.Forms.Padding(0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(8, 18);
            this.lblState.TabIndex = 5;
            // 
            // Pallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpStore);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Pallet";
            this.Size = new System.Drawing.Size(100, 20);
            this.tlpStore.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpStore;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblCaption;
    }
}
