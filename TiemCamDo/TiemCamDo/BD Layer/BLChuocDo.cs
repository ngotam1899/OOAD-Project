using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.Data_Access_Object;
using TiemCamDo.DB_Layer;

namespace TiemCamDo.BD_Layer
{
    class BLChuocDo
    {
        private static BLChuocDo instance;
        public static BLChuocDo Instance
        {
            get { if (instance == null) instance = new BLChuocDo(); return BLChuocDo.instance; }
            private set { BLChuocDo.instance = value; }
        }
        private BLChuocDo() { }
        public List<Regain> GetChDByMaPhieu(string MaHang)
        {
            List<Regain> regains = new List<Regain>();
            string sqlString = string.Format("EXEC spLoadChuocDoByCamDo N'{0}'", MaHang);
            DataTable data = DBMain.Instance.MyExecuteQuery(sqlString);
            foreach (DataRow item in data.Rows)
            {
                Regain regain= new Regain(item);
                regains.Add(regain);
            }
            return regains;
        }
        //public DataTable GetChDByMaPhieu(string MaHang)
        //{
        //    string sqlString = string.Format("EXEC spLoadChuocDoByCamDo N'{0}'", MaHang);
        //    return DBMain.Instance.MyExecuteQuery(sqlString);
        //}
        //Cập nhật chuộc đồ (ngày chuộc đồ)
        //Lấy ngày chuộc đồ từ Bảng PhieuCamDo
        public bool DeleteChD(string MaPhieuChuoc)
        {
            string sqlString = string.Format("EXEC spDeleteChuocDo N'{0}'", MaPhieuChuoc);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool InsertChD(string MaPhieuChuoc, DateTime NgayChuoc, string SoTienChuoc, string MaPhieu, string MaNV)
        {
            string sqlString =
           string.Format("EXEC spInsertChuocDo N'{0}',N'{1}',N'{2}',N'{3}',N'{4}'", MaPhieuChuoc, NgayChuoc, SoTienChuoc, MaPhieu, MaNV);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool UpdateChD(string MaPhieuChuoc, DateTime NgayChuoc, string SoTienChuoc, string MaPhieu, string MaNV)
        {
            string sqlString =
            string.Format("EXEC spUpdateChuocDo N'{0}',N'{1}',N'{2}',N'{3}',N'{4}'", MaPhieuChuoc, NgayChuoc, SoTienChuoc, MaPhieu, MaNV);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool DeleteChuocDoFromMaHang(string MaHang)
        {
            string sqlString = string.Format("EXEC spDeleteChuocDoFromMaHang N'{0}'", MaHang);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool DeleteChuocDoFromMaPhieuCam(string MaPhieuCam)
        {
            string sqlString = string.Format("EXEC spDeleteChuocDoFromMaPhieuCam N'{0}'", MaPhieuCam);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
    }
}
