using QLCuaHangBanhKeo.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FXemPhieuNhapHangQLK: Form
    {
        PhieuNhapHangDAO pnhDAO = new PhieuNhapHangDAO();
        public FXemPhieuNhapHangQLK()
        {
            InitializeComponent();
        }

        private void FXemPhieuNhapHangQLK_Load(object sender, EventArgs e)
        {
            dgvPhieuNhap.DataSource = pnhDAO.LayDanhSachPhieuNhap();

        }

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maPN = Convert.ToInt32(dgvPhieuNhap.Rows[e.RowIndex].Cells["MaPhieu"].Value);
                HienThiChiTietPhieuNhap(maPN);

                
            }
        }

        private void HienThiChiTietPhieuNhap(int maPN)
        {
            DataTable dtChiTiet = pnhDAO.LayChiTietPhieuNhap(maPN);
            dgvChiTietPN.DataSource = dtChiTiet;
        }

        private void gbChiTiet_Enter(object sender, EventArgs e)
        {

        }
    }
}
