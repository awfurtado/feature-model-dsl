using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace UFPE.FeatureModelDSL.Confeaturator {
    
    /// <summary>
    /// Form responsible for adding and removing Confeaturator Action Providers
    /// </summary>
    public partial class FrmAddRemoveConfeaturatorActionProviders : Form {

        /// <summary>
        /// Creates a FrmAddRemoveConfeaturatorActionProviders instance.
        /// </summary>
        public FrmAddRemoveConfeaturatorActionProviders() {
            InitializeComponent();        
        }

        /// <summary>
        /// Creates a FrmAddRemoveConfeaturatorActionProviders instance.
        /// </summary>
        /// <param name="confeaturatorActionSettings">A list of Confeaturator Action Provider settings</param>
        public FrmAddRemoveConfeaturatorActionProviders(List<ConfeaturatorActionProviderSetting> confeaturatorActionProviderSettings) : this() {
            confeaturatorActionProviderSettingBindingSource.Clear();
            foreach (ConfeaturatorActionProviderSetting confeaturatorSetting in confeaturatorActionProviderSettings) {
                confeaturatorActionProviderSettingBindingSource.Add(confeaturatorSetting);
            }
            dgvConfeaturatorActionProviders.DataError += new DataGridViewDataErrorEventHandler(dgvConfeaturatorActionProviders_DataError);
        }

        /// <summary>
        /// Handles DataGridView data errors in order to display them.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        void dgvConfeaturatorActionProviders_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            MessageBox.Show(e.Exception.Message);
        }

        /// <summary>
        /// Button click event handler that opens a dialog to find Confeaturator Action Providers.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnFind_Click(object sender, EventArgs e) {
            FrmFindConfeaturatorActionProvider frmFind = new FrmFindConfeaturatorActionProvider();
            if (frmFind.ShowDialog() == DialogResult.OK) {
                confeaturatorActionProviderSettingBindingSource.Add(frmFind.ConfeaturatorActionProviderSetting);
            }
        }

        /// <summary>
        /// Gets the list of ConfeaturatorActionProviderSettings.
        /// </summary>
        public List<ConfeaturatorActionProviderSetting> ConfeaturatorActionProviderSettings {
            get {
                List<ConfeaturatorActionProviderSetting> result = new List<ConfeaturatorActionProviderSetting>();
                foreach (ConfeaturatorActionProviderSetting confeaturatorSetting in confeaturatorActionProviderSettingBindingSource) {
                    result.Add(confeaturatorSetting);
                }
                return result;
            }
        }

        /// <summary>
        /// Button click event handler that displays help to the user.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnHelp_Click(object sender, EventArgs e) {
            string helpMessage = "Use this window to add a class (and its assembly) that implements IConfeaturatorActionProvider. The dll should exist as a project item in your project.";
            MessageBox.Show(helpMessage, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}

