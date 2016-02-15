using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using Microsoft.VisualStudio.Modeling;
using System.Windows.Forms;

namespace UFPE.FeatureModelDSL.CustomTypeEditors {
    /// <summary>
    /// UI Type editor for the FeatureModelDiagram
    /// </summary>
    public class FeatureModelDiagramTypeEditor : UITypeEditor {

        private IWindowsFormsEditorService edSvc = null;

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context) {
            if (context != null) {
                return UITypeEditorEditStyle.Modal;
            }
            return base.GetEditStyle(context); 
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            // Default behavior 
            if ((context == null) ||
                (provider == null) || (context.Instance == null)) {
                return base.EditValue(context, provider, value);
            }

            ModelElement modelElement = (context.Instance as ModelElement);
            edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null) {
                FrmFeatureModelDiagramSelector form = new FrmFeatureModelDiagramSelector(modelElement.Store);
                if (edSvc.ShowDialog(form) == DialogResult.OK) {
                    using (Transaction transaction = modelElement.Store.TransactionManager.BeginTransaction("UpdatingFeatureModelFileValue")) {

                        if (modelElement is FeatureShape) {
                            FeatureShape featureShape = modelElement as FeatureShape;
                            (featureShape.ModelElement as Feature).DefinitionFeatureModelFile = form.SelectedFeatureModelFile;
                        } else if (modelElement is FeatureModelDSLDiagram) {
                            FeatureModelDSLDiagram featureModelDslDiagram = modelElement as FeatureModelDSLDiagram;
                            (featureModelDslDiagram.ModelElement as FeatureModel).ParentFeatureModelFile = form.SelectedFeatureModelFile;
                        }

                        transaction.Commit();
                    }
                }
            }
            

            // Default behavior 
            return base.EditValue(context, provider, value);
        } 
    }
}
