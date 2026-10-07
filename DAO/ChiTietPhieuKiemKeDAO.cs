using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLCuaHangBanhKeo.DAO
{
    public class ChiTietPhieuKiemKeDAO
    {
        private static DBConnection db = new DBConnection();

        public static void ThemChiTiet(int maPhieu, int maSP, int soLuongThucTe, string lyDo)
        {
            // Lấy thông tin sản phẩm
            string querySP = "SELECT TenSP, SoLuong FROM SanPham WHERE MaSP = @MaSP";
            SqlParameter[] parametersSP = new SqlParameter[] {
        new SqlParameter("@MaSP", maSP)
    };
            DataTable dt = db.LayDuLieu(querySP, parametersSP);

            if (dt.Rows.Count == 0) return;

            string tenSP = dt.Rows[0]["TenSP"].ToString();
            int soLuongHeThong = Convert.ToInt32(dt.Rows[0]["SoLuong"]);
            int soLuongSaiLech = soLuongThucTe - soLuongHeThong;

            // Thêm chi tiết kiểm kê
            string queryInsert = @"
        INSERT INTO ChiTietKiemKe (MaPhieu, MaSP, TenSP, SoLuongSPThuc, SoLuongSaiLech, LyDo)
        VALUES (@MaPhieu, @MaSP, @TenSP, @SoLuongSPThuc, @SoLuongSaiLech, @LyDo)";

            SqlParameter[] parametersInsert = new SqlParameter[] {
        new SqlParameter("@MaPhieu", maPhieu),
        new SqlParameter("@MaSP", maSP),
        new SqlParameter("@TenSP", tenSP),
        new SqlParameter("@SoLuongSPThuc", soLuongThucTe),
        new SqlParameter("@SoLuongSaiLech", soLuongSaiLech),
        new SqlParameter("@LyDo", lyDo ?? "")
    };

            db.ThucThi(queryInsert, parametersInsert);
        }


        public static List<ChiTietPhieuKiemKeDTO> LayChiTietTheoPhieu(int maPhieu)
        {
            List<ChiTietPhieuKiemKeDTO> list = new List<ChiTietPhieuKiemKeDTO>();

            // Do LayDuLieu trong DBconnection không hỗ trợ SqlParameter, sửa lại câu truy vấn
            string query = @"
        SELECT ct.MaChiTiet, ct.MaPhieu, ct.MaSP, sp.TenSP, 
               ct.SoLuongSPThuc, sp.SoLuong AS SoLuongHeThong, ct.LyDo
        FROM ChiTietKiemKe ct
        JOIN SanPham sp ON ct.MaSP = sp.MaSP
        WHERE ct.MaPhieu = " + maPhieu;

            DataTable dt = db.LayDuLieu(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ChiTietPhieuKiemKeDTO
                {
                    MaChiTiet = Convert.ToInt32(row["MaChiTiet"]),
                    MaPhieu = Convert.ToInt32(row["MaPhieu"]),
                    MaSP = Convert.ToInt32(row["MaSP"]),
                    TenSP = row["TenSP"].ToString(),
                    SoLuongSPThuc = row["SoLuongSPThuc"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoLuongSPThuc"]),
                    SoLuongHeThong = Convert.ToInt32(row["SoLuongHeThong"]),
                    LyDo = row["LyDo"].ToString()
                });
            }

            return list;
        }


    }
}