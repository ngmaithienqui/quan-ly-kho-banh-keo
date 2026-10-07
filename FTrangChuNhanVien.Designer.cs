// FTrangChuNhanVien.Designer.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    partial class FTrangChuNhanVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnXemSanPham = new System.Windows.Forms.Button();
            this.btnXemDanhMuc = new System.Windows.Forms.Button();
            this.btnXemPhieuXuat = new System.Windows.Forms.Button();
            this.btnXemPhieuNhap = new System.Windows.Forms.Button();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblChaoMung = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelMenu.Controls.Add(this.btnDangXuat);
            this.panelMenu.Controls.Add(this.btnXemSanPham);
            this.panelMenu.Controls.Add(this.btnXemDanhMuc);
            this.panelMenu.Controls.Add(this.btnXemPhieuXuat);
            this.panelMenu.Controls.Add(this.btnXemPhieuNhap);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 628);
            this.panelMenu.TabIndex = 2;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDangXuat.FlatAppearance.BorderSize = 0;
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.Location = new System.Drawing.Point(0, 200);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(200, 50);
            this.btnDangXuat.TabIndex = 0;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = false;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnXemSanPham
            // 
            this.btnXemSanPham.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnXemSanPham.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnXemSanPham.FlatAppearance.BorderSize = 0;
            this.btnXemSanPham.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnXemSanPham.ForeColor = System.Drawing.Color.White;
            this.btnXemSanPham.Location = new System.Drawing.Point(0, 150);
            this.btnXemSanPham.Name = "btnXemSanPham";
            this.btnXemSanPham.Size = new System.Drawing.Size(200, 50);
            this.btnXemSanPham.TabIndex = 2;
            this.btnXemSanPham.Text = "Xem sản phẩm";
            this.btnXemSanPham.UseVisualStyleBackColor = false;
            this.btnXemSanPham.Click += new System.EventHandler(this.btnXemSanPham_Click);
            // 
            // btnXemDanhMuc
            // 
            this.btnXemDanhMuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnXemDanhMuc.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnXemDanhMuc.FlatAppearance.BorderSize = 0;
            this.btnXemDanhMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnXemDanhMuc.ForeColor = System.Drawing.Color.White;
            this.btnXemDanhMuc.Location = new System.Drawing.Point(0, 100);
            this.btnXemDanhMuc.Name = "btnXemDanhMuc";
            this.btnXemDanhMuc.Size = new System.Drawing.Size(200, 50);
            this.btnXemDanhMuc.TabIndex = 3;
            this.btnXemDanhMuc.Text = "Xem danh mục";
            this.btnXemDanhMuc.UseVisualStyleBackColor = false;
            this.btnXemDanhMuc.Click += new System.EventHandler(this.btnXemDanhMuc_Click);
            // 
            // btnXemPhieuXuat
            // 
            this.btnXemPhieuXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnXemPhieuXuat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnXemPhieuXuat.FlatAppearance.BorderSize = 0;
            this.btnXemPhieuXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemPhieuXuat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnXemPhieuXuat.ForeColor = System.Drawing.Color.White;
            this.btnXemPhieuXuat.Location = new System.Drawing.Point(0, 50);
            this.btnXemPhieuXuat.Name = "btnXemPhieuXuat";
            this.btnXemPhieuXuat.Size = new System.Drawing.Size(200, 50);
            this.btnXemPhieuXuat.TabIndex = 4;
            this.btnXemPhieuXuat.Text = "Xem phiếu xuất";
            this.btnXemPhieuXuat.UseVisualStyleBackColor = false;
            this.btnXemPhieuXuat.Click += new System.EventHandler(this.btnXemPhieuXuat_Click);
            // 
            // btnXemPhieuNhap
            // 
            this.btnXemPhieuNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnXemPhieuNhap.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnXemPhieuNhap.FlatAppearance.BorderSize = 0;
            this.btnXemPhieuNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemPhieuNhap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnXemPhieuNhap.ForeColor = System.Drawing.Color.White;
            this.btnXemPhieuNhap.Location = new System.Drawing.Point(0, 0);
            this.btnXemPhieuNhap.Name = "btnXemPhieuNhap";
            this.btnXemPhieuNhap.Size = new System.Drawing.Size(200, 50);
            this.btnXemPhieuNhap.TabIndex = 5;
            this.btnXemPhieuNhap.Text = "Xem phiếu nhập";
            this.btnXemPhieuNhap.UseVisualStyleBackColor = false;
            this.btnXemPhieuNhap.Click += new System.EventHandler(this.btnXemPhieuNhap_Click);
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTitle.Controls.Add(this.lblChaoMung);
            this.panelTitle.Controls.Add(this.lblDateTime);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(200, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(1074, 60);
            this.panelTitle.TabIndex = 1;
            // 
            // lblChaoMung
            // 
            this.lblChaoMung.AutoSize = true;
            this.lblChaoMung.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChaoMung.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblChaoMung.Location = new System.Drawing.Point(20, 20);
            this.lblChaoMung.Name = "lblChaoMung";
            this.lblChaoMung.Size = new System.Drawing.Size(243, 28);
            this.lblChaoMung.TabIndex = 0;
            this.lblChaoMung.Text = "Xin chào: Nhân viên kho";
            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDateTime.ForeColor = System.Drawing.Color.Gray;
            this.lblDateTime.Location = new System.Drawing.Point(890, 20);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(163, 23);
            this.lblDateTime.TabIndex = 1;
            this.lblDateTime.Text = "00/00/0000 00:00:00";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMain.Controls.Add(this.lblWelcome);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(200, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1074, 568);
            this.panelMain.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblWelcome.Location = new System.Drawing.Point(252, 207);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(578, 41);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "CHÀO MỪNG ĐẾN VỚI HỆ THỐNG KHO";
            // 
            // FTrangChuNhanVien
            // 
            this.ClientSize = new System.Drawing.Size(1274, 628);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.panelMenu);
            this.Name = "FTrangChuNhanVien";
            this.Text = "Trang Chủ - Nhân Viên Kho";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FTrangChuNhanVien_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        private Panel panelMenu;
        private Panel panelTitle;
        private Panel panelMain;
        private Button btnDangXuat;
        private Button btnXemSanPham;
        private Button btnXemDanhMuc;
        private Button btnXemPhieuXuat;
        private Button btnXemPhieuNhap;
        private Label lblChaoMung;
        private Label lblWelcome;
        private Label lblDateTime;
    }
}