using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_NhapThuChi
    {
        public static string[] LayDSIcon_DanhMuc()
        {
            return DAO_NhapThuChi.LayDSIcon_DanhMuc();
        }
        public static List<DTO_DanhMucChi> LayDSIcon_DanhMuc_Chi(string user)
        {
            return DAO_NhapThuChi.LayDSIcon_DanhMuc_Chi(user);
        }
        public static List<DTO_DanhMucThu> LayDSIcon_DanhMuc_Thu(string user)
        {
            return DAO_NhapThuChi.LayDSIcon_DanhMuc_Thu(user);
        } 
        public static DataTable LayDS_tienchi_Where_NgayChi(string date, string user)
        {
            return DAO_NhapThuChi.LayDS_tienchi_Where_NgayChi(date, user);
        } 
        public static DataTable LayDS_tienthu_Where_NgayThu(string date, string user)
        {
            return DAO_NhapThuChi.LayDS_tienthu_Where_NgayThu(date, user);
        }
        public static string getNameiconFromIdDanhMuc(int idIcon)
        {
            return DAO_NhapThuChi.getNameiconFromIdDanhMuc(idIcon);
        }
        public static string getNameIconDoNguoiDungDatFromIdDanhMucChi(int idIcon, string user)
        {
            return DAO_NhapThuChi.getNameIconDoNguoiDungDatFromIdDanhMucChi(idIcon, user);
        }
        public static string getNameIconDoNguoiDungDatFromIdDanhMucThu(int idIcon, string user)
        {
            return DAO_NhapThuChi.getNameIconDoNguoiDungDatFromIdDanhMucThu(idIcon, user);
        }
        public static bool ThemTienChi(DTO_TienChi tc, DTO_DanhMucChi dmc)
        {
            return DAO_NhapThuChi.ThemTienChi(tc, dmc);
        }
        public static bool ThemTienThu(DTO_TienThu tt, DTO_DanhMucThu dmt)
        {
            return DAO_NhapThuChi.ThemTienThu(tt, dmt);
        }
        public static bool CapNhatTienChi(DTO_TienChi tc)
        {
            return DAO_NhapThuChi.CapNhatTienChi(tc);
        }
        public static bool CapNhatTienThu(DTO_TienThu tt)
        {
            return DAO_NhapThuChi.CapNhatTienThu(tt);
        }
        public static DataTable LayIDDanhMucTuNameIconChi(string nameicon, string user)
        {
            return DAO_NhapThuChi.LayIDDanhMucTuNameIconChi(nameicon, user);
        }
        public static DataTable LayIDDanhMucTuNameIconThu(string nameicon, string user)
        {
            return DAO_NhapThuChi.LayIDDanhMucTuNameIconThu(nameicon, user);
        }
        public static bool XoaMucChi_where_NgayChi(DTO_TienChi tc)
        {
            return DAO_NhapThuChi.XoaMucChi_where_NgayChi(tc);
        }
        public static bool XoaMucChi_where_NgayThu(DTO_TienThu tc)
        {
            return DAO_NhapThuChi.XoaMucChi_where_NgayThu(tc);
        }


        public static DataTable LayTatCaTienChi(string tendanhmuc , string user)
        {
            return DAO_NhapThuChi.LayTatCaTienChi(tendanhmuc , user);
        }
        public static DataTable LayTatCaTienThu(string tendanhmuc , string user)
        {
            return DAO_NhapThuChi.LayTatCaTienThu(tendanhmuc , user);
        }
    }
}
