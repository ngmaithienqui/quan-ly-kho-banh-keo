using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    public class SanPhamDTO
    {
        int masp;
        int mancc;
        int madanhmuc;
        string tensp;
        int soluong;
        double gianhap;
        double giaxuat;
        string donvitinh;
        string mota;
        DateTime ngaynhap;
        DateTime hansudung;
        string tinhtrang;
        public int MaSP
        {
            get { return masp; }
            set { masp = value; }
        }
        public int MaNCC
        {
            get { return mancc; }
            set { mancc = value; }
        }
        public int MaDanhMuc
        {
            get { return madanhmuc; }
            set { madanhmuc = value; }
        }
        public string TenSP
        {
            get { return tensp; }
            set { tensp = value; }
        }
        public int SoLuong
        {
            get { return soluong; }
            set { soluong = value; }
        }


        public double GiaNhap
        {
            get { return gianhap; }
            set { gianhap = value; }
        }
        public double GiaXuat
        {
            get { return giaxuat; }
            set { giaxuat = value; }
        }
        public string DonViTinh
        {
            get { return donvitinh; }
            set { donvitinh = value; }
        }
        public string MoTa
        {
            get { return mota; }
            set { mota = value; }
        }
        public DateTime NgayNhap
        {
            get { return ngaynhap; }
            set { ngaynhap = value; }
        }
        public DateTime HanSuDung
        {
            get { return hansudung; }
            set { hansudung = value; }
        }
        public string TinhTrang
        {
            get { return tinhtrang; }
            set { tinhtrang = value; }
        }

        public SanPhamDTO(int masp)
        {
            MaSP = masp;
        }

        public SanPhamDTO(int masp, int mancc, int madanhmuc, string tensp, int soluong, double gianhap, double giaxuat, string donvitinh, string mota, DateTime ngaynhap, DateTime hansudung, string tinhtrang)
        {
            MaSP = masp;
            MaNCC = mancc;
            MaDanhMuc = madanhmuc;
            TenSP = tensp;
            SoLuong = soluong;
            GiaNhap = gianhap;
            GiaXuat = giaxuat;
            DonViTinh = donvitinh;
            MoTa = mota;
            NgayNhap = ngaynhap;
            HanSuDung = hansudung;
            TinhTrang = tinhtrang;
        }
        public SanPhamDTO(int mancc, int madanhmuc, string tensp, int soluong, double gianhap, double giaxuat, string donvitinh, string mota, DateTime ngaynhap, DateTime hansudung, string tinhtrang)
        {
            MaNCC = mancc;
            MaDanhMuc = madanhmuc;
            TenSP = tensp;
            SoLuong = soluong;
            GiaNhap = gianhap;
            GiaXuat = giaxuat;
            DonViTinh = donvitinh;
            MoTa = mota;
            NgayNhap = ngaynhap;
            HanSuDung = hansudung;
            TinhTrang = tinhtrang;
        }
    }
}
