using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.SqlServer.Server;

namespace DAO
{
    public class DAO_Lich
    {
        static SqlConnection s;

        public static int LaySoTien_tienchi_Where_NgayChi(string date)
        {
            

            string sTruyVan = string.Format("select so_tien from tien_chi where ngay_chi = N'{0}' and ten_tai_khoan=N'hello678';", date);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, s);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }

            int saveValue = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                saveValue += int.Parse(dt.Rows[i]["so_tien"].ToString());

            }

            //Console.WriteLine(saveValue+": lllllllllllllllllllllllllllll");
            dataProvider.DongKetNoi(s);
            return saveValue;
        }

        public static int LaySoTien_tienthu_Where_NgayThu(string date)
        {
            

            string sTruyVan = string.Format("select so_tien from tien_thu where ngay_thu = N'{0}' and ten_tai_khoan=N'hello678';", date);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, s);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }

            int saveValue = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                saveValue += int.Parse(dt.Rows[i]["so_tien"].ToString());

            }

            Console.WriteLine(saveValue+": lllllllllllllllllllllllllllll");
            dataProvider.DongKetNoi(s);
            return saveValue;
        }


        public static DataTable LayDS_tienchi_Where_NgayChi(string date)
        {
            string sTruyVan = string.Format("select * from tien_chi where ngay_chi = N'{0}' and ten_tai_khoan=N'hello678';", date);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            
            dataProvider.DongKetNoi(s);
            return dt;
        }
        public static DataTable LayDS_tienthu_Where_NgayThu(string date)
        {
            string sTruyVan = string.Format("select * from tien_thu where ngay_thu = N'{0}' and ten_tai_khoan=N'hello678';", date);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(s);
            return dt;
        }



        public static int TongTienChiTrongThang(int thang, int nam, string user)
        {
            string truyvan = string.Format(@"SELECT SUM(so_tien) AS tong_tien FROM tien_chi WHERE MONTH(ngay_chi) = {0}
                                           AND YEAR(ngay_chi) = {1} AND ten_tai_khoan = N'{2}';", thang, nam, user);

            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);

            int tongtienchi = 0;

            if (dt == null)
            {
                return 0;
            }
            else { 
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["tong_tien"].ToString() != "")
                    { 
                        tongtienchi = int.Parse(dr["tong_tien"].ToString()); 
                    }
                    else
                    {
                        return tongtienchi = 0;
                    }
                }
            }
            dataProvider.DongKetNoi(s);
            return tongtienchi;
        }

        public static int TongTienThuTrongThang(int thang, int nam, string user)
        {
            string truyvan = string.Format(@"SELECT SUM(so_tien) AS tong_tien FROM tien_thu WHERE MONTH(ngay_thu) = {0}
                                           AND YEAR(ngay_thu) = {1} AND ten_tai_khoan = N'{2}';", thang, nam, user);

            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);

            int tongtienchi = 0;
            if (dt == null)
            {
                return 0;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["tong_tien"].ToString() != "")
                    {
                        tongtienchi = int.Parse(dr["tong_tien"].ToString());
                    }
                    else
                    {
                        return tongtienchi = 0;
                    }
                }
            }
            dataProvider.DongKetNoi(s);

            return tongtienchi;
        }



        public static DataTable LayNgayCoGhiDuLieuThuChi(int thang, int nam, string user)
        {
            string truyvan = string.Format(@"SELECT DISTINCT ngay
                                            FROM (
                                                SELECT ngay_chi AS ngay
                                                FROM tien_chi
                                                WHERE MONTH(ngay_chi) = {0} AND YEAR(ngay_chi) = {1} and ten_tai_khoan = N'{2}'
                                                UNION ALL
                                                SELECT ngay_thu AS ngay
                                                FROM tien_thu
                                                WHERE MONTH(ngay_thu) = {0} AND YEAR(ngay_thu) = {1} and ten_tai_khoan = N'{2}'
                                            ) AS ngay_tien", thang, nam, user);


            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);

            dataProvider.DongKetNoi(s);

            return dt;
        }
  
        
        
        public static int TongChiTieu_all(string user)
        {

            string truyvan = string.Format(@"select sum(so_tien) as tong_tien From tien_chi where ten_tai_khoan = N'{0}';", user);


            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);

            int saveValue = 0;

            if (dt != null) { 
               
                foreach (DataRow row in dt.Rows)
                {
                    if (row["tong_tien"].ToString() != "")
                        saveValue = int.Parse(row["tong_tien"].ToString());
                }
            }
            else
            {
                saveValue = 0;
            }

            dataProvider.DongKetNoi(s);

            return saveValue;
        }
        public static int TongThuNhap_all(string user)
        {
            string truyvan = string.Format(@"select sum(so_tien) as tong_tien From tien_thu where ten_tai_khoan = N'{0}';", user);


            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);

            int saveValue = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["tong_tien"].ToString() != "")
                    saveValue = int.Parse(row["tong_tien"].ToString());
            }

            dataProvider.DongKetNoi(s);

            return saveValue;
        }
    }
}
