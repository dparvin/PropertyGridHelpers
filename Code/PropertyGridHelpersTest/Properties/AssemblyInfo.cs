using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("David Parvin")]
[assembly: AssemblyConfiguration("Debug")]
[assembly: AssemblyCopyright("Copyright © 2016-2025")]
[assembly: AssemblyDescription("Tests for Property Grid Helper objects")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyProduct("Property Grid Helpers Test")]
[assembly: AssemblyTitle("PropertyGridHelpersTest")]
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.MainAssembly)]
#if NET8_0_OR_GREATER
[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif

// Conditionally set AssemblyInformationalVersion for .NET versions that support it
#if !NET35
[assembly: AssemblyInformationalVersion("1.0.0+fbf3d0b3a24694ca8a461a71c6045e2d0cee3558")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
#if NET462 || NET472 || NET48 || NET5_0_OR_GREATER
[assembly: AssemblyMetadata("Verify.ProjectDirectory", "C:\\PropertyGridHelpers\\Code\\PropertyGridHelpers\\")]
[assembly: AssemblyMetadata("Verify.SolutionDirectory", "C:\\PropertyGridHelpers\\Code\\PropertyGridHelpers\\")]
#endif
