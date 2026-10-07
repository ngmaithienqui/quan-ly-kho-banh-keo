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
    public partial class FDonDatHang : Form
    {
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;
        private int selectedMaDonHang = -1;

        public FDonDatHang()
        {
            InitializeComponent();
        }

        private void FDonDatHang_Load(object sender, EventArgs e)
        {
            // Thiết lập mặc định cho combobox trạng thái
            cboTrangThai.SelectedIndex = 0;

            // Khởi tạo datepicker mặc định
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;

            // Tải dữ liệu đơn đặt hàng
            LoadDonDatHang();
        }

        private void LoadDonDatHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Câu SQL JOIN và tính tổng tiền
                    string sql = @"
                SELECT 
                    dh.MaPhieu AS MaDonHang,
                    dh.NgayDat,
                    kh.HoTen AS TenKH,
                    dh.TrangThai,
                    ISNULL(SUM(ct.SoLuong * sp.GiaNhap), 0) AS ThanhTien
                FROM PhieuDatHang dh
                LEFT JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                LEFT JOIN ChiTietPhieuDat ct ON dh.MaPhieu = ct.MaPhieu
                LEFT JOIN SanPham sp ON ct.MaSP = sp.MaSP
            ";

                    // Lọc theo trạng thái
                    if (cboTrangThai.SelectedIndex > 0)
                    {
                        sql += " WHERE dh.TrangThai = @TrangThai";
                    }

                    // Lọc theo ngày
                    if (cboTrangThai.SelectedIndex == 0)
                        sql += " WHERE";
                    else
                        sql += " AND";

                    sql += " dh.NgayDat BETWEEN @TuNgay AND @DenNgay";
                    sql += " GROUP BY dh.MaPhieu, dh.NgayDat, kh.HoTen, dh.TrangThai";
                    sql += " ORDER BY dh.NgayDat DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (cboTrangThai.SelectedIndex > 0)
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedItem.ToString());
                        }

                        cmd.Parameters.AddWithValue("@TuNgay", dtpTuNgay.Value.Date);
                        cmd.Parameters.AddWithValue("@DenNgay", dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1));

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            dgvDonDatHang.AutoGenerateColumns = false;
                            dgvDonDatHang.DataSource = dt;

                            // Gán ánh xạ cột
                            dgvDonDatHang.Columns["colMaDonHang"].DataPropertyName = "MaDonHang";
                            dgvDonDatHang.Columns["colNgayDat"].DataPropertyName = "NgayDat";
                            dgvDonDatHang.Columns["colKhachHang"].DataPropertyName = "TenKH";
                            dgvDonDatHang.Columns["colTrangThai"].DataPropertyName = "TrangThai";
                            dgvDonDatHang.Columns["colThanhTien"].DataPropertyName = "ThanhTien";

                            dgvDonDatHang.Columns["colThanhTien"].DefaultCellStyle.Format = "N0";

                            lblNoData.Visible = false;
                            dgvDonDatHang.Visible = true;
                        }
                        else
                        {
                            dgvDonDatHang.DataSource = null;
                            lblNoData.Visible = true;
                            dgvDonDatHang.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDonDatHang();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            LoadDonDatHang();
        }

        private void dgvDonDatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDonDatHang.Rows[e.RowIndex];
                selectedMaDonHang = Convert.ToInt32(row.Cells["colMaDonHang"].Value);
                
                // Bật tắt nút tùy theo trạng thái đơn hàng
                string trangThai = row.Cells["colTrangThai"].Value.ToString();
                btnSua.Enabled = trangThai != "Đã hoàn thành" && trangThai != "Đã hủy";
                btnXoa.Enabled = trangThai != "Đã hoàn thành";
                btnXuatPhieu.Enabled = trangThai != "Đã hủy";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Mở form tạo đơn hàng mới
            FChiTietDonHang frmChiTietDonHang = new FChiTietDonHang();
            if (frmChiTietDonHang.ShowDialog() == DialogResult.OK)
            {
                // Sau khi thêm đơn hàng thành công, tải lại dữ liệu
                LoadDonDatHang();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaDonHang <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần chỉnh sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form chỉnh sửa đơn hàng với ID đã chọn
            FChiTietDonHang frmChiTietDonHang = new FChiTietDonHang(selectedMaDonHang);
            if (frmChiTietDonHang.ShowDialog() == DialogResult.OK)
            {
                // Sau khi sửa đơn hàng thành công, tải lại dữ liệu
                LoadDonDatHang();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaDonHang <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa đơn hàng này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        
                        // Kiểm tra trạng thái đơn hàng trước khi xóa
                        string checkSql = "SELECT TrangThai FROM PhieuDatHang WHERE MaPhieu = @MaPhieu";
                        using (SqlCommand cmdCheck = new SqlCommand(checkSql, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@MaPhieu", selectedMaDonHang);
                            string trangThai = cmdCheck.ExecuteScalar()?.ToString();
                            
                            if (trangThai == "Đã hoàn thành")
                            {
                                MessageBox.Show("Không thể xóa đơn hàng đã hoàn thành!", "Thông báo", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        
                        // Xóa chi tiết đơn hàng trước
                        string deleteDetailSql = "DELETE FROM ChiTietPhieuDat WHERE MaPhieu = @MaPhieu";
                        using (SqlCommand cmdDetail = new SqlCommand(deleteDetailSql, conn))
                        {
                            cmdDetail.Parameters.AddWithValue("@MaPhieu", selectedMaDonHang);
                            cmdDetail.ExecuteNonQuery();
                        }
                        
                        // Sau đó xóa đơn hàng chính
                        string deleteSql = "DELETE FROM PhieuDatHang WHERE MaPhieu = @MaPhieu";
                        using (SqlCommand cmd = new SqlCommand(deleteSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaPhieu", selectedMaDonHang);
                            int result = cmd.ExecuteNonQuery();
                            
                            if (result > 0)
                            {
                                MessageBox.Show("Xóa đơn hàng thành công!", "Thông báo", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDonDatHang();
                                selectedMaDonHang = -1;
                            }
                            else
                            {
                                MessageBox.Show("Không thể xóa đơn hàng!", "Thông báo", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa đơn hàng: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (selectedMaDonHang <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xem chi tiết!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Mở form xem chi tiết đơn hàng ở chế độ chỉ đọc
            FChiTietDonHang frmChiTietDonHang = new FChiTietDonHang(selectedMaDonHang, true);
            frmChiTietDonHang.ShowDialog();


        }

        private void btnXuatPhieu_Click(object sender, EventArgs e)
        {
            if (selectedMaDonHang <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để tạo phiếu xuất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi form tạo phiếu xuất từ đơn đặt hàng
            FLapPhieuXuat frmLapPhieuXuat = new FLapPhieuXuat(selectedMaDonHang, true); // 'true' để kích hoạt chế độ từ đơn hàng
            if (frmLapPhieuXuat.ShowDialog() == DialogResult.OK)
            {
                LoadDonDatHang(); // Refresh lại đơn đặt hàng nếu cần
            }
        }


        private void dgvDonDatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
