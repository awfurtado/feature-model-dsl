namespace UFPE.FeatureModelDSL.Confeaturator {
    partial class FrmAddRemoveConfeaturatorActionProviders {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.dgvConfeaturatorActionProviders = new System.Windows.Forms.DataGridView();
            this.assemblyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qualifiedClassNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confeaturatorActionProviderSettingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfeaturatorActionProviders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.confeaturatorActionProviderSettingBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConfeaturatorActionProviders
            // 
            this.dgvConfeaturatorActionProviders.AutoGenerateColumns = false;
            this.dgvConfeaturatorActionProviders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvConfeaturatorActionProviders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfeaturatorActionProviders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.assemblyNameDataGridViewTextBoxColumn,
            this.qualifiedClassNameDataGridViewTextBoxColumn});
            this.dgvConfeaturatorActionProviders.DataSource = this.confeaturatorActionProviderSettingBindingSource;
            this.dgvConfeaturatorActionProviders.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvConfeaturatorActionProviders.Location = new System.Drawing.Point(0, 0);
            this.dgvConfeaturatorActionProviders.Name = "dgvConfeaturatorActionProviders";
            this.dgvConfeaturatorActionProviders.Size = new System.Drawing.Size(668, 218);
            this.dgvConfeaturatorActionProviders.TabIndex = 0;
            // 
            // assemblyNameDataGridViewTextBoxColumn
            // 
            this.assemblyNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.assemblyNameDataGridViewTextBoxColumn.DataPropertyName = "AssemblyName";
            this.assemblyNameDataGridViewTextBoxColumn.FillWeight = 194.9239F;
            this.assemblyNameDataGridViewTextBoxColumn.HeaderText = "Assembly Name";
            this.assemblyNameDataGridViewTextBoxColumn.Name = "assemblyNameDataGridViewTextBoxColumn";
            this.assemblyNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // qualifiedClassNameDataGridViewTextBoxColumn
            // 
            this.qualifiedClassNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.qualifiedClassNameDataGridViewTextBoxColumn.DataPropertyName = "QualifiedClassName";
            this.qualifiedClassNameDataGridViewTextBoxColumn.FillWeight = 5.076141F;
            this.qualifiedClassNameDataGridViewTextBoxColumn.HeaderText = "Qualified Class Name";
            this.qualifiedClassNameDataGridViewTextBoxColumn.Name = "qualifiedClassNameDataGridViewTextBoxColumn";
            // 
            // confeaturatorActionProviderSettingBindingSource
            // 
            this.confeaturatorActionProviderSettingBindingSource.AllowNew = true;
            this.confeaturatorActionProviderSettingBindingSource.DataSource = typeof(UFPE.FeatureModelDSL.Confeaturator.ConfeaturatorActionProviderSetting);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(257, 237);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(338, 237);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(176, 237);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "Find...";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(419, 237);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 4;
            this.btnHelp.Text = "Help...";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // FrmAddRemoveConfeaturatorActionProviders
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(668, 271);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvConfeaturatorActionProviders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAddRemoveConfeaturatorActionProviders";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add / Remove Confeaturator Action Providers";
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfeaturatorActionProviders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.confeaturatorActionProviderSettingBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConfeaturatorActionProviders;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.BindingSource confeaturatorActionProviderSettingBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn assemblyNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qualifiedClassNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnHelp;
    }
}