using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.DB_Layer;

namespace TiemCamDo.BD_Layer
{
    class BLThongKe
    {
        private static BLThongKe instance;
        public static BLThongKe Instance
        {
            get { if (instance == null) instance = new BLThongKe(); return BLThongKe.instance; }
            private set { BLThongKe.instance = value; }
        }
        private BLThongKe() { }
        public DataTable GetThongKe(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            string sqlString = string.Format("EXEC spThongKePhieuCamDo  N'{0}', N'{1}'", NgayBatDau, NgayKetThuc);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }

        public DataTable GetThongKeTraGop(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            string sqlString = string.Format("EXEC spThongKePhieuTraGop  N'{0}', N'{1}'", NgayBatDau, NgayKetThuc);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }

        public DataTable GetThongKeChuocDo(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            string sqlString = string.Format("EXEC spThongKePhieuChuocDo  N'{0}', N'{1}'", NgayBatDau, NgayKetThuc);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }
    }
}
