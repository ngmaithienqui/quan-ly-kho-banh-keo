using QLCuaHangBanhKeo.DAO;
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
    public partial class FSanPhamQLK: Form
    {
        SanPhamDAO spDAO = new SanPhamDAO();
        public FSanPhamQLK()
        {
            InitializeComponent();
            LoadSanPham();
        }

        private void LoadSanPham()
        {
            SanPhamDAO spDAO = new SanPhamDAO();
            //DataTable dt = spDAO.LayDanhSachSanPham();
            DataTable dt = spDAO.LayDanhSachSanPhamVaNCCVaDanhMuc();
            dgvDanhSachSP.DataSource = dt;
        }

        private void FSanPhamQLK_Load(object sender, EventArgs e)
        {
            DataTable dtNCC = spDAO.LayDanhSachNhaCungCap();
            DataTable dtDanhMuc = spDAO.LayDanhSachDanhMuc();
            
            dgvDanhSachSP.Columns["MaNCC"].Visible = false;
            dgvDanhSachSP.Columns["MaDanhMuc"].Visible = false;
            //dgvDanhSachSP.Columns["MaSp"].Visible = false;
            LoadSanPham();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            // Gọi đến DAO để tìm kiếm theo tên sản phẩm
            DataTable dt = spDAO.TimKiemSanPhamTheoTen(keyword);

            dgvDanhSachSP.DataSource = dt;
        }

        private void dgvDanhSachSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
