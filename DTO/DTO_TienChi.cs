using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_TienChi
    {
        private int sid;
        private int sid_danh_muc_chi;
        private string smo_ta;
        private int sso_tien;
        private string sten_tai_khoan;
        private DateTime sngay_chi;
        private string sNameIconSystems;
        private string sNameiconUserSet;



        public int Sid { get => sid; set => sid = value; }
        public int Sid_danh_muc_chi { get => sid_danh_muc_chi; set => sid_danh_muc_chi = value; }
        public string Smo_ta { get => smo_ta; set => smo_ta = value; }
        public int Sso_tien { get => sso_tien; set => sso_tien = value; }
        public string Sten_tai_khoan { get => sten_tai_khoan; set => sten_tai_khoan = value; }
        public DateTime Sngay_chi { get => sngay_chi; set => sngay_chi = value; }
        public string SNameIconSystems { get => sNameIconSystems; set => sNameIconSystems = value; }
        public string SNameiconUserSet { get => sNameiconUserSet; set => sNameiconUserSet = value; }
    }
}
