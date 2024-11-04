using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Lich
    {
        public static int LaySoTien_tienchi_Where_NgayChi(string date)
        {
            return DAO_Lich.LaySoTien_tienchi_Where_NgayChi(date);
        }
        public static int LaySoTien_tienthu_Where_NgayThu(string date)
        {
            return DAO_Lich.LaySoTien_tienthu_Where_NgayThu(date);
        }
        
        public static DataTable LayDS_tienchi_Where_NgayChi(string date)
        {
            return DAO_Lich.LayDS_tienchi_Where_NgayChi(date);
        }
        public static DataTable LayDS_tienthu_Where_NgayThu(string date)
        {
            return DAO_Lich.LayDS_tienthu_Where_NgayThu(date);
        }


        public static int TongTienChiTrongThang(int thang, int nam, string user)
        {
            return DAO_Lich.TongTienChiTrongThang(thang , nam, user);
        }
        public static int TongTienThuTrongThang(int thang, int nam, string user)
        {
            return DAO_Lich.TongTienThuTrongThang(thang , nam, user);
        }
        public static DataTable LayNgayCoGhiDuLieuThuChi(int thang, int nam, string user)
        {
            return DAO_Lich.LayNgayCoGhiDuLieuThuChi(thang , nam, user);
        }
        public static int TongChiTieu_all(string user)
        {
            return DAO_Lich.TongChiTieu_all(user);
        }
        public static int TongThuNhap_all(string user)
        {
            return DAO_Lich.TongThuNhap_all(user);
        }
    }
}
