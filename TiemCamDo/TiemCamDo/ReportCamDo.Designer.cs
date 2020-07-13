﻿namespace TiemCamDo
{
    partial class ReportCamDo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.KhachHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CamDoDataSet = new TiemCamDo.CamDoDataSet();
            this.PhieuCamDoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MatHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.KhachHangTableAdapter = new TiemCamDo.CamDoDataSetTableAdapters.KhachHangTableAdapter();
            this.PhieuCamDoTableAdapter = new TiemCamDo.CamDoDataSetTableAdapters.PhieuCamDoTableAdapter();
            this.MatHangTableAdapter = new TiemCamDo.CamDoDataSetTableAdapters.MatHangTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CamDoDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuCamDoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatHangBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.KhachHangBindingSource;
            reportDataSource2.Name = "DataSet3";
            reportDataSource2.Value = this.PhieuCamDoBindingSource;
            reportDataSource3.Name = "DataSet4";
            reportDataSource3.Value = this.MatHangBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TiemCamDo.ReportCamDo.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1046, 894);
            this.reportViewer1.TabIndex = 0;
            // 
            // KhachHangBindingSource
            // 
            this.KhachHangBindingSource.DataMember = "KhachHang";
            this.KhachHangBindingSource.DataSource = this.CamDoDataSet;
            // 
            // CamDoDataSet
            // 
            this.CamDoDataSet.DataSetName = "CamDoDataSet";
            this.CamDoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PhieuCamDoBindingSource
            // 
            this.PhieuCamDoBindingSource.DataMember = "PhieuCamDo";
            this.PhieuCamDoBindingSource.DataSource = this.CamDoDataSet;
            // 
            // MatHangBindingSource
            // 
            this.MatHangBindingSource.DataMember = "MatHang";
            this.MatHangBindingSource.DataSource = this.CamDoDataSet;
            // 
            // KhachHangTableAdapter
            // 
            this.KhachHangTableAdapter.ClearBeforeFill = true;
            // 
            // PhieuCamDoTableAdapter
            // 
            this.PhieuCamDoTableAdapter.ClearBeforeFill = true;
            // 
            // MatHangTableAdapter
            // 
            this.MatHangTableAdapter.ClearBeforeFill = true;
            // 
            // ReportCamDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 908);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportCamDo";
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CamDoDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuCamDoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatHangBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource KhachHangBindingSource;
        private CamDoDataSet CamDoDataSet;
        private System.Windows.Forms.BindingSource PhieuCamDoBindingSource;
        private System.Windows.Forms.BindingSource MatHangBindingSource;
        private CamDoDataSetTableAdapters.KhachHangTableAdapter KhachHangTableAdapter;
        private CamDoDataSetTableAdapters.PhieuCamDoTableAdapter PhieuCamDoTableAdapter;
        private CamDoDataSetTableAdapters.MatHangTableAdapter MatHangTableAdapter;
    }
}