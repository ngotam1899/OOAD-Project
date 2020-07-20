using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.Data_Access_Object
{
    public class Employee
    {
        public string EmployeeID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }   
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Authorize { get; set; }
        public Employee(string maNV, string email, string matKhau, string ten, string gioiTinh, string soDT, string diaChi, string quyen)
        {
            this.EmployeeID = maNV;
            this.Email = email;
            this.Password = matKhau;
            this.Name = ten;
            this.Gender = gioiTinh;
            this.Phone = soDT;
            this.Address = diaChi;
            this.Authorize = quyen;
        }
        public Employee(DataRow row)
        {
            this.EmployeeID = row["Mã NV"].ToString();
            this.Email = row["Email"].ToString();
            this.Password = row["Mật khẩu"].ToString();
            this.Name = row["Họ và tên"].ToString();
            this.Gender = row["Giới tính"].ToString();
            this.Phone = row["Số điện thoại"].ToString();
            this.Address = row["Địa chỉ"].ToString();
            this.Authorize = row["Quyền"].ToString();
        }
    }
}
