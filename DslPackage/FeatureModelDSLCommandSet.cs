using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling.Shell;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Drawing;
using UFPE.FeatureModelDSL.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace UFPE.FeatureModelDSL {
    /// <summary>
    /// Hosts diagram commands
    /// </summary>
    internal partial class FeatureModelDSLCommandSet {
        public Guid cmdGoToParentFeatureModelGuid = new Guid("{52F16959-782B-4bc8-BF1A-8A542A1EC24D}");
        public const int cmdGoToParentFeatureModelID = 0x810;

        public Guid cmdExportDiagramGuid = new Guid("{C87CEDC7-9FFA-4226-A7C1-EC140A5ABF05}");
        public const int cmdExportDiagramID = 0x811;

        public Guid cmdGoToDefinitionFeatureModelGuid = new Guid("{14606710-3AFD-4341-9497-A4D6EE65DBA1}");
        public const int cmdGoToDefinitionFeatureModelID = 0x812;

        public Guid cmdViewConfeaturatorGuid = new Guid("{A2EA2C46-6052-4e17-8F9D-8A64390D6869}");
        public const int cmdViewConfeaturatorID = 0x813;

        /// <summary>
        /// Adds commands to this diagram and returns them.
        /// </summary>
        /// <returns></returns>
        protected override IList<MenuCommand> GetMenuCommands() {
            IList<MenuCommand> commands = base.GetMenuCommands();
            DynamicStatusMenuCommand cmdGoToParentFeatureModel =
                new DynamicStatusMenuCommand(
                    new EventHandler(OnGoToParentFeatureModelDisplayAction),
                    new EventHandler(OnGoToParentFeatureModelClick),
                    new CommandID(cmdGoToParentFeatureModelGuid, cmdGoToParentFeatureModelID));
            commands.Add(cmdGoToParentFeatureModel);

            DynamicStatusMenuCommand cmdGoToDefinitionFeatureModel =
               new DynamicStatusMenuCommand(
                   new EventHandler(OnGoToDefinitionFeatureModelDisplayAction),
                   new EventHandler(OnGoToDefinitionFeatureModelClick),
                   new CommandID(cmdGoToDefinitionFeatureModelGuid, cmdGoToDefinitionFeatureModelID));
            commands.Add(cmdGoToDefinitionFeatureModel);

            DynamicStatusMenuCommand cmdExportDiagram =
                new DynamicStatusMenuCommand(
                    new EventHandler(OnExportDiagramDisplayAction),
                    new EventHandler(OnExportDiagramClick),
                    new CommandID(cmdExportDiagramGuid, cmdExportDiagramID));
            commands.Add(cmdExportDiagram);

            DynamicStatusMenuCommand cmdViewConfeaturator =
                new DynamicStatusMenuCommand(
                    new EventHandler(OnViewConfeaturatorDisplayAction),
                    new EventHandler(OnViewConfeaturatorClick),
                    new CommandID(cmdViewConfeaturatorGuid, cmdViewConfeaturatorID));
            commands.Add(cmdViewConfeaturator);

            return commands;
        }

        /// <summary>
        /// Determines if the menu command to navigate to the parent feature model
        /// will be visible. This happens only if this feature model contains
        /// a reference for a parent feature model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnGoToParentFeatureModelDisplayAction(object sender, EventArgs e) {
            MenuCommand command = sender as MenuCommand;
            command.Visible = false;
            command.Enabled = false; 
            foreach (object selectedObject in this.CurrentSelection)   {
                if (selectedObject is FeatureModelDSLDiagram) {
                    FeatureModel featureModel = (selectedObject as FeatureModelDSLDiagram).ModelElement as FeatureModel;
                    if (!string.IsNullOrEmpty(featureModel.ParentFeatureModelFile)) {
                        command.Visible = true;
                    command.Enabled = true;
                    }
                    break; 
                }
            }
        }

        /// <summary>
        /// Navigates to the parent feature model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnGoToParentFeatureModelClick(object sender, EventArgs e) {
            MenuCommand command = sender as MenuCommand; 
            foreach (object selectedObject in this.CurrentSelection)  {
                if (selectedObject is FeatureModelDSLDiagram) {
                    FeatureModel featureModel = (selectedObject as FeatureModelDSLDiagram).ModelElement as FeatureModel;
                    string parentFeatureModelFilePath = DTEHelper.GetProjectItemPath(featureModel.ParentFeatureModelFile);
                    DTEHelper.OpenFile(parentFeatureModelFilePath);
                    break;
                }
            }
        }

        /// <summary>
        /// Enables/show "Go to definition feature model" menu option only if
        /// a reference feature is part of the current selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnGoToDefinitionFeatureModelDisplayAction(object sender, EventArgs e) {
            MenuCommand command = sender as MenuCommand;
            command.Visible = false;
            command.Enabled = false;
            if (this.CurrentSelection.Count == 1) {
                foreach (object selectionObject in this.CurrentSelection) {
                    if (selectionObject is FeatureShape) {
                        Feature feature = (Feature) (selectionObject as FeatureShape).ModelElement;
                        if (feature.IsReference && !string.IsNullOrEmpty(feature.DefinitionFeatureModelFile)) {
                            command.Visible = true;
                            command.Enabled = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Navigates to the definition feature model of a reference feature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnGoToDefinitionFeatureModelClick(object sender, EventArgs e) {
            if (this.CurrentSelection.Count == 1) {
                foreach (object selectionObject in this.CurrentSelection) {
                    if (selectionObject is FeatureShape) {
                        Feature feature = (Feature)(selectionObject as FeatureShape).ModelElement;
                        if (feature.IsReference && !string.IsNullOrEmpty(feature.DefinitionFeatureModelFile)) {
                            string definitionFeatureModelFilePath = DTEHelper.GetProjectItemPath(feature.DefinitionFeatureModelFile);
                            DTEHelper.OpenFile(definitionFeatureModelFilePath);

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Enables/shows "Export diagram" menu option only if the diagram is part of the current selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnExportDiagramDisplayAction(object sender, EventArgs e) {
            MenuCommand command = sender as MenuCommand;
            command.Visible = false;
            command.Enabled = false;
            foreach (object selectedObject in this.CurrentSelection) {
                if (selectedObject is FeatureModelDSLDiagram) {
                        command.Visible = true;
                        command.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Export the feature model diagram
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnExportDiagramClick(object sender, EventArgs e) {
            foreach (object selectedObject in this.CurrentSelection) {
                if (selectedObject is FeatureModelDSLDiagram) {
                    FeatureModelDSLDiagram diagram = (selectedObject as FeatureModelDSLDiagram);
                    string featureModelName = (diagram.ModelElement as FeatureModel).Name + ".png";
                    FrmTextInputDialog frmTextInputDialog = new FrmTextInputDialog("Export Diagram", "Export to (complete file path):", featureModelName);
                    if (frmTextInputDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                        Bitmap picture = diagram.CreateBitmap(diagram.NestedChildShapes, Diagram.CreateBitmapPreference.FavorClarityOverSmallSize);
                        string saveLocation = frmTextInputDialog.InputText;
                        if (!saveLocation.EndsWith(".png")) {
                            saveLocation += ".png";
                        }
                        if (File.Exists(saveLocation)) {
                            if (MessageBox.Show(saveLocation + " already exists.\r\nDo you want to replace it?", "Confirm Export Diagram location", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                                == DialogResult.Yes) {
                                SaveDiagramBitmapToFile(picture, saveLocation);
                            }
                        } else {
                            SaveDiagramBitmapToFile(picture, saveLocation);
                        }
                    }
                    
                    break;
                }
            }
        }

        
        /// <summary>
        /// Saves a bitmap containing the exported diagram to a .png file.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="saveLocation">Complete path where to save the file</param>
        private static void SaveDiagramBitmapToFile(Bitmap picture, string saveLocation) {
            picture.Save(saveLocation, ImageFormat.Png);
            MessageBox.Show("Diagram exported successfully to " + saveLocation, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Enables/show "View Confeaturator" menu option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnViewConfeaturatorDisplayAction(object sender, EventArgs e) {
            MenuCommand command = sender as MenuCommand;
            command.Visible = true;
            command.Enabled = true;
        }

        /// <summary>
        /// Shows the Confeaturator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnViewConfeaturatorClick(object sender, EventArgs e) {
            try {
                DTEHelper.DTE.Windows.Item("Confeaturator").Activate();
            } catch (Exception ex) {
                Util.ShowError("It was not possible to display the Confeaturator: " + ex.Message + "\r\nYou can delete the following file, restart VS and try again: C:\\Documents and Settings\\<username>\\Application Data\\Microsoft\\VisualStudio\\9.0\\Windows.prf");
            }
        }
    }
}
