using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FXemPhieuNhapHang : Form
    {
        PhieuNhapHangDAO pnhDAO = new PhieuNhapHangDAO();
        ThongBaoDAO tbDAO = new ThongBaoDAO();
        public FXemPhieuNhapHang()
        {
            InitializeComponent();
        }

        private void FXemPhieuNhapHang_Load(object sender, EventArgs e)
        {
            dgvPhieuNhap.DataSource = pnhDAO.LayDanhSachPhieuNhap();

            /*cbTrangThai.Items.Clear();
            cbTrangThai.Items.AddRange(new string[] { "Chưa xử lý", "Đã xử lý", "Đã hủy" });*/
        }

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maPN = Convert.ToInt32(dgvPhieuNhap.Rows[e.RowIndex].Cells["MaPhieu"].Value);
                HienThiChiTietPhieuNhap(maPN);

                // Gán trạng thái vào ComboBox
                string trangThai = dgvPhieuNhap.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString();
                cbTrangThai.SelectedItem = trangThai;
                string trangThaiThanhToan = dgvPhieuNhap.Rows[e.RowIndex].Cells["TrangThaiThanhToan"].Value?.ToString();

                cbTrangThaiThanhToan.SelectedItem = trangThaiThanhToan;
                gbChiTiet.Text = $"Chi tiết phiếu nhập - Mã: {maPN}";
            }
        }

        private void HienThiChiTietPhieuNhap(int maPN)
        {
            DataTable dtChiTiet = pnhDAO.LayChiTietPhieuNhap(maPN);
            dgvChiTietPN.DataSource = dtChiTiet;
        }

        private void btnCapNhatTrangThai_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.CurrentRow != null)
            {
                int maPN = Convert.ToInt32(dgvPhieuNhap.CurrentRow.Cells["MaPhieu"].Value);
                string trangThaiMoi = cbTrangThai.SelectedItem?.ToString();
                string trangThaiThanhToanMoi = cbTrangThaiThanhToan.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(trangThaiMoi) && !string.IsNullOrEmpty(trangThaiThanhToanMoi))
                {
                    string trangThaiHienTai = dgvPhieuNhap.CurrentRow.Cells["TrangThai"].Value?.ToString();

                    if (trangThaiHienTai == "Đã nhận hàng" && trangThaiMoi != trangThaiHienTai)
                    {
                        MessageBox.Show("Phiếu nhập đã nhận hàng, không thể thay đổi trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (trangThaiHienTai != "Đã nhận hàng" && trangThaiMoi == "Đã nhận hàng")
                    {
                        PhieuNhapHangDAO.CapNhatSoLuongSanPhamTheoPhieuNhap(maPN);
                    }

                    if (trangThaiMoi == "Chờ xử lý")
                    {
                        // ➡️ MỞ form nhập lý do thất bại
                        FNVNhapHangThatBai formThatBai = new FNVNhapHangThatBai();
                        formThatBai.MaPhieuNhap = maPN;
                        DialogResult result = formThatBai.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            // ➡️ Nếu nhập lý do thành công => Cập nhật trạng thái Chờ xử lý
                            pnhDAO.CapNhatTrangThai(maPN, "Chờ xử lý", trangThaiThanhToanMoi);
                            MessageBox.Show("Đã lưu lý do và cập nhật trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Chưa nhập lý do thất bại. Không cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        // Nếu là trạng thái khác
                        pnhDAO.CapNhatTrangThai(maPN, trangThaiMoi, trangThaiThanhToanMoi);
                        MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    dgvPhieuNhap.DataSource = pnhDAO.LayDanhSachPhieuNhap();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ trạng thái và thanh toán!");
                }
            }

        }

        private void cbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
