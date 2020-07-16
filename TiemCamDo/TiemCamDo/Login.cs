using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiemCamDo.BD_Layer;

namespace TiemCamDo
{
    public partial class Login : Form
    {
        string MaNV;
        bool IsAdmin=true;
        public Login()
        {
            InitializeComponent();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnDangNhap.PerformClick();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (BLNhanVien.Instance.IsUser(txtUsername.Text, txtPassword.Text, rbAdmin.Checked ? "Admin" : "NhanVien"))
            {
                MessageBox.Show("Đăng nhập vào hệ thống !", "Thông báo !");
                MaNV = txtUsername.Text;
                if (rbAdmin.Checked)
                {
                    IsAdmin = true;
                    MainMenu f1 = new MainMenu(MaNV,IsAdmin);
                    this.Hide();
                    f1.ShowDialog();
                }
                else
                {
                    IsAdmin = false;
                    MainMenu f1 = new MainMenu(MaNV, IsAdmin);
                    this.Hide();
                    f1.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
                
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát khỏi chương trình? ", "Thông Báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
