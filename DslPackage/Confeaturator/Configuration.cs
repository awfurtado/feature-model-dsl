using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UFPE.FeatureModelDSL.Confeaturator {
    /// <summary>
    /// A feature model configuration.
    /// </summary>
    internal struct Configuration {
        /// <summary>
        /// Name of the product that corresponds to this configuration,
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Features selected in this configuration.
        /// </summary>
        public Dictionary<string, bool> Features { get; set; }

        // TODO: investigate performance of Dictionary x List in .NET and eventually change the property above to List<string>.
    }
}
