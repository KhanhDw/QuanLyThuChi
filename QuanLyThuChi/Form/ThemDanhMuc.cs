using BUS;
using DTO;
using QuanLyThuChi.ItemList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace QuanLyThuChi
{
    public partial class ThemDanhMuc : Form
    {

        // Định nghĩa sự kiện UserControlClosed
        public event EventHandler UserControlClosed;

        private string saveNameIconSelected;
        private string namePicSelected;
        private string namePicContain;
        private string nameFlowlayout;
        public string NamePicSelected
        { get => namePicSelected; set
            {
                if (namePicSelected != value)
                {
                    namePicSelected = value;
                }
            }
        }
        public string NamePicContain
        { get => namePicContain; set
            {
                if (namePicContain != value)
                {
                    namePicContain = value;
                }
            }
        }
        public string NameFlowlayout
        {
            get => nameFlowlayout;
            set {
                if (nameFlowlayout != value)
                {
                    nameFlowlayout = value;
                }
            }
        }

        public ThemDanhMuc()
        {
            InitializeComponent();
            this.MaximizeBox = false;

            flowLayoutPanel_contain.Name = "flowLayoutPanel_contain";
            flowLayoutPanel_select.Name = "flowLayoutPanel_select";

            // Thiết lập Interval cho Timer thành 900 milliseconds (0.9 giây)
            timer1.Interval = 900;
        }
        private void ThemDanhMuc_Load(object sender, EventArgs e)
        {

            btnThemChi.PerformClick();
            flowLayoutPanel_select.AutoScroll = true;
            flowLayoutPanel_contain.AutoScroll = true;
            HienThiIconDanhMuc();

        }

        private void HienThiIconDanhMuc()
        {
            string[] NameIcon = BUS_NhapThuChi.LayDSIcon_DanhMuc();

            for (int i = 0; i < NameIcon.Length; i++)
            {
                AddPictureBoxToFlowlayoutPanel(NameIcon[i]);
            }
        }
        private void HienThiIconDanhMuc_Chi()
        {
            List<DTO_DanhMucChi> NameIcon = BUS_NhapThuChi.LayDSIcon_DanhMuc_Chi(TruyenData.Instance.LoginTK);

            foreach (DTO_DanhMucChi dmt in NameIcon)
            {
                AddPictureBoxToFlowlayoutPanel_Selected(dmt.SNameIconSystems, dmt.SNameiconUserSet);
            }
        }
        private void HienThiIconDanhMuc_Thu()
        {
           

            List<DTO_DanhMucThu> NameIcon = BUS_NhapThuChi.LayDSIcon_DanhMuc_Thu(TruyenData.Instance.LoginTK);


            foreach (DTO_DanhMucThu dmt in NameIcon)
            {
                AddPictureBoxToFlowlayoutPanel_Selected( dmt.SNameIconSystems, dmt.SNameiconUserSet);
            }
        }

        private void ResetFlowlayoutPanel(string danhmuc)
        {
            // Xóa tất cả các control trong FlowLayoutPanel
            foreach (Control control in flowLayoutPanel_select.Controls)
            {
                control.Dispose(); // Giải phóng tài nguyên
            }
            // Xóa tất cả các control trong FlowLayoutPanel
            flowLayoutPanel_select.Controls.Clear();
            if (danhmuc == "chi")
                HienThiIconDanhMuc_Chi();
            else{ HienThiIconDanhMuc_Thu(); }
        }

        private void HienThiIconDanhMuc_Thu_VuaThem()
        {
            //string[,] NameIcon = BUS_NhapThuChi.LayDSIcon_DanhMuc_Thu();
            //AddPictureBoxToFlowlayoutPanel_Selected(NameIcon[NameIcon.Length - 1]);
            //AddPictureBoxToFlowlayoutPanel_Selected(NameIcon[NameIcon.GetLength(0) - 1, 0], NameIcon[NameIcon.GetLength(0) - 1, 1]);

            List<DTO_DanhMucThu> NameIcon = BUS_NhapThuChi.LayDSIcon_DanhMuc_Thu(TruyenData.Instance.LoginTK);

            // Kiểm tra xem danh sách có phần tử nào không
            if (NameIcon.Count > 0)
            {
                // Lấy phần tử cuối cùng
                DTO_DanhMucThu lastItem = NameIcon[NameIcon.Count - 1];
                //Console.WriteLine("Last item: " + lastItem);
                AddPictureBoxToFlowlayoutPanel_Selected(lastItem.SNameIconSystems, lastItem.SNameiconUserSet);
            }
            else
            {
                Console.WriteLine("The list is empty.");
            }

        }
        private void HienThiIconDanhMuc_Chi_VuaThem()
        {
            List<DTO_DanhMucChi> NameIcon = BUS_NhapThuChi.LayDSIcon_DanhMuc_Chi(TruyenData.Instance.LoginTK);

            // Kiểm tra xem danh sách có phần tử nào không
            if (NameIcon.Count > 0)
            {
                // Lấy phần tử cuối cùng
                DTO_DanhMucChi lastItem = NameIcon[NameIcon.Count - 1];
                //Console.WriteLine("Last item: " + lastItem);
                AddPictureBoxToFlowlayoutPanel_Selected(lastItem.SNameIconSystems, lastItem.SNameiconUserSet);
            }
            else
            {
                Console.WriteLine("The list is empty.");
            }
        }
        
        private void btnThemChi_Click(object sender, EventArgs e)
        {
            btnThemDanhMuc.Text = "Thêm vào danh mục chi";
            btnThemChi.BackColor = SystemColors.MenuHighlight;
            btnThemChi.ForeColor = SystemColors.HighlightText;

            btnThemThu.BackColor = SystemColors.Control;
            btnThemThu.ForeColor = SystemColors.ControlText;

            ResetFlowlayoutPanel("chi");
        }
        private void btnThemThu_Click(object sender, EventArgs e)
        {
            btnThemDanhMuc.Text = "Thêm vào danh mục thu";

            btnThemThu.BackColor = SystemColors.MenuHighlight;
            btnThemThu.ForeColor = SystemColors.HighlightText;

            btnThemChi.BackColor = SystemColors.Control;
            btnThemChi.ForeColor = SystemColors.ControlText;

            ResetFlowlayoutPanel("thu");
        }
       
        private void doimau(FlowLayoutPanel flpnl)
        {
            foreach (Control control in flpnl.Controls)
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
        private void AddPictureBoxToFlowlayoutPanel(string nameIconDanhMuc)
        {
            var imageName = nameIconDanhMuc.Substring(0, nameIconDanhMuc.Length - 4);

            var iconDanhMuc = new ItemPictureBox_ThemDanhMuc();
            iconDanhMuc.Bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);
            iconDanhMuc.Width = 40;
            iconDanhMuc.Height = 40;
            iconDanhMuc.NamePic = imageName;
            iconDanhMuc.pictureBox_Item.Click += UserControl_in_flowLayoutPanel_contain_Click;/////////
            iconDanhMuc.Margin = new Padding(2);
            flowLayoutPanel_contain.Controls.Add(iconDanhMuc);

            iconDanhMuc.OnSelect += (ss, ee) =>
            {
                var monSe = (ItemPictureBox_ThemDanhMuc)ss;
                string namePic = monSe.NamePic;
                NamePicContain = namePic;
                monSe.Clicked = true;
                doimau(flowLayoutPanel_contain);
            };
        }
        private void AddPictureBoxToFlowlayoutPanel_Selected(string nameIconDanhMuc, string nameDoNguoiDungDat)
        {
            var imageName = nameIconDanhMuc.Substring(0, nameIconDanhMuc.Length - 4);

            var iconDanhMuc = new ItemPictureBox_ThemDanhMuc();
            iconDanhMuc.Bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);
            iconDanhMuc.Width = 40;
            iconDanhMuc.Height = 40;
            iconDanhMuc.NamePic = imageName;
            iconDanhMuc.Margin = new Padding(2);
            iconDanhMuc.NameDanhMuc = nameDoNguoiDungDat;
            iconDanhMuc.pictureBox_Item.Click += UserControl_in_flowLayoutPanel_select_Click;/////////
            flowLayoutPanel_select.Controls.Add(iconDanhMuc);

            iconDanhMuc.OnSelect += (ss, ee) =>
            {
                var monSe = (ItemPictureBox_ThemDanhMuc)ss;
                string namePic = monSe.NamePic;
                NamePicSelected = namePic;
                txtTenDanhMucDangChon.Text = monSe.NameDanhMuc;
                saveNameIconSelected = monSe.NameDanhMuc;
                monSe.Clicked = true;
                doimau(flowLayoutPanel_select);
            };
        }
        
        private void UserControl_in_flowLayoutPanel_select_Click(object sender, EventArgs e)
        {
            //Nếu click qua flowlayout contain thì các icon đã chọn trong contain hiện tại sẽ đổi lại màu trắng hết
            //MessageBox.Show("Bạn đã click vào "+ NamePicSelected + " trong " + flowLayoutPanel_select.Name);
            if (NameFlowlayout != flowLayoutPanel_select.Name)
            {
                doimau(flowLayoutPanel_contain);
            }

            NameFlowlayout = flowLayoutPanel_select.Name;
        }
        private void UserControl_in_flowLayoutPanel_contain_Click(object sender, EventArgs e)
        {
            // Nếu click qua flowlayout select thì các icon đã chọn trong contain hiện tại sẽ đổi lại màu trắng hết
            if (NameFlowlayout != flowLayoutPanel_contain.Name)
            {
                doimau(flowLayoutPanel_select);
            }
            NameFlowlayout = flowLayoutPanel_contain.Name;
        }


        private void btnThemDanhMuc_Click(object sender, EventArgs e)
        {
            if (NameFlowlayout == flowLayoutPanel_contain.Name) {
                if (txtDatTenDanhMuc.Text == string.Empty)
                {
                    MessageBox.Show("Vui lòng đặt tên cho danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDatTenDanhMuc.Focus();
                    return;
                }
                if (btnThemDanhMuc.Text == "Thêm vào danh mục chi") {
                    if (!BUS_ThemDanhMuc.KiemTraNameBieuTuongNguoiDungNhapTonTaiTrongDanhMucChi(txtDatTenDanhMuc.Text, TruyenData.Instance.LoginTK)) {
                        if (BUS_ThemDanhMuc.ThemIconDanhChi(NamePicContain, txtDatTenDanhMuc.Text.ToString(), TruyenData.Instance.LoginTK))
                        {
                            //MessageBox.Show("Thêm Icon thành công!");
                            lbThongbao.Visible = true;
                            lbThongbao.Text = "Thêm Icon thành công!";
                            timer1.Start();
                            HienThiIconDanhMuc_Chi_VuaThem();
                            txtDatTenDanhMuc.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Thêm biểu tượng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //lbThongbao.Text = "Thêm Icon thành công!";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã dùng tên này để đặt cho biểu tượng trước đó \nVui lòng đặt tên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!BUS_ThemDanhMuc.KiemTraNameBieuTuongNguoiDungNhapTonTaiTrongDanhMucThu(txtDatTenDanhMuc.Text, TruyenData.Instance.LoginTK))
                    {
                        if (BUS_ThemDanhMuc.ThemIconDanhThu(NamePicContain, txtDatTenDanhMuc.Text.ToString(), TruyenData.Instance.LoginTK))
                        {
                            //MessageBox.Show("Thêm Icon thành công!");
                            lbThongbao.Visible = true;
                            lbThongbao.Text = "Thêm Icon thành công!";
                            timer1.Start();
                            HienThiIconDanhMuc_Thu_VuaThem();
                            txtDatTenDanhMuc.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Thêm biểu tượng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã dùng tên này để đặt cho biểu tượng trước đó \nVui lòng đặt tên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 biểu tượng trong [danh sách biểu tượng]", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Khi nút đóng được nhấp, kích hoạt sự kiện UserControlClosed
            UserControlClosed?.Invoke(this, EventArgs.Empty);
            this.Dispose(); // Đóng UserControl
            //this.Close();
        }
        private void btnXoaDanhMuc_Click(object sender, EventArgs e)
        {
            if (NameFlowlayout == flowLayoutPanel_select.Name)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa không?", "Xóa danh mục", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Kiểm tra người dùng đã chọn Yes hay không
                if (result == DialogResult.Yes)
                {
                    if (btnThemDanhMuc.Text == "Thêm vào danh mục chi")
                    {
                        if (BUS_ThemDanhMuc.XoaIconDanhChi(saveNameIconSelected, TruyenData.Instance.LoginTK))
                        {
                            ResetFlowlayoutPanel("chi");
                            NamePicSelected = string.Empty;
                            lbThongbao.Visible = true;
                            lbThongbao.Text = "Xóa thành công!";
                            timer1.Start();
                            //MessageBox.Show("Xóa thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại! \nVì danh mục hiện tại đang được sử dụng.\nBạn chỉ được thay đổi sang danh mục khác.");
                        }
                    }
                    else
                    {
                        if (BUS_ThemDanhMuc.XoaIconDanhThu(saveNameIconSelected, TruyenData.Instance.LoginTK))
                        {
                            ResetFlowlayoutPanel("thu");
                            NamePicSelected = string.Empty;
                            lbThongbao.Visible = true;
                            lbThongbao.Text = "Xóa thành công!";
                            timer1.Start();
                            //MessageBox.Show("Xóa thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 biểu tượng trong [danh sách biểu tượng đã chọn]", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Ẩn Label khi đếm ngược kết thúc
            lbThongbao.Visible = false;

            // Dừng Timer
            timer1.Stop();
        }

        private void btnDieuChinhTenDanhMucDaChon_Click(object sender, EventArgs e)
        {
            if (NameFlowlayout != flowLayoutPanel_select.Name)
            {
                MessageBox.Show("Vui lòng chọn danh mục trong [danh mục đã chọn] để chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtTenDanhMucDangChon.ReadOnly = false;
            txtTenDanhMucDangChon.Focus();
            btnLuuChinhSua.Visible = true;
        }



        private void btnSuaDanhMuc_Click(object sender, EventArgs e)
        {
                btnLuuChinhSua.Visible = true;
                btnThemDanhMuc.Enabled = false;
                btnXoaDanhMuc.Enabled = false;
                btnThemChi.Enabled = false;
                btnThemThu.Enabled = false;
                txtTenDanhMucDangChon.ReadOnly = false;
                btnSuaDanhMuc.Enabled = false;
                btnHuyCHinhSua.Visible = true;

        }

        private void btnLuuChinhSua_Click(object sender, EventArgs e)
        {
            btnHuyCHinhSua.Visible = false;
            btnLuuChinhSua.Visible = false;
            btnThemDanhMuc.Enabled = true;
            btnXoaDanhMuc.Enabled = true;
            btnThemChi.Enabled = true;
            btnThemThu.Enabled = true;
            btnSuaDanhMuc.Enabled = true;

            txtTenDanhMucDangChon.ReadOnly = true;
            lbThongbao.Visible = true;
            lbThongbao.Text = "Đã lưu thay đổi";
            timer1.Start();


            if (NameFlowlayout == flowLayoutPanel_select.Name)
            {

                if (txtTenDanhMucDangChon.Text == string.Empty)
                {
                    MessageBox.Show("Không được để trống \nVui lòng đặt tên cho danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenDanhMucDangChon.Focus();
                    return;
                }

                if (btnThemDanhMuc.Text == "Thêm vào danh mục chi")
                {
                    DTO_DanhMucChi dmc = new DTO_DanhMucChi();
                    //dmc.Sid_danh_muc = ;
                    dmc.Sten_tai_khoan = TruyenData.Instance.LoginTK;
                    dmc.Sten_danh_muc_do_nguoi_dung_dat = saveNameIconSelected;
                    


                    //chỉ thay đổi được name của danh mục chưa thay đổi được danh mục 
                    if (BUS_ThemDanhMuc.SuaIconDanhMucChi(dmc, txtTenDanhMucDangChon.Text))
                    {
                        lbThongbao.Visible = true;
                        lbThongbao.Text = "Sửa thành công!";
                        timer1.Start();
                    }
                    else
                    {
                        lbThongbao.Visible = true;
                        lbThongbao.Text = "Sửa thất bại!";
                        timer1.Start();
                    }
                }
                else
                {
                    DTO_DanhMucThu dmt = new DTO_DanhMucThu();
                    //dmc.Sid_danh_muc = ;
                    dmt.Sten_tai_khoan = TruyenData.Instance.LoginTK;
                    dmt.Sten_danh_muc_do_nguoi_dung_dat = saveNameIconSelected;

                    if (BUS_ThemDanhMuc.SuaIconDanhMucThu(dmt, txtTenDanhMucDangChon.Text))
                    {
                        lbThongbao.Visible = true;
                        lbThongbao.Text = "Sửa thành công!";
                        timer1.Start();
                    }
                    else
                    {
                        lbThongbao.Visible = true;
                        lbThongbao.Text = "Sửa thất bại!";
                        timer1.Start();
                    }
                }
            }
            else if (NameFlowlayout == flowLayoutPanel_contain.Name)
            {
                if (txtTenDanhMucDangChon.Text == string.Empty)
                {
                    MessageBox.Show("Không được để trống \nVui lòng đặt tên cho danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenDanhMucDangChon.Focus();
                    return;
                }
                if (btnThemDanhMuc.Text == "Thêm vào danh mục chi")
                {
                    DTO_DanhMucChi dmc = new DTO_DanhMucChi();
                    dmc.Sten_tai_khoan = TruyenData.Instance.LoginTK;
                    dmc.Sten_danh_muc_do_nguoi_dung_dat = saveNameIconSelected;
                    dmc.SNameIconSystems = NamePicContain;


                    //chỉ thay đổi được name của danh mục chưa thay đổi được danh mục 
                    if (BUS_ThemDanhMuc.SuaIconDanhMucChiAndNameDanhMuc(dmc, txtTenDanhMucDangChon.Text))
                    {
                        ResetFlowlayoutPanel("chi");
                        lbThongbao.Visible = true;
                        lbThongbao.Text = "Sửa thành công!";
                        timer1.Start();
                    }
                    else
                    {
                        lbThongbao.Visible = true;
                        lbThongbao.Text = "Sửa thất bại!";
                        timer1.Start();
                    }
                }
                else
                {
                    DTO_DanhMucThu dmt = new DTO_DanhMucThu();
                    dmt.Sten_tai_khoan = TruyenData.Instance.LoginTK;
                    dmt.Sten_danh_muc_do_nguoi_dung_dat = saveNameIconSelected;
                    dmt.SNameIconSystems = NamePicContain;


                    //chỉ thay đổi được name của danh mục chưa thay đổi được danh mục 
                    if (BUS_ThemDanhMuc.SuaIconDanhMucThuAndNameDanhMuc(dmt, txtTenDanhMucDangChon.Text))
                    {
                        ResetFlowlayoutPanel("thu");
                        lbThongbao.Visible = true;
                        lbThongbao.Text = "Sửa thành công!";
                        timer1.Start();
                    }
                    else
                    {
                        lbThongbao.Visible = true;
                        lbThongbao.Text = "Sửa thất bại!";
                        timer1.Start();
                    }
                    //}
                }
            }
            else
            {
                MessageBox.Show("Lỗi!!!!!!!!!!!!");
            }
        }

        private void btnHuyCHinhSua_Click(object sender, EventArgs e)
        {
            btnLuuChinhSua.Visible = false;
            btnThemDanhMuc.Enabled = true;
            btnXoaDanhMuc.Enabled = true;
            btnThemChi.Enabled = true;
            btnThemThu.Enabled = true;
            txtTenDanhMucDangChon.ReadOnly = true;
            btnSuaDanhMuc.Enabled = true;
            btnHuyCHinhSua.Visible = false;

            
        }
    }
}
