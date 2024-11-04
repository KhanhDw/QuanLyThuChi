using BUS;
using DTO;
using QuanLyThuChi.ItemList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace QuanLyThuChi
{
    public partial class Lich : UserControl
    {
        int currentMonth;
        int previousMonth;
        int currentYear;
        private bool isThreadRunning = false;

        // Định nghĩa một delegate cho sự kiện truyền giá trị i
        public delegate void ProgressUpdateDelegate(int value);

        // Khai báo sự kiện truyền giá trị i
        public event ProgressUpdateDelegate ProgressUpdate;

        public Lich()
        {
            InitializeComponent();
            flowLayoutPanel1.AutoScroll = true;

            // Đăng ký sự kiện để lắng nghe việc cập nhật giá trị i từ luồng Task
            ProgressUpdate += UpdateProgressBar;
            
        }

        public void Lich_Load(object sender, EventArgs e)
        {

            // part 1
            // Lấy tháng hiện tại
            DateTime currentDate = monthCalendar1.SelectionStart;
            int currentDay = currentDate.Day;
            currentMonth = currentDate.Month;
            currentYear = currentDate.Year;
            previousMonth = currentMonth;
            // Hiển thị tháng hiện tại
            lbThangNam.Text = "Tháng " + currentMonth + " năm " + currentYear;


            // part 2 
            DateTime dateTime = new DateTime(currentYear, currentMonth, 1);
            DayOfWeek dayOfWeek = dateTime.DayOfWeek;
            Console.WriteLine(dayOfWeek);

            // part 3
            LichTuTao(currentYear, currentMonth);


            // part 4
            ActionProcessBarAndBackGroundWorker(currentMonth, currentYear);


            // part 5
            TongTienChi(currentMonth, currentYear);
            TongTienThu(currentMonth, currentYear);
            TongSauThuVaChi();

            //part 6
            TongChi_All();
            TongThuNhap_all();
            SauTongThuVaTongChi();

            //part 7
            lbThang.Text = "Tháng " + currentMonth.ToString();
            label12.Text = "DỮ LIỆU THU CHI - THÁNG " + currentMonth.ToString();
        }

        private void setMaxProcessBar(int currentYear, int currentMonth)
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            

            // do xung đột luồng chính và phụ nên cần phải kiểm tra và đưa dữ liệu về luồng phụ để xử lý
            if (progressBar1.InvokeRequired)
            {
                progressBar1.BeginInvoke((MethodInvoker)(() => { progressBar1.Maximum = daysInMonth; }));
            }
            else
            {
                progressBar1.Maximum = daysInMonth;
            }
        }

        private void ActionProcessBarAndBackGroundWorker(int currentMonth, int currentYear)
        {
            if (!backgroundWorker1.IsBusy)
            {
                progressBar1.Visible = true;
                setMaxProcessBar(currentYear, currentMonth);
                backgroundWorker1.RunWorkerAsync(new Tuple<int, int>(currentMonth, currentYear));
            }
            
            
        }

        private void LichTuTao(int namSelect, int thangSelect)
        {

            foreach (Control control in pnlBtnLich.Controls)
            {
                // Kiểm tra xem control có phải là button và có tên trùng với name không
                if (control is System.Windows.Forms.Button button)
                {
                        button.Enabled = true;
                        button.BackColor = Color.White;
                }
            }



            int[] arrChild = new int[7];
            List<int[]> listThu = new List<int[]> { };
            int maxLength = 0;
            string[] btnList = new string[] { "btnT2D", "btnT3D", "btnT4D", "btnT5D", "btnT6D", "btnT7D", "btnCND" };
            int[] temp = new int[] { };
            int b = 0;
            int[] thu2 = new int[0];
            int[] thu3 = new int[0];
            int[] thu4 = new int[0];
            int[] thu5 = new int[0];
            int[] thu6 = new int[0];
            int[] thu7 = new int[0];
            int[] thuCn = new int[0];

            // Duyệt qua tất cả các controls trong form và gán lại bằng = 0 để bt button khi dùng được
            foreach (Control control in pnlBtnLich.Controls)
            {
                // Kiểm tra xem control có phải là button và có tên trùng với name không
                if (control is System.Windows.Forms.Button button)
                {
                    button.Text = "0";
                }
            }

            int daysInMonth = DateTime.DaysInMonth(namSelect, thangSelect);

            // lấy các ngày thuộc thứ nào cho vào mảng thứ đó
            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime date = new DateTime(namSelect, thangSelect, i);
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    // Tăng kích thước mảng lên một đơn vị
                    Array.Resize(ref thu2, thu2.Length + 1);
                    int index = thu2.Length - 1;
                    thu2[index] = i;
                }
                if (date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    Array.Resize(ref thu3, thu3.Length + 1);
                    int index = thu3.Length - 1;
                    thu3[index] = i;
                }
                if (date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    Array.Resize(ref thu4, thu4.Length + 1);
                    int index = thu4.Length - 1;
                    thu4[index] = i;
                }
                if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    Array.Resize(ref thu5, thu5.Length + 1);
                    int index = thu5.Length - 1;
                    thu5[index] = i;
                }
                if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    Array.Resize(ref thu6, thu6.Length + 1);
                    int index = thu6.Length - 1;
                    thu6[index] = i;
                }
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    Array.Resize(ref thu7, thu7.Length + 1);
                    int index = thu7.Length - 1;
                    thu7[index] = i;
                }
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    Array.Resize(ref thuCn, thuCn.Length + 1);
                    int index = thuCn.Length - 1;
                    thuCn[index] = i;
                }

            }

            listThu.Add(thu2);
            listThu.Add(thu3);
            listThu.Add(thu4);
            listThu.Add(thu5);
            listThu.Add(thu6);
            listThu.Add(thu7);
            listThu.Add(thuCn);

            for (int i = 0; i < 7; i++)
            {

                if (listThu[i][0] == 1)
                {
                    i = 8;
                }
                else
                {
                    //Console.WriteLine(arrChild[i] + "nn");
                    arrChild[i] = listThu[i][0];
                }
            }

            for (int i = 0; i < listThu.Count; i++)
            {
                if (maxLength < listThu[i].Length)
                {
                    maxLength = listThu[i].Length;
                }
            }

            for (int i = 0; i < listThu.Count; i++)
            {
                //Console.WriteLine(listThu[i][0] + " m,m,m " + arrChild[i]);

                if (listThu[i][0] == arrChild[i])//(maxLength > listThu[i].Length)
                {
                    // thêm số 0 vào đầu mảng thứ bằng copy mảng
                    int[] newArray = new int[listThu[i].Length + 1];
                    Array.Copy(listThu[i], 0, newArray, 1, listThu[i].Length);
                    newArray[0] = 0;
                    listThu[i] = newArray;
                    //}
                }
            }

            //foreach (int[] item in listThu)
            //{
            //    for (int i = 0; i < item.Length; i++)
            //    {
            //        Console.Write(item[i] + ", ");
            //    }
            //    Console.WriteLine(" ");
            //}


            

            foreach (string item in btnList)
            {
                if (b == 0) { temp = listThu[0]; }
                else if (b == 1) { temp = listThu[1]; }
                else if (b == 2) { temp = listThu[2]; }
                else if (b == 3) { temp = listThu[3]; }
                else if (b == 4) { temp = listThu[4]; }
                else if (b == 5) { temp = listThu[5]; }
                else if (b == 6) { temp = listThu[6]; }


                // gán ngày cho button
                for (int i = 1; i < 7; i++)
                {
                    System.Windows.Forms.Button a00 = FindButtonByName((item + i).ToString());
                    if (i - 1 < temp.Length)
                    {
                        a00.Text = temp[i - 1].ToString();
                    }
                }
                b++;
            }



            List<int> ngayNhapData = new List<int>();
            int thang;
            int nam;
            // lấy dữ liệu từ server
            DataTable dt = BUS_Lich.LayNgayCoGhiDuLieuThuChi(monthCalendar1.SelectionStart.Month, monthCalendar1.SelectionStart.Year, TruyenData.Instance.LoginTK);
            // Lấy dữ liệu từ DataTable và gán vào biến ngày, tháng, năm
            foreach (DataRow row in dt.Rows)
            {
                DateTime ngayThangNam = (DateTime)row["ngay"];
                int ngay = ngayThangNam.Day;
                thang = ngayThangNam.Month;
                nam = ngayThangNam.Year;

                ngayNhapData.Add(ngay);
                //Console.WriteLine($"Ngay: {ngay}, Thang: {thang}, Nam: {nam}");
            }

            


            foreach (Control control in pnlBtnLich.Controls)
            {
                // Kiểm tra xem control có phải là button và có tên trùng với name không
                if (control is System.Windows.Forms.Button button)
                {
                    if (button.Text == "0")
                    {
                        button.Enabled = false;
                        button.BackColor = Color.Black;
                    }
                    else
                    {
                        button.Enabled = true;
                        button.BackColor = Color.White;

                        foreach (int io in ngayNhapData)
                        {
                            //Console.WriteLine($"Ngay: {io} --- Ngay: {int.Parse(button.Text.ToString())}");
                            if (io == int.Parse(button.Text.ToString()))
                            {
                                button.Enabled = true;
                                button.BackColor = Color.DarkOrange;
                                //Console.WriteLine($"Ngay: {io} +++ Ngay: {int.Parse(button.Text.ToString())}");
                            }
                        }

                        
                    }
                }
            }
        }

        private System.Windows.Forms.Button FindButtonByName(string name)
        {
            // Duyệt qua tất cả các controls trong form
            foreach (Control control in pnlBtnLich.Controls)
            {
                // Kiểm tra xem control có phải là button và có tên trùng với name không
                if (control is System.Windows.Forms.Button button && control.Name == name)
                {
                    // Nếu tìm thấy, trả về button
                    return button;
                }
            }
            // Nếu không tìm thấy, trả về null
            return null;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Kiểm tra xem tháng hiện tại có thay đổi hay không 
            // Cập nhật dữ liệu
            if (monthCalendar1.SelectionStart.Month != previousMonth)
            {
                // Lưu trữ tháng hiện tại
                UpdateData(monthCalendar1.SelectionStart.Month, monthCalendar1.SelectionStart.Year);
                ResFlowlayout();
                addItemFlowLayoutPanel(monthCalendar1.SelectionStart.Month, monthCalendar1.SelectionStart.Year);
                
            }
            else
            {
                UpdateData(monthCalendar1.SelectionStart.Month, monthCalendar1.SelectionStart.Year);
                ResFlowlayout();
                addItemFlowLayoutPanel(monthCalendar1.SelectionStart.Month, monthCalendar1.SelectionStart.Year);

            }
        }

        private void UpdateData(int thang, int nam)
        {
            lbThangNam.Text = "Tháng " + thang + " năm " + nam;

            LichTuTao(nam, thang);
        }

        private void ResFlowlayout()
        {
            // Lặp qua từng điều khiển con trong FlowLayoutPanel
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                // Giải phóng tài nguyên của từng điều khiển con
                control.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();
        }

        private void addItemFlowLayoutPanel(int thang, int nam)
        {

            if (isThreadRunning)
            {
                // Luồng phụ đang chạy, không thực hiện hành động này
                return;
            }
            isThreadRunning = true;


            lbThang.Text = "Tháng " + thang.ToString();
            label12.Text = "DỮ LIỆU THU CHI - THÁNG " + thang.ToString();



            // Gọi phương thức RunWorkerAsync để bắt đầu thực hiện công việc trong luồng phụ
            backgroundWorker1.RunWorkerAsync(new Tuple<int, int>(thang, nam));

        } 

        private void addItemFlowThu(int nam, int thang, int day)
        {
            DateTime date = new DateTime(nam, thang, day);

            string formattedDateTime = date.ToString("yyyy-MM-dd");

            DataTable dt = BUS_NhapThuChi.LayDS_tienthu_Where_NgayThu(formattedDateTime, TruyenData.Instance.LoginTK);


            //// sẽ không hiện lỗi nếu rỗng (erro: object reference)
            if (dt != null)
            {
                List<DTO_TienThu> lstTienChi = new List<DTO_TienThu>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DTO_TienThu nv = new DTO_TienThu();
                    nv.Sid = int.Parse(dt.Rows[i]["id"].ToString());
                    nv.Sid_danh_muc_thu = int.Parse(dt.Rows[i]["id_danh_muc_thu"].ToString());
                    nv.Smo_ta = dt.Rows[i]["mo_ta"].ToString();
                    nv.Sso_tien = int.Parse(dt.Rows[i]["so_tien"].ToString());
                    nv.Sten_tai_khoan = dt.Rows[i]["ten_tai_khoan"].ToString();
                    nv.Sngay_thu = DateTime.Parse(dt.Rows[i]["ngay_thu"].ToString());
                    nv.SNameiconUserSet = BUS_NhapThuChi.getNameIconDoNguoiDungDatFromIdDanhMucThu(nv.Sid_danh_muc_thu, nv.Sten_tai_khoan);

                    lstTienChi.Add(nv);
                }


                foreach (DTO_TienThu itemtienchi in lstTienChi)
                {
                    string nameIcon = BUS_NhapThuChi.getNameiconFromIdDanhMuc(itemtienchi.Sid_danh_muc_thu);


                    ItemFlowLayout_Lich iconDanhMuc = new ItemFlowLayout_Lich();
                    iconDanhMuc.SBitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(nameIcon);
                    iconDanhMuc.Id = itemtienchi.Sid;
                    iconDanhMuc.NamePic = nameIcon;
                    iconDanhMuc.NameDanhMuc = itemtienchi.SNameiconUserSet;
                    iconDanhMuc.Sotien = itemtienchi.Sso_tien;
                    iconDanhMuc.Mota = itemtienchi.Smo_ta;
                    iconDanhMuc.IsThu = true;
                    flowLayoutPanel1.Controls.Add(iconDanhMuc);

                }


            }
        }

        private void addItemFlowChi(int nam, int thang, int day)
        {
            DateTime date = new DateTime(nam, thang, day);

            string formattedDateTime = date.ToString("yyyy-MM-dd");

            DataTable dt = BUS_NhapThuChi.LayDS_tienchi_Where_NgayChi(formattedDateTime, TruyenData.Instance.LoginTK);


            //// sẽ không hiện lỗi nếu rỗng (erro: object reference)
            if (dt != null)
            {
                List<DTO_TienChi> lstTienChi = new List<DTO_TienChi>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DTO_TienChi nv = new DTO_TienChi();
                    nv.Sid = int.Parse(dt.Rows[i]["id"].ToString());
                    nv.Sid_danh_muc_chi = int.Parse(dt.Rows[i]["id_danh_muc_chi"].ToString());
                    nv.Smo_ta = dt.Rows[i]["mo_ta"].ToString();
                    nv.Sso_tien = int.Parse(dt.Rows[i]["so_tien"].ToString());
                    nv.Sten_tai_khoan = dt.Rows[i]["ten_tai_khoan"].ToString();
                    nv.Sngay_chi = DateTime.Parse(dt.Rows[i]["ngay_chi"].ToString());
                    nv.SNameiconUserSet = BUS_NhapThuChi.getNameIconDoNguoiDungDatFromIdDanhMucThu(nv.Sid_danh_muc_chi, nv.Sten_tai_khoan);

                    lstTienChi.Add(nv);
                }


                foreach (DTO_TienChi itemtienchi in lstTienChi)
                {
                    string nameIcon = BUS_NhapThuChi.getNameiconFromIdDanhMuc(itemtienchi.Sid_danh_muc_chi);


                    ItemFlowLayout_Lich iconDanhMuc = new ItemFlowLayout_Lich();
                    iconDanhMuc.SBitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(nameIcon);
                    iconDanhMuc.Id = itemtienchi.Sid;
                    iconDanhMuc.NamePic = nameIcon;
                    iconDanhMuc.NameDanhMuc = itemtienchi.SNameiconUserSet;
                    iconDanhMuc.Sotien = itemtienchi.Sso_tien;
                    iconDanhMuc.Mota = itemtienchi.Smo_ta;
                    iconDanhMuc.IsThu = false;
                    flowLayoutPanel1.Controls.Add(iconDanhMuc);

                }


            }
        }

        private void btnThToi_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            // Lấy ngày hiện tại của MonthCalendar
            DateTime currentDate = monthCalendar1.SelectionStart;

            // Tính toán ngày đầu tiên của tháng tiếp theo
            DateTime nextMonth = currentDate.AddMonths(1);

            // Đặt ngày đầu tiên của tháng tiếp theo làm ngày hiện tại cho MonthCalendar
            monthCalendar1.SelectionStart = nextMonth;
            monthCalendar1.SelectionEnd = nextMonth;

            //

            DateTime ss = monthCalendar1.SelectionEnd;

            TongTienChi(int.Parse(ss.Month.ToString()), int.Parse(ss.Year.ToString()));
            TongTienThu(int.Parse(ss.Month.ToString()), int.Parse(ss.Year.ToString()));
            TongSauThuVaChi();

        }

        private void btnThHT_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            // Lấy ngày hiện tại của hệ thống
            DateTime currentDate = DateTime.Today;

            // Đặt ngày hiện tại làm ngày hiện tại cho MonthCalendar
            monthCalendar1.SetDate(currentDate);

            //
            DateTime ss = monthCalendar1.SelectionStart;

            TongTienChi(int.Parse(ss.Month.ToString()), int.Parse(ss.Year.ToString()));
            TongTienThu(int.Parse(ss.Month.ToString()), int.Parse(ss.Year.ToString()));
            TongSauThuVaChi();
        }

        private void btnThTruoc_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            // Lấy ngày hiện tại của MonthCalendar
            DateTime currentDate = monthCalendar1.SelectionStart;

            // Tính toán ngày đầu tiên của tháng tiếp theo
            DateTime nextMonth = currentDate.AddMonths(-1);

            // Đặt ngày đầu tiên của tháng tiếp theo làm ngày hiện tại cho MonthCalendar
            monthCalendar1.SelectionStart = nextMonth;
            monthCalendar1.SelectionEnd = nextMonth;

            //
            DateTime ss = monthCalendar1.SelectionStart;

            TongTienChi(int.Parse(ss.Month.ToString()), int.Parse(ss.Year.ToString()));
            TongTienThu(int.Parse(ss.Month.ToString()), int.Parse(ss.Year.ToString()));
            TongSauThuVaChi();

        }


        private void TongTienChi(int thang, int nam)
        {
            int tongtien = BUS_Lich.TongTienChiTrongThang(thang, nam, TruyenData.Instance.LoginTK);
            string tongtienn = string.Format("{0:#,##0}", tongtien);
            lbTongTienChi.Text = "-" + tongtienn;
        }
        private void TongTienThu(int thang, int nam)
        {
            int tongtien = BUS_Lich.TongTienThuTrongThang(thang, nam, TruyenData.Instance.LoginTK);
            string tongtienn = string.Format("{0:#,##0}", tongtien);
            lbTongTienThu.Text = "+" + tongtienn;
        }

        private void TongSauThuVaChi()
        {
            string tempa = lbTongTienThu.Text;
            string tempb = lbTongTienChi.Text;
            int sa = int.Parse(tempa.Replace(",", "").ToString());
            string sb = tempb.Replace(",", "").ToString();
            sb = sb.Replace("-", "").ToString();
            int sbb = int.Parse(sb);
            int tong = sa - sbb;
            //Console.WriteLine(tong + "="+sa +"+"+ sb);
            string tongtienn = string.Format("{0:#,##0}", tong);
            lbtongsauthuchi.Text = tongtienn.ToString();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<int, int> arguments = e.Argument as Tuple<int, int>;
            int thang = arguments.Item1;
            int nam = arguments.Item2;

            int daysInMonth = DateTime.DaysInMonth(nam, thang);
            setMaxProcessBar(nam, thang);
            

            Task.Run(() =>
            {
                // Kích hoạt lại nút sau khi hoàn thành
                btnThTruoc.Invoke((MethodInvoker)delegate
                {
                    btnThTruoc.Enabled = false;
                });
                // Kích hoạt lại nút sau khi hoàn thành
                btnThHT.Invoke((MethodInvoker)delegate
                {
                    btnThHT.Enabled = false;
                });
                // Kích hoạt lại nút sau khi hoàn thành
                btnThToi.Invoke((MethodInvoker)delegate
                {
                    btnThToi.Enabled = false;
                });

                // Kích hoạt lại nút sau khi hoàn thành
                flowLayoutPanel1.Invoke((MethodInvoker)delegate
                {
                    flowLayoutPanel1.Visible = false;
                });
                lbTientrinh.Invoke((MethodInvoker)delegate
                {
                    lbTientrinh.Visible = true;

                });

                // Kiểm tra xem nếu label1 cần được truy cập từ một luồng khác
                if (label1.InvokeRequired)
                {
                    // Gọi BeginInvoke để thực thi mã trên luồng giao diện người dùng
                    label1.BeginInvoke((MethodInvoker)delegate ()
                    {
                        // Hiển thị thông báo
                        lbThongBaoLoadData.Text = "Đang lấy dữ liệu, vui lòng chờ vài giây!";
                        lbThongBaoLoadData.Visible = true;
                    });
                }
                else
                {
                    // Trường hợp luồng gọi Invoke là luồng giao diện người dùng,
                    // chúng ta có thể truy cập label1 mà không cần Invoke
                    // Hiển thị thông báo
                    lbThongBaoLoadData.Text = "Đang lấy dữ liệu, vui lòng chờ vài giây!";
                    lbThongBaoLoadData.Visible = true;
                }


                for (int i = 0; i < daysInMonth; i++)
                {
                    Color color = Color.White;

                    color = (i % 2 == 0) ? Color.FromArgb(217, 237, 191) : Color.Lavender;
                   
                    PhanCachNgay_Lich item = new PhanCachNgay_Lich();
                    item.SDate = (i + 1).ToString();
                    item.SMonth = thang.ToString();
                    item.SYear = nam.ToString();
                    item.SColor = color;
                    Thread.Sleep(30);

                    // Thêm phần tử vào flowLayoutPanel1, cần chú ý rằng phải sử dụng phương thức Invoke khi thao tác trên thành phần giao diện từ một luồng khác
                    // Kích hoạt lại nút sau khi hoàn thành
                    flowLayoutPanel1.Invoke((MethodInvoker)delegate
                    {
                        flowLayoutPanel1.Controls.Add(item);

                        addItemFlowThu(nam, thang, i + 1);
                        addItemFlowChi(nam, thang, i + 1);
                    });

                    
                    // Báo cáo tiến trình
                    // Gửi giá trị i về luồng gốc thông qua sự kiện
                    ProgressUpdate?.Invoke(i);
                }

                //flowLayoutPanel1.Visible = true;

                // Kích hoạt lại nút sau khi hoàn thành
                flowLayoutPanel1.Invoke((MethodInvoker)delegate
                {
                    flowLayoutPanel1.Visible = true;
                });
                // Kích hoạt lại nút sau khi hoàn thành
                btnThTruoc.Invoke((MethodInvoker)delegate
                {
                    btnThTruoc.Enabled = true;
                });
                // Kích hoạt lại nút sau khi hoàn thành
                btnThHT.Invoke((MethodInvoker)delegate
                {
                    btnThHT.Enabled = true;
                });
                // Kích hoạt lại nút sau khi hoàn thành
                btnThToi.Invoke((MethodInvoker)delegate
                {
                    btnThToi.Enabled = true;
                });
                // Kích hoạt lại nút sau khi hoàn thành
                lbThongBaoLoadData.Invoke((MethodInvoker)delegate
                {
                    lbThongBaoLoadData.Text = "Tải dữ liệu thành công";
                    lbThongBaoLoadData.ForeColor = Color.Blue;

                });
                // Kích hoạt lại nút sau khi hoàn thành
                progressBar1.Invoke((MethodInvoker)delegate
                {
                    progressBar1.Visible = false;

                });


                isThreadRunning = false;

                Thread.Sleep(1000);

                // Kích hoạt lại nút sau khi hoàn thành
                lbThongBaoLoadData.Invoke((MethodInvoker)delegate
                {
                    lbThongBaoLoadData.Visible = false;

                });// Kích hoạt lại nút sau khi hoàn thành
                lbTientrinh.Invoke((MethodInvoker)delegate
                {
                    lbTientrinh.Visible = false;

                });
            });
        }

        // Phương thức để cập nhật ProgressBar
        private void UpdateProgressBar(int value)
        {
            // do xung đột luồng chính và phụ nên cần phải kiểm tra và đưa dữ liệu về luồng phụ để xử lý
            if (progressBar1.InvokeRequired)
            {
                progressBar1.BeginInvoke((MethodInvoker)(() => { 
                    progressBar1.Value = value;
                    lbTientrinh.Text = "Tiến trình đã xử lý: " + value;
                }));
            }
            else
            {
                progressBar1.Value = value;
            }
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
            if (cc > 0) { 
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
