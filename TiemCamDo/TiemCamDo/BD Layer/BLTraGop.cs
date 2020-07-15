using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.DB_Layer;

namespace TiemCamDo.BD_Layer
{
    class BLTraGop
    {
        public DataTable GetTraGopByMaPhieuCam(string MaPhieu)
        {
            string sqlString = string.Format("EXEC spLoadTraGopByCamDo N'{0}'", MaPhieu);
            return DBMain.Instance.MyExecuteQuery(sqlString);
        }
        //Show Thông tin khách hàng
        //Show thông tin phieu cam đồ
        //Lấy ra lãi xuất
        //Thêm phiếu trả lãi
        //Tổng tiền lãi đã trả trước đó. Nếu chưa trả lãi bao giờ thì = 0
        public bool DeleteTraGop(string MaTraGop)
        {
            string sqlString = string.Format("EXEC spDeleteTraGop N'{0}'", MaTraGop);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool InsertTraGop(string MaTraGop, DateTime NgayTraGop, string SoTienKhachTra, string SoTienDuNo,string MaPhieu, string MaNV)
        {
            string sqlString =
           string.Format("EXEC spInsertTraGop N'{0}',N'{1}',N'{2}',N'{3}',N'{4}', N'{5}'", MaTraGop, NgayTraGop, SoTienKhachTra, SoTienDuNo, MaPhieu, MaNV);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool UpdateTraGop(string MaTraGop, DateTime NgayTraGop, string SoTienKhachTra, string SoTienDuNo,string MaPhieu, string MaNV)
        {
            string sqlString =
            string.Format("EXEC spUpdateTraGop N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}'", MaTraGop, NgayTraGop, SoTienKhachTra, SoTienDuNo,MaPhieu, MaNV);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        //
    }
}
