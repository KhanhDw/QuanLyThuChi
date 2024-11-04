using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace QuanLyThuChi
{
    public partial class FormDangKy : Form
    {
        public FormDangKy()
        {
            InitializeComponent();
            txtMatKhau.PasswordChar = '*';
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            int viTriKyTu = txtGmail.Text.IndexOf('@');
            bool viTriKyTu1 = txtGmail.Text.Contains(".com");

            if (txtTenNguoiDung.Text.Length < 8 && txtTenNguoiDung.Text == "")
            {
                MessageBox.Show("Tên người dùng phải lớn hơn 7 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtGmail.Text == "" || viTriKyTu == -1 || !viTriKyTu1)
            {
                MessageBox.Show("Gmail nhập không đúng định dạng\nĐúng định dạng là phải có '@' và '.com'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtMatKhau.Text.Length < 6 || txtMatKhau.Text == "")
            {
                MessageBox.Show("Mật khẩu phải từ 6 ký tự trở lên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else {

                string chuoi = txtMatKhau.Text;
                Regex hoa = new Regex("[A-Z]");
                Regex thuong = new Regex("[a-z]");
                Regex so = new Regex("[0-9]");
                Regex dacBiet = new Regex("[@$!%*#?&]");

                bool coChuHoa = hoa.IsMatch(chuoi);
                bool coChuThuong = thuong.IsMatch(chuoi);
                bool coSo = so.IsMatch(chuoi);
                bool coKyTuDacBiet = dacBiet.IsMatch(chuoi);

                Console.WriteLine("Có chữ hoa: " + coChuHoa);
                Console.WriteLine("Có chữ thường: " + coChuThuong);
                Console.WriteLine("Có số: " + coSo);
                Console.WriteLine("Có ký tự đặc biệt: " + coKyTuDacBiet);

                if (coChuHoa && coChuThuong && coSo && coKyTuDacBiet) { 

                    DTO_TaiKhoan tk = new DTO_TaiKhoan();
                    tk.Sten_tai_khoan = txtTenNguoiDung.Text;
                    tk.Sgmail = txtGmail.Text;
                    tk.Smat_khau = txtMatKhau.Text;
                    tk.Quyen = "us";

                    if (BUS_Admin.ThemNguoiDung(tk))
                    {
                        MessageBox.Show("Thêm người dùng thành công \nVui lòng đăng nhập tài khoản vừa đăng ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DangNhap main = new DangNhap();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Thêm người dùng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu phải có chữ thường, chữ hoa, số và ký tự đặc biệt @$!%*#?& \n Và phải từ 6 ký tự trở lên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnQuayLaiDangNhap_Click(object sender, EventArgs e)
        {
            DangNhap main = new DangNhap();
            main.Show();
            this.Hide();
        }

        private void FormDangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtMatKhau.PasswordChar = '\0';

            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }
    }
}
