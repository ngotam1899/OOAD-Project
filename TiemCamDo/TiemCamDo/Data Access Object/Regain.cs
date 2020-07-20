using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.Data_Access_Object
{
    /*Chuộc đồ*/
    public class Regain
    {
        public string ID { get; set; }
        public string PawnID { get; set; }
        public string RegainDate { get; set; }
        public string Money { get; set; }
        public string EmployeeID { get; set; }
        public Regain(string maPhieuChuoc,string maPhieuCam, string ngayChuoc, string soTienChuoc,string maNV)
        {
            this.ID = maPhieuChuoc;
            this.PawnID = maPhieuCam;
            this.RegainDate = ngayChuoc;
            this.Money = soTienChuoc;
            this.EmployeeID = maNV;
        }
        public Regain(DataRow row)
        {
            this.ID = row["Mã phiếu chuộc"].ToString();
            this.PawnID = row["Mã phiếu cầm"].ToString();
            this.RegainDate = row["Ngày chuộc"].ToString();
            this.Money = row["Số tiền chuộc"].ToString();
            this.EmployeeID = row["Mã nhân viên"].ToString();
        }
    }
}
