// FNVXemPhieuNhapHang.cs
using QLCuaHangBanhKeo.DAO;

using System;
using System.Data;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FNVXemPhieuNhapHang : Form
    {
        NVXemPhieuNhapDAO nv = new NVXemPhieuNhapDAO();
        ThongBaoDAO tbDAO = new ThongBaoDAO();
        PhieuNhapHangDAO pnhDAO = new PhieuNhapHangDAO();
        public FNVXemPhieuNhapHang()
        {
            InitializeComponent();
        }

        private void FXemPhieuNhapHang_Load(object sender, EventArgs e)
        {
            dgvPhieuNhap.DataSource = pnhDAO.LayDanhSachPhieuNhap();
        }
        private void btnNhapHangThatBai_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.CurrentRow != null)
            {
                int maPhieu = Convert.ToInt32(dgvPhieuNhap.CurrentRow.Cells["MaPhieu"].Value);
                string trangThaiHienTai = dgvPhieuNhap.CurrentRow.Cells["TrangThai"].Value?.ToString();

                if (trangThaiHienTai == "Đã nhận hàng")
                {
                    MessageBox.Show("Phiếu nhập đã nhận hàng, không thể chuyển về Chờ xử lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    pnhDAO.CapNhatTrangThai(maPhieu, "Chờ xử lý", "Chưa thanh toán");

                    // Bỏ gọi ThemThongBaoNhap
                    // Tạo 1 thông báo mới
                    DateTime ngayThongBao = DateTime.Now;
                    string noiDung = $"Phiếu nhập #{maPhieu} chuyển sang Chờ xử lý!";
                    int maThongBao = tbDAO.ThemThongBaoSanPham(ngayThongBao, noiDung);

                    // Mở form thất bại
                    FNVNhapHangThatBai formThatBai = new FNVNhapHangThatBai();
                    formThatBai.MaPhieuNhap = maPhieu;
                    DialogResult result = formThatBai.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        MessageBox.Show("Đã lưu lý do thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa nhập lý do thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    dgvPhieuNhap.DataSource = pnhDAO.LayDanhSachPhieuNhap();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvPhieuNhap.Rows[e.RowIndex];
                if (selectedRow.Cells["MaPhieu"] != null && selectedRow.Cells["MaPhieu"].Value != DBNull.Value)
                {
                    int maPhieu = Convert.ToInt32(selectedRow.Cells["MaPhieu"].Value);
                    dgvChiTietPN.DataSource = new NVXemPhieuNhapDAO().LayChiTietPhieuNhap(maPhieu);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Mã Phiếu trong dòng được chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HienThiChiTietPhieuNhap(int maPN)
        {
            DataTable dtChiTiet = pnhDAO.LayChiTietPhieuNhap(maPN);
            dgvChiTietPN.DataSource = dtChiTiet;
        }
        private void dgvChiTietPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maSP = Convert.ToInt32(dgvChiTietPN.Rows[e.RowIndex].Cells["MaSP"].Value);
                string tenSP = dgvChiTietPN.Rows[e.RowIndex].Cells["TenSP"].Value.ToString();
                string soLuong = dgvChiTietPN.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
                string donGia = dgvChiTietPN.Rows[e.RowIndex].Cells["DonGia"].Value.ToString();
                MessageBox.Show($"Mã SP: {maSP}\nTên SP: {tenSP}\nSố lượng: {soLuong}\nĐơn giá: {donGia}", "Thông tin sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnNhapHangThanhCong_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.CurrentRow != null)
            {
                int maPN = Convert.ToInt32(dgvPhieuNhap.CurrentRow.Cells["MaPhieu"].Value);
                string trangThaiMoi = "Đã nhập hàng";

                // Gọi DAO để cập nhật trạng thái
                bool ok = NVXemPhieuNhapDAO.CapNhatTrangThaiNhapHang(maPN);


                if (ok)
                {
                    MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật trạng thái thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dgvPhieuNhap.DataSource = pnhDAO.LayDanhSachPhieuNhap(); // refresh lại

            }
        }


    }
}