using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyThuChi
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();

            txtMatKhau.PasswordChar = '*';
        }


        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenNguoiDung.Text.Trim();
            string password = txtMatKhau.Text;

            DTO_TaiKhoan tk = new DTO_TaiKhoan();

            tk.Sten_tai_khoan = username;
            tk.Smat_khau = password;


            DataTable dt = BUS_DangNhap.XacNhanDangNhap(tk);



            if (dt.Rows.Count > 0)
            {
                TruyenData.Instance.LoginTK = username;
                TruyenData.Instance.LoginMK = password;
                   

                 TruyenData.Instance.LoginGmail = dt.Rows[0]["gmail"].ToString();
                if (BUS_DangNhap.XacNhanQuyen(tk) == "us")
                {
                    
                    TruyenData.Instance.LoginQuyen = "us";
                    Form_Main main = new Form_Main();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    
                    TruyenData.Instance.LoginQuyen = "ad";
                    Admin main = new Admin();
                    main.Show();
                    this.Hide();
                }


            }
            else
            {
                MessageBox.Show("Sai thông tin đăng nhập\nVui lòng nhập lại!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                txtTenNguoiDung.Clear();
                txtMatKhau.Clear();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình không? ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            // Kiểm tra nút mà người dùng đã nhấn.
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Cancel) { }
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

        private void btnDk_Click(object sender, EventArgs e)
        {
            FormDangKy main = new FormDangKy();
            main.Show();
            this.Hide();
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn nút đóng cửa sổ
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Hiển thị thông báo
                Application.Exit();
            }
        }
    }

}
