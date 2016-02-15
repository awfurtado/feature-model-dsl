using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFPE.FeatureModelDSL.Forms;
using System.IO;

namespace UFPE.FeatureModelDSL.Confeaturator {

    /// <summary>
    /// Class responsible for generating Confeaturator reports
    /// </summary>
    internal static class ReportGenerator {
        /// <summary>
        /// A blank space in HTML.
        /// </summary>
        const string blank = "&nbsp;";

        /// <summary>
        /// The HTML background color of selected feature cells
        /// </summary>
        const string selectedBackgroundColor = "lightblue";
        
        /// <summary>
        /// Total number of mandatory features identified in the report, plus the root feature.
        /// </summary>
        private static int totalRootMandatory;
        
        /// <summary>
        /// Total number of selected root/mandatory features.
        /// </summary>
        private static int totalRootMandatorySelected;

        /// <summary>
        /// Total number of optional features plus features that are alternative options.
        /// </summary>
        private static int totalOptionalAlternative;

        /// <summary>
        /// Total number of selected optional/alternative option features.
        /// </summary>
        private static int totalOptionalAlternativeSelected;
        
        /// <summary>
        /// Generates a configuration report from a one or more configurations. If the number of configurations
        /// is more than one, an additional "Total" column gets generated in the detailed report.
        /// </summary>
        /// <param name="outputFileName">The output HTML file name.</param>
        /// <param name="root">The root Confeaturator node.</param>
        /// <param name="configurations">The set of configurations from which the report will be generated.</param>
        internal static void GenerateReport(string outputFileName, FeatureModelTreeNode root, List<Configuration> configurations) {
            InitializeCounters();
            string configurationName = Path.GetFileNameWithoutExtension(outputFileName);
            StringBuilder htmlStart = new StringBuilder();
            StringBuilder htmlTable = new StringBuilder();
            AppendStart(htmlStart, configurations);
            AppendTable(htmlTable, root, configurations);
            AppendEnd(htmlTable);
            AppendSummary(htmlStart);
            
            htmlStart.Append(htmlTable.ToString());

            StreamWriter sr = new StreamWriter(outputFileName);
            sr.Write(htmlStart.ToString());
            sr.Close();
        }

        /// <summary>
        /// Initialize report counters.
        /// </summary>
        private static void InitializeCounters() {
            totalRootMandatory = 0;
            totalRootMandatorySelected = 0;
            totalOptionalAlternative = 0;
            totalOptionalAlternativeSelected = 0;
        }

        /// <summary>
        /// Appends the summary section of the report to the specified StringBuilder
        /// </summary>
        /// <param name="htmlBuilder">The string builder.</param>
        private static void AppendSummary(StringBuilder htmlBuilder) {

            int totalFeatures = totalRootMandatory + totalOptionalAlternative;
            int totalSelected = totalRootMandatorySelected + totalOptionalAlternativeSelected;
            int totalRootMandatoryUnselected = totalRootMandatory - totalRootMandatorySelected;
            int totalOptionalAlternativeUnselected = totalOptionalAlternative - totalOptionalAlternativeSelected;
            int totalUnselected = totalRootMandatoryUnselected + totalOptionalAlternativeUnselected;


            htmlBuilder.Append("<h2>Summary</h2>");
            htmlBuilder.Append("<p><table border='1' cellpadding='5' style='text-align:center'>");
            htmlBuilder.Append("<tr style='background-color:wheat'><th style='background-color:white'/><th>Selected</th><th>Unselected</th><th>Total</th></tr>");
            htmlBuilder.Append("<tr><th style='background-color:wheat;text-align:left'>Root / Mandatory</th><td>" + totalRootMandatorySelected + "</td><td>" + totalRootMandatoryUnselected + "</td><td  style='background-color:lightyellow'>" + totalRootMandatory + "</td>");
            htmlBuilder.Append("<tr><th style='background-color:wheat;text-align:left'>Optional / alternative options</th><td>" + totalOptionalAlternativeSelected + "</td><td>" + totalOptionalAlternativeUnselected + "</td><td  style='background-color:lightyellow'>" + totalOptionalAlternative + "</td>");
            htmlBuilder.Append("<tr style='background-color:lightyellow'><th style='background-color:wheat;text-align:left'>Total</th><td>" + totalSelected + "</td><td>" + totalUnselected + "</td><td><b>" + totalFeatures + "</b></td>");
            htmlBuilder.Append("</table></p>");
        }

        

        /// <summary>
        /// Appends the start of the report to the specified StringBuilder.
        /// </summary>
        /// <param name="htmlBuilder">The HTML string builder.</param>
        /// <param name="configurations">List of configurations.</param>
        private static void AppendStart(StringBuilder htmlBuilder, List<Configuration> configurations) {
            string configurationList = configurations[0].ProductName;
            for (int i = 1; i < configurations.Count; i++) {
                configurationList += ", " + configurations[i].ProductName;
            }
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head><title>Configuration Report for " + configurationList + "</title></head>");
            htmlBuilder.Append("<body style='font-family: calibri, candara, arial'>");
            htmlBuilder.Append("<b style='font-size: x-large'> Configuration Report: " + configurationList + " </b>");
            htmlBuilder.Append("<hr/>");
            //htmlBuilder.Append("<br/><i style='font-size: x-small; color: gray'>" + configurationList + "</i>");           
        }

        /// <summary>
        /// Appends the Details table to the report to the specified StringBuilder.
        /// </summary>
        /// <param name="htmlBuilder">The HTML string builder</param>
        /// <param name="root">The root Confeaturator node.</param>
        /// <param name="configurations">List of configurations.</param>
        internal static void AppendTable(StringBuilder htmlBuilder, FeatureModelTreeNode root, List<Configuration> configurations) {
            htmlBuilder.Append("<h2>Details</h2>");
            htmlBuilder.Append("<table border='1' cellpadding='5'>"); htmlBuilder.Append("<tr style='background-color:wheat'><th>Features</th>");
            foreach (Configuration configuration in configurations) {
                htmlBuilder.Append("<th>" + configuration.ProductName + "</th>");
            }
            
            // no Total column if this report is being generated for only one configuration.
            if (configurations.Count > 1) {
                htmlBuilder.Append("<th>Total</th></tr>");
            }
            AppendRow(htmlBuilder, root, configurations, 0);
        }

        /// <summary>
        /// Appens a row to the Details table report to the specified StringBuilder.
        /// </summary>
        /// <param name="htmlFile">The HTML string builder.</param>
        /// <param name="node">The Confeaturator node from which the report row will be generated.</param>
        /// <param name="configurations">List of configurations.</param>
        /// <param name="identLevel">Identation level of the row.</param>
        private static void AppendRow(StringBuilder htmlFile, FeatureModelTreeNode node, List<Configuration> configurations, int identLevel) {
            StringBuilder blankSpaces = new StringBuilder();
            for (int i = 0; i < 5*identLevel; i++) {
                blankSpaces.Append(blank);
            }

            string nodeText = node.Text;
            Feature feature = node.FeatureModelElement as Feature;
            bool isFeatureNode = false;
            if (feature != null) {
                isFeatureNode = true;
            }
            if (node.Kind == FeatureModelNodeKind.NotApply || node.Kind == FeatureModelNodeKind.Optional) {
                nodeText = "<i>" + nodeText + "</i>";
            }
            if (node.FeatureModelElement is Feature) {
                htmlFile.Append("<tr><td style='background-color:lightyellow'>" + blankSpaces + nodeText + "</td>");
            } else {
                int colsToSpan = configurations.Count + 1; // Total # of products + feature column
                if (configurations.Count > 1) {
                    colsToSpan++; // Adding "total" column
                }
                htmlFile.Append("<tr><td style='color:dimgray;background-color:lightyellow' colspan='"+colsToSpan+"'>" + blankSpaces + node.Text + "</td>");
            }

            int timesSelected = 0;
            
            foreach (Configuration configuration in configurations) {
                if (isFeatureNode) {
                    if (configuration.Features.Keys.Contains(feature.Name) && configuration.Features[feature.Name]) {
                        htmlFile.Append("<td style='text-align:center;background-color:" + selectedBackgroundColor + "'>&bull;</td>");
                        timesSelected++;
                    } else {
                        htmlFile.Append("<td>&nbsp;</td>");
                    }
                }
            }

            // "Total" column and counters
            if (isFeatureNode) {
                if (node.Kind == FeatureModelNodeKind.Root || node.Kind == FeatureModelNodeKind.Mandatory) {
                    totalRootMandatory++;
                } else if (node.Kind == FeatureModelNodeKind.NotApply || node.Kind == FeatureModelNodeKind.Optional) {
                    totalOptionalAlternative++;
                }

                if (timesSelected > 0) {

                    if (node.Kind == FeatureModelNodeKind.Root || node.Kind == FeatureModelNodeKind.Mandatory) {
                        totalRootMandatorySelected++;
                    } else if (node.Kind == FeatureModelNodeKind.NotApply || node.Kind == FeatureModelNodeKind.Optional) {
                        totalOptionalAlternativeSelected++;
                    }

                    // "Total" columns
                    if (configurations.Count > 1) {
                        htmlFile.Append("<td style='text-align:center;background-color:" + selectedBackgroundColor + "'>" + timesSelected + "</td>");
                    }

                } else {

                    // "Total" columns
                    if (configurations.Count > 1) {
                        htmlFile.Append("<td style='text-align:center'>0</td>");
                    }
                }
            }

            htmlFile.Append("</tr>");

            foreach (FeatureModelTreeNode childNode in node.Nodes) {
                AppendRow(htmlFile, childNode, configurations, identLevel + 1);
            }
        }

        /// <summary>
        /// Appends the end of this report to the specified StringBuilder.
        /// </summary>
        /// <param name="htmlBuilder">The HTML string builder.</param>
        private static void AppendEnd(StringBuilder htmlBuilder) {
            htmlBuilder.Append("</table>");
            htmlBuilder.Append("<br/><hr/>");
            htmlBuilder.Append("<div style='font-size: x-small'>");
            htmlBuilder.Append("Generated by <b><a href='http://www.codeplex.com/FeatureModelDSL'>Feature Model DSL</a></b> on " + string.Format("{0:MMM dd, yyyy}",DateTime.Now));
            htmlBuilder.Append("<br/><i>Implementation and extended design by: <a href='http://www.afurtado.net'>Andre Furtado</a></i>");
            htmlBuilder.Append("<br/><i>Original design by: <a href='http://www.ispysoft.net'>Gunther Lenz and Christoph Wienands</a></i>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");
        }
    }
}
