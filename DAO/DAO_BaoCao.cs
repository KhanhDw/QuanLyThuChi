using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;
using System.Text.RegularExpressions;

namespace DAO
{
    public class DAO_BaoCao
    {
        static SqlConnection conn;


        public static DataTable HienThiBaoCaoTienChi_thangnam(string user, string thang, string nam)
        {
            string truyvan = string.Format(@"select * from tien_chi where ten_tai_khoan = N'{0}' AND MONTH(ngay_chi) = {1} and YEAR(ngay_chi) = {2};", user, thang, nam);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }

        public static DataTable HienThiBaoCaoTienChi_nam(string user, string nam)
        {
            string truyvan = string.Format(@"select * from tien_chi where ten_tai_khoan = N'{0}' and YEAR(ngay_chi) = {1};", user, nam);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }

        public static DataTable HienThiBaoCaoTienThu_thangnam(string user, string thang, string nam)
        {
            string truyvan = string.Format(@"select * from tien_thu where ten_tai_khoan = N'{0}' AND MONTH(ngay_thu) = {1} and YEAR(ngay_thu) = {2};", user, thang, nam);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }

        public static DataTable HienThiBaoCaoTienThu_nam(string user,  string nam)
        {
            string truyvan = string.Format(@"select * from tien_thu where ten_tai_khoan = N'{0}'  and YEAR(ngay_thu) = {1};", user, nam);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }


        // cho danh mục
        public static DataTable LayDSChi_where_Thang(DTO_TienChi tc)
        {
            string truyvan = string.Format(@"SELECT  d.id_danh_muc, SUM(t.so_tien) AS tong_so_tien  
                                                    FROM tien_chi t
                                                    JOIN danh_muc_chi d ON t.id_danh_muc_chi = d.id
                                                    where MONTH(t.ngay_chi) = {0}
                                                    AND YEAR(ngay_chi) = {1}
                                                    AND t.ten_tai_khoan = N'{2}' 
                                                    AND t.id_danh_muc_chi = {3}
                                                    GROUP BY d.id_danh_muc; ",
                                            tc.Sngay_chi.Month, tc.Sngay_chi.Year, tc.Sten_tai_khoan, tc.Sid_danh_muc_chi);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }
        public static DataTable LayDSThu_where_Thang(DTO_TienThu tt)
        {

                string truyvan = string.Format(@"SELECT  d.id_danh_muc, SUM(t.so_tien) AS tong_so_tien  
                                                    FROM tien_thu t
                                                    JOIN danh_muc_thu d ON t.id_danh_muc_thu = d.id
                                                    where MONTH(t.ngay_thu) = {0}
                                                    AND YEAR(ngay_thu) = {1}
                                                    AND t.ten_tai_khoan = N'{2}' 
                                                    AND t.id_danh_muc_thu = {3}
                                                    GROUP BY d.id_danh_muc; ",
                                            tt.Sngay_thu.Month, tt.Sngay_thu.Year, tt.Sten_tai_khoan, tt.Sid_danh_muc_thu);

            //string truyvan = string.Format(@"SELECT id_danh_muc_thu, SUM(so_tien) AS tong_so_tien FROM tien_thu 
            //                                WHERE ten_tai_khoan = N'{0}' 
            //                                  AND MONTH(ngay_thu) = {1}
            //                                  AND YEAR(ngay_thu) = {2}
            //                                  AND id_danh_muc_thu = {3}
            //                                GROUP BY id_danh_muc_thu;", 
            //                                tt.Sten_tai_khoan, tt.Sngay_thu.Month, tt.Sngay_thu.Year, tt.Sid_danh_muc_thu);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }

            
        public static DataTable LayIdDanhMucChi(DTO_DanhMucChi tk)
        {
            string truyvan = string.Format(@"SELECT id,id_danh_muc,ten_danh_muc_do_nguoi_dung_dat from danh_muc_chi WHERE ten_tai_khoan = N'{0}';", tk.Sten_tai_khoan);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }
        public static DataTable LayIdDanhMucThu(DTO_DanhMucThu tk)
        {
            string truyvan = string.Format(@"SELECT id,id_danh_muc,ten_danh_muc_do_nguoi_dung_dat  from danh_muc_thu WHERE ten_tai_khoan = N'{0}';", tk.Sten_tai_khoan);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }


        public static string getNameiconFromIdDanhMuc(int iddanhmucchi)
        {
            string truyvan = string.Format("select nameIcon from danh_muc where id = '{0}';", iddanhmucchi);

            conn = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(conn);

            string nameiCon = dt.Rows[0]["nameIcon"].ToString();

            nameiCon = nameiCon.Substring(0, nameiCon.Length - 4);

            return nameiCon;
        }


        public static DataTable LayDS_TienChi(DTO_TienChi tc)
        {
            string truyvan = string.Format(@"SELECT DAY(ngay_chi) AS ngay_chi, SUM(so_tien) AS tong_so_tien FROM tien_chi
                                            WHERE ten_tai_khoan = N'{0}' 
                                              AND MONTH(ngay_chi) = {1}
                                              AND YEAR(ngay_chi) = {2}
                                            GROUP BY ngay_chi;",
                                            tc.Sten_tai_khoan, tc.Sngay_chi.Month, tc.Sngay_chi.Year);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }
        
        public static DataTable LayDS_TienThu(DTO_TienThu tt)
        {
            string truyvan = string.Format(@"SELECT DAY(ngay_thu) AS ngay_thu, SUM(so_tien) AS tong_so_tien FROM tien_thu
                                            WHERE ten_tai_khoan = N'{0}' 
                                              AND MONTH(ngay_thu) = {1}
                                              AND YEAR(ngay_thu) = {2}
                                            GROUP BY ngay_thu;",
                                            tt.Sten_tai_khoan, tt.Sngay_thu.Month, tt.Sngay_thu.Year);
            conn = dataProvider.KetNoi();

            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);

            dataProvider.DongKetNoi(conn);
            return dt;
        }
    }
}
