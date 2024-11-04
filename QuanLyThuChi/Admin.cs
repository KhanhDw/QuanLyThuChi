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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyThuChi
{
    public partial class Admin : Form
    {
        string nameadmin = string.Empty;

        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            NameAdmin.Text = TruyenData.Instance.LoginTK;

            UpdateData();

            // Thêm dữ liệu cho ComboBox
            cbbQuyen.Items.Add("--Chọn quyền hạn--");
            cbbQuyen.Items.Add("Quản trị viên");
            cbbQuyen.Items.Add("Người dùng");

            // Chọn mặc định một phần tử trong ComboBox
            cbbQuyen.SelectedIndex = 0;
            // Thiết lập ComboBox thành chỉ đọc
            cbbQuyen.DropDownStyle = ComboBoxStyle.DropDownList;
        }




        private void UpdateData()
        {
            DataTable dt = BUS_Admin.LayDSNguoiDung();

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["id"].HeaderText = "ID";
            dataGridView1.Columns["ten_tai_khoan"].HeaderText = "Tên người dùng";
            dataGridView1.Columns["gmail"].HeaderText = "Gmail";
            dataGridView1.Columns["mat_khau"].HeaderText = "Mật khẩu";
            dataGridView1.Columns["quyen"].HeaderText = "Quyền hạn";



            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Điều chỉnh độ rộng của cột tự động lấp đầy bảng
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Điều chỉnh chiều cao của dòng header tự động
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Điều chỉnh chiều rộng cho tất cả các ô trong dòng
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Chỉnh định dạng cho font của header
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);

            // Chỉnh định dạng cho dữ liệu DataGridView
            dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Regular);

            dataGridView1.AllowUserToAddRows = false;


            // Điều chỉnh dữ liệu vào giữ
            DataGridViewCellStyle dataCellStyle = new DataGridViewCellStyle();
            dataCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Áp dụng điều chỉnh cho tất cả các cột
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle = dataCellStyle;
            }


        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMini_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                txtID.Text = selectedRow.Cells["id"].Value.ToString();
                txtTenNguoiDung.Text = selectedRow.Cells["ten_tai_khoan"].Value.ToString();
                txtGmail.Text = selectedRow.Cells["gmail"].Value.ToString();
                txtMatKhau.Text = selectedRow.Cells["mat_khau"].Value.ToString();

                nameadmin = selectedRow.Cells["ten_tai_khoan"].Value.ToString();

                string quyenn = selectedRow.Cells["quyen"].Value.ToString();

                if (quyenn == "ad")
                {
                    cbbQuyen.SelectedIndex = 1;
                }
                else if (quyenn == "us")
                {
                    cbbQuyen.SelectedIndex = 2;
                }

            }
        }

        private void ClearTextBox()
        {
            txtID.Clear();
            txtTenNguoiDung.Clear();
            txtGmail.Clear();
            txtMatKhau.Clear();
            cbbQuyen.SelectedIndex = 0;
        }

        private void btnRongMucNhap_Click(object sender, EventArgs e)
        {
            ClearTextBox();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            DTO_TaiKhoan tk = new DTO_TaiKhoan();
            //tk.Sid = int.Parse(txtID.Text);
            tk.Sten_tai_khoan = txtTenNguoiDung.Text;
            tk.Sgmail = txtGmail.Text;
            tk.Smat_khau = txtMatKhau.Text;

            object selectedItem = cbbQuyen.SelectedItem;
            string quyenn = selectedItem.ToString();

            Console.WriteLine(quyenn);
            if (quyenn == "Quản trị viên")
            {
                tk.Quyen = "ad";
            }
            else
            {
                tk.Quyen = "us";
            }



            if (BUS_Admin.ThemNguoiDung(tk))
            {
                UpdateData();
                MessageBox.Show("Thêm người dùng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTextBox();
            }
            else
            {
                MessageBox.Show("Thêm người dùng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (NameAdmin.Text == nameadmin)
            {
                NameAdmin.Text = txtTenNguoiDung.Text;
                TruyenData.Instance.LoginTK = txtTenNguoiDung.Text;
            }



            DTO_TaiKhoan tkedit = new DTO_TaiKhoan();
            tkedit.Sid = int.Parse(txtID.Text);
            tkedit.Sten_tai_khoan = txtTenNguoiDung.Text;
            tkedit.Sgmail = txtGmail.Text;
            tkedit.Smat_khau = txtMatKhau.Text;

            object selectedItem = cbbQuyen.SelectedItem;
            string quyenn = selectedItem.ToString();

            Console.WriteLine(quyenn);
            if (quyenn == "Quản trị viên")
            {
                tkedit.Quyen = "ad";
            }
            else
            {
                tkedit.Quyen = "us";
            }



            if (BUS_Admin.SuaNguoiDung(tkedit))
            {
                UpdateData();
                MessageBox.Show("Sửa người dùng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTextBox();
            }
            else
            {
                MessageBox.Show("Sửa người dùng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (NameAdmin.Text == nameadmin)
            {
                MessageBox.Show("Bạn không thể xóa tài khoản hiện tại \n Vì nó đang hoạt động", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DTO_TaiKhoan tk = new DTO_TaiKhoan();
                tk.Sid = int.Parse(txtID.Text);


                if (BUS_Admin.XoaNguoiDung(tk))
                {
                    UpdateData();
                    MessageBox.Show("Xóa người dùng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTextBox();
                }
                else
                {
                    MessageBox.Show("Xóa người dùng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnChuyenQuaGiaoDienNguoiDung_Click(object sender, EventArgs e)
        {
            Form_Main main = new Form_Main();
            main.Show();
            this.Hide();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTImkiem.Text == "")
            {
                MessageBox.Show($"Vui lòng nhập tên người dùng trước khi thực hiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ten = txtTImkiem.Text;
            List<DTO_TaiKhoan> lstnv = BUS_Admin.TimNguoiDung(ten);
            if (lstnv == null)
            {
                MessageBox.Show($"Không tìm thấy người dùng [ {ten} ]", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataGridView1.DataSource = lstnv;
            dataGridView1.Columns["Sid"].HeaderText = "ID";
            dataGridView1.Columns["Sten_tai_khoan"].HeaderText = "Tên người dùng";
            dataGridView1.Columns["Sgmail"].HeaderText = "Gmail";
            dataGridView1.Columns["Smat_khau"].HeaderText = "Mật khẩu";
            dataGridView1.Columns["Quyen"].HeaderText = "Quyền hạn";

            btnMacDinh.Visible = true;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DangNhap main = new DangNhap();
            main.Show();
            this.Hide();
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            UpdateData();
            btnMacDinh.Visible = false;
        }
    }
}
