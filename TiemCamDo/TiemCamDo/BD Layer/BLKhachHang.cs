 using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.DB_Layer;

namespace TiemCamDo.BD_Layer
{
    class BLKhachHang
    {
        DBMain db = null;
        public BLKhachHang()
        {
            db = new DBMain();
        }
        public DataTable GetKH()
        {
            string sqlString = "EXEC spLoadKhachHang";
            return db.MyExecuteQuery(sqlString);
        }
        public DataTable GetCMNDTen()
        {
            string sqlString = "EXEC spLoadCMNDTen";
            return db.MyExecuteQuery(sqlString);
             
        } 
        public bool DeleteKH(string CMND)
        {
            string sqlString = string.Format("EXEC spDeleteKhachHang N'{0}'", CMND);
            int result = db.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool InsertKH(string CMND, string Ten, string DiaChi, string SoDT, DateTime NgaySinh, string NoiCap, string GioiTinh)
        {
            string sqlString =
           string.Format("EXEC spInsertKhachHang N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}'", CMND, Ten, DiaChi, SoDT, NgaySinh, NoiCap, GioiTinh);
            int result = db.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool UpdateKH(string CMND, string Ten, string DiaChi, string SoDT, DateTime NgaySinh, string NoiCap, string GioiTinh)
        {
            string sqlString =
            string.Format("EXEC spUpdateKhachHang N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}'", CMND, Ten, DiaChi, SoDT, NgaySinh, NoiCap, GioiTinh);
            int result = db.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public DataTable SearchKHBySDT(string SDT)
        {

            string sqlString =
            string.Format("EXEC spSearchKhachHangBySDT N'%{0}%'", SDT);
            return db.MyExecuteQuery(sqlString);
        }
        public DataTable SearchKHByTen(string Ten)
        {
            string sqlString =
            string.Format("EXEC spSearchKhachHangByTen N'%{0}%'", Ten);
            return db.MyExecuteQuery(sqlString);
        }





    }
}
