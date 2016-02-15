using Microsoft.VisualStudio.Modeling.Validation;

namespace UFPE.FeatureModelDSL {
    /// <summary>
    /// Implements the Alternative domain concept
    /// </summary>
    [ValidationState(ValidationState.Enabled)]
    public partial class Alternative {
        /// <summary>
        /// Gets the value of the MinMaxIntervalText calculated property
        /// </summary>
        /// <returns></returns>
        public string GetMinMaxIntervalTextValue() {
            return string.Format("[{0}..{1}]", this.Min, this.Max);
        }


        /// <summary>
        /// Validates the number, type and properties of features connected to this alternative group.
        /// </summary>
        /// <param name="context">The validation context</param>
        [ValidationMethod()]
        public void ValidateSubFeatures(ValidationContext context) {
            foreach (FeatureModelElement fmElement in this.SubFeatureModelElements) {
                if (fmElement is Alternative) {
                    context.LogError("An alternative feature set should not be connected to other alternative feature set", "", this, fmElement);
                } else if (fmElement is Feature) {
                    Feature feature = fmElement as Feature;
                    if (feature.Occurence != Occurence.NotApply) {
                        context.LogError("Alternative features should have their occurrence set to Not Apply","",feature);
                    }
                }
            }
            if (this.SubFeatureModelElements.Count < 2) {
                context.LogError("Alternatives should have at least two features", "", this);
            }
        }

        /// <summary>
        /// Provides validations regarding the minimum and maximum number of features specified for this alternative.
        /// </summary>
        /// <param name="context">The validation context</param>
        [ValidationMethod()]
        public void ValidateMinMax(ValidationContext context) {
            if (this.Min > this.Max) {
                context.LogError("The minimum number of features in an alternative set cannot be greater than the maximum number","",this);
            }
            if (this.Min < 1 || this.Max < 1) {
                context.LogError("The minimum or maximum number of features in an alternative set cannot be lower than one", "", this);
            }
            if (this.Min > this.SubFeatureModelElements.Count) {
                context.LogError("The minimum number of features in an alternative set should not be greater than the actual number of features connected to the alternative set", "", this);
            }
            if (this.Max > this.SubFeatureModelElements.Count) {
                context.LogError("The maximum number of features in an alternative set should not be greater than the actual number of features connected to the alternative set", "", this);
            }
        }
    }
}
