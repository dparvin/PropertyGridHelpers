#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Provides logic to extract base names from resource names, removing a specified assembly prefix and 
    /// the standard <c>.resources</c> extension.
    /// </summary>
    /// <remarks>
    /// This implementation is useful when working with resource files that are embedded with fully qualified names,
    /// allowing you to obtain simpler base names for further processing.
    /// </remarks>
    /// <seealso cref="IResourceBaseNameExtractor"/>
    internal class RangeBasedBaseNameExtractor : IResourceBaseNameExtractor
    {
        /// <summary>
        /// Extracts base names from the specified resource names, removing the standard <c>.resources</c> 
        /// extension and stripping the provided assembly prefix if present.
        /// </summary>
        /// <param name="assemblyPrefix">
        /// The assembly prefix to remove from the beginning of resource names (e.g., the default namespace 
        /// of the assembly). If the base name starts with this prefix, it will be removed along with any 
        /// subsequent dot separator.
        /// </param>
        /// <param name="resourceNames">
        /// The full resource names to process, typically retrieved via <see cref="System.Reflection.Assembly.GetManifestResourceNames"/>.
        /// </param>
        /// <returns>
        /// An ordered list of distinct base names with the <c>.resources</c> extension and the assembly prefix removed.
        /// </returns>
        public IList<string> ExtractBaseNames(string assemblyPrefix, string[] resourceNames)
        {
            const string resourceExtension = ".resources";
            var resourceExtensionLength = resourceExtension.Length;
            var baseNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var name in resourceNames)
            {
                if (name.EndsWith(resourceExtension, StringComparison.OrdinalIgnoreCase))
                {
                    var baseName = name[..^resourceExtensionLength];

                    if (baseName.StartsWith(assemblyPrefix, StringComparison.OrdinalIgnoreCase))
                        baseName = baseName[assemblyPrefix.Length..].TrimStart('.');

                    _ = baseNames.Add(baseName);
                }
            }

            return [.. baseNames.OrderBy(x => x, StringComparer.OrdinalIgnoreCase)];
        }
    }
}
#endif
