using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    public class PhieuKiemKeDTO
    {
        public int MaPhieu { get; set; }
        public DateTime NgayLap { get; set; }
        public string TrangThai { get; set; }
        public string NguoiLapPhieu { get; set; }
        public List<ChiTietPhieuKiemKeDTO> ChiTietKiemKe { get; set; }
    }

}
