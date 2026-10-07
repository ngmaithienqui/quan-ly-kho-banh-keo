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
    public partial class FLapKiemKe : Form
    {
        public FLapKiemKe()
        {
            InitializeComponent();
            dgv_LapPhieuKiemKe.CellEndEdit += dataGridView1_CellEndEdit;
            //lBTimKiemSP.Click += lBTimKiemSP_Click; // Sử dụng sự kiện Click
            lBTimKiemSP.DoubleClick += lBTimKiemSP_DoubleClick;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (e.ColumnIndex == dgv_LapPhieuKiemKe.Columns["colMaSP"].Index)
            {
                var cell = dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colMaSP"];
                if (cell.Value != null && int.TryParse(cell.Value.ToString(), out int maSP))
                {
                    SanPhamDTO sp = SanPhamDAO.GetSanPhamLapPhieuKiemKe(maSP);
                    if (sp != null)
                    {
                        dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colTenSP"].Value = sp.TenSP;
                        dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colSoLuongSP"].Value = sp.SoLuong;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colTenSP"].Value = "";
                        dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colSoLuongSP"].Value = "";
                    }
                }
            }
            else if (e.ColumnIndex == dgv_LapPhieuKiemKe.Columns["colSoLuongThucTe"].Index)
            {
                var cellThucTe = dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colSoLuongThucTe"];
                var cellSoLuongSP = dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colSoLuongSP"];

                if (cellThucTe.Value != null && cellSoLuongSP.Value != null &&
                    int.TryParse(cellThucTe.Value.ToString(), out int soLuongThucTe) &&
                    int.TryParse(cellSoLuongSP.Value.ToString(), out int soLuongSP))
                {
                    if (soLuongThucTe != soLuongSP)
                    {
                        MessageBox.Show("Số lượng thực tế không khớp với số lượng sản phẩm!",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        dgv_LapPhieuKiemKe.CurrentCell = dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colLyDo"];
                    }
                }
            }
        }

        private void dgv_LapPhieuKiemKe_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                var cellThucTe = dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colSoLuongThucTe"];
                var cellSoLuongSP = dgv_LapPhieuKiemKe.Rows[rowIndex].Cells["colSoLuongSP"];

                if (cellThucTe.Value != null && cellSoLuongSP.Value != null &&
                    int.TryParse(cellThucTe.Value.ToString(), out int soLuongThucTe) &&
                    int.TryParse(cellSoLuongSP.Value.ToString(), out int soLuongSP))
                {
                    if (soLuongThucTe != soLuongSP)
                    {
                        e.CellStyle.BackColor = Color.LightPink;
                    }
                }
            }
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNguoiLapPhieu.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập tên người lập phiếu!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNguoiLapPhieu.Focus();
                    return;
                }

                PhieuKiemKeDTO phieu = new PhieuKiemKeDTO
                {
                    NguoiLapPhieu = txtNguoiLapPhieu.Text.Trim(),
                    NgayLap = DateTime.Now,
                    TrangThai = "Chờ duyệt"
                };

                int maPhieuMoi = PhieuKiemKeDAO.ThemPhieuKiemKe(phieu);
                if (maPhieuMoi <= 0)
                {
                    MessageBox.Show("Không thể lưu phiếu kiểm kê!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (DataGridViewRow row in dgv_LapPhieuKiemKe.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (int.TryParse(row.Cells["colMaSP"].Value?.ToString(), out int maSP) &&
                        int.TryParse(row.Cells["colSoLuongThucTe"].Value?.ToString(), out int soLuongThucTe))
                    {
                        string lyDo = row.Cells["colLyDo"].Value?.ToString() ?? "";

                        ChiTietPhieuKiemKeDTO chiTiet = new ChiTietPhieuKiemKeDTO
                        {
                            MaPhieu = maPhieuMoi,
                            MaSP = maSP,
                            SoLuongSPThuc = soLuongThucTe,
                            LyDo = lyDo
                        };

                        ChiTietPhieuKiemKeDAO.ThemChiTiet(chiTiet.MaPhieu, chiTiet.MaSP, chiTiet.SoLuongSPThuc, chiTiet.LyDo);
                    }
                }

                MessageBox.Show("Lưu phiếu kiểm kê thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FLapKiemKe_Load(object sender, EventArgs e)
        {
            // Cấu hình cột cho DataGridView nếu chưa có
            dgv_LapPhieuKiemKe.Columns.Clear();
            dgv_LapPhieuKiemKe.Columns.Add("colMaSP", "Mã SP");
            dgv_LapPhieuKiemKe.Columns.Add("colTenSP", "Tên SP");
            dgv_LapPhieuKiemKe.Columns.Add("colSoLuongSP", "Số Lượng SP");
            dgv_LapPhieuKiemKe.Columns.Add("colSoLuongThucTe", "Số Lượng Thực Tế");
            dgv_LapPhieuKiemKe.Columns.Add("colLyDo", "Lý Do");

            lBTimKiemSP.Visible = false;
            dgv_LapPhieuKiemKe.Rows.Clear();
        }


        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                List<SanPhamDTO> results = SanPhamDAO.SearchSanPhamByPartialName(keyword);
                lBTimKiemSP.DataSource = results;
                lBTimKiemSP.DisplayMember = "TenSP";
                lBTimKiemSP.ValueMember = "MaSP";
                lBTimKiemSP.Visible = true;
            }
            else
            {
                lBTimKiemSP.Visible = false;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        //private void lBTimKiemSP_Click(object sender, EventArgs e)
        //{
        //    if (lBTimKiemSP.SelectedItem != null)
        //    {
        //        SanPhamDTO selectedProduct = (SanPhamDTO)lBTimKiemSP.SelectedItem;

        //        if (selectedProduct != null)
        //        {
        //            int maSP = selectedProduct.MaSP;
        //            string tenSP = selectedProduct.TenSP;

        //            bool isProductExist = false;
        //            foreach (DataGridViewRow row in dgv_LapPhieuKiemKe.Rows)
        //            {
        //                if (row.IsNewRow) continue;
        //                if (row.Cells["colMaSP"].Value != null && Convert.ToInt32(row.Cells["colMaSP"].Value) == maSP)
        //                {
        //                    isProductExist = true;
        //                    break;
        //                }
        //            }

        //            if (!isProductExist)
        //            {
        //                int index = dgv_LapPhieuKiemKe.Rows.Add();
        //                dgv_LapPhieuKiemKe.Rows[index].Cells["colMaSP"].Value = maSP;
        //                dgv_LapPhieuKiemKe.Rows[index].Cells["colTenSP"].Value = tenSP;
        //                dgv_LapPhieuKiemKe.Rows[index].Cells["colSoLuongSP"].Value = selectedProduct.SoLuong;
        //            }
        //            else
        //            {
        //                MessageBox.Show("Sản phẩm này đã có trong bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }

        //            // Sau khi chọn xong, focus lại vào TextBox tìm kiếm
        //            txtTimKiem.Focus();
        //            txtTimKiem.SelectAll();
        //        }
        //    }
        //}
        private void lBTimKiemSP_DoubleClick(object sender, EventArgs e)
        {
            if (lBTimKiemSP.SelectedItem is SanPhamDTO selectedProduct)
            {
                int maSP = selectedProduct.MaSP;

                // Kiểm tra đã tồn tại chưa
                foreach (DataGridViewRow row in dgv_LapPhieuKiemKe.Rows)
                {
                    if (!row.IsNewRow && Convert.ToInt32(row.Cells["colMaSP"].Value) == maSP)
                    {
                        MessageBox.Show("Sản phẩm đã tồn tại trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Thêm mới
                int newRow = dgv_LapPhieuKiemKe.Rows.Add();
                dgv_LapPhieuKiemKe.Rows[newRow].Cells["colMaSP"].Value = selectedProduct.MaSP;
                dgv_LapPhieuKiemKe.Rows[newRow].Cells["colTenSP"].Value = selectedProduct.TenSP;
                dgv_LapPhieuKiemKe.Rows[newRow].Cells["colSoLuongSP"].Value = selectedProduct.SoLuong;

                // Reset lại tìm kiếm
                txtTimKiem.Clear();
                lBTimKiemSP.Visible = false;
                txtTimKiem.Focus();
            }
        }

        private void lBTimKiemSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }
    }
}
