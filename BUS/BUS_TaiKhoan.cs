using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_TaiKhoan
    {
        public static bool SuaNguoiDung(DTO_TaiKhoan tkedit, DTO_TaiKhoan user)
        {
            return DAO_TaiKhoan.SuaNguoiDung(tkedit, user);
        }
    }
}
