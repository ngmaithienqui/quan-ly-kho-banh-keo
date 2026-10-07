namespace QLCuaHangBanhKeo
{
    partial class FLichSuKiemKeCDL
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
            this.btnDong = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLSPhieuKiemKeCDL = new System.Windows.Forms.Label();
            this.dgv_LichSuKiemKe = new System.Windows.Forms.DataGridView();
            this.ColMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayLap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguoiLapPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnChiTiet = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LichSuKiemKe)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDong
            // 
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(254, 402);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 35);
            this.btnDong.TabIndex = 10;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(188)))), ((int)(((byte)(212)))));
            this.panel1.Controls.Add(this.lblLSPhieuKiemKeCDL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 60);
            this.panel1.TabIndex = 9;
            // 
            // lblLSPhieuKiemKeCDL
            // 
            this.lblLSPhieuKiemKeCDL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblLSPhieuKiemKeCDL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLSPhieuKiemKeCDL.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLSPhieuKiemKeCDL.ForeColor = System.Drawing.Color.White;
            this.lblLSPhieuKiemKeCDL.Location = new System.Drawing.Point(0, 0);
            this.lblLSPhieuKiemKeCDL.Name = "lblLSPhieuKiemKeCDL";
            this.lblLSPhieuKiemKeCDL.Size = new System.Drawing.Size(692, 60);
            this.lblLSPhieuKiemKeCDL.TabIndex = 0;
            this.lblLSPhieuKiemKeCDL.Text = "LỊCH SỬ KIỂM KÊ";
            this.lblLSPhieuKiemKeCDL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv_LichSuKiemKe
            // 
            this.dgv_LichSuKiemKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_LichSuKiemKe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColMaPhieu,
            this.colNgayLap,
            this.colTrangThai,
            this.colNguoiLapPhieu,
            this.btnChiTiet});
            this.dgv_LichSuKiemKe.Location = new System.Drawing.Point(6, 77);
            this.dgv_LichSuKiemKe.Name = "dgv_LichSuKiemKe";
            this.dgv_LichSuKiemKe.RowHeadersWidth = 51;
            this.dgv_LichSuKiemKe.RowTemplate.Height = 24;
            this.dgv_LichSuKiemKe.Size = new System.Drawing.Size(677, 302);
            this.dgv_LichSuKiemKe.TabIndex = 8;
            this.dgv_LichSuKiemKe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_LichSuKiemKe_CellContentClick);
            // 
            // ColMaPhieu
            // 
            this.ColMaPhieu.HeaderText = "Mã Phiếu";
            this.ColMaPhieu.MinimumWidth = 6;
            this.ColMaPhieu.Name = "ColMaPhieu";
            this.ColMaPhieu.Width = 125;
            // 
            // colNgayLap
            // 
            this.colNgayLap.HeaderText = "Ngày lập phiếu";
            this.colNgayLap.MinimumWidth = 6;
            this.colNgayLap.Name = "colNgayLap";
            this.colNgayLap.Width = 125;
            // 
            // colTrangThai
            // 
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 6;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Width = 125;
            // 
            // colNguoiLapPhieu
            // 
            this.colNguoiLapPhieu.HeaderText = "Người lập phiếu";
            this.colNguoiLapPhieu.MinimumWidth = 6;
            this.colNguoiLapPhieu.Name = "colNguoiLapPhieu";
            this.colNguoiLapPhieu.Width = 125;
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.HeaderText = "";
            this.btnChiTiet.MinimumWidth = 6;
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Text = "Chi tiết";
            this.btnChiTiet.UseColumnTextForButtonValue = true;
            this.btnChiTiet.Width = 125;
            // 
            // FLichSuKiemKeCDL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 450);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_LichSuKiemKe);
            this.Name = "FLichSuKiemKeCDL";
            this.Text = "FLichSuKiemKeCDL";
            this.Load += new System.EventHandler(this.FLichSuKiemKeCDL_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LichSuKiemKe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLSPhieuKiemKeCDL;
        private System.Windows.Forms.DataGridView dgv_LichSuKiemKe;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayLap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguoiLapPhieu;
        private System.Windows.Forms.DataGridViewButtonColumn btnChiTiet;
    }
}