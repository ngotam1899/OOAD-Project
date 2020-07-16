using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.BD_Layer
{
    interface INhanVien
    {
        NhanVienBuilder InsertMaNV(string MaNV);
        NhanVienBuilder InsertEmail(string Email);
        NhanVienBuilder InsertMatKhau(string MatKhau);
        NhanVienBuilder InsertTen(string Ten);
        NhanVienBuilder InsertGioiTinh(string GioiTinh);
        NhanVienBuilder InsertSoDT(string SoDT);
        NhanVienBuilder InsertDiaChi(string DiaChi);
        NhanVienBuilder InsertQuyen(string Quyen);

        Employee Build();
    }
}
