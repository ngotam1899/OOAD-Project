namespace TiemCamDo
{
    partial class ReportChuocDo
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.KhachHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetCamDo = new TiemCamDo.DataSetCamDo();
            this.MatHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhieuCamDoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhieuChuocDoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.KhachHangTableAdapter = new TiemCamDo.DataSetCamDoTableAdapters.KhachHangTableAdapter();
            this.MatHangTableAdapter = new TiemCamDo.DataSetCamDoTableAdapters.MatHangTableAdapter();
            this.PhieuCamDoTableAdapter = new TiemCamDo.DataSetCamDoTableAdapters.PhieuCamDoTableAdapter();
            this.PhieuChuocDoTableAdapter = new TiemCamDo.DataSetCamDoTableAdapters.PhieuChuocDoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetCamDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatHangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuCamDoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuChuocDoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // KhachHangBindingSource
            // 
            this.KhachHangBindingSource.DataMember = "KhachHang";
            this.KhachHangBindingSource.DataSource = this.DataSetCamDo;
            // 
            // DataSetCamDo
            // 
            this.DataSetCamDo.DataSetName = "DataSetCamDo";
            this.DataSetCamDo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MatHangBindingSource
            // 
            this.MatHangBindingSource.DataMember = "MatHang";
            this.MatHangBindingSource.DataSource = this.DataSetCamDo;
            // 
            // PhieuCamDoBindingSource
            // 
            this.PhieuCamDoBindingSource.DataMember = "PhieuCamDo";
            this.PhieuCamDoBindingSource.DataSource = this.DataSetCamDo;
            // 
            // PhieuChuocDoBindingSource
            // 
            this.PhieuChuocDoBindingSource.DataMember = "PhieuChuocDo";
            this.PhieuChuocDoBindingSource.DataSource = this.DataSetCamDo;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.KhachHangBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.MatHangBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.PhieuCamDoBindingSource;
            reportDataSource4.Name = "DataSet4";
            reportDataSource4.Value = this.PhieuChuocDoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TiemCamDo.ReportChuocDo.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-2, 1);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1346, 731);
            this.reportViewer1.TabIndex = 0;
            // 
            // KhachHangTableAdapter
            // 
            this.KhachHangTableAdapter.ClearBeforeFill = true;
            // 
            // MatHangTableAdapter
            // 
            this.MatHangTableAdapter.ClearBeforeFill = true;
            // 
            // PhieuCamDoTableAdapter
            // 
            this.PhieuCamDoTableAdapter.ClearBeforeFill = true;
            // 
            // PhieuChuocDoTableAdapter
            // 
            this.PhieuChuocDoTableAdapter.ClearBeforeFill = true;
            // 
            // ReportChuocDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 733);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportChuocDo";
            this.Text = "ReportChuocDo";
            this.Load += new System.EventHandler(this.ReportChuocDo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetCamDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatHangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuCamDoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuChuocDoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource KhachHangBindingSource;
        private DataSetCamDo DataSetCamDo;
        private System.Windows.Forms.BindingSource MatHangBindingSource;
        private System.Windows.Forms.BindingSource PhieuCamDoBindingSource;
        private System.Windows.Forms.BindingSource PhieuChuocDoBindingSource;
        private DataSetCamDoTableAdapters.KhachHangTableAdapter KhachHangTableAdapter;
        private DataSetCamDoTableAdapters.MatHangTableAdapter MatHangTableAdapter;
        private DataSetCamDoTableAdapters.PhieuCamDoTableAdapter PhieuCamDoTableAdapter;
        private DataSetCamDoTableAdapters.PhieuChuocDoTableAdapter PhieuChuocDoTableAdapter;
    }
}