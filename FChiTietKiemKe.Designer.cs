using System;

namespace QLCuaHangBanhKeo
{
    partial class FChiTietKiemKe
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
            this.dgv_ChiTietKiemKe = new System.Windows.Forms.DataGridView();
            this.colMaChiTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSLSanPhamThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoSPSaiLech = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLyDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblChiTietPhieuKiemKe = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChiTietKiemKe)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_ChiTietKiemKe
            // 
            this.dgv_ChiTietKiemKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ChiTietKiemKe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaChiTiet,
            this.colMaPhieu,
            this.colMaSP,
            this.colTenSP,
            this.colSLSanPhamThuc,
            this.colSoSPSaiLech,
            this.colLyDo});
            this.dgv_ChiTietKiemKe.Location = new System.Drawing.Point(12, 86);
            this.dgv_ChiTietKiemKe.Name = "dgv_ChiTietKiemKe";
            this.dgv_ChiTietKiemKe.RowHeadersWidth = 51;
            this.dgv_ChiTietKiemKe.RowTemplate.Height = 24;
            this.dgv_ChiTietKiemKe.Size = new System.Drawing.Size(928, 289);
            this.dgv_ChiTietKiemKe.TabIndex = 0;
            this.dgv_ChiTietKiemKe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ChiTietKiemKe_CellContentClick);
            // 
            // colMaChiTiet
            // 
            this.colMaChiTiet.DataPropertyName = "MaChiTiet";
            this.colMaChiTiet.HeaderText = "Mã chi tiết";
            this.colMaChiTiet.MinimumWidth = 6;
            this.colMaChiTiet.Name = "colMaChiTiet";
            this.colMaChiTiet.ReadOnly = true;
            this.colMaChiTiet.Width = 125;
            // 
            // colMaPhieu
            // 
            this.colMaPhieu.DataPropertyName = "MaPhieu";
            this.colMaPhieu.HeaderText = "Mã phiếu kiểm kê";
            this.colMaPhieu.MinimumWidth = 6;
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.ReadOnly = true;
            this.colMaPhieu.Width = 125;
            // 
            // colMaSP
            // 
            this.colMaSP.DataPropertyName = "MaSP";
            this.colMaSP.HeaderText = "Mã Sản Phẩm";
            this.colMaSP.MinimumWidth = 6;
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.ReadOnly = true;
            this.colMaSP.Width = 125;
            // 
            // colTenSP
            // 
            this.colTenSP.DataPropertyName = "TenSP";
            this.colTenSP.HeaderText = "Tên sản phẩm";
            this.colTenSP.MinimumWidth = 6;
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.ReadOnly = true;
            this.colTenSP.Width = 125;
            // 
            // colSLSanPhamThuc
            // 
            this.colSLSanPhamThuc.DataPropertyName = "SoLuongSPThuc";
            this.colSLSanPhamThuc.HeaderText = "Số lượng sản phẩm thực tế";
            this.colSLSanPhamThuc.MinimumWidth = 6;
            this.colSLSanPhamThuc.Name = "colSLSanPhamThuc";
            this.colSLSanPhamThuc.ReadOnly = true;
            this.colSLSanPhamThuc.Width = 125;
            // 
            // colSoSPSaiLech
            // 
            this.colSoSPSaiLech.DataPropertyName = "SoLuongSPSaiLech";
            this.colSoSPSaiLech.HeaderText = "Số sản phẩm sai lệch";
            this.colSoSPSaiLech.MinimumWidth = 6;
            this.colSoSPSaiLech.Name = "colSoSPSaiLech";
            this.colSoSPSaiLech.ReadOnly = true;
            this.colSoSPSaiLech.Width = 125;
            // 
            // colLyDo
            // 
            this.colLyDo.DataPropertyName = "LyDo";
            this.colLyDo.HeaderText = "Lý do sai lệch";
            this.colLyDo.MinimumWidth = 6;
            this.colLyDo.Name = "colLyDo";
            this.colLyDo.ReadOnly = true;
            this.colLyDo.Width = 125;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(188)))), ((int)(((byte)(212)))));
            this.panel1.Controls.Add(this.lblChiTietPhieuKiemKe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(956, 60);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblChiTietPhieuKiemKe
            // 
            this.lblChiTietPhieuKiemKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblChiTietPhieuKiemKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChiTietPhieuKiemKe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChiTietPhieuKiemKe.ForeColor = System.Drawing.Color.White;
            this.lblChiTietPhieuKiemKe.Location = new System.Drawing.Point(0, 0);
            this.lblChiTietPhieuKiemKe.Name = "lblChiTietPhieuKiemKe";
            this.lblChiTietPhieuKiemKe.Size = new System.Drawing.Size(956, 60);
            this.lblChiTietPhieuKiemKe.TabIndex = 0;
            this.lblChiTietPhieuKiemKe.Text = "CHI TIẾT PHIẾU KIỂM KÊ";
            this.lblChiTietPhieuKiemKe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(399, 403);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 35);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FChiTietKiemKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 450);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_ChiTietKiemKe);
            this.Name = "FChiTietKiemKe";
            this.Text = "FChiTietKiemKe";
            this.Load += new System.EventHandler(this.FChiTietKiemKe_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChiTietKiemKe)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.DataGridView dgv_ChiTietKiemKe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSLSanPhamThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoSPSaiLech;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyDo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblChiTietPhieuKiemKe;
        private System.Windows.Forms.Button btnDong;
    }
}