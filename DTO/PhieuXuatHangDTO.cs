using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    public class PhieuXuatHangDTO
    {
        public int MaPhieu { get; set; }
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public decimal ThanhTien { get; set; }
        public DateTime NgayXuatHang { get; set; }
        public string TrangThai { get; set; }
        public string TrangThaiThanhToan { get; set; } // ChuaThanhToan, DaThanhToan

        public PhieuXuatHangDTO()
        {
            TrangThaiThanhToan = "Chưa thanh toán"; // Mặc định là chưa thanh toán
        }

        public PhieuXuatHangDTO(int maPhieu, int maKH, decimal thanhTien, DateTime ngayXuatHang, string trangThai, string trangThaiThanhToan)
        {
            MaPhieu = maPhieu;
            MaKH = maKH;
            ThanhTien = thanhTien;
            NgayXuatHang = ngayXuatHang;
            TrangThai = trangThai;
            TrangThaiThanhToan = trangThaiThanhToan ?? "Chưa thanh toán"; // Mặc định là chưa thanh toán nếu null
        }

        public PhieuXuatHangDTO(int maPhieu, int maKH, string tenKH, decimal thanhTien, DateTime ngayXuatHang, string trangThai, string trangThaiThanhToan)
            : this(maPhieu, maKH, thanhTien, ngayXuatHang, trangThai, trangThaiThanhToan)
        {
            TenKH = tenKH;
        }
    }
}