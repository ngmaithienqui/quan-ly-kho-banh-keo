using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FXemPhieuXuatHangQLK: Form
    {
        private readonly PhieuXuatHangDAO phieuXuatDAO = new PhieuXuatHangDAO();
        private int selectedMaPhieu = -1;

        public FXemPhieuXuatHangQLK()
        {
            InitializeComponent();
        }

        private void FXemPhieuXuatHangQLK_Load(object sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            ConfigureDataGridView();
            LoadPhieuXuat();
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

        private void ConfigureDataGridView()
        {
            dgvPhieuXuat.AutoGenerateColumns = false;
            dgvPhieuXuat.RowHeadersVisible = false;
            dgvPhieuXuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieuXuat.ReadOnly = true;
        }

        private void dgvPhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPhieuXuat.Rows[e.RowIndex].DataBoundItem as PhieuXuatHangDTO;
                if (row != null)
                {
                    selectedMaPhieu = row.MaPhieu;
                    btnChiTiet.Enabled = true;
                }
            }
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}
