using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo.DAO
{
    public class KhachHangDAO
    {
        private static DBConnection db = new DBConnection();

        public static DataTable GetAllKhachHang()
        {
            string sql = "SELECT * FROM KhachHang";
            return db.LayDuLieu(sql);
        }

        public static bool Insert(KhachHangDTO kh)
        {
            // 1. Kiểm tra SĐT đã tồn tại chưa
            string checkSql = "SELECT COUNT(*) FROM KhachHang WHERE Sdt = @Sdt";
            SqlParameter[] checkParams = new SqlParameter[]
            {
        new SqlParameter("@Sdt", kh.Sdt)
            };

            object result = db.ThucThiGiaTri(checkSql, checkParams);
            int count = result != null ? Convert.ToInt32(result) : 0;

            if (count > 0)
            {
                MessageBox.Show("Số điện thoại đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Nếu không trùng thì thêm mới
            string sql = "INSERT INTO KhachHang (HoTen, Sdt, DiaChi, CongNo, LichSuGiaoDich) " +
                         "VALUES (@HoTen, @Sdt, @DiaChi, @CongNo, @LichSuGiaoDich)";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@HoTen", kh.HoTen),
        new SqlParameter("@Sdt", kh.Sdt),
        new SqlParameter("@DiaChi", kh.DiaChi),
        new SqlParameter("@CongNo", kh.CongNo),
        new SqlParameter("@LichSuGiaoDich", kh.LichSuGiaoDich ?? "")
            };
            db.ThucThi(sql, parameters);
            return true;
        }


        public static bool Update(KhachHangDTO kh)
        {
            // 1. Kiểm tra SĐT có bị trùng với khách hàng khác không
            string checkSql = "SELECT COUNT(*) FROM KhachHang WHERE Sdt = @Sdt AND MaKH != @MaKH";
            SqlParameter[] checkParams = new SqlParameter[]
            {
        new SqlParameter("@Sdt", kh.Sdt),
        new SqlParameter("@MaKH", kh.MaKH)
            };

            object result = db.ThucThiGiaTri(checkSql, checkParams);
            int count = result != null ? Convert.ToInt32(result) : 0;

            if (count > 0)
            {
                MessageBox.Show("Số điện thoại đã tồn tại cho khách hàng khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Nếu không trùng thì cập nhật
            string sql = "UPDATE KhachHang SET HoTen=@HoTen, Sdt=@Sdt, DiaChi=@DiaChi, CongNo=@CongNo WHERE MaKH=@MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaKH", kh.MaKH),
        new SqlParameter("@HoTen", kh.HoTen),
        new SqlParameter("@Sdt", kh.Sdt),
        new SqlParameter("@DiaChi", kh.DiaChi),
        new SqlParameter("@CongNo", kh.CongNo)
            };
            db.ThucThi(sql, parameters);
            return true;
        }

        public static bool KiemTraKhachHangCoPhieuXuat(int maKH)
        {
            string query = "SELECT COUNT(*) FROM dbo.PhieuXuatHang WHERE MaKH = @MaKH";

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKH", maKH);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public static bool Delete(int maKH)
        {
            // 1. Kiểm tra công nợ của khách hàng
            string checkSql = "SELECT CongNo FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] checkParams = new SqlParameter[]
            {
        new SqlParameter("@MaKH", maKH)
            };

            object congNoObj = db.ThucThiGiaTri(checkSql, checkParams);
            float congNo = congNoObj != null ? Convert.ToSingle(congNoObj) : 0;

            // 2. Nếu công nợ > 0 thì không cho xóa
            if (congNo > 0)
            {
                MessageBox.Show("Không thể xóa khách hàng vì vẫn còn công nợ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 3. Nếu không còn công nợ thì xóa
            string sql = "DELETE FROM KhachHang WHERE MaKH=@MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaKH", maKH)
            };
            db.ThucThi(sql, parameters);
            return true;
        }


        public static string GetLichSuGiaoDich(int maKH)
        {
            string sql = "SELECT LichSuGiaoDich FROM KhachHang WHERE MaKH=@MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            object result = db.ThucThiGiaTri(sql, parameters);
            return result?.ToString() ?? "";
        }

        public static void CapNhatCongNo()
        {
            string sqlSelectKH = "SELECT MaKH FROM KhachHang";
            DataTable dtKH = db.LayDuLieu(sqlSelectKH);
            foreach (DataRow row in dtKH.Rows)
            {
                int maKH = Convert.ToInt32(row["MaKH"]);

                string sqlTinhCongNo = "SELECT ISNULL(SUM(ThanhTien), 0) FROM PhieuXuatHang WHERE MaKH = @MaKH AND TrangThaiThanhToan = N'Chưa thanh toán'";
                SqlParameter[] paraTinh = { new SqlParameter("@MaKH", maKH) };
                object result = db.ThucThiGiaTri(sqlTinhCongNo, paraTinh);
                float tongCongNo = (result != null && float.TryParse(result.ToString(), out float tmp)) ? tmp : 0;

                string sqlUpdate = "UPDATE KhachHang SET CongNo = @CongNo WHERE MaKH = @MaKH";
                SqlParameter[] paraUpdate = {
                    new SqlParameter("@CongNo", tongCongNo),
                    new SqlParameter("@MaKH", maKH)
                };
                db.ThucThi(sqlUpdate, paraUpdate);

                if (tongCongNo == 0)
                {
                    PhieuXuatHangDAO.CapNhatTrangThaiThanhToan(maKH);
                }
            }
        }

        public static float LayCongNoHienTai(int maKH)
        {
            string sql = "SELECT CongNo FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] parameters = {
                new SqlParameter("@MaKH", maKH)
            };
            object result = db.ThucThiGiaTri(sql, parameters);
            return (result != null && float.TryParse(result.ToString(), out float congNo)) ? congNo : 0f;
        }
    }
}