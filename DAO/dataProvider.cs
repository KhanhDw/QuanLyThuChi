using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class dataProvider
    {
        public static SqlConnection KetNoi()
        {
            string s = @"Data Source=DESKTOP-7AIRAJ4;Initial Catalog=quan_ly_thu_chi;Integrated Security=True;";
            //string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\DeskTop\University\N3\HK2\LapTrinhQuanLy\doan\QuanLyThuChi\DAO\db_QLTC.mdf;Integrated Security=True";
            SqlConnection ketNoi = new SqlConnection(s);
            ketNoi.Open();
            return ketNoi;
        }

        public static void DongKetNoi(SqlConnection ketnoi) { 
            ketnoi.Close();
            // Gọi phương thức làm sạch pool kết nối
            SqlConnection.ClearPool(ketnoi);
        }


        public static DataTable TruyVanLayDuLieu(string truyvan, SqlConnection ketnoi)
        {

            SqlDataAdapter da = new SqlDataAdapter(truyvan, ketnoi);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static bool TruyVanKhongLayDuLieu(string truyvan, SqlConnection ketnoi)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(truyvan, ketnoi);
                cmd.ExecuteNonQuery();
                return true;

            }catch (Exception)
            {
                return false;
            }
        }
    }
}
