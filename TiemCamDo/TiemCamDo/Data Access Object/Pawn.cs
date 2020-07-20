using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.Data_Access_Object
{
    /*Cầm đồ*/
    public class Pawn
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string PawnDate { get; set; }
        public string RegainDate { get; set; }
        public string GetMoney { get; set; }
        public string Interest { get; set; }
        public string EmployeeID { get; set; }
        public string Debt { get; set; }
        public string ProductName { get; set; }
        public Pawn(string maPhieuCam, string maHang, string ngayCam, string ngayChuoc, string soTienCam, string laiXuat, string duNo, string tenHang, string maNV )
        {
            this.ID = maPhieuCam;
            this.ProductID = maHang;
            this.PawnDate = ngayCam;
            this.RegainDate = ngayChuoc;
            this.GetMoney = soTienCam;
            this.Interest = laiXuat;
            this.EmployeeID = maNV;
            this.Debt = duNo;
            this.ProductName = tenHang;
        }
        public Pawn(DataRow row)
        {
            this.ID = row["Mã phiếu cầm"].ToString();
            this.ProductID = row["Mã hàng"].ToString();
            this.PawnDate = row["Ngày cầm đồ"].ToString();
            this.RegainDate = row["Ngày quá hạn"].ToString();
            this.GetMoney = row["Số tiền cầm"].ToString();
            this.Interest = row["Lãi suất"].ToString();
            this.EmployeeID = row["Mã NV"].ToString();
            this.Debt = row["Số tiền dư nợ"].ToString();
            this.ProductName = row["Tên món hàng"].ToString();
        }
    }
}
