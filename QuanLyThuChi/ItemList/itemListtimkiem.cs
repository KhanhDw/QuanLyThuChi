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
    public partial class itemListtimkiem : UserControl
    {
        // Định nghĩa một sự kiện để thông báo khi nút được click
        public event EventHandler OnSelect = null;
        private bool isThu = false;

        private Bitmap bitmap;
        private string namePic;
        private string nameDanhMuc; // do người dùng đặt
        private int sotien;
        private string mota;
        private int id;
        private DateTime ngay;

        public Bitmap SBitmap { get => bitmap; set => bitmap = value; }
        public string NamePic { get => namePic; set => namePic = value; }
        public string NameDanhMuc { get => nameDanhMuc; set => nameDanhMuc = value; }
        public int Sotien { get => sotien; set => sotien = value; }
        public string Mota { get => mota; set => mota = value; }
        public bool IsThu { get => isThu; set => isThu = value; }
        public int Id { get => id; set => id = value; }
        public DateTime Ngay { get => ngay; set => ngay = value; }

        public itemListtimkiem()
        {
            InitializeComponent();
            lbmota.Text = "";
            lbmota.AutoSize = false; 
            lbmota.AutoEllipsis = true;
        }


        private void itemListtimkiem_Load(object sender, EventArgs e)
        {
            AddBitmapToPictureBox();

            lbName.Text = NameDanhMuc;
            lbmota.Text = Mota;

            string ss = String.Format("{0:0,0}", Sotien);
            lbmoney.Text = ss;

            lbngay.Text = Ngay.ToString("dd/MM/yyyy");

            laThu();
        }

        // Hàm để thêm một Bitmap vào PictureBox
        private void AddBitmapToPictureBox()
        {

            if (IsBitmapEmpty(SBitmap)) { Console.WriteLine("ảnh thêm NULL"); }
            // Xóa hình ảnh hiện tại nếu có
            pictureBox1.Image?.Dispose();

            // Gán hình ảnh mới
            pictureBox1.Image = SBitmap;
            pictureBox1.Name = NamePic;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }
        static bool IsBitmapEmpty(Bitmap bitmap)
        {
            return bitmap.Width == 0 && bitmap.Height == 0;
        }


        private void laThu()
        {
            if (IsThu)
            {
                this.BackColor = Color.LightSkyBlue;
                string ss = String.Format("{0:0,0}", Sotien);
                lbmoney.Text = "+" + ss;
            }
            else
            {
                this.BackColor = Color.LightCoral;
                string ss = String.Format("{0:0,0}", Sotien);
                lbmoney.Text = "-" + ss;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OnSelect?.Invoke(this, e);
        }
    }
}
    