using QLCuaHangBanhKeo.DAO;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FNVNhapHangThatBai : Form
    {
        public int MaPhieuNhap { get; set; }

        public FNVNhapHangThatBai()
        {
            InitializeComponent();
        }




        private void FNVNhapHangThatBai_Load(object sender, EventArgs e)
        {
            // Ban đầu disable NumericUpDown
            numThieuHang.Enabled = false;
            numDuHang.Enabled = false;
        }



        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                ThongBaoDAO tbDAO = new ThongBaoDAO();
                int maThongBao = tbDAO.ThemThongBaoSanPham(DateTime.Now, $"Phiếu nhập #{MaPhieuNhap} thất bại");

                if (cbThieuHang.Checked)
                {
                    int soLuongThieu = (int)numThieuHang.Value;
                    string ghiChuThieu = txtGhiChuThieu.Text.Trim();
                    tbDAO.ThemChiTietThongBaoThatBai(maThongBao, "Thiếu hàng", soLuongThieu, string.IsNullOrEmpty(ghiChuThieu) ? null : ghiChuThieu);
                }

                if (cbDuHang.Checked)
                {
                    int soLuongDu = (int)numDuHang.Value;
                    string ghiChuDu = txtGhiChuDu.Text.Trim();
                    tbDAO.ThemChiTietThongBaoThatBai(maThongBao, "Dư hàng", soLuongDu, string.IsNullOrEmpty(ghiChuDu) ? null : ghiChuDu);
                }

                if (cbHuHong.Checked)
                {
                    string ghiChuHuHong = txtGhiChuHuHong.Text.Trim();
                    tbDAO.ThemChiTietThongBaoThatBai(maThongBao, "Hàng hư hỏng", null, string.IsNullOrEmpty(ghiChuHuHong) ? "Hàng bị hư hỏng" : ghiChuHuHong);
                }

                if (cbNhapSai.Checked)
                {
                    string ghiChuNhapSai = txtGhiChuHuHong.Text.Trim();
                    tbDAO.ThemChiTietThongBaoThatBai(maThongBao, "Nhập sai hàng", null, string.IsNullOrEmpty(ghiChuNhapSai) ? "Đã nhập sai sản phẩm" : ghiChuNhapSai);
                }

                MessageBox.Show("Đã lưu lý do thất bại và gửi thông báo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông báo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cbNhapSai_CheckedChanged_1(object sender, EventArgs e)
        {
            txtGhiChuHuHong.Enabled = cbNhapSai.Checked;

        }

        private void cbHuHong_CheckedChanged_1(object sender, EventArgs e)
        {
            txtGhiChuHuHong.Enabled = cbHuHong.Checked;

        }

        private void cbDuHang_CheckedChanged_1(object sender, EventArgs e)
        {
            numDuHang.Enabled = cbDuHang.Checked;
            txtGhiChuDu.Enabled = cbDuHang.Checked;
        }

        private void cbThieuHang_CheckedChanged_1(object sender, EventArgs e)
        {
            numThieuHang.Enabled = cbThieuHang.Checked;
            txtGhiChuThieu.Enabled = cbThieuHang.Checked;
        }
    }
}