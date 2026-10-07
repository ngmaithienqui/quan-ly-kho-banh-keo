using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    public class ChiTietPhieuKiemKeDTO
    {

        public int MaChiTiet { get; set; }
        public int MaPhieu { get; set; }
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuongSPThuc { get; set; }
        public int SoLuongHeThong { get; set; }
        public int SoLuongSaiLech
        {
            get
            {
                return SoLuongSPThuc - SoLuongHeThong;
            }

        }

        public string LyDo { get; set; }
    }


}
