
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
    public partial class FCongNoNCC : Form
    {
        private static DBConnection db = new DBConnection();
        private int maNCC;
        private string tenNCC;
        private float congNoHienTai;

        public FCongNoNCC() { InitializeComponent(); }

        

        public FCongNoNCC(int maNCC, string tenNCC, float congNo)
        {
            InitializeComponent();
            this.maNCC = maNCC;
            this.tenNCC = tenNCC;
            this.congNoHienTai = congNo;
         
        }

        private void FCongNoNCC_Load(object sender, EventArgs e)
        {
            lblConLaiNCC.Hide();
            lblTenNCC.Text = tenNCC;
            lblCongNoHienTaiNCC.Text = string.Format("{0:#,##0}", congNoHienTai) + " VNĐ";
            dtpNgayThanhToan.Value = DateTime.Now;
            txtSoTienThanhToanNCC.Select();
            HienThiCongNoHienTai();
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

        

        private void txtSoTienThanhToan_Leave(object sender, EventArgs e)
        {
            // Format số tiền
            float soTien;
            if (float.TryParse(txtSoTienThanhToanNCC.Text.Replace(",", ""), out soTien))
            {
                txtSoTienThanhToanNCC.Text = string.Format("{0:#,##0}", soTien);
            }
        }

        private void txtSoTienThanhToan_Enter(object sender, EventArgs e)
        {
            // Remove formatting when entering the textbox
            float soTien;
            if (float.TryParse(txtSoTienThanhToanNCC.Text.Replace(",", ""), out soTien))
            {
                txtSoTienThanhToanNCC.Text = soTien.ToString();
            }
        }

       

        private void HienThiCongNoHienTai()
        {
            float congNo = NCCDAO.LayCongNoHienTai(maNCC);
            congNoHienTai = congNo;
            lblCongNoHienTaiNCC.Text = string.Format("{0:#,##0}", congNo) + " VNĐ";
        }
        

        private void lblCongNoHienTaiNCC_Click(object sender, EventArgs e)
        {

        }

        private void txtSoTienThanhToanNCC_TextChanged(object sender, EventArgs e)
        {
            lblConLaiNCC.Show();
            // Khi người dùng nhập số tiền, tính toán số tiền còn lại
            float soTienTraFloat = 0;
            if (float.TryParse(txtSoTienThanhToanNCC.Text.Replace(",", ""), out soTienTraFloat))
            {
                float conLai = congNoHienTai - soTienTraFloat;
                if (conLai < 0)
                {
                    MessageBox.Show("Số tiền thanh toán không thể lớn hơn công nợ!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTienThanhToanNCC.Text = congNoHienTai.ToString();
                    soTienTraFloat = congNoHienTai;
                    conLai = 0;
                }
                lblConLaiNCC.Text = string.Format("{0:#,##0}", conLai) + " VNĐ";
            }
            else
            {
                lblConLaiNCC.Text = string.Format("{0:#,##0}", congNoHienTai) + " VNĐ";
            }
        }

        private void btnThanhToanNCC_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(txtSoTienThanhToanNCC.Text.Trim().Replace(",", ""), out float soTienThanhToan) || soTienThanhToan <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền thanh toán hợp lệ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienThanhToanNCC.Focus();
                return;
            }
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    float congNoMoi = congNoHienTai - soTienThanhToan;

                    // 1. Cập nhật công nợ trong bảng NhaCungCap
                    using (SqlCommand cmd = new SqlCommand("UPDATE NhaCungCap SET CongNo = @CongNo WHERE MaNCC = @MaNCC", conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmd.Parameters.AddWithValue("@CongNo", congNoMoi);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Ghi lịch sử vào bảng GiaoDichNCC (giả sử đã có bảng này)
                    var giaoDich = new GiaoDichNCCDTO
                    {
                        MaNCC = maNCC,
                        MaPhieu = null, // Nếu không liên kết phiếu nào thì để null
                        LoaiGiaoDich = congNoMoi == 0 ? "Đã thanh toán" : "Thanh toán một phần",
                        NgayGiaoDich = dtpNgayThanhToan.Value,
                        SoTien = soTienThanhToan,
                        GhiChu = txtGhiChu.Text
                    };

                    if (GiaoDichNCCDAO.ThemGiaoDich(giaoDich))
                    {
                        if (congNoMoi <= 0)
                        {
                            PhieuNhapHangDAO.CapNhatTrangThaiThanhToan(maNCC);
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

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}