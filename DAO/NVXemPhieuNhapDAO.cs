using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLCuaHangBanhKeo.DAO
{
    public class NVXemPhieuNhapDAO
    {
        private static DBConnection db = new DBConnection();

        public static List<PhieuNhapHangDTO> GetPhieuNhapByMaNCC(int maNCC)
        {
            List<PhieuNhapHangDTO> list = new List<PhieuNhapHangDTO>();

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

        public static bool CapNhatTrangThaiNhapHang(int maPhieu)
        {
            string sql = @"
                BEGIN
                    -- Cập nhật trạng thái phiếu nhập
                    UPDATE PhieuNhapHang
                    SET TrangThai = N'Đã nhập hàng'
                    WHERE MaPhieu = @MaPhieu;

                    -- Cộng dồn số lượng sản phẩm trong chi tiết phiếu vào tồn kho
                    UPDATE SanPham
                    SET SoLuong = SoLuong + ctpn.SoLuongSP
                    FROM SanPham sp
                    INNER JOIN ChiTietPhieuNhap ctpn ON sp.MaSP = ctpn.MaSP
                    WHERE ctpn.MaPhieuNhap = @MaPhieu;
                END
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaPhieu", maPhieu)
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



        public void ThemChiTietPhieuNhap(int maPhieu, int maSP, double giaNhap, int soLuong, string donViTinh)
        {
            string query = $@"
            INSERT INTO ChiTietPhieuNhap(MaPhieuNhap, MaSP, GiaNhap, SoLuongSP, DonViTinh)
            VALUES ({maPhieu}, {maSP}, {giaNhap.ToString().Replace(',', '.')}, {soLuong}, N'{donViTinh}')";

            db.ThucThi(query);
        }

        public void CapNhatSanPham(int maSP, double giaNhap, double giaXuat, int soLuongThem, string donViTinh, int maNCC)
        {
            string query = $@"
            UPDATE SanPham
            SET GiaNhap = {giaNhap.ToString().Replace(',', '.')},
                GiaXuat = {giaXuat.ToString().Replace(',', '.')},
                SoLuong = SoLuong + {soLuongThem},
                MaNCC = {maNCC},
                DonViTinh = N'{donViTinh}'
            WHERE MaSP = {maSP}";

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
    }
}