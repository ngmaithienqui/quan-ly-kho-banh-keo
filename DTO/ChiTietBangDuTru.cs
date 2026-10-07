using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    internal class ChiTietBangDuTru
    {
        int machitiet;
        int masp;
        int soluongnhap;
        int soluongsp;

        public int MaChiTiet
        {
            get { return machitiet; }
            set { machitiet = value; }
        }
        public int MaSP
        {
            get { return masp; }
            set { masp = value; }
        }
        public int SoLuongNhap
        {
            get { return soluongnhap; }
            set { soluongnhap = value; }
        }
        public int SoLuongSp
        {
            get { return soluongsp; }
            set { soluongsp = value; }
        }
        public ChiTietBangDuTru(int machitiet, int masp, int soluongnhap, int soluongsp)
        {
            MaChiTiet = machitiet;
            MaSP = masp;
            SoLuongNhap = soluongnhap;
            SoLuongSp = soluongsp;
        }
        public ChiTietBangDuTru(int masp, int soluongnhap, int soluongsp)
        {
            MaSP = masp;
            SoLuongNhap = soluongnhap;
            SoLuongSp = soluongsp;
        }
    }
}