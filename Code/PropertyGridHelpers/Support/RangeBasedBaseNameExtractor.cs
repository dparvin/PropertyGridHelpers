﻿#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Extracts the base names of resources from a given assembly prefix and resource names.
    /// </summary>
    /// <seealso cref="IResourceBaseNameExtractor" />
    internal class RangeBasedBaseNameExtractor : IResourceBaseNameExtractor
    {
        /// <summary>
        /// Extracts the base names.
        /// </summary>
        /// <param name="assemblyPrefix">The assembly prefix.</param>
        /// <param name="resourceNames">The resource names.</param>
        /// <returns></returns>
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
