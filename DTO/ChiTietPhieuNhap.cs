using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    internal class ChiTietPhieuNhap
    {
        int machitiet;
        int maphieunhap;
        int masp;
        double gianhap;
        double giaxuat;
        int soluongsp;
        string donvitinh;

        public int MaChiTiet
        {
            get { return machitiet; }
            set { machitiet = value; }
        }
        public int MaPhieuNhap
        {
            get { return maphieunhap; }
            set { maphieunhap = value; }
        }
        public int MaSP
        {
            get { return masp; }
            set { masp = value; }
        }
        public double Gianhap
        {
            get { return gianhap; }
            set { gianhap = value; }
        }
        public double GiaXuat
        {
            get { return giaxuat; }
            set { giaxuat = value; }
        }
        public int SoLuongSP
        {
            get { return soluongsp; }
            set { soluongsp = value; }
        }
        public string DonViTinh
        {
            get { return donvitinh; }
            set { donvitinh = value; }
        }
        public ChiTietPhieuNhap(int machitiet, int maphieunhap, int masp, double gianhap, double giaxuat, int soluongsp, string donvitinh)
        {
            MaChiTiet = machitiet;
            MaPhieuNhap = maphieunhap;
            MaSP = masp;
            Gianhap = gianhap;
            GiaXuat = giaxuat;
            SoLuongSP = soluongsp;
            DonViTinh = donvitinh;
        }
        public ChiTietPhieuNhap(int maphieunhap, int masp, double gianhap, double giaxuat, int soluongsp, string donvitinh)
        {
            MaPhieuNhap = maphieunhap;
            MaSP = masp;
            Gianhap = gianhap;
            GiaXuat = giaxuat;
            SoLuongSP = soluongsp;
            DonViTinh = donvitinh;
        }
    }
}
