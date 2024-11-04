using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_TaiKhoan
    {
        private int sid;
        public int Sid
        {
            get { return sid; }
            set { sid = value; }
        }
        private string sten_tai_khoan;
        public string Sten_tai_khoan
        {
            get { return sten_tai_khoan; }
            set { sten_tai_khoan = value; }
        }
        private string sgmail;
        public string Sgmail
        {
            get { return sgmail; }
            set { sgmail = value; }
        }
        private string smat_khau;
        public string Smat_khau
        {
            get { return smat_khau; }
            set { smat_khau = value; }
        }

        public string Quyen { get => quyen; set => quyen = value; }

        private string quyen;
        
    }
}
