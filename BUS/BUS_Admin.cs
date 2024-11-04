using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Admin
    {
        public static DataTable LayDSNguoiDung()
        {
            return DAO_Admin.LayDSNguoiDung();
        }
        public static bool ThemNguoiDung(DTO_TaiKhoan tk)
        {
            return DAO_Admin.ThemNguoiDung(tk);
        }
        public static bool SuaNguoiDung(DTO_TaiKhoan tk)
        {
            return DAO_Admin.SuaNguoiDung(tk);
        }
        public static bool XoaNguoiDung(DTO_TaiKhoan tk)
        {
            return DAO_Admin.XoaNguoiDung(tk);
        }
        public static List<DTO_TaiKhoan> TimNguoiDung(string tk)
        {
            return DAO_Admin.TimNguoiDung(tk);
        }

    }
}
