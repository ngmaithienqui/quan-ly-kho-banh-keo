using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FNVXemPhieuXuat : Form
    {
        //private readonly NVXemPhieuXuatDAO phieuXuatDAO = new NVXemPhieuXuatDAO();

        private readonly PhieuXuatDAO phieuXuatDAO = new PhieuXuatDAO();
        private int selectedMaPhieu = -1;

        public FNVXemPhieuXuat()
        {
            InitializeComponent();
        }

        private void FNVXemPhieuXuat_Load(object sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;


            ConfigureDataGridView();
            LoadPhieuXuat();
        }

        private void ConfigureDataGridView()
        {
            dgvPhieuXuat.AutoGenerateColumns = false;
            dgvPhieuXuat.RowHeadersVisible = false;
            dgvPhieuXuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieuXuat.ReadOnly = true;
        }

        private void LoadPhieuXuat()
        {
            string trangThai = cboTrangThai.Text;
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            var list = phieuXuatDAO.LayPhieuXuat(trangThai, tuNgay, denNgay);
            dgvPhieuXuat.DataSource = list;

            lblNoData.Visible = list.Count == 0;
            btnChiTiet.Enabled = false;
        }

        private void dgvPhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPhieuXuat.Rows[e.RowIndex].DataBoundItem as PhieuXuatDTO;
                if (row != null)
                {
                    selectedMaPhieu = row.MaPhieu;
                    btnChiTiet.Enabled = true;
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadPhieuXuat();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu > 0)
            {
                FNVXemChiTietPhieuXuat form = new FNVXemChiTietPhieuXuat(selectedMaPhieu, true);
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Người dùng đã thao tác cập nhật => load lại danh sách
                    LoadPhieuXuat();
                }
            }
        }

        private void dgvPhieuXuat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Không cần xử lý gì thêm ở đây.
        }
        private void btnXuatThanhCong_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu > 0)
            {
                if (MessageBox.Show("Xác nhận xuất hàng thành công?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (phieuXuatDAO.LayTrangThaiPhieuXuat(selectedMaPhieu, "Đã xuất hàng"))
                    {
                        MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo");
                        LoadPhieuXuat(); // Refresh lại danh sách
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật trạng thái thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void btnXuatThatBai_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu > 0)
            {
                if (MessageBox.Show("Xác nhận xuất hàng thất bại?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (phieuXuatDAO.LayTrangThaiPhieuXuat(selectedMaPhieu, "Chờ xử lý"))
                    {
                        MessageBox.Show("Cập nhật trạng thái thất bại!", "Thông báo");
                        LoadPhieuXuat();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgvPhieuXuat_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
