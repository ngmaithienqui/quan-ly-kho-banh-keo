using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLCuaHangBanhKeo.DAO
{
    public class PhieuXuatHangDAO
    {
        private static DBConnection db = new DBConnection();

        public static List<PhieuXuatHangDTO> GetPhieuXuatByMaKH(int maKH)
        {
            List<PhieuXuatHangDTO> list = new List<PhieuXuatHangDTO>();
            string query = "SELECT * FROM PhieuXuatHang WHERE MaKH = " + maKH;

            DataTable dt = db.LayDuLieu(query);

            foreach (DataRow row in dt.Rows)
            {
                PhieuXuatHangDTO px = new PhieuXuatHangDTO
                {
                    MaPhieu = row["MaPhieu"] != DBNull.Value ? Convert.ToInt32(row["MaPhieu"]) : 0,
                    MaKH = row["MaKH"] != DBNull.Value ? Convert.ToInt32(row["MaKH"]) : 0,
                    ThanhTien = row["ThanhTien"] != DBNull.Value ? Convert.ToDouble(row["ThanhTien"]) : 0.0,
                    NgayXuatHang = row["NgayXuatHang"] != DBNull.Value ? Convert.ToDateTime(row["NgayXuatHang"]) : DateTime.MinValue,
                    TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString() : "",
                    TrangThaiThanhToan = row["TrangThaiThanhToan"] != DBNull.Value ? row["TrangThaiThanhToan"].ToString() : ""
                };
                list.Add(px);
            }

            return list;
        }

        public static float TinhCongNoChuaThanhToan(int maKH)
        {
            string sql = @"
            SELECT ISNULL(SUM(ThanhTien), 0)
            FROM PhieuXuatHang
            WHERE MaKH = @MaKH AND TrangThaiThanhToan = N'Chưa thanh toán'";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };

            object result = db.ThucThiGiaTri(sql, parameters);
            return result != null ? Convert.ToSingle(result) : 0;
        }

        public static bool CapNhatTrangThaiThanhToan(int maKH)
        {
            string sql = @"
            UPDATE PhieuXuatHang
            SET TrangThaiThanhToan = N'Đã thanh toán'
            WHERE MaKH = @MaKH AND TrangThaiThanhToan = N'Chưa thanh toán'";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
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
    }
}