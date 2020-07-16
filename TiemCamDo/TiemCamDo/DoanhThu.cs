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
    public partial class DoanhThu : Form
    {
        string MaNV;
        bool IsAdmin;
        public DoanhThu(string MaNV, bool IsAdmin)
        {
            InitializeComponent();
            this.MaNV = MaNV;
            this.IsAdmin = IsAdmin;
        }
        public void TinhTongDoanhThu()
        {
            int sumcamdo = 0;
            for (int i = 0; i <= dgvDoanhThuCamDo.Rows.Count - 1; i++)
            {
                sumcamdo = sumcamdo + int.Parse(dgvDoanhThuCamDo.Rows[i].Cells["Tiền cầm"].Value.ToString());
            }

            int sumchuocdo = 0;
            for (int i = 0; i <= dgvDoanhThuChuocDo.Rows.Count - 1; i++)
            {
                sumchuocdo = sumchuocdo + int.Parse(dgvDoanhThuChuocDo.Rows[i].Cells["Tiền chuộc"].Value.ToString());
            }

            int sumtragop = 0;
            for (int i = 0; i <= dgvDoanhThuTraGop.Rows.Count - 1; i++)
            {
                sumtragop = sumtragop + int.Parse(dgvDoanhThuTraGop.Rows[i].Cells["Tiền trả góp"].Value.ToString());
            }

            int doanhthu = sumchuocdo + sumtragop - sumcamdo;

            txtDoanhThu.Text = doanhthu.ToString();
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Hide();
            f1.ShowDialog();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            MainMenu f1 = new MainMenu(MaNV,IsAdmin);
            this.Hide();
            f1.ShowDialog();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            dgvDoanhThuCamDo.DataSource = BLThongKe.Instance.GetThongKe(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
            dgvDoanhThuCamDo.AllowUserToAddRows = false;
            dgvDoanhThuCamDo.ReadOnly = true;
            dgvDoanhThuCamDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDoanhThuTraGop.DataSource = BLThongKe.Instance.GetThongKeTraGop(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
            dgvDoanhThuTraGop.AllowUserToAddRows = false;
            dgvDoanhThuTraGop.ReadOnly = true;
            dgvDoanhThuTraGop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDoanhThuChuocDo.DataSource = BLThongKe.Instance.GetThongKeChuocDo(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
            dgvDoanhThuChuocDo.AllowUserToAddRows = false;
            dgvDoanhThuChuocDo.ReadOnly = true;
            dgvDoanhThuChuocDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            TinhTongDoanhThu();
            
        }

        private void dgvDoanhThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DoanhThu_Load(object sender, EventArgs e)
        {
            
        }
    }
}
