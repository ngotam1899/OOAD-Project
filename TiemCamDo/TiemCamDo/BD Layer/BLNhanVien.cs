using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.DB_Layer;

namespace TiemCamDo.BD_Layer
{
    class BLNhanVien
    {
        DBMain db = null;
        public BLNhanVien()
        {
            db = new DBMain();
        }
        public DataTable GetNV()
        {
            string sqlString = string.Format("EXEC spLoadNhanVien");
            return db.MyExecuteQuery(sqlString);
        }
        public bool DeleteNV(string MaNV)
        {
            string sqlString = string.Format("EXEC spDeleteNhanVien N'{0}'", MaNV);
            int result = db.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool InsertNV(string MaNV, string Email, string MatKhau, string Ten, string GioiTinh, string SoDT, string DiaChi, string Quyen)
        {
            string sqlString =
           string.Format("EXEC spInsertNhanVien N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}' ,N'{6}', N'{7}'", MaNV, Email, MatKhau, Ten, GioiTinh, SoDT, DiaChi, Quyen);
            int result = db.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool UpdateNV(string MaNV, string Email, string MatKhau, string Ten, string GioiTinh, string SoDT, string DiaChi, string Quyen)
        {
            string sqlString =
            string.Format("EXEC spUpdateNhanVien N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}', N'{7}'", MaNV, Email, MatKhau, Ten, GioiTinh, SoDT, DiaChi, Quyen);
            int result = db.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool IsUser(string MaNV, string MatKhau, string Quyen)
        {
            string sqlString =
            string.Format("EXEC spCheckedUser N'{0}',N'{1}',N'{2}'", MaNV, MatKhau, Quyen);
            return db.MyExecuteQuery(sqlString).Rows.Count > 0;
        }
        public DataTable SearchNVBySDT(string SDT)
        {

            string sqlString =
            string.Format("EXEC spSearchNhanVienBySDT N'%{0}%'", SDT);
            return db.MyExecuteQuery(sqlString);
        }
        public DataTable SearchNVByTen(string Ten)
        {
            string sqlString =
            string.Format("EXEC spSearchNhanVienByTen N'%{0}%'", Ten);
            return db.MyExecuteQuery(sqlString);
        }
    }
    


}
