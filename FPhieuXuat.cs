using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Drawing.Printing;
using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;

namespace QLCuaHangBanhKeo
{
    public partial class FPhieuXuat : Form
    {
        private readonly PhieuXuatDAO phieuXuatDAO = new PhieuXuatDAO();
        private int selectedMaPhieu = -1;
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;

        public FPhieuXuat()
        {
            InitializeComponent();
        }

        private void FPhieuXuat_Load(object sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            ConfigureDataGridView();
            LoadPhieuXuat();
        }

        private void ConfigureDataGridView()
        {
            dgvPhieuXuat.AutoGenerateColumns = false;
            dgvPhieuXuat.RowHeadersVisible = false;
            dgvPhieuXuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieuXuat.ReadOnly = true;
        }

        private void LoadPhieuXuat()
        {
          
            string trangThai = cboTrangThai.Text;
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            var list = phieuXuatDAO.LayPhieuXuat(trangThai, tuNgay, denNgay);
            dgvPhieuXuat.DataSource = list;

            lblNoData.Visible = list.Count == 0;
            btnSua.Enabled = btnXoa.Enabled = btnChiTiet.Enabled = btnInPhieu.Enabled = false;
        }

        private void dgvPhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPhieuXuat.Rows[e.RowIndex].DataBoundItem as PhieuXuatDTO;
                selectedMaPhieu = row.MaPhieu;
                string trangThaiThanhToan = row.TrangThaiThanhToan;
                btnSua.Enabled = trangThaiThanhToan != "Đã thanh toán";
                btnXoa.Enabled = trangThaiThanhToan != "Đã thanh toán";
                btnChiTiet.Enabled = true;
                btnInPhieu.Enabled = true;
                btnCapNhatTrangThaiThanhToan.Enabled = trangThaiThanhToan != "Đã thanh toán";
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadPhieuXuat();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            LoadPhieuXuat();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FLapPhieuXuat form = new FLapPhieuXuat();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPhieuXuat();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu > 0)
            {
                FLapPhieuXuat form = new FLapPhieuXuat(selectedMaPhieu);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadPhieuXuat();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu > 0 && MessageBox.Show("Bạn có chắc muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool result = phieuXuatDAO.XoaPhieuXuat(selectedMaPhieu);
                MessageBox.Show(result ? "Xóa thành công" : "Xóa thất bại");
                LoadPhieuXuat();
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu <= 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất để xem chi tiết!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dùng hàm riêng đã xử lý đầy đủ logic
            ShowPhieuXuatDetail(selectedMaPhieu);
        }

        private void ShowPhieuXuatDetail(int maPhieu)
        {
            try
            {
                // Lấy thông tin phiếu xuất
                DataTable dtPhieuXuat = GetPhieuXuatInfo(maPhieu);
                DataTable dtChiTiet = GetChiTietPhieuXuat(maPhieu);

                if (dtPhieuXuat.Rows.Count == 0 || dtChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin chi tiết phiếu xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuyển dữ liệu từ phiếu xuất sang đơn hàng để hiển thị trong FChiTietDonHang
                int maKhachHang = 0;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT MaKH FROM PhieuXuatHang WHERE MaPhieu = @MaPhieu";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        maKhachHang = Convert.ToInt32(result);
                    }
                }

                if (maKhachHang > 0)
                {
                    // Tạo đối tượng form chi tiết đơn hàng để xem chi tiết
                    // Sử dụng constructor có thêm tham số dummy để tránh lỗi ambiguous
                    FChiTietDonHang fChiTiet = new FChiTietDonHang(maPhieu, true, true);
                    fChiTiet.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin khách hàng của phiếu xuất!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị chi tiết phiếu xuất: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetPhieuXuatInfo(int maPhieu)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = @"SELECT px.MaPhieu, px.NgayXuatHang, kh.HoTen, kh.Sdt, kh.DiaChi,
                    px.TrangThai, px.ThanhTien, px.GhiChu
                    FROM PhieuXuatHang px
                    INNER JOIN KhachHang kh ON px.MaKH = kh.MaKH
                    WHERE px.MaPhieu = @MaPhieu";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }

        private DataTable GetChiTietPhieuXuat(int maPhieu)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = @"
            SELECT ct.MaChiTiet, sp.MaSP, sp.TenSP, ct.SoLuong, 
                   ct.GiaXuat, sp.DonViTinh, (ct.SoLuong * ct.GiaXuat) AS ThanhTien
            FROM ChiTietPhieuXuat ct
            INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
            WHERE ct.MaPhieu = @MaPhieu";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }

        private void btnCapNhatTrangThaiThanhToan_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu <= 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất để cập nhật trạng thái thanh toán!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThaiHienTai = phieuXuatDAO.LayTrangThaiThanhToan(selectedMaPhieu);

            using (Form formCapNhat = new Form())
            {
                formCapNhat.Text = "Cập nhật trạng thái thanh toán";
                formCapNhat.Size = new Size(400, 200);
                formCapNhat.StartPosition = FormStartPosition.CenterParent;
                formCapNhat.FormBorderStyle = FormBorderStyle.FixedDialog;
                formCapNhat.MaximizeBox = false;
                formCapNhat.MinimizeBox = false;

                Label lblTitle = new Label
                {
                    Text = "Chọn trạng thái thanh toán:",
                    AutoSize = true,
                    Location = new Point(20, 20)
                };

                ComboBox cboTrangThaiThanhToan = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(20, 50),
                    Size = new Size(350, 28)
                };
                cboTrangThaiThanhToan.Items.AddRange(new string[] { "Chưa thanh toán", "Đã thanh toán" });

                // Chọn giá trị hiện tại
                cboTrangThaiThanhToan.SelectedIndex = !string.IsNullOrEmpty(trangThaiHienTai) &&
                    cboTrangThaiThanhToan.Items.Contains(trangThaiHienTai)
                    ? cboTrangThaiThanhToan.Items.IndexOf(trangThaiHienTai)
                    : 0;

                Button btnLuu = new Button
                {
                    Text = "Lưu",
                    Size = new Size(100, 35),
                    Location = new Point(80, 100),
                    BackColor = Color.FromArgb(33, 150, 243),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                Button btnHuy = new Button
                {
                    Text = "Hủy",
                    Size = new Size(100, 35),
                    Location = new Point(200, 100),
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                btnLuu.Click += (s, ev) =>
                {
                    string trangThaiMoi = cboTrangThaiThanhToan.SelectedItem.ToString();
                    bool result = phieuXuatDAO.CapNhatTrangThaiThanhToan(selectedMaPhieu, trangThaiMoi);

                    if (result)
                    {
                        int rowIndex = -1;
                        foreach (DataGridViewRow row in dgvPhieuXuat.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["colMaPhieu"].Value) == selectedMaPhieu)
                            {
                                rowIndex = row.Index;
                                break;
                            }
                        }

                        if (rowIndex >= 0)
                        {
                            dgvPhieuXuat.Rows[rowIndex].Cells["colTrangThaiThanhToan"].Value = trangThaiMoi;
                            dgvPhieuXuat.Refresh();  // 👈 refresh giao diện
                        }

                        MessageBox.Show("Cập nhật trạng thái thanh toán thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formCapNhat.DialogResult = DialogResult.OK;
                        formCapNhat.Close();
                        LoadPhieuXuat();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật trạng thái thanh toán thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };


                btnHuy.Click += (s, ev) =>
                {
                    formCapNhat.DialogResult = DialogResult.Cancel;
                    formCapNhat.Close();
                };

                formCapNhat.Controls.Add(lblTitle);
                formCapNhat.Controls.Add(cboTrangThaiThanhToan);
                formCapNhat.Controls.Add(btnLuu);
                formCapNhat.Controls.Add(btnHuy);

                formCapNhat.ShowDialog();
            }
        }

        private void btnLapDonDatHang_Click(object sender, EventArgs e)
        {
            FDonDatHang frmDonDatHang = new FDonDatHang();
            frmDonDatHang.ShowDialog();
        }

        private void dgvPhieuXuat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
