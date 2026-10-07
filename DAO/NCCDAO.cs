using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo.DAO
{
    public class NCCDAO
    {
        private static DBConnection db = new DBConnection();

        public static DataTable GetAllNCC()
        {
            string sql = "SELECT * FROM NhaCungCap";
            return db.LayDuLieu(sql);
        }

        public static bool Insert(NhaCCDTO ncc)
        {
            // 1. Kiểm tra Sdt đã tồn tại chưa
            string checkSql = "SELECT COUNT(*) FROM NhaCungCap WHERE Sdt = @Sdt";
            SqlParameter[] checkParams = {
        new SqlParameter("@Sdt", ncc.Sdt)
    };
            int count = Convert.ToInt32(db.ThucThiGiaTri(checkSql, checkParams));

            if (count > 0)
            {
                MessageBox.Show("Số điện thoại này đã tồn tại. Vui lòng nhập số khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Nếu chưa tồn tại thì Insert
            string sql = "INSERT INTO NhaCungCap (HoTen, Sdt, DiaChi, CongNo, LichSuGiaoDich) " +
                         "VALUES (@HoTen , @Sdt, @DiaChi, @CongNo, @LichSuGiaoDich)";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@HoTen", ncc.HoTen),
        new SqlParameter("@Sdt", ncc.Sdt),
        new SqlParameter("@DiaChi", ncc.DiaChi),
        new SqlParameter("@CongNo", ncc.CongNo),
        new SqlParameter("@LichSuGiaoDich", ncc.LichSuGiaoDich ?? "")
            };

            db.ThucThi(sql, parameters);
            return true;
        }


        public static bool Update(NhaCCDTO ncc)
        {
            // 1. Kiểm tra xem SĐT này đã tồn tại ở nhà cung cấp khác chưa
            string checkSql = "SELECT COUNT(*) FROM NhaCungCap WHERE Sdt = @Sdt AND MaNCC <> @MaNCC";
            SqlParameter[] checkParams = new SqlParameter[]
            {
        new SqlParameter("@Sdt", ncc.Sdt),
        new SqlParameter("@MaNCC", ncc.MaNCC)
            };

            int count = Convert.ToInt32(db.ThucThiGiaTri(checkSql, checkParams));

            if (count > 0)
            {
                MessageBox.Show("Số điện thoại đã tồn tại ở nhà cung cấp khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Nếu không trùng thì cập nhật
            string sql = "UPDATE NhaCungCap SET HoTen=@HoTen, Sdt=@Sdt, DiaChi=@DiaChi, CongNo=@CongNo WHERE MaNCC=@MaNCC";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaNCC", ncc.MaNCC),
        new SqlParameter("@HoTen", ncc.HoTen),
        new SqlParameter("@Sdt", ncc.Sdt),
        new SqlParameter("@DiaChi", ncc.DiaChi),
        new SqlParameter("@CongNo", ncc.CongNo)
            };

            db.ThucThi(sql, parameters);
            return true;
        }

        public static bool KiemTraNCCCoPhieuXuat(int maNCC)
        {
            string query = "SELECT COUNT(*) FROM dbo.PhieuNhapHang WHERE MaNCC = @MaNCC";

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNCC", maNCC);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public static bool Delete(int maNCC)
        {
            // 1. Kiểm tra công nợ hiện tại
            string checkSql = "SELECT CongNo FROM NhaCungCap WHERE MaNCC = @MaNCC";
            SqlParameter[] checkParams = new SqlParameter[]
            {
        new SqlParameter("@MaNCC", maNCC)
            };

            object congNoObj = db.ThucThiGiaTri(checkSql, checkParams);
            float congNo = congNoObj != null ? Convert.ToSingle(congNoObj) : 0;

            // 2. Nếu công nợ > 0 thì không cho xóa
            if (congNo > 0)
            {
                MessageBox.Show("Không thể xóa nhà cung cấp vì vẫn còn công nợ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 3. Nếu không còn công nợ thì xóa
            string sql = "DELETE FROM NhaCungCap WHERE MaNCC=@MaNCC";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaNCC", maNCC)
            };
            db.ThucThi(sql, parameters);
            return true;
        }


        public static string GetLichSuGiaoDich(int maNCC)
        {
            string sql = "SELECT LichSuGiaoDich FROM NhaCungCap WHERE MaNCC=@MaNCC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", maNCC)
            };
            object result = db.ThucThiGiaTri(sql, parameters);
            return result?.ToString() ?? "";
        }

        public static void CapNhatCongNo()
        {
            // Lấy danh sách mã NCC
            DataTable dt = db.LayDuLieu("SELECT MaNCC FROM NhaCungCap");
            foreach (DataRow row in dt.Rows)
            {
                int maNCC = Convert.ToInt32(row["MaNCC"]);

                string sqlTinh = "SELECT ISNULL(SUM(ThanhTien), 0) FROM PhieuNhapHang WHERE MaNCC = @MaNCC AND TrangThaiThanhToan = N'Chưa thanh toán'";
                SqlParameter[] paramTinh = { new SqlParameter("@MaNCC", maNCC) };
                float congNo = Convert.ToSingle(db.ThucThiGiaTri(sqlTinh, paramTinh));

                string sqlUpdate = "UPDATE NhaCungCap SET CongNo = @CongNo WHERE MaNCC = @MaNCC";
                SqlParameter[] paramUpdate = {
                    new SqlParameter("@CongNo", congNo),
                    new SqlParameter("@MaNCC", maNCC)
                };
                db.ThucThi(sqlUpdate, paramUpdate);

                if (congNo == 0)
                {
                    PhieuNhapHangDAO.CapNhatTrangThaiThanhToan(maNCC); // Cần đảm bảo DAO này tương thích
                }
            }
        }

        public static float LayCongNoHienTai(int maNCC)
        {
            string sql = "SELECT CongNo FROM NhaCungCap WHERE MaNCC = @MaNCC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", maNCC)
            };
            object result = db.ThucThiGiaTri(sql, parameters);
            return (result != null && float.TryParse(result.ToString(), out float congNo)) ? congNo : 0f;
        }
    }
}