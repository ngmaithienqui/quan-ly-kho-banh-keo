using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    public class PhieuNhapHangDTO
    {
        public int MaPhieu { get; set; }
        public int MaNCC { get; set; }
        public double ThanhTien { get; set; }
        public DateTime NgayNhap { get; set; }
        public string TrangThai { get; set; }
        public string TrangThaiThanhToan { get; set; } // ChuaThanhToan, DaThanhToan

        public PhieuNhapHangDTO() { }

        public PhieuNhapHangDTO(int maPhieu, int maNCC, double thanhTien, DateTime ngayNhap, string trangThai, string trangThaiThanhToan)
        {
            MaPhieu = maPhieu;
            MaNCC = maNCC;
            ThanhTien = thanhTien;
            NgayNhap = ngayNhap;
            TrangThai = trangThai;
            TrangThaiThanhToan = "Chưa thanh toán"; // Mặc định là chưa thanh toán
        }
    }
}

