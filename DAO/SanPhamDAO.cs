using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QLCuaHangBanhKeo.DAO
{



    public class SanPhamDAO
    {
        private static DBConnection db = new DBConnection();

        public static SanPhamDTO GetSanPhamByMa(int maSP)
        {
            string query = "SELECT * FROM SanPham WHERE MaSP = " + maSP;
            DataTable dt = db.LayDuLieu(query);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new SanPhamDTO(
                    Convert.ToInt32(row["MaSP"]),
                    Convert.ToInt32(row["MaNCC"]),
                    Convert.ToInt32(row["MaDanhMuc"]),
                    row["TenSP"].ToString(),
                    Convert.ToInt32(row["SoLuong"]),
                    Convert.ToSingle(row["GiaNhap"]),
                    Convert.ToSingle(row["GiaXuat"]),
                    row["DonViTinh"].ToString(),
                    row["MoTa"].ToString(),
                    Convert.ToDateTime(row["NgayNhap"]),
                    Convert.ToDateTime(row["HanSuDung"]),
                    row["TinhTrang"].ToString()
                );
            }
            return null;
        }

        public static SanPhamDTO GetSanPhamLapPhieuKiemKe(int maSP)
        {
            string query = "SELECT MaSP, TenSP, SoLuong FROM SanPham WHERE MaSP = " + maSP;
            DataTable dt = db.LayDuLieu(query);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                SanPhamDTO sp = new SanPhamDTO(Convert.ToInt32(row["MaSP"]));
                sp.TenSP = row["TenSP"].ToString();
                sp.SoLuong = Convert.ToInt32(row["SoLuong"]);
                return sp;
            }
            return null;
        }

        public static List<SanPhamDTO> SearchSanPhamByPartialName(string partialName)
        {
            List<SanPhamDTO> results = new List<SanPhamDTO>();
            string query = "SELECT MaSP, TenSP, SoLuong FROM SanPham WHERE TenSP LIKE N'%" + partialName + "%'";
            DataTable dt = db.LayDuLieu(query);

            foreach (DataRow row in dt.Rows)
            {
                SanPhamDTO sp = new SanPhamDTO(Convert.ToInt32(row["MaSP"]));
                sp.TenSP = row["TenSP"].ToString();
                sp.SoLuong = Convert.ToInt32(row["SoLuong"]);
                results.Add(sp);
            }
            return results;
        }

        public static List<SanPhamDTO> GetAllSanPham()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            string query = "SELECT * FROM SanPham";
            DataTable dt = db.LayDuLieu(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SanPhamDTO(
                    Convert.ToInt32(row["MaSP"]),
                    Convert.ToInt32(row["MaNCC"]),
                    Convert.ToInt32(row["MaDanhMuc"]),
                    row["TenSP"].ToString(),
                    Convert.ToInt32(row["SoLuong"]),
                    Convert.ToSingle(row["GiaNhap"]),
                    Convert.ToSingle(row["GiaXuat"]),
                    row["DonViTinh"].ToString(),
                    row["MoTa"].ToString(),
                    Convert.ToDateTime(row["NgayNhap"]),
                    Convert.ToDateTime(row["HanSuDung"]),
                    row["TinhTrang"].ToString()
                ));
            }
            return list;
        }

        public static bool CapNhatSoLuongThucTe(int maSP, int soLuongMoi, SqlConnection conn, SqlTransaction trans)
        {
            // Phương thức ban đầu có transaction, nhưng DBConnection mới không hỗ trợ trực tiếp
            string query = "UPDATE SanPham SET SoLuong = @SoLuongMoi WHERE MaSP = @MaSP";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SoLuongMoi", soLuongMoi),
                new SqlParameter("@MaSP", maSP)
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

        public void ThemSanPham(int MaNCC, int MaDanhMuc, string TenSanPham, int SoLuong, double GiaNhap, double GiaXuat, string DonViTinh, string MoTa, DateTime HanSuDung, DateTime NgayXuat, string TinhTrang)
        {
            string ngayNhapStr = HanSuDung.ToString("yyyy-MM-dd");
            string ngayXuatStr = NgayXuat.ToString("yyyy-MM-dd");
            string query = string.Format(
                "INSERT INTO SanPham(MaNCC, MaDanhMuc, TenSP, SoLuong, GiaNhap, GiaXuat , DonViTinh, MoTa, NgayNhap, HanSuDung, TinhTrang) " +
                "VALUES ({0}, {1}, N'{2}', {3}, {4}, {5}, N'{6}', N'{7}', '{8}', '{9}', N'{10}')",
                MaNCC, MaDanhMuc, TenSanPham.Replace("'", "''"), SoLuong, GiaNhap, GiaXuat, DonViTinh, MoTa, ngayXuatStr, ngayNhapStr, TinhTrang
            );

            db.ThucThi(query);
        }

        public DataTable LayDanhSachSanPham()
        {
            string query = "SELECT * FROM SanPham";
            return db.LayDuLieu(query);
        }
        public DataTable HienCot()
        {
            string query = "SELECT sp.MaSP, sp.TenSP,sp.SoLuong, sp.DonViTinh,  sp.NgayNhap, sp.MaNCC, ncc.HoTen as NhaCungCap, dm.TenDanhMuc FROM SanPham sp JOIN NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC JOIN DanhMucSP dm ON sp.MaDanhMuc = dm.MaDanhMuc";
            return db.LayDuLieu(query);
        }
        public DataTable LayDanhSachSanPhamVaNCCVaDanhMuc()
        {
            string query = "SELECT sp.MaSP, sp.TenSP,sp.SoLuong, sp.GiaNhap, sp.GiaXuat, sp.DonViTinh, sp.MoTa, sp.NgayNhap,sp.HanSuDung, sp.TinhTrang, sp.MaNCC, ncc.HoTen as NhaCungCap, sp.MaDanhMuc, dm.TenDanhMuc FROM SanPham sp JOIN NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC JOIN DanhMucSP dm ON sp.MaDanhMuc = dm.MaDanhMuc";
            return db.LayDuLieu(query);
        }
        public DataTable LayDanhSachNhaCungCap()
        {
            string query = "SELECT MaNCC, HoTen FROM NhaCungCap";
            return db.LayDuLieu(query);
        }
        public DataTable LayDanhSachDanhMuc()
        {
            string query = "SELECT MaDanhMuc, TenDanhMuc FROM DanhMucSP";
            return db.LayDuLieu(query);
        }

        //

        public void CapNhatSanPham(SanPhamDTO sp)
        {
            string query = string.Format
            (
            "UPDATE SanPham SET MaNCC = {0}, MaDanhMuc = {1}, TenSP = N'{2}', SoLuong = {3}, GiaNhap = {4}, GiaXuat = {5}, DonViTinh = N'{6}', MoTa = N'{7}', NgayNhap = '{8}', HanSuDung = '{9}', TinhTrang = N'{10}' WHERE MaSP = {11}",
            sp.MaNCC, sp.MaDanhMuc, sp.TenSP.Replace("'", "''"), sp.SoLuong, sp.GiaNhap, sp.GiaXuat, sp.DonViTinh.Replace("'", "''"), sp.MoTa.Replace("'", "''"), sp.NgayNhap.ToString("yyyy-MM-dd"), sp.HanSuDung.ToString("yyyy-MM-dd"), sp.TinhTrang.Replace("'", "''"), sp.MaSP
            );

            db.ThucThi(query);
        }
        //public void XoaSanPham(SanPhamDTO sp)
        //{
          //  string query = string.Format("DELETE from SanPham Where MaSP = {0}", sp.MaSP);
            //db.ThucThi(query);

        //}
        public DataTable TimKiemSanPhamTheoTen(string ten)
        {
            string sql = $@"
            SELECT sp.MaSP, sp.TenSP, sp.SoLuong, sp.GiaNhap, sp.GiaXuat, sp.DonViTinh, sp.MoTa, sp.NgayNhap, sp.HanSuDung, sp.TinhTrang, sp.MaNCC,
                ncc.HoTen, sp.MaDanhMuc, dm.TenDanhMuc
            FROM SanPham sp
            JOIN NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC
            JOIN DanhMucSP dm ON sp.MaDanhMuc = dm.MaDanhMuc
            WHERE sp.TenSP LIKE N'%{ten}%'";

            return db.LayDuLieu(sql);

        }
        public DataTable LaySanPhamTonKhoLonHon(int soLuongToiThieu)
        {
            string sql = $@"
            SELECT sp.MaSP, sp.TenSP, sp.SoLuong, ({soLuongToiThieu} - sp.SoLuong) AS SoLuongThieu, sp.DonViTinh, sp.NgayNhap,  sp.MaNCC,
                ncc.HoTen as NhaCungCap, sp.MaDanhMuc, dm.TenDanhMuc
            FROM SanPham sp
            JOIN NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC
            JOIN DanhMucSP dm ON sp.MaDanhMuc = dm.MaDanhMuc
            WHERE SoLuong < {soLuongToiThieu}";

            return db.LayDuLieu(sql);
        }
        public DataTable LaySanPhamTheoMa(int maNCC)
        {
            string query = @"SELECT sp.MaSP, sp.TenSP, sp.SoLuong, sp.GiaNhap, sp.GiaXuat, sp.DonViTinh, 
                            sp.MoTa, sp.NgayNhap, sp.HanSuDung, sp.TinhTrang, sp.MaNCC, 
                            ncc.HoTen AS NhaCungCap, sp.MaDanhMuc, dm.TenDanhMuc 
                     FROM SanPham sp 
                     JOIN NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC 
                     JOIN DanhMucSP dm ON sp.MaDanhMuc = dm.MaDanhMuc 
                     WHERE sp.MaNCC = @MaNCC";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaNCC", maNCC)
            };

            return db.LayDuLieu(query, parameters);
        }
        public DataTable TimKiemSanPhamTheoTenVaNCC(string ten, int maNCC)
        {
            string sql = @"
    SELECT sp.MaSP, sp.TenSP, sp.SoLuong, sp.GiaNhap, sp.GiaXuat, sp.DonViTinh, 
           sp.MoTa, sp.NgayNhap, sp.HanSuDung, sp.TinhTrang, sp.MaNCC, 
           ncc.HoTen, sp.MaDanhMuc, dm.TenDanhMuc
    FROM SanPham sp
    JOIN NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC
    JOIN DanhMucSP dm ON sp.MaDanhMuc = dm.MaDanhMuc
    WHERE sp.TenSP LIKE @TenSP AND sp.MaNCC = @MaNCC";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenSP", "%" + ten + "%"),
        new SqlParameter("@MaNCC", maNCC)
            };

            return db.LayDuLieu(sql, parameters);
        }
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;

        public bool KiemTraTrungSanPham(string tenSP, int maNCC)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM SanPham WHERE TenSP = @TenSP AND MaNCC = @MaNCC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenSP", tenSP);
                cmd.Parameters.AddWithValue("@MaNCC", maNCC);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }

        }
        public bool KiemTraSanPhamTrongPhieu(int maSP)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = @"
            SELECT COUNT(*) FROM ChiTietPhieuDat WHERE MaSP = @MaSP
            UNION ALL
            SELECT COUNT(*) FROM ChiTietPhieuNhap WHERE MaSP = @MaSP
            UNION ALL
            SELECT COUNT(*) FROM ChiTietPhieuXuat WHERE MaSP = @MaSP";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Convert.ToInt32(reader[0]) > 0)
                        {
                            return true; // Có tồn tại sản phẩm trong phiếu => Không cho xóa
                        }
                    }
                }
                return false; // Không tồn tại ở phiếu => cho phép xóa
            }
        }

        public bool XoaSanPham(SanPhamDTO sp)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Check trước khi xóa
                if (KiemTraSanPhamTrongPhieu(sp.MaSP))
                {
                    return false; // Không cho phép xóa
                }

                string sql = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}