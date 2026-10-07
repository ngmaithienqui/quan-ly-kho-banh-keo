using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLCuaHangBanhKeo.DAO
{
    public class PhieuKiemKeDAO
    {
        private static DBConnection db = new DBConnection();

        public static int TaoPhieuKiemKe(DateTime ngayLap)
        {
            string query = "INSERT INTO PhieuKiemKe (NgayLap, TrangThai) OUTPUT INSERTED.MaPhieu VALUES (@NgayLap, @TrangThai)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@NgayLap", ngayLap),
                new SqlParameter("@TrangThai", "Chờ duyệt")
            };

            object result = db.ThucThiGiaTri(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static int ThemPhieuKiemKe(PhieuKiemKeDTO phieu)
        {
            string query = "INSERT INTO PhieuKiemKe (NguoiLapPhieu, NgayLap, TrangThai) " +
                          "OUTPUT INSERTED.MaPhieu " +
                          "VALUES (@NguoiLapPhieu, @NgayLap, @TrangThai)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@NguoiLapPhieu", phieu.NguoiLapPhieu),
                new SqlParameter("@NgayLap", phieu.NgayLap),
                new SqlParameter("@TrangThai", phieu.TrangThai)
            };

            object result = db.ThucThiGiaTri(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static List<PhieuKiemKeDTO> LayDanhSachPhieuChoDuyet()
        {
            List<PhieuKiemKeDTO> list = new List<PhieuKiemKeDTO>();
            string query = "SELECT * FROM PhieuKiemKe WHERE TrangThai = N'Chờ duyệt'";

            DataTable dt = db.LayDuLieu(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PhieuKiemKeDTO
                {
                    MaPhieu = Convert.ToInt32(row["MaPhieu"]),
                    NgayLap = Convert.ToDateTime(row["NgayLap"]),
                    TrangThai = row["TrangThai"].ToString()
                });
            }

            return list;
        }

        public static bool DuyetPhieuKiemKe(int maPhieu, SqlConnection conn, SqlTransaction trans)
        {
            // Note: The transaction parameter cannot be directly translated to this pattern
            // We have to modify this method to use the standard DBconnection approach
            string query = "UPDATE PhieuKiemKe SET TrangThai = N'Đã duyệt' WHERE MaPhieu = @MaPhieu";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhieu", maPhieu)
            };

            try
            {
                db.ThucThi(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<PhieuKiemKeDTO> GetLichSuKiemKe()
        {
            List<PhieuKiemKeDTO> list = new List<PhieuKiemKeDTO>();
            string query = "SELECT * FROM PhieuKiemKe ORDER BY NgayLap DESC";

            DataTable dt = db.LayDuLieu(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PhieuKiemKeDTO
                {
                    MaPhieu = Convert.ToInt32(row["MaPhieu"]),
                    NgayLap = Convert.ToDateTime(row["NgayLap"]),
                    TrangThai = row["TrangThai"].ToString(),
                    NguoiLapPhieu = row["NguoiLapPhieu"] != DBNull.Value ? row["NguoiLapPhieu"].ToString() : null
                });
            }

            return list;
        }
    }
}