using System;
using Microsoft.VisualStudio.Modeling.Diagrams.GraphObject;

namespace UFPE.FeatureModelDSL {
    /// <summary>
    /// Implementes the Connect connector.
    /// </summary>
    public partial class ConnectConnector {
        
        /// <summary>
        /// Sets the default routing style to VGRouteTreeNS
        /// </summary>
        [CLSCompliant(false)]
        protected override VGRoutingStyle DefaultRoutingStyle {
            get {
                return VGRoutingStyle.VGRouteTreeNS;
            }
        }
    }
}
