using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.BD_Layer
{
    class NhanVienBuilder:INhanVien
    {
        public string MaNV { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string Quyen { get; set; }

        public NhanVienBuilder InsertDiaChi(string DiaChi)
        {
            this.DiaChi = DiaChi;
            return this;
        }

        public NhanVienBuilder InsertEmail(string Email)
        {
            this.Email = Email;
            return this;
        }

        public NhanVienBuilder InsertGioiTinh(string GioiTinh)
        {
            this.GioiTinh = GioiTinh;
            return this;
        }

        public NhanVienBuilder InsertMaNV(string MaNV)
        {
            this.MaNV = MaNV;
            return this;
        }

        public NhanVienBuilder InsertMatKhau(string MatKhau)
        {
            this.MatKhau = MatKhau;
            return this;
        }

        public NhanVienBuilder InsertQuyen(string Quyen)
        {
            this.Quyen = Quyen;
            return this;
        }

        public NhanVienBuilder InsertSoDT(string SoDT)
        {
            this.SoDT = SoDT;
            return this;
        }

        public NhanVienBuilder InsertTen(string Ten)
        {
            this.Ten = Ten;
            return this;
        }
        public Employee Build()
        {
            return new Employee(MaNV, Email, MatKhau, Ten, GioiTinh, SoDT, DiaChi, Quyen);
        }
    }
}
