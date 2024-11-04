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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyThuChi
{
    public partial class TaiKhoan : UserControl
    {

        public event EventHandler ButtonClickInUserControl;
        public event EventHandler ButtonClickInUserControl_move_DangNhap;
        public TaiKhoan()
        {
            InitializeComponent();
            // Ẩn mật khẩu ban đầu
            txtMatKhau.PasswordChar = '*';
        }

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            txtTenTaiKhoan.Text = TruyenData.Instance.LoginTK;
            txtMatKhau.Text = TruyenData.Instance.LoginMK;
            txtGmail.Text = TruyenData.Instance.LoginGmail;


            if (TruyenData.Instance.LoginQuyen == "ad")
            {
                btnReturnAdmin.Visible = true;
            }
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            btnHuy.Visible = true;
            btnSAVE.Visible = true;
            btnDoiMK.Enabled = false;

            txtTenTaiKhoan.ReadOnly = false;
            txtMatKhau.ReadOnly = false;
            txtGmail.ReadOnly = false;
        }

        private void btnSAVE_Click(object sender, EventArgs e)
        {
            btnHuy.Visible = false;
            btnSAVE.Visible = false;
            btnDoiMK.Enabled = true;

            txtTenTaiKhoan.ReadOnly = true;
            txtMatKhau.ReadOnly = true;
            txtGmail.ReadOnly = true;

            DTO_TaiKhoan user = new DTO_TaiKhoan();
            user.Sten_tai_khoan = TruyenData.Instance.LoginTK; 
            user.Smat_khau =  TruyenData.Instance.LoginMK;
            user.Sgmail = TruyenData.Instance.LoginGmail;


            DTO_TaiKhoan tkedit = new DTO_TaiKhoan();
            tkedit.Sten_tai_khoan = txtTenTaiKhoan.Text;
            tkedit.Smat_khau = txtMatKhau.Text;
            tkedit.Sgmail = txtGmail.Text;

            if (!BUS_TaiKhoan.SuaNguoiDung(tkedit, user))
            {
                MessageBox.Show("Sửa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Visible = false;
            btnSAVE.Visible = false;
            btnDoiMK.Enabled = true;

            txtTenTaiKhoan.ReadOnly = true;
            txtMatKhau.ReadOnly = true;
            txtGmail.ReadOnly = true;
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

        



        private void btnReturnAdmin_Click(object sender, EventArgs e)
        {
            // Khi nút trong UserControl được nhấn, phát ra sự kiện
            ButtonClickInUserControl?.Invoke(this, EventArgs.Empty);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Khi nút trong UserControl được nhấn, phát ra sự kiện
            ButtonClickInUserControl_move_DangNhap?.Invoke(this, EventArgs.Empty);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, System.IO.Path.Combine(Application.StartupPath, "help.chm"));
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form giao diện mới
            ReportChiTieu childForm = new ReportChiTieu();

            // Hiển thị form giao diện mới
            childForm.Show();
        }
    }
}
