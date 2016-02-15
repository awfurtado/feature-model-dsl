using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling;
using System.Drawing.Imaging;
using System.Web;

namespace UFPE.FeatureModelDSL {
    /// <summary>
    /// Implements the Feature domain concept
    /// </summary>
    [ValidationState(ValidationState.Enabled)]
    public partial class Feature {

        /// <summary>
        /// Gets the name of this feature encoded in HTML, enclosed in bold HTML tags and,
        /// if this feature is a reference, enclosed in a HTML "a href" tag
        /// linking to the definition feature model file.
        /// </summary>
        public string HtmlFormattedName {
            get {
                string result = "<b>" + HttpUtility.HtmlEncode(this.Name) + "</b>";
                if (this.IsReference && !string.IsNullOrEmpty(this.DefinitionFeatureModelFile)) {
                    result = "<a href=\"" + this.DefinitionFeatureModelFile.Replace(".fm", ".html") +"\">" + result + "</a>";
                }
                 return result;
            }
        }

        /// <summary>
        /// Raises a warning if this feature has no description
        /// </summary>
        /// <param name="context"></param>
        [ValidationMethod()]
        public void ValidateDescription(ValidationContext context) {
            if (string.IsNullOrEmpty(this.Description)) {
                    context.LogWarning("No description provided for feature " + this.Name,"",this);
            }
        }

        /// <summary>
        /// Raises an error if IsReference is true but no DefinitionFeatureModelFile was specified.
        /// </summary>
        /// <param name="context"></param>
        [ValidationMethod()]
        public void ValidateDefinitionFeatureModelIsSet(ValidationContext context) {
            if (this.IsReference && string.IsNullOrEmpty(this.DefinitionFeatureModelFile)) {
                context.LogError("Feature '" + this.Name + "' is defined in a feature model other than this. Its Definition Feature Model Diagram property should be filled with the file name of the feature model that defines it.", "", this);
            }
        }
        
        /// <summary>
        /// Raises a warning if DefinitionFeatureModelFile is set and IsReference is false.
        /// </summary>
        /// <param name="context"></param>
        [ValidationMethod()]
        public void ValidateDefinitionFeatureModelIsNotSet(ValidationContext context) {
            if (!this.IsReference && !string.IsNullOrEmpty(this.DefinitionFeatureModelFile)) {
                context.LogWarning("Since feature '" + this.Name + "' is defined in this feature model, its Definition Feature Model File property is of no use and should be left blank.", "", this);
            }
        }

        /// <summary>
        /// Raises an error if the name contains a '\' (which is reserved to Confeaturator).
        /// </summary>
        /// <param name="context"></param>
        [ValidationMethod()]
        public void ValidateName(ValidationContext context) {
            if (this.Name.Contains('\\')) {
                context.LogError("Please do not use the escape character in a feature name.", "", this);
            }
        }
    }
}
