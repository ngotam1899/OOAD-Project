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
    public partial class TraGop : Form
    {
        bool Them;
        string MaNV;
        bool IsAdmin;
        public TraGop(string manv, bool isAdmin)
        {
            InitializeComponent();
            this.MaNV = manv;
            this.IsAdmin = isAdmin;
        }
        private void resettext()
        {
            txtCMND.ResetText();
            //txtTen.ResetText();
            txtSoTienCam.ResetText();
            //txtChiTiet.ResetText();
            txtTienDuNo.ResetText();
            dtpNgayCam.ResetText();
            txtTienTraGop.ResetText();
        }
        private void Enabletxt(bool t)
        {
            txtTienDuNo.Enabled = t;
            dtpNgayCam.Enabled = t;
            txtSoTienCam.Enabled = t;
            //txtChiTiet.Enabled = t;
            dtpNgayTraGop.Enabled = t;

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
            txtMaTraGop.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if ((!txtMaTraGop.Text.Trim().Equals("")))
                {
                    
                        if (BLTraGop.Instance.InsertTraGop(txtMaTraGop.Text, dtpNgayTraGop.Value, txtTienTraGop.Text, txtTienDuNo.Text, txtMaPhieu.Text, MaNV))
                        {
                            // Load lại dữ liệu trên DataGridView     
                            dgvTraGop.DataSource = BLTraGop.Instance.GetTraGopByMaPhieuCam(txtMaPhieu.Text);
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
                else
                    MessageBox.Show("Vui lòng điền thông tin");
            }
            else
            {
                if (BLTraGop.Instance.UpdateTraGop(txtMaTraGop.Text, dtpNgayTraGop.Value, txtTienTraGop.Text,txtTienDuNo.Text, txtMaPhieu.Text, MaNV))
                {
                    // Load lại dữ liệu trên DataGridView      
                    dgvTraGop.DataSource = BLTraGop.Instance.GetTraGopByMaPhieuCam(txtMaPhieu.Text);
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
            txtMaTraGop.Enabled = false;
            dtpNgayTraGop.Focus();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh    
                // Lấy thứ tự record hiện hành     
                int r = dgvTraGop.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành       
                string str = dgvTraGop.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL  

                // Hiện thông báo xác nhận việc xóa mẫu tin       
                // Khai báo biến traloi            
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp    
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?           
                if (traloi == DialogResult.Yes)
                {
                    if (BLTraGop.Instance.DeleteTraGop(str))
                    {
                        // Cập nhật lại DataGridView                
                        dgvTraGop.DataSource = BLTraGop.Instance.GetTraGopByMaPhieuCam(txtMaPhieu.Text);
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

        private void TraGop_Load(object sender, EventArgs e)
        {
            dgvKH.DataSource = BLKhachHang.Instance.GetKH();
            dgvKH.ReadOnly = true;
            dgvKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgvChuocDo.DataSource=chd.GetPhieuChuoc();
            dgvCamDo.ReadOnly = true;
            dgvCamDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            resettext();
            dgvTraGop.ReadOnly = true;
            dgvTraGop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnEdit.Enabled = true;
            btnDel.Enabled = true;
            btnExit.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Hide();
            f1.ShowDialog();
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvKH.CurrentCell.RowIndex;
            txtCMND.Text = dgvKH.Rows[r].Cells["SocialID"].Value.ToString();
            dgvCamDo.DataSource = BLCamDo.Instance.GetCDByCMND(txtCMND.Text);
        }

        private void dgvCamDo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvCamDo.CurrentCell.RowIndex;
                this.txtMaTraGop.Clear();
                this.txtTienTraGop.Clear();
                this.dtpNgayTraGop.Value = DateTime.Now;
                this.txtMaPhieu.Text = dgvCamDo.Rows[r].Cells["ID"].Value.ToString();
                this.dtpNgayCam.Text = dgvCamDo.Rows[r].Cells["PawnDate"].Value.ToString();
                this.txtSoTienCam.Text = dgvCamDo.Rows[r].Cells["GetMoney"].Value.ToString();
                dgvTraGop.DataSource = BLTraGop.Instance.GetTraGopByMaPhieuCam(txtMaPhieu.Text);
                txtTenMon.Text = dgvCamDo.Rows[r].Cells["ProductName"].Value.ToString();
                txtTienDuNo.Text = dgvCamDo.Rows[r].Cells["Debt"].Value.ToString();
                this.txtMaHang.Text = dgvCamDo.Rows[r].Cells["ProductID"].Value.ToString();
                if (txtTienDuNo.Text == "0")
                {
                    txtMaTraGop.Enabled = false;
                    txtTienTraGop.Enabled = false;
                    dtpNgayTraGop.Enabled = false;
                }
                else
                {
                    txtMaTraGop.Enabled = true;
                    txtTienTraGop.Enabled = true;
                    dtpNgayTraGop.Enabled = true;
                }
            }
            catch { }
        }

        private void dgvTraGop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvTraGop.CurrentCell.RowIndex;
            txtMaTraGop.Text = dgvTraGop.Rows[r].Cells["ID"].Value.ToString();
            dtpNgayTraGop.Text = dgvTraGop.Rows[r].Cells["PayDate"].Value.ToString();
            txtTienTraGop.Text = dgvTraGop.Rows[r].Cells["Money"].Value.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu f1 = new MainMenu(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            ReportTraGop rp = new ReportTraGop(this.txtMaHang.Text,this.txtMaTraGop.Text, this.txtCMND.Text, this.txtMaPhieu.Text);
            rp.ShowDialog();
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





        //
    }
}
