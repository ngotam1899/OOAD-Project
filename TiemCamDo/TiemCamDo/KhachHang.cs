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
    public partial class KhachHang : Form
    {
        bool Them;
        string MaNV;
        bool IsAdmin;
        public KhachHang(string manv, bool IsAdmin)
        {

            this.MaNV = manv;
            this.IsAdmin = IsAdmin;
            InitializeComponent();
        }
        private void Enabletxt(bool t)
        {
            txtCMND.Enabled = t;
            txtHoTen.Enabled = t;
            rdbNam.Enabled = t;
            rdbNu.Enabled = t;
            txtDiaChi.Enabled = t;
            txtNoiCap.Enabled = t;
            txtSDT.Enabled = t;
            dtpNgaySinh.Enabled = t;
        }
        private void resettext()
        {
            txtCMND.ResetText();
            txtHoTen.ResetText();
            rdbNam.Checked = false;
            rdbNu.Checked = false;
            txtDiaChi.ResetText();
            txtNoiCap.ResetText();
            txtSDT.ResetText();
            dtpNgaySinh.ResetText();
        }
        private void LoadData()
        {
            dgvKH.DataSource = BLKhachHang.Instance.GetKH();
            dgvKH.AllowUserToAddRows = false;
            dgvKH.ReadOnly = true;
            rdbSDT.Checked = true;
            dgvKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Enabletxt(false);
            Enabletxt(false);
            resettext();
            btnUpdate.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnEdit.Enabled = true;
            btnDel.Enabled = true;
            btnExit.Enabled = true;
        }
        private void KhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
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
            btnHuy.Enabled = true;
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
            txtCMND.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
               
            if (Them)
            {
                if ((!txtCMND.Text.Trim().Equals("")))
                {
                    try
                    {
                        if (BLKhachHang.Instance.InsertKH(txtCMND.Text, txtHoTen.Text, txtDiaChi.Text,txtSDT.Text, dtpNgaySinh.Value.Date, txtNoiCap.Text, (rdbNam.Checked) ? "Nam" : "Nữ"))
                        {
                            // Load lại dữ liệu trên DataGridView     
                            dgvKH.DataSource = BLKhachHang.Instance.GetKH();
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
                if (BLKhachHang.Instance.UpdateKH(txtCMND.Text, txtHoTen.Text, txtDiaChi.Text, txtSDT.Text, dtpNgaySinh.Value.Date, txtNoiCap.Text, (rdbNam.Checked) ? "Nam" : "Nữ"))
                {
                    // Load lại dữ liệu trên DataGridView      
                    dgvKH.DataSource = BLKhachHang.Instance.GetKH();
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


        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadData();
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
                int r = dgvKH.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành       
                string str = dgvKH.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL  

                // Hiện thông báo xác nhận việc xóa mẫu tin       
                // Khai báo biến traloi            
                DialogResult traloi;
                // Hiện hộp thoại hỏi đáp    
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không?           
                if (traloi == DialogResult.Yes)
                {
                    if (BLKhachHang.Instance.DeleteKH(str))
                    {
                        // Cập nhật lại DataGridView                
                        dgvKH.DataSource = BLKhachHang.Instance.GetKH();
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

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvKH.CurrentCell.RowIndex;
            txtCMND.Text = dgvKH.Rows[r].Cells["SocialID"].Value.ToString();
            txtHoTen.Text = dgvKH.Rows[r].Cells["Name"].Value.ToString();
            txtDiaChi.Text = dgvKH.Rows[r].Cells["Address"].Value.ToString();
            txtNoiCap.Text = dgvKH.Rows[r].Cells["SocialPlace"].Value.ToString();
            txtSDT.Text = dgvKH.Rows[r].Cells["Phone"].Value.ToString();
            dtpNgaySinh.Text = dgvKH.Rows[r].Cells["Birth"].Value.ToString();
            if (dgvKH.Rows[r].Cells["Gender"].Value.ToString() == "Nam") rdbNam.Checked = true; else rdbNam.Checked = false;
            if (dgvKH.Rows[r].Cells["Gender"].Value.ToString() == "Nữ") rdbNu.Checked = true; else rdbNu.Checked = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rdbSDT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Hide();
            f1.ShowDialog();
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
            txtCMND.Enabled = false;
            txtHoTen.Focus();
        }


        private void rdbNu_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void rdbNam_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu f1 = new MainMenu(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }
    }
}
