namespace MyUI
{
    partial class FrmEdit
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.ckCode = new System.Windows.Forms.CheckBox();
            this.ckArt = new System.Windows.Forms.CheckBox();
            this.ckSport = new System.Windows.Forms.CheckBox();
            this.ckMusic = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbGirl = new System.Windows.Forms.RadioButton();
            this.rbBoy = new System.Windows.Forms.RadioButton();
            this.btUpdate = new System.Windows.Forms.Button();
            this.cmbPro = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ckCode);
            this.panel2.Controls.Add(this.ckArt);
            this.panel2.Controls.Add(this.ckSport);
            this.panel2.Controls.Add(this.ckMusic);
            this.panel2.Location = new System.Drawing.Point(59, 151);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(290, 34);
            this.panel2.TabIndex = 56;
            // 
            // ckCode
            // 
            this.ckCode.AutoSize = true;
            this.ckCode.Location = new System.Drawing.Point(168, 8);
            this.ckCode.Name = "ckCode";
            this.ckCode.Size = new System.Drawing.Size(48, 16);
            this.ckCode.TabIndex = 0;
            this.ckCode.Text = "编码";
            this.ckCode.UseVisualStyleBackColor = true;
            // 
            // ckArt
            // 
            this.ckArt.AutoSize = true;
            this.ckArt.Location = new System.Drawing.Point(114, 8);
            this.ckArt.Name = "ckArt";
            this.ckArt.Size = new System.Drawing.Size(48, 16);
            this.ckArt.TabIndex = 0;
            this.ckArt.Text = "绘画";
            this.ckArt.UseVisualStyleBackColor = true;
            // 
            // ckSport
            // 
            this.ckSport.AutoSize = true;
            this.ckSport.Location = new System.Drawing.Point(58, 8);
            this.ckSport.Name = "ckSport";
            this.ckSport.Size = new System.Drawing.Size(48, 16);
            this.ckSport.TabIndex = 0;
            this.ckSport.Text = "体育";
            this.ckSport.UseVisualStyleBackColor = true;
            // 
            // ckMusic
            // 
            this.ckMusic.AutoSize = true;
            this.ckMusic.Location = new System.Drawing.Point(4, 8);
            this.ckMusic.Name = "ckMusic";
            this.ckMusic.Size = new System.Drawing.Size(48, 16);
            this.ckMusic.TabIndex = 0;
            this.ckMusic.Text = "音乐";
            this.ckMusic.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbGirl);
            this.panel1.Controls.Add(this.rbBoy);
            this.panel1.Location = new System.Drawing.Point(59, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 34);
            this.panel1.TabIndex = 57;
            // 
            // rbGirl
            // 
            this.rbGirl.AutoSize = true;
            this.rbGirl.Location = new System.Drawing.Point(55, 7);
            this.rbGirl.Name = "rbGirl";
            this.rbGirl.Size = new System.Drawing.Size(35, 16);
            this.rbGirl.TabIndex = 0;
            this.rbGirl.Text = "女";
            this.rbGirl.UseVisualStyleBackColor = true;
            // 
            // rbBoy
            // 
            this.rbBoy.AutoSize = true;
            this.rbBoy.Location = new System.Drawing.Point(4, 8);
            this.rbBoy.Name = "rbBoy";
            this.rbBoy.Size = new System.Drawing.Size(35, 16);
            this.rbBoy.TabIndex = 0;
            this.rbBoy.Text = "男";
            this.rbBoy.UseVisualStyleBackColor = true;
            // 
            // btUpdate
            // 
            this.btUpdate.Location = new System.Drawing.Point(63, 225);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(75, 23);
            this.btUpdate.TabIndex = 55;
            this.btUpdate.Text = "修  改";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // cmbPro
            // 
            this.cmbPro.FormattingEnabled = true;
            this.cmbPro.Location = new System.Drawing.Point(64, 199);
            this.cmbPro.Name = "cmbPro";
            this.cmbPro.Size = new System.Drawing.Size(121, 20);
            this.cmbPro.TabIndex = 54;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 53;
            this.label4.Text = "专业：";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(63, 78);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(196, 21);
            this.txtAge.TabIndex = 50;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(63, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(196, 21);
            this.txtName.TabIndex = 51;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(64, 13);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(195, 21);
            this.txtId.TabIndex = 52;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 45;
            this.label6.Text = "爱好：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "性别：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 47;
            this.label3.Text = "年龄：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 48;
            this.label2.Text = "姓名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "学号：";
            // 
            // FrmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 264);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.cmbPro);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmEdit";
            this.Text = "FrmEdit";
            this.Load += new System.EventHandler(this.FrmEdit_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox ckCode;
        private System.Windows.Forms.CheckBox ckArt;
        private System.Windows.Forms.CheckBox ckSport;
        private System.Windows.Forms.CheckBox ckMusic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbGirl;
        private System.Windows.Forms.RadioButton rbBoy;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.ComboBox cmbPro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}