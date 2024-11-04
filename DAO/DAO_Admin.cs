using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;


namespace DAO
{
    public class DAO_Admin
    {
        static SqlConnection conn;

        public static DataTable LayDSNguoiDung()
        {
            string truyvan = string.Format(@"select * from tai_khoan");

            conn = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);
            dataProvider.DongKetNoi(conn);
            return dt;
        }



        public static bool ThemNguoiDung(DTO_TaiKhoan tk)
        {
            string truyvan = string.Format(@"INSERT INTO tai_khoan (  ten_tai_khoan ,  gmail ,  mat_khau , quyen ) 
                                                VALUES ( N'{0}', N'{1}', N'{2}' , N'{3}' );",
                                                tk.Sten_tai_khoan, tk.Sgmail, tk.Smat_khau, tk.Quyen);

            conn = dataProvider.KetNoi();
            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, conn);
            dataProvider.DongKetNoi(conn);
            return kq;
        }
        public static bool SuaNguoiDung(DTO_TaiKhoan tkedit)
        {
            conn = dataProvider.KetNoi();
            string truyvan = string.Format(@"UPDATE tai_khoan
                                                SET ten_tai_khoan = N'{0}', 
                                                    gmail = N'{1}', 
                                                    mat_khau = N'{2}', 
                                                    quyen = N'{3}'
                                                WHERE id={4}",
                                                tkedit.Sten_tai_khoan, tkedit.Sgmail, tkedit.Smat_khau, tkedit.Quyen,tkedit.Sid);

            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, conn);
            dataProvider.DongKetNoi(conn);
            return kq;
        }

        public static bool XoaNguoiDung( DTO_TaiKhoan user)
        {
            conn = dataProvider.KetNoi();
            string truyvan = string.Format(@"delete from tai_khoan where id = {0};",
                                                user.Sid);

            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, conn);
            dataProvider.DongKetNoi(conn);
            return kq;
        }


        // Lấy thông tin nhân viên có mã ma, trả về null nếu không thấy 
        public static List<DTO_TaiKhoan> TimNguoiDung(string nameUser)
        {
            string sTruyVan = string.Format(@"select * from tai_khoan where ten_tai_khoan like N'%{0}%'", nameUser);
            conn = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, conn);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            List<DTO_TaiKhoan> lstNhanVien = new List<DTO_TaiKhoan>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DTO_TaiKhoan tk = new DTO_TaiKhoan();
                tk.Sid = int.Parse(dt.Rows[0]["id"].ToString());
                tk.Sten_tai_khoan = dt.Rows[0]["ten_tai_khoan"].ToString();
                tk.Sgmail = dt.Rows[0]["gmail"].ToString();
                tk.Smat_khau = dt.Rows[0]["mat_khau"].ToString();
                tk.Quyen = dt.Rows[0]["quyen"].ToString();

                lstNhanVien.Add(tk);
            }
            dataProvider.DongKetNoi(conn);
            return lstNhanVien;
        }
    }
}
