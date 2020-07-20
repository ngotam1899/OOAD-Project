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
using TiemCamDo.Data_Access_Object;

namespace TiemCamDo
{
    public partial class NhanVien : Form
    {
        bool Them;
        string MaNV;
        bool IsAdmin;
        public NhanVien(string manv, bool IsAdmin)
        {
            this.MaNV = manv;
            this.IsAdmin = IsAdmin;
            InitializeComponent();
        }
        private void Enabletxt(bool t)
        {
            txtHoTen.Enabled = t;
            txtDiaChi.Enabled = t;
            txtGioiTinh.Enabled = t;
            txtMaNV.Enabled = t;
            txtMK.Enabled = t;
            txtSoDT.Enabled = t;
            txtQuyen.Enabled = t;
            txtEmail.Enabled = t;
        }
        private void resettext()
        {
            txtHoTen.ResetText();
            txtDiaChi.ResetText();
            txtGioiTinh.ResetText();
            txtMaNV.ResetText();
            txtMK.ResetText();
            txtSoDT.ResetText();
            txtQuyen.ResetText();
            txtEmail.ResetText();
        }
        private void QuanLy_Load(object sender, EventArgs e)
        {
            List<Employee> employees = BLNhanVien.Instance.GetNV();
            dgvNV.DataSource = employees;
            dgvNV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNV.AllowUserToAddRows = false;
            dgvNV.ReadOnly = true;
            dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            groupBox1.Enabled = false;
        }
        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Hide();
            f1.ShowDialog();
        }

        private void dgvNV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvNV.CurrentCell.RowIndex;
            txtHoTen.Text = dgvNV.Rows[r].Cells["Name"].Value.ToString();
            txtMK.Text = dgvNV.Rows[r].Cells["Password"].Value.ToString();
            txtMaNV.Text = dgvNV.Rows[r].Cells["EmployeeID"].Value.ToString();
            txtEmail.Text = dgvNV.Rows[r].Cells["Email"].Value.ToString();
            txtGioiTinh.Text = dgvNV.Rows[r].Cells["Gender"].Value.ToString();
            txtSoDT.Text = dgvNV.Rows[r].Cells["Phone"].Value.ToString();
            txtDiaChi.Text = dgvNV.Rows[r].Cells["Address"].Value.ToString();
            txtQuyen.Text = dgvNV.Rows[r].Cells["Authorize"].Value.ToString();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa
            Them = false;
            // Cho phép thao tác trên Panel
            Enabletxt(true);
            groupBox1.Enabled = true;
            // Cho thao tác trên các nút Lưu / Hủy / Panel
            btnUpdate.Enabled = true;
            btnHuy.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            // Đưa con trỏ đến TextField 
            txtMaNV.Enabled = false;
            txtHoTen.Focus();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            Them = true;
            groupBox1.Enabled = true;
            resettext();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            txtHoTen.Focus();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (Them)
            {
                if ((!txtMaNV.Text.Trim().Equals("")))
                {
                    try
                    {
                        if (BLNhanVien.Instance.InsertNV(txtMaNV.Text, txtEmail.Text, txtMK.Text, txtHoTen.Text, txtGioiTinh.Text, txtSoDT.Text, txtDiaChi.Text, txtQuyen.Text))
                        {
                            // Load lại dữ liệu trên DataGridView     
                            dgvNV.DataSource = BLNhanVien.Instance.GetNV();
                            Enabletxt(false);
                            resettext();
                            //// Không cho thao tác trên các nút Lưu / Hủy
                            btnUpdate.Enabled = false;
                            btnHuy.Enabled = false;
                            //// Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
                            btnThem.Enabled = true;
                            btnSua.Enabled = true;
                            btnXoa.Enabled = true;
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
                if (BLNhanVien.Instance.UpdateNV(txtMaNV.Text, txtEmail.Text, txtMK.Text, txtHoTen.Text, txtGioiTinh.Text, txtSoDT.Text, txtDiaChi.Text, txtQuyen.Text))
                {
                    // Load lại dữ liệu trên DataGridView      
                    dgvNV.DataSource = BLNhanVien.Instance.GetNV();
                    Enabletxt(false);
                    resettext();
                    //// Không cho thao tác trên các nút Lưu / Hủy
                    btnUpdate.Enabled = false;
                    btnHuy.Enabled = false;
                    //// Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    // Thông báo              
                    MessageBox.Show("Đã sửa xong!");
                }
            }
            // Đóng kết nối   
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Thực hiện lệnh    
                // Lấy thứ tự record hiện hành     
                int r = dgvNV.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành       
                string str = dgvNV.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL  

                // Hiện thông báo xác nhận việc xóa mẫu tin       
                // Khai báo biến traloi            
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp    
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?           
                if (traloi == DialogResult.Yes)
                {
                    if (BLNhanVien.Instance.DeleteNV(str))
                    {
                        // Cập nhật lại DataGridView                
                        dgvNV.DataSource = BLNhanVien.Instance.GetNV();
                        Enabletxt(false);
                        resettext();
                        //// Không cho thao tác trên các nút Lưu / Hủy
                        btnUpdate.Enabled = false;
                        btnHuy.Enabled = false;
                        //// Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
                        btnThem.Enabled = true;
                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
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

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel
            resettext();
            Enabletxt(false);
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            resettext();
            if (rdbSDT.Checked) //tìm theo mã SV
            {
                dgvNV.DataSource = BLNhanVien.Instance.SearchNVBySDT(txtSearch.Text.Trim());
            }
            else   //tìm theo Họ Tên SV
            {
                dgvNV.DataSource = BLNhanVien.Instance.SearchNVByTen(txtSearch.Text.Trim());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu f1 = new MainMenu(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }
    }
}
