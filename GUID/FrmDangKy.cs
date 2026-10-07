using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace QLCuaHangBanhKeo
{
    public partial class FrmDangKy : Form
    {
        public FrmDangKy()
        {
            InitializeComponent();
            
            // Set the form icon and title
            this.Text = "Đăng Ký Tài Khoản Mới";
            
            // Setup initial UI elements
            SetupFormUI();
        }

        private void SetupFormUI()
        {
            // Set a default icon for pictureBox1
            try
            {
                // Try to load an image from a resource or path
                // Option 1: From embedded resource
                // pictureBox1.Image = Properties.Resources.ResourceName;
                
                // Option 2: From file system (if available)
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "store.png");
                if (File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Option 3: Use a colored background and text as fallback
                    pictureBox1.BackColor = Color.FromArgb(25, 118, 210);
                    
                    // Add a label on the pictureBox to display text
                    Label storeLabel = new Label();
                    storeLabel.Text = "STORE";
                    storeLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                    storeLabel.ForeColor = Color.White;
                    storeLabel.BackColor = Color.Transparent;
                    storeLabel.AutoSize = false;
                    storeLabel.TextAlign = ContentAlignment.MiddleCenter;
                    storeLabel.Dock = DockStyle.Fill;
                    
                    pictureBox1.Controls.Add(storeLabel);
                }
            }
            catch (Exception ex)
            {
                // Just log the error or ignore, don't crash the form
                Console.WriteLine("Error loading image: " + ex.Message);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            if (!Regex.IsMatch(txtSDT.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            if (txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }

            if (txtMatKhau.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }

            if (!chkDieuKhoan.Checked)
            {
                MessageBox.Show("Vui lòng đồng ý với điều khoản sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo tên đăng nhập từ email (lấy phần trước @)
            string tenDangNhap = txtEmail.Text.Split('@')[0];

            // Đăng ký người dùng mới
            RegisterUser(tenDangNhap, txtMatKhau.Text, txtHoTen.Text, txtEmail.Text, txtSDT.Text);
        }

        private void RegisterUser(string tenDangNhap, string matKhau, string hoTen, string email, string sdt)
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Kiểm tra xem tài khoản đã tồn tại chưa
                    if (IsUserExist(conn, tenDangNhap, email))
                    {
                        MessageBox.Show("Email hoặc tên đăng nhập đã được sử dụng!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Mã hóa mật khẩu
                    string hashedPassword = HashPassword(matKhau);

                    // Thêm người dùng mới vào database
                    string query = @"INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, Sdt, VaiTro) 
                                     VALUES (@TenDangNhap, @MatKhau, @HoTen, @Email, @Sdt, @VaiTro)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Sdt", sdt);
                        cmd.Parameters.AddWithValue("@VaiTro", "KhachHang"); // Vai trò mặc định
                        
                        int result = cmd.ExecuteNonQuery();
                        
                        if (result > 0)
                        {
                            MessageBox.Show("Đăng ký thành công!\n\nChào mừng " + hoTen + " đến với cửa hàng bánh kẹo!", 
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reset form
                            ResetForm();
                        }
                        else
                        {
                            MessageBox.Show("Đăng ký không thành công!", "Lỗi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsUserExist(SqlConnection conn, string tenDangNhap, string email)
        {
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap OR Email = @Email";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@Email", email);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private string HashPassword(string password)
        {
            // Sử dụng SHA256 để mã hóa mật khẩu
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển đổi chuỗi thành mảng byte và tính toán hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển đổi mảng byte thành chuỗi
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void ResetForm()
        {
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtMatKhau.Text = "";
            txtXacNhanMatKhau.Text = "";
            chkDieuKhoan.Checked = false;
            txtHoTen.Focus();
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open login form
            MessageBox.Show("Chuyển đến trang đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // In a real project, you would open the login form here
            // For example: var frmDangNhap = new FrmDangNhap(); frmDangNhap.Show(); this.Close();
        }
    }
}
