using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Windows.Forms;
using System.Xml.Serialization;
using EnvDTE;
using Microsoft.VisualStudio.Modeling.Shell;

namespace UFPE.FeatureModelDSL.Confeaturator {

    /// <summary>
    /// Event that enables other code to be launched when the Confeaturator is asked to perform environment configuration.
    /// This makes it possible to add Action Providers to the Confeaturator at compile time (the other possibility is
    /// to bind Confeaturator Action Providers at runtime by using the Confeaturator Action Providers XML settings file and UIs)
    /// </summary>
    /// <param name="sender">The event sender object.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void ConfeaturatorActionProviderHandler(object sender, ConfeaturatorActionProviderEventArgs e);

    /// <summary>
    /// The Confeaturator user control, hosted in a Visual Studio toolwindow. It enables the user to select features from the Feature Model and
    /// launch actions based on the selected features, provided by him/herself of third parties.
    /// </summary>
    public partial class CtrlConfeaturator : UserControl {

        #region Confeaturator fields and properties

        /// <summary>
        /// Location of the Confeaturator Settings file.
        /// </summary>
        const string confeaturatorSettingsFile = "ConfeaturatorSettings.xml";

        /// <summary>
        /// Service provider received from the ToolWindow
        /// </summary>
        private IServiceProvider serviceProvider;

        /// <summary>
        /// Serializer object used to save and load Confeaturator settings regarding to registered Confeaturator Action Providers.
        /// </summary>
        private XmlSerializer settingsXmlSerializer = new XmlSerializer(typeof(List<ConfeaturatorActionProviderSetting>));

        /// <summary>
        /// List of features used to avoid adding the same feature to a Confeaturator tree more than twice
        /// (situation that commonly happens in cross-feature model scenarios)
        /// </summary>
        private List<string> addedFeatures;

        /// <summary>
        /// Event to handle environment configuration launched from the confeaturator tool window.
        /// </summary>
        public event ConfeaturatorActionProviderHandler ConfeaturatorActions;

        /// <summary>
        /// Checks whether the Confeaturator feature tree view has nodes.
        /// </summary>
        private bool ConfeaturatorTreeViewHasNodes {
            get {
                return
                    this.trvFeatures != null
                    && this.trvFeatures.Nodes != null
                    && this.trvFeatures.Nodes.Count > 0;
            }
        }

        #endregion

        #region Confeaturator constructors

        /// <summary>
        /// Creates a new instance of the Confeaturator control.
        /// </summary>
        private CtrlConfeaturator() {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance of the Confeaturator control.
        /// </summary>
        /// <param name="serviceProvider">Service provider received from the ToolWindow.</param>
        public CtrlConfeaturator(IServiceProvider serviceProvider)
            : this() {
            this.serviceProvider = serviceProvider;
        }

        #endregion

        #region Confeaturator initialization

        /// <summary>
        /// Event handler that creates or updates the Confeaturator tree.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnRefresh_Click(object sender, EventArgs e) {
            RefreshConfeaturatorTree();
        }

        /// <summary>
        /// Creates or updates the Confeaturator tree.
        /// </summary>
        private void RefreshConfeaturatorTree() {
            try {
                SingleDiagramDocView diagramDocView = DesignerHelper.GetDiagramDocView(serviceProvider);
                FeatureModelDSLDiagram diagram = diagramDocView.CurrentDiagram as FeatureModelDSLDiagram;
                if (diagram != null) {
                    FeatureModel featureModel = diagram.Subject as FeatureModel;
                    if (featureModel != null) {
                        Feature rootFeature = FeatureModel.GetCrossDiagramRootFeature(featureModel);
                        trvFeatures.Nodes.Clear();
                        if (rootFeature != null) {
                            addedFeatures = new List<string>();
                            FeatureModelTreeNode rootNode = CreateFeatureModelTreeNode(rootFeature);
                            trvFeatures.Nodes.Add(rootNode);
                            rootNode.Expand();
                            rootNode.Kind = FeatureModelNodeKind.Root;
                            rootNode.Status = FeatureModelNodeStatus.CheckedAndDisabled;
                            trvFeatures.SelectedNode = rootNode;
                        } else {
                            Util.ShowError("Feature model was loaded but root feature is null.");
                        }
                    } else {
                        Util.ShowError("Could not load feature model into Confeaturator. Please ensure you have a valid feature model file opened.");
                    }
                } else {
                    Util.ShowError("Please have the feature model diagram you want to refresh into Confeaturator open and active in Visual Studio.");
                }
            } catch (Exception ex) {
                Util.ShowError("Error loading feature model into Confeaturator: " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Confeaturator node.
        /// </summary>
        /// <param name="fmElement">Feature model element used to create the node.</param>
        /// <returns>The created Confeaturator node.</returns>
        private FeatureModelTreeNode CreateFeatureModelTreeNode(FeatureModelElement fmElement) {
            FeatureModelTreeNode node = new FeatureModelTreeNode(fmElement);
            AddChildFeatureModelNodes(node);
            node.AdjustFont(trvFeatures.Font);
            return node;
        }

        /// <summary>
        /// Adds child Confeaturator nodes to a given node.
        /// </summary>
        /// <param name="node">The node to which children should be added.</param>
        private void AddChildFeatureModelNodes(FeatureModelTreeNode node) {
            FeatureModelElement fmElement = node.FeatureModelElement;
            foreach (FeatureModelElement childFMElement in fmElement.SubFeatureModelElements) {
                node.Nodes.Add(CreateFeatureModelTreeNode(childFMElement));
            }

            // cross-feature model logic
            Feature feature = fmElement as Feature;
            if (feature != null) {
                if (feature.IsReference && !addedFeatures.Contains(feature.Name)) {
                    FeatureModel definitionFeatureModel = Util.LoadFeatureModel(DTEHelper.GetFullProjectItemPath(feature.DefinitionFeatureModelFile));
                    Feature definitionFeature = definitionFeatureModel.GetFeature(feature.Name);
                    if (definitionFeature != null) {
                        foreach (FeatureModelElement childFMElement in definitionFeature.SubFeatureModelElements) {
                            node.Nodes.Add(CreateFeatureModelTreeNode(childFMElement));
                        }
                    }
                }
                addedFeatures.Add(feature.Name);
            }
        }

        #endregion

        #region Confeaturator end-user configuration
        /// <summary>
        /// Checks/unchecks a Confeaturator node and perform related actions, unless the
        /// click happened as a result collapsing/expanding the node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvFeatures_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (!(e.Node as FeatureModelTreeNode).IsCollapsingOrExpanding) {
                if (e.Button == MouseButtons.Left) {
                    FeatureModelTreeNode node = e.Node as FeatureModelTreeNode;
                    node.Click();
                }
            }
            (e.Node as FeatureModelTreeNode).IsCollapsingOrExpanding = false;
        }

        /// <summary>
        /// Before collapsing a node, the node's IsCollapsingOrExpanding is set to true, so that the node
        /// doesn't get checked/unchecked by accident when the user collapses it using the mouse.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments</param>
        private void trvFeatures_BeforeCollapse(object sender, TreeViewCancelEventArgs e) {
            (e.Node as FeatureModelTreeNode).IsCollapsingOrExpanding = true;
        }

        /// <summary>
        /// Before expanding a node, the node's IsCollapsingOrExpanding is set to true, so that the node
        /// doesn't get checked/unchecked by accident when the user expands it using the mouse.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments</param>
        private void trvFeatures_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            (e.Node as FeatureModelTreeNode).IsCollapsingOrExpanding = true;
        }

        /// <summary>
        /// Enables the user to check/uncheck nodes using the space keyboard key.
        /// </summary>
        /// <param name="sender">Event sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void trvFeatures_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space) {
                if (trvFeatures != null && trvFeatures.SelectedNode != null) {
                    FeatureModelTreeNode selectedNode = trvFeatures.SelectedNode as FeatureModelTreeNode;
                    selectedNode.Click();
                }

            }
        }

        /// <summary>
        /// Automatically expands Alternative nodes when the parent of an alternative node is expanded.
        /// </summary>
        /// <param name="sender">Event sender object.</param>
        /// <param name="e">TreeView event arguments.</param>
        private void trvFeatures_AfterExpand(object sender, TreeViewEventArgs e) {
            foreach (FeatureModelTreeNode childNode in e.Node.Nodes) {
                if (childNode.Kind == FeatureModelNodeKind.Alternative) {
                    childNode.Expand();
                }
            }
        }

        /// <summary>
        /// Event handler that avoids a Confeaturator node icon to be changed after being selected.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void trvFeatures_AfterSelect(object sender, TreeViewEventArgs e) {
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }

        #endregion

        #region Confeaturator Action Providers

        /// <summary>
        /// Inovkes actions from registered Confeaturator Action Providers.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnConfigureEnvironment_Click(object sender, EventArgs e) {
            if (this.ConfeaturatorTreeViewHasNodes) {
                List<string> errors = GetConfigurationErrors();
                if (errors.Count > 0) {
                    MessageBox.Show("You feature model configuration has errors, therefore Confeaturator Actions cannot be launched. Please check the Error List and fix the errors reported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowErrorsInErrorList(errors);
                } else {
                    // launching Confeaturator actions registered through FeatureModelConfigureEnvironment event
                    if (ConfeaturatorActions != null) {
                        try {
                            ConfeaturatorActions(this, new ConfeaturatorActionProviderEventArgs(trvFeatures.Nodes[0] as FeatureModelTreeNode, serviceProvider));
                        } catch (Exception ex) {
                            MessageBox.Show("Error launching Confeaturator actions: " + ex.Message);
                        }
                    }

                    // launching Confeaturator actions registered through IConfeaturatorActionProvider iterface
                    List<ConfeaturatorActionProviderSetting> confeaturatorActionSettings = LoadConfeaturatorActionSettings();
                    if (confeaturatorActionSettings.Count == 0) {
                        MessageBox.Show("No Confeaturator Action Providers are currently specified for this solution. You can edit the Confeaturator Action Providers for this solution by clicking in the \"Add/Remove Confeaturator Action Providers\" button.", "Confeaturator Actions not run", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    foreach (ConfeaturatorActionProviderSetting confeaturatorActionSetting in confeaturatorActionSettings) {
                        try {
                            string projectPath = DTEHelper.GetProjectFolderPath();
                            string assemblyPath = Path.Combine(projectPath, confeaturatorActionSetting.AssemblyName);
                            //AppDomainSetup appDomainSetup = new AppDomainSetup();
                            // setting ShadowCopyFile to true so that we don't lock the assembly
                            //appDomainSetup.ShadowCopyFiles = "true";
                            //appDomainSetup.ApplicationBase = Path.GetDirectoryName(projectPath);
                            //tempDomain = AppDomain.CreateDomain("TempConfeaturatorDomain", null, appDomainSetup);
                            //Assembly confeaturatorActionAssembly = tempDomain.Load(AssemblyName.GetAssemblyName(assemblyPath));
                            Assembly confeaturatorActionAssembly = Assembly.LoadFile(assemblyPath);
                            IConfeaturatorActionProvider confeaturatorAction = confeaturatorActionAssembly.CreateInstance(confeaturatorActionSetting.QualifiedClassName) as IConfeaturatorActionProvider;
                            FeatureModelTreeNode rootNode = trvFeatures.Nodes[0] as FeatureModelTreeNode;
                            confeaturatorAction.PerformConfeaturatorAction(DTEHelper.DTE, GetSelectedFeatures());
                        } catch (Exception ex) {
                            MessageBox.Show("Error launching Confeaturator actions: " + ex.Message + "\r\n\r\nYou can edit the Confeaturator Action Providers for this solution by clicking in the \"Add/Remove Confeaturator Action Providers\" button.", "Confeaturator Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } else {
                Util.ShowError("Confeaturator tree view has no nodes! Please refresh it.");
            }
        }

        /// <summary>
        /// Gets a list of the name of feature that are part of the end-user configuration. This is different
        /// from features which are just checked.
        /// </summary>
        /// <returns>Gets a list of features that are part of the end-user configuration.</returns>
        private List<string> GetSelectedFeatures() {
            List<string> result = new List<string>();
            result.Add(((trvFeatures.Nodes[0] as FeatureModelTreeNode).FeatureModelElement as Feature).Name);
            LogSelectedFeatures(trvFeatures.Nodes, result);
            return result;
        }

        /// <summary>
        /// Recursively accumulates the names of the features that are part of the end-user configuration into a list.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="result"></param>
        private void LogSelectedFeatures(TreeNodeCollection nodes, List<string> result) {
            foreach (FeatureModelTreeNode node in nodes) {
                if (node.IsPartOfConfiguration) {
                    Feature feature = node.FeatureModelElement as Feature;
                    if (feature != null) {
                        result.Add(feature.Name);
                    }
                    LogSelectedFeatures(node.Nodes, result);
                }
            }
        }

        /// <summary>
        /// Loads, from the Confeaturator XML settings file, the Confeaturator settings regarding registered Action Providers.
        /// </summary>
        /// <returns></returns>
        private List<ConfeaturatorActionProviderSetting> LoadConfeaturatorActionSettings() {
            List<ConfeaturatorActionProviderSetting> result = new List<ConfeaturatorActionProviderSetting>();
            foreach (ProjectItem projectItem in DTEHelper.Project.ProjectItems) {
                if (projectItem.Name == confeaturatorSettingsFile) {
                    try {
                        string confeaturatorSettingsFileFullPath = Path.Combine(Path.GetDirectoryName(DTEHelper.Project.FullName), confeaturatorSettingsFile);
                        if (File.Exists(confeaturatorSettingsFileFullPath)) {
                            using (FileStream fs = new FileStream(confeaturatorSettingsFileFullPath, FileMode.Open)) {
                                result = settingsXmlSerializer.Deserialize(fs) as List<ConfeaturatorActionProviderSetting>;
                            }
                        }
                    } catch (Exception ex) {
                        // show error in status bar, confeaturator actions will be initialized to empty list.
                        DTEHelper.DTE.StatusBar.Text = "Error reading confeaturator settings file: " + ex.Message;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Launches a form enabling the user to add and remove Confeaturator Action Providers.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAddRemoveConfeaturatorActionProviders_Click(object sender, EventArgs e) {
            FrmAddRemoveConfeaturatorActionProviders frmAddRemove = new FrmAddRemoveConfeaturatorActionProviders(LoadConfeaturatorActionSettings());
            if (frmAddRemove.ShowDialog() == DialogResult.OK) {
                SaveConfeaturatorActionProviderSettings(frmAddRemove.ConfeaturatorActionProviderSettings);
            }
        }

        /// <summary>
        /// Saves, into the Confeaturator XML settings file, the Confeaturator settings regarding registered Action Providers.
        /// </summary>
        /// <param name="confeaturatorActionProviderSettings">List of Confeaturator Action Provider Settings to be saved.</param>
        private void SaveConfeaturatorActionProviderSettings(List<ConfeaturatorActionProviderSetting> confeaturatorActionProviderSettings) {
            string tempFilePath = Path.GetTempFileName();
            FileStream fs = new FileStream(tempFilePath, FileMode.Create);
            settingsXmlSerializer.Serialize(fs, confeaturatorActionProviderSettings);
            fs.Close();
            StreamReader sr = new StreamReader(tempFilePath);
            string serializedContents = sr.ReadToEnd();
            sr.Close();
            DTEHelper.AddTextToProjectAsFile(confeaturatorSettingsFile, serializedContents);
        }

        #endregion

        #region Confeatureator validation

        /// <summary>
        /// Checks and logs, in the Visual Studio Error List, a list of errors present in the current
        /// configuration.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void btnValidateConfiguration_Click(object sender, EventArgs e) {
            List<string> errors = GetConfigurationErrors();
            if (errors.Count == 0) {
                MessageBox.Show("No Confeaturator errors found", "Validation completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else {
                ShowErrorsInErrorList(errors);
            }
        }

        /// <summary>
        /// Logs a list of error messages in the Visual Studio Error List.
        /// </summary>
        /// <param name="errors">List of errors to be logged into the Error List.</param>
        private void ShowErrorsInErrorList(List<string> errorMessages) {
            foreach (string error in errorMessages) {
                DTEHelper.AddErrorToErrorListFromOutputPane(error);
            }
        }

        /// <summary>
        /// Gets a list of errors present in the current configuration.
        /// </summary>
        /// <returns>A list of errors present in the current configuration.</returns>
        private List<string> GetConfigurationErrors() {
            List<string> errors = new List<string>();
            DTEHelper.ClearErrorsFromOutputPane();
            if (this.trvFeatures.Nodes == null || this.trvFeatures.Nodes.Count == 0) {
                errors.Add("No features available. Ensure you feature model diagram(s) has features and Confeaturator is refreshed.");
            } else {
                FeatureModelTreeNode rootNode = this.trvFeatures.Nodes[0] as FeatureModelTreeNode;
                Feature feature = rootNode.FeatureModelElement as Feature;
                if (feature == null) {
                    errors.Add("Root node should be a feature. Please re-check you feature model diagrams.");
                } else {
                    if (rootNode.Status == FeatureModelNodeStatus.Unchecked) {
                        string errorMessage = string.Format("Root Confeaturator node '{0}' should be checked.", feature.Name);
                        errors.Add(errorMessage);
                    }
                    rootNode.LogAlternativeIntervalErrors(errors);
                }
            }
            return errors;
        }

        #endregion

        #region Confeaturator saving and loading

        /// <summary>
        /// Event handler to save the current user configuration.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnSaveConfiguration_Click(object sender, EventArgs e) {
            if (this.ConfeaturatorTreeViewHasNodes) {
                try {
                    if (ProceedEvenWithErrors()) {
                        if (saveConfigurationDialog.ShowDialog() == DialogResult.OK) {
                            SaveConfiguration(saveConfigurationDialog.FileName);
                            Util.ShowSuccess("Configuration successfully saved to " + saveConfigurationDialog.FileName + ". You may want to add the file to the project case that was not done already.");
                        }
                    }

                } catch (Exception ex) {
                    Util.ShowError("Error saving configuration: " + ex.Message);
                }
            } else {
                Util.ShowError("Confeaturator tree view has no nodes! Please refresh it.");
            }
        }

        /// <summary>
        /// Checks wheter the user wants to proceed even if the current coniguration has errors.
        /// </summary>
        /// <returns>Wheter the user wants to proceed even if the current coniguration has errors.</returns>
        private bool ProceedEvenWithErrors() {
            bool proceedWithErrors = true;
            List<string> errorMessages = this.GetConfigurationErrors();
            if (errorMessages.Count > 0
                && MessageBox.Show("The current configuration has validation errors. Do you still want to proceed?", "Configuration not valid", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.No) {
                proceedWithErrors = false;
                ShowErrorsInErrorList(errorMessages);
            }
            return proceedWithErrors;
        }

        /// <summary>
        /// Saves the current Confeaturator feature model configuration.
        /// </summary>
        /// <param name="outputFileName">The target file name where to save the configuration to.</param>
        private void SaveConfiguration(string outputFileName) {
            List<string> checkedNodePaths = new List<string>();
            LogCheckedNodePaths(trvFeatures.Nodes[0] as FeatureModelTreeNode, checkedNodePaths);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            FileStream fs = new FileStream(outputFileName, FileMode.Create);
            xmlSerializer.Serialize(fs, checkedNodePaths);
            fs.Close();
        }

        /// <summary>
        /// Recursively logs the checked Confeaturator node paths into a list of paths.
        /// </summary>
        /// <param name="node">The node where to start the logging recursion.</param>
        /// <param name="checkedNodePaths">The cumulative list of strings where to log the checked node paths.</param>
        private void LogCheckedNodePaths(FeatureModelTreeNode node, List<string> checkedNodePaths) {
            if (node.IsChecked) {
                checkedNodePaths.Add(HttpUtility.HtmlEncode(node.FullPath));
            }
            foreach (FeatureModelTreeNode childNode in node.Nodes) {
                LogCheckedNodePaths(childNode, checkedNodePaths);
            }
        }

        /// <summary>
        /// Event handler to load a configuration into Confeaturator.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnOpenConfiguration_Click(object sender, EventArgs e) {
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Load Feature Model configuration";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                RefreshConfeaturatorTree();
                if (this.ConfeaturatorTreeViewHasNodes) {
                    List<string> pathWithErrors = LoadConfiguration(openFileDialog.FileName);
                    if (pathWithErrors.Count > 0) {
                        string warningMessage = "It seems your feature model is out of sync with the specified configuration file. Please re-check your configuration and save it to ensure everything is in sync.";
                        warningMessage += "\r\n\r\nConfeaturator did its best to load the configuration but problems were found when loading the following feature(s): ";
                        foreach (string pathWithError in pathWithErrors) {
                            warningMessage += ("\r\n- " + pathWithError);
                        }
                        Util.ShowWarning(warningMessage);
                    } else {
                        Util.ShowSuccess("Configuration loaded successfully");
                    }

                } else {
                    Util.ShowError("Confeaturator tree view has no nodes! Please refresh it.");
                }
            }
        }

        /// <summary>
        /// Loads a configuration into Confeaturator. Configuration validation errors do not pop-up
        /// in the UI, but a list of them are returned.
        /// </summary>
        /// <param name="fileName">The .confeaturator file path containing the configuration to be loaded.</param>
        /// <returns>Any path with errors that were found when loading the configuration.</returns>
        private List<string> LoadConfiguration(string fileName) {
            List<string> pathWithErrors = new List<string>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            FileStream fs = new FileStream(fileName, FileMode.Open);
            List<string> checkedNodePaths = xmlSerializer.Deserialize(fs) as List<string>;
            fs.Close();
            foreach (string checkedNodePath in checkedNodePaths) {
                string path = HttpUtility.HtmlDecode(checkedNodePath);
                FeatureModelTreeNode node = GetNodeFromFullPath(trvFeatures.Nodes, path);
                if (node != null) {
                    if (node.Kind == FeatureModelNodeKind.Optional || node.Kind == FeatureModelNodeKind.NotApply) {
                        node.Check();
                    }
                } else {
                    pathWithErrors.Add(path);
                }
            }
            return pathWithErrors;
        }
        
        // TODO: the code below can be optimized! I'm using a dumb/slow approach here checking every node. 

        /// <summary>
        /// Traverses a tree looking for the node containing a given full path.
        /// </summary>
        /// <param name="nodes">The collection of nodes.</param>
        /// <param name="path">Path to be searched for.</param>
        /// <returns></returns>
        private FeatureModelTreeNode GetNodeFromFullPath(TreeNodeCollection nodes, string path) {
            FeatureModelTreeNode result = null;
            foreach (FeatureModelTreeNode node in nodes) {
                if (node.FullPath == path) {
                    result = node;
                    break;
                }
            }
            if (result == null) {
                foreach (FeatureModelTreeNode node in nodes) {
                    result = GetNodeFromFullPath(node.Nodes, path);
                    if (result != null) {
                        break;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Confeaturator reports

        /// <summary>
        /// Event handler to generate simple report.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnGenerateReport_Click(object sender, EventArgs e) {
            try {
                if (ConfeaturatorTreeViewHasNodes) {
                    if (ProceedEvenWithErrors()) {
                        if (saveReportDialog.ShowDialog() == DialogResult.OK) {
                            Cursor = Cursors.WaitCursor;
                            string tempFilePath = Path.Combine(Path.GetTempPath(), saveReportDialog.FileName);
                            SaveConfiguration(tempFilePath);
                            GenerateReport(saveReportDialog.FileName, tempFilePath);
                        }
                    }
                } else {
                    Util.ShowError("Confeatureator is empty. Please refresh it.");
                }
            } catch (Exception ex) {
                Cursor = Cursors.Arrow;
                Util.ShowError("Error generating Confeaturator report: " + ex.Message);
            } finally {
                Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Event handler to generate the consolidated report (from multiple .confeaturator files)
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnGenerateConsolidatedReport_Click(object sender, EventArgs e) {
            try {
                if (ConfeaturatorTreeViewHasNodes) {
                    openFileDialog.Multiselect = true;
                    openFileDialog.Title = "Load one or more Feature Model configurations";
                    if (openFileDialog.ShowDialog() == DialogResult.OK) {
                        if (saveReportDialog.ShowDialog() == DialogResult.OK) {
                            Cursor = Cursors.WaitCursor;
                            string tempConfigFile = Path.GetTempFileName() + ".confeaturator";
                            this.SaveConfiguration(tempConfigFile);
                            GenerateReport(saveReportDialog.FileName, openFileDialog.FileNames);
                            this.LoadConfiguration(tempConfigFile);
                        }
                    }
                } else {
                    Util.ShowError("Confeatureator is empty. Please refresh it.");
                }
            } catch (Exception ex) {
                Cursor = Cursors.Arrow;
                Util.ShowError("Error generating Confeaturator report: " + ex.Message);
            } finally {
                Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Generates a configuration report from a set of input configurations.
        /// </summary>
        /// <param name="ouputFileName">The HTML file where to generate the report.</param>
        /// <param name="inputFileNames">Input configuration (.confeaturator) files.</param>
        private void GenerateReport(string ouputFileName, params string[] inputFileNames) {
            List<Configuration> configurations = new List<Configuration>();

            bool generatedWithWarnings = false;
            foreach (string fileName in inputFileNames) {
                if (LoadConfiguration(fileName).Count > 0) {
                    generatedWithWarnings = true;
                }
                Dictionary<string, bool> configurationFeatures = new Dictionary<string, bool>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
                FileStream fs = new FileStream(fileName, FileMode.Open);
                List<string> checkedNodePaths = xmlSerializer.Deserialize(fs) as List<string>;
                fs.Close();
                foreach (string checkedNodePath in checkedNodePaths) {
                    FeatureModelTreeNode node = GetNodeFromFullPath(trvFeatures.Nodes, checkedNodePath);
                    if (node != null && node.IsPartOfConfiguration) {
                        string[] featurePathItems = checkedNodePath.Split(new[] { trvFeatures.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
                        string featureName = featurePathItems[featurePathItems.Length - 1];
                        configurationFeatures[featureName] = true;
                    }
                }
                string configurationName = Path.GetFileNameWithoutExtension(fileName);
                configurations.Add(new Configuration { ProductName = configurationName, Features = configurationFeatures });
            }
            ReportGenerator.GenerateReport(ouputFileName, trvFeatures.Nodes[0] as FeatureModelTreeNode, configurations);
            System.Diagnostics.Process.Start(ouputFileName);
            if (generatedWithWarnings) {
                Util.ShowWarning("Report generated, but one or more configurations seem to be out of sync with the feature model. Please re-check your configurations and save them to ensure everything is in sync.");
            }
        }

        #endregion

    }
}

