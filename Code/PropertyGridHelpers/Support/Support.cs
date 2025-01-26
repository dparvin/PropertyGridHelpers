using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Support Functions
    /// </summary>
    public static class Support
    {
        /// <summary>
        /// Gets the resources names.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">enumType</exception>
        /// <exception cref="ArgumentException">The provided type must be an enum. - enumType</exception>
        public static string[] GetResourcesNames(Type enumType)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(enumType);
#else
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));
#endif

            if (!enumType.IsEnum)
                throw new ArgumentException("The provided type must be an enum.", nameof(enumType));

            // Get the assembly where the enum type is declared
            var assembly = enumType.Assembly;

            // Get all embedded resources in the assembly
            var resourceNames = assembly.GetManifestResourceNames();

            return resourceNames;
        }

        /// <summary>
        /// Checks the type of the resource.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <exception cref="ArgumentNullException">assembly</exception>
        public static void CheckResourceType(Assembly assembly)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(assembly);
#else
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
#endif

            Console.WriteLine("Checking resources in assembly:");

            // Check for embedded resources
            var embeddedResources = assembly.GetManifestResourceNames();
            Console.WriteLine("\nEmbedded Resources:");
            foreach (var resource in embeddedResources)
            {
                Console.WriteLine($"- {resource} (Embedded Resource)");
            }

            // Check for resource files (.resx compiled to .resources)
            Console.WriteLine("\nResource Files:");
            foreach (var resourceName in embeddedResources.Where(r => r.EndsWith(".resources", StringComparison.InvariantCulture)))
            {
                Console.WriteLine($"- {resourceName} (Resource File)");
#if NET5_0_OR_GREATER
                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream != null)
                {
                    using var reader = new System.Resources.Extensions.DeserializingResourceReader(stream);
                    foreach (DictionaryEntry entry in reader)
                        Console.WriteLine($"  -> {entry.Key}: {entry.Value.GetType().FullName}");
                }
#else
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                    if (stream != null)
                        using (var reader = new ResourceReader(stream))
                            foreach (DictionaryEntry entry in reader)
                                Console.WriteLine($"  -> {entry.Key}: {entry.Value.GetType().FullName}");
#endif
            }
        }

        /// <summary>
        /// Gets the resource string from a resource file.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceSource">The resource source.</param>
        /// <returns>
        /// Returns the text for the current culture from the resource source if it exists,
        /// otherwise it returns the resource key.
        /// </returns>
        public static string GetResourceString(string resourceKey, Type resourceSource)
        {
            var resourceManager = new ResourceManager(resourceSource);
            return resourceManager.GetString(resourceKey, CultureInfo.CurrentCulture) ?? resourceKey;
        }
    }
}
