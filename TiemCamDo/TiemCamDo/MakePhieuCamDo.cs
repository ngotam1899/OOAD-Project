using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiemCamDo.BD_Layer;

namespace TiemCamDo
{
    public partial class MakePhieuCamDo : Form
    {
        BLKhachHang kh = new BLKhachHang();
        BLCamDo cd = new BLCamDo();
        BLMatHang mh = new BLMatHang();
        bool Them;
        string MaNV;
        public MakePhieuCamDo()
        {
            InitializeComponent();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvKhachHang.CurrentCell.RowIndex;
            txtCMND.Text = dgvKhachHang.Rows[r].Cells["CMND"].Value.ToString();
            txtTenKH.Text = dgvKhachHang.Rows[r].Cells["Họ và tên"].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.Rows[r].Cells["Địa chỉ"].Value.ToString();
            txtNoiCap.Text = dgvKhachHang.Rows[r].Cells["Nơi cấp"].Value.ToString();
            txtSoDT.Text = dgvKhachHang.Rows[r].Cells["Số điện thoại"].Value.ToString();
            dtpNgaySinh.Text = dgvKhachHang.Rows[r].Cells["Ngày sinh"].Value.ToString();
            if (dgvKhachHang.Rows[r].Cells["Giới tính"].Value.ToString() == "Nam") rdbNam.Checked = true; else rdbNam.Checked = false;
            if (dgvKhachHang.Rows[r].Cells["Giới tính"].Value.ToString() == "Nữ") rdbNu.Checked = true; else rdbNu.Checked = false;
        }

        private void MakePhieuCamDo_Load(object sender, EventArgs e)
        {
            dgvKhachHang.DataSource = kh.GetKH();
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.ReadOnly = true;
            btnThemMH.Enabled = false;
            btnThemPhieu.Enabled = false;
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            btnThemMH.Enabled = true;
            if ((!txtCMND.Text.Trim().Equals("")))
            {
                try
                {
                    if (kh.InsertKH(txtCMND.Text, txtTenKH.Text, txtDiaChi.Text, txtSoDT.Text, dtpNgaySinh.Value.Date, txtNoiCap.Text, (rdbNam.Checked) ? "Nam" : "Nữ"))
                    {
                        // Load lại dữ liệu trên DataGridView     
                        dgvKhachHang.DataSource = kh.GetKH();
                        //// Không cho thao tác trên các nút Lưu / Hủy
                        // Thông báo         
                        MessageBox.Show("Đã thêm xong!");
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }

            }
        }

        private void btnThemMH_Click(object sender, EventArgs e)
        {
            //if (mh.InsertMH(txtMaHang.Text, txtLoaiHang.Text, txtTenHang.Text, txtGiaTri.Text, txtTinhTrang.Text, txtCMND.Text))
            //{
            //    // Load lại dữ liệu trên DataGridView     
            //}
        }
    }
}
