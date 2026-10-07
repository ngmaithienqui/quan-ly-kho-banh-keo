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
using Font = System.Drawing.Font;
using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;

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

                cboVaiTro.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                // Just log the error or ignore, don't crash the form
                Console.WriteLine("Error loading image: " + ex.Message);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string vaiTro;
            switch (cboVaiTro.SelectedIndex)
            {
                case 0: vaiTro = "ChuDaiLy"; break;
                case 1: vaiTro = "QuanLyKho"; break;
                case 2: vaiTro = "NhanVien"; break;
                default: vaiTro = "KhachHang"; break;
            }


            NguoiDungDTO nguoiDung = new NguoiDungDTO
            {
                TenDangNhap = txtTenDangNhap.Text,
                MatKhau = txtMatKhau.Text,
                HoTen = txtHoTen.Text,
                Email = txtEmail.Text,
                Sdt = txtSDT.Text,
                VaiTro = vaiTro
            };

            NguoiDungDAO dao = new NguoiDungDAO();
            if (dao.KiemTraTonTai(nguoiDung.TenDangNhap, nguoiDung.Email))
            {
                MessageBox.Show("Tên đăng nhập hoặc email đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dao.DangKyNguoiDung(nguoiDung))
            {
                MessageBox.Show("Đăng ký thành công!\n\nChào mừng " + nguoiDung.HoTen + " đến với cửa hàng bánh kẹo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            else
            {
                MessageBox.Show("Đăng ký không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtTenDangNhap.Text, @"^[a-zA-Z0-9_]+$"))
            {
                MessageBox.Show("Tên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSDT.Text) || !Regex.IsMatch(txtSDT.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            NguoiDungDAO dao = new NguoiDungDAO();
            if (dao.KiemTraTrungSoDienThoai(txtSDT.Text))
            {
                MessageBox.Show("Số điện thoại đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text) || txtMatKhau.Text.Length < 8)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text) || txtMatKhau.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return false;
            }

            if (cboVaiTro.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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
            txtTenDangNhap.Text = "";
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtMatKhau.Text = "";
            txtXacNhanMatKhau.Text = "";
            cboVaiTro.SelectedIndex = 0;
           
            txtTenDangNhap.Focus();
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form fdangnhap = new FDangNhap();
            fdangnhap.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void txtMatKhau_TextChanged(object sender, EventArgs e) { }

        private void FrmDangKy_Load(object sender, EventArgs e)
        {

        }

        private void txtXacNhanMatKhau_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
