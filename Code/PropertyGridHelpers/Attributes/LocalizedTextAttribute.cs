using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Provides a base attribute class for attaching localized display text
    /// to properties or fields using a resource key.
    /// </summary>
    /// <param name="resourceKey">
    /// The key used to look up the localized string in the resource file.
    /// </param>
    /// <seealso cref="Attribute" />
    public abstract class LocalizedTextAttribute(string resourceKey) : Attribute
#else
    /// <summary>
    /// Provides a base attribute class for attaching localized display text
    /// to properties or fields using a resource key.
    /// </summary>
    /// <seealso cref="Attribute" />
    public abstract class LocalizedTextAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the resource key associated with this attribute.
        /// </summary>
        /// <value>
        /// The resource key to look up in a resource file.
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
        /// <param name="resourceKey">
        /// The key used to look up the localized string in the resource file.
        /// </param>
        protected LocalizedTextAttribute(string resourceKey) =>
                ResourceKey = resourceKey;
#endif

        /// <summary>
        /// Resolves and retrieves the localized text for this attribute using
        /// the provided target type's associated <see cref="ResourcePathAttribute" />.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="targetType">The type whose resource path is used to locate the resource class.</param>
        /// <returns>
        /// The resolved localized string based on the <see cref="ResourceKey" /> and the
        /// target type's resource path.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// ResourcePathAttribute not found on {targetType.FullName}
        /// or
        /// Resource type '{fullResourcePath}' not found in assembly '{rootNamespace}'
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="ResourcePathAttribute" /> is missing or if the
        /// resource type cannot be located in the assembly.</exception>
        public string GetLocalizedText(ITypeDescriptorContext context, CultureInfo culture, Type targetType)
        {
            // Retrieve the ResourcePathAttribute from the target type
            var resourcePath = Support.Support.GetResourcePath(context, targetType, Enums.ResourceUsage.Strings);

            Debug.WriteLine($"ResourcePath: {resourcePath}");

            // Get the namespace of the target type itself
            // Extract the root namespace (first segment of the namespace)
            var rootNamespace = targetType.Namespace.Split('.')[0];

            // Combine the assembly namespace with the resource path
            var fullResourcePath = $"{rootNamespace}.{resourcePath}";
            Console.WriteLine($"Computed ResourcePath: {fullResourcePath}");

            // Determine the resource source type dynamically
            var resourceSource = targetType.Assembly.GetType(fullResourcePath) // Convert path to a Type
                 ?? throw new InvalidOperationException($"Resource type '{fullResourcePath}' not found in assembly '{rootNamespace}'");

            // Call your existing method to get the localized string
            return Support.Support.GetResourceString(ResourceKey, culture, resourceSource);
        }

        /// <summary>
        /// Retrieves the <see cref="LocalizedTextAttribute"/> from the given
        /// type descriptor context, if available.
        /// </summary>
        /// <param name="context">
        /// The design-time or runtime type descriptor context.
        /// </param>
        /// <returns>
        /// The <see cref="LocalizedTextAttribute"/> if found, or <c>null</c> if not present.
        /// </returns>
        public static LocalizedTextAttribute Get(ITypeDescriptorContext context) =>
            context == null || context.Instance == null || context.PropertyDescriptor == null
                ? null
                : Support.Support.GetFirstCustomAttribute<LocalizedTextAttribute>(
                    Support.Support.GetPropertyInfo(context));
    }
}
