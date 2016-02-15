using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.Windows.Forms;

namespace UFPE.FeatureModelDSL.Confeaturator {
    /// <summary>
    /// Event arguments used for Confeaturator Action Provider Handlers
    /// </summary>
    public class ConfeaturatorActionProviderEventArgs : EventArgs {

        /// <summary>
        /// The root node of the configuration tree
        /// </summary>
        public FeatureModelTreeNode RootFeatureNode { get; set; }

        /// <summary>
        /// Service provider.
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Development Tools Extensibility object, used to provide VS automation.
        /// </summary>
        [CLSCompliant(false)]
        public DTE DTE { get; set; }

        /// <summary>
        /// Creates a ConfeaturatorActionProviderEventArgs instance.
        /// </summary>
        /// <param name="rootFeatureNode">The root feature model node.</param>
        /// <param name="serviceProvider">A service provider.</param>
        public ConfeaturatorActionProviderEventArgs(FeatureModelTreeNode rootFeatureNode, IServiceProvider serviceProvider) {
            RootFeatureNode = rootFeatureNode;
            ServiceProvider = serviceProvider;
            DTE = serviceProvider.GetService(typeof(DTE)) as DTE;
        }

        /// <summary>
        /// Checks if a given feature was included in the user configuration. Returns always true for
        /// mandatory features.
        /// </summary>
        /// <param name="featureName">The name of the feature to be checked.</param>
        /// <returns>Wheter the feature was included in the user configuration.</returns>
        public bool ConfigurationIncludesFeature(string featureName) {
            bool result = false;
            try {
                TreeNode[] nodes = RootFeatureNode.Nodes.Find(featureName, true);
                foreach (FeatureModelTreeNode node in nodes) {
                    if (node.IsPartOfConfiguration) {
                        result = true;
                        break;
                    }
                }
                return result;

            } catch (Exception ex) {
                DTEHelper.DTE.StatusBar.Text = "Error checking if feature '" + featureName + "' was selected:" + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Gets a list of all features included in the user configuration, either mandatory or selected optional features.
        /// </summary>
        /// <returns>A list of all features included in the user configuration, either mandatory or selected optional features.</returns>
        public List<Feature> GetAllIncludedFeatures() {
            List<Feature> result = new List<Feature>();
            LogIncludedFeatures(RootFeatureNode, result);
            return result;
        }

        /// <summary>
        /// Recursively logs included features from a given Feature Model tree node into a list of features.
        /// </summary>
        /// <param name="node">The node from where to start the recursion.</param>
        /// <param name="result">A cumulative list of included features.</param>
        private void LogIncludedFeatures(FeatureModelTreeNode node, List<Feature> result) {
            Feature feature = node.FeatureModelElement as Feature;
            if (feature != null && node.IsPartOfConfiguration) {
                result.Add(feature);
            }
            foreach (FeatureModelTreeNode childNode in node.Nodes) {
                LogIncludedFeatures(childNode, result);
            }
        }
    }
}
