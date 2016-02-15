using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;

namespace UFPE.FeatureModelDSL
{
    /// <summary>
    /// Utilitary methods.
    /// </summary>
    public static class Util
    {

        /// <summary>
        /// Shows an error message in a default error message box.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public static void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a warning message in a default warning message box.
        /// </summary>
        /// <param name="warningMessage">The warning message.</param>
        public static void ShowWarning(string warningMessage)
        {
            MessageBox.Show(warningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows a success message in a default success message box.
        /// </summary>
        /// <param name="successMessage">The success message.</param>
        public static void ShowSuccess(string successMessage)
        {
            MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Loads a Feature Model from a .fm file
        /// </summary>
        /// <param name="fileName">Feature Model file (.fm)</param>
        /// <returns></returns>
        public static FeatureModel LoadFeatureModel(string fileName)
        {
            FeatureModel result = null;
            Store store = new Store();
            Type[] modelTypes = new Type[] {
                typeof(Microsoft.VisualStudio.Modeling.Diagrams.CoreDesignSurfaceDomainModel),
                typeof(UFPE.FeatureModelDSL.FeatureModelDSLDomainModel)
            };
            store.LoadDomainModels(modelTypes);
            using (Transaction t = store.TransactionManager.BeginTransaction("Load model", true))
            {
                result = FeatureModelDSLSerializationHelper.Instance.LoadModel(store, fileName, null, null, null);
                t.Commit();
            }

            return result;
        }
    }
}
