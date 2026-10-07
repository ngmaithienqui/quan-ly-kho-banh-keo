using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;

namespace QLCuaHangBanhKeo
{
    public partial class FChiTietKiemKe : Form
    {
        private int _maPhieu;

        // Constructor nhận mã phiếu kiểm kê
        public FChiTietKiemKe(int maPhieu)
        {
            InitializeComponent();
            _maPhieu = maPhieu;
            this.Load += FChiTietKiemKe_Load_1;
        }

        // Hàm tải chi tiết phiếu kiểm kê
        private void LoadChiTietPhieu()
        {
            List<ChiTietPhieuKiemKeDTO> chitiet = ChiTietPhieuKiemKeDAO.LayChiTietTheoPhieu(_maPhieu);

            // Configure the DataGridView first
            dgv_ChiTietKiemKe.AutoGenerateColumns = false;

            // Clear existing columns if any
            dgv_ChiTietKiemKe.Columns.Clear();

            // Create columns programmatically
            dgv_ChiTietKiemKe.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaChiTiet",
                HeaderText = "Mã chi tiết",
                DataPropertyName = "MaChiTiet"
            });

            dgv_ChiTietKiemKe.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaPhieu",
                HeaderText = "Mã phiếu",
                DataPropertyName = "MaPhieu"
            });

            dgv_ChiTietKiemKe.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaSP",
                HeaderText = "Mã sản phẩm",
                DataPropertyName = "MaSP"
            });

            dgv_ChiTietKiemKe.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenSP",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "TenSP"
            });

            dgv_ChiTietKiemKe.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSLSanPhamThuc",
                HeaderText = "Số lượng sản phẩm thực tế",
                DataPropertyName = "SoLuongSPThuc"
            });

            dgv_ChiTietKiemKe.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoSPSaiLech",
                HeaderText = "Số lượng sản phẩm sai lệch",
                DataPropertyName = "SoLuongSaiLech"
            });

            dgv_ChiTietKiemKe.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLyDo",
                HeaderText = "Lý do",
                DataPropertyName = "LyDo"
            });

            // Now set the data source
            dgv_ChiTietKiemKe.DataSource = chitiet;
        }

        // Hàm xử lý khi click vào cell (nếu cần)
        private void dgv_ChiTietKiemKe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý nếu cần
        }
        private void FChiTietKiemKe_Load_1(object sender, EventArgs e)
        {
            LoadChiTietPhieu();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
