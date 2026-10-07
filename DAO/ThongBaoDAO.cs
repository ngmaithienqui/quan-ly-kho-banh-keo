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
    internal class ThongBaoDAO
    {
        DBConnection dbconn = new DBConnection();
        public DataTable LayDanhSachThongBao()
        {
            string query = "SELECT * FROM ThongBaoSanPham";
            return dbconn.LayDuLieu(query);
        }
        public void KiemTraSanPhamHetHan()
        {
            string ngayHomNay = DateTime.Now.ToString("yyyy-MM-dd");

            // Kiểm tra đã có thông báo hôm nay chưa
            string kiemTraThongBao = $"SELECT COUNT(*) FROM ThongBaoSanPham WHERE NgayThongBao = '{ngayHomNay}'";
            int count = Convert.ToInt32(dbconn.ThucThiGiaTri(kiemTraThongBao));

            if (count > 0)
                return;

            // Lấy các sản phẩm gần hết hạn (<= 30 ngày và >= 0 ngày)
            string query = @"
            SELECT MaSP, TenSP, HanSuDung
            FROM SanPham
            WHERE DATEDIFF(day, GETDATE(), HanSuDung) <= 30 AND DATEDIFF(day, GETDATE(), HanSuDung) >= 0";

            DataTable dtSanPham = dbconn.LayDuLieu(query);

            if (dtSanPham.Rows.Count == 0)
                return;

            // 1. Tạo bản ghi thông báo mới
            string noiDung = "Sản phẩm sắp hết hạn";
            string insertThongBao = $@"
            INSERT INTO ThongBaoSanPham (NgayThongBao, NoiDungThongBao)
            VALUES ('{ngayHomNay}', N'{noiDung}');
            SELECT SCOPE_IDENTITY();";

            int maThongBao = Convert.ToInt32(dbconn.ThucThiGiaTri(insertThongBao));

            // 2. Thêm vào chi tiết
            foreach (DataRow row in dtSanPham.Rows)
            {
                int maSP = Convert.ToInt32(row["MaSP"]);
                string insertChiTiet = $@"
                INSERT INTO ThongBaoChiTiet (MaThongBao, MaSanPham)
                VALUES ({maThongBao}, {maSP})";
                dbconn.ThucThi(insertChiTiet);
            }
        }
        public DataTable LayChiTietThongBao(int maThongBao)
        {
            string query = $@"
            SELECT sp.TenSP, sp.HanSuDung
            FROM ThongBaoChiTiet ct
            JOIN SanPham sp ON ct.MaSanPham = sp.MaSP
            WHERE ct.MaThongBao = {maThongBao}";
            return dbconn.LayDuLieu(query);
        }
        /*public bool CoThongBaoMoiTrongNgay()
        {
            string query = "SELECT COUNT(*) FROM ThongBaoSanPham WHERE NgayThongBao = CAST(GETDATE() AS DATE) AND DaXem = 0";
            int count = Convert.ToInt32(dbconn.ThucThiGiaTri(query));
            return count > 0;
        }
        

        public void DanhDauThongBaoDaXem()
        {
            string updateQuery = @"
            UPDATE ThongBaoSanPham 
            SET DaXem = 1 
            WHERE NgayThongBao = CAST(GETDATE() AS DATE)";

            dbconn.ThucThi(updateQuery);
        }*/
        public bool CoThongBaoChuaDoc()
        {
            string query = "SELECT COUNT(*) FROM ThongBaoSanPham WHERE DaXem = 0";
            int count = Convert.ToInt32(dbconn.ThucThiGiaTri(query));
            return count > 0;
        }
        public void DanhDauTatCaThongBaoDaXem()
        {
            string query = "UPDATE ThongBaoSanPham SET DaXem = 1 WHERE DaXem = 0";
            dbconn.ThucThi(query);
        }
        public int DemThongBaoChuaDoc()
        {
            string query = "SELECT COUNT(*) FROM ThongBaoSanPham WHERE DaXem = 0";
            object result = dbconn.ThucThiGiaTri(query);

            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count;
            }

            return 0; // trả về 0 nếu không có kết quả hoặc lỗi
        }
        public int ThemThongBaoSanPham(DateTime ngayThongBao, string noiDung)

        {

            string query = "INSERT INTO ThongBaoSanPham (NgayThongBao, NoiDungThongBao) OUTPUT INSERTED.MaThongBao VALUES (@Ngay, @NoiDung)";

            SqlParameter[] parameters = {

                new SqlParameter("@Ngay", ngayThongBao),

                new SqlParameter("@NoiDung", noiDung)

            };



            object result = dbconn.ThucThiGiaTri(query, parameters);

            return result != null ? Convert.ToInt32(result) : -1;

        }



        // ➡️ Thêm chi tiết lỗi nhập/xuất thất bại

        public void ThemChiTietThongBaoThatBai(int maThongBao, string loaiSuCo, int? soLuong, string ghiChu)

        {

            string query = @"

                INSERT INTO ThongBaoChiTietNhapXuatThatBai (MaThongBao, LoaiSuCo, SoLuong, GhiChu)

                VALUES (@MaThongBao, @LoaiSuCo, @SoLuong, @GhiChu)";

            SqlParameter[] parameters = {

                new SqlParameter("@MaThongBao", maThongBao),

                new SqlParameter("@LoaiSuCo", loaiSuCo),

                new SqlParameter("@SoLuong", (object)soLuong ?? DBNull.Value),

                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)

            };

            dbconn.ThucThi(query, parameters);

        }



        // ➡️ Lấy chi tiết lỗi theo mã thông báo

        public DataTable LayChiTietThongBaoThatBai(int maThongBao)

        {

            string query = @"

                SELECT LoaiSuCo, SoLuong, GhiChu 

                FROM ThongBaoChiTietNhapXuatThatBai

                WHERE MaThongBao = @MaThongBao";

            SqlParameter[] parameters = {

                new SqlParameter("@MaThongBao", maThongBao)

            };

            return dbconn.LayDuLieu(query, parameters);

        }

        public void ThemThongBaoNhap(int maThongBao, int maPhieuNhap)

        {

            string query = @"

        INSERT INTO ThongBaoNhap (MaThongBao, MaPhieuNhap)

        VALUES (@MaThongBao, @MaPhieuNhap)";



            SqlParameter[] parameters = new SqlParameter[]

            {

        new SqlParameter("@MaThongBao", maThongBao),

        new SqlParameter("@MaPhieuNhap", maPhieuNhap)

            };



            dbconn.ThucThi(query, parameters); // Gọi DBConnection để thực thi

        }
    }
}