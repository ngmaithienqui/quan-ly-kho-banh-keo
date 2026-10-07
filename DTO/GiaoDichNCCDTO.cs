using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    public class GiaoDichNCCDTO
    {
        public int MaGD { get; set; }
        public int MaNCC { get; set; }
        public int? MaPhieu { get; set; }
        public string LoaiGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public float SoTien { get; set; }
        public string GhiChu { get; set; }
    }
}

