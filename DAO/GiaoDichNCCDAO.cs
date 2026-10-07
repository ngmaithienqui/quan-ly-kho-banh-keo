using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QLCuaHangBanhKeo.DTO;

namespace QLCuaHangBanhKeo.DAO
{

    public class GiaoDichNCCDAO
    {
        private static DBConnection db = new DBConnection();
        public static List<GiaoDichNCCDTO> LayDanhSachGiaoDich(int maNCC)

        {
            
            List<GiaoDichNCCDTO> ds = new List<GiaoDichNCCDTO>();
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT * FROM GiaoDichNCC WHERE MaNCC = @MaNCC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GiaoDichNCCDTO gd = new GiaoDichNCCDTO
                    {
                        MaGD = Convert.ToInt32(reader["MaGD"]),
                        MaNCC = Convert.ToInt32(reader["MaNCC"]),
                        MaPhieu = reader["MaPhieu"] != DBNull.Value ? Convert.ToInt32(reader["MaPhieu"]) : (int?)null,
                        LoaiGiaoDich = reader["LoaiGiaoDich"].ToString(),
                        NgayGiaoDich = Convert.ToDateTime(reader["NgayGiaoDich"]),
                        SoTien = Convert.ToSingle(reader["SoTien"]),
                        GhiChu = reader["GhiChu"].ToString()
                    };
                    ds.Add(gd);
                }
            }
            return ds;
        }

        public static bool ThemGiaoDich(GiaoDichNCCDTO gd)
        {

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO GiaoDichNCC (MaNCC, NgayGiaoDich, SoTien, LoaiGiaoDich, MaPhieu, GhiChu) " +
                               "VALUES (@MaNCC, @NgayGiaoDich, @SoTien, @LoaiGiaoDich, @MaPhieu, @GhiChu)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNCC", gd.MaNCC);
                    cmd.Parameters.AddWithValue("@NgayGiaoDich", gd.NgayGiaoDich);
                    cmd.Parameters.AddWithValue("@SoTien", gd.SoTien);
                    cmd.Parameters.AddWithValue("@LoaiGiaoDich", gd.LoaiGiaoDich ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", gd.GhiChu ?? (object)DBNull.Value);

                    if (gd.MaPhieu.HasValue)
                        cmd.Parameters.AddWithValue("@MaPhieu", gd.MaPhieu.Value);
                    else
                        cmd.Parameters.AddWithValue("@MaPhieu", DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
