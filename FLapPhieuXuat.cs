using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace QLCuaHangBanhKeo
{
    public partial class FLapPhieuXuat : Form
    {
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;
        private int maPhieu = -1;
        private DataTable dtSanPham = new DataTable();
        private decimal tongTien = 0;
        private bool viewOnly = false; // Thêm biến để xác định chế độ chỉ xem

        private int maDonHang = -1; // bổ sung thêm

        // Constructor tạo phiếu xuất từ mã đơn hàng
        // Constructor tạo từ mã đơn đặt hàng


        // Constructor mặc định để tạo phiếu xuất mới
        public FLapPhieuXuat()
        {
            InitializeComponent();
            SetupDataTable();
            ApplyFormStyles();
        }

        // Constructor nhận mã phiếu để chỉnh sửa phiếu xuất hiện có
        public FLapPhieuXuat(int maPhieu)
        {
            InitializeComponent();
            this.maPhieu = maPhieu;
            this.Text = "Cập nhật phiếu xuất #" + maPhieu;
            lblTitle.Text = "CẬP NHẬT PHIẾU XUẤT HÀNG";
            SetupDataTable();
            ApplyFormStyles();
        }

        // Constructor mới để xem chi tiết phiếu xuất (chế độ chỉ xem)
        public FLapPhieuXuat(int maDonHangFromDatHang, bool isFromDonHang)
        {
            InitializeComponent();
            this.Text = "Tạo phiếu xuất từ đơn hàng #" + maDonHangFromDatHang;
            lblTitle.Text = "LẬP PHIẾU XUẤT TỪ ĐƠN HÀNG";

            this.maDonHang = maDonHangFromDatHang; 
            SetupDataTable();
            LoadDanhSachKhachHang();
            LoadDanhSachSanPham();
            LoadDataFromDonDatHang(maDonHangFromDatHang);
            ApplyFormStyles();
        }

        private void ApplyFormStyles()
        {
            // Áp dụng font chữ Segoe UI cho các phần tử chính
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            
            // Màu sắc và hiệu ứng cho panel header
            panelHeader.BackColor = Color.FromArgb(25, 118, 210);
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            
            // Style cho datagridview
            dgvSanPham.BorderStyle = BorderStyle.None;
            dgvSanPham.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvSanPham.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 222, 251);
            dgvSanPham.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSanPham.EnableHeadersVisualStyles = false;
            dgvSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgvSanPham.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSanPham.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            
            // Style cho nút lưu và hủy
            btnLuu.BackColor = Color.FromArgb(33, 150, 243);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            
            btnHuy.BackColor = Color.FromArgb(229, 57, 53);
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            
            // Style cho các textbox và combobox
            var controls = GetAllControls(this);
            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).BorderStyle = BorderStyle.FixedSingle;
                    control.Font = new Font("Segoe UI", 9F);
                }
                else if (control is ComboBox)
                {
                    control.Font = new Font("Segoe UI", 9F);
                }
                else if (control is Label && control != lblTitle && control != lblTongTien)
                {
                    control.Font = new Font("Segoe UI", 9F);
                }
                else if (control is Button && control != btnLuu && control != btnHuy)
                {
                    ((Button)control).FlatStyle = FlatStyle.Flat;
                    ((Button)control).BackColor = Color.FromArgb(33, 150, 243);
                    ((Button)control).ForeColor = Color.White;
                    control.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
            
            // Style cho GroupBox
            groupBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            
            // Style cho panel footer
            panelFooter.BackColor = Color.FromArgb(225, 245, 254);
            lblTongTien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(211, 47, 47);
            
            // Thêm viền và hiệu ứng shadow cho panel chính
            panelContent.BackColor = Color.White;
            panelContainer.BackColor = Color.FromArgb(245, 245, 245);
        }
        
        private List<Control> GetAllControls(Control container)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.Add(c);
                controlList.AddRange(GetAllControls(c));
            }
            return controlList;
        }

        private void SetupDataTable()
        {
            // Tạo cấu trúc cho bảng tạm chứa sản phẩm
            dtSanPham.Columns.Add("MaSP", typeof(int));
            dtSanPham.Columns.Add("TenSP", typeof(string));
            dtSanPham.Columns.Add("SoLuong", typeof(int));
            dtSanPham.Columns.Add("DonGia", typeof(decimal));
            dtSanPham.Columns.Add("DonViTinh", typeof(string));
            dtSanPham.Columns.Add("ThanhTien", typeof(decimal));
        }

        private void LoadPhieuTuDonHang(int maDonHang)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Lấy thông tin khách hàng
                string sqlDon = @"SELECT MaKH FROM PhieuDatHang WHERE MaPhieu = @MaPhieu";
                SqlCommand cmdDon = new SqlCommand(sqlDon, conn);
                cmdDon.Parameters.AddWithValue("@MaPhieu", maDonHang);
                object maKHObj = cmdDon.ExecuteScalar();
                if (maKHObj != null)
                {
                    cboKhachHang.SelectedValue = Convert.ToInt32(maKHObj);
                }

                // Lấy chi tiết sản phẩm
                string sqlCT = @"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, sp.GiaXuat, sp.DonViTinh
                         FROM ChiTietPhieuDat ct
                         INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                         WHERE ct.MaPhieu = @MaPhieu";

                SqlCommand cmd = new SqlCommand(sqlCT, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maDonHang);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int maSP = Convert.ToInt32(reader["MaSP"]);
                        string tenSP = reader["TenSP"].ToString();
                        int soLuong = Convert.ToInt32(reader["SoLuong"]);
                        decimal donGia = Convert.ToDecimal(reader["GiaXuat"]);
                        string donViTinh = reader["DonViTinh"].ToString();
                        decimal thanhTien = donGia * soLuong;

                        DataRow row = dtSanPham.NewRow();
                        row["MaSP"] = maSP;
                        row["TenSP"] = tenSP;
                        row["SoLuong"] = soLuong;
                        row["DonGia"] = donGia;
                        row["DonViTinh"] = donViTinh;
                        row["ThanhTien"] = thanhTien;
                        dtSanPham.Rows.Add(row);
                    }
                }

                CapNhatTongTien();
            }
        }


        private void FLapPhieuXuat_Load(object sender, EventArgs e)
        {
            try
            {
                if (cboTrangThai.Items.Count > 0)
                    cboTrangThai.SelectedIndex = 0;
                dtpNgayXuat.Value = DateTime.Now;

                LoadDanhSachKhachHang();
                LoadDanhSachSanPham();

                if (maPhieu > 0)
                {
                    LoadThongTinPhieuXuat();
                    LoadDanhSachChiTietPhieuXuat();
                }
                else if (maDonHang > 0)
                {
                    LoadPhieuTuDonHang(maDonHang); // mới thêm
                }

                dgvSanPham.DataSource = dtSanPham;
                dgvSanPham.Columns["colDonGia"].DefaultCellStyle.Format = "N0";
                dgvSanPham.Columns["colThanhTien"].DefaultCellStyle.Format = "N0";

                if (viewOnly)
                    SetViewOnlyMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo form: " + ex.Message);
            }
        }


        private void SetViewOnlyMode()
        {
            // Disable các control để không thể chỉnh sửa
            dtpNgayXuat.Enabled = false;
            cboKhachHang.Enabled = false;
            cboTrangThai.Enabled = false;
            txtGhiChu.ReadOnly = true;
            
            // Ẩn các nút thêm, sửa, xóa
            btnThemSanPham.Visible = false;
            cboSanPham.Enabled = false;
            numSoLuong.Enabled = false;
            
            // Ẩn nút Lưu và đổi nút Hủy thành Đóng
            btnLuu.Visible = false;
            btnHuy.Text = "Đóng";

            // Ẩn cột Xóa trong DataGridView nếu có
            if (dgvSanPham.Columns.Contains("colXoa"))
            {
                dgvSanPham.Columns["colXoa"].Visible = false;
            }
            
            // Không cho phép chỉnh sửa các cell trong DataGridView
            dgvSanPham.ReadOnly = true;
        }

        private void LoadDataFromDonDatHang(int maDonHang)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 1. Lấy thông tin khách hàng của đơn đặt hàng
                string sqlKhach = @"SELECT MaKH FROM PhieuDatHang WHERE MaPhieu = @MaPhieu";
                using (SqlCommand cmd = new SqlCommand(sqlKhach, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maDonHang);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int maKH = Convert.ToInt32(result);
                        cboKhachHang.SelectedValue = maKH;

                        // Gọi sự kiện selectedIndexChanged để load SDT & Địa chỉ
                        cboKhachHang_SelectedIndexChanged(cboKhachHang, EventArgs.Empty);
                    }
                }

                // 2. Load sản phẩm từ ChiTietPhieuDat
                string sqlCT = @"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, sp.GiaXuat, sp.DonViTinh
                         FROM ChiTietPhieuDat ct
                         INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                         WHERE ct.MaPhieu = @MaPhieu";

                using (SqlCommand cmd = new SqlCommand(sqlCT, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maDonHang);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow row = dtSanPham.NewRow();
                            row["MaSP"] = reader["MaSP"];
                            row["TenSP"] = reader["TenSP"];
                            row["SoLuong"] = reader["SoLuong"];
                            row["DonGia"] = reader["GiaXuat"];
                            row["DonViTinh"] = reader["DonViTinh"];
                            row["ThanhTien"] = Convert.ToDecimal(reader["GiaXuat"]) * Convert.ToInt32(reader["SoLuong"]);
                            dtSanPham.Rows.Add(row);
                        }
                    }
                }

                CapNhatTongTien();
            }
        }


        private void LoadDanhSachKhachHang()
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

                    // Thêm một dòng "-- Chọn khách hàng --" vào đầu combobox
                    DataRow row = dt.NewRow();
                    row["MaKH"] = -1;
                    row["HoTen"] = "-- Chọn khách hàng --";
                    dt.Rows.InsertAt(row, 0);

                    cboKhachHang.DataSource = dt;
                    cboKhachHang.DisplayMember = "HoTen";
                    cboKhachHang.ValueMember = "MaKH";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khách hàng: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachSanPham()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    // Lấy thông tin sản phẩm từ bảng SanPham
                    string sql = @"SELECT MaSP, TenSP FROM SanPham ORDER BY TenSP";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Thêm một dòng "-- Chọn sản phẩm --" vào đầu combobox
                    DataRow row = dt.NewRow();
                    row["MaSP"] = -1;
                    row["TenSP"] = "-- Chọn sản phẩm --";
                    dt.Rows.InsertAt(row, 0);

                    cboSanPham.DataSource = dt;
                    cboSanPham.DisplayMember = "TenSP";
                    cboSanPham.ValueMember = "MaSP";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongTinPhieuXuat()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT px.MaPhieu, px.NgayXuatHang, px.MaKH, kh.HoTen, kh.Sdt, 
                        kh.DiaChi, px.TrangThai, px.ThanhTien, px.GhiChu 
                        FROM PhieuXuatHang px 
                        INNER JOIN KhachHang kh ON px.MaKH = kh.MaKH
                        WHERE px.MaPhieu = @MaPhieu";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Hiển thị thông tin phiếu xuất
                            txtMaPhieu.Text = reader["MaPhieu"].ToString();
                            dtpNgayXuat.Value = Convert.ToDateTime(reader["NgayXuatHang"]);
                            cboKhachHang.SelectedValue = reader["MaKH"];
                            txtSDT.Text = reader["Sdt"].ToString();
                            txtDiaChi.Text = reader["DiaChi"].ToString();
                            
                            // Thiết lập trạng thái phiếu
                            string trangThai = reader["TrangThai"].ToString();
                            for (int i = 0; i < cboTrangThai.Items.Count; i++)
                            {
                                if (cboTrangThai.Items[i].ToString() == trangThai)
                                {
                                    cboTrangThai.SelectedIndex = i;
                                    break;
                                }
                            }

                            // Hiển thị tổng tiền
                            tongTien = Convert.ToDecimal(reader["ThanhTien"]);
                            lblTongTien.Text = string.Format(new CultureInfo("vi-VN"), "{0:N0} đ", tongTien);
                            
                            // Hiển thị ghi chú nếu có
                            if (reader["GhiChu"] != DBNull.Value)
                            {
                                txtGhiChu.Text = reader["GhiChu"].ToString();
                            }
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
                MessageBox.Show("Lỗi khi tải thông tin phiếu xuất: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachChiTietPhieuXuat()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.GiaXuat, sp.DonViTinh, 
                        (ct.SoLuong * ct.GiaXuat) as ThanhTien 
                        FROM ChiTietPhieuXuat ct
                        INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                        WHERE ct.MaPhieu = @MaPhieu";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow row = dtSanPham.NewRow();
                            row["MaSP"] = reader["MaSP"];
                            row["TenSP"] = reader["TenSP"];
                            row["SoLuong"] = reader["SoLuong"];
                            row["DonGia"] = reader["GiaXuat"];
                            row["DonViTinh"] = reader["DonViTinh"];
                            row["ThanhTien"] = reader["ThanhTien"];
                            dtSanPham.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách chi tiết phiếu xuất: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Xử lý khi thay đổi khách hàng
                if (cboKhachHang.SelectedIndex <= 0)
                {
                    txtSDT.Text = string.Empty;
                    txtDiaChi.Text = string.Empty;
                    return;
                }

                int maKH = Convert.ToInt32(cboKhachHang.SelectedValue);
                if (maKH <= 0) return;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT Sdt, DiaChi FROM KhachHang WHERE MaKH = @MaKH";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtSDT.Text = reader["Sdt"].ToString();
                            txtDiaChi.Text = reader["DiaChi"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin khách hàng: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem đã chọn sản phẩm chưa
                if (cboSanPham.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maSP = Convert.ToInt32(cboSanPham.SelectedValue);
                int soLuong = (int)numSoLuong.Value;

                // ✅ Thêm đoạn kiểm tra tồn kho
                int soLuongTonKho = 0;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sqlCheck = "SELECT SoLuong FROM SanPham WHERE MaSP = @MaSP";
                    SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                    cmdCheck.Parameters.AddWithValue("@MaSP", maSP);
                    object result = cmdCheck.ExecuteScalar();
                    if (result != null)
                        soLuongTonKho = Convert.ToInt32(result);
                }
                if (soLuong > soLuongTonKho)
                {
                    MessageBox.Show($"Chỉ còn {soLuongTonKho} sản phẩm trong kho. Không thể xuất {soLuong} sản phẩm!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Kiểm tra xem sản phẩm đã có trong danh sách chưa
                foreach (DataRow row in dtSanPham.Rows)
                {
                    if (Convert.ToInt32(row["MaSP"]) == maSP)
                    {
                        MessageBox.Show("Sản phẩm đã có trong danh sách! Vui lòng cập nhật số lượng trực tiếp trong bảng.", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Lấy thông tin sản phẩm từ DB
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT TenSP, GiaXuat, DonViTinh FROM SanPham WHERE MaSP = @MaSP";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string tenSP = reader["TenSP"].ToString();
                            decimal donGia = Convert.ToDecimal(reader["GiaXuat"]);
                            string donViTinh = reader["DonViTinh"].ToString();
                            decimal thanhTien = donGia * soLuong;

                            // Thêm sản phẩm vào DataTable
                            DataRow newRow = dtSanPham.NewRow();
                            newRow["MaSP"] = maSP;
                            newRow["TenSP"] = tenSP;
                            newRow["SoLuong"] = soLuong;
                            newRow["DonGia"] = donGia;
                            newRow["DonViTinh"] = donViTinh;
                            newRow["ThanhTien"] = thanhTien;
                            dtSanPham.Rows.Add(newRow);

                            // Cập nhật tổng tiền
                            CapNhatTongTien();

                            // Reset lại các control
                            cboSanPham.SelectedIndex = 0;
                            numSoLuong.Value = 1;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin sản phẩm!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý khi click vào nút Xóa trên DataGridView
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSanPham.Columns["colXoa"].Index)
            {
                // Xác nhận xóa sản phẩm
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // Xóa dòng được chọn
                    dtSanPham.Rows.RemoveAt(e.RowIndex);
                    CapNhatTongTien();
                }
            }
        }

        private void dgvSanPham_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Chỉ xử lý khi người dùng sửa cột số lượng
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSanPham.Columns["colSoLuong"].Index)
            {
                int soLuong;
                if (int.TryParse(dgvSanPham.Rows[e.RowIndex].Cells["colSoLuong"].Value.ToString(), out soLuong))
                {
                    if (soLuong <= 0)
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvSanPham.Rows[e.RowIndex].Cells["colSoLuong"].Value = 1;
                        soLuong = 1;
                    }

                    // Cập nhật thành tiền
                    decimal donGia = Convert.ToDecimal(dgvSanPham.Rows[e.RowIndex].Cells["colDonGia"].Value);
                    dgvSanPham.Rows[e.RowIndex].Cells["colThanhTien"].Value = donGia * soLuong;
                    
                    // Cập nhật tổng tiền
                    CapNhatTongTien();
                }
            }
        }

        private void dgvSanPham_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Kiểm tra chỉ nhập số ở cột số lượng
            if (e.ColumnIndex == dgvSanPham.Columns["colSoLuong"].Index && e.RowIndex >= 0)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int result) || result <= 0)
                {
                    dgvSanPham.Rows[e.RowIndex].ErrorText = "Số lượng phải là số nguyên dương";
                    e.Cancel = true;
                }
                else
                {
                    dgvSanPham.Rows[e.RowIndex].ErrorText = string.Empty;
                }
            }
        }

        private void dgvSanPham_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Đảm bảo chỉ nhập số ở cột số lượng
            if (dgvSanPham.CurrentCell.ColumnIndex == dgvSanPham.Columns["colSoLuong"].Index)
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress += (s, args) =>
                    {
                        if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar))
                        {
                            args.Handled = true;
                        }
                    };
                }
            }
        }

        private void CapNhatTongTien()
        {
            tongTien = 0;
            foreach (DataRow row in dtSanPham.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }

            // Hiển thị tổng tiền với định dạng tiền tệ VN
            lblTongTien.Text = string.Format(new CultureInfo("vi-VN"), "{0:N0} đ", tongTien);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (cboKhachHang.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKhachHang.Focus();
                    return;
                }

                if (dtSanPham.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboTrangThai.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái phiếu xuất!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboTrangThai.Focus();
                    return;
                }

                // Lấy thông tin từ form
                int maKH = Convert.ToInt32(cboKhachHang.SelectedValue);
                DateTime ngayXuat = dtpNgayXuat.Value;
                string trangThai = cboTrangThai.SelectedItem.ToString();
                string ghiChu = txtGhiChu.Text.Trim();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Nếu là tạo mới phiếu xuất
                        if (maPhieu <= 0)
                        {
                            // Thêm phiếu xuất mới
                            string insertPhieuXuatSql = @"INSERT INTO PhieuXuatHang (MaKH, NgayXuatHang, ThanhTien, TrangThai, GhiChu, TrangThaiThanhToan)
                                VALUES (@MaKH, @NgayXuat, @ThanhTien, @TrangThai, @GhiChu, 'Chưa thanh toán');
                                SELECT SCOPE_IDENTITY();";

                            SqlCommand cmd = new SqlCommand(insertPhieuXuatSql, conn, transaction);
                            cmd.Parameters.AddWithValue("@MaKH", maKH);
                            cmd.Parameters.AddWithValue("@NgayXuat", ngayXuat);
                            cmd.Parameters.AddWithValue("@ThanhTien", tongTien);
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                            
                            if (string.IsNullOrEmpty(ghiChu))
                                cmd.Parameters.AddWithValue("@GhiChu", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                            // Lấy mã phiếu xuất vừa thêm
                            maPhieu = Convert.ToInt32(cmd.ExecuteScalar());

                            // Thêm chi tiết phiếu xuất
                            foreach (DataRow row in dtSanPham.Rows)
                            {
                                int maSP = Convert.ToInt32(row["MaSP"]);
                                int soLuong = Convert.ToInt32(row["SoLuong"]);
                                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                                string donViTinh = row["DonViTinh"].ToString();

                                string insertChiTietSql = @"INSERT INTO ChiTietPhieuXuat 
                                (MaPhieu, MaSP, GiaXuat, SoLuong, DonViTinh)
                                VALUES (@MaPhieu, @MaSP, @GiaXuat, @SoLuong, @DonViTinh)";

                                SqlCommand cmdDetail = new SqlCommand(insertChiTietSql, conn, transaction);
                                cmdDetail.Parameters.AddWithValue("@MaPhieu", maPhieu);
                                cmdDetail.Parameters.AddWithValue("@MaSP", maSP);
                                cmdDetail.Parameters.AddWithValue("@GiaXuat", donGia);
                                cmdDetail.Parameters.AddWithValue("@SoLuong", soLuong);
                                cmdDetail.Parameters.AddWithValue("@DonViTinh", donViTinh);

                                cmdDetail.ExecuteNonQuery();
                            }

                        }
                        else
                        {
                            // Cập nhật phiếu xuất hiện có
                            string updatePhieuXuatSql = @"UPDATE PhieuXuatHang 
                                SET MaKH = @MaKH, NgayXuatHang = @NgayXuat, 
                                ThanhTien = @ThanhTien, TrangThai = @TrangThai, GhiChu = @GhiChu
                                WHERE MaPhieu = @MaPhieu";

                            SqlCommand cmd = new SqlCommand(updatePhieuXuatSql, conn, transaction);
                            cmd.Parameters.AddWithValue("@MaKH", maKH);
                            cmd.Parameters.AddWithValue("@NgayXuat", ngayXuat);
                            cmd.Parameters.AddWithValue("@ThanhTien", tongTien);
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                            
                            if (string.IsNullOrEmpty(ghiChu))
                                cmd.Parameters.AddWithValue("@GhiChu", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                            
                            cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);

                            cmd.ExecuteNonQuery();

                            // Xóa chi tiết phiếu xuất cũ
                            string deleteChiTietSql = "DELETE FROM ChiTietPhieuXuat WHERE MaPhieu = @MaPhieu";
                            SqlCommand cmdDeleteDetail = new SqlCommand(deleteChiTietSql, conn, transaction);
                            cmdDeleteDetail.Parameters.AddWithValue("@MaPhieu", maPhieu);
                            cmdDeleteDetail.ExecuteNonQuery();

                            // Thêm lại chi tiết phiếu xuất mới
                            foreach (DataRow row in dtSanPham.Rows)
                            {
                                int maSP = Convert.ToInt32(row["MaSP"]);
                                int soLuong = Convert.ToInt32(row["SoLuong"]);
                                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                                decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);
                                string donViTinh = row["DonViTinh"].ToString();

                                string insertChiTietSql = @"INSERT INTO ChiTietPhieuXuat 
                                    (MaPhieu, MaSP, GiaXuat, SoLuong, DonViTinh)
                                    VALUES (@MaPhieu, @MaSP, @GiaXuat, @SoLuong, @DonViTinh)";

                                SqlCommand cmdDetail = new SqlCommand(insertChiTietSql, conn, transaction);
                                cmdDetail.Parameters.AddWithValue("@MaPhieu", maPhieu);
                                cmdDetail.Parameters.AddWithValue("@MaSP", maSP);

                                cmdDetail.Parameters.AddWithValue("@GiaXuat", donGia);
                                cmdDetail.Parameters.AddWithValue("@SoLuong", soLuong);
                                cmdDetail.Parameters.AddWithValue("@DonViTinh", donViTinh);

                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction
                        transaction.Commit();
                        MessageBox.Show("Lưu phiếu xuất thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        // Rollback nếu có lỗi
                        transaction.Rollback();
                        throw new Exception("Lỗi khi lưu phiếu xuất: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Xác nhận hủy
            if (dtSanPham.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy phiếu xuất này?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
