using QLCuaHangBanhKeo.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FXemBangDuTru: Form
    {
        BangDuTruDAO bdtDAO = new BangDuTruDAO();
        PhieuNhapHangDAO pnhDAO = new PhieuNhapHangDAO();
        SanPhamDAO spDAO = new SanPhamDAO();
        public FXemBangDuTru()
        {
            InitializeComponent();
        }

        private void FXemBangDuTru_Load(object sender, EventArgs e)
        {
            dgvBangDuTru.DataSource = bdtDAO.LayDanhSachBangDuTru();
            // Load NhaCungCap hiển thị "MaNCC - TenNCC"
            DataTable dtNCC = spDAO.LayDanhSachNhaCungCap();

            // Tạo cột hiển thị kết hợp
            dtNCC.Columns.Add("MaVaTen", typeof(string));
            foreach (DataRow row in dtNCC.Rows)
            {
                row["MaVaTen"] = $"{row["MaNCC"]} - {row["HoTen"]}";
            }

            cbNCC.DataSource = dtNCC;
            cbNCC.DisplayMember = "MaVaTen"; 
            cbNCC.ValueMember = "MaNCC";     
        }

        private void dgvBangDuTru_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy mã bảng dự trù từ dòng được chọn
                int maBDT = Convert.ToInt32(dgvBangDuTru.Rows[e.RowIndex].Cells["MaBDT"].Value);

                // Gọi DAO để lấy chi tiết
                HienThiChiTietBangDuTru(maBDT);

                gbChiTiet.Text = $"Chi tiết bảng dự trù - Mã: {maBDT}";
            }
        }

        private void HienThiChiTietBangDuTru(int maBDT)
        {
            DataTable dtChiTiet = bdtDAO.LayChiTietBangDuTruVoiSoLuong(maBDT);
            dgvChiTiet.DataSource = dtChiTiet;
            if (!dgvChiTiet.Columns.Contains("MaVaTenNCC"))
                dgvChiTiet.Columns.Add("MaVaTenNCC", "Nhà cung cấp");

            DataTable dtNCC = spDAO.LayDanhSachNhaCungCap();

            // Tạo cột hiển thị kết hợp
            dtNCC.Columns.Add("MaVaTen", typeof(string));
            foreach (DataRow row in dtNCC.Rows)
            {
                row["MaVaTen"] = $"{row["MaNCC"]} - {row["HoTen"]}";
            }

            cbNCC.DataSource = dtNCC;
            cbNCC.DisplayMember = "MaVaTen"; // Cột hiển thị
            cbNCC.ValueMember = "MaNCC";     // Cột lấy giá trị thật

        }

        private void dgvChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChiTiet.Rows[e.RowIndex];

                //txtTenSP.Text = row.Cells["TenSP"].Value?.ToString();
                numGiaNhap.Value = Convert.ToDecimal(row.Cells["GiaNhap"].Value);
                numSoLuong.Value = Convert.ToInt32(row.Cells["SoLuongNhap"].Value); // hoặc SoLuongSP tùy yêu cầu
                cbDonViTinh.Text = row.Cells["DonViTinh"].Value?.ToString();
                //cbTinhTrang.Text = row.Cells["TinhTrang"].Value?.ToString();
                //dtpHanSuDung.Value = Convert.ToDateTime(row.Cells["HanSuDung"].Value);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow != null)
            {
                DataGridViewRow row = dgvChiTiet.CurrentRow;

                row.Cells["GiaNhap"].Value = numGiaNhap.Value;
                row.Cells["SoLuongNhap"].Value = numSoLuong.Value;
                row.Cells["DonViTinh"].Value = cbDonViTinh.Text;
                row.Cells["MaNCC"].Value = cbNCC.SelectedValue;

                MessageBox.Show("Đã cập nhật thông tin sản phẩm cho phiếu nhập.");
            }
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu chi tiết để tạo phiếu nhập.");
                return;
            }

            string trangThai = "Chưa xử lý";
            DateTime ngayNhap = DateTime.Now;

            // Gom nhóm theo MaNCC
            var nhomTheoNCC = new Dictionary<int, List<DataGridViewRow>>();

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.IsNewRow) continue;

                if (!int.TryParse(row.Cells["MaNCC"].Value?.ToString(), out int maNCC))
                    continue;

                if (!nhomTheoNCC.ContainsKey(maNCC))
                    nhomTheoNCC[maNCC] = new List<DataGridViewRow>();

                nhomTheoNCC[maNCC].Add(row);
            }

            if (nhomTheoNCC.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu hợp lệ theo nhà cung cấp.");
                return;
            }

            // Tạo phiếu nhập cho từng nhà cung cấp
            foreach (var kvp in nhomTheoNCC)
            {
                int maNCC = kvp.Key;
                List<DataGridViewRow> dsSanPham = kvp.Value;

                double tongTien = dsSanPham.Sum(r =>
                {
                    double gia = Convert.ToDouble(r.Cells["GiaNhap"].Value);
                    int sl = Convert.ToInt32(r.Cells["SoLuongNhap"].Value);
                    return gia * sl;
                });

                int maPhieuMoi = pnhDAO.TaoPhieuNhap(maNCC, tongTien, ngayNhap, trangThai);

                foreach (var row in dsSanPham)
                {
                    int maSP = Convert.ToInt32(row.Cells["MaSP"].Value);
                    double giaNhap = Convert.ToDouble(row.Cells["GiaNhap"].Value);
                    int soLuong = Convert.ToInt32(row.Cells["SoLuongNhap"].Value);
                    string donViTinh = row.Cells["DonViTinh"].Value?.ToString();

                    pnhDAO.ThemChiTietPhieuNhap(maPhieuMoi, maSP, giaNhap, soLuong, donViTinh);
                }
            }

            MessageBox.Show("Đã tạo phiếu nhập cho từng nhà cung cấp thành công!");
        }

        private void btnTaoPhieuNhapMoi_Click(object sender, EventArgs e)
        {
            FTaoPhieuNhapHang f = new FTaoPhieuNhapHang();
            f.Show();
        }
    }
}
