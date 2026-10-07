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
    public partial class FDanhMucQLK: Form
    {
        private DanhMucDAO danhMucDAO = new DanhMucDAO();
        private DataTable originalDataTable;
        public FDanhMucQLK()
        {
            InitializeComponent();
        }

        private void FDanhMucQLK_Load(object sender, EventArgs e)
        {
            LoadDanhSachDanhMuc();
        }

        private void LoadDanhSachDanhMuc()
        {
            List<DanhMucDTO> ds = danhMucDAO.LayTatCaDanhMuc();

            // Convert List<DanhMucDTO> to DataTable
            originalDataTable = new DataTable();
            originalDataTable.Columns.Add("MaDanhMuc", typeof(int));
            originalDataTable.Columns.Add("TenDanhMuc", typeof(string));
            originalDataTable.Columns.Add("MoTa", typeof(string));

            foreach (var item in ds)
            {
                originalDataTable.Rows.Add(item.MaDanhMuc, item.TenDanhMuc, item.MoTa);
            }

            dgvDanhMucSP.DataSource = originalDataTable;
        }

        private void dgvDanhMucSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhMucSP.Rows[e.RowIndex];

                txtMaDanhMuc.Text = row.Cells["colMaDanhMuc"].Value.ToString();
                txtTenDanhMuc.Text = row.Cells["colTenDanhMuc"].Value.ToString();
                txtMoTa.Text = row.Cells["colMoTa"].Value.ToString();


            }
        }

        private void dgvDanhMucSP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maDanhMuc = Convert.ToInt32(dgvDanhMucSP.Rows[e.RowIndex].Cells["colMaDanhMuc"].Value);  // hoặc Cells[0]
                FChiTietSanPhamTheoDanhMuc form = new FChiTietSanPhamTheoDanhMuc(maDanhMuc);
                form.ShowDialog();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập tên danh mục để tìm kiếm..." && txtTimKiem.ForeColor == System.Drawing.Color.Gray)
            {
                return;
            }

            string searchText = txtTimKiem.Text.Trim();

            if (originalDataTable == null)
            {
                // If original data is not available, reload the data
                LoadDanhMucData();
                return;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                // If search text is empty, show all data
                dgvDanhMucSP.DataSource = originalDataTable;
                return;
            }

            try
            {
                // Create a new filtered DataTable
                DataTable filteredTable = new DataTable();
                using (DataTable _ = filteredTable = originalDataTable.Clone()) ; // Copy structure without data

                // Search by name starting with the entered text (case insensitive)
                foreach (DataRow row in originalDataTable.Rows)
                {
                    string tenDanhMuc = row["TenDanhMuc"].ToString().ToLower();
                    if (tenDanhMuc.StartsWith(searchText.ToLower()))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                // Update the DataGridView
                dgvDanhMucSP.DataSource = filteredTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhMucData()
        {
            throw new NotImplementedException();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
