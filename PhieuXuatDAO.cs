using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo.DAO
{
    public class PhieuXuatDAO
    {
        private string connStr = ConfigurationManager.ConnectionStrings["QLCuaHang"].ConnectionString;

        public List<PhieuXuatDTO> LayPhieuXuat(string trangThai, DateTime tuNgay, DateTime denNgay)
        {
            List<PhieuXuatDTO> list = new List<PhieuXuatDTO>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = @"SELECT px.MaPhieu, px.NgayXuatHang, kh.HoTen AS TenKH, 
                               px.TrangThai, px.ThanhTien, px.TrangThaiThanhToan
                               FROM PhieuXuatHang px
                               INNER JOIN KhachHang kh ON px.MaKH = kh.MaKH";

                if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
                    sql += " WHERE px.TrangThai = @TrangThai";
                else
                    sql += " WHERE 1=1";

                sql += " AND px.NgayXuatHang BETWEEN @TuNgay AND @DenNgay ORDER BY px.NgayXuatHang DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhieuXuatDTO
                            {
                                MaPhieu = Convert.ToInt32(reader["MaPhieu"]),
                                NgayXuatHang = Convert.ToDateTime(reader["NgayXuatHang"]),
                                TenKH = reader["TenKH"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                ThanhTien = Convert.ToDecimal(reader["ThanhTien"]),
                                TrangThaiThanhToan = reader["TrangThaiThanhToan"] == DBNull.Value ? "Chưa thanh toán" : reader["TrangThaiThanhToan"].ToString()
                            });
                        }
                    }
                }
            }

            return list;
        }

        public bool XoaPhieuXuat(int maPhieu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    string deleteChiTiet = "DELETE FROM ChiTietPhieuXuat WHERE MaPhieu = @MaPhieu";
                    using (SqlCommand cmd1 = new SqlCommand(deleteChiTiet, conn, tran))
                    {
                        cmd1.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmd1.ExecuteNonQuery();
                    }

                    string deleteMain = "DELETE FROM PhieuXuatHang WHERE MaPhieu = @MaPhieu";
                    using (SqlCommand cmd2 = new SqlCommand(deleteMain, conn, tran))
                    {
                        cmd2.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        int result = cmd2.ExecuteNonQuery();
                        tran.Commit();
                        return result > 0;
                    }
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public string LayTrangThai(int maPhieu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT TrangThai FROM PhieuXuatHang WHERE MaPhieu = @MaPhieu";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        public string LayTrangThaiThanhToan(int maPhieu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT TrangThaiThanhToan FROM PhieuXuatHang WHERE MaPhieu = @MaPhieu";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    object result = cmd.ExecuteScalar();
                    return result == DBNull.Value ? "Chưa thanh toán" : result?.ToString();
                }
            }
        }

        public bool CapNhatTrangThaiThanhToan(int maPhieu, string trangThaiThanhToan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "UPDATE PhieuXuatHang SET TrangThaiThanhToan = @TrangThaiThanhToan WHERE MaPhieu = @MaPhieu";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmd.Parameters.AddWithValue("@TrangThaiThanhToan", trangThaiThanhToan);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void InPhieuXuat(int maPhieu)
        {
            DataTable dtPhieu = GetPhieuXuatInfo(maPhieu);
            DataTable dtChiTiet = GetChiTietPhieuXuat(maPhieu);

            if (dtPhieu.Rows.Count == 0 || dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu để in phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

            printDoc.PrintPage += (s, e) =>
            {
                Font titleFont = new Font("Arial", 16, FontStyle.Bold);
                Font headerFont = new Font("Arial", 12, FontStyle.Bold);
                Font normalFont = new Font("Arial", 10);
                Font totalFont = new Font("Arial", 12, FontStyle.Bold);

                int y = 50;
                int margin = 50;
                DataRow rowPhieu = dtPhieu.Rows[0];

                // Tiêu đề
                e.Graphics.DrawString("PHIẾU XUẤT HÀNG", titleFont, Brushes.Black, e.MarginBounds.Width / 2, y, new StringFormat { Alignment = StringAlignment.Center });
                y += 40;

                e.Graphics.DrawString($"Mã phiếu: {rowPhieu["MaPhieu"]}", headerFont, Brushes.Black, margin, y);
                e.Graphics.DrawString($"Ngày: {Convert.ToDateTime(rowPhieu["NgayXuatHang"]).ToString("dd/MM/yyyy")}", headerFont, Brushes.Black, margin + 400, y);
                y += 30;

                e.Graphics.DrawString($"Khách hàng: {rowPhieu["HoTen"]}", normalFont, Brushes.Black, margin, y);
                y += 20;
                e.Graphics.DrawString($"Địa chỉ: {rowPhieu["DiaChi"]}", normalFont, Brushes.Black, margin, y);
                y += 20;
                e.Graphics.DrawString($"SĐT: {rowPhieu["Sdt"]}", normalFont, Brushes.Black, margin, y);
                y += 30;

                // Bảng chi tiết
                e.Graphics.DrawString("STT", headerFont, Brushes.Black, margin, y);
                e.Graphics.DrawString("Sản phẩm", headerFont, Brushes.Black, margin + 50, y);
                e.Graphics.DrawString("SL", headerFont, Brushes.Black, margin + 300, y);
                e.Graphics.DrawString("Đơn giá", headerFont, Brushes.Black, margin + 370, y);
                e.Graphics.DrawString("Thành tiền", headerFont, Brushes.Black, margin + 470, y);
                y += 25;

                int stt = 1;
                foreach (DataRow r in dtChiTiet.Rows)
                {
                    e.Graphics.DrawString(stt.ToString(), normalFont, Brushes.Black, margin, y);
                    e.Graphics.DrawString(r["TenSP"].ToString(), normalFont, Brushes.Black, margin + 50, y);
                    e.Graphics.DrawString(r["SoLuong"].ToString(), normalFont, Brushes.Black, margin + 300, y);
                    e.Graphics.DrawString($"{Convert.ToDecimal(r["GiaXuat"]):N0} đ", normalFont, Brushes.Black, margin + 370, y);
                    e.Graphics.DrawString($"{Convert.ToDecimal(r["ThanhTien"]):N0} đ", normalFont, Brushes.Black, margin + 470, y);
                    y += 20;
                    stt++;
                }

                y += 20;
                e.Graphics.DrawLine(Pens.Black, margin, y, margin + 550, y);
                y += 10;
                e.Graphics.DrawString($"Tổng tiền: {Convert.ToDecimal(rowPhieu["ThanhTien"]):N0} đ", totalFont, Brushes.Black, margin + 370, y);
            };

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDoc
            };
            preview.ShowDialog();
        }

        private DataTable GetPhieuXuatInfo(int maPhieu)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT px.MaPhieu, px.NgayXuatHang, kh.HoTen, kh.Sdt, kh.DiaChi,
                               px.TrangThai, px.ThanhTien, px.GhiChu
                               FROM PhieuXuatHang px
                               INNER JOIN KhachHang kh ON px.MaKH = kh.MaKH
                               WHERE px.MaPhieu = @MaPhieu";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        private DataTable GetChiTietPhieuXuat(int maPhieu)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT sp.TenSP, ct.SoLuong, ct.GiaXuat, 
                                      (ct.SoLuong * ct.GiaXuat) AS ThanhTien
                               FROM ChiTietPhieuXuat ct
                               INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                               WHERE ct.MaPhieu = @MaPhieu";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}