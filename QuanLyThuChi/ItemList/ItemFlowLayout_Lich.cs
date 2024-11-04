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
    public partial class ItemFlowLayout_Lich : UserControl
    {
        // Định nghĩa một sự kiện để thông báo khi nút được click
        public event EventHandler OnSelect = null;
        private Color sColor;
        private Bitmap bitmap;
        private string namePic;
        private string nameDanhMuc; 
        private int sotien;
        private string mota;
        private int id;
        private bool isThu = false;

        public ItemFlowLayout_Lich()
        {
            InitializeComponent();
        }

        public Color SColor { get => sColor; set => sColor = value; }
        public Bitmap SBitmap { get => bitmap; set => bitmap = value; }
        public string NamePic { get => namePic; set => namePic = value; }
        public string NameDanhMuc { get => nameDanhMuc; set => nameDanhMuc = value; }
        public int Sotien { get => sotien; set => sotien = value; }
        public string Mota { get => mota; set => mota = value; }
        public int Id { get => id; set => id = value; }
        public bool IsThu { get => isThu; set => isThu = value; }

        private void ItemFlowLayout_Lich_Load(object sender, EventArgs e)
        {
            AddBitmapToPictureBox();
            this.BackColor = SColor;
            
            if (IsThu)
            {
                lbmoney.Text = "+"+string.Format("{0:#,##0}", Sotien);
                lbmoney.ForeColor = Color.Blue;
            }
            else
            {
                lbmoney.Text = "-" + string.Format("{0:#,##0}", Sotien);
                lbmoney.ForeColor = Color.Red;
            }


            lbName.Text = NameDanhMuc;
            lbmota.Text = Mota;
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
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }
        static bool IsBitmapEmpty(Bitmap bitmap)
        {
            return bitmap.Width == 0 && bitmap.Height == 0;
        }
    }
}
