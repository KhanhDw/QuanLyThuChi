using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuChi.ItemList
{
    public partial class ItemListThuChi_BaoCao : UserControl
    {
        private Bitmap bitmap;
        private string namePic;
        private string nameDanhMuc; // do người dùng đặt
        private int sotien;
        private string mota;
        private string nameDoNguoiDungDat;

        public ItemListThuChi_BaoCao()
        {
            InitializeComponent();
        }

        public Bitmap Bitmap { get => bitmap; set => bitmap = value; }
        public string NamePic { get => namePic; set => namePic = value; }// từ resources
        public string NameDanhMuc { get => nameDanhMuc; set => nameDanhMuc = value; } // từ sql danh muc
        public int Sotien { get => sotien; set => sotien = value; }
        public string Mota { get => mota; set => mota = value; }
        public string NameDoNguoiDungDat { get => nameDoNguoiDungDat; set => nameDoNguoiDungDat = value; }


        // Hàm để thêm một Bitmap vào PictureBox
        private void AddBitmapToPictureBox()
        {

            if (IsBitmapEmpty(Bitmap)) { Console.WriteLine("m"); }
            // Xóa hình ảnh hiện tại nếu có
            pictureBox1.Image?.Dispose();

            // Gán hình ảnh mới
            pictureBox1.Image = Bitmap;
            pictureBox1.Name = NamePic;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            lbName.Text = NameDoNguoiDungDat;
            string teinformat = string.Format("{0:0,0}", Sotien);
            lbmoney.Text = teinformat;
        }

        static bool IsBitmapEmpty(Bitmap bitmap)
        {
            return bitmap.Width == 0 && bitmap.Height == 0;
        }

        private void ItemListThuChi_BaoCao_Load(object sender, EventArgs e)
        {
            AddBitmapToPictureBox();
        }
    }
}
