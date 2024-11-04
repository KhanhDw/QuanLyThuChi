using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuChi
{
    public partial class Form_Main : Form
    {


        //biến dùng để xác định cho việc di chuyển form
        private bool isDragging = false;
        private int mouseX;
        private int mouseY;
        private Boolean enable = false;
        private Button currentButton;


        BaoCao userControl_BaoCao ;
        NhapThuChi userControl_NhapThuChi ;
        Lich userControl_Lich;
        TaiKhoan userControl_TaiKhoan;
        timkiem userControl_timkiem;


        private List<UserControl> userControls = new List<UserControl>();
        public Form_Main()
        {
            InitializeComponent();

            userControl_BaoCao = new BaoCao();
            userControl_NhapThuChi = new NhapThuChi();
            userControl_Lich = new Lich();
            userControl_TaiKhoan = new TaiKhoan();
            userControl_timkiem = new timkiem();

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            userControl_BaoCao.ButtonClicked_move_userNhapThuChi += btnNhapchitieu_Click;
            userControl_timkiem.ButtonClicked_move_usertimkiem += btnNhapchitieu_Click;
            userControl_timkiem.truyenDataNhanThuChi += userControl_NhapThuChi.ReceiveData;

            // Đăng ký sự kiện ButtonClickInUserControl của UserControl
            userControl_TaiKhoan.ButtonClickInUserControl += UserControl_ButtonClickInUserControl;
            userControl_TaiKhoan.ButtonClickInUserControl_move_DangNhap += UserControl_ButtonClickInUserControl_DangNhap;
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            // Gọi userControl baocao
            btnBaoCao.PerformClick();
        }

        // Thêm usercontrol vào panel để hiển thị
        private void addUserControlForPanel(UserControl userControl)
        {
            HidePreviousUserControls();

            // Kiểm tra xem UserControl đã tồn tại trong Panel hay chưa
            if (!userControls.Contains(userControl))
            {
                // Thêm UserControl vào list usercontrol
                userControls.Add(userControl);

                //containUserControl.Controls.Clear();
                userControl.Dock = DockStyle.Fill;

                // Thêm UserControl vào panel
                containUserControl.Controls.Add(userControl);

                userControl.BackColor = Color.FromArgb(255, 255, 255);

                userControl.BringToFront();
            }
            else
            {
                userControl.Show();
            }

          

            CheckNhapThuChiClicked(userControl);
            
            
        }

        // Phương thức để ẩn tất cả các UserControl trước đó trong Panel
        private void HidePreviousUserControls()
        {
            foreach (var control in userControls)
            {
                control.Hide();
            }
        }

        private void CheckNhapThuChiClicked(UserControl userControl)
        {
            if (userControl == userControl_NhapThuChi)
            {
                btnNhapchitieu.BackColor = Color.SlateBlue;
                btnNhapchitieu.ForeColor = Color.Honeydew;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                // Nếu cửa sổ đang ở trạng thái bình thường, thì chuyển sang trạng thái phóng to (maximized).
                this.WindowState = FormWindowState.Maximized;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                // Nếu cửa sổ đang ở trạng thái phóng to, thì chuyển về trạng thái bình thường.
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // di chuyển cửa sổ (form)
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (enable)
            {
                this.isDragging = true;
                this.mouseX = e.X;
                this.mouseY = e.Y;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (enable)
            {
                if (isDragging)
                {
                    int deltaX = e.X - this.mouseX;
                    int deltaY = e.Y - this.mouseY;
                    this.Location = new System.Drawing.Point(this.Location.X + deltaX, this.Location.Y + deltaY);
                }
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (enable)
                this.isDragging = false;
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            enable = true;
            MainForm_MouseDown(sender, e);
            enable = false;
        }

        private void btn_MouseMove(object sender, MouseEventArgs e)
        {
            enable = true;
            MainForm_MouseMove(sender, e);
            enable = false;
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            enable = true;
            MainForm_MouseUp(sender, e);
        }
        // đổi màu button cửa sổ đang bật
        private void ActivateButton(object btnSender)
        {
            if (btnSender is Button)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    //currentButton.BackColor = Color.FromArgb(0, 119, 179);
                    currentButton.BackColor = Color.SlateBlue;
                    currentButton.ForeColor = Color.Honeydew;
                }
            }
        }
        // trả lại màu cũ khi button không còn được kích hoạt
        private void DisableButton()
        {
            foreach (Control previousBtn in LeftBarControl.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.ForeColor = Color.Black;
                    previousBtn.BackColor = Color.AliceBlue;
                }
            }
        }
        // button tổng quan đổi màu và kích hoạt hiện form user control khi được lick vào
        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            addUserControlForPanel(userControl_BaoCao);
            userControl_BaoCao.ResDataFlowLayoutPanel();
        }
        public void btnNhapchitieu_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            addUserControlForPanel(userControl_NhapThuChi);
        }
        private async void btnLich_Click(object sender, EventArgs e)
        {
            
            ActivateButton(sender);
            addUserControlForPanel(userControl_Lich);
            userControl_Lich.Lich_Load(sender, e);

            // Tắt nút sau khi được nhấn
            btnBaoCao.Enabled = false;
            btnNhapchitieu.Enabled = false;
            btnTaiKhoan.Enabled = false;
            btntimkiem.Enabled = false;

            // Chờ 2 giây
            await Task.Delay(2300);

            // Kích hoạt lại nút sau khi đã trôi qua 2 giây
            btnBaoCao.Enabled = true;
            btnNhapchitieu.Enabled = true;
            btnTaiKhoan.Enabled = true;
            btntimkiem.Enabled = true;
        }
        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            addUserControlForPanel(userControl_TaiKhoan);
        }

        private void UserControl_ButtonClickInUserControl(object sender, EventArgs e)
        {
            // Khi nút trong UserControl được nhấn, ẩn Form A và hiển thị Form B
            Admin formB = new Admin();
            formB.Show();
            this.Hide();
        }
        private void UserControl_ButtonClickInUserControl_DangNhap(object sender, EventArgs e)
        {
            // Khi nút trong UserControl được nhấn, ẩn Form A và hiển thị Form B
            DangNhap formB = new DangNhap();
            formB.Show();
            this.Hide();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            addUserControlForPanel(userControl_timkiem);
        }
    }
}
