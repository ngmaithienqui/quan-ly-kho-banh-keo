namespace QLCuaHangBanhKeo
{
    partial class FNVXuatHangThatBai
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
            this.txtGhiChuThieu = new System.Windows.Forms.TextBox();
            this.numThieuHang = new System.Windows.Forms.NumericUpDown();
            this.cbThieuHang = new System.Windows.Forms.CheckBox();
            this.txtGhiChuHuHong = new System.Windows.Forms.TextBox();
            this.cbHuHong = new System.Windows.Forms.CheckBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.numHangHuHong = new System.Windows.Forms.NumericUpDown();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numThieuHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHangHuHong)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGhiChuThieu
            // 
            this.txtGhiChuThieu.Location = new System.Drawing.Point(437, 102);
            this.txtGhiChuThieu.Name = "txtGhiChuThieu";
            this.txtGhiChuThieu.Size = new System.Drawing.Size(232, 22);
            this.txtGhiChuThieu.TabIndex = 6;
            // 
            // numThieuHang
            // 
            this.numThieuHang.Location = new System.Drawing.Point(205, 177);
            this.numThieuHang.Name = "numThieuHang";
            this.numThieuHang.Size = new System.Drawing.Size(120, 22);
            this.numThieuHang.TabIndex = 5;
            this.numThieuHang.ValueChanged += new System.EventHandler(this.numThieuHang_ValueChanged);
            // 
            // cbThieuHang
            // 
            this.cbThieuHang.AutoSize = true;
            this.cbThieuHang.Location = new System.Drawing.Point(40, 102);
            this.cbThieuHang.Name = "cbThieuHang";
            this.cbThieuHang.Size = new System.Drawing.Size(96, 20);
            this.cbThieuHang.TabIndex = 4;
            this.cbThieuHang.Text = "Thiếu hàng";
            this.cbThieuHang.UseVisualStyleBackColor = true;
            this.cbThieuHang.CheckedChanged += new System.EventHandler(this.cbThieuHang_CheckedChanged);
            // 
            // txtGhiChuHuHong
            // 
            this.txtGhiChuHuHong.Location = new System.Drawing.Point(437, 167);
            this.txtGhiChuHuHong.Name = "txtGhiChuHuHong";
            this.txtGhiChuHuHong.Size = new System.Drawing.Size(232, 22);
            this.txtGhiChuHuHong.TabIndex = 11;
            // 
            // cbHuHong
            // 
            this.cbHuHong.AutoSize = true;
            this.cbHuHong.Location = new System.Drawing.Point(40, 178);
            this.cbHuHong.Name = "cbHuHong";
            this.cbHuHong.Size = new System.Drawing.Size(112, 20);
            this.cbHuHong.TabIndex = 10;
            this.cbHuHong.Text = "Hàng hủ hỏng";
            this.cbHuHong.UseVisualStyleBackColor = true;
            this.cbHuHong.CheckedChanged += new System.EventHandler(this.cbHuHong_CheckedChanged);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.Transparent;
            this.btnXacNhan.Location = new System.Drawing.Point(256, 249);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(146, 48);
            this.btnXacNhan.TabIndex = 12;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // numHangHuHong
            // 
            this.numHangHuHong.Location = new System.Drawing.Point(205, 103);
            this.numHangHuHong.Name = "numHangHuHong";
            this.numHangHuHong.Size = new System.Drawing.Size(120, 22);
            this.numHangHuHong.TabIndex = 13;
            this.numHangHuHong.ValueChanged += new System.EventHandler(this.numHangHuHong_ValueChanged);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(690, 54);
            this.panelHeader.TabIndex = 14;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(138, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(406, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LÝ DO XUẤT HÀNG THẤT BẠI";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // FNVXuatHangThatBai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 321);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.numHangHuHong);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtGhiChuHuHong);
            this.Controls.Add(this.cbHuHong);
            this.Controls.Add(this.txtGhiChuThieu);
            this.Controls.Add(this.numThieuHang);
            this.Controls.Add(this.cbThieuHang);
            this.Name = "FNVXuatHangThatBai";
            this.Text = "FNVXuatHangThatBai";
            this.Load += new System.EventHandler(this.FNVXuatHangThatBai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numThieuHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHangHuHong)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGhiChuThieu;
        private System.Windows.Forms.NumericUpDown numThieuHang;
        private System.Windows.Forms.CheckBox cbThieuHang;
        private System.Windows.Forms.TextBox txtGhiChuHuHong;
        private System.Windows.Forms.CheckBox cbHuHong;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.NumericUpDown numHangHuHong;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
    }
}