
using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FLichSuGDNCC : Form
    {
        private int maNCC;

        public FLichSuGDNCC(int maNCC)
        {
            InitializeComponent();
            this.maNCC = maNCC;
        }

        private void FLichSuGDNCC_Load(object sender, EventArgs e)
        {
            NCCDAO.CapNhatCongNo(); // cập nhật lại công nợ trong DB
            LoadGiaoDich();
            LoadPhieuNhap();
            HienThiCongNoHienTai();
        }

        private void LoadGiaoDich()
        {
            // Giả sử bạn có lớp GiaoDichNCCDAO và GiaoDichNCCDTO tương tự
            List<GiaoDichNCCDTO> listGD = GiaoDichNCCDAO.LayDanhSachGiaoDich(maNCC);
            dgv_ThongTinGD.DataSource = listGD;
            dgv_ThongTinGD.Columns["MaGD"].HeaderText = "Mã GD";
            dgv_ThongTinGD.Columns["NgayGiaoDich"].HeaderText = "Ngày GD";
            dgv_ThongTinGD.Columns["SoTien"].HeaderText = "Số tiền";
            dgv_ThongTinGD.Columns["LoaiGiaoDich"].HeaderText = "Loại";
            dgv_ThongTinGD.Columns["MaPhieu"].HeaderText = "Mã phiếu";
            dgv_ThongTinGD.Columns["GhiChu"].HeaderText = "Ghi chú";
        }

        private void LoadPhieuNhap()
        {
            List<PhieuNhapHangDTO> listPN = PhieuNhapHangDAO.GetPhieuNhapByMaNCC(maNCC);
            dgv_DSPhieuNhap.DataSource = listPN;
            dgv_DSPhieuNhap.Columns["MaPhieu"].HeaderText = "Mã phiếu";
            dgv_DSPhieuNhap.Columns["NgayNhap"].HeaderText = "Ngày nhập";
            dgv_DSPhieuNhap.Columns["ThanhTien"].HeaderText = "Tổng tiền";
            dgv_DSPhieuNhap.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgv_DSPhieuNhap.Columns["TrangThaiThanhToan"].HeaderText = "Trạng thái thanh toán ";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        

        private void HienThiCongNoHienTai()
        {
            float congNo = NCCDAO.LayCongNoHienTai(maNCC);
            // Hiển thị công nợ lên form (thêm control hiển thị)
            // Ví dụ: lblCongNo.Text = congNo.ToString("N0") + " VNĐ";
        }
        private void dgv_ThongTinGD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnDong_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}