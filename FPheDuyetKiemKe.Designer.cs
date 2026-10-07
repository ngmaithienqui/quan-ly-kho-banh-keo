namespace QLCuaHangBanhKeo
{
    partial class FPheDuyetKiemKe
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblChiTietPhieuKiemKe = new System.Windows.Forms.Label();
            this.dgv_ChiTietKiemKe_PheDuyet = new System.Windows.Forms.DataGridView();
            this.colMaChiTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSLSanPhamThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoSPSaiLech = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLyDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPheDuyet = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChiTietKiemKe_PheDuyet)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(188)))), ((int)(((byte)(212)))));
            this.panel1.Controls.Add(this.lblChiTietPhieuKiemKe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 60);
            this.panel1.TabIndex = 9;
            // 
            // lblChiTietPhieuKiemKe
            // 
            this.lblChiTietPhieuKiemKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblChiTietPhieuKiemKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChiTietPhieuKiemKe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChiTietPhieuKiemKe.ForeColor = System.Drawing.Color.White;
            this.lblChiTietPhieuKiemKe.Location = new System.Drawing.Point(0, 0);
            this.lblChiTietPhieuKiemKe.Name = "lblChiTietPhieuKiemKe";
            this.lblChiTietPhieuKiemKe.Size = new System.Drawing.Size(864, 60);
            this.lblChiTietPhieuKiemKe.TabIndex = 0;
            this.lblChiTietPhieuKiemKe.Text = "CHI TIẾT PHIẾU KIỂM KÊ";
            this.lblChiTietPhieuKiemKe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv_ChiTietKiemKe_PheDuyet
            // 
            this.dgv_ChiTietKiemKe_PheDuyet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ChiTietKiemKe_PheDuyet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaChiTiet,
            this.colMaPhieu,
            this.colMaSP,
            this.colTenSP,
            this.colSLSanPhamThuc,
            this.colSoSPSaiLech,
            this.colLyDo});
            this.dgv_ChiTietKiemKe_PheDuyet.Location = new System.Drawing.Point(-64, 102);
            this.dgv_ChiTietKiemKe_PheDuyet.Name = "dgv_ChiTietKiemKe_PheDuyet";
            this.dgv_ChiTietKiemKe_PheDuyet.RowHeadersWidth = 51;
            this.dgv_ChiTietKiemKe_PheDuyet.RowTemplate.Height = 24;
            this.dgv_ChiTietKiemKe_PheDuyet.Size = new System.Drawing.Size(928, 289);
            this.dgv_ChiTietKiemKe_PheDuyet.TabIndex = 7;
            this.dgv_ChiTietKiemKe_PheDuyet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ChiTietKiemKe_PheDuyet_CellContentClick);
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
            // btnPheDuyet
            // 
            this.btnPheDuyet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPheDuyet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnPheDuyet.FlatAppearance.BorderSize = 0;
            this.btnPheDuyet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPheDuyet.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPheDuyet.ForeColor = System.Drawing.Color.White;
            this.btnPheDuyet.Location = new System.Drawing.Point(321, 403);
            this.btnPheDuyet.Name = "btnPheDuyet";
            this.btnPheDuyet.Size = new System.Drawing.Size(159, 35);
            this.btnPheDuyet.TabIndex = 10;
            this.btnPheDuyet.Text = "Phê duyệt";
            this.btnPheDuyet.UseVisualStyleBackColor = false;
            this.btnPheDuyet.Click += new System.EventHandler(this.btnPheDuyet_Click);
            // 
            // FPheDuyetKiemKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 450);
            this.Controls.Add(this.btnPheDuyet);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_ChiTietKiemKe_PheDuyet);
            this.Name = "FPheDuyetKiemKe";
            this.Text = "FPheDuyetKiemKe";
            this.Load += new System.EventHandler(this.FPheDuyetKiemKe_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChiTietKiemKe_PheDuyet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblChiTietPhieuKiemKe;
        private System.Windows.Forms.DataGridView dgv_ChiTietKiemKe_PheDuyet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSLSanPhamThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoSPSaiLech;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyDo;
        private System.Windows.Forms.Button btnPheDuyet;
    }
}