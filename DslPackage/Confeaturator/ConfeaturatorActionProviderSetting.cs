using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UFPE.FeatureModelDSL.Confeaturator {

    
    /// <summary>
    /// Contains the settings of a Confeaturator Action Provider
    /// </summary>
    public class ConfeaturatorActionProviderSetting {
        
        /// <summary>
        /// The assembly name of the confeaturator action provider, relative to the current project.
        /// </summary>
        [XmlAttribute]
        public string AssemblyName { get; set; }
        
        /// <summary>
        /// The qualified name of the class that implements IConfeaturatorActionProvider.
        /// </summary>
        [XmlAttribute]
        public string QualifiedClassName { get; set; }

        /// <summary>
        /// Creates an instance of a ConfeaturatorActionProviderSetting.
        /// </summary>
        /// <param name="assemblyName">The assembly name.</param>
        /// <param name="qualifiedClassName">The qualified name of the class.</param>
        public ConfeaturatorActionProviderSetting(string assemblyName, string qualifiedClassName) :this (){
            this.AssemblyName = assemblyName;
            this.QualifiedClassName = qualifiedClassName;
        }

        public ConfeaturatorActionProviderSetting() {

        }
    }
}
