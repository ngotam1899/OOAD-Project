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
            this.reportViewer1.RefreshReport();
        }
    }
}
