using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    public partial class FPheDuyetKiemKe: Form
    {
        DBConnection db = new DBConnection();
        private int _maPhieu;
        private FLichSuKiemKeCDL _parentForm;
        public FPheDuyetKiemKe(int maPhieu, FLichSuKiemKeCDL parentForm)
        {
            InitializeComponent();
            _maPhieu = maPhieu;
            _parentForm = parentForm;
            this.Load += FPheDuyetKiemKe_Load;
        }


        private void dgv_ChiTietKiemKe_PheDuyet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //abcxyz
        }

        private void FPheDuyetKiemKe_Load(object sender, EventArgs e)
        {
            LoadChiTietPhieu();
        }

        private void LoadChiTietPhieu()
        {
            List<ChiTietPhieuKiemKeDTO> chitiet = ChiTietPhieuKiemKeDAO.LayChiTietTheoPhieu(_maPhieu);

            // Configure the DataGridView first
            dgv_ChiTietKiemKe_PheDuyet.AutoGenerateColumns = false;

            // Clear existing columns if any
            dgv_ChiTietKiemKe_PheDuyet.Columns.Clear();

            // Create columns programmatically
            dgv_ChiTietKiemKe_PheDuyet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaChiTiet",
                HeaderText = "Mã chi tiết",
                DataPropertyName = "MaChiTiet"
            });

            dgv_ChiTietKiemKe_PheDuyet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaPhieu",
                HeaderText = "Mã phiếu",
                DataPropertyName = "MaPhieu"
            });

            dgv_ChiTietKiemKe_PheDuyet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaSP",
                HeaderText = "Mã sản phẩm",
                DataPropertyName = "MaSP"
            });

            dgv_ChiTietKiemKe_PheDuyet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenSP",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "TenSP"
            });

            dgv_ChiTietKiemKe_PheDuyet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSLSanPhamThuc",
                HeaderText = "Số lượng sản phẩm thực tế",
                DataPropertyName = "SoLuongSPThuc"
            });

            dgv_ChiTietKiemKe_PheDuyet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoSPSaiLech",
                HeaderText = "Số lượng sản phẩm sai lệch",
                DataPropertyName = "SoLuongSaiLech"
            });

            dgv_ChiTietKiemKe_PheDuyet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLyDo",
                HeaderText = "Lý do",
                DataPropertyName = "LyDo"
            });

            // Now set the data source
            dgv_ChiTietKiemKe_PheDuyet  .DataSource = chitiet;
        }

        private void btnPheDuyet_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn phê duyệt phiếu kiểm kê này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();

                    try
                    {
                        List<ChiTietPhieuKiemKeDTO> chiTietList = ChiTietPhieuKiemKeDAO.LayChiTietTheoPhieu(_maPhieu);

                        foreach (var ct in chiTietList)
                        {
                            bool capNhat = SanPhamDAO.CapNhatSoLuongThucTe(ct.MaSP, ct.SoLuongSPThuc, conn, trans);
                            if (!capNhat)
                                throw new Exception("Cập nhật tồn kho thất bại!");
                        }

                        bool daDuyet = PhieuKiemKeDAO.DuyetPhieuKiemKe(_maPhieu, conn, trans);
                        if (!daDuyet)
                            throw new Exception("Phê duyệt phiếu thất bại!");

                        trans.Commit();

                        MessageBox.Show("Phiếu kiểm kê đã được phê duyệt!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _parentForm?.LoadLichSuKiemKeCDL();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Lỗi khi phê duyệt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


    }
}
