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
            txtMaPhieu.ResetText();
            dtpNgayCam.ResetText();
            txtSoTienCam.ResetText();
            txtLaiSuat.ResetText();
            dtpNgayChuoc.ResetText();
            txtCMND.ResetText();
            txtGiaTri.ResetText();
            txtMaHang.ResetText();
            txtLoaiHang.ResetText();
            txtChiTiet.ResetText();
        }
        private void CamDo_Load(object sender, EventArgs e)
        {
            dgvKH.DataSource = BLKhachHang.Instance.GetKH();
            dgvKH.AllowUserToAddRows = false;
            dgvKH.ReadOnly = true;
            dgvKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCamDo.ReadOnly = true;
            dgvCamDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Enabletxt(false);
            resettext();
            rdbTen.Checked = true;
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnEdit.Enabled = true;
            btnDel.Enabled = true;
            btnExit.Enabled = true;
            txtCMND.Enabled = false;
            txtMaHang.Enabled = false;
            txtLoaiHang.Enabled = false;
            txtChiTiet.Enabled = false;
            txtGiaTri.Enabled = false;


        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            Enabletxt(true);
            resettext();
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            btnUpdate.Enabled = true;
            btnHuy.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            btnThem.Enabled = false;
            btnEdit.Enabled = false;
            btnDel.Enabled = false;
            btnExit.Enabled = false;

            txtMaPhieu.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if ((!txtMaPhieu.Text.Trim().Equals("")))
                {
                    //try
                    //{
                        if (BLMatHang.Instance.InsertMH(txtMaHang.Text, txtLoaiHang.Text, txtChiTiet.Text, txtGiaTri.Text, txtCMND.Text))
                        {
                            BLCamDo.Instance.InsertCD(txtMaPhieu.Text, txtMaHang.Text, dtpNgayCam.Value, dtpNgayChuoc.Value, txtSoTienCam.Text, txtLaiSuat.Text, MaNV);
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
                            btnDel.Enabled = true;
                            btnExit.Enabled = true;
                            // Thông báo         
                            MessageBox.Show("Đã thêm xong!");
                        }
                    //}
                    //catch (SqlException)
                    //{
                    //    MessageBox.Show("Không thêm được. Lỗi rồi!");
                    //}
                }
                else
                    MessageBox.Show("Vui lòng điền thông tin");
            }
            else
            {
                if (BLCamDo.Instance.UpdateCD(txtMaPhieu.Text, txtMaHang.Text, dtpNgayCam.Value, dtpNgayChuoc.Value, txtSoTienCam.Text, txtLaiSuat.Text, MaNV)
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
                    btnDel.Enabled = true;
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
            btnDel.Enabled = false;
            btnExit.Enabled = false;
            // Đưa con trỏ đến TextField 
            txtMaPhieu.Enabled = false;
            dtpNgayCam.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel
            resettext();
            Enabletxt(false);
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            btnThem.Enabled = true;
            btnEdit.Enabled = true;
            btnDel.Enabled = true;
            btnExit.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh    
                // Lấy thứ tự record hiện hành     
                int r = dgvCamDo.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành       
                string str = dgvCamDo.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL  

                // Hiện thông báo xác nhận việc xóa mẫu tin       
                // Khai báo biến traloi            
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp    
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?           
                if (traloi == DialogResult.Yes)
                {
                    if (BLCamDo.Instance.DeleteCD(str))
                    {
                        // Cập nhật lại DataGridView                
                        dgvCamDo.DataSource = BLCamDo.Instance.GetCDByMaHang(txtMaHang.Text);
                        Enabletxt(false);
                        resettext();
                        //// Không cho thao tác trên các nút Lưu / Hủy
                        btnUpdate.Enabled = false;
                        btnHuy.Enabled = false;
                        //// Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
                        btnThem.Enabled = true;
                        btnEdit.Enabled = true;
                        btnDel.Enabled = true;
                        btnExit.Enabled = true;
                        // Thông báo           
                        MessageBox.Show("Đã xóa xong!");
                    }
                    else
                    {
                        MessageBox.Show("Xóa không được,Vui lòng chọn môn muốn xóa");
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi");
            }
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
            }
            else   //tìm theo Họ Tên SV
            {
                dgvKH.DataSource = BLKhachHang.Instance.SearchKHByTen(txtSearch.Text.Trim());
            }
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
            txtCMND.Text = dgvKH.Rows[r].Cells["CMND"].Value.ToString();
            dgvMonHang.DataSource = BLMatHang.Instance.GetMHByCMND(txtCMND.Text);
        }

        private void dgvCamDo_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvCamDo.CurrentCell.RowIndex;
            this.txtMaPhieu.Text = dgvCamDo.Rows[r].Cells["Mã phiếu cầm"].Value.ToString();
            this.dtpNgayCam.Text = dgvCamDo.Rows[r].Cells["Ngày cầm đồ"].Value.ToString();
            this.dtpNgayChuoc.Text = dgvCamDo.Rows[r].Cells["Ngày quá hạn"].Value.ToString();
            this.txtSoTienCam.Text = dgvCamDo.Rows[r].Cells["Số tiền cầm"].Value.ToString();
            this.txtLaiSuat.Text = dgvCamDo.Rows[r].Cells["Lãi suất"].Value.ToString();
        }

        private void dgvMonHang_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvMonHang.CurrentCell.RowIndex;
            txtMaHang.Text = dgvMonHang.Rows[r].Cells["Mã hàng"].Value.ToString();
            txtLoaiHang.Text = dgvMonHang.Rows[r].Cells["Loại hàng"].Value.ToString();
            txtChiTiet.Text = dgvMonHang.Rows[r].Cells["Tên món hàng"].Value.ToString();
            txtCMND.Text = dgvMonHang.Rows[r].Cells["CMND"].Value.ToString();
            txtGiaTri.Text = dgvMonHang.Rows[r].Cells["Gía trị thực"].Value.ToString();
            dgvCamDo.DataSource = BLCamDo.Instance.GetCDByMaHang(txtMaHang.Text);
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
