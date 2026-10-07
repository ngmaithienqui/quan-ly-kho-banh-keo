using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo.DAO
{
    public class NVXemPhieuXuatDAO
    {
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;

        public List<NVXemPhieuXuatDTO> LayPhieuXuat(string trangThai, DateTime tuNgay, DateTime denNgay)
        {
            List<NVXemPhieuXuatDTO> list = new List<NVXemPhieuXuatDTO>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = @"SELECT px.MaPhieu, px.NgayXuatHang, kh.HoTen AS TenKH, 
                               px.TrangThai, px.ThanhTien, px.TrangThaiThanhToan
                               FROM PhieuXuatHang px
                               INNER JOIN KhachHang kh ON px.MaKH = kh.MaKH";


                if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
                    sql += " WHERE px.TrangThai = @TrangThai";
                else
                    sql += " WHERE 1=1";

                sql += " AND px.NgayXuatHang BETWEEN @TuNgay AND @DenNgay ORDER BY px.NgayXuatHang DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NVXemPhieuXuatDTO
                            {
                                MaPhieu = Convert.ToInt32(reader["MaPhieu"]),
                                NgayXuatHang = Convert.ToDateTime(reader["NgayXuatHang"]),
                                TenKH = reader["TenKH"].ToString(),
                                TrangThai = reader["TrangThai"] == DBNull.Value ? "Chưa xuất hàng" : reader["TrangThai"].ToString(), // ✅
                                ThanhTien = Convert.ToDecimal(reader["ThanhTien"]),
                                TrangThaiThanhToan = reader["TrangThaiThanhToan"] == DBNull.Value ? "Chưa thanh toán" : reader["TrangThaiThanhToan"].ToString()
                            });

                        }
                    }
                }
            }

            return list;
        }


        public string LayTrangThai(int maPhieu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT TrangThai FROM PhieuXuatHang WHERE MaPhieu = @MaPhieu";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }
        public bool CapNhatTrangThaiXuatHang(int maPhieuXuat)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Kiểm tra tồn kho trước khi trừ
                    string checkTonKhoQuery = @"
                SELECT sp.MaSP, sp.TenSP, sp.SoLuong AS TonKho, ct.SoLuong AS SoLuongXuat
                FROM SanPham sp
                INNER JOIN ChiTietPhieuXuat ct ON sp.MaSP = ct.MaSP
                WHERE ct.MaPhieu = @MaPhieu";

                    using (SqlCommand cmdCheck = new SqlCommand(checkTonKhoQuery, conn, transaction))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaPhieu", maPhieuXuat);
                        using (SqlDataReader reader = cmdCheck.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int tonKho = Convert.ToInt32(reader["TonKho"]);
                                int soLuongXuat = Convert.ToInt32(reader["SoLuongXuat"]);
                                string tenSP = reader["TenSP"].ToString();

                                if (soLuongXuat > tonKho)
                                {
                                    reader.Close();
                                    transaction.Rollback();
                                    MessageBox.Show($"Sản phẩm '{tenSP}' không đủ số lượng tồn kho!\nTồn kho: {tonKho}, Số lượng cần xuất: {soLuongXuat}",
                                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                            }
                        }
                    }

                    // 2. Trừ tồn kho
                    string updateTonKhoQuery = @"
                UPDATE sp
                SET sp.SoLuong = sp.SoLuong - ct.SoLuong
                FROM SanPham sp
                INNER JOIN ChiTietPhieuXuat ct ON sp.MaSP = ct.MaSP
                WHERE ct.MaPhieu = @MaPhieu";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateTonKhoQuery, conn, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@MaPhieu", maPhieuXuat);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // 3. Cập nhật trạng thái Phiếu Xuất
                    string updateTrangThaiQuery = @"
                UPDATE PhieuXuatHang
                SET TrangThai = N'Đã xuất'
                WHERE MaPhieu = @MaPhieu";

                    using (SqlCommand cmdTrangThai = new SqlCommand(updateTrangThaiQuery, conn, transaction))
                    {
                        cmdTrangThai.Parameters.AddWithValue("@MaPhieu", maPhieuXuat);
                        cmdTrangThai.ExecuteNonQuery();
                    }

                    // 4. Commit Transaction
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Có lỗi xảy ra khi xuất hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }









        public string LayTrangThaiThanhToan(int maPhieu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT TrangThaiThanhToan FROM PhieuXuatHang WHERE MaPhieu = @MaPhieu";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    object result = cmd.ExecuteScalar();
                    return result == DBNull.Value ? "Chưa thanh toán" : result?.ToString();
                }
            }
        }





    }



}
