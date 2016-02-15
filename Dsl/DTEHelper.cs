using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.Modeling;
using System.IO;
using EnvDTE80;

namespace UFPE.FeatureModelDSL
{

    /// <summary>
    /// Helper functions for interacting with the DTE
    /// </summary>
    public static class DTEHelper
    {

        private static Project project = null;

        /// <summary>
        /// Development Tools Extensibility object
        /// </summary>
        [CLSCompliant(false)]
        public static DTE DTE { get; set; }

        /// <summary>
        /// Current active solution project
        /// </summary>
        [CLSCompliant(false)]
        public static Project Project
        {
            get
            {
                if (DTEHelper.project == null)
                {
                    DTEHelper.project = ((object[])DTE.ActiveSolutionProjects)[0] as Project;
                }

                return DTEHelper.project;
            }
        }

        /// <summary>
        /// Visual Studio Output pane
        /// </summary>
        private static OutputWindowPane outputPane = null;

        /// <summary>
        /// Singleton implementation to get the instance of Visual Studio Output pane
        /// </summary>
        private static OutputWindowPane OutputWindowPane
        {
            get
            {
                if (outputPane == null)
                {
                    OutputWindow outputWindow = DTEHelper.DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Object as OutputWindow;

                    foreach (OutputWindowPane pane in outputWindow.OutputWindowPanes)
                    {
                        if (pane.Guid.Equals(BuildOutputPaneGuid))
                        {
                            outputPane = pane;
                            break;
                        }
                    }
                }
                return outputPane;
            }
        }

        /// <summary>
        /// GUID used for the Visual Studio Build output pane
        /// </summary>
        const string BuildOutputPaneGuid = "{1BD8A850-02D1-11D1-BEE7-00A0C913D1F8}";

        /// <summary>
        /// Initializes the DTEHelper
        /// </summary>
        /// <param name="serviceProvider">A service provider interface from where the DTE
        /// service can be obtained.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            DTE = serviceProvider.GetService(typeof(DTE)) as DTE;
        }

        /// <summary>
        /// Gets the full path of a project item.
        /// </summary>
        /// <param name="projectItemName">Project item name</param>
        public static string GetProjectItemPath(string projectItemName)
        {
            string result = string.Empty;
            foreach (ProjectItem item in Project.ProjectItems)
            {
                if (item.Name.Equals(projectItemName))
                {
                    result = item.get_FileNames(0);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Opens a file in Visual Studio
        /// </summary>
        /// <param name="filePath">The file path</param>
        public static void OpenFile(string filePath)
        {
            DTE.ItemOperations.OpenFile(filePath, EnvDTE.Constants.vsViewKindAny);
        }

        /// <summary>
        /// Gets all item names of the current project.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllProjectItemNames()
        {
            List<string> result = new List<string>();
            foreach (ProjectItem item in Project.ProjectItems)
            {
                result.Add(item.Name);
            }
            return result;
        }

        /// <summary>
        /// Gets the folder path of the active project
        /// </summary>
        /// <returns></returns>
        public static string GetProjectFolderPath()
        {
            return Path.GetDirectoryName(Project.FullName);
        }


        /// <summary>
        /// Adds a new text file to the project based on a given text
        /// </summary>
        /// <param name="targetFileName"></param>
        /// <param name="text"></param>
        public static void AddTextToProjectAsFile(string targetFileName, string text)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), targetFileName);
            StreamWriter sr = new StreamWriter(tempFilePath);
            sr.Write(text);
            sr.Close();
            ProjectItem projectItemToBeDeleted = null;
            foreach (ProjectItem projectItem in Project.ProjectItems)
            {
                if (projectItem.Name == targetFileName)
                {
                    projectItemToBeDeleted = projectItem;
                }
            }

            if (projectItemToBeDeleted != null)
            {
                // if the file is already in the project, delete it
                projectItemToBeDeleted.Delete();
            }
            else
            {
                // if the file is not added to the project but exists anyway, delete it.
                string fileName = Path.Combine(Path.GetDirectoryName(Project.FullName), targetFileName);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            Project.ProjectItems.AddFromFileCopy(tempFilePath);
        }

        /// <summary>
        /// Adds an error to the Visual Studio Error List, through the OutputWindowPane
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        public static void AddErrorToErrorListFromOutputPane(string errorMessage)
        {
            OutputWindowPane.OutputTaskItemString(errorMessage, vsTaskPriority.vsTaskPriorityHigh, "", vsTaskIcon.vsTaskIconCompile, "Confeaturator", 0, errorMessage, true);
            DTE.Windows.Item(EnvDTE80.WindowKinds.vsWindowKindErrorList).Activate();

        }

        /// <summary>
        /// Clears the Output pane contents, therefore clearing any errors in the Error List
        /// raised from the Output pane.
        /// </summary>
        public static void ClearErrorsFromOutputPane()
        {
            OutputWindowPane.Clear();
        }

        /// <summary>
        /// Gets the full (absolute) file path of a project item.
        /// </summary>
        /// <param name="projectItemFileName">The project item file name.</param>
        /// <returns></returns>
        public static string GetFullProjectItemPath(string projectItemFileName)
        {
            return Path.Combine(Path.GetDirectoryName(Project.FullName), projectItemFileName);
        }
    }
}
