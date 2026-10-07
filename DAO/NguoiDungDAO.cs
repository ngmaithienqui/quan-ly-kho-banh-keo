using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DAO
{
    public class NguoiDungDAO
    {
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;

        public bool KiemTraTonTai(string tenDangNhap, string email)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap OR Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@Email", email);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool KiemTraSoDienThoaiTonTai(string sdt)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM NguoiDung WHERE Sdt = @Sdt";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Sdt", sdt);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool DangKyNguoiDung(NguoiDungDTO nguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, Sdt, VaiTro) 
                                 VALUES (@TenDangNhap, @MatKhau, @HoTen, @Email, @Sdt, @VaiTro)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
                cmd.Parameters.AddWithValue("@HoTen", nguoiDung.HoTen);
                cmd.Parameters.AddWithValue("@Email", nguoiDung.Email);
                cmd.Parameters.AddWithValue("@Sdt", nguoiDung.Sdt);
                cmd.Parameters.AddWithValue("@VaiTro", nguoiDung.VaiTro);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public NguoiDungDTO DangNhap(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"SELECT TenDangNhap, MatKhau, HoTen, Email, Sdt, VaiTro 
                                 FROM NguoiDung 
                                 WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", username);
                    cmd.Parameters.AddWithValue("@MatKhau", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NguoiDungDTO
                            {
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                Email = reader["Email"].ToString(),
                                Sdt = reader["Sdt"].ToString(),
                                VaiTro = reader["VaiTro"].ToString()
                            };
                        }
                    }
                }
            }
            return null; // Đăng nhập thất bại
        }
        public bool KiemTraTrungSoDienThoai(string SDT)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM NguoiDung WHERE Sdt = @Sdt";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Sdt", SDT);
                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Nếu count > 0 => đã tồn tại số điện thoại
            }
        }

    }
}
