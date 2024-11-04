using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class BUS_ThemDanhMuc
    {
        public static bool ThemIconDanhChi(string nameIcon, string nameNguoiDungDat, string user)
        {
            return DAO_ThemDanhMuc.ThemIconDanhChi(nameIcon, nameNguoiDungDat, user);
        }
        public static bool ThemIconDanhThu(string nameIcon, string nameNguoiDungDat, string user)
        {
            return DAO_ThemDanhMuc.ThemIconDanhThu(nameIcon, nameNguoiDungDat, user);
        }
        public static bool XoaIconDanhChi(string nameIcon, string user)
        {
            return DAO_ThemDanhMuc.XoaIconDanhChi(nameIcon, user);
        }
        public static bool XoaIconDanhThu(string nameIcon, string user)
        {
            return DAO_ThemDanhMuc.XoaIconDanhThu(nameIcon, user);
        }
        public static bool SuaIconDanhMucChi(DTO_DanhMucChi dmc, string nameIconEdit)
        {
            return DAO_ThemDanhMuc.SuaIconDanhMucChi(dmc, nameIconEdit);
        }
        public static bool SuaIconDanhMucThu(DTO_DanhMucThu dmt, string nameIconEdit)
        {
            return DAO_ThemDanhMuc.SuaIconDanhMucThu(dmt, nameIconEdit);
        }
        
        
        public static bool SuaIconDanhMucChiAndNameDanhMuc(DTO_DanhMucChi dmc, string nameIconEdit)
        {
            return DAO_ThemDanhMuc.SuaIconDanhMucChiAndNameDanhMuc(dmc, nameIconEdit);
        }
        public static bool SuaIconDanhMucThuAndNameDanhMuc(DTO_DanhMucThu dmc, string nameIconEdit)
        {
            return DAO_ThemDanhMuc.SuaIconDanhMucThuAndNameDanhMuc(dmc, nameIconEdit);
        }




        public static bool KiemTraNameBieuTuongNguoiDungNhapTonTaiTrongDanhMucChi(string nameIcon, string user)
        {
            return DAO_ThemDanhMuc.KiemTraNameBieuTuongNguoiDungNhapTonTaiTrongDanhMucChi(nameIcon, user);
        }
        public static bool KiemTraNameBieuTuongNguoiDungNhapTonTaiTrongDanhMucThu(string nameIcon, string user)
        {
            return DAO_ThemDanhMuc.KiemTraNameBieuTuongNguoiDungNhapTonTaiTrongDanhMucThu(nameIcon, user);
        }
    }
}
