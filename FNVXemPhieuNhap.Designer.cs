namespace QLCuaHangBanhKeo
{
    partial class FNVXemPhieuNhapHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.gbChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvChiTietPN = new System.Windows.Forms.DataGridView();
            this.dgvPhieuNhap = new System.Windows.Forms.DataGridView();
            this.btnNhapHangThanhCong = new System.Windows.Forms.Button();
            this.btnNhapHangThatBai = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbChiTiet
            // 
            this.gbChiTiet.Controls.Add(this.dgvChiTietPN);
            this.gbChiTiet.Location = new System.Drawing.Point(12, 351);
            this.gbChiTiet.Name = "gbChiTiet";
            this.gbChiTiet.Size = new System.Drawing.Size(834, 216);
            this.gbChiTiet.TabIndex = 5;
            this.gbChiTiet.TabStop = false;
            this.gbChiTiet.Text = "Chi tiết phiếu nhập hàng";
            // 
            // dgvChiTietPN
            // 
            this.dgvChiTietPN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietPN.Location = new System.Drawing.Point(6, 21);
            this.dgvChiTietPN.Name = "dgvChiTietPN";
            this.dgvChiTietPN.RowHeadersWidth = 51;
            this.dgvChiTietPN.RowTemplate.Height = 24;
            this.dgvChiTietPN.Size = new System.Drawing.Size(822, 195);
            this.dgvChiTietPN.TabIndex = 0;
            this.dgvChiTietPN.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietPN_CellContentClick);
            // 
            // dgvPhieuNhap
            // 
            this.dgvPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhieuNhap.Location = new System.Drawing.Point(12, 73);
            this.dgvPhieuNhap.Name = "dgvPhieuNhap";
            this.dgvPhieuNhap.RowHeadersWidth = 51;
            this.dgvPhieuNhap.RowTemplate.Height = 24;
            this.dgvPhieuNhap.Size = new System.Drawing.Size(834, 271);
            this.dgvPhieuNhap.TabIndex = 4;
            this.dgvPhieuNhap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhieuNhap_CellClick);
            // 
            // btnNhapHangThanhCong
            // 
            this.btnNhapHangThanhCong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnNhapHangThanhCong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnNhapHangThanhCong.ForeColor = System.Drawing.Color.Transparent;
            this.btnNhapHangThanhCong.Location = new System.Drawing.Point(113, 587);
            this.btnNhapHangThanhCong.Name = "btnNhapHangThanhCong";
            this.btnNhapHangThanhCong.Size = new System.Drawing.Size(227, 48);
            this.btnNhapHangThanhCong.TabIndex = 9;
            this.btnNhapHangThanhCong.Text = "Nhập hàng thành công";
            this.btnNhapHangThanhCong.UseVisualStyleBackColor = false;
            this.btnNhapHangThanhCong.Click += new System.EventHandler(this.btnNhapHangThanhCong_Click);
            // 
            // btnNhapHangThatBai
            // 
            this.btnNhapHangThatBai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnNhapHangThatBai.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnNhapHangThatBai.ForeColor = System.Drawing.Color.Transparent;
            this.btnNhapHangThatBai.Location = new System.Drawing.Point(547, 587);
            this.btnNhapHangThatBai.Name = "btnNhapHangThatBai";
            this.btnNhapHangThatBai.Size = new System.Drawing.Size(200, 48);
            this.btnNhapHangThatBai.TabIndex = 10;
            this.btnNhapHangThatBai.Text = "Nhập hàng thất bại";
            this.btnNhapHangThatBai.UseVisualStyleBackColor = false;
            this.btnNhapHangThatBai.Click += new System.EventHandler(this.btnNhapHangThatBai_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(866, 54);
            this.panelHeader.TabIndex = 15;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(264, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(346, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "XEM PHIẾU NHẬP HÀNG";
            // 
            // FNVXemPhieuNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 647);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.gbChiTiet);
            this.Controls.Add(this.dgvPhieuNhap);
            this.Controls.Add(this.btnNhapHangThanhCong);
            this.Controls.Add(this.btnNhapHangThatBai);
            this.Name = "FNVXemPhieuNhapHang";
            this.Text = "Xem Phiếu Nhập Hàng";
            this.Load += new System.EventHandler(this.FXemPhieuNhapHang_Load);
            this.gbChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox gbChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTietPN;
        private System.Windows.Forms.DataGridView dgvPhieuNhap;
        private System.Windows.Forms.Button btnNhapHangThanhCong;
        private System.Windows.Forms.Button btnNhapHangThatBai;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
    }
}
