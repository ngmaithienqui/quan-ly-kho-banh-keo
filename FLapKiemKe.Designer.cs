using System;

namespace QLCuaHangBanhKeo
{
    partial class FLapKiemKe
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPhieuKiemKe = new System.Windows.Forms.Label();
            this.dgv_LapPhieuKiemKe = new System.Windows.Forms.DataGridView();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLyDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gBThongTinLapPhieu = new System.Windows.Forms.GroupBox();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.txtNguoiLapPhieu = new System.Windows.Forms.TextBox();
            this.lblNguoiLapPhieu = new System.Windows.Forms.Label();
            this.dtpNgayThanhToan = new System.Windows.Forms.DateTimePicker();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lBTimKiemSP = new System.Windows.Forms.ListBox();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LapPhieuKiemKe)).BeginInit();
            this.gBThongTinLapPhieu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnDong);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 600);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(701, 60);
            this.panel4.TabIndex = 7;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(281, 9);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 35);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(188)))), ((int)(((byte)(212)))));
            this.panel1.Controls.Add(this.lblPhieuKiemKe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 60);
            this.panel1.TabIndex = 5;
            // 
            // lblPhieuKiemKe
            // 
            this.lblPhieuKiemKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblPhieuKiemKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhieuKiemKe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhieuKiemKe.ForeColor = System.Drawing.Color.White;
            this.lblPhieuKiemKe.Location = new System.Drawing.Point(0, 0);
            this.lblPhieuKiemKe.Name = "lblPhieuKiemKe";
            this.lblPhieuKiemKe.Size = new System.Drawing.Size(701, 60);
            this.lblPhieuKiemKe.TabIndex = 0;
            this.lblPhieuKiemKe.Text = "PHIẾU KIỂM KÊ";
            this.lblPhieuKiemKe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv_LapPhieuKiemKe
            // 
            this.dgv_LapPhieuKiemKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_LapPhieuKiemKe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSP,
            this.colTenSP,
            this.colSoLuongSP,
            this.colSoLuongThucTe,
            this.colLyDo});
            this.dgv_LapPhieuKiemKe.Location = new System.Drawing.Point(12, 283);
            this.dgv_LapPhieuKiemKe.Name = "dgv_LapPhieuKiemKe";
            this.dgv_LapPhieuKiemKe.RowHeadersWidth = 51;
            this.dgv_LapPhieuKiemKe.RowTemplate.Height = 24;
            this.dgv_LapPhieuKiemKe.Size = new System.Drawing.Size(677, 320);
            this.dgv_LapPhieuKiemKe.TabIndex = 8;
            this.dgv_LapPhieuKiemKe.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dgv_LapPhieuKiemKe.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_LapPhieuKiemKe_CellFormatting);
            // 
            // colMaSP
            // 
            this.colMaSP.HeaderText = "Mã sản phẩm";
            this.colMaSP.MinimumWidth = 10;
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.Width = 125;
            // 
            // colTenSP
            // 
            this.colTenSP.HeaderText = "Tên sản phẩm";
            this.colTenSP.MinimumWidth = 10;
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.ReadOnly = true;
            this.colTenSP.Width = 125;
            // 
            // colSoLuongSP
            // 
            this.colSoLuongSP.HeaderText = "Số lượng sản phẩm";
            this.colSoLuongSP.MinimumWidth = 10;
            this.colSoLuongSP.Name = "colSoLuongSP";
            this.colSoLuongSP.ReadOnly = true;
            this.colSoLuongSP.Width = 125;
            // 
            // colSoLuongThucTe
            // 
            this.colSoLuongThucTe.HeaderText = "Số lượng thực tế";
            this.colSoLuongThucTe.MinimumWidth = 6;
            this.colSoLuongThucTe.Name = "colSoLuongThucTe";
            this.colSoLuongThucTe.Width = 125;
            // 
            // colLyDo
            // 
            this.colLyDo.HeaderText = "Lý do sai lệch";
            this.colLyDo.MinimumWidth = 6;
            this.colLyDo.Name = "colLyDo";
            this.colLyDo.Width = 125;
            // 
            // gBThongTinLapPhieu
            // 
            this.gBThongTinLapPhieu.Controls.Add(this.txtNguoiLapPhieu);
            this.gBThongTinLapPhieu.Controls.Add(this.lblNguoiLapPhieu);
            this.gBThongTinLapPhieu.Controls.Add(this.dtpNgayThanhToan);
            this.gBThongTinLapPhieu.Controls.Add(this.lblNgayLap);
            this.gBThongTinLapPhieu.Location = new System.Drawing.Point(4, 66);
            this.gBThongTinLapPhieu.Name = "gBThongTinLapPhieu";
            this.gBThongTinLapPhieu.Size = new System.Drawing.Size(512, 147);
            this.gBThongTinLapPhieu.TabIndex = 9;
            this.gBThongTinLapPhieu.TabStop = false;
            this.gBThongTinLapPhieu.Text = "Thông tin lập phiếu";
            this.gBThongTinLapPhieu.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnLuuPhieu
            // 
            this.btnLuuPhieu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLuuPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLuuPhieu.FlatAppearance.BorderSize = 0;
            this.btnLuuPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuPhieu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Location = new System.Drawing.Point(559, 178);
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Size = new System.Drawing.Size(110, 35);
            this.btnLuuPhieu.TabIndex = 4;
            this.btnLuuPhieu.Text = "Lưu phiếu";
            this.btnLuuPhieu.UseVisualStyleBackColor = false;
            this.btnLuuPhieu.Click += new System.EventHandler(this.btnLuuPhieu_Click);
            // 
            // txtNguoiLapPhieu
            // 
            this.txtNguoiLapPhieu.Location = new System.Drawing.Point(182, 98);
            this.txtNguoiLapPhieu.Name = "txtNguoiLapPhieu";
            this.txtNguoiLapPhieu.Size = new System.Drawing.Size(252, 22);
            this.txtNguoiLapPhieu.TabIndex = 4;
            // 
            // lblNguoiLapPhieu
            // 
            this.lblNguoiLapPhieu.AutoSize = true;
            this.lblNguoiLapPhieu.Location = new System.Drawing.Point(0, 101);
            this.lblNguoiLapPhieu.Name = "lblNguoiLapPhieu";
            this.lblNguoiLapPhieu.Size = new System.Drawing.Size(151, 16);
            this.lblNguoiLapPhieu.TabIndex = 3;
            this.lblNguoiLapPhieu.Text = "Người lập phiếu kiểm kê";
            // 
            // dtpNgayThanhToan
            // 
            this.dtpNgayThanhToan.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayThanhToan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayThanhToan.Location = new System.Drawing.Point(182, 44);
            this.dtpNgayThanhToan.Name = "dtpNgayThanhToan";
            this.dtpNgayThanhToan.Size = new System.Drawing.Size(109, 22);
            this.dtpNgayThanhToan.TabIndex = 2;
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(0, 50);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(148, 16);
            this.lblNgayLap.TabIndex = 0;
            this.lblNgayLap.Text = "Ngày lập phiếu kiểm kê";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(73, 221);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 23);
            this.label11.TabIndex = 60;
            this.label11.Text = "Tìm kiếm ";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(186, 219);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(249, 22);
            this.txtTimKiem.TabIndex = 59;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // lBTimKiemSP
            // 
            this.lBTimKiemSP.FormattingEnabled = true;
            this.lBTimKiemSP.ItemHeight = 16;
            this.lBTimKiemSP.Location = new System.Drawing.Point(186, 236);
            this.lBTimKiemSP.Name = "lBTimKiemSP";
            this.lBTimKiemSP.Size = new System.Drawing.Size(248, 212);
            this.lBTimKiemSP.TabIndex = 61;
            this.lBTimKiemSP.Visible = false;
            this.lBTimKiemSP.SelectedIndexChanged += new System.EventHandler(this.lBTimKiemSP_SelectedIndexChanged);
            // 
            // FLapKiemKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 660);
            this.Controls.Add(this.btnLuuPhieu);
            this.Controls.Add(this.lBTimKiemSP);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.gBThongTinLapPhieu);
            this.Controls.Add(this.dgv_LapPhieuKiemKe);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "FLapKiemKe";
            this.Text = "FKiemKe";
            this.Load += new System.EventHandler(this.FLapKiemKe_Load);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LapPhieuKiemKe)).EndInit();
            this.gBThongTinLapPhieu.ResumeLayout(false);
            this.gBThongTinLapPhieu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPhieuKiemKe;
        private System.Windows.Forms.DataGridView dgv_LapPhieuKiemKe;
        private System.Windows.Forms.GroupBox gBThongTinLapPhieu;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.TextBox txtNguoiLapPhieu;
        private System.Windows.Forms.Label lblNguoiLapPhieu;
        private System.Windows.Forms.DateTimePicker dtpNgayThanhToan;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongThucTe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyDo;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.ListBox lBTimKiemSP;
    }
}