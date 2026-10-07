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
    public partial class FCongNo : Form
    {
        private static DBConnection db = new DBConnection();
        private int maKH;
        private string tenKH;
        private float congNoHienTai;

        public FCongNo() { InitializeComponent(); }

        public FCongNo(int maKH, string tenKH, float congNo)
        {
            InitializeComponent();
            this.maKH = maKH;
            this.tenKH = tenKH;
            this.congNoHienTai = congNo;
            HienThiCongNoHienTai();
        }

        private void FCongNo_Load(object sender, EventArgs e)
        {
            lblTenKH.Text = tenKH;
            lblCongNoHienTai.Text = string.Format("{0:#,##0}", congNoHienTai) + " VNĐ";
            dtpNgayThanhToan.Value = DateTime.Now;
            txtSoTienThanhToan.Select();
            HienThiCongNoHienTai();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(txtSoTienThanhToan.Text.Trim().Replace(",", ""), out float soTienThanhToan) || soTienThanhToan <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền thanh toán hợp lệ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienThanhToan.Focus();
                return;
            }
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    float congNoMoi = congNoHienTai - soTienThanhToan;

                    // 1. Cập nhật công nợ trong bảng KhachHang
                    using (SqlCommand cmd = new SqlCommand("UPDATE KhachHang SET CongNo = @CongNo WHERE MaKH = @MaKH", conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@CongNo", congNoMoi);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Ghi lịch sử vào bảng GiaoDichKH
                    var giaoDich = new GiaoDichKHDTO
                    {
                        MaKH = maKH,
                        MaPhieu = null, // Nếu không liên kết phiếu nào thì để 0
                        LoaiGiaoDich = congNoMoi == 0 ? "Đã thanh toán" : "Thanh toán một phần",
                        NgayGiaoDich = dtpNgayThanhToan.Value,
                        SoTien = soTienThanhToan,
                        GhiChu = txtGhiChu.Text
                    };

                    if (GiaoDichKHDAO.ThemGiaoDich(giaoDich))
                    {
                        if (congNoMoi <= 0)
                        {
                            PhieuXuatHangDAO.CapNhatTrangThaiThanhToan(maKH);
                        }
                        MessageBox.Show($"Đã thanh toán {soTienThanhToan:#,##0} VNĐ thành công.\nCông nợ còn lại: {congNoMoi:#,##0} VNĐ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không thể ghi nhận giao dịch!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán công nợ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtSoTienThanhToan_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và dấu chấm
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Không cho phép nhập 2 dấu chấm
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtSoTienThanhToan_TextChanged(object sender, EventArgs e)
        {
            // Khi người dùng nhập số tiền, tính toán số tiền còn lại
            float soTienTraFloat = 0;
            if (float.TryParse(txtSoTienThanhToan.Text.Replace(",", ""), out soTienTraFloat))
            {
                float conLai = congNoHienTai - soTienTraFloat;
                if (conLai < 0)
                {
                    MessageBox.Show("Số tiền thanh toán không thể lớn hơn công nợ!", 
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTienThanhToan.Text = congNoHienTai.ToString();
                    soTienTraFloat = congNoHienTai;
                    conLai = 0;
                }
                lblConLai.Text = string.Format("{0:#,##0}", conLai) + " VNĐ";
            }
            else
            {
                lblConLai.Text = string.Format("{0:#,##0}", congNoHienTai) + " VNĐ";
            }
        }

        private void txtSoTienThanhToan_Leave(object sender, EventArgs e)
        {
            // Format số tiền
            float soTien;
            if (float.TryParse(txtSoTienThanhToan.Text.Replace(",", ""), out soTien))
            {
                txtSoTienThanhToan.Text = string.Format("{0:#,##0}", soTien);
            }
        }

        private void txtSoTienThanhToan_Enter(object sender, EventArgs e)
        {
            // Remove formatting when entering the textbox
            float soTien;
            if (float.TryParse(txtSoTienThanhToan.Text.Replace(",", ""), out soTien))
            {
                txtSoTienThanhToan.Text = soTien.ToString();
            }
        }

        private void lblCongNoHienTai_Click(object sender, EventArgs e)
        {

        }
        private void HienThiCongNoHienTai()
        {
            float? congNo = KhachHangDAO.LayCongNoHienTai(maKH);
            if (congNo.HasValue)
            {
                congNoHienTai = congNo.Value;
                lblCongNoHienTai.Text = string.Format("{0:#,##0}", congNo.Value) + " VNĐ";
            }
            else
            {
                MessageBox.Show("Không thể lấy dữ liệu công nợ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
