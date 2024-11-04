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
    public class DAO_NhapThuChi
    {
        static SqlConnection s;

        public static string[] LayDSIcon_DanhMuc()
        {
            string truyvan = String.Format(@"SELECT nameIcon FROM danh_muc");
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);
            dataProvider.DongKetNoi(s);

            // Tạo mảng để lưu trữ dữ liệu
            string[] names = new string[dt.Rows.Count];

            // Duyệt qua DataTable và thêm dữ liệu vào mảng
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                names[i++] = row["nameIcon"].ToString();
            }

            return names;
        }


        public static List<DTO_DanhMucChi> LayDSIcon_DanhMuc_Chi(string user)
        {
            string truyvan = String.Format(@"SELECT danh_muc.nameIcon, danh_muc_chi.ten_danh_muc_do_nguoi_dung_dat FROM danh_muc_chi INNER JOIN danh_muc  
                                            ON danh_muc_chi.id_danh_muc = danh_muc.id
                                            Where danh_muc_chi.ten_tai_khoan = N'{0}';", user);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);
            dataProvider.DongKetNoi(s);
            List<DTO_DanhMucChi> lstienchi = new List<DTO_DanhMucChi>();
            foreach (DataRow row in dt.Rows)
            {
                DTO_DanhMucChi dmt = new DTO_DanhMucChi();
                dmt.SNameIconSystems = row["nameIcon"].ToString();
                dmt.SNameiconUserSet = row["ten_danh_muc_do_nguoi_dung_dat"].ToString();
                lstienchi.Add(dmt);
            }

            return lstienchi;
        }
        public static List<DTO_DanhMucThu> LayDSIcon_DanhMuc_Thu(string user)
        {
            string truyvan = String.Format(@"SELECT danh_muc.nameIcon, danh_muc_thu.ten_danh_muc_do_nguoi_dung_dat FROM danh_muc_thu INNER JOIN danh_muc  
                                            ON danh_muc_thu.id_danh_muc = danh_muc.id
                                            Where danh_muc_thu.ten_tai_khoan = N'{0}';", user);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);




            List<DTO_DanhMucThu> lstienthu = new List<DTO_DanhMucThu>();
            foreach (DataRow row in dt.Rows)
            {
                DTO_DanhMucThu dmt = new DTO_DanhMucThu();
                dmt.SNameIconSystems = row["nameIcon"].ToString();
                dmt.SNameiconUserSet = row["ten_danh_muc_do_nguoi_dung_dat"].ToString();
                lstienthu.Add(dmt);
            }


            dataProvider.DongKetNoi(s);
            return lstienthu;
        }


        public static DataTable LayDS_tienchi_Where_NgayChi(string date, string user)
        {
            string sTruyVan = string.Format("SELECT t.id, d.id_danh_muc as id_danh_muc_chi, t.mo_ta, t.so_tien, t.ten_tai_khoan, t.ngay_chi " +
                                            "FROM tien_chi t " +
                                            "JOIN danh_muc_chi d ON t.id_danh_muc_chi = d.id where t.ngay_chi = N'{0}' and t.ten_tai_khoan = N'{1}';",
                                            date, user);

            //string sTruyVan = string.Format("select * from tien_chi where ngay_chi = N'{0}' and ten_tai_khoan=N'{1}';", date, user);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(s);
            return dt;
        }
        public static DataTable LayDS_tienthu_Where_NgayThu(string date, string user)
        {
            string sTruyVan = string.Format("SELECT t.id, d.id_danh_muc as id_danh_muc_thu, t.mo_ta, t.so_tien, t.ten_tai_khoan, t.ngay_thu " +
                                            "FROM tien_thu t " +
                                            "JOIN danh_muc_thu d ON t.id_danh_muc_thu = d.id where t.ngay_thu = N'{0}' and t.ten_tai_khoan = N'{1}';",
                                            date, user);
            // string sTruyVan = string.Format("select * from tien_thu where ngay_thu = N'{0}' and ten_tai_khoan=N'{1}';", date, user);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(s);
            return dt;
        }


        public static string getNameiconFromIdDanhMuc(int iddanhmucchi)
        {
            string truyvan = string.Format("select nameIcon from danh_muc where id = '{0}';", iddanhmucchi);

            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(s);

            string nameiCon = dt.Rows[0]["nameIcon"].ToString();

            nameiCon = nameiCon.Substring(0, nameiCon.Length - 4);

            return nameiCon;
        }


        public static string getNameIconDoNguoiDungDatFromIdDanhMucChi(int iddanhmucchi, string user)
        {
            string truyvan = string.Format("select ten_danh_muc_do_nguoi_dung_dat from danh_muc_chi where id_danh_muc = {0} and ten_tai_khoan = N'{1}';", iddanhmucchi, user);

            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(s);

            string nameiCon = dt.Rows[0]["ten_danh_muc_do_nguoi_dung_dat"].ToString();
            Console.WriteLine(nameiCon);
            return nameiCon;
        }

        public static string getNameIconDoNguoiDungDatFromIdDanhMucThu(int iddanhmucchi, string user)
        {
            string truyvan = string.Format("select ten_danh_muc_do_nguoi_dung_dat from danh_muc_thu where id_danh_muc = {0} and ten_tai_khoan = N'{1}';", iddanhmucchi, user);

            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(s);

            string nameiCon = dt.Rows[0]["ten_danh_muc_do_nguoi_dung_dat"].ToString();
            Console.WriteLine(nameiCon);
            return nameiCon;
        }



        public static bool ThemTienChi(DTO_TienChi tc, DTO_DanhMucChi dmc)
        {
            bool dt = false;
            string truyvan1 = string.Format(@"select id from danh_muc where nameIcon = N'{0}';", tc.SNameIconSystems + ".png");

            s = dataProvider.KetNoi();
            DataTable idIcon = dataProvider.TruyVanLayDuLieu(truyvan1, s);

            if (idIcon.Rows.Count != 0)
            {
                string idDanhmuc = idIcon.Rows[0]["id"].ToString();

                Console.WriteLine(idDanhmuc + "--" + dmc.Sten_danh_muc_do_nguoi_dung_dat + "--" + dmc.Sten_tai_khoan);
                // câu lệnh kiểm tra id có tồn tại trong danh mục không
                string truyvan2 = string.Format(@"select id from danh_muc_chi where id_danh_muc = {0} and ten_danh_muc_do_nguoi_dung_dat = N'{1}' and ten_tai_khoan=N'{2}';",
                                                    int.Parse(idDanhmuc), dmc.Sten_danh_muc_do_nguoi_dung_dat, dmc.Sten_tai_khoan);


                string idDanhmucChi = string.Empty;
                DataTable dtidDanhMucChi = dataProvider.TruyVanLayDuLieu(truyvan2, s);
                try
                {
                    idDanhmucChi = dtidDanhMucChi.Rows[0]["id"].ToString();
                }catch (Exception){
                  
                }


                    string ngayychi = tc.Sngay_chi.ToString("yyyy-MM-dd");
                //string truyvan3 = string.Format(@"select id_danh_muc from danh_muc_chi where id = {0};", int.Parse(idDanhmucChi));
                //DataTable dtiddanhmucc = dataProvider.TruyVanLayDuLieu(truyvan3, s);
                //string idDanhmucc = dtiddanhmucc.Rows[0]["id_danh_muc"].ToString();


                Console.WriteLine(" dt: " + dt);
                Console.WriteLine(idDanhmucChi + "--" + tc.Smo_ta + "--" + tc.Sso_tien + "--" + tc.Sten_tai_khoan + "--" + ngayychi);

                string truyvan = string.Format(@"INSERT INTO tien_chi (  id_danh_muc_chi, mo_ta, so_tien, ten_tai_khoan, ngay_chi  ) 
                                            VALUES ( {0}, N'{1}',{2}, N'{3}', N'{4}');",
                                        idDanhmucChi, tc.Smo_ta, tc.Sso_tien, tc.Sten_tai_khoan, ngayychi); // tc.Sngay_chi);
                dt = dataProvider.TruyVanKhongLayDuLieu(truyvan, s);

            }

            dataProvider.DongKetNoi(s);
            return dt;
        }

        public static bool ThemTienThu(DTO_TienThu tt, DTO_DanhMucThu dmt)
        {
            bool dt = false;
            string truyvan1 = string.Format(@"select id from danh_muc where nameIcon = N'{0}';", tt.SNameIconSystems + ".png");

            s = dataProvider.KetNoi();
            DataTable idIcon = dataProvider.TruyVanLayDuLieu(truyvan1, s);


            if (idIcon.Rows.Count != 0)
            {
                string idDanhmuc = idIcon.Rows[0]["id"].ToString();

                Console.WriteLine(idDanhmuc + "--" + dmt.Sten_danh_muc_do_nguoi_dung_dat + "--" + dmt.Sten_tai_khoan);
                // câu lệnh kiểm tra id có tồn tại trong danh mục không
                string truyvan2 = string.Format(@"select id from danh_muc_thu where id_danh_muc = {0} and ten_danh_muc_do_nguoi_dung_dat = N'{1}' and ten_tai_khoan=N'{2}';",
                                                    int.Parse(idDanhmuc), dmt.Sten_danh_muc_do_nguoi_dung_dat, dmt.Sten_tai_khoan);

                DataTable dtidDanhMucChi = dataProvider.TruyVanLayDuLieu(truyvan2, s);
                string idDanhmucChi = dtidDanhMucChi.Rows[0]["id"].ToString();


                string ngayythu = tt.Sngay_thu.ToString("yyyy-MM-dd");
                //string truyvan3 = string.Format(@"select id_danh_muc from danh_muc_chi where id = {0};", int.Parse(idDanhmucChi));
                //DataTable dtiddanhmucc = dataProvider.TruyVanLayDuLieu(truyvan3, s);
                //string idDanhmucc = dtiddanhmucc.Rows[0]["id_danh_muc"].ToString();


                Console.WriteLine(idDanhmucChi + "--" + tt.Smo_ta + "--" + tt.Sso_tien + "--" + tt.Sten_tai_khoan + "--" + ngayythu);

                string truyvan = string.Format(@"INSERT INTO tien_thu (  id_danh_muc_thu, mo_ta, so_tien, ten_tai_khoan, ngay_thu  ) 
                                            VALUES ( {0}, N'{1}',{2}, N'{3}', N'{4}');",
                                        idDanhmucChi, tt.Smo_ta, tt.Sso_tien, tt.Sten_tai_khoan, ngayythu); // tc.Sngay_chi);
                dt = dataProvider.TruyVanKhongLayDuLieu(truyvan, s);

            }




            dataProvider.DongKetNoi(s);
            return dt;
        }

        public static bool CapNhatTienChi(DTO_TienChi ct)
        {
            string truyvan = string.Format(@"UPDATE tien_chi SET mo_ta = N'{0}', so_tien = {1}, ngay_chi = N'{2}', id_danh_muc_chi = {3} WHERE id = {4} and ten_tai_khoan = N'{5}';",
                ct.Smo_ta, ct.Sso_tien, ct.Sngay_chi, ct.Sid_danh_muc_chi, ct.Sid, ct.Sten_tai_khoan);
            s = dataProvider.KetNoi();
            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, s);

            dataProvider.DongKetNoi(s);
            return kq;
        }
        public static bool CapNhatTienThu(DTO_TienThu ct)
        {
            Console.WriteLine("** " + " " + ct.Smo_ta + " " + ct.Sso_tien + " " + ct.Sngay_thu + " " + ct.Sid_danh_muc_thu + " " + ct.Sid);

            string truyvan = string.Format(@"UPDATE tien_thu SET mo_ta = N'{0}', so_tien = {1}, ngay_thu = N'{2}', id_danh_muc_thu = {3} WHERE id = {4} and ten_tai_khoan = N'{5}';",
                ct.Smo_ta, ct.Sso_tien, ct.Sngay_thu, ct.Sid_danh_muc_thu, ct.Sid, ct.Sten_tai_khoan);
            s = dataProvider.KetNoi();
            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, s);

            dataProvider.DongKetNoi(s);
            return kq;
        }



        public static DataTable LayIDDanhMucTuNameIconChi(string nameIcon, string user)
        {
            string truyvanlan2 = string.Format("SELECT id FROM danh_muc_chi WHERE ten_tai_khoan = N'{0}' and ten_danh_muc_do_nguoi_dung_dat = N'{1}';", user, nameIcon);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvanlan2, s);
            dataProvider.DongKetNoi(s);
            return dt;

        }
        public static DataTable LayIDDanhMucTuNameIconThu(string nameIcon, string user)
        {
            string truyvanlan2 = string.Format("SELECT id FROM danh_muc_thu WHERE ten_tai_khoan = N'{0}' and ten_danh_muc_do_nguoi_dung_dat = N'{1}';", user, nameIcon);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvanlan2, s);
            dataProvider.DongKetNoi(s);
            return dt;

        }

        public static bool XoaMucChi_where_NgayChi(DTO_TienChi tc)
        {
            string truyvan = string.Format(@"DELETE FROM tien_chi WHERE id = {0} AND ngay_chi = '{1}' and ten_tai_khoan = N'{2}';", tc.Sid, tc.Sngay_chi, tc.Sten_tai_khoan);
            s = dataProvider.KetNoi();
            bool dt = dataProvider.TruyVanKhongLayDuLieu(truyvan, s);
            dataProvider.DongKetNoi(s);
            return dt;
        }

        public static bool XoaMucChi_where_NgayThu(DTO_TienThu tt)
        {
            string truyvan = string.Format(@"DELETE FROM tien_thu WHERE id = {0} AND ngay_thu = '{1}' and ten_tai_khoan = N'{2}';", tt.Sid, tt.Sngay_thu, tt.Sten_tai_khoan);
            s = dataProvider.KetNoi();
            bool dt = dataProvider.TruyVanKhongLayDuLieu(truyvan, s);
            dataProvider.DongKetNoi(s);
            return dt;
        }
    
    
        public static DataTable LayTatCaTienChi(string tendanhmuc, string user)
        {
            string sTruyVan = string.Format("SELECT t.id, d.id_danh_muc as id_danh_muc_chi, t.mo_ta, t.so_tien, t.ten_tai_khoan, t.ngay_chi " +
                                           "FROM tien_chi t " +
                                           "JOIN danh_muc_chi d ON t.id_danh_muc_chi = d.id where d.ten_danh_muc_do_nguoi_dung_dat like N'%{0}%' and t.ten_tai_khoan = N'{1}';",
                                           tendanhmuc, user);
            // string sTruyVan = string.Format("select * from tien_thu where ngay_thu = N'{0}' and ten_tai_khoan=N'{1}';", date, user);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(s);
            return dt;
        }

        public static DataTable LayTatCaTienThu(string tendanhmuc, string user)
        {
            string sTruyVan = string.Format("SELECT t.id, d.id_danh_muc as id_danh_muc_thu, t.mo_ta, t.so_tien, t.ten_tai_khoan, t.ngay_thu " +
                                           "FROM tien_thu t " +
                                           "JOIN danh_muc_thu d ON t.id_danh_muc_thu = d.id where d.ten_danh_muc_do_nguoi_dung_dat like N'%{0}%' and t.ten_tai_khoan = N'{1}';",
                                           tendanhmuc, user);
            // string sTruyVan = string.Format("select * from tien_thu where ngay_thu = N'{0}' and ten_tai_khoan=N'{1}';", date, user);
            s = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(sTruyVan, s);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dataProvider.DongKetNoi(s);
            return dt;
        }
    }
}
