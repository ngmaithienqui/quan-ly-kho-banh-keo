using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FLichSuGDKH : Form
    {
        private int maKH;

        public FLichSuGDKH(int maKH)
        {
            InitializeComponent();
            this.maKH = maKH;
        }

        private void FLichSuGDKH_Load(object sender, EventArgs e)
        {
            KhachHangDAO.CapNhatCongNo(); // cập nhật lại công nợ trong DB
            LoadGiaoDich();
            LoadPhieuXuat();
            HienThiCongNoHienTai();
            
        }

        private void LoadGiaoDich()
        {
            List<GiaoDichKHDTO> listGD = GiaoDichKHDAO.LayDanhSachGiaoDich(maKH);
            dgv_ThongTinGD.DataSource = listGD;
            dgv_ThongTinGD.Columns["MaGD"].HeaderText = "Mã GD";
            dgv_ThongTinGD.Columns["NgayGiaoDich"].HeaderText = "Ngày GD";
            dgv_ThongTinGD.Columns["SoTien"].HeaderText = "Số tiền";
            dgv_ThongTinGD.Columns["LoaiGiaoDich"].HeaderText = "Loại";
            dgv_ThongTinGD.Columns["MaPhieu"].HeaderText = "Mã phiếu";
            dgv_ThongTinGD.Columns["GhiChu"].HeaderText = "Ghi chú";
        }

        private void LoadPhieuXuat()
        {
            List<PhieuXuatHangDTO> listPX = PhieuXuatHangDAO.GetPhieuXuatByMaKH(maKH);
            dgv_DSPhieuXuat.DataSource = listPX;
            dgv_DSPhieuXuat.Columns["MaPhieu"].HeaderText = "Mã phiếu";
            dgv_DSPhieuXuat.Columns["NgayXuatHang"].HeaderText = "Ngày xuất";
            dgv_DSPhieuXuat.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgv_DSPhieuXuat.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgv_DSPhieuXuat.Columns["TrangThaiThanhToan"].HeaderText = "Trạng thái thanh toán ";

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void HienThiCongNoHienTai()
        {
            float congNo = PhieuXuatHangDAO.TinhCongNoChuaThanhToan(maKH);
            
        }

        private void dgv_ThongTinGD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
