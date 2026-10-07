// FTrangChuNhanVien.cs
using System;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FTrangChuNhanVien : Form
    {
        private Button currentButton;
        private string _hoTen;
        private string _vaiTro;
        private Timer timerDateTime;
        public FTrangChuNhanVien()
        {
            InitializeComponent();
            _hoTen = "Nhân viên kho";
            _vaiTro = "Nhân viên";
            SetupTimer();
        }
        public FTrangChuNhanVien(string hoTen, string vaiTro)
        {
            InitializeComponent();
            _hoTen = hoTen;
            _vaiTro = vaiTro;
            SetupTimer();
        }
        private void ActivateButton(object sender)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.FromArgb(0, 150, 136); // Màu khi được chọn
            }
        }

        private void btnXemPhieuNhap_Click(object sender, EventArgs e)
        {
            var frmNVNhapHang = new FNVXemPhieuNhapHang();
            frmNVNhapHang.ShowDialog();
        }

        private void btnXemPhieuXuat_Click(object sender, EventArgs e)
        {
            FNVXemPhieuXuat frmNVXuatHang = new FNVXemPhieuXuat();
            frmNVXuatHang.ShowDialog();
        }

        private void btnXemDanhMuc_Click(object sender, EventArgs e)
        {
            FDanhMucQLK fQuanLyDanhMuc = new FDanhMucQLK();
            fQuanLyDanhMuc.ShowDialog();
        }

        private void btnXemSanPham_Click(object sender, EventArgs e)
        {
            var frmSanPhamQLK = new FSanPhamQLK();
            frmSanPhamQLK.ShowDialog();
        }

        private void btnCapNhatTrangThai_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            MessageBox.Show("Cập nhật trạng thái đơn hàng được chọn.", "Thông báo");
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
           

            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();  // Ẩn form hiện tại
                FDangNhap frmLogin = new FDangNhap();
                frmLogin.ShowDialog();  // Show form đăng nhập dạng modal
                this.Close();  // Đóng form chính sau khi đăng nhập lại hoặc thoát
            }
        }

        private void FTrangChuNhanVien_Load(object sender, EventArgs e)
        {
            lblChaoMung.Text = $"Xin chào: {_hoTen} - {_vaiTro}";
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        private void SetupTimer()
        {
            timerDateTime = new Timer();
            timerDateTime.Interval = 1000;
            timerDateTime.Tick += timerDateTime_Tick;
            timerDateTime.Start();
        }
        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
