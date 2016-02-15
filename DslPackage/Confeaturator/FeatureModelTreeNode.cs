using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace UFPE.FeatureModelDSL.Confeaturator {

    /// <summary>
    /// Enumeration for the feature node status and image indexes. The order should not be changed!
    /// </summary>
    internal enum FeatureModelNodeStatus {
        CheckedAndEnabled = 0,
        CheckedAndDisabled = 1,
        Unchecked = 2,
        Alternative = 3,
    }

    /// <summary>
    /// Enumeration for the feature node kind.
    /// </summary>
    public enum FeatureModelNodeKind {
        Alternative,
        Mandatory,
        Optional,
        NotApply,
        Root,
    }

    /// <summary>
    /// TreeNode that encapsulates a feaure.
    /// </summary>
    public class FeatureModelTreeNode : TreeNode {

        /// <summary>
        /// The encapsulated feature model element (feature or alternative).
        /// </summary>
        public FeatureModelElement FeatureModelElement { get; set; }

        /// <summary>
        /// Feature model tree node kind.
        /// </summary>
        public FeatureModelNodeKind Kind { get; set; }

        /// <summary>
        /// Feature model tree node status.
        /// </summary>
        internal FeatureModelNodeStatus Status {
            get {
                return (FeatureModelNodeStatus)this.ImageIndex;
            }
            set {
                this.ImageIndex = (int)value;
            }
        }

        /// <summary>
        /// Wheter a node is checked. Alternative nodes returns false. Being checked doesn't mean the
        /// feature of this node is part of the configuration, since a parent node can be unchecked. Use
        /// the IsPartOfConfiguration for that.
        /// </summary>
        internal bool IsChecked {
            get {
                return
                    this.Status == FeatureModelNodeStatus.CheckedAndDisabled
                    || this.Status == FeatureModelNodeStatus.CheckedAndEnabled;
            }
        }

        /// <summary>
        /// Checks if this node is part of the current configuration, i.e., if all of its parents and grandparents are
        /// checked up to the root node.
        /// </summary>
        public bool IsPartOfConfiguration {
            get {
                bool result = true;
                FeatureModelTreeNode nodeUnderInvestigation = this;
                while (nodeUnderInvestigation != null) {
                    if (nodeUnderInvestigation.Status == FeatureModelNodeStatus.Unchecked) {
                        result = false;
                        break;
                    } else {
                        nodeUnderInvestigation = nodeUnderInvestigation.Parent as FeatureModelTreeNode;
                    }

                }
                return result;
            }
        }

        /// <summary>
        /// Used to avoid node to be checked/unchecked when the user clicks on the MinusPlus icon next to the node.
        /// </summary>
        internal bool IsCollapsingOrExpanding { get; set; }

        /// <summary>
        /// Creates a FeatureModelTreeNode instance.
        /// </summary>
        /// <param name="fmElement">The feature model element that will belong to this node.</param>
        public FeatureModelTreeNode(FeatureModelElement fmElement) {
            if (fmElement == null) {
                throw new ArgumentException("Cannot build Confeaturator tree with null features");
            }
            this.FeatureModelElement = fmElement;
            Alternative alternative = fmElement as Alternative;
            if (alternative != null) {
                this.Text = "Alternative " + alternative.MinMaxIntervalText;
                this.Status = FeatureModelNodeStatus.Alternative;
                this.ForeColor = Color.DimGray;
                this.Kind = FeatureModelNodeKind.Alternative;
            } else {
                Feature feature = fmElement as Feature;
                this.Text = feature.Name;
                switch (feature.Occurence) {
                    case Occurence.Mandatory:
                        this.Status = FeatureModelNodeStatus.CheckedAndDisabled;
                        this.Kind = FeatureModelNodeKind.Mandatory;
                        this.Checked = true;
                        break;
                    case Occurence.Optional:
                        this.Status = FeatureModelNodeStatus.Unchecked;
                        this.Kind = FeatureModelNodeKind.Optional;
                        break;
                    case Occurence.NotApply:
                        this.Status = FeatureModelNodeStatus.Unchecked;
                        this.Kind = FeatureModelNodeKind.NotApply;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the font of a node to italic case it is an optional ocurrence.
        /// </summary>
        /// <param name="font"></param>
        internal void AdjustFont(Font font){
            Feature feature = this.FeatureModelElement as Feature;
            if (feature != null && feature.Occurence == Occurence.Optional && feature.Occurence == Occurence.NotApply && this.Kind != FeatureModelNodeKind.Root) {
                this.NodeFont = new Font(font, FontStyle.Italic);
            }
        }

        /// <summary>
        /// Clicks in a Optional/NotApply node (no effect on other nodes). Status of child nodes to not change automatically when checking/unchecking an optional
        /// node, due to usability reasons.
        /// </summary>
        /// <param name="node"></param>
        internal void Click() {
            if (this.Kind == FeatureModelNodeKind.Optional
                || this.Kind == FeatureModelNodeKind.NotApply) {
                if (this.Checked) {
                    this.UnCheck();
                } else {
                    this.Check();
                }
            }
            this.SelectedImageIndex = this.ImageIndex;
        }

        /// <summary>
        /// Unchecks this node.
        /// </summary>
        internal void UnCheck() {
            this.Checked = false;
            this.Status = FeatureModelNodeStatus.Unchecked;
        }

        /// <summary>
        /// Checks this node.
        /// </summary>
        internal void Check() {
            this.Checked = true;
            this.Status = FeatureModelNodeStatus.CheckedAndEnabled;
        }


        /// <summary>
        /// Recursively logs configuration violations on alternative intervals.
        /// </summary>
        /// <param name="errors">List of messages in which errors should be logged.</param>
        internal void LogAlternativeIntervalErrors(List<string> errorMessages) {
            if (this.Kind == FeatureModelNodeKind.Alternative
                && this.IsPartOfConfiguration) {
                Alternative alternative = this.FeatureModelElement as Alternative;
                if (alternative != null) {
                    int totalChildrenChecked = 0;
                    foreach (FeatureModelTreeNode childNode in this.Nodes) {
                        if (childNode.IsChecked) {
                            totalChildrenChecked++;
                        }
                    }

                    string nodeFullPath = this.Parent.FullPath;
                    if (totalChildrenChecked < alternative.Min) {
                        string errorMsg = string.Format("Alternative under path '{0}' should have at least {1} child(ren) selected", nodeFullPath, alternative.Min);
                        errorMessages.Add(errorMsg);
                    }
                    if (totalChildrenChecked > alternative.Max) {
                        string errorMsg = string.Format("Alternative under path '{0}' should have at most {1} child(ren) selected", nodeFullPath, alternative.Max);
                        errorMessages.Add(errorMsg);
                    }

                } else {
                    DTEHelper.DTE.StatusBar.Text = "Warning (possible FeatureModelDSL bug): FeatureModelNodeKind.Alternative with null Alternative.";
                }

            }
            foreach (FeatureModelTreeNode childNode in this.Nodes) {
                childNode.LogAlternativeIntervalErrors(errorMessages);
            }
        }
    }
}
