namespace TiemCamDo
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
            this.DataSetCamDo = new TiemCamDo.DataSetCamDo();
            this.PhieuCamDoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhieuCamDoTableAdapter = new TiemCamDo.DataSetCamDoTableAdapters.PhieuCamDoTableAdapter();
            this.KhachHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.KhachHangTableAdapter = new TiemCamDo.DataSetCamDoTableAdapters.KhachHangTableAdapter();
            this.MatHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MatHangTableAdapter = new TiemCamDo.DataSetCamDoTableAdapters.MatHangTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetCamDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuCamDoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatHangBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PhieuCamDoBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.KhachHangBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.MatHangBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TiemCamDo.ReportCD.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(4, 2);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1307, 1117);
            this.reportViewer1.TabIndex = 0;
            // 
            // DataSetCamDo
            // 
            this.DataSetCamDo.DataSetName = "DataSetCamDo";
            this.DataSetCamDo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PhieuCamDoBindingSource
            // 
            this.PhieuCamDoBindingSource.DataMember = "PhieuCamDo";
            this.PhieuCamDoBindingSource.DataSource = this.DataSetCamDo;
            // 
            // PhieuCamDoTableAdapter
            // 
            this.PhieuCamDoTableAdapter.ClearBeforeFill = true;
            // 
            // KhachHangBindingSource
            // 
            this.KhachHangBindingSource.DataMember = "KhachHang";
            this.KhachHangBindingSource.DataSource = this.DataSetCamDo;
            // 
            // KhachHangTableAdapter
            // 
            this.KhachHangTableAdapter.ClearBeforeFill = true;
            // 
            // MatHangBindingSource
            // 
            this.MatHangBindingSource.DataMember = "MatHang";
            this.MatHangBindingSource.DataSource = this.DataSetCamDo;
            // 
            // MatHangTableAdapter
            // 
            this.MatHangTableAdapter.ClearBeforeFill = true;
            // 
            // ReportCamDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 1055);
            this.Controls.Add(this.reportViewer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ReportCamDo";
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataSetCamDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuCamDoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatHangBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PhieuCamDoBindingSource;
        private DataSetCamDo DataSetCamDo;
        private DataSetCamDoTableAdapters.PhieuCamDoTableAdapter PhieuCamDoTableAdapter;
        private System.Windows.Forms.BindingSource KhachHangBindingSource;
        private System.Windows.Forms.BindingSource MatHangBindingSource;
        private DataSetCamDoTableAdapters.KhachHangTableAdapter KhachHangTableAdapter;
        private DataSetCamDoTableAdapters.MatHangTableAdapter MatHangTableAdapter;
    }
}