using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo.DAO
{
    public class ThongKeDAO
    {
        private string connStr = Properties.Settings.Default.connStr;

        public List<BaoCaoDoanhThuDTO> LayBaoCaoTheoLoai(string loai, DateTime tuNgay, DateTime denNgay)
        {
            List<(DateTime Ngay, double Tien, string Loai)> allData = new List<(DateTime, double, string)>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"
                        SELECT NgayNhap AS Ngay, ThanhTien, 'Nhap' AS Loai FROM dbo.PhieuNhapHang WHERE NgayNhap BETWEEN @tuNgay AND @denNgay
                        UNION ALL
                        SELECT NgayXuatHang AS Ngay, ThanhTien, 'Xuat' AS Loai FROM dbo.PhieuXuatHang WHERE NgayXuatHang BETWEEN @tuNgay AND @denNgay";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@denNgay", denNgay);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allData.Add((
                                Convert.ToDateTime(reader["Ngay"]),
                                Convert.ToDouble(reader["ThanhTien"]),
                                reader["Loai"].ToString()
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<BaoCaoDoanhThuDTO>();
            }

            Func<DateTime, string> keySelector;

            if (loai == "Theo ngày")
            {
                keySelector = x => x.ToString("dd/MM/yyyy");
            }
            else if (loai == "Theo tuần")
            {
                keySelector = x =>
                    "Tuần " + System.Globalization.CultureInfo.CurrentCulture.Calendar
                        .GetWeekOfYear(x, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            }
            else // Theo tháng
            {
                keySelector = x => x.ToString("MM/yyyy");
            }

            return allData
                .GroupBy(x => keySelector(x.Ngay))
                .Select(g => new BaoCaoDoanhThuDTO
                {
                    ThoiGian = g.Key,
                    TongDoanhThu = g.Where(x => x.Loai == "Xuat").Sum(x => x.Tien) - g.Where(x => x.Loai == "Nhap").Sum(x => x.Tien)
                })
                .ToList();
        }
    }
}
