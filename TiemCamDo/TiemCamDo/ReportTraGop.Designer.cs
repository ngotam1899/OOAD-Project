namespace TiemCamDo
{
    partial class ReportTraGop
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
            this.PhieuCamDoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhieuTraGopBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MatHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuCamDoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuTraGopBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatHangBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.KhachHangBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.PhieuCamDoBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.PhieuTraGopBindingSource;
            reportDataSource4.Name = "DataSet4";
            reportDataSource4.Value = this.MatHangBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TiemCamDo.ReportTraGop.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(2, -1);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1052, 776);
            this.reportViewer1.TabIndex = 1;
            // 
            // ReportTraGop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 787);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportTraGop";
            this.Text = "ReportChuocDo";
            this.Load += new System.EventHandler(this.ReportTraGop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuCamDoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuTraGopBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatHangBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource KhachHangBindingSource;
        private System.Windows.Forms.BindingSource PhieuCamDoBindingSource;
        private System.Windows.Forms.BindingSource PhieuTraGopBindingSource;
        private System.Windows.Forms.BindingSource MatHangBindingSource;
    }
}