using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.DB_Layer;

namespace TiemCamDo.BD_Layer
{
    class BLMatHang
    {
        public DataTable GetMH()
        {
            string sqlString = string.Format("EXEC spLoadMatHang");
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }
        public DataTable GetMHByMaPhieuCam(string MaPhieu)
        {
            string sqlString = string.Format("EXEC spLoadMatHangByMaPhieuCam N'{0}'", MaPhieu);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }

        public DataTable GetMHByCMND(string CMND)
        {
            string sqlString = string.Format("EXEC spLoadMatHangByCMND N'{0}'", CMND);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }

        public DataTable SearchMHByTenMH(string TenMH)
        {

            string sqlString =
            string.Format("EXEC spSearchMatHangByTenMatHang N'%{0}%'", TenMH);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }

        public DataTable SearchMHByCMND(string CMND)
        {

            string sqlString =
            string.Format("EXEC spSearchMHByCMND N'%{0}%'", CMND);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }
        public bool DeleteMH(string MaHang)
        {
            string sqlString = string.Format("EXEC spDeleteMatHang N'{0}'", MaHang);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool InsertMH(string MaHang, string LoaiHang, string ChiTiet, string GiaTri, string CMND)
        {
            string sqlString =
           string.Format("EXEC spInsertMatHang N'{0}',N'{1}',N'{2}',N'{3}',N'{4}'", MaHang, LoaiHang, ChiTiet, GiaTri, CMND);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool UpdateMH(string MaHang, string LoaiHang, string ChiTiet, string GiaTri, string CMND)
        {
            string sqlString =
            string.Format("EXEC spUpdateMatHang N'{0}',N'{1}',N'{2}',N'{3}',N'{4}'", MaHang, LoaiHang, ChiTiet, GiaTri, CMND);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }

    }
}
