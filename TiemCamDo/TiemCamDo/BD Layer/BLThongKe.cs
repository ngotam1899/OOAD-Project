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
        DBMain db = null;
        public BLThongKe()
        {
            db = new DBMain();
        }
        public DataTable GetThongKe(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            string sqlString = string.Format("EXEC spThongKePhieuCamDo  N'{0}', N'{1}'", NgayBatDau, NgayKetThuc);
            return db.MyExecuteQuery(sqlString);
        }

        public DataTable GetThongKeTraGop(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            string sqlString = string.Format("EXEC spThongKePhieuTraGop  N'{0}', N'{1}'", NgayBatDau, NgayKetThuc);
            return db.MyExecuteQuery(sqlString);
        }

        public DataTable GetThongKeChuocDo(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            string sqlString = string.Format("EXEC spThongKePhieuChuocDo  N'{0}', N'{1}'", NgayBatDau, NgayKetThuc);
            return db.MyExecuteQuery(sqlString);
        }
    }
}
