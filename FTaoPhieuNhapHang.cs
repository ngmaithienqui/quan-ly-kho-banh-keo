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
    public partial class FTaoPhieuNhapHang : Form
    {
        SanPhamDAO spDAO = new SanPhamDAO();
        PhieuNhapHangDAO pnhDAO = new PhieuNhapHangDAO();
        int maSanPhamDangChinhSua = -1;
        int rowDangChinhSua = -1;
        private int nhaCungCapDangChon = -1; // Nhà cung cấp hiện tại của phiếu
        private bool dangThayDoiNCC = false;
        public FTaoPhieuNhapHang()
        {
            InitializeComponent();
            LoadSanPham();
            LoadComboBoxNCC();
        }

        private void FTaoPhieuNhapHang_Load(object sender, EventArgs e)
        {
            dgvPhieuNhapHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieuNhapHang.MultiSelect = false; // Chỉ cho chọn 1 dòng

            DataTable dtNCC = spDAO.LayDanhSachNhaCungCap();
            cbNCC.DataSource = dtNCC;
            cbNCC.DisplayMember = "HoTen";
            cbNCC.ValueMember = "MaNCC";
            dgvPhieuNhapHang.Columns.Clear();
            dgvPhieuNhapHang.Columns.Add("MaSanPham", "Mã sản phẩm");
            dgvPhieuNhapHang.Columns.Add("SoLuong", "Số lượng");
            dgvPhieuNhapHang.Columns.Add("GiaNhap", "Giá nhập");
            dgvPhieuNhapHang.Columns.Add("DonViTinh", "Đơn vị tính");
            dgvPhieuNhapHang.Columns.Add("NhaCungCap", "Nhà cung cấp");
            dgvPhieuNhapHang.Columns.Add("ThanhTien", "Thành tiền");
            if (cbNCC.Items.Count > 0)
            {
                cbNCC.SelectedIndex = 0; // chọn NCC đầu tiên
                nhaCungCapDangChon = Convert.ToInt32(cbNCC.SelectedValue);

                DataTable dt = spDAO.LaySanPhamTheoMa(nhaCungCapDangChon);
                dgvDanhSachSP.DataSource = dt;
            }
            cbNCC.SelectedIndex = 0; // chọn nhà cung cấp đầu tiên

            //dgvDanhSachSP.Columns["MaNCC"].Visible = false;
            //dgvDanhSachSP.Columns["MaSp"].Visible = false;
            LoadSanPham();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhapHang.CurrentRow != null && !dgvPhieuNhapHang.CurrentRow.IsNewRow)
            {
                dgvPhieuNhapHang.Rows.RemoveAt(dgvPhieuNhapHang.CurrentRow.Index);
                CapNhatTongTien();
                ResetNhapLieu();
                rowDangChinhSua = -1;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (rowDangChinhSua >= 0 && rowDangChinhSua < dgvPhieuNhapHang.Rows.Count)
            {
                var selectedRow = dgvPhieuNhapHang.Rows[rowDangChinhSua];

                selectedRow.Cells[0].Value = maSanPhamDangChinhSua;
                selectedRow.Cells[1].Value = (int)numSoLuong.Value;
                selectedRow.Cells[2].Value = (double)numGiaNhap.Value;
                selectedRow.Cells[3].Value = cbDonViTinh.Text;
                selectedRow.Cells[4].Value = Convert.ToInt32(cbNCC.SelectedValue);
                selectedRow.Cells[5].Value = (double)numGiaNhap.Value * (int)numSoLuong.Value;

                CapNhatTongTien();
                ResetNhapLieu();
                rowDangChinhSua = -1; // Reset sau khi cập nhật
            }
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if (maSanPhamDangChinhSua == -1) return;

            // Nếu đang sửa dòng thì không cho thêm, yêu cầu cập nhật
            if (rowDangChinhSua != -1)
            {
                MessageBox.Show("Bạn đang chỉnh sửa sản phẩm, hãy cập nhật trước khi thêm mới!");
                return;
            }

            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dgvPhieuNhapHang);

            newRow.Cells[0].Value = maSanPhamDangChinhSua;
            newRow.Cells[1].Value = (int)numSoLuong.Value;
            newRow.Cells[2].Value = (double)numGiaNhap.Value;
            newRow.Cells[3].Value = cbDonViTinh.Text;
            newRow.Cells[4].Value = Convert.ToInt32(cbNCC.SelectedValue);
            newRow.Cells[5].Value = (double)numGiaNhap.Value * (int)numSoLuong.Value;

            dgvPhieuNhapHang.Rows.Add(newRow);

            ResetNhapLieu();
            CapNhatTongTien();
        }

        private void btnTaoBang_Click(object sender, EventArgs e)
        {
            if (cbNCC.SelectedValue != null && int.TryParse(cbNCC.SelectedValue.ToString(), out int maNCC))
            {
                TaoPhieuNhap(maNCC);

                // Sau khi tạo phiếu thì dọn sạch bảng và reset lại
                dgvPhieuNhapHang.Rows.Clear();
                ResetNhapLieu();
                CapNhatTongTien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp hợp lệ trước khi tạo phiếu nhập!");
            }
        }

        private void cbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangThayDoiNCC) return;

            if (cbNCC.SelectedValue != null && int.TryParse(cbNCC.SelectedValue.ToString(), out int maNCCMoi))
            {
                bool coSanPhamTrongPhieu = dgvPhieuNhapHang.Rows
                .Cast<DataGridViewRow>()
                .Any(r => !r.IsNewRow && r.Cells[0].Value != null);

                if (maNCCMoi != nhaCungCapDangChon && coSanPhamTrongPhieu)
                {
                    DialogResult result = MessageBox.Show(
                        "Bạn đang có sản phẩm trong phiếu.\n\n" +
                        "Yes - Tạo phiếu hiện tại và xóa dữ liệu\n" +
                        "No - Xóa dữ liệu hiện tại\n" +
                        "Cancel - Giữ nguyên phiếu và không thay đổi nhà cung cấp",
                        "Thay đổi nhà cung cấp",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        TaoPhieuNhap(nhaCungCapDangChon);
                        dgvPhieuNhapHang.Rows.Clear();
                        ResetNhapLieu();
                        CapNhatTongTien();
                    }
                    else if (result == DialogResult.No)
                    {
                        dgvPhieuNhapHang.Rows.Clear();
                        ResetNhapLieu();
                        CapNhatTongTien();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        dangThayDoiNCC = true;
                        cbNCC.SelectedValue = nhaCungCapDangChon;
                        dangThayDoiNCC = false;
                        return;
                    }
                }

                // ✅ Sau khi xử lý xong mới gán lại NCC đang chọn
                nhaCungCapDangChon = maNCCMoi;
                LoadSanPham();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            // Gọi đến DAO để tìm kiếm theo tên sản phẩm
            DataTable dt = spDAO.TimKiemSanPhamTheoTen(keyword);

            dgvDanhSachSP.DataSource = dt;
        }

        private void dgvPhieuNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPhieuNhapHang.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvPhieuNhapHang.Rows[e.RowIndex];

                rowDangChinhSua = e.RowIndex; // ➔ thêm dòng này

                if (selectedRow.Cells[0].Value != null)
                    maSanPhamDangChinhSua = Convert.ToInt32(selectedRow.Cells[0].Value);

                if (selectedRow.Cells[1].Value != null)
                    numSoLuong.Value = Convert.ToInt32(selectedRow.Cells[1].Value);

                if (selectedRow.Cells[2].Value != null)
                    numGiaNhap.Value = Convert.ToDecimal(selectedRow.Cells[2].Value);

                if (selectedRow.Cells[3].Value != null)
                    cbDonViTinh.Text = selectedRow.Cells[3].Value.ToString();

                if (selectedRow.Cells[4].Value != null)
                    cbNCC.SelectedValue = Convert.ToInt32(selectedRow.Cells[4].Value);
            }
        }
        private void LoadComboBoxNCC()
        {
            DataTable dtNCC = spDAO.LayDanhSachNhaCungCap();

            dtNCC.Columns.Add("TenHienThi", typeof(string));
            foreach (DataRow row in dtNCC.Rows)
            {
                row["TenHienThi"] = row["HoTen"] + " (" + row["MaNCC"] + ")";
            }

            cbNCC.DataSource = dtNCC;
            cbNCC.DisplayMember = "TenHienThi";
            cbNCC.ValueMember = "MaNCC";
        }

        private void LoadSanPham()
        {
            if (cbNCC.SelectedItem != null)
            {
                DataRowView selectedNCC = cbNCC.SelectedItem as DataRowView;
                int maNCC = Convert.ToInt32(selectedNCC["MaNCC"]);

                // ❌ Không cần tạo mới spDAO
                DataTable dt = spDAO.LaySanPhamTheoMa(maNCC);
                dgvDanhSachSP.DataSource = dt;
            }
        }

        private void ResetNhapLieu()
        {
            maSanPhamDangChinhSua = -1;
            numSoLuong.Value = 1;
            numGiaNhap.Value = 0;
            cbDonViTinh.SelectedIndex = -1;
            rowDangChinhSua = -1;
        }

        private void CapNhatTongTien()
        {
            double tongTien = 0;
            foreach (DataGridViewRow row in dgvPhieuNhapHang.Rows)
            {
                if (row.IsNewRow) continue;
                tongTien += Convert.ToDouble(row.Cells[5].Value);
            }
            lblTongTien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
        }
        private void TaoPhieuNhap(int maNCC)
        {
            if (dgvPhieuNhapHang.Rows.Cast<DataGridViewRow>().All(r => r.IsNewRow))
            {
                MessageBox.Show("Chưa có sản phẩm nào trong phiếu nhập!");
                return;
            }

            string trangThai = "Chưa nhập hàng";
            DateTime ngayNhap = DateTime.Now;
            double tongTien = 0;

            var chiTietPhieu = new List<(int MaSP, double GiaNhap, int SoLuong, string DonViTinh)>();

            foreach (DataGridViewRow row in dgvPhieuNhapHang.Rows)
            {
                if (row.IsNewRow || IsEmptyRow(row)) continue;

                if (!TryGetCellValue(row, 0, out int maSP) ||
                    !TryGetCellValue(row, 1, out int soLuong) ||
                    !TryGetCellValue(row, 2, out double giaNhap) ||
                    row.Cells[3].Value == null)
                {
                    MessageBox.Show("Có dòng dữ liệu không hợp lệ, vui lòng kiểm tra lại!");
                    return;
                }

                string donViTinh = row.Cells[3].Value.ToString();
                tongTien += giaNhap * soLuong;

                chiTietPhieu.Add((maSP, giaNhap, soLuong, donViTinh));
            }

            if (chiTietPhieu.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm hợp lệ trong phiếu nhập!");
                return;
            }

            int maPhieuMoi = pnhDAO.TaoPhieuNhap(maNCC, tongTien, ngayNhap, trangThai);
            if (maPhieuMoi <= 0)
            {
                MessageBox.Show("Tạo phiếu nhập thất bại!");
                return;
            }

            foreach (var item in chiTietPhieu)
            {
                pnhDAO.ThemChiTietPhieuNhap(maPhieuMoi, item.MaSP, item.GiaNhap, item.SoLuong, item.DonViTinh);
            }

            MessageBox.Show("Tạo phiếu nhập thành công!");
        }

        // Helper: kiểm tra dòng trống
        private bool IsEmptyRow(DataGridViewRow row)
        {
            return row.Cells.Cast<DataGridViewCell>().All(c => c.Value == null || string.IsNullOrWhiteSpace(c.Value.ToString()));
        }

        // Helper: lấy dữ liệu kiểu int/double an toàn
        private bool TryGetCellValue<T>(DataGridViewRow row, int cellIndex, out T value)
        {
            value = default;
            try
            {
                if (row.Cells[cellIndex].Value == null) return false;
                value = (T)Convert.ChangeType(row.Cells[cellIndex].Value, typeof(T));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void dgvDanhSachSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDanhSachSP.Rows[e.RowIndex];

                int maNCCSanPham = Convert.ToInt32(row.Cells["MaNCC"].Value);

                // 👇 Dùng flag để tạm thời tắt xử lý trong SelectedIndexChanged
                dangThayDoiNCC = true;
                cbNCC.SelectedValue = row.Cells["MaNCC"].Value;
                dangThayDoiNCC = false;

                // Kiểm tra nếu sản phẩm thuộc NCC khác với phiếu hiện tại


                maSanPhamDangChinhSua = Convert.ToInt32(row.Cells["MaSP"].Value);
                numSoLuong.Value = 1;
                numGiaNhap.Value = Convert.ToDecimal(row.Cells["GiaNhap"].Value);
                cbDonViTinh.Text = row.Cells["DonViTinh"].Value.ToString();
                cbNCC.SelectedValue = row.Cells["MaNCC"].Value;
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
