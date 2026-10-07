using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FDangNhap : Form
    {
        public FDangNhap()
        {
            InitializeComponent();
            this.Text = "Đăng Nhập - Cửa Hàng Bánh Kẹo";
            SetupFormUI();
        }

        private void FDangNhap_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            LoadSavedCredentials();
        }

        private void SetupFormUI()
        {
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "store.png");
                if (File.Exists(imagePath))
                {
                    pictureBoxLogo.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureBoxLogo.BackColor = Color.FromArgb(25, 118, 210);
                    Label storeLabel = new Label();
                    storeLabel.Text = "STORE";
                    storeLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                    storeLabel.ForeColor = Color.White;
                    storeLabel.BackColor = Color.Transparent;
                    storeLabel.Dock = DockStyle.Fill;
                    pictureBoxLogo.Controls.Add(storeLabel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading logo image: " + ex.Message);
            }
        }

        private void LoadSavedCredentials()
        {
            if (Properties.Settings.Default.RememberMe)
            {
                txtUsername.Text = Properties.Settings.Default.SavedUsername;
                chkRemember.Checked = true;
            }
        }

        private void SaveCredentials()
        {
            Properties.Settings.Default.RememberMe = chkRemember.Checked;
            Properties.Settings.Default.SavedUsername = chkRemember.Checked ? txtUsername.Text : string.Empty;
            Properties.Settings.Default.Save();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            ProcessLogin();
        }

        private void ProcessLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NguoiDungDAO dao = new NguoiDungDAO();
            NguoiDungDTO user = dao.DangNhap(username, password);

            if (user != null)
            {
                SaveCredentials();
                MessageBox.Show($"Xin chào, {user.HoTen}! Đăng nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenFormByRole(user.VaiTro);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenFormByRole(string vaiTro)
        {
            Form nextForm = null;

            switch (vaiTro)
            {
                case "ChuDaiLy":
                    nextForm = new FTrangChuChuDaiLy();
                    break;

                case "QuanLyKho":
                    nextForm = new FTrangchuQuanlykho();
                    break;

                case "NhanVien":
                    nextForm = new FTrangchuQuanlykho(); // Bạn có thể thay bằng FNhanVien nếu có form riêng
                    break;

                default:
                    nextForm = null;
                    break;
            }

            if (nextForm != null)
            {
                nextForm.Show();
            }
            else
            {
                MessageBox.Show("Vai trò không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ProcessLogin();
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new FrmDangKy().ShowDialog();
        }

        

        private void panelRight_Paint(object sender, PaintEventArgs e) { }
    }
}
