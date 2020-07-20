using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.Data_Access_Object
{
    public class Customer
    {
        public string SocialID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Birth { get; set; }
        public string SocialPlace { get; set; }
        public string Gender { get; set; }
        public Customer(string maKH, string ten, string diaChi, string soDT,string ngaySinh, string noiCap,string gioiTinh)
        {
            this.SocialID = maKH;
            this.Name = ten;
            this.Address = diaChi;
            this.Phone = soDT;
            this.Birth = ngaySinh;
            this.SocialPlace = noiCap;
            this.Gender = gioiTinh;
        }
        public Customer(DataRow row)
        {
            this.SocialID = row["CMND"].ToString();
            this.Name = row["Họ và tên"].ToString();
            this.Address = row["Địa chỉ"].ToString();
            this.Phone = row["Số điện thoại"].ToString();
            this.Birth = row["Ngày sinh"].ToString();
            this.SocialPlace = row["Nơi cấp"].ToString();
            this.Gender = row["Giới tính"].ToString();
        }

    }
}
