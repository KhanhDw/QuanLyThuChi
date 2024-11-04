using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;


namespace BUS
{
    public class BUS_DangNhap
    {
        public static DataTable XacNhanDangNhap(DTO_TaiKhoan tk)
        {
            return DAO_DangNhap.XacNhanDangNhap(tk);
        }
        public static string XacNhanQuyen(DTO_TaiKhoan tk)
        {
            return DAO_DangNhap.XacNhanQuyen(tk);
        }
    }
}
