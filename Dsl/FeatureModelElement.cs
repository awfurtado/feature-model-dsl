using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UFPE.FeatureModelDSL {
    /// <summary>
    /// Implements the Feature Model Element domain concept.
    /// </summary>
    public abstract partial class FeatureModelElement {
        /// <summary>
        /// Used in validations from the FeatureModel
        /// </summary>
        internal bool Visited { get; set; }
    }
}
