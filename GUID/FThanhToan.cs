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
    public partial class FThanhToan : Form
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CuaHangBanhKeo;Integrated Security=True";
        private int maNCC;
        private string tenNCC;
        private float congNoHienTai;

        public FThanhToan()
        {
            InitializeComponent();
        }

        public FThanhToan(int maNCC, string tenNCC, float congNo)
        {
            InitializeComponent();
            this.maNCC = maNCC;
            this.tenNCC = tenNCC;
            this.congNoHienTai = congNo;
        }

        private void FThanhToan_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin nhà cung cấp và công nợ hiện tại
            lblTenNCC.Text = tenNCC;
            lblCongNoHienTai.Text = string.Format("{0:#,##0}", congNoHienTai) + " VNĐ";
            
            // Mặc định ngày thanh toán là hôm nay
            dtpNgayThanhToan.Value = DateTime.Now;
            
            // Focus vào số tiền thanh toán
            txtSoTienThanhToan.Select();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Kiểm tra số tiền thanh toán
            float soTienThanhToan;
            if (!float.TryParse(txtSoTienThanhToan.Text.Trim(), out soTienThanhToan) || soTienThanhToan <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền thanh toán hợp lệ", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienThanhToan.Focus();
                return;
            }

            // Cập nhật công nợ nhà cung cấp
            float congNoMoi = congNoHienTai - soTienThanhToan;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Get current transaction history
                    string getLichSuSql = "SELECT LichSuGiaoDich FROM NhaCungCap WHERE MaNCC=@MaNCC";
                    SqlCommand getLichSuCmd = new SqlCommand(getLichSuSql, conn);
                    getLichSuCmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    
                    string lichSuGD = getLichSuCmd.ExecuteScalar()?.ToString() ?? "";
                    
                    // Add new entry to transaction history
                    string newEntry = $"[{DateTime.Now:dd/MM/yyyy HH:mm}] Thanh toán công nợ: {string.Format("{0:#,##0}", soTienThanhToan)} VNĐ. Ghi chú: {txtGhiChu.Text}";
                    string updatedHistory = string.IsNullOrEmpty(lichSuGD) 
                        ? newEntry 
                        : lichSuGD + Environment.NewLine + newEntry;
                    
                    // Update công nợ và lịch sử giao dịch
                    string updateSql = "UPDATE NhaCungCap SET CongNo=@CongNo, LichSuGiaoDich=@LichSu WHERE MaNCC=@MaNCC";
                    SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                    updateCmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    updateCmd.Parameters.AddWithValue("@CongNo", congNoMoi);
                    updateCmd.Parameters.AddWithValue("@LichSu", updatedHistory);
                    
                    int result = updateCmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show($"Đã thanh toán {string.Format("{0:#,##0}", soTienThanhToan)} VNĐ thành công.\nCông nợ còn lại: {string.Format("{0:#,##0}", congNoMoi)} VNĐ", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán công nợ: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtSoTienThanhToan_Leave(object sender, EventArgs e)
        {
            // Format số tiền
            float soTien;
            if (float.TryParse(txtSoTienThanhToan.Text.Trim(), out soTien))
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
    }
}
