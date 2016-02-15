using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;

namespace UFPE.FeatureModelDSL.Confeaturator {
    
    /// <summary>
    /// Interface that should be implemented by Confeaturator Action Providers
    /// </summary>
    [CLSCompliant(false)]
    public interface IConfeaturatorActionProvider {
        void PerformConfeaturatorAction(DTE dte, List<string> features);
    }
}
