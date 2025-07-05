using System.Collections.Generic;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Provides a contract for extracting resource base names from a set of
    /// embedded resource names within an assembly.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are responsible for parsing
    /// the full resource names (for example, <c>MyApp.Resources.MyStrings.resources</c>)
    /// and returning their logical base names suitable for use with
    /// resource managers or localization tools.
    /// </remarks>
    internal interface IResourceBaseNameExtractor
    {
        /// <summary>
        /// Extracts the base names from the given list of resource names.
        /// </summary>
        /// <param name="assemblyPrefix">
        /// The root namespace or prefix of the assembly, typically matching
        /// <c>Assembly.GetName().Name</c>.
        /// </param>
        /// <param name="resourceNames">
        /// The array of full resource names retrieved from
        /// <see cref="System.Reflection.Assembly.GetManifestResourceNames"/>.
        /// </param>
        /// <returns>
        /// A list of base names with the prefix and standard suffixes removed,
        /// suitable for identifying resources logically.
        /// </returns>
        IList<string> ExtractBaseNames(string assemblyPrefix, string[] resourceNames);
    }
}
