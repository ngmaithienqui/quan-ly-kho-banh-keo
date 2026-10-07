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
using QLCuaHangBanhKeo.DAO;

namespace QLCuaHangBanhKeo
{
    public partial class FNVXemChiTietPhieuXuat : Form
    {
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;
        private int maDonHang = -1;
        private bool isViewOnly = false;
        private DataTable dtChiTietDonHang = new DataTable();
        private bool isPhieuXuat = false;
        private readonly PhieuXuatDAO phieuXuatDAO = new PhieuXuatDAO();
        private readonly NVXemPhieuXuatDAO pnhDAO = new NVXemPhieuXuatDAO();

        private int selectedMaPhieu = -1;

       
     
      
        private DataTable dtNVXemChiTietPhieuXuat = new DataTable();
      

        // Constructor for adding a new order
        public FNVXemChiTietPhieuXuat()
        {
            InitializeComponent();
        }

        // Constructor for editing or viewing an order
        public FNVXemChiTietPhieuXuat(int maDonHang, bool isViewOnly = false)
        {
            InitializeComponent();
            this.maDonHang = maDonHang;
            this.isViewOnly = isViewOnly;
        }

        // Constructor for viewing a PhieuXuatHang
        // Constructor cho PHIẾU XUẤT
        public FNVXemChiTietPhieuXuat(int maPhieuXuat, bool isFromPhieuXuat, bool dummy = false)
        {
            InitializeComponent();
            this.maDonHang = maPhieuXuat;
            this.isViewOnly = true;
            this.isPhieuXuat = isFromPhieuXuat;
        }

        private void SetupDataGridViewForNewOrder()
        {
            // Clear any existing data
            dtChiTietDonHang.Clear();
        }
        private void FNVXemChiTietPhieuXuat_Load(object sender, EventArgs e)
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

                SetupDataGridViewForNewOrder();
            }
        }
     

        private void LoadChiTietDonHang()
        {
        }
        private void InitializeForm()
        {
            dtNVXemChiTietPhieuXuat.Columns.Add("MaSP", typeof(int));
            dtNVXemChiTietPhieuXuat.Columns.Add("TenSP", typeof(string));
            dtNVXemChiTietPhieuXuat.Columns.Add("DonViTinh", typeof(string));
            dtNVXemChiTietPhieuXuat.Columns.Add("SoLuong", typeof(int));
            dtNVXemChiTietPhieuXuat.Columns.Add("DonGia", typeof(decimal));
            dtNVXemChiTietPhieuXuat.Columns.Add("ThanhTien", typeof(decimal));

            dgvChiTietPheuXuat.DataSource = dtNVXemChiTietPhieuXuat;
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
                           
                            txtGhiChu.Text = reader["GhiChu"].ToString();

                            // Set selected customer
                            
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

        

        

        private void SetReadOnlyMode()
        {
            // Disable all controls for editing
            
            dtpNgayDat.Enabled = false;
              txtGhiChu.ReadOnly = true;
            
          

            // Set the form title
            this.Text = "Xem chi tiết đơn hàng";
            lblTitle.Text = "XEM CHI TIẾT ĐƠN HÀNG";
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

       

        private void dgvChiTietDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grpInfo_Enter(object sender, EventArgs e)
        {

        }

        private void LoadPhieuXuat()
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
                        txtGhiChu.Text = reader["GhiChu"]?.ToString();
                        //cboKhachHang.SelectedValue = Convert.ToInt32(reader["MaKH"]);
                        txtTongTien.Text = string.Format("{0:N0} VNĐ", Convert.ToDecimal(reader["ThanhTien"]));
                    }
                }
            }
        }

        private void LoadChiTietPhieuXuat()
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

                dtNVXemChiTietPhieuXuat.Clear();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtNVXemChiTietPhieuXuat);

                dgvChiTietPheuXuat.AutoGenerateColumns = false;
                dgvChiTietPheuXuat.DataSource = dtNVXemChiTietPhieuXuat;

                CalculateTotalAmount();
            }
        }


      

        private void FNVChiTietPhieuXuat_Load_1(object sender, EventArgs e)
        {
            FNVXemChiTietPhieuXuat_Load(sender, e); // Gọi lại hàm Load chính
        }
        private void btnXuatThanhCong_Click(object sender, EventArgs e)
        {
            if (maDonHang <= 0)
            {
                MessageBox.Show("Không tìm thấy mã phiếu xuất!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NVXemPhieuXuatDAO dao = new NVXemPhieuXuatDAO();

            // Gọi hàm mới CapNhatTrangThaiXuatHang
            if (dao.CapNhatTrangThaiXuatHang(maDonHang))
            {
                MessageBox.Show("Xuất hàng thành công và đã cập nhật tồn kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi xuất hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnXuatThatBai_Click(object sender, EventArgs e)
        {
            if(maDonHang > 0)
    {
                PhieuXuatDAO dao = new PhieuXuatDAO();
                dao.CapNhatTrangThai(maDonHang, "Chờ xử lý");

                // Mở form nhập lý do thất bại
                FNVXuatHangThatBai form = new FNVXuatHangThatBai();
                form.MaPhieuXuat = maDonHang;
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    MessageBox.Show("Đã lưu lý do thất bại thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ Quan trọng: Trả về OK cho form mẹ
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }


        }

    }
}
