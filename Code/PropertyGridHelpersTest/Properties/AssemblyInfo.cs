using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("David Parvin")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCopyright("Copyright © 2016-2025")]
[assembly: AssemblyDescription("Tests for Property Grid Helper objects")]
[assembly: AssemblyProduct("Property Grid Helpers Test")]
[assembly: AssemblyTitle("PropertyGridHelpersTest")]
[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.MainAssembly)]
#if NET8_0_OR_GREATER
[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
#if NET462 || NET472 || NET48 || NET5_0_OR_GREATER
[assembly: AssemblyMetadata("Verify.ProjectDirectory", "C:\\PropertyGridHelpers\\Code\\PropertyGridHelpers\\")]
[assembly: AssemblyMetadata("Verify.SolutionDirectory", "C:\\PropertyGridHelpers\\Code\\PropertyGridHelpers\\")]
#endif
