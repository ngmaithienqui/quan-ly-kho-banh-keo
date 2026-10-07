using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;

namespace QLCuaHangBanhKeo
{
    public partial class FQuanLyDanhMuc : Form
    {
        private DanhMucDAO danhMucDAO = new DanhMucDAO();
        private DataTable originalDataTable;

        public FQuanLyDanhMuc()
        {
            InitializeComponent();
        }

        private void FQuanLyDanhMuc_Load(object sender, EventArgs e)
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

        


        private void btnThem_Click(object sender, EventArgs e)
        {
            DanhMucDTO dm = new DanhMucDTO
            {
                TenDanhMuc = txtTenDanhMuc.Text,
                MoTa = txtMoTa.Text
            };

            if (danhMucDAO.ThemDanhMuc(dm))
            {
                MessageBox.Show("Thêm danh mục thành công!");
                LoadDanhSachDanhMuc();
            }
            else
            {
                MessageBox.Show("Thêm thất bại.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDanhMucSP.CurrentRow != null)
            {
                DanhMucDTO dm = new DanhMucDTO
                {
                    MaDanhMuc = Convert.ToInt32(dgvDanhMucSP.CurrentRow.Cells["colMaDanhMuc"].Value),
                    TenDanhMuc = txtTenDanhMuc.Text,
                    MoTa = txtMoTa.Text
                };

                if (danhMucDAO.CapNhatDanhMuc(dm))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadDanhSachDanhMuc();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhMucSP.CurrentRow != null)
            {
                int maDM = Convert.ToInt32(dgvDanhMucSP.CurrentRow.Cells["colMaDanhMuc"].Value);
                if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (danhMucDAO.XoaDanhMuc(maDM))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadDanhSachDanhMuc();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại.");
                    }
                }
            }
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
        /// <summary>
        /// /////////////////////

        private void dgvDanhMucSP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maDanhMuc = Convert.ToInt32(dgvDanhMucSP.Rows[e.RowIndex].Cells["colMaDanhMuc"].Value);  // hoặc Cells[0]
                FChiTietSanPhamTheoDanhMuc form = new FChiTietSanPhamTheoDanhMuc(maDanhMuc);
                form.ShowDialog();
            }
        }





        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Don't filter if the text is the placeholder
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

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
