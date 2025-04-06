using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Base class for localized text attributes.
    /// </summary>
    /// <param name="resourceKey">The resource key.</param>
    /// <seealso cref="Attribute" />
    public abstract class LocalizedTextAttribute(string resourceKey) : Attribute
#else
    /// <summary>
    /// Base class for localized text attributes.
    /// </summary>
    /// <seealso cref="Attribute" />
    public abstract class LocalizedTextAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the resource key.
        /// </summary>
        /// <value>
        /// The resource key.
        /// </value>
        public string ResourceKey
        {
            get;
#if NET8_0_OR_GREATER
        } = resourceKey;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedTextAttribute"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        protected LocalizedTextAttribute(string resourceKey) =>
            ResourceKey = resourceKey;
#endif

        /// <summary>
        /// Gets the localized text.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public string GetLocalizedText(Type targetType)
        {
            // Retrieve the ResourcePathAttribute from the target type
            var resourcePathAttr = (ResourcePathAttribute)GetCustomAttribute(targetType, typeof(ResourcePathAttribute)) ?? throw new InvalidOperationException($"ResourcePathAttribute not found on {targetType.FullName}");

            Console.WriteLine($"ResourcePath: {resourcePathAttr.ResourcePath}");

            // Get the namespace of the target type itself
            // Extract the root namespace (first segment of the namespace)
            var rootNamespace = targetType.Namespace.Split('.')[0];

            // Combine the assembly namespace with the resource path
            var fullResourcePath = $"{rootNamespace}.{resourcePathAttr.ResourcePath}";
            Console.WriteLine($"Computed ResourcePath: {fullResourcePath}");

            // Determine the resource source type dynamically
            var resourceSource = targetType.Assembly.GetType(fullResourcePath) // Convert path to a Type
                 ?? throw new InvalidOperationException($"Resource type '{fullResourcePath}' not found in assembly '{rootNamespace}'");

            // Call your existing method to get the localized string
            return Support.Support.GetResourceString(ResourceKey, resourceSource);
        }
    }
}
