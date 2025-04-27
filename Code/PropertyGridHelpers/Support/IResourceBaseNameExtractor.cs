using System.Collections.Generic;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Interface for extracting resource base names.
    /// </summary>
    internal interface IResourceBaseNameExtractor
    {
        /// <summary>
        /// Extracts the base names.
        /// </summary>
        /// <param name="assemblyPrefix">The assembly prefix.</param>
        /// <param name="resourceNames">The resource names.</param>
        /// <returns></returns>
        IList<string> ExtractBaseNames(string assemblyPrefix, string[] resourceNames);
    }
}
