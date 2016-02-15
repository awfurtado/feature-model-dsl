
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSShellInterop = Microsoft.VisualStudio.Shell.Interop;
using VSShell = Microsoft.VisualStudio.Shell;
using DslShell = Microsoft.VisualStudio.Modeling.Shell;
using DslDesign = Microsoft.VisualStudio.Modeling.Design;
using VSTextTemplatingHost = Microsoft.VisualStudio.TextTemplating.VSHost;
using UFPE.FeatureModelDSL.Confeaturator;

namespace UFPE.FeatureModelDSL {
    /// <summary>
    /// The FeatureModelDSL Package
    /// </summary>
    [VSShell::ProvideToolWindowVisibility(typeof(ConfeaturatorToolWindow),
             Constants.FeatureModelDSLEditorFactoryId)]
    [VSShell::ProvideToolWindow(typeof(ConfeaturatorToolWindow),
              MultiInstances = false,
              Style = VSShell::VsDockStyle.Tabbed,
              Orientation = VSShell::ToolWindowOrientation.Left,
              //Window = "{3AE79031-E1BC-11D0-8F78-00A0C9110057}")]
              Window = "{B1E99781-AB81-11D0-B683-00AA00A3EE26}")]
    internal partial class FeatureModelDSLPackage {
        protected override void Initialize() {
            base.Initialize();
            DTEHelper.Initialize(this);        
            this.AddToolWindow(typeof(ConfeaturatorToolWindow));
        }
    }
}
