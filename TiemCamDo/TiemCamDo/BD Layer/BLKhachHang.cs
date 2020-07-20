 using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.DB_Layer;
using TiemCamDo.Data_Access_Object;

namespace TiemCamDo.BD_Layer
{
    class BLKhachHang
    {
        private static BLKhachHang instance;
        public static BLKhachHang Instance
        {
            get { if (instance == null) instance = new BLKhachHang(); return BLKhachHang.instance; }
            private set { BLKhachHang.instance = value; }
        }
        private BLKhachHang() { }
        public List<Customer> GetKH()
        {
            List<Customer> customers = new List<Customer>();
            string sqlString = string.Format("EXEC spLoadKhachHang");
            DataTable data = DBMain.Instance.MyExecuteQuery(sqlString);
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customers.Add(customer);
            }
            return customers;
        }
        //public DataTable GetKH()/
        //{
        //    string sqlString = "EXEC spLoadKhachHang";
        //    return DBMain.Instance.MyExecuteQuery(sqlString);
        //}
        public DataTable GetCMNDTen()
        {
            string sqlString = "EXEC spLoadCMNDTen";
            return DBMain.Instance.MyExecuteQuery(sqlString);
             
        } 
        public bool DeleteKH(string CMND)
        {
            string sqlString = string.Format("EXEC spDeleteKhachHang N'{0}'", CMND);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool InsertKH(string CMND, string Ten, string DiaChi, string SoDT, DateTime NgaySinh, string NoiCap, string GioiTinh)
        {
            string sqlString =
           string.Format("EXEC spInsertKhachHang N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}'", CMND, Ten, DiaChi, SoDT, NgaySinh, NoiCap, GioiTinh);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public bool UpdateKH(string CMND, string Ten, string DiaChi, string SoDT, DateTime NgaySinh, string NoiCap, string GioiTinh)
        {
            string sqlString =
            string.Format("EXEC spUpdateKhachHang N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}'", CMND, Ten, DiaChi, SoDT, NgaySinh, NoiCap, GioiTinh);
            int result = DBMain.Instance.MyExecuteNonQuery(sqlString);
            return result > 0;
        }
        public List<Customer> SearchKHBySDT(string SDT)
        {
            List<Customer> customers = new List<Customer>();
            string sqlString = string.Format("EXEC spSearchKhachHangBySDT N'%{0}%'", SDT);
            DataTable data = DBMain.Instance.MyExecuteQuery(sqlString);
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customers.Add(customer);
            }
            return customers;
        }
        //public DataTable SearchKHBySDT(string SDT)
        //{

        //    string sqlString =
        //    string.Format("EXEC spSearchKhachHangBySDT N'%{0}%'", SDT);
        //    return DBMain.Instance.MyExecuteQuery(sqlString);
        //}
        //public DataTable SearchKHByTen(string Ten)
        //{
        //    string sqlString =
        //    string.Format("EXEC spSearchKhachHangByTen N'%{0}%'", Ten);
        //    return DBMain.Instance.MyExecuteQuery(sqlString);
        //}

        public List<Customer> SearchKHByTen(string Ten)
        {
            List<Customer> customers = new List<Customer>();
            string sqlString = string.Format("EXEC spSearchKhachHangByTen N'%{0}%'", Ten);
            DataTable data = DBMain.Instance.MyExecuteQuery(sqlString);
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customers.Add(customer);
            }
            return customers;
        }
    }
}
