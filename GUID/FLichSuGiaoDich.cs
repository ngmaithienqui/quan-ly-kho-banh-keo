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
    public partial class FLichSuGiaoDich : Form
    {
        // Connection string to connect to the database
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CuaHangBanhKeo;Integrated Security=True";
        private int maNCC;
        private string tenNCC;

        public FLichSuGiaoDich()
        {
            InitializeComponent();
        }

        public FLichSuGiaoDich(int maNCC, string tenNCC)
        {
            InitializeComponent();
            this.maNCC = maNCC;
            this.tenNCC = tenNCC;
        }

        private void FLichSuGiaoDich_Load(object sender, EventArgs e)
        {
            // Set the form title with supplier name
            this.Text = $"Lịch sử giao dịch - {tenNCC}";
            lblTitle.Text = $"LỊCH SỬ GIAO DỊCH - {tenNCC.ToUpper()}";

            // Load transaction history data
            LoadTransactionHistory();
            
            // Load phiếu nhập history
            LoadPhieuNhapHistory();
        }

        private void LoadTransactionHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string sql = "SELECT LichSuGiaoDich FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        txtLichSuGD.Text = result.ToString();
                    }
                    else
                    {
                        txtLichSuGD.Text = "Không có lịch sử giao dịch.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử giao dịch: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPhieuNhapHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string sql = @"SELECT p.MaPhieu, p.NgayNhap, p.ThanhTien, p.TrangThai 
                                  FROM PhieuNhapHang p
                                  WHERE p.MaNCC = @MaNCC
                                  ORDER BY p.NgayNhap DESC";
                    
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    dgvPhieuNhap.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử phiếu nhập: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtThemGhiChu.Text))
            {
                MessageBox.Show("Vui lòng nhập ghi chú để thêm vào lịch sử", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Get current transaction history
                    string getHistorySql = "SELECT LichSuGiaoDich FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    SqlCommand getCmd = new SqlCommand(getHistorySql, conn);
                    getCmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    
                    string currentHistory = (getCmd.ExecuteScalar() ?? "").ToString();
                    
                    // Add new entry with timestamp
                    string newEntry = $"[{DateTime.Now:dd/MM/yyyy HH:mm}] {txtThemGhiChu.Text}";
                    string updatedHistory = string.IsNullOrEmpty(currentHistory) 
                        ? newEntry 
                        : currentHistory + Environment.NewLine + newEntry;
                    
                    // Update the database
                    string updateSql = "UPDATE NhaCungCap SET LichSuGiaoDich = @LichSu WHERE MaNCC = @MaNCC";
                    SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                    updateCmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    updateCmd.Parameters.AddWithValue("@LichSu", updatedHistory);
                    
                    int result = updateCmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        txtThemGhiChu.Clear();
                        LoadTransactionHistory();
                        MessageBox.Show("Đã thêm ghi chú vào lịch sử giao dịch", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm ghi chú: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập để xem chi tiết", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // Get the selected invoice ID
            int maPhieu = Convert.ToInt32(dgvPhieuNhap.SelectedRows[0].Cells["MaPhieu"].Value);
            
            // Show invoice details form (you'll need to implement this)
            MessageBox.Show($"Hiển thị chi tiết phiếu nhập có mã: {maPhieu}", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Open detailed form
            // var frmChiTiet = new FrmChiTietPhieuNhap(maPhieu);
            // frmChiTiet.ShowDialog();
        }
    }
}
