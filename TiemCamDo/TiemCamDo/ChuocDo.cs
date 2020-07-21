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
    public partial class ChuocDo : Form
    {
        bool Them;
        string MaNV;
        bool IsAdmin;
        public ChuocDo(string manv, bool IsAdmin)
        {
            InitializeComponent();
            this.MaNV = manv;
            this.IsAdmin = IsAdmin;
        }
        private void resettext()
        {
            txtCMND.ResetText();
            txtTen.ResetText();
            txtSoTienCam.ResetText();
            txtChiTiet.ResetText();
            txtMaPhieuChuoc.ResetText();
            dtpNgayCam.ResetText();
            txtSoTienChuoc.ResetText();
        }
        private void Enabletxt(bool t)
        {
            txtMaPhieuChuoc.Enabled = t;
            dtpNgayCam.Enabled = t;
            txtSoTienCam.Enabled = t;
            txtChiTiet.Enabled = t;
            dtpNgayChuoc.Enabled = t;

        }
        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvKH.CurrentCell.RowIndex;
            txtCMND.Text = dgvKH.Rows[r].Cells["SocialID"].Value.ToString();
            txtTen.Text = dgvKH.Rows[r].Cells["Name"].Value.ToString();
            dgvCamDo.DataSource = BLCamDo.Instance.GetCDByCMND(txtCMND.Text);

        }

        private void ChuocDo_Load(object sender, EventArgs e)
        {
            dgvKH.DataSource = BLKhachHang.Instance.GetKH();
            dgvKH.ReadOnly = true;
            dgvKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgvChuocDo.DataSource=chd.GetPhieuChuoc();
            dgvCamDo.ReadOnly = true;
            dgvCamDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            resettext();
            dgvMonHang.ReadOnly = true;
            dgvMonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnEdit.Enabled = true;
            btnDel.Enabled = true;
            btnExit.Enabled = true;
            
            //txtCMND.Enabled = false;
            //txtMaHang.Enabled = false;
            //txtLoaiHang.Enabled = false;
            //txtChiTiet.Enabled = false;
            //txtGiaTri.Enabled = false;
        }

        private void dgvCamDo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaPhieuChuoc.Clear();
                this.dtpNgayChuoc.Value = DateTime.Now;
                int r = dgvCamDo.CurrentCell.RowIndex;
                this.txtMaPhieu.Text = dgvCamDo.Rows[r].Cells["ID"].Value.ToString();
                this.dtpNgayCam.Text = dgvCamDo.Rows[r].Cells["PawnDate"].Value.ToString();
                this.txtSoTienCam.Text = dgvCamDo.Rows[r].Cells["GetMoney"].Value.ToString();
                txtChiTiet.Text = dgvCamDo.Rows[r].Cells["ProductName"].Value.ToString();
                txtSoTienChuoc.Text= dgvCamDo.Rows[r].Cells["Debt"].Value.ToString();
                dgvMonHang.DataSource = BLChuocDo.Instance.GetChDByMaPhieu(txtMaPhieu.Text);
                if (txtSoTienChuoc.Text == "0")
                {
                    txtMaPhieuChuoc.Enabled = false;
                    txtSoTienChuoc.Enabled = false;
                    dtpNgayChuoc.Enabled = false;
                }
                else
                {
                    txtMaPhieuChuoc.Enabled = true;
                    txtSoTienChuoc.Enabled = true;
                    dtpNgayChuoc.Enabled = true;
                }
            }
            catch { }
        }



        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh    
                // Lấy thứ tự record hiện hành     
                int r = dgvMonHang.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành       
                string str = dgvMonHang.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL  

                // Hiện thông báo xác nhận việc xóa mẫu tin       
                // Khai báo biến traloi            
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp    
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?           
                if (traloi == DialogResult.Yes)
                {
                    if (BLChuocDo.Instance.DeleteChD(str))
                    {
                        // Cập nhật lại DataGridView                
                        dgvMonHang.DataSource = BLChuocDo.Instance.GetChDByMaPhieu(txtMaPhieu.Text);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if ((!txtMaPhieuChuoc.Text.Trim().Equals("")))
                {
                    try
                    {
                        if (BLChuocDo.Instance.InsertChD(txtMaPhieuChuoc.Text, dtpNgayChuoc.Value, txtSoTienChuoc.Text, txtMaPhieu.Text,MaNV))
                        {
                            // Load lại dữ liệu trên DataGridView     
                            dgvMonHang.DataSource = BLChuocDo.Instance.GetChDByMaPhieu(txtMaPhieu.Text);
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
                if (BLChuocDo.Instance.UpdateChD(txtMaPhieuChuoc.Text, dtpNgayChuoc.Value, txtSoTienChuoc.Text, txtMaPhieu.Text,MaNV))
                {
                    // Load lại dữ liệu trên DataGridView      
                    dgvMonHang.DataSource = BLChuocDo.Instance.GetChDByMaPhieu(txtMaPhieu.Text);
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
            txtMaPhieuChuoc.Enabled = false;
            dtpNgayChuoc.Focus();
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
            txtMaPhieuChuoc.Focus();
        }


        private void dgvMonHang_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
                int r = dgvMonHang.CurrentCell.RowIndex;
                txtMaPhieuChuoc.Clear();
                txtMaPhieuChuoc.Text = dgvMonHang.Rows[r].Cells["ID"].Value.ToString();
                dtpNgayChuoc.Text = dgvMonHang.Rows[r].Cells["RegainDate"].Value.ToString();
                txtSoTienChuoc.Text = dgvMonHang.Rows[r].Cells["Money"].Value.ToString();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Hide();
            f1.ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu f1 = new MainMenu(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
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
    }
}
