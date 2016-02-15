#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"")]
[assembly: AssemblyDescription(@"")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(@"UFPE")]
[assembly: AssemblyProduct(@"FeatureModelDSL")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: System.Resources.NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"UFPE.FeatureModelDSL.DslPackage, PublicKey=00240000048000009400000006020000002400005253413100040000010001008FF4A4C8D6DFCF5A1A4611C651376D137C20DFD4C34F65E06C6C13D1CBD325A6236DCB48073662FFA278DE96BBEDEA19C43580F6405BC4DC088C7C41EB730ED2A6CE2FDD2DCCE85202BA7434C2766DCE8D88ECD18C5979EBA92CE08D8612B929449D7DB80A56FBD52BD1BE63F1829742C274AC168964515F005BD7AA34A03BD8")]