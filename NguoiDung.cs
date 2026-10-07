using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo
{
    internal class NguoiDung
    {
        public int ID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }
        public string DiaChi { get; set; }
        public string VaiTro { get; set; }

        // Default constructor
        public NguoiDung() { }

        // Constructor with parameters for registration
        public NguoiDung(string tenDangNhap, string matKhau, string hoTen, string email, string sdt)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            HoTen = hoTen;
            Email = email;
            Sdt = sdt;
            VaiTro = "KhachHang"; // Default role for new users
        }
    }
}
