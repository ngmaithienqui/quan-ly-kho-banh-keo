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
    public partial class FDanhSachSP : Form
    {
        SanPhamDAO spDAO = new SanPhamDAO();
        private int maSanPhamDangChinhSua;
        public FDanhSachSP()
        {
            InitializeComponent();
            LoadSanPham();
        }

        private void FDanhSachSP_Load(object sender, EventArgs e)
        {
            DataTable dtNCC = spDAO.LayDanhSachNhaCungCap();
            DataTable dtDanhMuc = spDAO.LayDanhSachDanhMuc();
            cbNhaCungCap.DataSource = dtNCC;
            cbNhaCungCap.DisplayMember = "HoTen";
            cbNhaCungCap.ValueMember = "MaNCC";

            cbDanhMucSP.DataSource = dtDanhMuc;
            cbDanhMucSP.DisplayMember = "TenDanhMuc";
            cbDanhMucSP.ValueMember = "MaDanhMuc";
            dgvDanhSachSP.Columns["MaNCC"].Visible = false;
            dgvDanhSachSP.Columns["MaDanhMuc"].Visible = false;
            //dgvDanhSachSP.Columns["MaSp"].Visible = false;
            LoadSanPham();
        }

        private void LoadSanPham()
        {
            SanPhamDAO spDAO = new SanPhamDAO();
            //DataTable dt = spDAO.LayDanhSachSanPham();
            DataTable dt = spDAO.LayDanhSachSanPhamVaNCCVaDanhMuc();
            dgvDanhSachSP.DataSource = dt;
        }

        

       

        

       

        private void dtpNgayNhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private bool KiemTraDuLieuHopLe()
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm.");
                return false;
            }

            if (cbNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp.");
                return false;
            }

            if (cbDanhMucSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục sản phẩm.");
                return false;
            }

            if (numGiaNhap.Value <= 0 || numGiaXuat.Value <= 0)
            {
                MessageBox.Show("Giá nhập và giá xuất phải lớn hơn 0.");
                return false;
            }
            if (numSoLuong.Value <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0.");
                return false;
            }

            if (numGiaNhap.Value > numGiaXuat.Value)
            {
                MessageBox.Show("Giá nhập không được lớn hơn giá xuất.");
                return false;
            }

            if (cbDonViTinh.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cbDonViTinh.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính.");
                return false;
            }

            if (dtpHanSuDung.Value < dtpNgayNhap.Value)
            {
                MessageBox.Show("Hạn sử dụng không được bé hơn ngày nhập.");
                return false;
            }

            if (cbTinhTrang.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cbTinhTrang.Text))
            {
                MessageBox.Show("Vui lòng chọn tình trạng.");
                return false;
            }

            return true;
        }

        
        private void txtTimKiem_TextChanged_1(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            // Gọi đến DAO để tìm kiếm theo tên sản phẩm
            DataTable dt = spDAO.TimKiemSanPhamTheoTen(keyword);

            dgvDanhSachSP.DataSource = dt;
        }

       

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SanPhamDTO sp = new SanPhamDTO(maSanPhamDangChinhSua);

                if (!spDAO.XoaSanPham(sp))
                {
                    MessageBox.Show("Không thể xóa sản phẩm vì đã tồn tại trong các phiếu giao dịch.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSanPham();
            }
        }

        private void btnThemSanPham_Click_1(object sender, EventArgs e)
        {
            if (!KiemTraDuLieuHopLe()) return;

            string tenSP = txtTenSP.Text.Trim();
            int maNCC = Convert.ToInt32(cbNhaCungCap.SelectedValue);

            // 💬 Check tên sản phẩm có tồn tại cùng nhà cung cấp chưa
            if (spDAO.KiemTraTrungSanPham(tenSP, maNCC))
            {
                MessageBox.Show("Tên sản phẩm đã tồn tại với nhà cung cấp này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu không bị trùng, mới cho thêm
            SanPhamDTO sp = new SanPhamDTO(
                maNCC,
                Convert.ToInt32(cbDanhMucSP.SelectedValue),
                tenSP,
                Convert.ToInt32(numSoLuong.Value),
                Convert.ToDouble(numGiaNhap.Value),
                Convert.ToDouble(numGiaXuat.Value),
                cbDonViTinh.Text,
                txtMoTa.Text,
                dtpNgayNhap.Value,
                dtpHanSuDung.Value,
                cbTinhTrang.Text
            );

            spDAO.ThemSanPham(sp.MaNCC, sp.MaDanhMuc, sp.TenSP, sp.SoLuong, sp.GiaNhap, sp.GiaXuat, sp.DonViTinh, sp.MoTa, sp.NgayNhap, sp.HanSuDung, sp.TinhTrang);

            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadSanPham();
        }


        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {
            if (!KiemTraDuLieuHopLe()) return;

            SanPhamDTO sp = new SanPhamDTO(
            maSanPhamDangChinhSua,
            Convert.ToInt32(cbNhaCungCap.SelectedValue),
            Convert.ToInt32(cbDanhMucSP.SelectedValue),
            txtTenSP.Text,
            Convert.ToInt32(numSoLuong.Value),
            Convert.ToDouble(numGiaNhap.Value),
            Convert.ToDouble(numGiaXuat.Value),
            cbDonViTinh.Text,
            txtMoTa.Text,
            dtpNgayNhap.Value,
            dtpHanSuDung.Value,
            cbTinhTrang.Text
);



            spDAO.CapNhatSanPham(sp);

            MessageBox.Show("Cập nhật thành công!");
            LoadSanPham();
        }

        private void dgvDanhSachSP_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachSP.Rows[e.RowIndex];
                maSanPhamDangChinhSua = Convert.ToInt32(row.Cells["MaSP"].Value);
                txtTenSP.Text = row.Cells["TenSP"].Value.ToString();
                numSoLuong.Value = Convert.ToInt32(row.Cells["SoLuong"].Value);
                cbNhaCungCap.SelectedValue = row.Cells["MaNCC"].Value;
                cbDanhMucSP.SelectedValue = row.Cells["MaDanhMuc"].Value;
                numGiaNhap.Value = Convert.ToDecimal(row.Cells["GiaNhap"].Value);
                numGiaXuat.Value = Convert.ToDecimal(row.Cells["GiaXuat"].Value);
                cbDonViTinh.Text = row.Cells["DonViTinh"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                dtpHanSuDung.Value = Convert.ToDateTime(row.Cells["HanSuDung"].Value);
                cbTinhTrang.Text = row.Cells["TinhTrang"].Value.ToString();
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
