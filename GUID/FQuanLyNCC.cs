using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FQuanLyNCC : Form
    {
        // Connection string to connect to the database
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CuaHangBanhKeo;Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public FQuanLyNCC()
        {
            InitializeComponent();
        }

        private void FQuanLyNCC_Load(object sender, EventArgs e)
        {
            // Load data from database when form loads
            LoadNhaCungCapData();
            // Clear input fields
            ClearFields();
        }

        private void LoadNhaCungCapData()
        {
            try
            {
                // Create a new SQL connection
                connection = new SqlConnection(connectionString);
                connection.Open();

                // Create a SQL command to select all suppliers
                string sql = "SELECT * FROM NhaCungCap";
                command = new SqlCommand(sql, connection);

                // Create adapter and fill data table
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind data to DataGridView
                dgvNhaCungCap.DataSource = dataTable;

                // Close connection
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtMaNCC.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtCongNo.Text = "0";
            
            // Focus on the first field
            txtHoTen.Focus();
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get data from the selected row
                DataGridViewRow row = dgvNhaCungCap.Rows[e.RowIndex];
                
                txtMaNCC.Text = row.Cells["colMaNCC"].Value.ToString();
                txtHoTen.Text = row.Cells["colHoTen"].Value.ToString();
                txtSDT.Text = row.Cells["colSdt"].Value.ToString();
                txtDiaChi.Text = row.Cells["colDiaChi"].Value.ToString();
                txtCongNo.Text = row.Cells["colCongNo"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                
                string sql = "INSERT INTO NhaCungCap (HoTen, Sdt, DiaChi, CongNo, LichSuGiaoDich) " +
                             "VALUES (@HoTen, @Sdt, @DiaChi, @CongNo, @LichSuGiaoDich)";
                             
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                command.Parameters.AddWithValue("@Sdt", txtSDT.Text.Trim());
                command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                
                float congNo = 0;
                float.TryParse(txtCongNo.Text, out congNo);
                command.Parameters.AddWithValue("@CongNo", congNo);
                command.Parameters.AddWithValue("@LichSuGiaoDich", ""); // Empty transaction history for new supplier
                
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhaCungCapData();
                    ClearFields();
                }
                
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtHoTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                
                // Get current transaction history
                string getLichSuSql = "SELECT LichSuGiaoDich FROM NhaCungCap WHERE MaNCC=@MaNCC";
                command = new SqlCommand(getLichSuSql, connection);
                command.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                
                string lichSuGiaoDich = command.ExecuteScalar()?.ToString() ?? "";
                
                string sql = "UPDATE NhaCungCap SET HoTen=@HoTen, Sdt=@Sdt, DiaChi=@DiaChi, " +
                             "CongNo=@CongNo WHERE MaNCC=@MaNCC";
                             
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                command.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                command.Parameters.AddWithValue("@Sdt", txtSDT.Text.Trim());
                command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                
                float congNo = 0;
                float.TryParse(txtCongNo.Text, out congNo);
                command.Parameters.AddWithValue("@CongNo", congNo);
                
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhaCungCapData();
                    ClearFields();
                }
                
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
            if (result == DialogResult.Yes)
            {
                try
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                    
                    // Check if supplier is referenced in other tables
                    string checkSql = "SELECT COUNT(*) FROM SanPham WHERE MaNCC=@MaNCC";
                    command = new SqlCommand(checkSql, connection);
                    command.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                    
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Không thể xóa nhà cung cấp này vì đã có sản phẩm liên quan.", 
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // Proceed with deletion
                    string sql = "DELETE FROM NhaCungCap WHERE MaNCC=@MaNCC";
                    command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                    
                    int deleteResult = command.ExecuteNonQuery();
                    if (deleteResult > 0)
                    {
                        MessageBox.Show("Xóa nhà cung cấp thành công", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhaCungCapData();
                        ClearFields();
                    }
                    
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nhà cung cấp: " + ex.Message, 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnLichSuGD_Click(object sender, EventArgs e)
        {
            // Check if a supplier is selected
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xem lịch sử giao dịch", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Open transaction history form with supplier information
            int maNCC = int.Parse(txtMaNCC.Text);
            string tenNCC = txtHoTen.Text;
            
            FLichSuGiaoDich frmLichSu = new FLichSuGiaoDich(maNCC, tenNCC);
            frmLichSu.ShowDialog();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn nhà cung cấp chưa
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để thanh toán công nợ", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem nhà cung cấp có công nợ không
            float congNo = 0;
            if (!float.TryParse(txtCongNo.Text, out congNo) || congNo <= 0)
            {
                MessageBox.Show("Nhà cung cấp này không có công nợ cần thanh toán", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mở form thanh toán công nợ
            int maNCC = int.Parse(txtMaNCC.Text);
            string tenNCC = txtHoTen.Text;
            
            FThanhToan frmThanhToan = new FThanhToan(maNCC, tenNCC, congNo);
            if (frmThanhToan.ShowDialog() == DialogResult.OK)
            {
                // Nếu thanh toán thành công, cập nhật lại dữ liệu
                LoadNhaCungCapData();
            }
        }
    }
}
