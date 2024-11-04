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
    public class BUS_BaoCao
    {
        public static DataTable HienThiBaoCaoTienThu_thangnam(string tc, string thang, string nam)
        {
            return DAO_BaoCao.HienThiBaoCaoTienThu_thangnam(tc, thang, nam);
        }

        public static DataTable HienThiBaoCaoTienChi_thangnam(string tc, string thang, string nam)
        {
            return DAO_BaoCao.HienThiBaoCaoTienChi_thangnam(tc, thang, nam);
        }
        //--------------------------------------------------------
        public static DataTable HienThiBaoCaoTienThu_nam(string tc, string nam)
        {
            return DAO_BaoCao.HienThiBaoCaoTienThu_nam(tc, nam);
        }

        public static DataTable HienThiBaoCaoTienChi_nam(string tc, string nam)
        {
            return DAO_BaoCao.HienThiBaoCaoTienChi_nam(tc, nam);
        }
        //--------------------------------------------------------
        public static DataTable LayDSChi_where_Thang(DTO_TienChi tc)
        {
            return DAO_BaoCao.LayDSChi_where_Thang(tc);
        }
        public static DataTable LayIdDanhMucChi(DTO_DanhMucChi tk)
        {
            return DAO_BaoCao.LayIdDanhMucChi(tk);
        }
        public static DataTable LayDSThu_where_Thang(DTO_TienThu tc)
        {
            return DAO_BaoCao.LayDSThu_where_Thang(tc);
        }
        public static DataTable LayIdDanhMucThu(DTO_DanhMucThu tk)
        {
            return DAO_BaoCao.LayIdDanhMucThu(tk);
        }
        public static string getNameiconFromIdDanhMuc(int id_danh_muc)
        {
            return DAO_BaoCao.getNameiconFromIdDanhMuc(id_danh_muc);
        }
        public static DataTable LayDS_TienChi(DTO_TienChi tc)
        {
            return DAO_BaoCao.LayDS_TienChi(tc);
        }
        public static DataTable LayDS_TienThu(DTO_TienThu tc)
        {
            return DAO_BaoCao.LayDS_TienThu(tc);
        }
    }
}
