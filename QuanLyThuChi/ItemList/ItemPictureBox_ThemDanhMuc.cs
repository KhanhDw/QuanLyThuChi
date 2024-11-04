using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyThuChi.ItemList
{
    public partial class ItemPictureBox_ThemDanhMuc : UserControl
    {

        public event EventHandler OnSelect = null;
        private System.Drawing.Image iconDanhMuc;
        private Bitmap bitmap;
        private string namePic;
        private string nameDanhMuc;
        private bool clicked = false;
        //private int idDanhMuc;
        public System.Drawing.Image IconDanhMuc { get => iconDanhMuc; set => iconDanhMuc = value; }
        public Bitmap Bitmap { get => bitmap; set => bitmap = value; }
        public string NamePic { get => namePic; set => namePic = value; }
        public bool Clicked { get => clicked; set => clicked = value; }
        public string NameDanhMuc { get => nameDanhMuc; set => nameDanhMuc = value; }
        //public int IdDanhMuc { get => idDanhMuc; set => idDanhMuc = value; }

        public ItemPictureBox_ThemDanhMuc()
        {
            InitializeComponent();
        }

        private void ItemPictureBox_ThemDanhMuc_Load(object sender, EventArgs e)
        {
            AddBitmapToPictureBox();
        }

        // Hàm để thêm một Bitmap vào PictureBox
        private void AddBitmapToPictureBox()
        {

            if (IsBitmapEmpty(Bitmap)) { Console.WriteLine("m"); }
            // Xóa hình ảnh hiện tại nếu có
            pictureBox_Item.Image?.Dispose();

            // Gán hình ảnh mới
            pictureBox_Item.Image = Bitmap;
            pictureBox_Item.Name = NamePic;
            pictureBox_Item.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_Item.BorderStyle = BorderStyle.FixedSingle;
        }

        static bool IsBitmapEmpty(Bitmap bitmap)
        {
            return bitmap.Width == 0 && bitmap.Height == 0;
        }

        private void pictureBox_Item_Click(object sender, EventArgs e)
        {
            if (OnSelect != null)
            {
                OnSelect.Invoke(this, e);
            }
           
            this.BackColor = Color.Yellow;
        }

        
    }
}
