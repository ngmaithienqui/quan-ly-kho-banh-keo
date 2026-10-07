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
        public double ThanhTien { get; set; }
        public DateTime NgayXuatHang { get; set; }
        public string TrangThai { get; set; }
        public string TrangThaiThanhToan { get; set; } // ChuaThanhToan, DaThanhToan

        public PhieuXuatHangDTO() { }

        public PhieuXuatHangDTO(int maPhieu, int maKH, double thanhTien, DateTime ngayXuatHang, string trangThai, string trangThaiThanhToan)
        {
            MaPhieu = maPhieu;
            MaKH = maKH;
            ThanhTien = thanhTien;
            NgayXuatHang = ngayXuatHang;
            TrangThai = trangThai;
            TrangThaiThanhToan = "Chưa thanh toán"; // Mặc định là chưa thanh toán
        }
    }
}

