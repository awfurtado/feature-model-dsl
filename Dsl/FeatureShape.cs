using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using EnvDTE;
using System.Windows.Forms;

namespace UFPE.FeatureModelDSL {
    /// <summary>
    /// Implements the FeatureShape geometry shape
    /// </summary>
    public partial class FeatureShape {

        /// <summary>
        /// Opens the definition feature model file of a reference feature
        /// </summary>
        /// <param name="e"></param>
        public override void OnDoubleClick(DiagramPointEventArgs e) {
            Feature feature = this.ModelElement as Feature;
            if (feature != null && feature.IsReference && !string.IsNullOrEmpty(feature.DefinitionFeatureModelFile)) {
                string filePath = DTEHelper.GetProjectItemPath(feature.DefinitionFeatureModelFile);
                if (!string.IsNullOrEmpty(filePath)) {
                    DTEHelper.OpenFile(filePath);
                } else {
                    MessageBox.Show("Definition feature model '" + feature.DefinitionFeatureModelFile + "' was not found. Is the Definition Feature Model File property of this feature out of date?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
