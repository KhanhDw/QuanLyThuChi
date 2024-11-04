using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_ThemDanhMuc
    {
        static SqlConnection conn;


        public static int LayIdIconDanhMucTuNameIcon(string nameIcon)
        {

            string truyvan = string.Format("SELECT id FROM danh_muc WHERE nameIcon = N'{0}';", nameIcon);
            conn = dataProvider.KetNoi();
            int idNameIcon = -1;
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvan, conn);
            foreach (DataRow row in dt.Rows)
            {
                idNameIcon = int.Parse(row["id"].ToString());
            }
            return idNameIcon;
        }
        public static bool ThemIconDanhChi(string nameIcon, string nameNguoiDungDat, string user)
        {

            string nameIconPng = nameIcon + ".png";
            bool kq = false;
            conn = dataProvider.KetNoi();

            int idDanhMuc = LayIdIconDanhMucTuNameIcon(nameIconPng);

            if (idDanhMuc != -1)
            {
                string truyvanlan2 = string.Format("insert into danh_muc_chi (id_danh_muc, ten_tai_khoan, ten_danh_muc_do_nguoi_dung_dat ) values ({0},N'{1}',N'{2}')", idDanhMuc, user, nameNguoiDungDat);
                kq = dataProvider.TruyVanKhongLayDuLieu(truyvanlan2, conn);
            }

            dataProvider.DongKetNoi(conn);
            return kq;
        }
        public static bool ThemIconDanhThu(string nameIcon, string nameNguoiDungDat, string user)
        {

            string nameIconPng = nameIcon + ".png";
            bool kq = false;
            conn = dataProvider.KetNoi();

            int idDanhMuc = LayIdIconDanhMucTuNameIcon(nameIconPng);

            if (idDanhMuc != -1)
            {
                string truyvanlan2 = string.Format("insert into danh_muc_thu (id_danh_muc, ten_tai_khoan,ten_danh_muc_do_nguoi_dung_dat ) values ({0},N'{1}',N'{2}')", idDanhMuc, user, nameNguoiDungDat);
                kq = dataProvider.TruyVanKhongLayDuLieu(truyvanlan2, conn);
            }

            dataProvider.DongKetNoi(conn);
            return kq;
        }
        public static bool XoaIconDanhChi(string nameIcon, string user)
        {

            conn = dataProvider.KetNoi();

            string truyvanlan2 = string.Format("DELETE FROM danh_muc_chi WHERE ten_tai_khoan = N'{0}' and ten_danh_muc_do_nguoi_dung_dat = N'{1}';", user, nameIcon);
            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvanlan2, conn);

            dataProvider.DongKetNoi(conn);
            return kq;
        }
        public static bool XoaIconDanhThu(string nameIcon, string user)
        {

            //string nameIconPng = nameIcon + ".png";
            conn = dataProvider.KetNoi();

            //int idDanhMuc = LayIdIconDanhMucTuNameIcon(nameIconPng);

            //if (idDanhMuc != -1){
            string truyvanlan2 = string.Format("DELETE FROM danh_muc_thu WHERE ten_tai_khoan = N'{0}' and ten_danh_muc_do_nguoi_dung_dat = N'{1}';", user, nameIcon);
            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvanlan2, conn);
            //}

            dataProvider.DongKetNoi(conn);
            return kq;
        }

        public static bool SuaIconDanhMucChi(DTO_DanhMucChi dmc, string nameIconEdit)
        {
            conn = dataProvider.KetNoi();

            string truyvan = string.Format(@"UPDATE danh_muc_chi SET ten_danh_muc_do_nguoi_dung_dat = N'{0}' 
                                            WHERE ten_tai_khoan = N'{1}' and ten_danh_muc_do_nguoi_dung_dat = N'{2}';",
                                        nameIconEdit, dmc.Sten_tai_khoan, dmc.Sten_danh_muc_do_nguoi_dung_dat);
            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, conn);
            dataProvider.DongKetNoi(conn);
            return kq;
        }
        public static bool SuaIconDanhMucThu(DTO_DanhMucThu dmt, string nameIconEdit)
        {
            conn = dataProvider.KetNoi();
            string truyvan = string.Format(@"UPDATE danh_muc_thu SET ten_danh_muc_do_nguoi_dung_dat = N'{0}' 
                                            WHERE ten_tai_khoan = N'{1}' and ten_danh_muc_do_nguoi_dung_dat = N'{2}';",
                                            nameIconEdit, dmt.Sten_tai_khoan, dmt.Sten_danh_muc_do_nguoi_dung_dat);
            bool kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, conn);
            dataProvider.DongKetNoi(conn);
            return kq;
        }

        public static bool SuaIconDanhMucChiAndNameDanhMuc(DTO_DanhMucChi dmc, string nameIconEdit)
        {
            conn = dataProvider.KetNoi();
            string nameIconPng = dmc.SNameIconSystems + ".png";
            bool kq = false;
            conn = dataProvider.KetNoi();

            int idDanhMuc = LayIdIconDanhMucTuNameIcon(nameIconPng);

            if (idDanhMuc != -1)
            {
                string truyvan = string.Format(@"UPDATE danh_muc_chi SET id_danh_muc={0}, ten_danh_muc_do_nguoi_dung_dat = N'{1}' 
                                            WHERE ten_tai_khoan = N'{2}' and ten_danh_muc_do_nguoi_dung_dat = N'{3}';",
                                            idDanhMuc, nameIconEdit, dmc.Sten_tai_khoan, dmc.Sten_danh_muc_do_nguoi_dung_dat);
                kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, conn);
            }
            dataProvider.DongKetNoi(conn);
            return kq;
        }
        public static bool SuaIconDanhMucThuAndNameDanhMuc(DTO_DanhMucThu dmt, string nameIconEdit)
        {
            conn = dataProvider.KetNoi();
            string nameIconPng = dmt.SNameIconSystems + ".png";
            bool kq = false;
            conn = dataProvider.KetNoi();

            int idDanhMuc = LayIdIconDanhMucTuNameIcon(nameIconPng);

            if (idDanhMuc != -1)
            {
                string truyvan = string.Format(@"UPDATE danh_muc_thu SET id_danh_muc={0}, ten_danh_muc_do_nguoi_dung_dat = N'{1}' 
                                            WHERE ten_tai_khoan = N'{2}' and ten_danh_muc_do_nguoi_dung_dat = N'{3}';",
                                            idDanhMuc, nameIconEdit, dmt.Sten_tai_khoan, dmt.Sten_danh_muc_do_nguoi_dung_dat);
                kq = dataProvider.TruyVanKhongLayDuLieu(truyvan, conn);
            }
            dataProvider.DongKetNoi(conn);
            return kq;
        }

        public static bool KiemTraNameBieuTuongNguoiDungNhapTonTaiTrongDanhMucChi(string nameIcon, string user)
        {
            string truyvanlan2 = string.Format("SELECT * FROM danh_muc_chi WHERE ten_tai_khoan = N'{0}' and ten_danh_muc_do_nguoi_dung_dat = N'{1}';", user, nameIcon);
            conn = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvanlan2, conn);
            if (dt.Rows.Count == 0)
            {
                dataProvider.DongKetNoi(conn);
                return false;
            }
            else
            {
                dataProvider.DongKetNoi(conn);
                return true;
            }
        }
        public static bool KiemTraNameBieuTuongNguoiDungNhapTonTaiTrongDanhMucThu(string nameIcon, string user)
        {
            string truyvanlan2 = string.Format("SELECT * FROM danh_muc_thu WHERE  ten_tai_khoan = N'{0}' and ten_danh_muc_do_nguoi_dung_dat = N'{1}';", user, nameIcon);
            conn = dataProvider.KetNoi();
            DataTable dt = dataProvider.TruyVanLayDuLieu(truyvanlan2, conn);
            if (dt.Rows.Count == 0)
            {
                dataProvider.DongKetNoi(conn);
                return false;
            }
            else
            {
                dataProvider.DongKetNoi(conn);
                return true;
            }
        }



    }
}
