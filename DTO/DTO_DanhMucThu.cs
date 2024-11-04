using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_DanhMucThu
    {
        private int sid;
        private int sid_danh_muc;
        private string sten_danh_muc_do_nguoi_dung_dat;
        private string sten_tai_khoan;
        
        private string sNameiconUserSet;
        private string sNameIconSystems;

        public int Sid { get => sid; set => sid = value; }
        public int Sid_danh_muc { get => sid_danh_muc; set => sid_danh_muc = value; }
        public string SNameiconUserSet { get => sNameiconUserSet; set => sNameiconUserSet = value; }
        public string SNameIconSystems { get => sNameIconSystems; set => sNameIconSystems = value; }
        public string Sten_danh_muc_do_nguoi_dung_dat { get => sten_danh_muc_do_nguoi_dung_dat; set => sten_danh_muc_do_nguoi_dung_dat = value; }
        public string Sten_tai_khoan { get => sten_tai_khoan; set => sten_tai_khoan = value; }
    }
}
