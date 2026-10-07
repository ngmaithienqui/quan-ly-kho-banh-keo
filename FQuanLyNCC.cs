using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FQuanLyNCC : Form
    {
        public FQuanLyNCC()
        {
            InitializeComponent();
        }

        private void FQuanLyNCC_Load(object sender, EventArgs e)
        {
            NCCDAO.CapNhatCongNo();
            LoadNCCData();
        }

        private void LoadNCCData()
        {
            dgvNhaCungCap.DataSource = NCCDAO.GetAllNCC();

            foreach (DataGridViewRow row in dgvNhaCungCap.Rows)
            {
                if (row.Cells["colMaNCC"].Value != null)
                {
                    int maNCC = Convert.ToInt32(row.Cells["colMaNCC"].Value);
                    float congNo = NCCDAO.LayCongNoHienTai(maNCC);
                    row.Cells["colCongNo"].Value = congNo;
                }
            }
        }

        private void ClearFields()
        {
            txtMaNCC.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtCongNo.Clear();
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp");
                return;
            }

            // 👉 Thêm kiểm tra số điện thoại đúng 10 số
            if (string.IsNullOrEmpty(txtSDT.Text.Trim()) || !System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text.Trim(), @"^\d{10}$"))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            var ncc = new NhaCCDTO
            {
                HoTen = txtHoTen.Text.Trim(),
                Sdt = txtSDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                CongNo = float.TryParse(txtCongNo.Text, out float cn) ? cn : 0
            };

            if (NCCDAO.Insert(ncc))
            {
                MessageBox.Show("Thêm nhà cung cấp thành công");
                LoadNCCData();
                ClearFields();
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa");
                return;
            }

            var ncc = new NhaCCDTO
            {
                MaNCC = int.Parse(txtMaNCC.Text),
                HoTen = txtHoTen.Text.Trim(),
                Sdt = txtSDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                CongNo = float.TryParse(txtCongNo.Text, out float cn) ? cn : 0
            };

            if (NCCDAO.Update(ncc))
            {
                MessageBox.Show("Cập nhật nhà cung cấp thành công");
                LoadNCCData();
                ClearFields();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 0) return;

            int maNCC = Convert.ToInt32(dgvNhaCungCap.SelectedRows[0].Cells["colMaNCC"].Value);
            if (NCCDAO.KiemTraNCCCoPhieuXuat(maNCC))
            {
                MessageBox.Show("Không thể xóa nhà cung cấp này vì có thông tin trong phiếu nhập hàng!",
                               "Lỗi ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (NCCDAO.Delete(maNCC))
                {
                    MessageBox.Show("Xóa nhà cung cấp thành công");
                    LoadNCCData();
                    ClearFields();
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để thanh toán công nợ");
                return;
            }

            if (!float.TryParse(txtCongNo.Text, out float congNo) || congNo <= 0)
            {
                MessageBox.Show("Nhà cung cấp này không có công nợ");
                return;
            }

            int maNCC = int.Parse(txtMaNCC.Text);
            string tenNCC = txtHoTen.Text;

            FCongNoNCC frmCongNo = new FCongNoNCC(maNCC, tenNCC, congNo);
            if (frmCongNo.ShowDialog() == DialogResult.OK)
            {
                LoadNCCData();
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLichSuGD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xem lịch sử giao dịch");
                return;
            }

            int maNCC = int.Parse(txtMaNCC.Text);
            FLichSuGDNCC frm = new FLichSuGDNCC(maNCC);
            frm.ShowDialog();
        }

        private void txtMaNCC_TextChanged(object sender, EventArgs e)
        {
            // Chỉ xử lý nếu có mã khách hàng (tránh lúc bị Clear)
            if (!string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                int maKH;
                if (int.TryParse(txtMaNCC.Text, out maKH))
                {
                    // Ví dụ: load lịch sử, hay làm gì đó
                    Console.WriteLine("Mã KH thay đổi: " + maKH);
                    // Bạn có thể gọi DAO hoặc logic nào đó tại đây nếu cần
                }
            }
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvNhaCungCap.Rows[e.RowIndex];

                txtMaNCC.Text = row.Cells["colMaNCC"].Value.ToString();
                txtHoTen.Text = row.Cells["colHoTen"].Value.ToString();
                txtSDT.Text = row.Cells["colSdt"].Value.ToString();
                txtDiaChi.Text = row.Cells["colDiaChi"].Value.ToString();
                txtCongNo.Text = row.Cells["colCongNo"].Value.ToString();
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
