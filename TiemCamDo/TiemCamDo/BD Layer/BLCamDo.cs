using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.DB_Layer;

namespace TiemCamDo.BD_Layer
{
    class BLCamDo
    {
        private static BLCamDo instance;
        public static BLCamDo Instance
        {
            get { if (instance == null) instance = new BLCamDo(); return BLCamDo.instance; }
            private set { BLCamDo.instance = value; }
        }
        private BLCamDo() { }
        public DataTable GetCDByCMND(string CMND)
        {
            string sqlString = string.Format("EXEC spLoadCamDoByMaKH N'{0}'", CMND);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }

        public DataTable GetCDByMaHang(string MaHang)
        {
            string sqlString = string.Format("EXEC spLoadCamDoByMaHang N'{0}'", MaHang);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }

        public DataTable SearchCDByTen(string TenKH)
        {
            string sqlString = string.Format("EXEC spSearchCamDoByTenKhach N'{0}'", TenKH);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }
        public bool DeleteCD(string MaPhieu)
        {
            string sqlString = string.Format("EXEC spDeleteCamDo N'{0}'", MaPhieu);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        //spDeleteCamDoFromMaHang
        public bool DeleteCamDoFromMaHangCD(string MaHang)
        {
            string sqlString = string.Format("EXEC spDeleteCamDoFromMaHang N'{0}'", MaHang);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool InsertCD(string MaPhieu, string MaHang, DateTime NgayCam, DateTime NgayChuoc, string SoTienCam, string LaiSuat, string MaNV)
        {
            string sqlString =
           string.Format("EXEC spInsertCamDo N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}'", MaPhieu, MaHang, NgayCam, NgayChuoc, SoTienCam, LaiSuat, MaNV);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool UpdateCD(string MaPhieu, string MaHang, DateTime NgayCam, DateTime NgayChuoc, string SoTienCam, string LaiSuat, string MaNV)
        {
            string sqlString =
            string.Format("EXEC spUpdateCamDo N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}'", MaPhieu, MaHang, NgayCam, NgayChuoc, SoTienCam, LaiSuat, MaNV);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
    }
}
