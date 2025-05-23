﻿using System.Reflection;
using System.Resources;
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
[assembly: AssemblyDescription("PropertyGridHelpers Sample Controls")]
[assembly: AssemblyProduct("Property Grid Helper Sample Controls")]
[assembly: AssemblyTitle("SampleControls")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
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
[assembly: Guid("c6a197d6-0c14-4b95-9301-1bb3399ef2d9")]

#if NET35
#else
[assembly: System.Reflection.AssemblyMetadata("Verify.ProjectDirectory", "C:\\Projects\\Repos\\dparvin\\PropertyGridHelpers\\Code\\SampleControls\\")]
[assembly: System.Reflection.AssemblyMetadata("Verify.SolutionDirectory", "C:\\Projects\\Repos\\dparvin\\PropertyGridHelpers\\Code\\PropertyGridHelpers\\")]
#endif
