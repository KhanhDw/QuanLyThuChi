using BUS;
using DTO;
using QuanLyThuChi.ItemList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuChi
{

    public partial class timkiem : UserControl
    {

        private itemListtimkiem itemListtimkiem;

        public event EventHandler ButtonClicked_move_usertimkiem;

        List<String> datatruyendi;


        // Định nghĩa delegate để truyền dữ liệu
        public delegate void TruyenDataNhanThuChi(List<String> data);

        // Sự kiện sử dụng delegate để truyền dữ liệu
        public event TruyenDataNhanThuChi truyenDataNhanThuChi;




        public timkiem()
        {
            InitializeComponent();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
        }

        private void timkiem_Load(object sender, EventArgs e)
        {

            
        }





        private void add_itemListtimkiemChi(string tendanhmuc)
        {
            DataTable dt = BUS_NhapThuChi.LayTatCaTienChi(tendanhmuc , TruyenData.Instance.LoginTK);

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

                    itemListtimkiem = new itemListtimkiem();
                    itemListtimkiem.SBitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(nameIcon);
                    itemListtimkiem.Id = itemtienchi.Sid;
                    itemListtimkiem.NamePic = nameIcon;
                    itemListtimkiem.NameDanhMuc = itemtienchi.SNameiconUserSet;
                    itemListtimkiem.Sotien = itemtienchi.Sso_tien;
                    itemListtimkiem.Mota = itemtienchi.Smo_ta;
                    itemListtimkiem.Ngay = itemtienchi.Sngay_chi;
                    flowLayoutPanel1.Controls.Add(itemListtimkiem);
                    itemListtimkiem.OnSelect += (ss, ee) =>
                    {
                        var monSe = (itemListtimkiem)ss;
                        string txtNhapTien = monSe.Sotien.ToString();
                        //string txtNhapTien = string.Format("{0:0,0}", int.Parse(monSe.Sotien.ToString()));
                        string txtNote = monSe.Mota.ToString();
                        string lbNameIconSelected = monSe.NameDanhMuc;
                        // //btnNhapChi.PerformClick();

                        // //doimau();
                        string namePic = monSe.NamePic;
                        string namdanhmuc = monSe.NameDanhMuc;
                        //// doimauVangchoDanhMuc(monSe.NamePic, monSe.NameDanhMuc);
                        // //btnDelete.Visible = true;
                        // //btnHuyDieuChinh.Visible = true;
                        string btnSubmit = "Điều chỉnh khoản chi";
                        int luuIdThuChiDaTao = monSe.Id;

                        //// //btnThemIcon.Enabled = false;
                        //// //btnDelete.Enabled = true;
                        //// //flowLayoutPanel1.Enabled = false;
                        //// //btnNhapThu.Enabled = false;
                        //// //btnNhapChi.Enabled = false;

                        Form_Main frm = new Form_Main();
                        Button btn = frm.btnNhapchitieu;
                        ButtonClicked_move_usertimkiem?.Invoke(btn, EventArgs.Empty);

                        datatruyendi = new List<string>() { };

                        datatruyendi.Add(itemtienchi.Sngay_chi.ToString());
                        datatruyendi.Add(txtNhapTien);
                        datatruyendi.Add(txtNote);
                        datatruyendi.Add(lbNameIconSelected);
                        datatruyendi.Add(namePic);
                        datatruyendi.Add(btnSubmit);
                        datatruyendi.Add(luuIdThuChiDaTao.ToString());

                        truyenDataNhanThuChi?.Invoke(datatruyendi);
                    };
                    k++;
                }

            }

        }


        private void add_itemListtimkiemThu(string tendanhmuc)
        {

            DataTable dt = BUS_NhapThuChi.LayTatCaTienThu(tendanhmuc, TruyenData.Instance.LoginTK);


            // sẽ không hiện lỗi nếu rỗng (erro: object reference)
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


                int k = 0;
                foreach (DTO_TienThu itemtienchi in lstTienChi)
                {
                    string nameIcon = BUS_NhapThuChi.getNameiconFromIdDanhMuc(itemtienchi.Sid_danh_muc_thu);


                    itemListtimkiem = new itemListtimkiem();
                    itemListtimkiem.SBitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(nameIcon);
                    itemListtimkiem.Id = itemtienchi.Sid;
                    itemListtimkiem.NamePic = nameIcon;
                    itemListtimkiem.NameDanhMuc = itemtienchi.SNameiconUserSet;
                    itemListtimkiem.Sotien = itemtienchi.Sso_tien;
                    itemListtimkiem.Mota = itemtienchi.Smo_ta;
                    itemListtimkiem.IsThu = true;
                    itemListtimkiem.Ngay = itemtienchi.Sngay_thu;
                    flowLayoutPanel2.Controls.Add(itemListtimkiem);
                    itemListtimkiem.OnSelect += (ss, ee) =>
                    {
                        var monSe = (itemListtimkiem)ss;
                        string txtNhapTien = monSe.Sotien.ToString();
                        //string txtNhapTien = string.Format("{0:0,0}", int.Parse(monSe.Sotien.ToString()));
                        string txtNote = monSe.Mota.ToString();
                        string lbNameIconSelected = monSe.NameDanhMuc;
                        // //btnNhapChi.PerformClick();

                        // //doimau();
                        string namePic = monSe.NamePic;
                        string namdanhmuc = monSe.NameDanhMuc;
                        //// doimauVangchoDanhMuc(monSe.NamePic, monSe.NameDanhMuc);
                        // //btnDelete.Visible = true;
                        // //btnHuyDieuChinh.Visible = true;
                        string btnSubmit = "Điều chỉnh khoản thu";
                        int luuIdThuChiDaTao = monSe.Id;

                        //// //btnThemIcon.Enabled = false;
                        //// //btnDelete.Enabled = true;
                        //// //flowLayoutPanel1.Enabled = false;
                        //// //btnNhapThu.Enabled = false;
                        //// //btnNhapChi.Enabled = false;

                        Form_Main frm = new Form_Main();
                        Button btn = frm.btnNhapchitieu;
                        ButtonClicked_move_usertimkiem?.Invoke(btn, EventArgs.Empty);


                        datatruyendi = new List<string>() { };

                        datatruyendi.Add(itemtienchi.Sngay_thu.ToString());
                        datatruyendi.Add(txtNhapTien);
                        datatruyendi.Add(txtNote);
                        datatruyendi.Add(lbNameIconSelected);
                        datatruyendi.Add(namePic);
                        datatruyendi.Add(btnSubmit);
                        datatruyendi.Add(luuIdThuChiDaTao.ToString());

                        truyenDataNhanThuChi?.Invoke(datatruyendi);
                    };
                    k++;
                }

            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {

            // Xóa tất cả các control trong FlowLayoutPanel
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                control.Dispose(); // Giải phóng tài nguyên
            }
            // Xóa tất cả các control trong FlowLayoutPanel
            flowLayoutPanel1.Controls.Clear();
            // Xóa tất cả các control trong FlowLayoutPanel
            foreach (Control control in flowLayoutPanel2.Controls)
            {
                control.Dispose(); // Giải phóng tài nguyên
            }
            // Xóa tất cả các control trong FlowLayoutPanel
            flowLayoutPanel2.Controls.Clear();

            add_itemListtimkiemChi(txtimkiem.Text);
            add_itemListtimkiemThu(txtimkiem.Text);
        }
    }
}
