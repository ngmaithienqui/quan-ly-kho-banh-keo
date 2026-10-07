using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FChiTietDonHang : Form
    {
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;
        private int maDonHang = -1;
        private bool isViewOnly = false;
        private DataTable dtChiTietDonHang = new DataTable();
        private bool isPhieuXuat = false;

        // Constructor for adding a new order
        public FChiTietDonHang()
        {
            InitializeComponent();
        }

        // Constructor for editing or viewing an order
        public FChiTietDonHang(int maDonHang, bool isViewOnly = false)
        {
            InitializeComponent();
            this.maDonHang = maDonHang;
            this.isViewOnly = isViewOnly;
        }

        // Constructor for viewing a PhieuXuatHang
        public FChiTietDonHang(int maPhieuXuat, bool isFromPhieuXuat, bool dummy = false)
        {
            InitializeComponent();
            this.maDonHang = maPhieuXuat;
            this.isViewOnly = true;
            this.isPhieuXuat = isFromPhieuXuat;
        }

        private void FChiTietDonHang_Load(object sender, EventArgs e)
        {
            InitializeForm();
            LoadKhachHang();
            LoadSanPham();
            
            if (maDonHang > 0)
            {
                // Load existing order data for edit or view
                if (isPhieuXuat)
                {
                    LoadPhieuXuat();
                    LoadChiTietPhieuXuat();
                    SetReadOnlyMode();
                    lblTitle.Text = "CHI TIẾT PHIẾU XUẤT HÀNG";
                    this.Text = "Chi tiết phiếu xuất hàng";
                    label1.Text = "Mã phiếu xuất:";
                    label3.Text = "Ngày xuất:";
                }
                else
                {
                    LoadDonHang();
                    LoadChiTietDonHang();
                    
                    if (isViewOnly)
                    {
                        SetReadOnlyMode();
                    }
                }
            }
            else
            {
                // Set up form for new order
                dtpNgayDat.Value = DateTime.Now;
                cboTrangThai.SelectedIndex = 0; // Default "Chờ xử lý"
                SetupDataGridViewForNewOrder();
            }
        }

        private void InitializeForm()
        {
            // Set form title based on mode
            if (isViewOnly)
                this.Text = "Chi tiết đơn hàng";
            else if (maDonHang > 0)
                this.Text = "Chỉnh sửa đơn hàng";
            else
                this.Text = "Tạo đơn hàng mới";
                
            // Initialize the dtChiTietDonHang data table
            dtChiTietDonHang.Columns.Add("MaSP", typeof(int));
            dtChiTietDonHang.Columns.Add("TenSP", typeof(string));
            dtChiTietDonHang.Columns.Add("DonViTinh", typeof(string));
            dtChiTietDonHang.Columns.Add("SoLuong", typeof(int));
            dtChiTietDonHang.Columns.Add("DonGia", typeof(decimal));
            dtChiTietDonHang.Columns.Add("ThanhTien", typeof(decimal));
            
            // Set dgvSanPham DataSource
            dgvChiTietDonHang.DataSource = dtChiTietDonHang;
            
            // Set up combobox for order status
            cboTrangThai.Items.AddRange(new string[] { "Chờ xử lý", "Đang xử lý", "Đã hoàn thành", "Đã hủy" });
        }

        private void LoadKhachHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT MaKH, HoTen FROM KhachHang ORDER BY HoTen";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    cboKhachHang.DisplayMember = "HoTen";
                    cboKhachHang.ValueMember = "MaKH";
                    cboKhachHang.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khách hàng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPham()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT MaSP, TenSP, GiaNhap, DonViTinh FROM SanPham ORDER BY TenSP";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    cboSanPham.DisplayMember = "TenSP";
                    cboSanPham.ValueMember = "MaSP";
                    cboSanPham.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDonHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT ph.MaPhieu, ph.NgayDat, ph.TrangThai, ph.GhiChu, ph.MaKH, kh.HoTen
                                FROM PhieuDatHang ph
                                INNER JOIN KhachHang kh ON ph.MaKH = kh.MaKH
                                WHERE ph.MaPhieu = @MaPhieu";
                    
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhieu", maDonHang);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtMaDonHang.Text = reader["MaPhieu"].ToString();
                            dtpNgayDat.Value = Convert.ToDateTime(reader["NgayDat"]);
                            cboTrangThai.Text = reader["TrangThai"].ToString();
                            txtGhiChu.Text = reader["GhiChu"].ToString();
                            
                            // Set selected customer
                            cboKhachHang.SelectedValue = Convert.ToInt32(reader["MaKH"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin đơn hàng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietDonHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT ct.MaSP, sp.TenSP, sp.DonViTinh, ct.SoLuong, sp.GiaNhap AS DonGia,
                                  (ct.SoLuong * sp.GiaNhap) AS ThanhTien
                              FROM ChiTietPhieuDat ct
                              INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                              WHERE ct.MaPhieu = @MaPhieu";
                              
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhieu", maDonHang);
                    
                    // Debug: Hiển thị mã đơn hàng đang tải
                    Console.WriteLine($"Đang tải chi tiết cho đơn hàng: {maDonHang}");
                    MessageBox.Show($"Đang tải chi tiết cho đơn hàng: {maDonHang}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Xóa dữ liệu cũ trước khi đổ dữ liệu mới
                    dtChiTietDonHang.Clear();
                    
                    // Đổ dữ liệu mới vào bảng chi tiết
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtChiTietDonHang);
                    
                    // Kiểm tra số lượng bản ghi
                    int count = dtChiTietDonHang.Rows.Count;
                    Console.WriteLine($"Tìm thấy {count} chi tiết đơn hàng");
                    
                    if (count > 0)
                    {
                        // Đảm bảo DataGridView sử dụng DataTable đúng cách
                        dgvChiTietDonHang.AutoGenerateColumns = false;
                        
                        // Ánh xạ các cột trong DataGridView với các cột trong DataTable
                        dgvChiTietDonHang.Columns["colMaSP"].DataPropertyName = "MaSP";
                        dgvChiTietDonHang.Columns["colTenSP"].DataPropertyName = "TenSP";
                        dgvChiTietDonHang.Columns["colDonViTinh"].DataPropertyName = "DonViTinh";
                        dgvChiTietDonHang.Columns["colSoLuong"].DataPropertyName = "SoLuong";
                        dgvChiTietDonHang.Columns["colDonGia"].DataPropertyName = "DonGia";
                        dgvChiTietDonHang.Columns["colThanhTien"].DataPropertyName = "ThanhTien";
                        
                        // Định dạng các cột số
                        dgvChiTietDonHang.Columns["colDonGia"].DefaultCellStyle.Format = "N0";
                        dgvChiTietDonHang.Columns["colThanhTien"].DefaultCellStyle.Format = "N0";
                        
                        // Gán DataSource cho DataGridView
                        dgvChiTietDonHang.DataSource = null;
                        dgvChiTietDonHang.DataSource = dtChiTietDonHang;
                        
                        // Tính tổng tiền
                        CalculateTotalAmount();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy chi tiết nào cho đơn hàng này!", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết đơn hàng: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewForNewOrder()
        {
            // Clear any existing data
            dtChiTietDonHang.Clear();
        }

        private void SetReadOnlyMode()
        {
            // Disable all controls for editing
            cboKhachHang.Enabled = false;
            dtpNgayDat.Enabled = false;
            cboTrangThai.Enabled = false;
            txtGhiChu.ReadOnly = true;
            cboSanPham.Enabled = false;
            numSoLuong.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Visible = false;
            btnHuy.Text = "Đóng";
            
            // Set the form title
            this.Text = "Xem chi tiết đơn hàng";
            lblTitle.Text = "XEM CHI TIẾT ĐƠN HÀNG";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numSoLuong.Value <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSP = Convert.ToInt32(cboSanPham.SelectedValue);
            int soLuongMuonThem = Convert.ToInt32(numSoLuong.Value);

            int soLuongTonKho = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT SoLuong FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                object result = cmd.ExecuteScalar();
                if (result != null)
                    soLuongTonKho = Convert.ToInt32(result);
            }

            // Kiểm tra số lượng
            if (soLuongMuonThem > soLuongTonKho)
            {
                MessageBox.Show($"Số lượng tồn kho chỉ còn {soLuongTonKho}. Không thể thêm {soLuongMuonThem} sản phẩm!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            string tenSP = cboSanPham.Text;
            
            // Check if product already exists in the order
            foreach (DataRow row in dtChiTietDonHang.Rows)
            {
                if (Convert.ToInt32(row["MaSP"]) == maSP)
                {
                    // Update quantity instead of adding a new row
                    int soLuongHienTai = Convert.ToInt32(row["SoLuong"]);
                    int soLuongMoi = soLuongHienTai + Convert.ToInt32(numSoLuong.Value);
                    row["SoLuong"] = soLuongMoi;
                    row["ThanhTien"] = Convert.ToDecimal(row["DonGia"]) * soLuongMoi;
                    
                    CalculateTotalAmount();
                    return;
                }
            }

            // Product not found in order, add new row
            DataRow newRow = dtChiTietDonHang.NewRow();
            
            // Get product details
            decimal donGia = 0;
            string donViTinh = "";
            
            DataTable productTable = ((DataTable)cboSanPham.DataSource);
            foreach (DataRow row in productTable.Rows)
            {
                if (Convert.ToInt32(row["MaSP"]) == maSP)
                {
                    donGia = Convert.ToDecimal(row["GiaNhap"]);
                    donViTinh = row["DonViTinh"].ToString();
                    break;
                }
            }
            
            newRow["MaSP"] = maSP;
            newRow["TenSP"] = tenSP;
            newRow["DonViTinh"] = donViTinh;
            newRow["SoLuong"] = Convert.ToInt32(numSoLuong.Value);
            newRow["DonGia"] = donGia;
            newRow["ThanhTien"] = donGia * Convert.ToInt32(numSoLuong.Value);
            
            dtChiTietDonHang.Rows.Add(newRow);
            
            // Reset the controls
            numSoLuong.Value = 1;
            
            // Calculate the total amount
            CalculateTotalAmount();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChiTietDonHang.CurrentRow != null)
            {
                int rowIndex = dgvChiTietDonHang.CurrentRow.Index;
                dtChiTietDonHang.Rows.RemoveAt(rowIndex);
                
                // Calculate the total amount
                CalculateTotalAmount();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CalculateTotalAmount()
        {
            decimal tongTien = 0;
            foreach (DataRow row in dtChiTietDonHang.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }
            
            txtTongTien.Text = string.Format("{0:N0} VNĐ", tongTien);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtChiTietDonHang.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm vào đơn hàng!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    
                    // Use transaction for data integrity
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int maKH = Convert.ToInt32(cboKhachHang.SelectedValue);
                            string trangThai = cboTrangThai.Text;
                            string ghiChu = txtGhiChu.Text;
                            
                            if (maDonHang <= 0)
                            {
                                // Insert new order
                                string insertOrderSql = @"INSERT INTO PhieuDatHang (MaKH, NgayDat, TrangThai, GhiChu)
                                                      VALUES (@MaKH, @NgayDat, @TrangThai, @GhiChu);
                                                      SELECT SCOPE_IDENTITY();";
                                
                                SqlCommand cmdInsertOrder = new SqlCommand(insertOrderSql, conn, transaction);
                                cmdInsertOrder.Parameters.AddWithValue("@MaKH", maKH);
                                cmdInsertOrder.Parameters.AddWithValue("@NgayDat", dtpNgayDat.Value);
                                cmdInsertOrder.Parameters.AddWithValue("@TrangThai", trangThai);
                                cmdInsertOrder.Parameters.AddWithValue("@GhiChu", ghiChu);
                                
                                // Execute the insert statement and get the generated ID
                                maDonHang = Convert.ToInt32(cmdInsertOrder.ExecuteScalar());
                            }
                            else
                            {
                                // Update existing order
                                string updateOrderSql = @"UPDATE PhieuDatHang 
                                                      SET MaKH = @MaKH, NgayDat = @NgayDat, 
                                                        TrangThai = @TrangThai, GhiChu = @GhiChu
                                                      WHERE MaPhieu = @MaPhieu";
                                

                                SqlCommand cmdUpdateOrder = new SqlCommand(updateOrderSql, conn, transaction);
                                cmdUpdateOrder.Parameters.AddWithValue("@MaKH", maKH);
                                cmdUpdateOrder.Parameters.AddWithValue("@NgayDat", dtpNgayDat.Value);
                                cmdUpdateOrder.Parameters.AddWithValue("@TrangThai", trangThai);
                                cmdUpdateOrder.Parameters.AddWithValue("@GhiChu", ghiChu);
                                cmdUpdateOrder.Parameters.AddWithValue("@MaPhieu", maDonHang);
                                
                                cmdUpdateOrder.ExecuteNonQuery();
                                
                                // Delete existing order details to replace with new ones
                                string deleteDetailsSql = "DELETE FROM ChiTietPhieuDat WHERE MaPhieu = @MaPhieu";
                                SqlCommand cmdDeleteDetails = new SqlCommand(deleteDetailsSql, conn, transaction);
                                cmdDeleteDetails.Parameters.AddWithValue("@MaPhieu", maDonHang);
                                cmdDeleteDetails.ExecuteNonQuery();
                            }
                            
                            // Insert order details
                            foreach (DataRow row in dtChiTietDonHang.Rows)
                            {
                                string insertDetailSql = @"INSERT INTO ChiTietPhieuDat (MaPhieu, MaSP, SoLuong)
                                                      VALUES (@MaPhieu, @MaSP, @SoLuong)";
                                
                                SqlCommand cmdInsertDetail = new SqlCommand(insertDetailSql, conn, transaction);
                                cmdInsertDetail.Parameters.AddWithValue("@MaPhieu", maDonHang);
                                cmdInsertDetail.Parameters.AddWithValue("@MaSP", row["MaSP"]);
                                cmdInsertDetail.Parameters.AddWithValue("@SoLuong", row["SoLuong"]);
                                
                                cmdInsertDetail.ExecuteNonQuery();
                            }
                            
                            // Commit the transaction
                            transaction.Commit();
                            
                            MessageBox.Show("Lưu đơn hàng thành công!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // Rollback on error
                            transaction.Rollback();
                            throw new Exception("Lỗi khi lưu đơn hàng: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (isViewOnly)
            {
                this.Close();
                return;
            }
            
            if (MessageBox.Show("Bạn có chắc chắn muốn hủy thao tác này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedIndex >= 0)
            {
                DataRowView selectedRow = (DataRowView)cboSanPham.SelectedItem;
                lblDonGia.Text = string.Format("{0:N0} VNĐ", Convert.ToDecimal(selectedRow["GiaNhap"]));
                lblDonViTinh.Text = selectedRow["DonViTinh"].ToString();
            }
        }
        
        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedIndex >= 0)
            {
                DataRowView selectedRow = (DataRowView)cboSanPham.SelectedItem;
                decimal donGia = Convert.ToDecimal(selectedRow["GiaNhap"]);
                decimal thanhTien = donGia * Convert.ToDecimal(numSoLuong.Value);
                lblThanhTien.Text = string.Format("{0:N0} VNĐ", thanhTien);
            }
        }

        private void dgvChiTietDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grpInfo_Enter(object sender, EventArgs e)
        {

        }

        private void LoadPhieuXuat()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT px.MaPhieu, px.NgayXuatHang, px.TrangThai, px.GhiChu, px.MaKH, kh.HoTen, px.ThanhTien
                                  FROM PhieuXuatHang px
                                  INNER JOIN KhachHang kh ON px.MaKH = kh.MaKH
                                  WHERE px.MaPhieu = @MaPhieu";
                    
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhieu", maDonHang);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtMaDonHang.Text = reader["MaPhieu"].ToString();
                            dtpNgayDat.Value = Convert.ToDateTime(reader["NgayXuatHang"]);
                            
                            // Cập nhật trạng thái
                            string trangThai = reader["TrangThai"].ToString();
                            if (string.IsNullOrEmpty(trangThai))
                            {
                                cboTrangThai.SelectedIndex = -1;
                            }
                            else
                            {
                                bool found = false;
                                for (int i = 0; i < cboTrangThai.Items.Count; i++)
                                {
                                    if (cboTrangThai.Items[i].ToString() == trangThai)
                                    {
                                        cboTrangThai.SelectedIndex = i;
                                        found = true;
                                        break;
                                    }
                                }
                                
                                if (!found)
                                {
                                    cboTrangThai.Items.Add(trangThai);
                                    cboTrangThai.SelectedIndex = cboTrangThai.Items.Count - 1;
                                }
                            }
                            
                            txtGhiChu.Text = reader["GhiChu"]?.ToString();
                            
                            // Set selected customer
                            cboKhachHang.SelectedValue = Convert.ToInt32(reader["MaKH"]);
                            
                            // Set tổng tiền
                            decimal thanhTien = Convert.ToDecimal(reader["ThanhTien"]);
                            txtTongTien.Text = string.Format("{0:N0} VNĐ", thanhTien);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin phiếu xuất!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin phiếu xuất: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietPhieuXuat()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT ct.MaSP, sp.TenSP, sp.DonViTinh, ct.SoLuong, ct.GiaXuat AS DonGia,
                                  (ct.SoLuong * ct.GiaXuat) AS ThanhTien
                              FROM ChiTietPhieuXuat ct
                              INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                              WHERE ct.MaPhieu = @MaPhieu";
                              
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhieu", maDonHang);
                    
                    // Debug: Hiển thị mã phiếu xuất đang tải
                    Console.WriteLine($"Đang tải chi tiết cho phiếu xuất: {maDonHang}");
                    
                    // Xóa dữ liệu cũ trước khi đổ dữ liệu mới
                    dtChiTietDonHang.Clear();
                    
                    // Đổ dữ liệu mới vào bảng chi tiết
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtChiTietDonHang);
                    
                    // Kiểm tra số lượng bản ghi
                    int count = dtChiTietDonHang.Rows.Count;
                    Console.WriteLine($"Tìm thấy {count} chi tiết phiếu xuất");
                    
                    if (count > 0)
                    {
                        // Đảm bảo DataGridView sử dụng DataTable đúng cách
                        dgvChiTietDonHang.AutoGenerateColumns = false;
                        
                        // Ánh xạ các cột trong DataGridView với các cột trong DataTable
                        dgvChiTietDonHang.Columns["colMaSP"].DataPropertyName = "MaSP";
                        dgvChiTietDonHang.Columns["colTenSP"].DataPropertyName = "TenSP";
                        dgvChiTietDonHang.Columns["colDonViTinh"].DataPropertyName = "DonViTinh";
                        dgvChiTietDonHang.Columns["colSoLuong"].DataPropertyName = "SoLuong";
                        dgvChiTietDonHang.Columns["colDonGia"].DataPropertyName = "DonGia";
                        dgvChiTietDonHang.Columns["colThanhTien"].DataPropertyName = "ThanhTien";
                        
                        // Định dạng các cột số
                        dgvChiTietDonHang.Columns["colDonGia"].DefaultCellStyle.Format = "N0";
                        dgvChiTietDonHang.Columns["colThanhTien"].DefaultCellStyle.Format = "N0";
                        
                        // Gán DataSource cho DataGridView
                        dgvChiTietDonHang.DataSource = null;
                        dgvChiTietDonHang.DataSource = dtChiTietDonHang;
                        
                        // Tính tổng tiền
                        CalculateTotalAmount();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy chi tiết nào cho phiếu xuất này!", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết phiếu xuất: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
