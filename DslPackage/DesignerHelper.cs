using System;
using Microsoft.VisualStudio.Modeling.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace UFPE.FeatureModelDSL {
    /// <summary>
    /// Providers helper functions to manipulate language designers
    /// </summary>
    public static class DesignerHelper {
        
        /// <summary>
        /// Gets a window frame property.
        /// </summary>
        /// <param name="provider">A service provider.</param>
        /// <param name="propertyId">A property id.</param>
        /// <returns>A window frame property.</returns>
        private static ModelingDocView GetWindowFrameProperty(IServiceProvider provider, __VSFPROPID propertyId) {
            IVsMonitorSelection selection = (IVsMonitorSelection)provider.GetService(typeof(IVsMonitorSelection));
            if (selection != null) {
                object frameObject;
                selection.GetCurrentElementValue(2, out frameObject);

                IVsWindowFrame windowFrame = frameObject as IVsWindowFrame;
                if (windowFrame != null) {
                    object propertyValue;
                    windowFrame.GetProperty((int)propertyId, out propertyValue);
                    return propertyValue as ModelingDocView;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a modeling DocView.
        /// </summary>
        /// <param name="provider">A service provider.</param>
        /// <returns></returns>
        [CLSCompliant (false)]
        public static ModelingDocView GetModelingDocView(IServiceProvider provider) {
            return GetWindowFrameProperty(provider, __VSFPROPID.VSFPROPID_DocView);
        }


        /// <summary>
        /// Gets a diagram DocView.
        /// </summary>
        /// <param name="provider">A service provider.</param>
        [CLSCompliant(false)]
        public static SingleDiagramDocView GetDiagramDocView(IServiceProvider provider) {
            return GetWindowFrameProperty(provider, __VSFPROPID.VSFPROPID_DocView) as SingleDiagramDocView;
        }
    }
}
