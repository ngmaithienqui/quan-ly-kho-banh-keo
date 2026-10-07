using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using QLCuaHangBanhKeo.DTO;

namespace QLCuaHangBanhKeo.DAO
{
    public class GiaoDichKHDAO
    {
        private static DBConnection db = new DBConnection();

        public static List<GiaoDichKHDTO> LayDanhSachGiaoDich(int maKH)
        {
            List<GiaoDichKHDTO> ds = new List<GiaoDichKHDTO>();
            string query = "SELECT * FROM GiaoDichKH WHERE MaKH=" + maKH;

            DataTable dt = db.LayDuLieu(query);

            foreach (DataRow row in dt.Rows)
            {
                GiaoDichKHDTO gd = new GiaoDichKHDTO
                {
                    MaGD = Convert.ToInt32(row["MaGD"]),
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    MaPhieu = row["MaPhieu"] != DBNull.Value ? Convert.ToInt32(row["MaPhieu"]) : (int?)null,
                    LoaiGiaoDich = row["LoaiGiaoDich"].ToString(),
                    NgayGiaoDich = Convert.ToDateTime(row["NgayGiaoDich"]),
                    SoTien = Convert.ToSingle(row["SoTien"]),
                    GhiChu = row["GhiChu"].ToString()
                };
                ds.Add(gd);
            }

            return ds;
        }

        public static bool ThemGiaoDich(GiaoDichKHDTO gd)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKH", gd.MaKH),
                    new SqlParameter("@NgayGiaoDich", gd.NgayGiaoDich),
                    new SqlParameter("@SoTien", gd.SoTien),
                    new SqlParameter("@LoaiGiaoDich", (object)gd.LoaiGiaoDich ?? DBNull.Value),
                    new SqlParameter("@GhiChu", (object)gd.GhiChu ?? DBNull.Value),
                    new SqlParameter("@MaPhieu", gd.MaPhieu.HasValue ? (object)gd.MaPhieu.Value : DBNull.Value)
                };

                string query = "INSERT INTO GiaoDichKH (MaKH, NgayGiaoDich, SoTien, LoaiGiaoDich, MaPhieu, GhiChu) " +
                               "VALUES (@MaKH, @NgayGiaoDich, @SoTien, @LoaiGiaoDich, @MaPhieu, @GhiChu)";

                db.ThucThi(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}