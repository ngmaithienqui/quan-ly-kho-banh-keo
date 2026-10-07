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
    public partial class FTrangchuQuanlykho : Form
    {
        // Khai báo biến thành viên để lưu thông tin người dùng đăng nhập
        private string _hoTen;
        private string _vaiTro;
        private Button currentButton;

        public FTrangchuQuanlykho()
        {
            InitializeComponent();
            // Thiết lập giá trị mặc định
            _hoTen = "Nhân viên kho";
            _vaiTro = "Quản lý kho";
            
            // Ẩn tất cả các submenu khi khởi động
            HideAllSubmenus();
            
            // Thiết lập giao diện nút
            SetupButtonUI();
        }

        // Constructor với thông tin người dùng đăng nhập
        public FTrangchuQuanlykho(string hoTen, string vaiTro)
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
            // Thiết lập padding cho các nút
            SetMainButtonsPadding();
            
            // Thiết lập nút trong submenu
            SetupSubmenuButtonsUI(panelGiaoDichSubmenu);
            SetupSubmenuButtonsUI(panelSanPhamSubmenu);
            SetupSubmenuButtonsUI(panelKiemKeSubmenu);
            
            // Tải biểu tượng cho các nút menu chính
            try
            {
                string iconFolder = Path.Combine(Application.StartupPath, "Resources", "Icons");
                
                // Tải biểu tượng cho menu chính
                LoadButtonIcon(btnQuanLyGiaoDich, Path.Combine(iconFolder, "transaction.png"));
                LoadButtonIcon(btnQuanLySanPham, Path.Combine(iconFolder, "products.png"));
                LoadButtonIcon(btnDanhMuc, Path.Combine(iconFolder, "category.png"));
                LoadButtonIcon(btnKiemKeKho, Path.Combine(iconFolder, "inventory.png"));
                LoadButtonIcon(btnDangXuat, Path.Combine(iconFolder, "logout.png"));
                
                // Tải logo
                string logoPath = Path.Combine(iconFolder, "store-logo.png");
                if (File.Exists(logoPath))
                {
                    pictureBoxLogo.Image = Image.FromFile(logoPath);
                    pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải biểu tượng: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void SetMainButtonsPadding()
        {
            // Thiết lập padding cho các nút menu chính
            btnQuanLyGiaoDich.Padding = new Padding(15, 0, 0, 0);
            btnQuanLySanPham.Padding = new Padding(15, 0, 0, 0);
            btnDanhMuc.Padding = new Padding(15, 0, 0, 0);
            btnKiemKeKho.Padding = new Padding(15, 0, 0, 0);
            btnDangXuat.Padding = new Padding(15, 0, 0, 0);
        }
        
        private void SetSubmenuButtonsPadding()
        {
            // Thiết lập padding cho các nút trong các submenu
            foreach (Control control in panelGiaoDichSubmenu.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).Padding = new Padding(35, 0, 0, 0);
                }
            }
            
            foreach (Control control in panelSanPhamSubmenu.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).Padding = new Padding(35, 0, 0, 0);
                }
            }
            
            foreach (Control control in panelKiemKeSubmenu.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).Padding = new Padding(35, 0, 0, 0);
                }
            }
        }
        
        private void SetupSubmenuButtonsUI(Panel submenuPanel)
        {
            // Thiết lập giao diện cho các nút trong submenu
            foreach (Control control in submenuPanel.Controls)
            {
                if (control is Button)
                {
                    Button btn = (Button)control;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ForeColor = Color.White;
                    btn.Dock = DockStyle.Top;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(35, 0, 0, 0);
                }
            }
        }
        
        private void LoadButtonIcon(Button btn, string iconPath)
        {
            if (File.Exists(iconPath))
            {
                try
                {
                    btn.Image = Image.FromFile(iconPath);
                    btn.ImageAlign = ContentAlignment.MiddleLeft;
                    btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                }
                catch (Exception) 
                {
                    // Xử lý lỗi khi tải hình ảnh
                    btn.Image = null;
                }
            }
        }
        
        private void HideAllSubmenus()
        {
            panelGiaoDichSubmenu.Visible = false;
            panelSanPhamSubmenu.Visible = false;
            panelKiemKeSubmenu.Visible = false;
        }
        
        private void HideSubmenu()
        {
            if (panelGiaoDichSubmenu.Visible)
                panelGiaoDichSubmenu.Visible = false;
            if (panelSanPhamSubmenu.Visible)
                panelSanPhamSubmenu.Visible = false;
            if (panelKiemKeSubmenu.Visible)
                panelKiemKeSubmenu.Visible = false;
        }
        
        private void ShowSubmenu(Panel submenu)
        {
            if (!submenu.Visible)
            {
                HideSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }
        
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = Color.FromArgb(0, 150, 136); // Màu chính khi nút được kích hoạt
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                }
            }
        }
        
        private void DisableButton()
        {
            foreach (Control previousBtn in panelSideMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(33, 150, 243); // Màu nền mặc định
                    previousBtn.ForeColor = Color.White;
                }
            }
        }

        private void FTrangchuQuanlykho_Load(object sender, EventArgs e)
        {
            // Cập nhật thông tin người dùng
            lblUserName.Text = _hoTen;
            lblRole.Text = _vaiTro;
            
            // Thiết lập thời gian hiện tại
            UpdateDateTime();
        }
        
        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }
        
        private void UpdateDateTime()
        {
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        
        #region Menu Click Events
        
        private void btnQuanLyGiaoDich_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            ShowSubmenu(panelGiaoDichSubmenu);
        }
        
        private void btnXemPhieuNhap_Click(object sender, EventArgs e)
        {
            // Xử lý khi nhấn nút Xem phiếu nhập
            // MessageBox.Show("Chức năng Xem phiếu nhập hàng đang được phát triển.", "Thông báo", 
            //   MessageBoxButtons.OK, MessageBoxIcon.Information);
            //HideSubmenu();
            var frmNhapHangQLK = new FXemPhieuNhapHangQLK();
            frmNhapHangQLK.ShowDialog();

        }
        
        private void btnXemPhieuXuat_Click(object sender, EventArgs e)
        {
            //// Xử lý khi nhấn nút Xem phiếu xuất
            //MessageBox.Show("Chức năng Xem phiếu xuất hàng đang được phát triển.", "Thông báo", 
            //    MessageBoxButtons.OK, MessageBoxIcon.Information);
            //HideSubmenu();
            FXemPhieuXuatHangQLK fXemPhieuXuatHangQLK = new FXemPhieuXuatHangQLK();
            fXemPhieuXuatHangQLK.ShowDialog();
        }
        
        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            ShowSubmenu(panelSanPhamSubmenu);
        }
        
        private void btnXemSanPham_Click(object sender, EventArgs e)
        {
            //// Xử lý khi nhấn nút Xem sản phẩm
            //MessageBox.Show("Chức năng Xem sản phẩm đang được phát triển.", "Thông báo", 
            //    MessageBoxButtons.OK, MessageBoxIcon.Information);
            //HideSubmenu();
            FSanPhamQLK fSanPhamQLK = new FSanPhamQLK();
            fSanPhamQLK.ShowDialog();
        }
        
        private void btnCapNhatSoLuong_Click(object sender, EventArgs e)
        {
            // Xử lý khi nhấn nút Cập nhật số lượng
            MessageBox.Show("Chức năng Cập nhật số lượng sản phẩm đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            HideSubmenu();
        }
        
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            //ActivateButton(sender);
            //HideSubmenu();

            //// Xử lý khi nhấn nút Danh mục
            //MessageBox.Show("Chức năng Xem danh mục đang được phát triển.", "Thông báo", 
            //    MessageBoxButtons.OK, MessageBoxIcon.Information);
            FDanhMucQLK fDanhMucQLK = new FDanhMucQLK();
            fDanhMucQLK.ShowDialog();
        }
        
        private void btnKiemKeKho_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            ShowSubmenu(panelKiemKeSubmenu);
        }
        
        private void btnLapPhieuKiemKe_Click(object sender, EventArgs e)
        {
            // Xử lý khi nhấn nút Lập phiếu kiểm kê
           // MessageBox.Show("Chức năng Lập phiếu kiểm kê đang được phát triển.", "Thông báo", 
               // MessageBoxButtons.OK, MessageBoxIcon.Information);
            //HideSubmenu();
            FLapKiemKe frmKiemKe = new FLapKiemKe();
            frmKiemKe.ShowDialog();
        }
        
        private void btnLichSuKiemKe_Click(object sender, EventArgs e)
        {
            // Xử lý khi nhấn nút Xem lịch sử kiểm kê
            // MessageBox.Show("Chức năng Xem lịch sử kiểm kê đang được phát triển.", "Thông báo", 
            //   MessageBoxButtons.OK, MessageBoxIcon.Information);
            //HideSubmenu();
            FLichSuKiemKe frmKiemKe = new FLichSuKiemKe();
            frmKiemKe.ShowDialog();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            FLapBangDuTru fLapBangDuTru = new FLapBangDuTru();
            fLapBangDuTru.ShowDialog();
        }

        #endregion

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            HideAllSubmenus();

            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();  // Ẩn form hiện tại
                FDangNhap frmLogin = new FDangNhap();
                frmLogin.ShowDialog();  // Show form đăng nhập dạng modal
                this.Close();  // Đóng form chính sau khi đăng nhập lại hoặc thoát
            }
        }
    }
}
