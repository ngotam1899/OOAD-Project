using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.Data_Access_Object
{
    class Installment
    {
        /*Trả góp*/
        public string ID { get; set; }
        public string PayDate { get; set; }
        public string PawnID { get; set; }
        public string Money { get; set; }
        public string Debt { get; set; }
        public string EmployeeID { get; set; }
        public Installment(string maTraGop,string maPhieuCam, string ngayTraGop, string soTienTra,string soTienNo, string maNV)
        {
            this.ID = maTraGop;
            this.PawnID = maPhieuCam;
            this.PayDate = ngayTraGop;
            this.Money = soTienTra;
            this.Debt = soTienNo;
            this.EmployeeID = maNV;
        }
        public Installment(DataRow row)
        {
            this.ID = row["Mã trả góp"].ToString();
            this.PawnID = row["Mã phiếu cầm"].ToString();
            this.PayDate = row["Ngày trả góp"].ToString();
            this.Money = row["Số tiền khách trả"].ToString();
            this.Debt = row["Số tiền dư nợ"].ToString();
            this.EmployeeID = row["Mã nhân viên"].ToString();
        }
    }
}
