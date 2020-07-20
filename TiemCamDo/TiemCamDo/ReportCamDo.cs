﻿using System;
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
    public partial class ReportCamDo : Form
    {
        string MaHang, CMND, MaPhieu;
        public ReportCamDo(string MaHang, string CMND, string MaPhieu)
        {
            InitializeComponent();
            this.MaHang = MaHang;
            this.CMND = CMND;
            this.MaPhieu = MaPhieu;
        }

        private void Report_Load(object sender, EventArgs e)
        {
            //this.ReportTableAdapter.Fill(this.CafeteriaBillReport.Report, this.ID_Bill);
            this.reportViewer1.RefreshReport();
        }
    }
}
