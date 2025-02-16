using PropertyGridHelpers.Attributes;
using System;
using System.Collections;
using System.ComponentModel;
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

        /// <summary>
        /// Gets the resource path.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <remarks>
        /// This routine looks for a <see cref="DynamicPathSourceAttribute"/> on 
        /// the property or a <see cref="ResourcePathAttribute"/> on the property 
        /// or enum type.
        /// </remarks>
        public static string GetResourcePath(ITypeDescriptorContext context, Type type)
        {
            // Check the context for a dynamic path
            if (context?.Instance != null && context.PropertyDescriptor != null)
            {
                // Check if the property has the DynamicPathSourceAttribute
                var propertyInfo = context.Instance.GetType().GetProperty(context.PropertyDescriptor.Name);
                if (Attribute.GetCustomAttribute(propertyInfo, typeof(DynamicPathSourceAttribute)) is DynamicPathSourceAttribute dynamicPathAttr)
                {
                    // Find the referenced property
                    var pathProperty = context.Instance.GetType().GetProperty(dynamicPathAttr.PathPropertyName);
                    if (pathProperty.PropertyType == typeof(string))
                        // Return the value of the referenced property
                        return pathProperty.GetValue(context.Instance, null) as string;
                }
            }

            // Check PropertyDescriptor for ResourcePathAttribute (only directly applied attributes)
            if (context?.Instance != null && context.PropertyDescriptor != null)
            {
                var propertyInfo = context.Instance.GetType().GetProperty(context.PropertyDescriptor.Name);
                if (propertyInfo != null)
                {
                    // Get attributes directly applied to the property
                    var propertyAttributes = Attribute.GetCustomAttributes(propertyInfo, typeof(ResourcePathAttribute));
                    if (propertyAttributes.FirstOrDefault() is ResourcePathAttribute directPropertyAttribute)
                        return directPropertyAttribute.ResourcePath;
                }
            }

            // Check if the type is an enum
            if (type.IsEnum ||
                (Nullable.GetUnderlyingType(type) is Type underlyingType && underlyingType.IsEnum))
            {
                // Determine the actual enum type to inspect for the attribute.
                var enumType = type.IsEnum ? type : Nullable.GetUnderlyingType(type);

                // Check for ResourcePathAttribute on the enum type
                if (Attribute.GetCustomAttribute(enumType, typeof(ResourcePathAttribute)) is ResourcePathAttribute enumAttribute)
                    return enumAttribute.ResourcePath;
            }
            // Default resource path
            return "Properties.Resources";
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static string GetFileExtension(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                var FileExtensionAttr = FileExtensionAttribute.Get(context);
                if (FileExtensionAttr != null)
                {
                    // Find the referenced property
                    var fileExtensionProperty = GetRequiredProperty(context.Instance, FileExtensionAttr.PropertyName);
                    if (fileExtensionProperty.PropertyType == typeof(string))
                    {
                        // Return the value of the referenced property
                        return fileExtensionProperty.GetValue(context.Instance, null) as string;
                    }
                    else if (fileExtensionProperty.PropertyType.IsEnum ||
                             (Nullable.GetUnderlyingType(fileExtensionProperty.PropertyType) is Type underlyingType && underlyingType.IsEnum))
                    {
                        // Get the property value.
                        var rawValue = fileExtensionProperty.GetValue(context.Instance, null);
                        if (rawValue == null)
                            // If the value is null, return an empty string (or handle as needed).
                            return string.Empty;

                        // At this point rawValue should be an enum value.
                        var extension = (Enum)rawValue;
                        var enumField = extension.GetType().GetField(extension.ToString());
                        if (enumField != null)
                        {
                            var enumTextAttr = enumField.GetCustomAttributes(typeof(EnumTextAttribute), false) as EnumTextAttribute[];
                            if (enumTextAttr.Length > 0)
                                return enumTextAttr[0].EnumText; // Return custom text
                        }
                        // Return the value of the referenced property
                        return (string.IsNullOrEmpty(extension.ToString()) ||
                                string.Equals(extension.ToString(), "None", StringComparison.OrdinalIgnoreCase))
                            ? string.Empty
                            : extension.ToString();
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the required public property.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// The <see cref="PropertyInfo"/> of the property if it exists and is public.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the property is not found or is not public.
        /// </exception>
        private static PropertyInfo GetRequiredProperty(object instance, string propertyName)
        {
            var property = instance.GetType().GetProperty(propertyName, BindingFlags.Instance |
                                                                        BindingFlags.Public |
                                                                        BindingFlags.NonPublic) ??
                           throw new InvalidOperationException(
                    $"Property '{propertyName}' not found on type '{instance.GetType()}'.");

#if NET35
            if (property.GetGetMethod() == null)
#else
            if (!property.GetMethod.IsPublic)
#endif
            {
                throw new InvalidOperationException(
                    $"Property '{propertyName}' on type '{instance.GetType()}' must be public.");
            }

            return property;
        }
    }
}
