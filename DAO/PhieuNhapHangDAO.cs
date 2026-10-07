using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLCuaHangBanhKeo.DAO
{
    public class PhieuNhapHangDAO
    {
        private static DBConnection db = new DBConnection();

        public static List<PhieuNhapHangDTO> GetPhieuNhapByMaNCC(int maNCC)
        {
            List<PhieuNhapHangDTO> list = new List<PhieuNhapHangDTO>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", maNCC)
            };

            string query = $"SELECT * FROM PhieuNhapHang WHERE MaNCC = {maNCC}";
            DataTable dt = db.LayDuLieu(query);


            foreach (DataRow row in dt.Rows)
            {
                PhieuNhapHangDTO pn = new PhieuNhapHangDTO
                {
                    MaPhieu = row["MaPhieu"] != DBNull.Value ? Convert.ToInt32(row["MaPhieu"]) : 0,
                    MaNCC = row["MaNCC"] != DBNull.Value ? Convert.ToInt32(row["MaNCC"]) : 0,
                    ThanhTien = row["ThanhTien"] != DBNull.Value ? Convert.ToDouble(row["ThanhTien"]) : 0.0,
                    NgayNhap = row["NgayNhap"] != DBNull.Value ? Convert.ToDateTime(row["NgayNhap"]) : DateTime.MinValue,
                    TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString() : "",
                    TrangThaiThanhToan = row["TrangThaiThanhToan"] != DBNull.Value ? row["TrangThaiThanhToan"].ToString() : ""
                };
                list.Add(pn);
            }

            return list;
        }

        public static float TinhCongNoChuaThanhToan(int maNCC)
        {
            string sql = @"
                SELECT ISNULL(SUM(ThanhTien), 0)
                FROM PhieuNhapHang
                WHERE MaNCC = @MaNCC AND TrangThaiThanhToan = N'Chưa thanh toán'";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", maNCC)
            };

            object result = db.ThucThiGiaTri(sql, parameters);
            return result != null ? Convert.ToSingle(result) : 0;
        }

        public static bool CapNhatTrangThaiThanhToan(int maNCC)
        {
            string sql = @"
                UPDATE PhieuNhapHang
                SET TrangThaiThanhToan = N'Đã thanh toán'
                WHERE MaNCC = @MaNCC AND TrangThaiThanhToan = N'Chưa thanh toán'";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", maNCC)
            };

            try
            {
                db.ThucThi(sql, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int TaoPhieuNhap(int maNCC, double thanhTien, DateTime ngayNhap, string trangThai)
        {
            string query = $@"
            INSERT INTO PhieuNhapHang(MaNCC, ThanhTien, NgayNhap, TrangThai)
            VALUES ({maNCC}, {thanhTien.ToString().Replace(',', '.')}, '{ngayNhap:yyyy-MM-dd}', N'{trangThai}');
            SELECT SCOPE_IDENTITY();
        ";

            object result = db.ThucThiGiaTri(query);
            return Convert.ToInt32(result);
        }

        // Thêm chi tiết phiếu nhập
        public void ThemChiTietPhieuNhap(int maPhieu, int maSP, double giaNhap, int soLuong, string donViTinh)
        {
            string query = $@"
            INSERT INTO ChiTietPhieuNhap(MaPhieuNhap, MaSP, GiaNhap, SoLuongSP, DonViTinh)
            VALUES ({maPhieu}, {maSP}, {giaNhap.ToString().Replace(',', '.')}, {soLuong}, N'{donViTinh}')
        ";

            db.ThucThi(query);
        }

        // Cập nhật sản phẩm (nếu muốn cập nhật khi nhập)
        public void CapNhatSanPham(int maSP, double giaNhap, double giaXuat, int soLuongThem, string donViTinh, int maNCC)
        {
            string query = $@"
            UPDATE SanPham
            SET GiaNhap = {giaNhap.ToString().Replace(',', '.')},
                GiaXuat = {giaXuat.ToString().Replace(',', '.')},
                SoLuong = SoLuong + {soLuongThem},
                MaNCC = {maNCC.ToString()}
                DonViTinh = N'{donViTinh}'
            WHERE MaSP = {maSP}
        ";

            db.ThucThi(query);
        }
        public DataTable LayDanhSachPhieuNhap()
        {
            string query = "SELECT * FROM PhieuNhapHang";
            return db.LayDuLieu(query);
        }

        public DataTable LayChiTietPhieuNhap(int maPN)
        {
            string query = $"SELECT * FROM ChiTietPhieuNhap WHERE MaPhieuNhap = {maPN}";
            return db.LayDuLieu(query);
        }

        public void CapNhatTrangThai(int maPN, string trangThai)
        {
            string query = $"UPDATE PhieuNhapHang SET TrangThai = N'{trangThai}' WHERE MaPhieu = {maPN}";
            db.ThucThi(query);
        }
        public void CapNhatTrangThai(int maPN, string trangThai, string trangThaiThanhToan)
        {
            string query = $"UPDATE PhieuNhapHang SET TrangThai = N'{trangThai}', TrangThaiThanhToan =N'{trangThaiThanhToan}' WHERE MaPhieu = {maPN}";
            db.ThucThi(query);
        }
        public static void CapNhatSoLuongSanPhamTheoPhieuNhap(int maPN)
        {
            List<SanPhamDTO> danhSachSanPham = new List<SanPhamDTO>();

            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    string queryChiTietPN = "SELECT MaSP, SoLuongSP FROM ChiTietPhieuNhap WHERE MaPhieuNhap = @MaPN";
                    using (SqlCommand cmd = new SqlCommand(queryChiTietPN, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int maSP = reader.GetInt32(0);
                                int soLuongNhap = reader.GetInt32(1);

                                SanPhamDTO sp = new SanPhamDTO(maSP)
                                {
                                    SoLuong = soLuongNhap
                                };
                                danhSachSanPham.Add(sp);
                            }
                        }
                    }

                    // Sau khi đọc xong danh sách sản phẩm => cập nhật số lượng tồn
                    foreach (SanPhamDTO sp in danhSachSanPham)
                    {
                        string queryUpdate = "UPDATE SanPham SET SoLuong = SoLuong + @SoLuongNhap WHERE MaSP = @MaSP";

                        using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@SoLuongNhap", sp.SoLuong);
                            cmdUpdate.Parameters.AddWithValue("@MaSP", sp.MaSP);

                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi cập nhật số lượng sản phẩm theo phiếu nhập: " + ex.Message);
                }
            }
        }

    }
}