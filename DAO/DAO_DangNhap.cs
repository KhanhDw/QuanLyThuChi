using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_DangNhap
    {

        private static SqlConnection con;
        public static DataTable XacNhanDangNhap(DTO_TaiKhoan tk)
        {
            con = dataProvider.KetNoi();
            string truyvan = string.Format(@"select ten_tai_khoan, gmail, mat_khau from tai_khoan where ten_tai_khoan = N'{0}' and mat_khau = N'{1}';", tk.Sten_tai_khoan, tk.Smat_khau);
            DataTable kq = dataProvider.TruyVanLayDuLieu(truyvan, con);
            dataProvider.DongKetNoi(con);
            return kq;
        }

        public static string XacNhanQuyen(DTO_TaiKhoan tk)
        {
            con = dataProvider.KetNoi();
            string truyvan = string.Format(@"select quyen from tai_khoan where ten_tai_khoan = N'{0}'  and mat_khau = N'{1}';", tk.Sten_tai_khoan, tk.Smat_khau);
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, con);

            string sQuyen = string.Empty;

            if (dt == null)
            {
                return "";
            }

            foreach (DataRow row in dt.Rows)
            {
                sQuyen = row["quyen"].ToString();
            }

            dataProvider.DongKetNoi(con);
            return sQuyen;
        }
    }
}
