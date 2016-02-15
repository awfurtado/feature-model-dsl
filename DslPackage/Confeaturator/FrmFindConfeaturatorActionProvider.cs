using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using System.Reflection;
using System.IO;

namespace UFPE.FeatureModelDSL.Confeaturator {
    /// <summary>
    /// Lists Confeaturator Action Providers in the current Visual Studio project.
    /// </summary>
    public partial class FrmFindConfeaturatorActionProvider : Form {

        /// <summary>
        /// The Confeaturator Action Provider selected by the user.
        /// </summary>
        public ConfeaturatorActionProviderSetting ConfeaturatorActionProviderSetting { get; set; }

        /// <summary>
        /// Creates a FrmFindConfeaturatorActionProvider instance. Searches through the current Visual Studio 
        /// Project to list all Confeaturator Action Providers found.
        /// </summary>
        public FrmFindConfeaturatorActionProvider() {
            InitializeComponent();
            Project project = DTEHelper.Project;
            TreeNode rootNode = new TreeNode(project.Name);
            rootNode.ImageIndex = 0; // project icon
            trvProject.Nodes.Add(rootNode);
            foreach (ProjectItem projectItem in project.ProjectItems) {
                if (projectItem.Name.EndsWith(".dll")) {
                    //// Action Provider assemblies are loaded in a different application domain, in order to not lock the assembly.
                    //AppDomain tempDomain = null;
                    try {
                        //AppDomainSetup appDomainSetup = new AppDomainSetup();
                        ////// setting ShadowCopyFile to true so that we don't lock the assembly
                        //appDomainSetup.ShadowCopyFiles = "true";
                        //appDomainSetup.ApplicationBase = Path.GetDirectoryName(projectItem.get_FileNames(0));
                        //tempDomain = AppDomain.CreateDomain("TempConfeaturatorDomain",null,appDomainSetup);
                        ////Assembly assembly = tempDomain.Load(AssemblyName.GetAssemblyName(projectItem.get_FileNames(0)));
                        Assembly assembly = Assembly.LoadFile(projectItem.get_FileNames(0));
                        string assemblyName = projectItem.Name;
                        TreeNode assemblyNode = new TreeNode(assemblyName);
                        assemblyNode.ImageIndex = 1; // assembly icon
                        rootNode.Nodes.Add(assemblyNode);
                        Type[] types = assembly.GetExportedTypes();
                        foreach (Type type in types) {
                            if (type.IsClass && typeof(IConfeaturatorActionProvider).IsAssignableFrom(type)) {
                                TreeNode classNode = new TreeNode(type.Name);
                                classNode.ImageIndex = 2; //class icon
                                classNode.Tag = new ConfeaturatorActionProviderSetting(assemblyName, type.FullName);
                                assemblyNode.Nodes.Add(classNode);
                                assemblyNode.Expand();
                                if (trvProject.SelectedNode == null) {
                                    trvProject.SelectedNode = classNode;
                                    trvProject.Select();
                                }
                            }
                        }
                    } catch (Exception ex) {
                        DTEHelper.DTE.StatusBar.Text = "Error loading assembly: " + ex.Message;
                        this.DialogResult = DialogResult.Cancel;
                        btnCancel.PerformClick();
                    } finally {
                        //if (tempDomain != null) {
                        //    AppDomain.Unload(tempDomain);
                        //}
                    }
                }
            }
            rootNode.Expand();
        }

        /// <summary>
        /// Button click event handler that tries to close the form.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnOK_Click(object sender, EventArgs e) {
            TryClose();
        }

        /// <summary>
        /// Closes the form if a valid Confeaturator Action Provider is selected.
        /// </summary>
        private void TryClose() {
            if (trvProject.SelectedNode == null) {
                Util.ShowError("No action provider selected");
            } else if (trvProject.SelectedNode.Tag == null) {
                Util.ShowError("The selected project item is not a valid Confeaturator action provider");
            } else {
                this.ConfeaturatorActionProviderSetting = (ConfeaturatorActionProviderSetting)trvProject.SelectedNode.Tag;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        
        /// <summary>
        /// Tries to close the form when the user double-clicks a node in the Confeaturator Action Providers tree.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void trvProject_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            TryClose();
        }

        /// <summary>
        /// Maintains the Confeaturator Action Providers tree image indexes.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void trvProject_AfterSelect(object sender, TreeViewEventArgs e) {
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }
    }
}
