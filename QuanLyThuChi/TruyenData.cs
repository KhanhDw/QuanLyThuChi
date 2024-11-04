using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuChi
{
    public class TruyenData
    {

        private static TruyenData instance;
        public string LoginTK { get; set; }
        public string LoginMK { get; set; }
        public string LoginGmail { get; set; }
        public string LoginQuyen { get; set; }


        // Đảm bảo không thể tạo nhiều thể hiện của lớp này
        // Phương thức để lấy thể hiện của lớp đã lưu
        public static TruyenData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TruyenData();
                }
                return instance;
            }
        }
    }
}

