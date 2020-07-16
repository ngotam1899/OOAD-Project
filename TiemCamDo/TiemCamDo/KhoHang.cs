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
    public partial class KhoHang : Form
    {
        string MaNV;
        bool Them;
        bool IsAdmin;
        public KhoHang(string MaNV, bool IsAdmin)
        {
        
            InitializeComponent();
            this.MaNV = MaNV;
            this.IsAdmin = IsAdmin;
        }

        private void Enabletxt(bool t)
        {
            txtMaHang.Enabled = t;
            txtLoaiHang.Enabled = t;
            txtChiTiet.Enabled = t;
            txtGiaTri.Enabled = t;
            
        }
        private void resettext()
        {
            txtMaHang.ResetText();
            txtLoaiHang.ResetText();
            txtChiTiet.ResetText();
            txtGiaTri.ResetText();
            
        }

        private void MatHang_Load(object sender, EventArgs e)
        {
            dgvMatHang.DataSource = BLMatHang.Instance.GetMH();
            rdbCMND.Checked = true;
            dgvMatHang.AllowUserToAddRows = false;
            dgvMatHang.ReadOnly = true;
            dgvMatHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Enabletxt(false);
            resettext();
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnEdit.Enabled = true;
            btnDel.Enabled = true;
            btnExit.Enabled = true;
            txtCMND.Enabled = false;
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
            // Đưa con trỏ đến TextField txtMaKH
            txtMaHang.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                if ((!txtMaHang.Text.Trim().Equals("")))
                {
                    try
                    {
                        if (BLMatHang.Instance.InsertMH(txtMaHang.Text, txtLoaiHang.Text, txtChiTiet.Text, txtGiaTri.Text, txtCMND.Text ))
                        {
                            // Load lại dữ liệu trên DataGridView     
                            dgvMatHang.DataSource = BLMatHang.Instance.GetMH();
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
                if (BLMatHang.Instance.UpdateMH(txtMaHang.Text, txtLoaiHang.Text, txtChiTiet.Text, txtGiaTri.Text, txtCMND.Text))
                {
                    // Load lại dữ liệu trên DataGridView      
                    dgvMatHang.DataSource = BLMatHang.Instance.GetMH();
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
            txtMaHang.Enabled = false;
            txtLoaiHang.Focus();
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
                int r = dgvMatHang.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành       
                string str = dgvMatHang.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL  

                // Hiện thông báo xác nhận việc xóa mẫu tin       
                // Khai báo biến traloi            
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp    
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?           
                if (traloi == DialogResult.Yes)
                {
                    if (BLMatHang.Instance.DeleteMH(str))
                    {
                        // Cập nhật lại DataGridView                
                        dgvMatHang.DataSource = BLMatHang.Instance.GetMH();
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

        private void dgvMatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvMatHang.CurrentCell.RowIndex;
                this.txtMaHang.Text = dgvMatHang.Rows[r].Cells["Mã hàng"].Value.ToString();
                this.txtLoaiHang.Text = dgvMatHang.Rows[r].Cells["Loại hàng"].Value.ToString();
                this.txtChiTiet.Text = dgvMatHang.Rows[r].Cells["Tên món hàng"].Value.ToString();
                this.txtGiaTri.Text = dgvMatHang.Rows[r].Cells["Gía trị thực"].Value.ToString();   
                this.txtCMND.Text = dgvMatHang.Rows[r].Cells["CMND"].Value.ToString();
                this.cmbTinhTrang.Text = dgvMatHang.Rows[r].Cells["Tình trạng"].Value.ToString();
            }
            catch { }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            resettext();
            if (rdbCMND.Checked) //tìm theo mã SV
            {
                dgvMatHang.DataSource = BLMatHang.Instance.SearchMHByCMND(txtSearch.Text);
            }
            else    //tìm theo Họ Tên SV
            {
                dgvMatHang.DataSource = BLMatHang.Instance.SearchMHByTenMH(txtSearch.Text);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu f1 = new MainMenu(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Hide();
            f1.ShowDialog();
        }
    }
}
