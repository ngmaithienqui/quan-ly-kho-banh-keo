using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FTrangChuChuDaiLy : Form
    {
        // Khai báo biến thành viên để lưu thông tin người dùng đăng nhập
        private string _hoTen;
        private string _vaiTro;
        private Button currentButton;

        public FTrangChuChuDaiLy()
        {
            InitializeComponent();
            // Thiết lập giá trị mặc định
            _hoTen = "Quản lý";
            _vaiTro = "Chủ đại lý";
            
            // Ẩn tất cả các submenu khi khởi động
            HideAllSubmenus();
            
            // Thiết lập giao diện nút
            SetupButtonUI();
        }

        // Constructor với thông tin người dùng đăng nhập
        public FTrangChuChuDaiLy(string hoTen, string vaiTro)
        {
            InitializeComponent();
            _hoTen = hoTen;
            _vaiTro = vaiTro;
            
            // Ẩn tất cả các submenu khi khởi động
            HideAllSubmenus();
            
            // Thiết lập giao diện nút
            SetupButtonUI();
        }
        
        private void SetupButtonUI()
        {
            // Thiết lập màu nền và hiệu ứng cho các nút
            foreach (Control ctrl in panelSideMenu.Controls)
            {
                if (ctrl is Button)
                {
                    Button btn = (Button)ctrl;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 118, 210);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(13, 71, 161);
                    btn.BackColor = Color.FromArgb(33, 150, 243);
                    
                    // Thêm hiệu ứng cho nút (drop shadow)
                    btn.Paint += (sender, e) => 
                    {
                        Button button = (Button)sender;
                        if (button == currentButton)
                        {
                            // Vẽ đường viền bên trái cho nút được chọn
                            using (SolidBrush brush = new SolidBrush(Color.White))
                            {
                                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, 5, button.Height));
                            }
                        }
                    };
                }
            }

            // Thiết lập màu nền và hiệu ứng cho các nút submenu
            SetupSubmenuButtonsUI(panelDoiTacSubmenu);
            SetupSubmenuButtonsUI(panelGiaoDichSubmenu);
            SetupSubmenuButtonsUI(panelHangHoaSubmenu);
            
            // Set text padding for main buttons
            SetMainButtonsPadding();
            
            // Set text padding for submenu buttons
            SetSubmenuButtonsPadding();
        }
        
        private void SetMainButtonsPadding()
        {
            // Thiết lập padding cho các nút chính
            btnQuanLyDoiTac.Padding = new Padding(15, 0, 0, 0);
            btnQuanLyGiaoDich.Padding = new Padding(15, 0, 0, 0);
            btnThongKe.Padding = new Padding(15, 0, 0, 0);
            btnQuanLyHangHoa.Padding = new Padding(15, 0, 0, 0);
            btnKiemKeKho.Padding = new Padding(15, 0, 0, 0);
            btnTaoTaiKhoan.Padding = new Padding(15, 0, 0, 0);
            btnDangXuat.Padding = new Padding(15, 0, 0, 0);
        }
        
        private void SetSubmenuButtonsPadding()
        {
            // Thiết lập padding cho các nút submenu
            btnNhaCungCap.Padding = new Padding(35, 0, 0, 0);
            btnKhachHang.Padding = new Padding(35, 0, 0, 0);
            btnNhapHang.Padding = new Padding(35, 0, 0, 0);
            btnXuatHang.Padding = new Padding(35, 0, 0, 0);
            btnDanhMuc.Padding = new Padding(35, 0, 0, 0);
            btnSanPham.Padding = new Padding(35, 0, 0, 0);
        }
        
        private void SetupSubmenuButtonsUI(Panel submenuPanel)
        {
            foreach (Control ctrl in submenuPanel.Controls)
            {
                if (ctrl is Button)
                {
                    Button btn = (Button)ctrl;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 71, 161);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 50, 120);
                    
                    // Thêm hiệu ứng gradient color cho submenu
                    btn.Paint += (sender, e) => 
                    {
                        Button button = (Button)sender;
                        LinearGradientBrush brush = new LinearGradientBrush(
                            button.ClientRectangle,
                            Color.FromArgb(66, 165, 245),
                            Color.FromArgb(30, 136, 229),
                            90F);
                        
                        // Vẽ nền gradient chỉ khi hover
                        if (button.ClientRectangle.Contains(button.PointToClient(Cursor.Position)) 
                            && !button.Focused)
                        {
                            e.Graphics.FillRectangle(brush, button.ClientRectangle);
                        }
                        
                        // Dispose brush
                        brush.Dispose();
                    };
                }
            }
        }

        private void FTrangChuChuDaiLy_Load(object sender, EventArgs e)
        {
            // Cập nhật thông tin người dùng
            lblUserName.Text = _hoTen;
            lblRole.Text = _vaiTro;
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // Cập nhật thời gian định kỳ
            timerDateTime.Start();

            try
            {
                // Tải hình ảnh logo nếu có
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "store.png");
                if (File.Exists(logoPath))
                {
                    pictureBoxLogo.Image = Image.FromFile(logoPath);
                }
                else
                {
                    // Tạo logo mặc định nếu không có hình
                    using (Bitmap bitmap = new Bitmap(64, 64))
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.Clear(Color.FromArgb(25, 118, 210));
                        
                        using (Font font = new Font("Segoe UI", 20, FontStyle.Bold))
                        using (SolidBrush brush = new SolidBrush(Color.White))
                        {
                            g.DrawString("BK", font, brush, 12, 12);
                        }
                        
                        pictureBoxLogo.Image = new Bitmap(bitmap);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading images: " + ex.Message);
            }
            
            // Tạo hiệu ứng gradiant cho header 
            ApplyHeaderGradient();
        }
        
        private void ApplyHeaderGradient()
        {
            panelHeader.Paint += (sender, e) => {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    panelHeader.ClientRectangle,
                    Color.FromArgb(245, 245, 245), 
                    Color.FromArgb(230, 230, 230),
                    90F))
                {
                    e.Graphics.FillRectangle(brush, panelHeader.ClientRectangle);
                }
            };
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            // Cập nhật thời gian hiện tại
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        #region Highlight Active Button
        
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(25, 118, 210);
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    currentButton.Invalidate(); // Redraw button to show indicator
                }
            }
        }
        
        private void DisableButton()
        {
            foreach (Control previousBtn in panelSideMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(33, 150, 243);
                    previousBtn.ForeColor = Color.White;
                    previousBtn.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
            }
        }
        
        #endregion

        #region Xử lý submenu
        
        private void HideAllSubmenus()
        {
            // Ẩn tất cả các panel submenu
            panelDoiTacSubmenu.Visible = false;
            panelGiaoDichSubmenu.Visible = false;
            panelHangHoaSubmenu.Visible = false;
        }
        
        private void HideOtherSubmenus(Panel exceptPanel)
        {
            // Ẩn các panel khác ngoại trừ panel đã chỉ định
            if (panelDoiTacSubmenu != exceptPanel)
                panelDoiTacSubmenu.Visible = false;
                
            if (panelGiaoDichSubmenu != exceptPanel)
                panelGiaoDichSubmenu.Visible = false;
                
            if (panelHangHoaSubmenu != exceptPanel)
                panelHangHoaSubmenu.Visible = false;
        }
        
        private void ToggleSubmenu(Panel submenu)
        {
            // Nếu panel đang hiển thị thì ẩn nó đi
            if (submenu.Visible)
            {
                submenu.Visible = false;
            }
            else
            {
                // Ẩn các panel khác
                HideOtherSubmenus(submenu);
                
                // Hiện panel được chọn
                submenu.Visible = true;
            }
        }
        
        #endregion

        #region Xử lý sự kiện nút chức năng

        private void btnQuanLyDoiTac_Click(object sender, EventArgs e)
        {
            // Đánh dấu nút hiện tại là đang hoạt động
            ActivateButton(sender);
            
            // Hiện/ẩn submenu Đối tác
            ToggleSubmenu(panelDoiTacSubmenu);
        }
        
        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            // Open the supplier management form
            FQuanLyNCC frmNhaCungCap = new FQuanLyNCC();
            frmNhaCungCap.ShowDialog();
        }
        
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            // Mở form quản lý khách hàng
            MessageBox.Show("Chức năng Quản lý khách hàng đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Mã xử lý mở form quản lý khách hàng
            // var frmKhachHang = new FrmKhachHang();
            // frmKhachHang.ShowDialog();
        }

        private void btnQuanLyGiaoDich_Click(object sender, EventArgs e)
        {
            // Đánh dấu nút hiện tại là đang hoạt động
            ActivateButton(sender);
            
            // Hiện/ẩn submenu Giao dịch
            ToggleSubmenu(panelGiaoDichSubmenu);
        }
        
        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            // Mở form quản lý nhập hàng
            MessageBox.Show("Chức năng Quản lý nhập hàng đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Mã xử lý mở form quản lý nhập hàng
            // var frmNhapHang = new FrmNhapHang();
            // frmNhapHang.ShowDialog();
        }
        
        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            // Mở form quản lý xuất hàng
            MessageBox.Show("Chức năng Quản lý xuất hàng đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Mã xử lý mở form quản lý xuất hàng
            // var frmXuatHang = new FrmXuatHang();
            // frmXuatHang.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // Đánh dấu nút hiện tại là đang hoạt động
            ActivateButton(sender);
            
            // Ẩn tất cả submenu khi chọn chức năng thống kê
            HideAllSubmenus();
            
            MessageBox.Show("Chức năng Thống kê đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Mở form Thống kê ở đây
            // var frmThongKe = new FrmThongKe();
            // frmThongKe.ShowDialog();
        }

        private void btnQuanLyHangHoa_Click(object sender, EventArgs e)
        {
            // Đánh dấu nút hiện tại là đang hoạt động
            ActivateButton(sender);
            
            // Hiện/ẩn submenu Hàng hóa
            ToggleSubmenu(panelHangHoaSubmenu);
        }
        
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            // Mở form quản lý danh mục
            MessageBox.Show("Chức năng Quản lý danh mục đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Mã xử lý mở form quản lý danh mục
            // var frmDanhMuc = new FrmDanhMuc();
            // frmDanhMuc.ShowDialog();
        }
        
        private void btnSanPham_Click(object sender, EventArgs e)
        {
            // Mở form quản lý sản phẩm
            MessageBox.Show("Chức năng Quản lý sản phẩm đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Mã xử lý mở form quản lý sản phẩm
            // var frmSanPham = new FrmSanPham();
            // frmSanPham.ShowDialog();
        }

        private void btnKiemKeKho_Click(object sender, EventArgs e)
        {
            // Đánh dấu nút hiện tại là đang hoạt động
            ActivateButton(sender);
            
            // Ẩn tất cả submenu khi chọn chức năng kiểm kê kho
            HideAllSubmenus();
            
            MessageBox.Show("Chức năng Kiểm kê kho đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Mở form Kiểm kê kho ở đây
            // var frmKiemKe = new FrmKiemKeKho();
            // frmKiemKe.ShowDialog();
        }

        private void btnTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            // Đánh dấu nút hiện tại là đang hoạt động
            ActivateButton(sender);
            
            // Ẩn tất cả submenu khi chọn chức năng tạo tài khoản
            HideAllSubmenus();
            
            // Mở form đăng ký
            FrmDangKy frmDangKy = new FrmDangKy();
            frmDangKy.ShowDialog();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Đánh dấu nút hiện tại là đang hoạt động
            ActivateButton(sender);
            
            // Ẩn tất cả submenu khi chọn chức năng đăng xuất
            HideAllSubmenus();
            
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Đăng xuất và quay về form đăng nhập
                this.Close();
                // Form1 frmLogin = new Form1();
                // frmLogin.Show();
            }
        }

        #endregion
    }
}
