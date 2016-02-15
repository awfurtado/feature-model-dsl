namespace UFPE.FeatureModelDSL.Confeaturator {
    partial class CtrlConfeaturator {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlConfeaturator));
            this.trvFeatures = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenConfiguration = new System.Windows.Forms.ToolStripButton();
            this.btnSaveConfiguration = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidateConfiguration = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConfigureEnvironment = new System.Windows.Forms.ToolStripButton();
            this.btnAddRemoveConfeaturatorActionProviders = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerateReport = new System.Windows.Forms.ToolStripButton();
            this.btnGenerateConsolidatedReport = new System.Windows.Forms.ToolStripButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.saveConfigurationDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveReportDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvFeatures
            // 
            this.trvFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvFeatures.ImageIndex = 0;
            this.trvFeatures.ImageList = this.imageList;
            this.trvFeatures.Location = new System.Drawing.Point(0, 0);
            this.trvFeatures.Name = "trvFeatures";
            this.trvFeatures.SelectedImageIndex = 0;
            this.trvFeatures.Size = new System.Drawing.Size(246, 358);
            this.trvFeatures.TabIndex = 2;
            this.trvFeatures.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvFeatures_BeforeCollapse);
            this.trvFeatures.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvFeatures_BeforeExpand);
            this.trvFeatures.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvFeatures_AfterExpand);
            this.trvFeatures.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvFeatures_AfterSelect);
            this.trvFeatures.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvFeatures_NodeMouseClick);
            this.trvFeatures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvFeatures_KeyDown);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "CheckedEnabled.bmp");
            this.imageList.Images.SetKeyName(1, "CheckedDisabled.bmp");
            this.imageList.Images.SetKeyName(2, "Unchecked.bmp");
            this.imageList.Images.SetKeyName(3, "AlternativeIcon.bmp");
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.toolStripSeparator1,
            this.btnOpenConfiguration,
            this.btnSaveConfiguration,
            this.toolStripSeparator2,
            this.btnValidateConfiguration,
            this.toolStripSeparator3,
            this.btnConfigureEnvironment,
            this.btnAddRemoveConfeaturatorActionProviders,
            this.toolStripSeparator4,
            this.btnGenerateReport,
            this.btnGenerateConsolidatedReport});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(246, 25);
            this.toolStrip.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOpenConfiguration
            // 
            this.btnOpenConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenConfiguration.Image")));
            this.btnOpenConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenConfiguration.Name = "btnOpenConfiguration";
            this.btnOpenConfiguration.Size = new System.Drawing.Size(23, 22);
            this.btnOpenConfiguration.Text = "Open Configuration";
            this.btnOpenConfiguration.Click += new System.EventHandler(this.btnOpenConfiguration_Click);
            // 
            // btnSaveConfiguration
            // 
            this.btnSaveConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveConfiguration.Image")));
            this.btnSaveConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveConfiguration.Name = "btnSaveConfiguration";
            this.btnSaveConfiguration.Size = new System.Drawing.Size(23, 22);
            this.btnSaveConfiguration.Text = "Save Configuration";
            this.btnSaveConfiguration.Click += new System.EventHandler(this.btnSaveConfiguration_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnValidateConfiguration
            // 
            this.btnValidateConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidateConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("btnValidateConfiguration.Image")));
            this.btnValidateConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidateConfiguration.Name = "btnValidateConfiguration";
            this.btnValidateConfiguration.Size = new System.Drawing.Size(23, 22);
            this.btnValidateConfiguration.Text = "Validate Configuration";
            this.btnValidateConfiguration.Click += new System.EventHandler(this.btnValidateConfiguration_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnConfigureEnvironment
            // 
            this.btnConfigureEnvironment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConfigureEnvironment.Image = ((System.Drawing.Image)(resources.GetObject("btnConfigureEnvironment.Image")));
            this.btnConfigureEnvironment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfigureEnvironment.Name = "btnConfigureEnvironment";
            this.btnConfigureEnvironment.Size = new System.Drawing.Size(23, 22);
            this.btnConfigureEnvironment.Text = "Launch Confeaturator Actions to configure environment";
            this.btnConfigureEnvironment.ToolTipText = "Launch Confeaturator Actions to configure environment";
            this.btnConfigureEnvironment.Click += new System.EventHandler(this.btnConfigureEnvironment_Click);
            // 
            // btnAddRemoveConfeaturatorActionProviders
            // 
            this.btnAddRemoveConfeaturatorActionProviders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddRemoveConfeaturatorActionProviders.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRemoveConfeaturatorActionProviders.Image")));
            this.btnAddRemoveConfeaturatorActionProviders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRemoveConfeaturatorActionProviders.Name = "btnAddRemoveConfeaturatorActionProviders";
            this.btnAddRemoveConfeaturatorActionProviders.Size = new System.Drawing.Size(23, 22);
            this.btnAddRemoveConfeaturatorActionProviders.Text = "Add / Remove Confeaturator Action Providers";
            this.btnAddRemoveConfeaturatorActionProviders.ToolTipText = "Add / Remove Confeaturator Action Providers";
            this.btnAddRemoveConfeaturatorActionProviders.Click += new System.EventHandler(this.btnAddRemoveConfeaturatorActionProviders_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerateReport.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateReport.Image")));
            this.btnGenerateReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(23, 22);
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // btnGenerateConsolidatedReport
            // 
            this.btnGenerateConsolidatedReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerateConsolidatedReport.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateConsolidatedReport.Image")));
            this.btnGenerateConsolidatedReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerateConsolidatedReport.Name = "btnGenerateConsolidatedReport";
            this.btnGenerateConsolidatedReport.Size = new System.Drawing.Size(23, 22);
            this.btnGenerateConsolidatedReport.Text = "Generate Consolidated Report (from multiple configurations)";
            this.btnGenerateConsolidatedReport.Click += new System.EventHandler(this.btnGenerateConsolidatedReport_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.toolStrip);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.trvFeatures);
            this.splitContainer.Size = new System.Drawing.Size(246, 387);
            this.splitContainer.SplitterDistance = 25;
            this.splitContainer.TabIndex = 4;
            // 
            // saveConfigurationDialog
            // 
            this.saveConfigurationDialog.DefaultExt = "confeaturator";
            this.saveConfigurationDialog.Filter = "Confeaturator files|*.confeaturator";
            this.saveConfigurationDialog.SupportMultiDottedExtensions = true;
            this.saveConfigurationDialog.Title = "Save Feature Model configuration";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Confeaturator files|*.confeaturator";
            this.openFileDialog.Title = "Load Feature Model configuration";
            // 
            // saveReportDialog
            // 
            this.saveReportDialog.DefaultExt = "confeaturator";
            this.saveReportDialog.Filter = "HTML files|*.html";
            this.saveReportDialog.SupportMultiDottedExtensions = true;
            this.saveReportDialog.Title = "Enter a HTML file name where to save the report";
            // 
            // CtrlConfeaturator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "CtrlConfeaturator";
            this.Size = new System.Drawing.Size(246, 387);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvFeatures;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnConfigureEnvironment;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAddRemoveConfeaturatorActionProviders;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSaveConfiguration;
        private System.Windows.Forms.ToolStripButton btnOpenConfiguration;
        private System.Windows.Forms.ToolStripButton btnValidateConfiguration;
        private System.Windows.Forms.SaveFileDialog saveConfigurationDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton btnGenerateConsolidatedReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SaveFileDialog saveReportDialog;
        private System.Windows.Forms.ToolStripButton btnGenerateReport;
    }
}
