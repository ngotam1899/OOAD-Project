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
    public partial class ReportTraGop : Form
    {
        string MaHang,MaTraGop, CMND, MaPhieu;
        public ReportTraGop(string MaHang,string MaTraGop, string CMND, string MaPhieu)
        {
            InitializeComponent();
            this.MaHang = MaHang;
            this.MaTraGop = MaTraGop;
            this.CMND = CMND;
            this.MaPhieu = MaPhieu;
        }

        private void ReportTraGop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'CamDoDataSet.KhachHang' table. You can move, or remove it, as needed.
            this.KhachHangTableAdapter.Fill(this.CamDoDataSet.KhachHang,CMND);
            // TODO: This line of code loads data into the 'CamDoDataSet.PhieuCamDo' table. You can move, or remove it, as needed.
            this.PhieuCamDoTableAdapter.Fill(this.CamDoDataSet.PhieuCamDo,MaPhieu);
            // TODO: This line of code loads data into the 'CamDoDataSet.PhieuTraGop' table. You can move, or remove it, as needed.
            this.PhieuTraGopTableAdapter.Fill(this.CamDoDataSet.PhieuTraGop,MaTraGop);
            // TODO: This line of code loads data into the 'CamDoDataSet.MatHang' table. You can move, or remove it, as needed.
            this.MatHangTableAdapter.Fill(this.CamDoDataSet.MatHang,MaHang);
            this.reportViewer1.RefreshReport();
        }
    }
}
