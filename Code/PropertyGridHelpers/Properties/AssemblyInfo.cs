using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyCompany("David Parvin")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCopyright("Copyright © 2016-2025")]
[assembly: AssemblyDescription("Property Grid Helper objects")]
[assembly: AssemblyProduct("Property Grid Helpers")]
[assembly: AssemblyTitle("PropertyGridHelpers")]
[assembly: AssemblyFileVersion("1.0.0.1")]
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

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("21830e27-425e-459e-b77d-38f3d1959cee")]

[assembly: InternalsVisibleTo("PropertyGridHelpersTest, PublicKey=0024000004800000940000000602000000240000525341310004000001000100fd7f9a5f5b28059ab64eed79a2fc23f4c211d91f79dc2ebf656ffc4f26fff2b6d1bdaf04f62f2f6ad05f0a3ff6fb4185d087c64dfa0c58a9f398eb6e98312279ae908a41f2e6b4745f5e87dafb040d57fec739ed007a92edcbfae17c0400fe155a32943898f5ad1b879c59aff530d7766d3e59641d0e80ace360ba47a32f60b7")]
