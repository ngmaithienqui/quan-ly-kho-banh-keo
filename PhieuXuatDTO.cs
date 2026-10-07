using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    public class PhieuXuatDTO
    {
        public int MaPhieu { get; set; }
        public DateTime NgayXuatHang { get; set; }
        public string TenKH { get; set; }
        public string TrangThai { get; set; }
        public decimal ThanhTien { get; set; }
        public string TrangThaiThanhToan { get; set; }
    }
}

