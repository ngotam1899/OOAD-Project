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
    public partial class ReportChuocDo : Form
    {
        string MaHang, MaChuocDo, CMND, MaPhieu;
        public ReportChuocDo(string MaHang, string MaPhieuChuoc, string CMND, string MaPhieu)
        {
            this.MaHang = MaHang;
            this.MaChuocDo = MaPhieuChuoc;
            this.CMND = CMND;
            this.MaPhieu = MaPhieu;
            InitializeComponent();
        }

        private void ReportChuocDo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSetCamDo.KhachHang' table. You can move, or remove it, as needed.
            this.KhachHangTableAdapter.Fill(this.DataSetCamDo.KhachHang,CMND);
            // TODO: This line of code loads data into the 'DataSetCamDo.MatHang' table. You can move, or remove it, as needed.
            this.MatHangTableAdapter.Fill(this.DataSetCamDo.MatHang,MaHang);
            // TODO: This line of code loads data into the 'DataSetCamDo.PhieuCamDo' table. You can move, or remove it, as needed.
            this.PhieuCamDoTableAdapter.Fill(this.DataSetCamDo.PhieuCamDo,MaPhieu);
            // TODO: This line of code loads data into the 'DataSetCamDo.PhieuChuocDo' table. You can move, or remove it, as needed.
            this.PhieuChuocDoTableAdapter.Fill(this.DataSetCamDo.PhieuChuocDo,MaChuocDo);

            this.reportViewer1.RefreshReport();
        }
    }
}
