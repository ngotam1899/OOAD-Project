using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiemCamDo
{
    public partial class MainMenu : Form
    {
        string MaNV;
        bool IsAdmin;
        public MainMenu(string manv, bool IsAdmin)
        {
            InitializeComponent();
            this.MaNV = manv;
            this.IsAdmin = IsAdmin;
            //this.tsmiTaiKhoan.Enabled = true;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            if (IsAdmin==false)
            {
                groupBox2.Visible=false;
            }
        }


        private void tsmiLogOut_Click(object sender, EventArgs e)
        {

        }

        private void tsmiCamDo_Click(object sender, EventArgs e)
        {
            CamDo cd = new CamDo(MaNV, IsAdmin);
            cd.ShowDialog();
        }

        private void tsmiDSKH_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang(MaNV,IsAdmin);
            kh.ShowDialog();
        }

        private void tsmiQL_Click(object sender, EventArgs e)
        {
            NhanVien ql = new NhanVien(MaNV, IsAdmin);
            ql.ShowDialog();
        }

        private void tsmiTT_Click(object sender, EventArgs e)
        {
            //ThongTinNV ttnv = new ThongTinNV();
            //ttnv.ShowDialog();
        }

        private void tsmiDL_Click(object sender, EventArgs e)
        {
            TraGop tl = new TraGop(MaNV,IsAdmin);
            tl.ShowDialog();
        }

        private void tsmiChuoc_Click(object sender, EventArgs e)
        {
            ChuocDo chd = new ChuocDo(MaNV,IsAdmin);
            chd.ShowDialog();
        }

        private void tsmiMatHang_Click(object sender, EventArgs e)
        {
            KhoHang mh = new KhoHang(MaNV,IsAdmin);
            mh.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            CamDo f1 = new CamDo(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KhachHang f1 = new KhachHang(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            NhanVien f1 = new NhanVien(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            KhoHang f1 = new KhoHang(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void btnMonAn_Click(object sender, EventArgs e)
        {
            KhoHang f1 = new KhoHang(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChuocDo f1 = new ChuocDo(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            TraGop f1 = new TraGop(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           TraGop f1 = new TraGop(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CamDo f1 = new CamDo(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            KhachHang f1 = new KhachHang(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NhanVien f1 = new NhanVien(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Hide();
            f1.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DoanhThu f1 = new DoanhThu(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            DoanhThu f1 = new DoanhThu(MaNV, IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }
    }
}
