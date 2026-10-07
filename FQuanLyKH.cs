using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FQuanLyKH : Form
    {
        public FQuanLyKH()
        {
            InitializeComponent();
        }

        private void FQuanLyKH_Load(object sender, EventArgs e)
        {
            KhachHangDAO.CapNhatCongNo();
            LoadKHData();
        }

        private void LoadKHData()
        {
            dgvKhachHang.DataSource = KhachHangDAO.GetAllKhachHang();


            // Cập nhật công nợ cho từng khách hàng
            foreach (DataGridViewRow row in dgvKhachHang.Rows)
            {
                if (row.Cells["colMaKH"].Value != null)
                {
                    int maKH = Convert.ToInt32(row.Cells["colMaKH"].Value);
                    float congNo = KhachHangDAO.LayCongNoHienTai(maKH);  // Tính công nợ
                    row.Cells["colCongNo"].Value = congNo;  // Cập nhật cột công nợ
                }
            }

        }


        private void ClearFields()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtCongNo.Clear();
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtMaKH.Text = row.Cells["colMaKH"].Value.ToString();
                txtHoTen.Text = row.Cells["colHoTen"].Value.ToString();
                txtSDT.Text = row.Cells["colSdt"].Value.ToString();
                txtDiaChi.Text = row.Cells["colDiaChi"].Value.ToString();
                txtCongNo.Text = row.Cells["colCongNo"].Value.ToString();
            }
        }
        

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 👉 Thêm kiểm tra SĐT đúng 10 số
            if (string.IsNullOrEmpty(txtSDT.Text.Trim()) || !System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text.Trim(), @"^\d{10}$"))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            KhachHangDTO kh = new KhachHangDTO
            {
                HoTen = txtHoTen.Text.Trim(),
                Sdt = txtSDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                CongNo = float.TryParse(txtCongNo.Text, out float cn) ? cn : 0,
                LichSuGiaoDich = ""
            };

            if (KhachHangDAO.Insert(kh))
            {
                MessageBox.Show("Thêm khách hàng thành công");
                LoadKHData();
                ClearFields();
            }
        }


        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KhachHangDTO kh = new KhachHangDTO
            {
                MaKH = int.Parse(txtMaKH.Text),
                HoTen = txtHoTen.Text.Trim(),
                Sdt = txtSDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                CongNo = float.TryParse(txtCongNo.Text, out float cn) ? cn : 0
            };

            if (KhachHangDAO.Update(kh))
            {
                MessageBox.Show("Cập nhật khách hàng thành công");
                LoadKHData();
                ClearFields();
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maKH = Convert.ToInt32(dgvKhachHang.SelectedRows[0].Cells["colMaKH"].Value);

            if (KhachHangDAO.KiemTraKhachHangCoPhieuXuat(maKH))
            {
                MessageBox.Show("Không thể xóa khách hàng này vì có thông tin trong phiếu xuất hàng!",
                               "Lỗi ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (KhachHangDAO.Delete(maKH))
                {
                    MessageBox.Show("Xóa khách hàng thành công");
                    LoadKHData();
                    ClearFields();
                }
            }

        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get data from the selected row
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtMaKH.Text = row.Cells["colMaKH"].Value.ToString();
                txtHoTen.Text = row.Cells["colHoTen"].Value.ToString();
                txtSDT.Text = row.Cells["colSdt"].Value.ToString();
                txtDiaChi.Text = row.Cells["colDiaChi"].Value.ToString();
                txtCongNo.Text = row.Cells["colCongNo"].Value.ToString();
            }
        }


        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xem lịch sử giao dịch", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maKH = Convert.ToInt32(dgvKhachHang.SelectedRows[0].Cells["colMaKH"].Value);

            string ls = KhachHangDAO.GetLichSuGiaoDich(maKH);

            MessageBox.Show(string.IsNullOrEmpty(ls) ? "Chưa có lịch sử giao dịch" : ls,
                            "Lịch sử giao dịch",

                            MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLichSuGD_Click_1(object sender, EventArgs e)
        {
            // Check if a supplier is selected
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xem lịch sử giao dịch", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open transaction history form with supplier information
            int maKH = int.Parse(txtMaKH.Text);
            string tenKH = txtHoTen.Text;

            FLichSuGDKH frmLichSu = new FLichSuGDKH(maKH);
            frmLichSu.ShowDialog();
        }

        private void btnThanhToan_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn nhà cung cấp chưa
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để thanh toán công nợ", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem nhà cung cấp có công nợ không
            float congNo = 0;
            if (!float.TryParse(txtCongNo.Text, out congNo) || congNo <= 0)
            {
                MessageBox.Show("Khách hàng này không có công nợ cần thanh toán", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mở form thanh toán công nợ
            int maKH = int.Parse(txtMaKH.Text);
            string tenKH = txtHoTen.Text;

            FCongNo frmCongNo = new FCongNo(maKH, tenKH, congNo);
            if (frmCongNo.ShowDialog() == DialogResult.OK)
            {

                LoadKHData();
            }

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {
            // Chỉ xử lý nếu có mã khách hàng (tránh lúc bị Clear)
            if (!string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                int maKH;
                if (int.TryParse(txtMaKH.Text, out maKH))
                {
                    // Ví dụ: load lịch sử, hay làm gì đó
                    Console.WriteLine("Mã KH thay đổi: " + maKH);
                    // Bạn có thể gọi DAO hoặc logic nào đó tại đây nếu cần
                }
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
