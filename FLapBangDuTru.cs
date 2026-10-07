using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
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
    public partial class FLapBangDuTru : Form
    {
        SanPhamDAO spDAO = new SanPhamDAO();
        BangDuTruDAO bdtDAO = new BangDuTruDAO();
        private int maSanPhamDangChinhSua;
        public FLapBangDuTru()
        {
            InitializeComponent();
        }

        private void FLapBangDuTru_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            numSLToiThieu.Value = 20;
            btnTaoBangDuTru.Enabled = false;
            dtpNgayHienTai.Enabled = false;
        }

        private void LoadSanPham()
        {
            DataTable dt = spDAO.HienCot();

            // Thêm các cột phụ nếu chưa tồn tại
            if (!dt.Columns.Contains("SoLuongThieu"))
                dt.Columns.Add("SoLuongThieu", typeof(int));
            if (!dt.Columns.Contains("SanPhamPhieu"))
                dt.Columns.Add("SanPhamPhieu", typeof(string)); // hoặc kiểu phù hợp

            dgvDanhSachSP.DataSource = dt;

            // Ẩn các cột bổ sung cho đến khi cần
            if (dgvDanhSachSP.Columns.Contains("SoLuongThieu"))
                dgvDanhSachSP.Columns["SoLuongThieu"].Visible = false;

            if (dgvDanhSachSP.Columns.Contains("SanPhamPhieu"))
                dgvDanhSachSP.Columns["SanPhamPhieu"].Visible = false;
        }

        private void btnXacNhanSL_Click(object sender, EventArgs e)
        {
            if (int.TryParse(numSLToiThieu.Text, out int soLuongMin))
            {
                DataTable dt = spDAO.LaySanPhamTonKhoLonHon(soLuongMin);

                // Thêm các cột nếu chưa có (phòng ngừa)
                if (!dt.Columns.Contains("SoLuongThieu"))
                    dt.Columns.Add("SoLuongThieu", typeof(int));
                if (!dt.Columns.Contains("SanPhamPhieu"))
                    dt.Columns.Add("SanPhamPhieu", typeof(string));

                dgvDanhSachSP.DataSource = dt;

                dgvDanhSachSP.Columns["SoLuongThieu"].Visible = true;
                dgvDanhSachSP.Columns["SoLuongThieu"].DisplayIndex = dgvDanhSachSP.Columns["SoLuong"].DisplayIndex + 1;

                dgvDanhSachSP.Columns["MaNCC"].Visible = false;
                dgvDanhSachSP.Columns["MaDanhMuc"].Visible = false;
                dgvDanhSachSP.Columns["SanPhamPhieu"].Visible = false;

                btnTaoBangDuTru.Enabled = true;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số lượng tối thiểu hợp lệ.", "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dgvDanhSachSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachSP.Rows[e.RowIndex];
                maSanPhamDangChinhSua = Convert.ToInt32(row.Cells["MaSP"].Value);

                object value = row.Cells["SoLuongThieu"].Value;

                if (value != DBNull.Value && value != null)
                {
                    numChinhSoLuong.Value = Convert.ToInt32(value);
                }
                else
                {
                    numChinhSoLuong.Value = 0; // Hoặc giá trị mặc định bạn muốn
                }
            }
        }

        private void numChinhSoLuong_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnDieuChinhSoLuong_Click(object sender, EventArgs e)
        {
            if (maSanPhamDangChinhSua == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần điều chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int soLuongMoi = (int)numChinhSoLuong.Value;

            foreach (DataGridViewRow row in dgvDanhSachSP.Rows)
            {
                if (Convert.ToInt32(row.Cells["MaSP"].Value) == maSanPhamDangChinhSua)
                {
                    row.Cells["SoLuongThieu"].Value = soLuongMoi;
                    MessageBox.Show($"Đã cập nhật số lượng thiếu của sản phẩm [{row.Cells["TenSP"].Value}] thành: {soLuongMoi}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
        }

        private void btnTaoBangDuTru_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm nào để lập bảng dự trù.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1. Tạo đối tượng BangDuTru mới và lưu vào DB
                BangDuTru bangDuTru = new BangDuTru(DateTime.Now);
                int maBDT = bdtDAO.TaoBangDuTruMoi(bangDuTru); // <-- Truyền đối tượng vào
                bangDuTru.MaBDT = maBDT;

                // 2. Duyệt DataGridView để thêm ChiTietBangDuTru
                foreach (DataGridViewRow row in dgvDanhSachSP.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (row.Cells["MaSP"].Value == null ||
                        row.Cells["SoLuongThieu"].Value == null ||
                        row.Cells["SoLuong"].Value == null)
                        continue;

                    int maSP = Convert.ToInt32(row.Cells["MaSP"].Value);
                    int soLuongNhap = Convert.ToInt32(row.Cells["SoLuongThieu"].Value);
                    int soLuongSP = Convert.ToInt32(row.Cells["SoLuong"].Value);

                    ChiTietBangDuTru chiTiet = new ChiTietBangDuTru(maSP, soLuongNhap, soLuongSP);

                    // Gọi DAO để thêm chi tiết vào DB
                    bdtDAO.ThemChiTietBangDuTru(maBDT, chiTiet);
                }

                MessageBox.Show("Tạo bảng dự trù thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTaoBangDuTru.Enabled = false; // Ngăn tạo lại
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tạo bảng dự trù: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDanhSachSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDanhSachSP_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Rename columns with Vietnamese names
            if (dgvDanhSachSP.Columns.Count > 0)
            {
                // Set Vietnamese headers based on existing column names
                // Adjust these mappings according to your actual column names
                var columnMappings = new Dictionary<string, string>
                {
                    { "MaSP", "Mã SP" },
                    { "TenSP", "Tên SP" },
                    { "SoLuong", "Số Lượng" },
                    { "DonViTinh", "Đơn Vị Tính" },
                    { "NgayNhap", "Ngày Nhập" },
                    { "MaNCC", "Mã NCC" },
                    { "NhaCungCap", "Nhà Cung Cấp" },
                    { "TenDanhMuc", "Tên Danh Mục" }
                    // Add more mappings as needed
                };

                foreach (DataGridViewColumn column in dgvDanhSachSP.Columns)
                {
                    if (columnMappings.ContainsKey(column.Name))
                    {
                        column.HeaderText = columnMappings[column.Name];
                    }

                    // Center the text in all columns
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Center data in certain columns (adjust as needed)
                    if (column.Name == "MaSP" || column.Name == "SoLuong" ||
                        column.Name == "MaNCC" || column.Name == "NgayNhap")
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }

            // Alternate row colors for better readability
            foreach (DataGridViewRow row in dgvDanhSachSP.Rows)
            {
                if (row.Index % 2 == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
                }
            }
        }
    }
}