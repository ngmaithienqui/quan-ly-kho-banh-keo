using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanhKeo.DTO
{
    internal class BangDuTru
    {
        int mabdt;
        DateTime ngaylap;

        public int MaBDT
        {
            get { return mabdt; }
            set { mabdt = value; }
        }
        public DateTime NgayLap
        {
            get { return ngaylap; }
            set { ngaylap = value; }
        }
        public BangDuTru(DateTime ngaylap)
        {
            NgayLap = ngaylap;
        }
    }
}