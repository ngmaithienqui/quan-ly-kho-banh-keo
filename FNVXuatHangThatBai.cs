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
    public partial class FNVXuatHangThatBai : Form
    {
        public FNVXuatHangThatBai()
        {
            InitializeComponent();
        }

        private void FNVXuatHangThatBai_Load(object sender, EventArgs e)
        {
            numThieuHang.Enabled = cbThieuHang.Checked;
            numHangHuHong.Enabled = cbHuHong.Checked;
        }
        public int MaPhieuXuat { get; set; }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // 🔥 Đọc dữ liệu từ form
            int? thieu = cbThieuHang.Checked ? (int?)numThieuHang.Value : null;
            bool huHong = cbHuHong.Checked;

            // 🔥 Lấy ghi chú riêng
            string ghiChuThieu = txtGhiChuThieu.Text.Trim();
            string ghiChuHuHong = txtGhiChuHuHong.Text.Trim();

            try
            {
                ThongBaoDAO tbDAO = new ThongBaoDAO();
                int maThongBao = tbDAO.ThemThongBaoSanPham(DateTime.Now, $"Phiếu xuất #{MaPhieuXuat} thất bại");

                // ➡️ Gửi từng lý do
                if (cbThieuHang.Checked)
                {
                    tbDAO.ThemChiTietThongBaoThatBai(maThongBao, "Thiếu hàng",
                        thieu.HasValue ? thieu.Value : 0,
                        string.IsNullOrEmpty(ghiChuThieu) ? null : ghiChuThieu);
                }

                if (cbHuHong.Checked)
                {
                    tbDAO.ThemChiTietThongBaoThatBai(maThongBao, "Hàng hư hỏng",
                        null,
                        string.IsNullOrEmpty(ghiChuHuHong) ? "Hàng bị hư hỏng" : ghiChuHuHong);
                }

                MessageBox.Show("Đã lưu lý do thất bại và gửi thông báo!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close(); // ✅ Đóng form
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cbThieuHang_CheckedChanged(object sender, EventArgs e)
        {
           
            numThieuHang.Enabled = cbThieuHang.Checked;

        }

        private void cbHuHong_CheckedChanged(object sender, EventArgs e)
        {
            numHangHuHong.Enabled = cbHuHong.Checked;

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void numHangHuHong_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numThieuHang_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
