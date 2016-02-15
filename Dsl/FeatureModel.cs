using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling;

namespace UFPE.FeatureModelDSL {
    
    //TODO: validate unique names cross-feature models
    //TODO: in HTML report generation, print report for embedded features that contrain others
    //TODO: after connecting an alternative to a node, set the node occurence automatically to not apply.
    //TODO: validate that the Definition Feature Model Diagram property of a feature points to a diagram containing the reference feature.
    //TODO: show pictures in the report: http://social.msdn.microsoft.com/Forums/en-US/vsx/thread/8403ceec-4142-45d6-9091-730353a41c3a

    /// <summary>
    /// Implements the Feature Model domain root.
    /// </summary>
    [ValidationState(ValidationState.Enabled)]
    public partial class FeatureModel {

        /// <summary>
        /// Gets a string describing the total number of mandatory features, optional features,
        /// features under alternatives, reference features and total features.
        /// </summary>
        public string TotalFeaturesReport {
            get {
                int total = 0;
                int totalMandatory = 0;
                int totalOptional = 0;
                int totalReference = 0;
                int totalUnderAlternatives = 0;

                foreach (FeatureModelElement fmElement in this.FeatureModelElements) {
                    if (fmElement is Feature) {
                        Feature feature = fmElement as Feature;
                        total++;
                        if (feature.IsReference) {
                            totalReference++;
                        }

                        switch (feature.Occurence) {
                            case Occurence.Mandatory:
                                totalMandatory++;
                                break;
                            case Occurence.Optional:
                                totalOptional++;
                                break;
                            default:
                                break;
                        }
                    } else if (fmElement is Alternative) {
                        Alternative alternative = fmElement as Alternative;
                        foreach (FeatureModelElement alternativeElement in alternative.SubFeatureModelElements) {
                            if (alternativeElement is Feature) {
                                totalUnderAlternatives++;
                            }
                        }
                    }
                }

                return string.Format("{0} mandatory, {1} optional, {2} under alternatives, {3} references, {4} total",
                    totalMandatory, totalOptional, totalUnderAlternatives, totalReference, total);
            }
        }

        /// <summary>
        /// Validates that this feature mode contains a root feature.
        /// </summary>
        /// <param name="context">The validation context object.</param>
        [ValidationMethod()]
        public void ValidateRootFeature(ValidationContext context) {
            if (RootFeature == null) {
                context.LogError("This feature model contains no root feature", "", this);
            }
        }

        /// <summary>
        /// Validates that all feature models element can be reached from the root feature.
        /// </summary>
        /// <param name="context"></param>
        [ValidationMethod()]
        public void ValidateAllElementsAreReachable(ValidationContext context) {
            foreach (FeatureModelElement fmElement in this.FeatureModelElements) {
                fmElement.Visited = false;
                Feature feature = fmElement as Feature;
                if (feature != null) {
                    if (feature.ConstrainedFeatures.Count > 0) {
                        // elements that constrain others are considered visited
                        fmElement.Visited = true;
                    }
                    if (feature.IsReference) {
                        //elements defined elsewhere are considered visited
                        fmElement.Visited = true;
                    }
                }
            }
            
            Feature rootFeature = RootFeature;
            if (rootFeature != null) {
                Visit(rootFeature);
            }
            
            List<ModelElement> unreachableFeatures = new List<ModelElement>();

            foreach (FeatureModelElement fmElement in this.FeatureModelElements) {
                if (fmElement.Visited == false) {
                    unreachableFeatures.Add(fmElement);
                }
            }

            if (unreachableFeatures.Count > 0) {
                context.LogError("Unreachable feature model elements found", "", unreachableFeatures.ToArray());
            }
        }

        /// <summary>
        /// Recursively sets a feature model element as visited.
        /// </summary>
        /// <param name="fmElement">The feature model element.</param>
        private void Visit(FeatureModelElement fmElement) {
            fmElement.Visited = true;
            foreach (FeatureModelElement subElement in fmElement.SubFeatureModelElements) {
                Visit(subElement);
            }
        }

        /// <summary>
        /// Gets the root feature of this feature model.
        /// </summary>
        public Feature RootFeature {
            get {
                Feature result = null;
                foreach (FeatureModelElement element in this.FeatureModelElements) {
                    Feature feature = element as Feature;
                    if (feature != null && feature.ParentFeatureModelElement == null && !feature.IsReference) {
                        result = element as Feature;
                        break;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Gets a feature in a feature model diagram.
        /// </summary>
        /// <param name="featureName">The feature name.</param>
        /// <returns>A feature</returns>
        public Feature GetFeature(string featureName) {
            Feature result = null;
            foreach (FeatureModelElement fmElement in this.FeatureModelElements) {
                Feature resultCandidate = fmElement as Feature;
                if (resultCandidate != null && resultCandidate.Name == featureName) {
                    result = resultCandidate;
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the root feature of a given feature model instance, searching up into the parent feature model files.
        /// </summary>
        /// <param name="featureModel">The feature model.</param>
        /// <returns>The feature model's root feature</returns>
        public static Feature GetCrossDiagramRootFeature(FeatureModel featureModel) {
            if (!string.IsNullOrEmpty(featureModel.ParentFeatureModelFile)) {
                FeatureModel parentFeatureModel = Util.LoadFeatureModel(DTEHelper.GetFullProjectItemPath(featureModel.ParentFeatureModelFile));
                return GetCrossDiagramRootFeature(parentFeatureModel);
            } else {
                return featureModel.RootFeature;
            }
        }
    }
}
