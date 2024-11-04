using BUS;
using DTO;
using QuanLyThuChi.ItemList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Timers;
using Timer = System.Windows.Forms.Timer;

namespace QuanLyThuChi
{
    public partial class BaoCao : UserControl
    {

        DateTime dateTime;
        int thangChoBieuDo = 0; 
        int namChoBieuDo = 0; 

        string series;
        public event EventHandler ButtonClicked_move_userNhapThuChi;

        public BaoCao()
        {
            InitializeComponent();
            flowLayoutPanel_Chi.AutoScroll = true;
            flowLayoutPanel_Chi.WrapContents = true;
            flowLayoutPanel_Thu.AutoScroll = true;
            flowLayoutPanel_Thu.WrapContents = true;
        }
        

        private void BaoCao_Load(object sender, EventArgs e)
        {
            btnChartChi.PerformClick();
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString("MM-yyyy");
            groupBox3.Text = groupBox3.Text + " -> " + date;
            groupBox4.Text = groupBox4.Text + " -> " + date;
            addItemUserControlBaoCao_Flowlayout_chi();
            addItemUserControlBaoCao_Flowlayout_thu();

          
            TongTienChi(dateTime.Month, dateTime.Year);
            TongTienThu(dateTime.Month, dateTime.Year);
            TongSauThuVaChi();

            TongChi_All();
            TongThuNhap_all();
            SauTongThuVaTongChi();
        }
        private bool ChartAreaExists(string name)
        {
            foreach (ChartArea chartArea in chart1.ChartAreas)
            {
                if (chartArea.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public void bieudo(String newSeries)
        {

            // Xóa dữ liệu cũ (nếu có)
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Legends.Clear();
            chart1.Legends.Add("Tiền");
            // Thêm dữ liệu vào biểu đồ
            chart1.Series.Add(newSeries);

            // Cài đặt biểu đồ cột
            chart1.Series[newSeries].ChartType = SeriesChartType.Column;

            // hiện số trên mỗi cột
            chart1.Series[newSeries].IsValueShownAsLabel = true;


            // Vô hiệu hóa vị trí tự động chú thích
            chart1.Legends["Tiền"].Position.Auto = false;

            if (!ChartAreaExists("ThuChi"))
                chart1.ChartAreas.Add("ThuChi");

            //Đặt tên cho trục
            chart1.ChartAreas["ThuChi"].AxisX.Title = "Ngày"; // Đặt tên trục X
            chart1.ChartAreas["ThuChi"].AxisY.Title = "Số tiền"; // Đặt tên trục Y
        }
        private void btnChartChi_Click(object sender, EventArgs e)
        {
            titlechart.Text = "Biểu đồ Chi tiêu";
            btnChartChi.BackColor = Color.Thistle;
            btnChartThu.BackColor = Color.Azure;

            dateTime = DateTime.Now;
            thangChoBieuDo = int.Parse(dateTime.Month.ToString());
            namChoBieuDo = int.Parse(dateTime.Year.ToString());
            DuLieuChartChi(dateTime, thangChoBieuDo, namChoBieuDo);

        }

        private void DuLieuChartChi(DateTime dateTime, int thangChoBieuDo, int namChoBieuDo)
        {
            // Thêm dữ liệu vào biểu đồ
            string series = "chartChi";

            // Xóa dữ liệu cũ (nếu có)
            chart1.Series.Clear();

            DTO_TienChi tc = new DTO_TienChi();

            
            string dateee = dateTime.ToString("yyyy-MM-dd");

            titlechart.Text = "Biểu đồ Chi tiêu " + dateTime.Month + "-" + dateTime.Year;

            tc.Sngay_chi = DateTime.Parse(dateee);
            tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;

            DataTable dt = BUS_BaoCao.LayDS_TienChi(tc);

            if (dt.Rows.Count == 0){}

            bieudo(series);

            DataTable dtreceive = new DataTable();
            // Xóa dữ liệu trước đó nếu có
            dtreceive.Columns.Add("ngay_chi", typeof(int));
            dtreceive.Columns.Add("so_tien", typeof(int));
            dtreceive.Clear();
            // Thêm dữ liệu cho tháng cụ thể
            AddDataForMonth(dtreceive, thangChoBieuDo, namChoBieuDo);


            UpdateChart(CompareDataTables("chi", dtreceive, dt), series, "ngay_chi");
        }

        static void AddDataForMonth(DataTable dataTable, int month, int year)
        {
            // Tính số ngày trong tháng và năm cụ thể
            int daysInMonth = DateTime.DaysInMonth(year, month);

            // Thêm dữ liệu cho từng ngày trong tháng
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime ngayChi = new DateTime(year, month, day);
                int soTien = 0; // Hàm để lấy số tiền cho ngày chi cụ thể
                dataTable.Rows.Add(int.Parse(ngayChi.Day.ToString()), soTien);
            }
        }

        private DataTable CompareDataTables(string ThuOrChi,DataTable tableA, DataTable tableB)
        {
            string sThuOrChi = string.Empty;

            if (ThuOrChi == "thu")
            {
                sThuOrChi = "ngay_thu";
            }
            else if (ThuOrChi == "chi")
            {
                sThuOrChi = "ngay_chi";
            }

            if (tableA.Rows.Count > 0 && tableB.Rows.Count > 0)
            {
                
                int k = 0;
                // Kiểm tra và sao chép dữ liệu từ bảng B vào bảng A nếu có
                foreach (DataRow rowA in tableA.Rows)
                {
                    int ngayA = int.Parse(rowA[sThuOrChi].ToString());

                    foreach (DataRow rowB in tableB.Rows)
                    {
                        int ngayB = int.Parse(rowB[sThuOrChi].ToString());
                        //Console.WriteLine(ngayA + " == "+ ngayB);

                        if (ngayA == ngayB)
                        {
                            for (int i = 0; i < tableB.Rows.Count; i++)
                            {
                                //Sao chép dữ liệu từ bảng B vào bảng A tại hàng hiện tại
                                if (int.Parse(tableA.Rows[k][sThuOrChi].ToString()) == int.Parse(tableB.Rows[i][sThuOrChi].ToString()))
                                {
                                    tableA.Rows[k]["so_tien"] = int.Parse(tableB.Rows[i]["tong_so_tien"].ToString());
                                }
                            }
                        }
                    }
                    k++;
                }
            }
            return tableA;
        }

        // Hàm cập nhật dữ liệu cho biểu đồ
        private void UpdateChart(DataTable dt, string seri, string thuchi)
        {
            chart1.DataSource = dt;
            chart1.Series.Clear(); // Xóa các loại biểu đồ hiện có
            chart1.Series.Add(seri); // Thêm loại biểu đồ mới
            chart1.Series[seri].XValueMember = thuchi; // Trục X sẽ lấy dữ liệu từ cột "Month"
            chart1.Series[seri].YValueMembers = "so_tien"; // Trục Y sẽ lấy dữ liệu từ cột "Sales"
            chart1.DataBind(); // Kích hoạt việc liên kết dữ liệu và vẽ biểu đồ
            chart1.Series[seri].IsValueShownAsLabel = true;
        }

        private void btnChartThu_Click(object sender, EventArgs e)
        {
            
            btnChartChi.BackColor = Color.Azure;
            btnChartThu.BackColor = Color.Thistle;

            dateTime = DateTime.Now;
            thangChoBieuDo = int.Parse(dateTime.Month.ToString());
            namChoBieuDo = int.Parse(dateTime.Year.ToString());
            DuLieuChartThu(dateTime, thangChoBieuDo, namChoBieuDo);
        }

        private void DuLieuChartThu(DateTime dateTime, int thangChoBieuDo, int namChoBieuDo)
        {
            // Thêm dữ liệu vào biểu đồ
            string series = "chartThu";

            // Xóa dữ liệu cũ (nếu có)
            chart1.Series.Clear();
            DTO_TienThu tc = new DTO_TienThu();

            
            string dateee = dateTime.ToString("yyyy-MM-dd");
            titlechart.Text = "Biểu đồ Thu nhập " + dateTime.Month + "-" + dateTime.Year;
            tc.Sngay_thu = DateTime.Parse(dateee);
            tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;

            DataTable dt = BUS_BaoCao.LayDS_TienThu(tc);

            if (dt.Rows.Count == 0)
            {

            }
                

            bieudo(series);

            DataTable dtreceive = new DataTable();
            dtreceive.Columns.Add("ngay_thu", typeof(int));
            dtreceive.Columns.Add("so_tien", typeof(int));
            dtreceive.Clear();

            // Thêm dữ liệu cho tháng cụ thể
            AddDataForMonth(dtreceive, thangChoBieuDo, namChoBieuDo);

            UpdateChart(CompareDataTables("thu", dtreceive, dt), series, "ngay_thu");
        }

        private void btnNhapThuChi_Click(object sender, EventArgs e)
        {
            Form_Main frm = new Form_Main();
            Button btn = frm.btnNhapchitieu;

            ButtonClicked_move_userNhapThuChi?.Invoke(btn, EventArgs.Empty);
        }
        public void addItemUserControlBaoCao_Flowlayout_chi()
        {
            // tham số: id_danh_muc, ngày chi
            DTO_DanhMucChi dmt = new DTO_DanhMucChi();
            dmt.Sten_tai_khoan = TruyenData.Instance.LoginTK;

            DateTime dtime = DateTime.Now;
            DataTable dtIdDanhMucChi = BUS_BaoCao.LayIdDanhMucChi(dmt);


            // Khởi tạo danh sách DTO_TienChi
            List<DTO_TienChi> danhSachTienChi = new List<DTO_TienChi>();



            // Duyệt qua từng dòng trong DataTable
            foreach (DataRow row in dtIdDanhMucChi.Rows)
            {
                // Tạo một đối tượng DTO_TienChi mới
                DTO_TienChi tc = new DTO_TienChi();
                tc.Sngay_chi = dtime;
                // Gán giá trị của cột "id" từ DataTable cho thuộc tính Sid_danh_muc_chi của DTO_TienChi
                //tc.Sid_danh_muc_chi = int.Parse(row["id_danh_muc"].ToString());
                tc.Sid_danh_muc_chi = int.Parse(row["id"].ToString());
                tc.SNameiconUserSet = row["ten_danh_muc_do_nguoi_dung_dat"].ToString();
                tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;
                // Thêm DTO_TienChi vào danh sách
                danhSachTienChi.Add(tc);
            }

            foreach (DTO_TienChi tc in danhSachTienChi)
            {
                // Gán giá trị cho các thuộc tính khác của DTO_TienChi nếu cần
                DataTable dt = BUS_BaoCao.LayDSChi_where_Thang(tc);
                //Console.WriteLine(dt.Rows.Count+ "//////////////");

                foreach (DataRow row in dt.Rows)
                {
                    //string nameicon = BUS_BaoCao.getNameiconFromIdDanhMuc(int.Parse(row["id_danh_muc_chi"].ToString()));
                    string nameicon = BUS_BaoCao.getNameiconFromIdDanhMuc(int.Parse(row["id_danh_muc"].ToString()));


                    ItemListThuChi_BaoCao iconDanhMuc = new ItemListThuChi_BaoCao();
                    iconDanhMuc.Bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(nameicon);
                    iconDanhMuc.NamePic = nameicon;
                    iconDanhMuc.NameDanhMuc = nameicon;
                    iconDanhMuc.NameDoNguoiDungDat = tc.SNameiconUserSet;
                    iconDanhMuc.Sotien = int.Parse(row["tong_so_tien"].ToString());
                    flowLayoutPanel_Chi.Controls.Add(iconDanhMuc);
                }
            }
        }
        public void addItemUserControlBaoCao_Flowlayout_thu()
        {
            // tham số: id_danh_muc, ngày chi

            DTO_DanhMucThu dmt =  new DTO_DanhMucThu();
            dmt.Sten_tai_khoan = TruyenData.Instance.LoginTK;

            DateTime dtime = DateTime.Now;
            DataTable dtIdDanhMucChi = BUS_BaoCao.LayIdDanhMucThu(dmt);


            // Khởi tạo danh sách DTO_TienChi
            List<DTO_TienThu> danhSachTienTHu = new List<DTO_TienThu>();



            // Duyệt qua từng dòng trong DataTable
            foreach (DataRow row in dtIdDanhMucChi.Rows)
            {
                // Tạo một đối tượng DTO_TienChi mới
                DTO_TienThu tc = new DTO_TienThu();
                tc.Sngay_thu = dtime;
                // Gán giá trị của cột "id" từ DataTable cho thuộc tính Sid_danh_muc_chi của DTO_TienChi
                //tc.Sid_danh_muc_thu = int.Parse(row["id_danh_muc"].ToString());
                tc.Sid_danh_muc_thu = int.Parse(row["id"].ToString());
                tc.SNameiconUserSet = row["ten_danh_muc_do_nguoi_dung_dat"].ToString();
                tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;
                // Thêm DTO_TienChi vào danh sách
                danhSachTienTHu.Add(tc);
            }

            foreach (DTO_TienThu tc in danhSachTienTHu)
            {
                // Gán giá trị cho các thuộc tính khác của DTO_TienChi nếu cần
                DataTable dt = BUS_BaoCao.LayDSThu_where_Thang(tc);
                //Console.WriteLine(dt.Rows.Count + "//////////////");

                foreach (DataRow row in dt.Rows)
                {
                    string nameicon = BUS_BaoCao.getNameiconFromIdDanhMuc(int.Parse(row["id_danh_muc"].ToString()));


                    ItemListThuChi_BaoCao iconDanhMuc = new ItemListThuChi_BaoCao();
                    iconDanhMuc.Bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(nameicon);
                    iconDanhMuc.NamePic = nameicon;
                    iconDanhMuc.NameDanhMuc = nameicon;
                    iconDanhMuc.NameDoNguoiDungDat = tc.SNameiconUserSet;
                    iconDanhMuc.Sotien = int.Parse(row["tong_so_tien"].ToString());
                    flowLayoutPanel_Thu.Controls.Add(iconDanhMuc);
                }
            }

        }
        public void ResFlowlayoutPanel()
        {
            
            // Xóa tất cả các điều khiển con trong FlowLayoutPanel
            flowLayoutPanel_Chi.Controls.Clear();

            // Giải phóng tài nguyên của các điều khiển con đã bị xóa
            foreach (Control control in flowLayoutPanel_Chi.Controls)
            {
                control.Dispose();
            }

            // Xóa tất cả các điều khiển con trong FlowLayoutPanel
            flowLayoutPanel_Thu.Controls.Clear();

            // Giải phóng tài nguyên của các điều khiển con đã bị xóa
            foreach (Control control in flowLayoutPanel_Thu.Controls)
            {
                control.Dispose();
            }
        }        
        public void ResDataFlowLayoutPanel()
        {
            // Thực hiện các công việc bạn muốn ở đây
            ResFlowlayoutPanel();
            addItemUserControlBaoCao_Flowlayout_chi();
            addItemUserControlBaoCao_Flowlayout_thu();
            DateTime dateTime = DateTime.Now;
            TongTienChi(dateTime.Month, dateTime.Year);
            TongTienThu(dateTime.Month, dateTime.Year);
            TongSauThuVaChi();

            TongChi_All();
            TongThuNhap_all();
            SauTongThuVaTongChi();
        }

        private void btnBDThangTruoc_Click(object sender, EventArgs e)
        {
            // chi duoc kich hoat
            if (btnChartChi.BackColor != Color.Azure)
            {
                //Console.WriteLine("mmmmm");
                thangChoBieuDo++;
                dateTime = dateTime.AddMonths(-1);

                if (thangChoBieuDo > 12)
                {
                    namChoBieuDo++;
                    thangChoBieuDo = 1;
                }

                DuLieuChartChi(dateTime, thangChoBieuDo, namChoBieuDo);
            }
            else  // thu duoc kich hoat
            {
                //Console.WriteLine("mmmmm");
                thangChoBieuDo++;
                dateTime = dateTime.AddMonths(-1);

                if (thangChoBieuDo > 12)
                {
                    namChoBieuDo++;
                    thangChoBieuDo = 1;
                }

                DuLieuChartThu(dateTime, thangChoBieuDo, namChoBieuDo);
            }


            
        }

        private void btnBDThangSau_Click(object sender, EventArgs e)
        {
            // chi duoc kich hoat
            if (btnChartChi.BackColor != Color.Azure)
            {
               // Console.WriteLine("mmmmm");
                thangChoBieuDo++;
                dateTime = dateTime.AddMonths(1);

                if (thangChoBieuDo > 12)
                {
                    namChoBieuDo++;
                    thangChoBieuDo = 1;
                }

                DuLieuChartChi(dateTime, thangChoBieuDo, namChoBieuDo);
            }
            else  // thu duoc kich hoat
            {
                //Console.WriteLine("mmmmm");
                thangChoBieuDo++;
                dateTime = dateTime.AddMonths(1);

                if (thangChoBieuDo > 12)
                {
                    namChoBieuDo++;
                    thangChoBieuDo = 1;
                }

                DuLieuChartThu(dateTime, thangChoBieuDo, namChoBieuDo);
            }
        }


        private void TongTienChi(int thang, int nam)
        {
            int tongtien = BUS_Lich.TongTienChiTrongThang(thang, nam, TruyenData.Instance.LoginTK);
            string tongtienn = string.Format("{0:#,##0}", tongtien);
            lbchitrongthang.Text = "-" + tongtienn;
        }
        private void TongTienThu(int thang, int nam)
        {
            int tongtien = BUS_Lich.TongTienThuTrongThang(thang, nam, TruyenData.Instance.LoginTK);
            string tongtienn = string.Format("{0:#,##0}", tongtien);
            lbthutrongthang.Text = "+" + tongtienn;
        }

        private void TongSauThuVaChi()
        {
            string tempa = lbthutrongthang.Text;
            string tempb = lbchitrongthang.Text;
            int sa = int.Parse(tempa.Replace(",", "").ToString());
            string sb = tempb.Replace(",", "").ToString();
            sb = sb.Replace("-", "").ToString();
            int sbb = int.Parse(sb);
            int tong = sa - sbb;
            //Console.WriteLine(tong + "="+sa +"+"+ sb);
            string tongtienn = string.Format("{0:#,##0}", tong);
            lbtongthithutrongthang.Text = tongtienn.ToString();
        }


        private void TongChi_All()
        {
            int value = BUS_Lich.TongChiTieu_all(TruyenData.Instance.LoginTK);
            string aa = string.Format("{0:#,##0}", value);
            lbTongChitieu.Text = aa;
        }
        private void TongThuNhap_all()
        {
            int value = BUS_Lich.TongThuNhap_all(TruyenData.Instance.LoginTK);
            string aa = string.Format("{0:#,##0}", value);
            lbTongThunhap.Text = aa;
        }

        private void SauTongThuVaTongChi()
        {
            string aa = lbTongThunhap.Text.Replace(",", "").ToString();
            string bb = lbTongChitieu.Text.Replace(",", "").ToString();
            int cc = int.Parse(aa) - int.Parse(bb);
            string dd = string.Format("{0:#,##0}", cc);
            if (cc > 0)
            {
                dd = "+" + dd;
                lbSauTongThuVaTongChi.ForeColor = Color.Green;
            }
            else
            {
                lbSauTongThuVaTongChi.ForeColor = Color.Red;
            }
            lbSauTongThuVaTongChi.Text = dd;
        }

        
    }
}
