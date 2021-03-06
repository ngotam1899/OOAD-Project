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
            // TODO: This line of code loads data into the 'DataSetCamDo.KhachHang' table. You can move, or remove it, as needed.
            this.KhachHangTableAdapter.Fill(this.DataSetCamDo.KhachHang,CMND);
            // TODO: This line of code loads data into the 'DataSetCamDo.PhieuCamDo' table. You can move, or remove it, as needed.
            this.PhieuCamDoTableAdapter.Fill(this.DataSetCamDo.PhieuCamDo,MaPhieu);
            // TODO: This line of code loads data into the 'DataSetCamDo.PhieuTraGop' table. You can move, or remove it, as needed.
            this.PhieuTraGopTableAdapter.Fill(this.DataSetCamDo.PhieuTraGop,MaTraGop);
            // TODO: This line of code loads data into the 'DataSetCamDo.MatHang' table. You can move, or remove it, as needed.
            this.MatHangTableAdapter.Fill(this.DataSetCamDo.MatHang,MaHang);
            this.reportViewer1.RefreshReport();
        }
    }
}
