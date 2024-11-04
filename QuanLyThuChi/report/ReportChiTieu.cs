using BUS;
using DTO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuChi
{
    public partial class ReportChiTieu : Form
    {
        public ReportChiTieu()
        {
            InitializeComponent();

            radioButton2.Checked = true;
        }

        private void ReportChiTieu_Load(object sender, EventArgs e)
        {

            if  (radioButton2.Checked)
            {
                label1.Text = "vui lòng chọn 'tháng - năm' để lọc dữ liệu";
            }
            else
            {
                label1.Text = "vui lòng chọn 'năm' để lọc dữ liệu";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            reportViewer1.Visible = true;

            this.reportViewer1.RefreshReport();

            List<DTO_TienChi> list = new List<DTO_TienChi>();

            if (radioButton2.Checked)
            {
                // Lấy giá trị DateTime từ DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value;

                // Lấy tháng từ giá trị DateTime
                int month = selectedDate.Month;
                int year = selectedDate.Year;

                DataTable lsTienchi = BUS_BaoCao.HienThiBaoCaoTienChi_thangnam(TruyenData.Instance.LoginTK, month.ToString(), year.ToString());

                foreach (DataRow row in lsTienchi.Rows)
                {
                    DTO_TienChi obj = new DTO_TienChi();

                    obj.Sngay_chi = DateTime.Parse(row["ngay_chi"].ToString());
                    obj.Smo_ta = row["mo_ta"].ToString();
                    obj.Sso_tien = int.Parse(row["so_tien"].ToString());

                    list.Add(obj);
                }


                if (lsTienchi != null)
                {
                    // Tạo một ReportDataSource với tên "DataSetName" (tên dataset trong file RDLC) và gán DataTable vào đó
                    ReportDataSource rdsc = new ReportDataSource("DataSet1", list);


                    // Đặt đường dẫn của report RDLC
                    this.reportViewer1.LocalReport.ReportPath = "ReportChitieu.rdlc";

                    // Xóa các dataSource hiện có (nếu có)
                    reportViewer1.LocalReport.DataSources.Clear();

                    // Thêm ReportDataSource vào LocalReport
                    reportViewer1.LocalReport.DataSources.Add(rdsc);

                    // Refresh reportViewer để hiển thị report
                    reportViewer1.RefreshReport();
                }
            }    
            else
            {
                // Lấy giá trị DateTime từ DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value;

                int year = selectedDate.Year;

                DataTable lsTienchi = BUS_BaoCao.HienThiBaoCaoTienChi_nam(TruyenData.Instance.LoginTK,  year.ToString());

                foreach (DataRow row in lsTienchi.Rows)
                {
                    DTO_TienChi obj = new DTO_TienChi();

                    obj.Sngay_chi = DateTime.Parse(row["ngay_chi"].ToString());
                    obj.Smo_ta = row["mo_ta"].ToString();
                    obj.Sso_tien = int.Parse(row["so_tien"].ToString());

                    list.Add(obj);
                }


                if (lsTienchi != null)
                {
                    // Tạo một ReportDataSource với tên "DataSetName" (tên dataset trong file RDLC) và gán DataTable vào đó
                    ReportDataSource rdsc = new ReportDataSource("DataSet1", list);


                    // Đặt đường dẫn của report RDLC
                    this.reportViewer1.LocalReport.ReportPath = "ReportChitieu.rdlc";

                    // Xóa các dataSource hiện có (nếu có)
                    reportViewer1.LocalReport.DataSources.Clear();

                    // Thêm ReportDataSource vào LocalReport
                    reportViewer1.LocalReport.DataSources.Add(rdsc);

                    // Refresh reportViewer để hiển thị report
                    reportViewer1.RefreshReport();
                }
            }
              
        }

        private void button2_Click(object sender, EventArgs e)
        {

            reportViewer1.Visible = true;

            this.reportViewer1.RefreshReport();

            List<DTO_TienThu> list = new List<DTO_TienThu>();

            if (radioButton2.Checked)
            {
                // Lấy giá trị DateTime từ DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value;

                // Lấy tháng từ giá trị DateTime
                int month = selectedDate.Month;
                int year = selectedDate.Year;

                DataTable lsTienchi = BUS_BaoCao.HienThiBaoCaoTienThu_thangnam(TruyenData.Instance.LoginTK, month.ToString(), year.ToString());

                foreach (DataRow row in lsTienchi.Rows)
                {
                    DTO_TienThu obj = new DTO_TienThu();

                    obj.Sngay_thu = DateTime.Parse(row["ngay_thu"].ToString());
                    obj.Smo_ta = row["mo_ta"].ToString();
                    obj.Sso_tien = int.Parse(row["so_tien"].ToString());

                    list.Add(obj);
                }


                if (lsTienchi != null)
                {
                    // Tạo một ReportDataSource với tên "DataSetName" (tên dataset trong file RDLC) và gán DataTable vào đó
                    ReportDataSource rdsc = new ReportDataSource("DataSet1", list);


                    // Đặt đường dẫn của report RDLC
                    this.reportViewer1.LocalReport.ReportPath = "ReportTienThu.rdlc";

                    // Xóa các dataSource hiện có (nếu có)
                    reportViewer1.LocalReport.DataSources.Clear();

                    // Thêm ReportDataSource vào LocalReport
                    reportViewer1.LocalReport.DataSources.Add(rdsc);

                    // Refresh reportViewer để hiển thị report
                    reportViewer1.RefreshReport();
                }
            }    
            else
            {
                // Lấy giá trị DateTime từ DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value;

                int year = selectedDate.Year;

                DataTable lsTienchi = BUS_BaoCao.HienThiBaoCaoTienThu_nam(TruyenData.Instance.LoginTK,  year.ToString());

                foreach (DataRow row in lsTienchi.Rows)
                {
                    DTO_TienThu obj = new DTO_TienThu();

                    obj.Sngay_thu = DateTime.Parse(row["ngay_thu"].ToString());
                    obj.Smo_ta = row["mo_ta"].ToString();
                    obj.Sso_tien = int.Parse(row["so_tien"].ToString());

                    list.Add(obj);
                }


                if (lsTienchi != null)
                {
                    // Tạo một ReportDataSource với tên "DataSetName" (tên dataset trong file RDLC) và gán DataTable vào đó
                    ReportDataSource rdsc = new ReportDataSource("DataSet1", list);


                    // Đặt đường dẫn của report RDLC
                    this.reportViewer1.LocalReport.ReportPath = "ReportTienThu.rdlc";

                    // Xóa các dataSource hiện có (nếu có)
                    reportViewer1.LocalReport.DataSources.Clear();

                    // Thêm ReportDataSource vào LocalReport
                    reportViewer1.LocalReport.DataSources.Add(rdsc);

                    // Refresh reportViewer để hiển thị report
                    reportViewer1.RefreshReport();
                }
            }
        }


        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label1.Text = "vui lòng chọn 'tháng - năm' để lọc dữ liệu";
            }
            else
            {
                label1.Text = "vui lòng chọn 'năm' để lọc dữ liệu";
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
          if (radioButton2.Checked)
            {
                label1.Text = "vui lòng chọn 'tháng - năm' để lọc dữ liệu";
            }
            else
            {
                label1.Text = "vui lòng chọn 'năm' để lọc dữ liệu";
            }
        }
    }
}
