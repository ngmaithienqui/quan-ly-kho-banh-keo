using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FLichSuKiemKe : Form
    {
        public FLichSuKiemKe()
        {
            InitializeComponent();
            Load += FLichSuKiemKe_Load_1;
           // dgv_LichSuKiemKe.CellContentClick += dgv_LichSuKiemKe_CellContentClick;
        }

        

        private void LoadLichSuKiemKe()
        {
            dgv_LichSuKiemKe.Columns.Clear();
            dgv_LichSuKiemKe.Rows.Clear();

            dgv_LichSuKiemKe.Columns.Add("MaPhieu", "Mã Phiếu");
            dgv_LichSuKiemKe.Columns["MaPhieu"].Visible = false;

            dgv_LichSuKiemKe.Columns.Add("NgayLap", "Ngày Lập");
            dgv_LichSuKiemKe.Columns.Add("TrangThai", "Trạng Thái");
            dgv_LichSuKiemKe.Columns.Add("NguoiLapPhieu", "Người Lập");

            DataGridViewButtonColumn btnChiTiet = new DataGridViewButtonColumn
            {
                Name = "btnChiTiet",
                HeaderText = "Thao Tác",
                Text = "Chi tiết",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgv_LichSuKiemKe.Columns.Add(btnChiTiet);

            // Lấy dữ liệu từ DAO
            List<PhieuKiemKeDTO> danhSach = PhieuKiemKeDAO.GetLichSuKiemKe();
            foreach (var phieu in danhSach)
            {
                dgv_LichSuKiemKe.Rows.Add(
                    phieu.MaPhieu,
                    phieu.NgayLap.ToString("dd/MM/yyyy"),
                    phieu.TrangThai,
                    phieu.NguoiLapPhieu
                );
            }
        }

        private void dgv_LichSuKiemKe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_LichSuKiemKe.Columns[e.ColumnIndex].Name == "btnChiTiet" && e.RowIndex >= 0)
            {
                int maPhieu = Convert.ToInt32(dgv_LichSuKiemKe.Rows[e.RowIndex].Cells["MaPhieu"].Value);
                FChiTietKiemKe formChiTiet = new FChiTietKiemKe(maPhieu);
                formChiTiet.ShowDialog();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FLichSuKiemKe_Load_1(object sender, EventArgs e)
        {
            LoadLichSuKiemKe();
        }
    }
}
