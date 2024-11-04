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
    public class DAO_TaiKhoan
    {
        private static SqlConnection con;

        public static bool SuaNguoiDung(DTO_TaiKhoan tkedit, DTO_TaiKhoan user)
        {

            Console.WriteLine(tkedit.Sten_tai_khoan + "-" + tkedit.Sgmail + "-" + tkedit.Smat_khau + "-" + user.Sten_tai_khoan + "-" + user.Sgmail + "-" + user.Smat_khau);

            con = dataProvider.KetNoi();
            string truyvan = string.Format(@"UPDATE tai_khoan
                                                SET ten_tai_khoan = N'{0}', 
                                                    gmail = N'{1}', 
                                                    mat_khau = N'{2}'
                                                WHERE 
                                                    ten_tai_khoan = N'{3}', 
                                                    gmail = N'{4}', 
                                                    mat_khau = N'{5}';",
                                                tkedit.Sten_tai_khoan, tkedit.Sgmail, tkedit.Smat_khau, 
                                                user.Sten_tai_khoan, user.Sgmail, user.Smat_khau);

            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, con);
            dataProvider.DongKetNoi(con);
            return kq;
        }

    }
}
