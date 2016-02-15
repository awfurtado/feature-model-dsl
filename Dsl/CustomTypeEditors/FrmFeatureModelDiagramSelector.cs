using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;

namespace UFPE.FeatureModelDSL.CustomTypeEditors {
    public partial class FrmFeatureModelDiagramSelector : Form {

        /// <summary>
        /// Gets the feature model file selected by the user
        /// </summary>
        public string SelectedFeatureModelFile {
            get {
                string result = string.Empty;
                if (this.lstFeatureModels.SelectedItem != null) {
                    result = this.lstFeatureModels.SelectedItem.ToString();
                }
                return result;
            }
        }

        public FrmFeatureModelDiagramSelector() {
            InitializeComponent();
        }

        public FrmFeatureModelDiagramSelector(Store store) : this () {
            lstFeatureModels.Items.Clear();
            foreach (string projectItemName in DTEHelper.GetAllProjectItemNames()) {
                if (projectItemName.EndsWith(".fm")) {
                    lstFeatureModels.Items.Add(projectItemName);
                }
            }

            if (lstFeatureModels.Items.Count > 0) {
                lstFeatureModels.SelectedIndex = 0;
            }
        }

        private void FrmFeatureModelFileSelector_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult == DialogResult.OK && string.IsNullOrEmpty(this.SelectedFeatureModelFile)) {
                e.Cancel = true;
                MessageBox.Show("No feature model selected", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstFeatureModels_DoubleClick(object sender, EventArgs e) {
            btnOK.PerformClick();
        }
    }
}
