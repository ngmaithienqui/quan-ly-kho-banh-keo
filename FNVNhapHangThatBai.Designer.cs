namespace QLCuaHangBanhKeo
{
    partial class FNVNhapHangThatBai
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
            this.cbThieuHang = new System.Windows.Forms.CheckBox();
            this.cbDuHang = new System.Windows.Forms.CheckBox();
            this.numThieuHang = new System.Windows.Forms.NumericUpDown();
            this.txtGhiChuThieu = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.cbHuHong = new System.Windows.Forms.CheckBox();
            this.cbNhapSai = new System.Windows.Forms.CheckBox();
            this.numDuHang = new System.Windows.Forms.NumericUpDown();
            this.txtGhiChuDu = new System.Windows.Forms.TextBox();
            this.txtGhiChuHuHong = new System.Windows.Forms.TextBox();
            this.txtGhiChuNhapSai = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numThieuHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuHang)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbThieuHang
            // 
            this.cbThieuHang.AutoSize = true;
            this.cbThieuHang.Location = new System.Drawing.Point(45, 82);
            this.cbThieuHang.Name = "cbThieuHang";
            this.cbThieuHang.Size = new System.Drawing.Size(96, 20);
            this.cbThieuHang.TabIndex = 0;
            this.cbThieuHang.Text = "Thiếu hàng";
            this.cbThieuHang.UseVisualStyleBackColor = true;
            this.cbThieuHang.CheckedChanged += new System.EventHandler(this.cbThieuHang_CheckedChanged_1);
            // 
            // cbDuHang
            // 
            this.cbDuHang.AutoSize = true;
            this.cbDuHang.Location = new System.Drawing.Point(45, 145);
            this.cbDuHang.Name = "cbDuHang";
            this.cbDuHang.Size = new System.Drawing.Size(79, 20);
            this.cbDuHang.TabIndex = 1;
            this.cbDuHang.Text = "Dư hàng";
            this.cbDuHang.UseVisualStyleBackColor = true;
            this.cbDuHang.CheckedChanged += new System.EventHandler(this.cbDuHang_CheckedChanged_1);
            // 
            // numThieuHang
            // 
            this.numThieuHang.Location = new System.Drawing.Point(258, 82);
            this.numThieuHang.Name = "numThieuHang";
            this.numThieuHang.Size = new System.Drawing.Size(120, 22);
            this.numThieuHang.TabIndex = 2;
            // 
            // txtGhiChuThieu
            // 
            this.txtGhiChuThieu.Location = new System.Drawing.Point(492, 81);
            this.txtGhiChuThieu.Name = "txtGhiChuThieu";
            this.txtGhiChuThieu.Size = new System.Drawing.Size(232, 22);
            this.txtGhiChuThieu.TabIndex = 3;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.Color.Transparent;
            this.btnXacNhan.Location = new System.Drawing.Point(317, 365);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(138, 58);
            this.btnXacNhan.TabIndex = 4;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // cbHuHong
            // 
            this.cbHuHong.AutoSize = true;
            this.cbHuHong.Location = new System.Drawing.Point(45, 218);
            this.cbHuHong.Name = "cbHuHong";
            this.cbHuHong.Size = new System.Drawing.Size(112, 20);
            this.cbHuHong.TabIndex = 5;
            this.cbHuHong.Text = "Hàng hủ hỏng";
            this.cbHuHong.UseVisualStyleBackColor = true;
            this.cbHuHong.CheckedChanged += new System.EventHandler(this.cbHuHong_CheckedChanged_1);
            // 
            // cbNhapSai
            // 
            this.cbNhapSai.AutoSize = true;
            this.cbNhapSai.Location = new System.Drawing.Point(45, 297);
            this.cbNhapSai.Name = "cbNhapSai";
            this.cbNhapSai.Size = new System.Drawing.Size(116, 20);
            this.cbNhapSai.TabIndex = 6;
            this.cbNhapSai.Text = "Nhập sai hàng";
            this.cbNhapSai.UseVisualStyleBackColor = true;
            this.cbNhapSai.CheckedChanged += new System.EventHandler(this.cbNhapSai_CheckedChanged_1);
            // 
            // numDuHang
            // 
            this.numDuHang.Location = new System.Drawing.Point(257, 145);
            this.numDuHang.Name = "numDuHang";
            this.numDuHang.Size = new System.Drawing.Size(120, 22);
            this.numDuHang.TabIndex = 7;
            // 
            // txtGhiChuDu
            // 
            this.txtGhiChuDu.Location = new System.Drawing.Point(492, 143);
            this.txtGhiChuDu.Name = "txtGhiChuDu";
            this.txtGhiChuDu.Size = new System.Drawing.Size(232, 22);
            this.txtGhiChuDu.TabIndex = 8;
            // 
            // txtGhiChuHuHong
            // 
            this.txtGhiChuHuHong.Location = new System.Drawing.Point(492, 216);
            this.txtGhiChuHuHong.Name = "txtGhiChuHuHong";
            this.txtGhiChuHuHong.Size = new System.Drawing.Size(232, 22);
            this.txtGhiChuHuHong.TabIndex = 9;
            // 
            // txtGhiChuNhapSai
            // 
            this.txtGhiChuNhapSai.Location = new System.Drawing.Point(492, 297);
            this.txtGhiChuNhapSai.Name = "txtGhiChuNhapSai";
            this.txtGhiChuNhapSai.Size = new System.Drawing.Size(232, 22);
            this.txtGhiChuNhapSai.TabIndex = 10;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(763, 54);
            this.panelHeader.TabIndex = 15;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(165, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(412, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LÝ DO NHẬP HÀNG THẤT BẠI";
            // 
            // FNVNhapHangThatBai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 450);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.txtGhiChuNhapSai);
            this.Controls.Add(this.txtGhiChuHuHong);
            this.Controls.Add(this.txtGhiChuDu);
            this.Controls.Add(this.numDuHang);
            this.Controls.Add(this.cbNhapSai);
            this.Controls.Add(this.cbHuHong);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtGhiChuThieu);
            this.Controls.Add(this.numThieuHang);
            this.Controls.Add(this.cbDuHang);
            this.Controls.Add(this.cbThieuHang);
            this.Name = "FNVNhapHangThatBai";
            this.Text = "FNVNhapHangThatBai";
            this.Load += new System.EventHandler(this.FNVNhapHangThatBai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numThieuHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuHang)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbThieuHang;
        private System.Windows.Forms.CheckBox cbDuHang;
        private System.Windows.Forms.NumericUpDown numThieuHang;
        private System.Windows.Forms.TextBox txtGhiChuThieu;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.CheckBox cbHuHong;
        private System.Windows.Forms.CheckBox cbNhapSai;
        private System.Windows.Forms.NumericUpDown numDuHang;
        private System.Windows.Forms.TextBox txtGhiChuDu;
        private System.Windows.Forms.TextBox txtGhiChuHuHong;
        private System.Windows.Forms.TextBox txtGhiChuNhapSai;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
    }
}