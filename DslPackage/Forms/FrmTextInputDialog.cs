using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UFPE.FeatureModelDSL.Forms {
    /// <summary>
    /// A dialog for getting simple user text input.
    /// </summary>
    public partial class FrmTextInputDialog : Form {

        /// <summary>
        /// The user text input.
        /// </summary>
        public string InputText {
            get {
                return txtInput.Text;
            }
        }
        
        /// <summary>
        /// Creates a FrmTextInputDialog instance.
        /// </summary>
        public FrmTextInputDialog() {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a FrmTextInputDialog instance.
        /// </summary>
        /// <param name="titleText">The dialog title.</param>
        /// <param name="lblText">The label text.</param>
        public FrmTextInputDialog(string titleText, string lblText)
            : this() {
            this.Text = titleText;
            this.lblText.Text = lblText;
        }

        /// <summary>
        /// Creates a FrmTextInputDialog instance.
        /// </summary>
        /// <param name="titleText">The dialog title.</param>
        /// <param name="lblText">The label text.</param>
        /// <param name="initialText">The initial text displayed in the dialog's textbox</param>
        public FrmTextInputDialog(string titleText, string lblText, string initialText)
            : this() {
            this.Text = titleText;
            this.lblText.Text = lblText;
            this.txtInput.Text = initialText;
        }

        /// <summary>
        /// Handler for loading the form.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void FrmTextInputDialog_Load(object sender, EventArgs e) {
            txtInput.Focus();
        }
    }
}
