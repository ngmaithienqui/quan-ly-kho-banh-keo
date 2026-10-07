namespace QLCuaHangBanhKeo
{
    partial class FXemPhieuNhapHang
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnCapNhatTrangThai;
        private System.Windows.Forms.ComboBox cbTrangThai;
        private System.Windows.Forms.GroupBox gbChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTietPN;
        private System.Windows.Forms.DataGridView dgvPhieuNhap;
        private System.Windows.Forms.ComboBox cbTrangThaiThanhToan;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;

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
            this.btnCapNhatTrangThai = new System.Windows.Forms.Button();
            this.cbTrangThai = new System.Windows.Forms.ComboBox();
            this.gbChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvChiTietPN = new System.Windows.Forms.DataGridView();
            this.dgvPhieuNhap = new System.Windows.Forms.DataGridView();
            this.cbTrangThaiThanhToan = new System.Windows.Forms.ComboBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCapNhatTrangThai
            // 
            this.btnCapNhatTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnCapNhatTrangThai.FlatAppearance.BorderSize = 0;
            this.btnCapNhatTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhatTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCapNhatTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatTrangThai.Location = new System.Drawing.Point(620, 580);
            this.btnCapNhatTrangThai.Name = "btnCapNhatTrangThai";
            this.btnCapNhatTrangThai.Size = new System.Drawing.Size(200, 35);
            this.btnCapNhatTrangThai.TabIndex = 5;
            this.btnCapNhatTrangThai.Text = "Cập nhật trạng thái";
            this.btnCapNhatTrangThai.UseVisualStyleBackColor = false;
            this.btnCapNhatTrangThai.Click += new System.EventHandler(this.btnCapNhatTrangThai_Click);
            // 
            // cbTrangThai
            // 
            this.cbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbTrangThai.FormattingEnabled = true;
            this.cbTrangThai.Items.AddRange(new object[] {
            "Chưa nhận hàng",
            "Đã nhận hàng",
            "Chờ xử lý"});
            this.cbTrangThai.Location = new System.Drawing.Point(80, 580);
            this.cbTrangThai.Name = "cbTrangThai";
            this.cbTrangThai.Size = new System.Drawing.Size(160, 31);
            this.cbTrangThai.TabIndex = 3;
            this.cbTrangThai.SelectedIndexChanged += new System.EventHandler(this.cbTrangThai_SelectedIndexChanged);
            // 
            // gbChiTiet
            // 
            this.gbChiTiet.BackColor = System.Drawing.Color.White;
            this.gbChiTiet.Controls.Add(this.dgvChiTietPN);
            this.gbChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.gbChiTiet.Location = new System.Drawing.Point(12, 360);
            this.gbChiTiet.Name = "gbChiTiet";
            this.gbChiTiet.Size = new System.Drawing.Size(834, 200);
            this.gbChiTiet.TabIndex = 2;
            this.gbChiTiet.TabStop = false;
            this.gbChiTiet.Text = "Chi tiết phiếu nhập hàng";
            // 
            // dgvChiTietPN
            // 
            this.dgvChiTietPN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietPN.Location = new System.Drawing.Point(6, 26);
            this.dgvChiTietPN.Name = "dgvChiTietPN";
            this.dgvChiTietPN.RowHeadersWidth = 51;
            this.dgvChiTietPN.RowTemplate.Height = 24;
            this.dgvChiTietPN.Size = new System.Drawing.Size(822, 168);
            this.dgvChiTietPN.TabIndex = 0;
            // 
            // dgvPhieuNhap
            // 
            this.dgvPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhieuNhap.Location = new System.Drawing.Point(12, 70);
            this.dgvPhieuNhap.Name = "dgvPhieuNhap";
            this.dgvPhieuNhap.RowHeadersWidth = 51;
            this.dgvPhieuNhap.RowTemplate.Height = 24;
            this.dgvPhieuNhap.Size = new System.Drawing.Size(834, 280);
            this.dgvPhieuNhap.TabIndex = 1;
            this.dgvPhieuNhap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhieuNhap_CellClick);
            // 
            // cbTrangThaiThanhToan
            // 
            this.cbTrangThaiThanhToan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrangThaiThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbTrangThaiThanhToan.FormattingEnabled = true;
            this.cbTrangThaiThanhToan.Items.AddRange(new object[] {
            "Chưa thanh toán",
            "Đã thanh toán"});
            this.cbTrangThaiThanhToan.Location = new System.Drawing.Point(340, 580);
            this.cbTrangThaiThanhToan.Name = "cbTrangThaiThanhToan";
            this.cbTrangThaiThanhToan.Size = new System.Drawing.Size(160, 31);
            this.cbTrangThaiThanhToan.TabIndex = 4;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(863, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(863, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHIẾU NHẬP HÀNG";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FXemPhieuNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(863, 630);
            this.Controls.Add(this.btnCapNhatTrangThai);
            this.Controls.Add(this.cbTrangThaiThanhToan);
            this.Controls.Add(this.cbTrangThai);
            this.Controls.Add(this.gbChiTiet);
            this.Controls.Add(this.dgvPhieuNhap);
            this.Controls.Add(this.panelHeader);
            this.Name = "FXemPhieuNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem Phiếu Nhập Hàng";
            this.Load += new System.EventHandler(this.FXemPhieuNhapHang_Load);
            this.gbChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
