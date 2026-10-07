using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DAO
{

    internal class BangDuTruDAO
    {
        DBConnection dbconn = new DBConnection();
        public int TaoBangDuTruMoi(BangDuTru bangDuTru)
        {
            string query = @"
                INSERT INTO BangDuTru (NgayLap)
                VALUES (@NgayLap);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@NgayLap", bangDuTru.NgayLap)
            };

            return Convert.ToInt32(dbconn.ThucThiGiaTri(query, parameters));
        }

        public void ThemChiTietBangDuTru(int maBDT, ChiTietBangDuTru ct)
        {
            string query = @"
                INSERT INTO ChiTietBangDuTru (MaSP, MaBDT, SoLuongNhap, SoLuongSP)
                VALUES (@MaSP, @MaBDT, @SoLuongNhap, @SoLuongSP)";

            SqlParameter[] parameters = {
                new SqlParameter("@MaSP", ct.MaSP),
                new SqlParameter("@MaBDT", maBDT),
                new SqlParameter("@SoLuongNhap", ct.SoLuongNhap),
                new SqlParameter("@SoLuongSP", ct.SoLuongSp)
            };

            dbconn.ThucThi(query, parameters);
        }
        public DataTable LayDanhSachBangDuTru()
        {
            string query = "SELECT MaBDT, NgayLap FROM BangDuTru ORDER BY NgayLap DESC";
            return dbconn.LayDuLieu(query);
        }
        public DataTable LayChiTietBangDuTru(int maBDT)
        {
            string query = @"
        SELECT c.MaSP, s.TenSP, c.SoLuongNhap, c.SoLuongSP
        FROM ChiTietBangDuTru c
        INNER JOIN SanPham s ON c.MaSP = s.MaSP
        WHERE c.MaBDT = " + maBDT;

            return dbconn.LayDuLieu(query);
        }


        public DataTable LayChiTietBangDuTruVoiSoLuong(int maBDT)
        {
            string query = $@"
    SELECT 
    ct.MaSP,
    sp.TenSP,
    ct.SoLuongNhap,
    sp.DonViTinh,
    sp.GiaNhap,
    sp.GiaXuat,
    sp.MaNCC,
    ncc.HoTen AS TenNCC,
    CAST(sp.MaNCC AS varchar) + ' - ' + ncc.HoTen AS MaVaTenNCC
FROM 
    ChiTietBangDuTru ct
JOIN 
    SanPham sp ON ct.MaSP = sp.MaSP
JOIN 
    NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC
WHERE 
    ct.MaBDT = {maBDT}
";

            return dbconn.LayDuLieu(query);
        }


    }
}
