using QLCuaHangBanhKeo.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo.DTO
{
    public class DanhMucDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;

        public List<DanhMucDTO> LayTatCaDanhMuc()
        {
            List<DanhMucDTO> danhMucs = new List<DanhMucDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM DanhMucSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DanhMucDTO dm = new DanhMucDTO()
                    {
                        MaDanhMuc = Convert.ToInt32(reader["MaDanhMuc"]),
                        TenDanhMuc = reader["TenDanhMuc"].ToString(),
                        MoTa = reader["MoTa"].ToString()
                    };
                    danhMucs.Add(dm);
                }
            }

            return danhMucs;
        }

        public bool ThemDanhMuc(DanhMucDTO dm)
        {
            // 1. Kiểm tra tên danh mục không được rỗng
            if (string.IsNullOrWhiteSpace(dm.TenDanhMuc))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 2. Kiểm tra trùng tên kể cả đã xóa
                string checkSql = "SELECT COUNT(*) FROM DanhMucSP WHERE LTRIM(RTRIM(TenDanhMuc)) = LTRIM(RTRIM(@Ten))";
                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@Ten", dm.TenDanhMuc);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tên danh mục đã tồn tại (bao gồm cả danh mục đã xóa).", "Cảnh báo");
                    return false;
                }

                // 3. Thêm mới nếu hợp lệ
                string sql = "INSERT INTO DanhMucSP (TenDanhMuc, MoTa) VALUES (@Ten, @MoTa)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ten", dm.TenDanhMuc);
                cmd.Parameters.AddWithValue("@MoTa", (object)dm.MoTa ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }



        public bool CapNhatDanhMuc(DanhMucDTO dm)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Lấy dữ liệu hiện tại từ DB
                string checkSql = "SELECT TenDanhMuc, MoTa FROM DanhMucSP WHERE MaDanhMuc = @Ma";
                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@Ma", dm.MaDanhMuc);
                using (SqlDataReader reader = checkCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string tenCu = reader["TenDanhMuc"].ToString();
                        string moTaCu = reader["MoTa"]?.ToString() ?? "";

                        // Nếu không có thay đổi gì
                        if (tenCu == dm.TenDanhMuc && moTaCu == (dm.MoTa ?? ""))
                        {
                            MessageBox.Show("Bạn chưa thay đổi gì để cập nhật!", "Thông báo");
                            return false;
                        }
                    }
                }

                // 2. Có thay đổi → cập nhật
                string sql = "UPDATE DanhMucSP SET TenDanhMuc = @Ten, MoTa = @MoTa WHERE MaDanhMuc = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ten", dm.TenDanhMuc);
                cmd.Parameters.AddWithValue("@MoTa", (object)dm.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ma", dm.MaDanhMuc);
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public bool XoaDanhMuc(int maDanhMuc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kiểm tra tồn tại sản phẩm trong danh mục
                string checkSql = "SELECT COUNT(*) FROM SanPham WHERE MaDanhMuc = @MaDM";
                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@MaDM", maDanhMuc);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Không thể xóa danh mục vì còn sản phẩm đang sử dụng.", "Cảnh báo");
                    return false;
                }

                // Nếu không còn sản phẩm, cho phép xóa
                string sql = "DELETE FROM DanhMucSP WHERE MaDanhMuc = @MaDM";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDM", maDanhMuc);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}
