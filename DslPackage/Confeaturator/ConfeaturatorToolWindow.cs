using System;
using System.Windows.Forms;
using VSShellInterop = Microsoft.VisualStudio.Shell.Interop;
using VSShell = Microsoft.VisualStudio.Shell;
using DslShell = Microsoft.VisualStudio.Modeling.Shell;
using DslDesign = Microsoft.VisualStudio.Modeling.Design;
using VSTextTemplatingHost = Microsoft.VisualStudio.TextTemplating.VSHost;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Shell;
using UFPE.FeatureModelDSL.Forms;
using EnvDTE;

namespace UFPE.FeatureModelDSL.Confeaturator {
    
    
    /// <summary>
    /// Creates a tool window and gives it a title, icon, and label;
    /// </summary>
    [CLSCompliant(false)]
    public class ConfeaturatorToolWindow : DslShell.ToolWindow {

        /// <summary>
        /// Reference to the Confeaturator control.
        /// </summary>
        private CtrlConfeaturator confeaturator;

       /// <summary>
        /// Creates the tool window.
       /// </summary>
       /// <param name="serviceProvider">A service provider.</param>
        public ConfeaturatorToolWindow(IServiceProvider serviceProvider)
            : base(serviceProvider) {

        }

        /// <summary>
        /// Gets the icon for the tool window.
        /// </summary>
        protected override int BitmapResource {
            get { return 104; }
        }

        /// <summary>
        /// Gets the index for the icon.
        /// </summary>
        protected override int BitmapIndex {
            get { return 0; }
        }

        /// <summary>
        /// Gets the name of the tool window.
        /// </summary>
        public override string WindowTitle {
            get { return "Confeaturator"; }
        }

        /// <summary>
        /// Window creation.
        /// </summary>
        protected override void OnToolWindowCreate() {
            confeaturator = new CtrlConfeaturator(ServiceProvider);
            
        }

        /// <summary>
        /// Puts a Confeaturator on the tool window
        /// </summary>
        public override System.Windows.Forms.IWin32Window Window {
            get { return this.confeaturator; }
        }
    }
}
