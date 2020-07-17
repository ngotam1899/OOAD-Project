using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.BD_Layer
{
    public class Employee
    {
        public string MaNV { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string Quyen { get; set; }
        public Employee(string MaNV, string Email, string MatKhau, string Ten, string GioiTinh, string SoDT, string DiaChi, string Quyen)
        {
            this.MaNV = MaNV;
            this.Email = Email;
            this.MatKhau = MatKhau;
            this.Ten = Ten;
            this.GioiTinh = GioiTinh;
            this.SoDT = SoDT;
            this.DiaChi = DiaChi;
            this.Quyen = Quyen;
        }
        public Employee(DataRow row)
        {
            this.MaNV = row["Mã NV"].ToString();
            this.Email = row["Email"].ToString();
            this.MatKhau = row["Mật khẩu"].ToString();
            this.Ten = row["Họ và tên"].ToString();
            this.GioiTinh = row["Giới tính"].ToString();
            this.SoDT = row["Số điện thoại"].ToString();
            this.DiaChi = row["Địa chỉ"].ToString();
            this.Quyen = row["Quyền"].ToString();
        }
    }
}
