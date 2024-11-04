using BUS;
using DTO;
using QuanLyThuChi.ItemList;
using QuanLyThuChi.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyThuChi
{
    public partial class NhapThuChi : UserControl
    {
        public event EventHandler ButtonClick;
        string nameIconSystem;
        itemListNhapThuChi iconDanhMuc;
        int luuIdThuChiDaTao = -1;

       


        public NhapThuChi()
        {
            InitializeComponent();
            boxPictureFlow.AutoScroll = true;
            flowLayoutPanel1.AutoScroll = true;
            //"Giá trị mặc định";

        }

        
      


        private void NhapThuChi_Load(object sender, EventArgs e)
        {
            btnNhapChi.PerformClick();
            //HienThiIconDanhMucChi();
            ResetFlowlayoutPanel("chi");

            LayThuTuBoxDate();
            HienThiNgayThangNamTrongDateTimePicker();

            txtNhapTien.Focus();

            DateTime dateTime = DateTime.Now;

            string date = dateTime.ToString("dd-MM-yyy");
            groupBox3.Text += "  -  "+date;

            //addItemListNhapThuChiTrongNgay(dateTime);

            txtNhapTien.MaxLength = 11;

            addItemListNhapThuChiTrongNgay(dateTime);
            addItemListNhapThuThuTrongNgay(dateTime);

            TongTienChi(dateTime.Month, dateTime.Year);
            TongTienThu(dateTime.Month, dateTime.Year);
            TongSauThuVaChi();

            TongChi_All();
            TongThuNhap_all();
            SauTongThuVaTongChi();



        }


      



        private void HienThiIconDanhMucChi()
        {
            List<DTO_DanhMucChi> NameIcon = BUS_NhapThuChi.LayDSIcon_DanhMuc_Chi(TruyenData.Instance.LoginTK);

            foreach (DTO_DanhMucChi dmt in NameIcon)
            {
                AddPictureBoxToFlowlayoutPanel(dmt.SNameIconSystems, dmt.SNameiconUserSet);
            }
        }
        private void HienThiIconDanhMucThu()
        {
            List<DTO_DanhMucThu> NameIcon = BUS_NhapThuChi.LayDSIcon_DanhMuc_Thu(TruyenData.Instance.LoginTK);

            foreach (DTO_DanhMucThu dmt in NameIcon)
            {
                AddPictureBoxToFlowlayoutPanel(dmt.SNameIconSystems, dmt.SNameiconUserSet);
            }
        }
        private void AddPictureBoxToFlowlayoutPanel(string nameIconDanhMuc, string nameDoNguoiDungdat)
        {
            var imageName = nameIconDanhMuc.Substring(0, nameIconDanhMuc.Length - 4);

            var iconDanhMuc = new ItemPictureBox_ThemDanhMuc();
            iconDanhMuc.Bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);
            iconDanhMuc.Width = 40;
            iconDanhMuc.Height = 40;
            iconDanhMuc.NamePic = imageName;
            iconDanhMuc.Margin = new Padding(3);
            iconDanhMuc.NameDanhMuc = nameDoNguoiDungdat;
            boxPictureFlow.Controls.Add(iconDanhMuc);

            iconDanhMuc.OnSelect += (ss, ee) =>
            {
                var monSe = (ItemPictureBox_ThemDanhMuc)ss;
                string namePic = monSe.NamePic;
                monSe.Clicked = true;
                //Console.WriteLine(monSe.NameDanhMuc+"..");
                lbNameIconSelected.Text = monSe.NameDanhMuc;
                nameIconSystem = monSe.NamePic;
                doimau();
                
            };
        }
        private void doimau()
        {
            foreach (Control control in boxPictureFlow.Controls)
            {
                if (control is ItemPictureBox_ThemDanhMuc userControl)
                {
                    if (control.BackColor == Color.Yellow)
                    {
                        control.BackColor = Color.White;
                    }
                }
            }
        }
        private void LayThuTuBoxDate()
        {

            List<String> thus = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            if (dateTimePicker1.Value.DayOfWeek.ToString() == thus[0])
            {
                txtThu.Text = "Thứ 2";
            }
            else if (dateTimePicker1.Value.DayOfWeek.ToString() == thus[1])
            {
                txtThu.Text = "Thứ 3";
            }
            else if (dateTimePicker1.Value.DayOfWeek.ToString() == thus[2])
            {
                txtThu.Text = "Thứ 4";
            }
            else if (dateTimePicker1.Value.DayOfWeek.ToString() == thus[3])
            {
                txtThu.Text = "Thứ 5";
            }
            else if (dateTimePicker1.Value.DayOfWeek.ToString() == thus[4])
            {
                txtThu.Text = "Thứ 6";
            }
            else if (dateTimePicker1.Value.DayOfWeek.ToString() == thus[5])
            {
                txtThu.Text = "Thứ 7";
            }
            else if (dateTimePicker1.Value.DayOfWeek.ToString() == thus[6])
            {
                txtThu.Text = "Chủ Nhật";
            }
        }
        private void btnNhapChi_Click(object sender, EventArgs e)
        {
            btnNhapChi.BackColor = Color.LightGreen;
            btnNhapThu.BackColor = Color.White;
            btnSubmit.Text = "Nhập Khoản Chi";
            lbchithu.Text = "Danh mục chi";
            ResetFlowlayoutPanel("chi");
            
            

        }
        private void btnNhapThu_Click(object sender, EventArgs e)
        {
            btnNhapChi.BackColor = Color.White;
            btnNhapThu.BackColor = Color.LightGreen;
            btnSubmit.Text = "Nhập Khoản Thu";
            lbchithu.Text = "Danh mục thu";
            ResetFlowlayoutPanel("thu");
        }
        private void btnTangDay_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
            DateTime ngayDuocChon = dateTimePicker1.Value.Date;
            string ngayDuocChonString = ngayDuocChon.ToString("dd-MM-yyyy"); // Định dạng ngày tháng theo dd/MM/yyyy
            groupBox3.Text = "Danh Sách Thu Chi Trong Ngày - " + ngayDuocChonString;
            ResetFlowlayoutPanelThuCHiTrongNgay();
            addItemListNhapThuChiTrongNgay(ngayDuocChon);
            addItemListNhapThuThuTrongNgay(ngayDuocChon);

        }
        private void btnGiamDay_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
            DateTime ngayDuocChon = dateTimePicker1.Value.Date;
            string ngayDuocChonString = ngayDuocChon.ToString("dd-MM-yyyy"); // Định dạng ngày tháng theo dd/MM/yyyy
            groupBox3.Text = "Danh Sách Thu Chi Trong Ngày - " + ngayDuocChonString;

            ResetFlowlayoutPanelThuCHiTrongNgay();

            addItemListNhapThuChiTrongNgay(ngayDuocChon);
            addItemListNhapThuThuTrongNgay(ngayDuocChon);
        }
        private void HienThiNgayThangNamTrongDateTimePicker()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LayThuTuBoxDate();

            DateTime ngayDuocChon = dateTimePicker1.Value.Date;
            string ngayDuocChonString = ngayDuocChon.ToString("dd-MM-yyyy"); // Định dạng ngày tháng theo dd/MM/yyyy
            groupBox3.Text = "Danh Sách Thu Chi Trong Ngày - " + ngayDuocChonString;

            ResetFlowlayoutPanelThuCHiTrongNgay();

            addItemListNhapThuChiTrongNgay(ngayDuocChon);
            addItemListNhapThuThuTrongNgay(ngayDuocChon);
        }
        private void btnThemIcon_Click(object sender, EventArgs e)
        {
            ThemDanhMuc fThemDanhMuc = new ThemDanhMuc();
            fThemDanhMuc.UserControlClosed += fThemDanhMuc_UserControlClosed;
            fThemDanhMuc.ShowDialog();
        }
        private void fThemDanhMuc_UserControlClosed(object sender, EventArgs e)
        {
            // Xử lý khi User Control đã đóng
            //MessageBox.Show("User Control đã đóng.");
            if (lbchithu.Text == "Danh mục chi"){
                ResetFlowlayoutPanel("chi");
                DateTime ngayDuocChon = dateTimePicker1.Value.Date;
                ResetFlowlayoutPanelThuCHiTrongNgay();
                addItemListNhapThuThuTrongNgay(ngayDuocChon);
                addItemListNhapThuChiTrongNgay(ngayDuocChon);
            }
            else { ResetFlowlayoutPanel("thu");
                DateTime ngayDuocChon = dateTimePicker1.Value.Date;
                ResetFlowlayoutPanelThuCHiTrongNgay();
                addItemListNhapThuThuTrongNgay(ngayDuocChon);
                addItemListNhapThuChiTrongNgay(ngayDuocChon);
            }
        }
        private void btnHienTai_Click(object sender, EventArgs e)
        {
            // Lấy ngày hiện tại
            DateTime currentDate = DateTime.Now;

            // Gán ngày hiện tại cho DateTimePicker
            dateTimePicker1.Value = currentDate;

            DateTime ngayDuocChon = dateTimePicker1.Value.Date;
            string ngayDuocChonString = ngayDuocChon.ToString("dd-MM-yyyy"); // Định dạng ngày tháng theo dd/MM/yyyy
            groupBox3.Text = "Danh Sách Thu Chi Trong Ngày - " + ngayDuocChonString;
        }
        private void ResetFlowlayoutPanel(string turnThuChi)
        {
            // Xóa tất cả các control trong FlowLayoutPanel
            foreach (Control control in boxPictureFlow.Controls)
            {
                control.Dispose(); // Giải phóng tài nguyên
            }
            // Xóa tất cả các control trong FlowLayoutPanel
            boxPictureFlow.Controls.Clear();

            if (turnThuChi == "chi")
            {
                HienThiIconDanhMucChi();
                
            }
            else{ 
                HienThiIconDanhMucThu();
                
            }
        }
        private void ResetFlowlayoutPanelThuCHiTrongNgay()
        {
            // Xóa tất cả các control trong FlowLayoutPanel
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                control.Dispose(); // Giải phóng tài nguyên
            }
            // Xóa tất cả các control trong FlowLayoutPanel
            flowLayoutPanel1.Controls.Clear();

            
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            DateTime ngayDuocChon = dateTimePicker1.Value.Date;
            if (btnSubmit.Text == "Nhập Khoản Chi")
            {
                ResetFlowlayoutPanelThuCHiTrongNgay();
                NhapChi();
                addItemListNhapThuThuTrongNgay(ngayDuocChon);
                addItemListNhapThuChiTrongNgay(ngayDuocChon);
                ResetFlowlayoutPanel("chi");
            }
            else if (btnSubmit.Text == "Nhập Khoản Thu")
            {
                ResetFlowlayoutPanelThuCHiTrongNgay();
                NhapThu();
                addItemListNhapThuChiTrongNgay(ngayDuocChon);
                addItemListNhapThuThuTrongNgay(ngayDuocChon);
                ResetFlowlayoutPanel("thu");

            }
            else if(btnSubmit.Text == "Điều chỉnh khoản chi")
            {
                ResetFlowlayoutPanelThuCHiTrongNgay();
                CapNhatKhoanChi();
                addItemListNhapThuThuTrongNgay(ngayDuocChon);
                addItemListNhapThuChiTrongNgay(ngayDuocChon);
                btnSubmit.Text = "Nhập Khoản Chi";
                luuIdThuChiDaTao = -1;
                doimau();
                btnHuyDieuChinh.Visible = false;
                btnDelete.Visible = false;
            }
            else if (btnSubmit.Text == "Điều chỉnh khoản thu")
            {
                ResetFlowlayoutPanelThuCHiTrongNgay();
                CapNhatKhoanThu();
                addItemListNhapThuThuTrongNgay(ngayDuocChon);
                addItemListNhapThuChiTrongNgay(ngayDuocChon);
                btnSubmit.Text = "Nhập Khoản Thu";
                luuIdThuChiDaTao = -1;
                doimau();
                btnHuyDieuChinh.Visible = false;
                btnDelete.Visible = false;
            }

            txtNhapTien.Clear();
            txtNote.Clear();
            lbNameIconSelected.Text = string.Empty;
            // Kích hoạt sự kiện khi button được click
            ButtonClick?.Invoke(this, EventArgs.Empty);

            TongTienChi(ngayDuocChon.Month, ngayDuocChon.Year);
            TongTienThu(ngayDuocChon.Month, ngayDuocChon.Year);
            TongSauThuVaChi();

            TongChi_All();
            TongThuNhap_all();
            SauTongThuVaTongChi();

            btnNhapChi.Enabled = true;
            btnNhapThu.Enabled = true;

            btnThemIcon.Enabled = true;
            flowLayoutPanel1.Enabled = true;
        }

        private void CapNhatKhoanThu()
        {
            DTO_TienThu tc = new DTO_TienThu();
            
            if (luuIdThuChiDaTao != -1)
            {
                Console.WriteLine(luuIdThuChiDaTao+ "khác -1 nè thu");
            }

           
            tc.Sid = luuIdThuChiDaTao;
            string chuoiDaLoaiBoDauPhay = txtNhapTien.Text.Replace(",", "");
            tc.Sso_tien = int.Parse(chuoiDaLoaiBoDauPhay);
            tc.Smo_ta = txtNote.Text;
            tc.SNameiconUserSet = lbNameIconSelected.Text;
            DateTime dateTime = dateTimePicker1.Value;
            tc.Sngay_thu = DateTime.Parse(dateTime.ToShortDateString());
            tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;

            DataTable dt = BUS_NhapThuChi.LayIDDanhMucTuNameIconThu(tc.SNameiconUserSet , tc.Sten_tai_khoan);
            tc.Sid_danh_muc_thu = int.Parse(dt.Rows[0]["id"].ToString());
            tc.SNameIconSystems = nameIconSystem;

            Console.WriteLine("["+ tc.Smo_ta + " : " + tc.Sso_tien + " : " + tc.SNameiconUserSet + " : " + tc.Sid_danh_muc_thu + "]");

            if (BUS_NhapThuChi.CapNhatTienThu(tc))
            {
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CapNhatKhoanChi()
        {
            DTO_TienChi tc = new DTO_TienChi();
            
            if (luuIdThuChiDaTao != -1)
            {
                Console.WriteLine(luuIdThuChiDaTao);
            }

           
            tc.Sid = luuIdThuChiDaTao;
            string chuoiDaLoaiBoDauPhay = txtNhapTien.Text.Replace(",", "");
            tc.Sso_tien = int.Parse(chuoiDaLoaiBoDauPhay);
            tc.Smo_ta = txtNote.Text;
            tc.SNameiconUserSet = lbNameIconSelected.Text;
            DateTime dateTime = dateTimePicker1.Value;
            tc.Sngay_chi = DateTime.Parse(dateTime.ToShortDateString());
            tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;


            DataTable dt = BUS_NhapThuChi.LayIDDanhMucTuNameIconChi(tc.SNameiconUserSet, tc.Sten_tai_khoan);
            tc.Sid_danh_muc_chi = int.Parse(dt.Rows[0]["id"].ToString());
            //tc.Sid_danh_muc_chi = int.Parse(dt.Rows[0]["id_danh_muc"].ToString());
            tc.SNameIconSystems = nameIconSystem;

            //Console.WriteLine("["+tc.SNameiconUserSet + " fdfhkdjsfhd:  "+ tc.Sid_danh_muc_chi+ "]");

            if (BUS_NhapThuChi.CapNhatTienChi(tc))
            {
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NhapThu()
        {
            if (txtNhapTien.Text == string.Empty)
            {
                MessageBox.Show("Số tiền không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (nameIconSystem == string.Empty)
            {
                MessageBox.Show("Chưa chọn icon danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            DTO_TienThu tc = new DTO_TienThu();

            DateTime dateTime = dateTimePicker1.Value;
            tc.Sngay_thu = DateTime.Parse(dateTime.ToShortDateString());
            tc.Smo_ta = txtNote.Text;
            string chuoiDaLoaiBoDauPhay = txtNhapTien.Text.Replace(",", "");
            tc.Sso_tien = int.Parse(chuoiDaLoaiBoDauPhay);
            //tc.Sid_danh_muc_chi 
            tc.SNameiconUserSet = lbNameIconSelected.Text;
            tc.SNameIconSystems = nameIconSystem;
            tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;

            DTO_DanhMucThu dmt = new DTO_DanhMucThu();
            dmt.Sten_tai_khoan = TruyenData.Instance.LoginTK;
            dmt.Sten_danh_muc_do_nguoi_dung_dat = lbNameIconSelected.Text;

            if (BUS_NhapThuChi.ThemTienThu(tc, dmt))
            {
                MessageBox.Show("Thêm dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void NhapChi()
        {
            if (txtNhapTien.Text == string.Empty)
            {
                MessageBox.Show("Số tiền không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }else if (nameIconSystem == string.Empty)
            {
                MessageBox.Show("Chưa chọn icon danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            DTO_TienChi tc = new DTO_TienChi();

            DateTime dateTime = dateTimePicker1.Value;
            tc.Sngay_chi = DateTime.Parse(dateTime.ToShortDateString());
            tc.Smo_ta = txtNote.Text;
            string chuoiDaLoaiBoDauPhay = txtNhapTien.Text.Replace(",", "");
            tc.Sso_tien = int.Parse(chuoiDaLoaiBoDauPhay);
            //tc.Sid_danh_muc_chi 
            tc.SNameiconUserSet= lbNameIconSelected.Text;
            tc.SNameIconSystems = nameIconSystem;
            tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;

            DTO_DanhMucChi dmc = new DTO_DanhMucChi();
            dmc.Sten_tai_khoan = TruyenData.Instance.LoginTK;
            dmc.Sten_danh_muc_do_nguoi_dung_dat = lbNameIconSelected.Text;

            if (BUS_NhapThuChi.ThemTienChi(tc, dmc))
            {
                MessageBox.Show("Thêm dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtNhapTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }


        }
        private void txtNhapTien_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox
            string inputText = txtNhapTien.Text;

            // Loại bỏ dấu phẩy nếu có
            inputText = inputText.Replace(",", "");

            // Kiểm tra xem có phải là số không
            if (!int.TryParse(inputText, out int number))
            {
                // Nếu không phải số, không thực hiện gì cả
                return;
            }

            // Sử dụng string.Format để định dạng số với dấu phẩy
            string formattedText = string.Format("{0:#,##0}", number);

            // Gán giá trị đã định dạng vào TextBox
            txtNhapTien.Text = formattedText;

            // Di chuyển con trỏ về cuối TextBox
            txtNhapTien.SelectionStart = txtNhapTien.Text.Length;

            

        }
        private void addItemListNhapThuChiTrongNgay(DateTime date)
        {
            DateTime dateTime = date;

            string formattedDateTime = dateTime.ToString("yyyy-MM-dd");

            Console.WriteLine(formattedDateTime);

            DataTable dt = BUS_NhapThuChi.LayDS_tienchi_Where_NgayChi(formattedDateTime, TruyenData.Instance.LoginTK);


            // sẽ không hiện lỗi nếu rỗng (erro: object reference)

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

                   nv.SNameiconUserSet = BUS_NhapThuChi.getNameIconDoNguoiDungDatFromIdDanhMucChi(nv.Sid_danh_muc_chi, nv.Sten_tai_khoan);
                    lstTienChi.Add(nv);
                }

                int k = 0;
                foreach (DTO_TienChi itemtienchi in lstTienChi)
                {
                    string nameIcon = BUS_NhapThuChi.getNameiconFromIdDanhMuc(itemtienchi.Sid_danh_muc_chi);
                   
                    iconDanhMuc = new itemListNhapThuChi();
                    iconDanhMuc.SBitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(nameIcon);
                    iconDanhMuc.Id = itemtienchi.Sid;
                    iconDanhMuc.NamePic = nameIcon;
                    iconDanhMuc.NameDanhMuc = itemtienchi.SNameiconUserSet;
                    iconDanhMuc.Sotien = itemtienchi.Sso_tien;
                    iconDanhMuc.Mota = itemtienchi.Smo_ta;

                    flowLayoutPanel1.Controls.Add(iconDanhMuc);
                    iconDanhMuc.OnSelect += (ss, ee) =>
                    {
                        var monSe = (itemListNhapThuChi)ss;
                        txtNhapTien.Text = string.Format("{0:0,0}", int.Parse(monSe.Sotien.ToString()));
                        txtNote.Text = monSe.Mota.ToString();
                        lbNameIconSelected.Text = monSe.NameDanhMuc;
                        btnNhapChi.PerformClick();
                        
                        doimau();
                        doimauVangchoDanhMuc(monSe.NamePic, monSe.NameDanhMuc);
                        btnDelete.Visible = true;
                        btnHuyDieuChinh.Visible = true;
                        btnSubmit.Text = "Điều chỉnh khoản chi";
                        luuIdThuChiDaTao = monSe.Id;

                        btnThemIcon.Enabled = false;
                        btnDelete.Enabled = true;
                        flowLayoutPanel1.Enabled = false;
                        btnNhapThu.Enabled = false;
                        btnNhapChi.Enabled = false;



                        

                    };
                    k++;
                }

            }

            //flowLayoutPanel1.Controls.Add(item);
        }
        private void addItemListNhapThuThuTrongNgay(DateTime date)
        {
            
            DateTime dateTime = date;

            string formattedDateTime = dateTime.ToString("yyyy-MM-dd");

            Console.WriteLine(formattedDateTime);

            DataTable dt = BUS_NhapThuChi.LayDS_tienthu_Where_NgayThu(formattedDateTime, TruyenData.Instance.LoginTK);


            // sẽ không hiện lỗi nếu rỗng (erro: object reference)
            if (dt != null){
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


                int k = 0;
                foreach (DTO_TienThu itemtienchi in lstTienChi)
                {
                    string nameIcon = BUS_NhapThuChi.getNameiconFromIdDanhMuc(itemtienchi.Sid_danh_muc_thu);


                    iconDanhMuc = new itemListNhapThuChi();
                    iconDanhMuc.SBitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(nameIcon);
                    iconDanhMuc.Id = itemtienchi.Sid;
                    iconDanhMuc.NamePic = nameIcon;
                    iconDanhMuc.NameDanhMuc = itemtienchi.SNameiconUserSet;
                    iconDanhMuc.Sotien = itemtienchi.Sso_tien;
                    iconDanhMuc.Mota = itemtienchi.Smo_ta;
                    iconDanhMuc.IsThu = true;
                    flowLayoutPanel1.Controls.Add(iconDanhMuc);
                    iconDanhMuc.OnSelect += (ss, ee) =>
                    {
                        var monSe = (itemListNhapThuChi)ss;
                        txtNhapTien.Text = string.Format("{0:0,0}", int.Parse(monSe.Sotien.ToString()));
                        txtNote.Text = monSe.Mota.ToString();
                        lbNameIconSelected.Text = monSe.NameDanhMuc;
                        btnNhapThu.PerformClick();
                        btnNhapChi.Enabled = false;
                        doimau();
                        doimauVangchoDanhMuc(monSe.NamePic, monSe.NameDanhMuc);
                        btnHuyDieuChinh.Visible = true;
                        btnSubmit.Text = "Điều chỉnh khoản thu";
                        luuIdThuChiDaTao = monSe.Id;

                        btnThemIcon.Enabled = false;
                        btnDelete.Enabled = true; 
                        flowLayoutPanel1.Enabled = false;
                        btnNhapThu.Enabled = false;
                        btnNhapChi.Enabled = false;
                    };
                    k++;
                }

            }


           
            //flowLayoutPanel1.Controls.Add(item);
        }


        private void doimauVangchoDanhMuc(string namePiic, string namedanhmuc )
        {
            foreach (Control control in boxPictureFlow.Controls)
            {
                if (control is ItemPictureBox_ThemDanhMuc)
                {
                    ItemPictureBox_ThemDanhMuc userControl = (ItemPictureBox_ThemDanhMuc)control;

                    // Tương tác với các thành phần bên trong UserControl
                    // Gán giá trị cho thuộc tính public của UserControl
                   
                    if (userControl.NamePic == namePiic && userControl.NameDanhMuc == namedanhmuc)
                    {
                        control.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void btnHuyDieuChinh_Click(object sender, EventArgs e)
        {
            btnThemIcon.Enabled = true;
            btnNhapChi.Enabled = true;
            btnNhapThu.Enabled = true;
            btnDelete.Enabled = false;
            btnHuyDieuChinh.Visible = false;
            flowLayoutPanel1.Enabled = true;

            doimau();
            lbNameIconSelected.Text = string.Empty;
            txtNote.Text = string.Empty;
            txtNhapTien.Text = string.Empty;
            if(btnSubmit.Text == "Điều chỉnh khoản chi")
            {
                btnSubmit.Text = "Nhập Khoản Chi";
            }
            else
            {
                btnSubmit.Text = "Nhập Khoản Thu";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            doimau();
            txtNhapTien.Clear();
            txtNote.Clear();

            btnThemIcon.Enabled = true;
            btnNhapChi.Enabled = true;
            btnNhapThu.Enabled = true;
            btnDelete.Enabled = false;
            btnHuyDieuChinh.Visible = false;
            flowLayoutPanel1.Enabled = true;

            if (btnSubmit.Text == "Điều chỉnh khoản chi")
            {
                DTO_TienChi tc = new DTO_TienChi();

                tc.Sid = luuIdThuChiDaTao;
                DateTime dateTime = dateTimePicker1.Value;
                DateTime ngayDuocChon = dateTimePicker1.Value.Date;
                tc.Sngay_chi = DateTime.Parse(dateTime.ToShortDateString());
                tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;


                if (BUS_NhapThuChi.XoaMucChi_where_NgayChi(tc))
                {
                    ResetFlowlayoutPanelThuCHiTrongNgay();
                    addItemListNhapThuThuTrongNgay(ngayDuocChon);
                    addItemListNhapThuChiTrongNgay(ngayDuocChon);
                    MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (btnSubmit.Text == "Điều chỉnh khoản thu")
            {
                DTO_TienThu tc = new DTO_TienThu();

                tc.Sid = luuIdThuChiDaTao;
                DateTime dateTime = dateTimePicker1.Value;
                DateTime ngayDuocChon = dateTimePicker1.Value.Date;
                tc.Sngay_thu = DateTime.Parse(dateTime.ToShortDateString());
                tc.Sten_tai_khoan = TruyenData.Instance.LoginTK;

                if (BUS_NhapThuChi.XoaMucChi_where_NgayThu(tc))
                {
                    ResetFlowlayoutPanelThuCHiTrongNgay();
                    addItemListNhapThuThuTrongNgay(ngayDuocChon);
                    addItemListNhapThuChiTrongNgay(ngayDuocChon);
                    MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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


        public void ReceiveData(List<String> data)
        {

            btnThemIcon.Enabled = true;
            btnDelete.Enabled = true;
            flowLayoutPanel1.Enabled = true;
            btnNhapThu.Enabled = true;
            btnNhapChi.Enabled = true;


            dateTimePicker1.Value = DateTime.Parse(data[0]);
            txtNote.Text = data[2];

            string chuoiDaLoaiBoDauPhay = data[1].Replace(",", "");
            txtNhapTien.Text = chuoiDaLoaiBoDauPhay;

           
            lbNameIconSelected.Text = data[3];
            nameIconSystem = data[4];
            //btnSubmit.Text = data[5];


            if (data[5] == "Điều chỉnh khoản chi")
            {
                btnSubmit.Text = data[5];
                btnNhapChi.PerformClick();
                doimau();
                doimauVangchoDanhMuc(nameIconSystem, lbNameIconSelected.Text);
                btnHuyDieuChinh.Visible = true;
                luuIdThuChiDaTao = int.Parse(data[6]);

                btnThemIcon.Enabled = false;
                btnDelete.Enabled = true;
                flowLayoutPanel1.Enabled = false;
                btnNhapThu.Enabled = false;
                btnNhapChi.Enabled = false;
            }
            else //if (data[5] == "Điều chỉnh khoản thu")
            {
                btnSubmit.Text = data[5];
                btnNhapThu.PerformClick();
                doimau();
                doimauVangchoDanhMuc(nameIconSystem, lbNameIconSelected.Text);
                btnHuyDieuChinh.Visible = true;
                luuIdThuChiDaTao = int.Parse(data[6]);

                btnThemIcon.Enabled = false;
                btnDelete.Enabled = true;
                flowLayoutPanel1.Enabled = false;
                btnNhapThu.Enabled = false;
                btnNhapChi.Enabled = false;
            }
            
        }   

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {
            // Kiểm tra xem vị trí click có nằm trong phần số của DateTimePicker hay không
            if (e.X > dateTimePicker1.Width - 26 ) // 25 là chiều rộng của nút chọn ngày bên phải
            {
                // Cho phép click vào nút chọn ngày bên phải
                dateTimePicker1.Select();
            }
            else
            {
                // Ngăn chặn click vào phần số của DateTimePicker
                dateTimePicker1.Enabled = false;
                dateTimePicker1.Enabled = true; // Đặt lại trạng thái enabled để cập nhật giao diện
            }
        }
    }
}
