using QLCuaHangBanhKeo.DAO;
using QLCuaHangBanhKeo.DTO;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLCuaHangBanhKeo
{
    public partial class FThongKe : Form
    {
        private ThongKeDAO thongKeDAO = new ThongKeDAO();

        public FThongKe()
        {
            InitializeComponent();
            InitializeChart();
            SetupUI();
        }

        private void SetupUI()
        {
            cbLoaiThongKe.Items.Clear();
            cbLoaiThongKe.Items.AddRange(new object[] { "Theo ngày", "Theo tuần", "Theo tháng" });
            cbLoaiThongKe.SelectedIndex = 0;

            btnThongKe.Click += btnThongKe_Click;
            btnXuatBaoCao.Click += btnXuatBaoCao_Click;
        }

        private void InitializeChart()
        {
            chartThongKe.ChartAreas.Clear();
            chartThongKe.Series.Clear();

            var area = new ChartArea("MainArea")
            {
                AxisX = { MajorGrid = { Enabled = false } },
                AxisY = { MajorGrid = { Enabled = false } }
            };

            chartThongKe.ChartAreas.Add(area);

            chartThongKe.Series.Add(new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                IsValueShownAsLabel = true,
                Color = Color.DodgerBlue
            });
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (cbLoaiThongKe.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string loai = cbLoaiThongKe.Text;
            DateTime tuNgay = dtpStartDate.Value.Date;
            DateTime denNgay = dtpEndDate.Value.Date;

            var data = thongKeDAO.LayBaoCaoTheoLoai(loai, tuNgay, denNgay);

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trong khoảng thời gian này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvBaoCao.DataSource = data;

            chartThongKe.Series["DoanhThu"].Points.Clear();
            foreach (var item in data)
            {
                chartThongKe.Series["DoanhThu"].Points.AddXY(item.ThoiGian, item.TongDoanhThu);
            }

            lblTongDoanhThu.Text = $"Tổng doanh thu: {data.Sum(x => x.TongDoanhThu):N0} VNĐ";
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            var data = dgvBaoCao.DataSource as List<BaoCaoDoanhThuDTO>;
            if (data != null && data.Any())
            {
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("BaoCao");
                    ws.Cell(1, 1).Value = "Thời gian";
                    ws.Cell(1, 2).Value = "Tổng doanh thu";

                    int row = 2;
                    foreach (var item in data)
                    {
                        ws.Cell(row, 1).Value = item.ThoiGian;
                        ws.Cell(row, 2).Value = item.TongDoanhThu;
                        row++;
                    }

                    SaveFileDialog saveDlg = new SaveFileDialog { Filter = "Excel Workbook|*.xlsx" };
                    if (saveDlg.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(saveDlg.FileName);
                        MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
