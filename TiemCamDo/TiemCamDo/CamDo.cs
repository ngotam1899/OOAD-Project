using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiemCamDo.DB_Layer;
using TiemCamDo.BD_Layer;
using TiemCamDo.Facade;
using System.Data.SqlClient;


namespace TiemCamDo
{
    public partial class CamDo : Form
    {
        bool Them;
        string MaNV;
        bool IsAdmin;
        public CamDo(string manv, bool IsAdmin)
        {
            
            InitializeComponent();
            this.MaNV = manv;
            this.IsAdmin = IsAdmin;
        }

        private void Enabletxt(bool t)
        {
            txtMaPhieu.Enabled = t;
            dtpNgayCam.Enabled = t;
            txtSoTienCam.Enabled = t;
            txtLaiSuat.Enabled = t;
            dtpNgayChuoc.Enabled = t;
            txtCMND.Enabled = t;
            txtGiaTri.Enabled = t;
            txtMaHang.Enabled = t;
            txtLoaiHang.Enabled = t;
            txtChiTiet.Enabled = t;
        }
        private void resettext()
        {
            
            txtCMND.ResetText();
            resetMatHang();
            resetCamDo();
        }
        private void resetMatHang()
        {
            txtGiaTri.ResetText();
            txtMaHang.ResetText();
            txtLoaiHang.ResetText();
            txtChiTiet.ResetText();
        }
        private void resetCamDo()
        {
            txtMaPhieu.ResetText();
            dtpNgayCam.ResetText();
            txtSoTienCam.ResetText();
            txtLaiSuat.ResetText();
            dtpNgayChuoc.ResetText();
        }
        private void LoadKhachHang()
        {
            dgvKH.DataSource = BLKhachHang.Instance.GetKH();
            dgvKH.AllowUserToAddRows = false;
            dgvKH.ReadOnly = true;
            dgvKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Enabletxt(false);
            resettext();
        }
        private void CamDo_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
            dgvCamDo.ReadOnly = true;
            dgvCamDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            rdbTen.Checked = true;
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = false;
            btnEdit.Enabled = false;
            btnExit.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            Enabletxt(true);
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            btnUpdate.Enabled = true;
            btnHuy.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            btnThem.Enabled = false;
            btnEdit.Enabled = false;
            btnExit.Enabled = false;

            txtMaPhieu.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if ((!txtMaPhieu.Text.Trim().Equals("")))
                {
                    try
                    {
                        if (BLMatHang.Instance.InsertMH(txtMaHang.Text, txtLoaiHang.Text, txtChiTiet.Text, txtGiaTri.Text, txtCMND.Text))
                        {
                            BLCamDo.Instance.InsertCD(txtMaPhieu.Text, txtMaHang.Text, dtpNgayCam.Value, dtpNgayChuoc.Value,  txtSoTienCam.Text, txtLaiSuat.Text,  MaNV);
                            // Load lại dữ liệu trên DataGridView     
                            dgvCamDo.DataSource = BLCamDo.Instance.GetCDByMaHang(txtMaHang.Text);
                            dgvMonHang.DataSource = BLMatHang.Instance.GetMHByCMND(txtCMND.Text);
                            Enabletxt(false);
                            resettext();
                            //// Không cho thao tác trên các nút Lưu / Hủy
                            btnUpdate.Enabled = false;
                            btnHuy.Enabled = false;
                            //// Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
                            btnThem.Enabled = true;
                            btnEdit.Enabled = true;
                            btnExit.Enabled = true;
                            // Thông báo         
                            MessageBox.Show("Đã thêm xong!");
                        }
                    }
                    catch (SqlException)
                    {
                       MessageBox.Show("Không thêm được. Lỗi rồi!");
                    }
                }
                else
                    MessageBox.Show("Vui lòng điền thông tin");
            }
            else
            {
                if (BLCamDo.Instance.UpdateCD(txtMaPhieu.Text, txtMaHang.Text, dtpNgayCam.Value, dtpNgayChuoc.Value, txtSoTienCam.Text, txtLaiSuat.Text,MaNV)
                    && BLMatHang.Instance.UpdateMH(txtMaHang.Text, txtLoaiHang.Text, txtChiTiet.Text, txtGiaTri.Text, txtCMND.Text))
                {
                    // Load lại dữ liệu trên DataGridView      
                    dgvCamDo.DataSource = BLCamDo.Instance.GetCDByMaHang(txtMaHang.Text);
                    dgvMonHang.DataSource = BLMatHang.Instance.GetMHByCMND((txtCMND.Text));
                    Enabletxt(false);
                    resettext();
                    //// Không cho thao tác trên các nút Lưu / Hủy
                    btnUpdate.Enabled = false;
                    btnHuy.Enabled = false;
                    //// Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
                    btnThem.Enabled = true;
                    btnEdit.Enabled = true;
                    btnExit.Enabled = true;
                    // Thông báo              
                    MessageBox.Show("Đã sửa xong!");
                }
            }
            // Đóng kết nối     
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa
            Them = false;
            // Cho phép thao tác trên Panel
            Enabletxt(true);
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            btnUpdate.Enabled = true;
            btnHuy.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            btnThem.Enabled = false;
            btnEdit.Enabled = false;
            btnExit.Enabled = false;
            // Đưa con trỏ đến TextField 
            txtCMND.Enabled = false;
            dtpNgayCam.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadKhachHang();
            // Xóa trống các đối tượng trong Panel
            resettext();
            Enabletxt(false);
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            btnThem.Enabled = false;
            //btnEdit.Enabled = true;
            btnExit.Enabled = false;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Khai báo biến traloi
            DialogResult traloi;
            // Hiện hộp thoại hỏi đáp
            traloi = MessageBox.Show("Chắc không?", "Trả lời",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            // Kiểm tra có nhắp chọn nút Ok không?
            if (traloi == DialogResult.OK) this.Close();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            resettext();
            if (rdbSDT.Checked) //tìm theo mã SV
            {
                dgvKH.DataSource = BLKhachHang.Instance.SearchKHBySDT(txtSearch.Text.Trim());
                dgvCamDo.DataSource = null;
                dgvMonHang.DataSource = null;
            }
            else   //tìm theo Họ Tên SV
            {
                dgvKH.DataSource = BLKhachHang.Instance.SearchKHByTen(txtSearch.Text.Trim());
                dgvCamDo.DataSource = null;
                dgvMonHang.DataSource = null;
            }
            btnHuy.Enabled = true;
        }

 

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Hide();
            f1.ShowDialog();
        }

        private void dgvKH_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvKH.CurrentCell.RowIndex;
            txtCMND.Text = dgvKH.Rows[r].Cells["SocialID"].Value.ToString();
            dgvMonHang.DataSource = BLMatHang.Instance.GetMHByCMND(txtCMND.Text);
            dgvCamDo.DataSource = null;
            resetMatHang();
            resetCamDo();
        }

        private void dgvCamDo_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvCamDo.CurrentCell.RowIndex;
            this.txtMaPhieu.Text = dgvCamDo.Rows[r].Cells["ID"].Value.ToString();
            this.dtpNgayCam.Text = dgvCamDo.Rows[r].Cells["PawnDate"].Value.ToString();
            this.dtpNgayChuoc.Text = dgvCamDo.Rows[r].Cells["RegainDate"].Value.ToString();
            this.txtSoTienCam.Text = dgvCamDo.Rows[r].Cells["GetMoney"].Value.ToString();
            this.txtLaiSuat.Text = dgvCamDo.Rows[r].Cells["Interest"].Value.ToString();
            btnEdit.Enabled = true;
        }

        private void dgvMonHang_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvMonHang.CurrentCell.RowIndex;
            txtMaHang.Text = dgvMonHang.Rows[r].Cells["ID"].Value.ToString();
            txtLoaiHang.Text = dgvMonHang.Rows[r].Cells["Type"].Value.ToString();
            txtChiTiet.Text = dgvMonHang.Rows[r].Cells["Name"].Value.ToString();
            txtCMND.Text = dgvMonHang.Rows[r].Cells["SocialID"].Value.ToString();
            txtGiaTri.Text = dgvMonHang.Rows[r].Cells["Price"].Value.ToString();
            dgvCamDo.DataSource = BLCamDo.Instance.GetCDByMaHang(txtMaHang.Text);
            resetCamDo();
            this.btnThem.Enabled = true;
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            ReportCamDo rp = new ReportCamDo(this.txtMaHang.Text, this.txtCMND.Text, this.txtMaPhieu.Text);
            rp.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu f1 = new MainMenu(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void txtLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
