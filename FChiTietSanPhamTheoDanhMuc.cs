using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FChiTietSanPhamTheoDanhMuc : Form
    {
        private int maDanhMuc;
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;

        public FChiTietSanPhamTheoDanhMuc(int maDanhMuc)
        {
            InitializeComponent();
            this.maDanhMuc = maDanhMuc;
        }

        private void FChiTietSanPhamTheoDanhMuc_Load(object sender, EventArgs e)
        {
            LoadSanPhamTheoDanhMuc();
        }

        private void LoadSanPhamTheoDanhMuc()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT MaSP, TenSP, SoLuong, GiaNhap, GiaXuat, DonViTinh, TinhTrang 
                                 FROM SanPham 
                                 WHERE MaDanhMuc = @MaDanhMuc";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDanhMuc", maDanhMuc);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvSanPham.DataSource = dt;
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
