using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace QuanLyThuChi.ItemList
{
    public partial class PhanCachNgay_Lich : UserControl
    {
        private string sDate;
        private string sMonth;
        private string sYear;
        private Color sColor;
        

        public PhanCachNgay_Lich()
        {
            InitializeComponent();
        }


        public string SDate { get => sDate; set => sDate = value; }
        public string SMonth { get => sMonth; set => sMonth = value; }
        public string SYear { get => sYear; set => sYear = value; }
        public Color SColor { get => sColor; set => sColor = value; }

        private void PhanCachNgay_Lich_Load(object sender, EventArgs e)
        {
            lbDay.Text = sDate + "-" + sMonth + "-" + sYear;

            this.BackColor = SColor;

            tinhTongThuChiNgay();
        }

        private void  tinhTongThuChiNgay()
        {
            int tongtienchi;
            int tongtienthu;

            // Tạo đối tượng DateTime từ ba số nguyên year, month và day
            DateTime date = new DateTime(int.Parse(sYear), int.Parse(sMonth), int.Parse(sDate));
            string aa = date.ToString("yyyy-MM-dd");

            tongtienchi = laychitrongngay(aa);
            tongtienthu = layThutrongNgay(aa);

            lbchi.Text = "-" + string.Format("{0:#,##0}", tongtienchi);
            lbthu.Text = "+" + string.Format("{0:#,##0}", tongtienthu);

            int kqsauthuchi = tongtienthu - tongtienchi;
            if (kqsauthuchi >= 0)
            {
                lbtong.Text = "+" + string.Format("{0:#,##0}", kqsauthuchi);
            }
            else
            {
                lbtong.Text = string.Format("{0:#,##0}", kqsauthuchi);
            }
        }

        private int layThutrongNgay(string day)
        {
            int nhantienchi = BUS_Lich.LaySoTien_tienthu_Where_NgayThu(day);
            return nhantienchi;
        }

        private int laychitrongngay(string day)
        {
            int nhantienchi = BUS_Lich.LaySoTien_tienchi_Where_NgayChi(day);
            return nhantienchi;
        }
    }
}
