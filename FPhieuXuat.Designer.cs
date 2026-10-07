using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    partial class FPhieuXuat
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvPhieuXuat = new System.Windows.Forms.DataGridView();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThaiThanhToan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNoData = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnLapDonDatHang = new System.Windows.Forms.Button();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.btnLapPhieuXuat = new System.Windows.Forms.Button();
            this.btnInPhieu = new System.Windows.Forms.Button();
            this.btnCapNhatTrangThaiThanhToan = new System.Windows.Forms.Button();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuXuat)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1109, 65);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(388, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(310, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ PHIẾU XUẤT";
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.panelContent);
            this.panelContainer.Controls.Add(this.panelButtons);
            this.panelContainer.Controls.Add(this.panelFilter);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 65);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1109, 664);
            this.panelContainer.TabIndex = 1;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.dgvPhieuXuat);
            this.panelContent.Controls.Add(this.lblNoData);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 85);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20);
            this.panelContent.Size = new System.Drawing.Size(1109, 462);
            this.panelContent.TabIndex = 2;
            // 
            // dgvPhieuXuat
            // 
            this.dgvPhieuXuat.AllowUserToAddRows = false;
            this.dgvPhieuXuat.AllowUserToDeleteRows = false;
            this.dgvPhieuXuat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieuXuat.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPhieuXuat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhieuXuat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPhieuXuat.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhieuXuat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPhieuXuat.ColumnHeadersHeight = 50;
            this.dgvPhieuXuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPhieuXuat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPhieu,
            this.colNgayXuat,
            this.colTenKH,
            this.colTrangThai,
            this.colThanhTien,
            this.colTrangThaiThanhToan});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhieuXuat.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPhieuXuat.EnableHeadersVisualStyles = false;
            this.dgvPhieuXuat.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.dgvPhieuXuat.Location = new System.Drawing.Point(12, 6);
            this.dgvPhieuXuat.Name = "dgvPhieuXuat";
            this.dgvPhieuXuat.ReadOnly = true;
            this.dgvPhieuXuat.RowHeadersVisible = false;
            this.dgvPhieuXuat.RowHeadersWidth = 51;
            this.dgvPhieuXuat.RowTemplate.Height = 40;
            this.dgvPhieuXuat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhieuXuat.Size = new System.Drawing.Size(1079, 417);
            this.dgvPhieuXuat.TabIndex = 1;
            this.dgvPhieuXuat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhieuXuat_CellClick);
            this.dgvPhieuXuat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhieuXuat_CellContentClick);
            // 
            // colMaPhieu
            // 
            this.colMaPhieu.DataPropertyName = "MaPhieu";
            this.colMaPhieu.HeaderText = "Mã phiếu";
            this.colMaPhieu.MinimumWidth = 6;
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.ReadOnly = true;
            // 
            // colNgayXuat
            // 
            this.colNgayXuat.DataPropertyName = "NgayXuatHang";
            this.colNgayXuat.HeaderText = "Ngày xuất hàng";
            this.colNgayXuat.MinimumWidth = 6;
            this.colNgayXuat.Name = "colNgayXuat";
            this.colNgayXuat.ReadOnly = true;
            // 
            // colTenKH
            // 
            this.colTenKH.DataPropertyName = "TenKH";
            this.colTenKH.HeaderText = "Khách hàng";
            this.colTenKH.MinimumWidth = 6;
            this.colTenKH.Name = "colTenKH";
            this.colTenKH.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.DataPropertyName = "TrangThai";
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 6;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "ThanhTien";
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.MinimumWidth = 6;
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            // 
            // colTrangThaiThanhToan
            // 
            this.colTrangThaiThanhToan.DataPropertyName = "TrangThaiThanhToan";
            this.colTrangThaiThanhToan.HeaderText = "Trạng thái thanh toán";
            this.colTrangThaiThanhToan.MinimumWidth = 6;
            this.colTrangThaiThanhToan.Name = "colTrangThaiThanhToan";
            this.colTrangThaiThanhToan.ReadOnly = true;
            // 
            // lblNoData
            // 
            this.lblNoData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoData.ForeColor = System.Drawing.Color.Gray;
            this.lblNoData.Location = new System.Drawing.Point(20, 20);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(1069, 422);
            this.lblNoData.TabIndex = 0;
            this.lblNoData.Text = "Không có dữ liệu phiếu xuất";
            this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoData.Visible = false;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnXoa);
            this.panelButtons.Controls.Add(this.btnSua);
            this.panelButtons.Controls.Add(this.btnLapDonDatHang);
            this.panelButtons.Controls.Add(this.btnChiTiet);
            this.panelButtons.Controls.Add(this.btnLapPhieuXuat);
            this.panelButtons.Controls.Add(this.btnInPhieu);
            this.panelButtons.Controls.Add(this.btnCapNhatTrangThaiThanhToan);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 547);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1109, 117);
            this.panelButtons.TabIndex = 1;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(627, 37);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(111, 45);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(157, 37);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(118, 45);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnLapDonDatHang
            // 
            this.btnLapDonDatHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLapDonDatHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapDonDatHang.FlatAppearance.BorderSize = 0;
            this.btnLapDonDatHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapDonDatHang.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLapDonDatHang.ForeColor = System.Drawing.Color.White;
            this.btnLapDonDatHang.Location = new System.Drawing.Point(891, 37);
            this.btnLapDonDatHang.Name = "btnLapDonDatHang";
            this.btnLapDonDatHang.Size = new System.Drawing.Size(170, 45);
            this.btnLapDonDatHang.TabIndex = 6;
            this.btnLapDonDatHang.Text = "Đơn đặt hàng";
            this.btnLapDonDatHang.UseVisualStyleBackColor = false;
            this.btnLapDonDatHang.Click += new System.EventHandler(this.btnLapDonDatHang_Click);
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnChiTiet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChiTiet.FlatAppearance.BorderSize = 0;
            this.btnChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChiTiet.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnChiTiet.Location = new System.Drawing.Point(509, 37);
            this.btnChiTiet.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Size = new System.Drawing.Size(105, 45);
            this.btnChiTiet.TabIndex = 2;
            this.btnChiTiet.Text = "Chi tiết";
            this.btnChiTiet.UseVisualStyleBackColor = false;
            this.btnChiTiet.Click += new System.EventHandler(this.btnChiTiet_Click);
            // 
            // btnLapPhieuXuat
            // 
            this.btnLapPhieuXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLapPhieuXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLapPhieuXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapPhieuXuat.FlatAppearance.BorderSize = 0;
            this.btnLapPhieuXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapPhieuXuat.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLapPhieuXuat.ForeColor = System.Drawing.Color.White;
            this.btnLapPhieuXuat.Location = new System.Drawing.Point(25, 37);
            this.btnLapPhieuXuat.Name = "btnLapPhieuXuat";
            this.btnLapPhieuXuat.Size = new System.Drawing.Size(109, 45);
            this.btnLapPhieuXuat.TabIndex = 6;
            this.btnLapPhieuXuat.Text = "Lập phiếu";
            this.btnLapPhieuXuat.UseVisualStyleBackColor = false;
            this.btnLapPhieuXuat.Click += new System.EventHandler(this.btnLapPhieuXuat_Click);
            // 
            // btnInPhieu
            // 
            this.btnInPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnInPhieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInPhieu.Enabled = false;
            this.btnInPhieu.FlatAppearance.BorderSize = 0;
            this.btnInPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInPhieu.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInPhieu.ForeColor = System.Drawing.Color.White;
            this.btnInPhieu.Location = new System.Drawing.Point(757, 37);
            this.btnInPhieu.Name = "btnInPhieu";
            this.btnInPhieu.Size = new System.Drawing.Size(117, 45);
            this.btnInPhieu.TabIndex = 4;
            this.btnInPhieu.Text = "In phiếu";
            this.btnInPhieu.UseVisualStyleBackColor = false;
            this.btnInPhieu.Click += new System.EventHandler(this.btnInPhieu_Click);
            // 
            // btnCapNhatTrangThaiThanhToan
            // 
            this.btnCapNhatTrangThaiThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhatTrangThaiThanhToan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnCapNhatTrangThaiThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapNhatTrangThaiThanhToan.Enabled = false;
            this.btnCapNhatTrangThaiThanhToan.FlatAppearance.BorderSize = 0;
            this.btnCapNhatTrangThaiThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhatTrangThaiThanhToan.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatTrangThaiThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatTrangThaiThanhToan.Location = new System.Drawing.Point(293, 37);
            this.btnCapNhatTrangThaiThanhToan.Name = "btnCapNhatTrangThaiThanhToan";
            this.btnCapNhatTrangThaiThanhToan.Size = new System.Drawing.Size(201, 45);
            this.btnCapNhatTrangThaiThanhToan.TabIndex = 7;
            this.btnCapNhatTrangThaiThanhToan.Text = "Trạng Thái Thanh Toán";
            this.btnCapNhatTrangThaiThanhToan.UseVisualStyleBackColor = false;
            this.btnCapNhatTrangThaiThanhToan.Click += new System.EventHandler(this.btnCapNhatTrangThaiThanhToan_Click);
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.panelFilter.Controls.Add(this.btnLamMoi);
            this.panelFilter.Controls.Add(this.btnTimKiem);
            this.panelFilter.Controls.Add(this.dtpDenNgay);
            this.panelFilter.Controls.Add(this.label2);
            this.panelFilter.Controls.Add(this.dtpTuNgay);
            this.panelFilter.Controls.Add(this.label1);
            this.panelFilter.Controls.Add(this.cboTrangThai);
            this.panelFilter.Controls.Add(this.label3);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1109, 85);
            this.panelFilter.TabIndex = 0;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(909, 18);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(105, 45);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(757, 18);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(106, 45);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(557, 28);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(130, 27);
            this.dtpDenNgay.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(479, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Đến ngày";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(343, 28);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(130, 27);
            this.dtpTuNgay.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(275, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Từ ngày";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Tất cả",
            "Đã xuất hàng",
            "Chưa xuất hàng",
            "Chờ xử lý",
            });
            this.cboTrangThai.Location = new System.Drawing.Point(127, 27);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(105, 28);
            this.cboTrangThai.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Lọc theo";
            // 
            // FPhieuXuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 729);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FPhieuXuat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý phiếu xuất";
            this.Load += new System.EventHandler(this.FPhieuXuat_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContainer.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuXuat)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu <= 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất để in!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Chuẩn bị dữ liệu để in
                DataTable dtPhieuXuat = GetPhieuXuatInfo(selectedMaPhieu);
                DataTable dtChiTietPhieuXuat = GetChiTietPhieuXuat(selectedMaPhieu);

                if (dtPhieuXuat.Rows.Count > 0 && dtChiTietPhieuXuat.Rows.Count > 0)
                {
                    // Hiển thị hộp thoại xác nhận in
                    if (MessageBox.Show("Bạn có muốn in phiếu xuất này?", "Xác nhận in",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Gọi hàm in phiếu xuất
                        PrintPhieuXuat(dtPhieuXuat, dtChiTietPhieuXuat);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phiếu xuất để in!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuẩn bị in: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPhieuXuat(DataTable dtPhieuXuat, DataTable dtChiTiet)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169); // A4 size in hundredths of an inch

                // Đăng ký sự kiện in
                pd.PrintPage += (sender, e) => {
                    // Lấy dữ liệu từ DataTable
                    DataRow rowPhieu = dtPhieuXuat.Rows[0];

                    // Thiết lập font
                    Font titleFont = new Font("Arial", 16, FontStyle.Bold);
                    Font headerFont = new Font("Arial", 12, FontStyle.Bold);
                    Font normalFont = new Font("Arial", 10, FontStyle.Regular);
                    Font totalFont = new Font("Arial", 12, FontStyle.Bold);

                    // Thiết lập vị trí và kích thước
                    int margin = 50;
                    int y = margin;

                    // In tiêu đề
                    e.Graphics.DrawString("PHIẾU XUẤT HÀNG", titleFont, Brushes.Black,
                        e.PageBounds.Width / 2 - 100, y);
                    y += 40;

                    // In thông tin phiếu
                    e.Graphics.DrawString("Mã phiếu: " + rowPhieu["MaPhieu"].ToString(), headerFont,
                        Brushes.Black, margin, y);
                    e.Graphics.DrawString("Ngày: " + Convert.ToDateTime(rowPhieu["NgayXuatHang"]).ToString("dd/MM/yyyy"),
                        headerFont, Brushes.Black, e.PageBounds.Width - 250, y);
                    y += 30;

                    // In thông tin khách hàng
                    e.Graphics.DrawString("Khách hàng: " + rowPhieu["HoTen"].ToString(), normalFont,
                        Brushes.Black, margin, y);
                    y += 20;
                    e.Graphics.DrawString("Địa chỉ: " + rowPhieu["DiaChi"].ToString(), normalFont,
                        Brushes.Black, margin, y);
                    y += 20;
                    e.Graphics.DrawString("Điện thoại: " + rowPhieu["Sdt"].ToString(), normalFont,
                        Brushes.Black, margin, y);
                    y += 40;

                    // In header bảng chi tiết
                    e.Graphics.DrawString("STT", headerFont, Brushes.Black, margin, y);
                    e.Graphics.DrawString("Tên sản phẩm", headerFont, Brushes.Black, margin + 50, y);
                    e.Graphics.DrawString("Số lượng", headerFont, Brushes.Black, margin + 300, y);
                    e.Graphics.DrawString("Đơn giá", headerFont, Brushes.Black, margin + 400, y);
                    e.Graphics.DrawString("Thành tiền", headerFont, Brushes.Black, margin + 500, y);
                    y += 25;

                    // Vẽ đường kẻ
                    e.Graphics.DrawLine(Pens.Black, margin, y, e.PageBounds.Width - margin, y);
                    y += 10;

                    // In chi tiết sản phẩm
                    int stt = 1;
                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        e.Graphics.DrawString(stt.ToString(), normalFont, Brushes.Black, margin, y);
                        e.Graphics.DrawString(row["TenSP"].ToString(), normalFont, Brushes.Black, margin + 50, y);
                        e.Graphics.DrawString(row["SoLuong"].ToString(), normalFont, Brushes.Black, margin + 300, y);
                        e.Graphics.DrawString(string.Format("{0:N0} đ", row["GiaXuat"]), normalFont,
                            Brushes.Black, margin + 400, y);
                        e.Graphics.DrawString(string.Format("{0:N0} đ", row["ThanhTien"]), normalFont,
                            Brushes.Black, margin + 500, y);
                        y += 25;
                        stt++;
                    }

                    // Vẽ đường kẻ
                    y += 10;
                    e.Graphics.DrawLine(Pens.Black, margin, y, e.PageBounds.Width - margin, y);
                    y += 20;

                    // In tổng tiền
                    string tongTien = string.Format("{0:N0} đ", rowPhieu["ThanhTien"]);
                    e.Graphics.DrawString("Tổng tiền:", totalFont, Brushes.Black, margin + 400, y);
                    e.Graphics.DrawString(tongTien, totalFont, Brushes.Black, margin + 500, y);
                    y += 40;

                    // In ghi chú nếu có
                    if (rowPhieu["GhiChu"] != DBNull.Value && !string.IsNullOrEmpty(rowPhieu["GhiChu"].ToString()))
                    {
                        e.Graphics.DrawString("Ghi chú: " + rowPhieu["GhiChu"].ToString(), normalFont,
                            Brushes.Black, margin, y);
                        y += 30;
                    }

                    // In chữ ký
                    y += 30;
                    e.Graphics.DrawString("Người lập phiếu", headerFont, Brushes.Black, margin + 100, y);
                    e.Graphics.DrawString("Người nhận hàng", headerFont, Brushes.Black, margin + 500, y);
                    y += 20;
                    e.Graphics.DrawString("(Ký, ghi rõ họ tên)", normalFont, Brushes.Black, margin + 100, y);
                    e.Graphics.DrawString("(Ký, ghi rõ họ tên)", normalFont, Brushes.Black, margin + 500, y);
                };

                PrintPreviewDialog ppd = new PrintPreviewDialog();
                ppd.Document = pd;
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in phiếu xuất: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLapPhieuXuat_Click(object sender, EventArgs e)
        {
            // Mở form lập phiếu xuất mới
            FLapPhieuXuat frmLapPhieuXuat = new FLapPhieuXuat();
            if (frmLapPhieuXuat.ShowDialog() == DialogResult.OK)
            {
                // Sau khi đóng form lập phiếu, load lại danh sách phiếu xuất
                LoadPhieuXuat();
            }
        }








        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.DataGridView dgvPhieuXuat;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnChiTiet;
        private System.Windows.Forms.Button btnInPhieu;
        private System.Windows.Forms.Button btnLapPhieuXuat;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThaiThanhToan;
        private System.Windows.Forms.Button btnCapNhatTrangThaiThanhToan;
        private Button btnLapDonDatHang;
    }
}