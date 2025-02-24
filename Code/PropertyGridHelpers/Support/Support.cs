using PropertyGridHelpers.Attributes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace PropertyGridHelpers.Support
{
    /// <summary>
    /// Functions used to Support the processes provided by the PropertyGridHelpers library.
    /// </summary>
    public static class Support
    {
        /// <summary>
        /// Gets the resources names from the assembly where the enum is located.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns>
        /// Returns a string array containing the names of all resources in 
        /// the assembly where the passed in enum type is located.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the parameter <paramref name="enumType"/> is null</exception>
        /// <exception cref="ArgumentException">Throw when the parameter <paramref name="enumType"/> is not an Enum type</exception>
        /// <!-- IntelliSense Only -->
        /// See <see href="https://github.com/dparvin/PropertyGridHelpers/wiki/73ec243d-2005-9d6b-8d20-bfc12895eec6">PropertyHelpers Wiki</see>.
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
        /// Analyzes the resources embedded within a given assembly and prints details 
        /// about their type and structure.
        /// </summary>
        /// <param name="assembly">The assembly to inspect for embedded resources.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="assembly"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This method examines the specified <paramref name="assembly"/> and identifies:
        /// <list type="bullet">
        ///     <item><description>Embedded resources.</description></item>
        ///     <item><description>Compiled resource files (<c>.resources</c> files).</description></item>
        /// </list>
        ///
        /// The results are written to the standard output (console).
        ///
        /// <para><b>Example Output:</b></para>
        /// <pre>
        /// Checking resources in assembly:
        ///
        /// Embedded Resources:
        /// - MyNamespace.MyResource.txt (Embedded Resource)
        ///
        /// Resource Files:
        /// - MyNamespace.Strings.resources (Resource File)
        ///   -> WelcomeMessage: System.String
        ///   -> AppVersion: System.Int32
        /// </pre>
        ///
        /// <para>If a compiled resource file (<c>.resources</c>) is found, this method also
        /// attempts to deserialize its contents and print the key-value pairs along with
        /// their data types.</para>
        ///
        /// <para><b>Note:</b> This method is intended primarily for debugging and inspection purposes.
        /// It may not be suitable for use in production applications.</para>
        /// </remarks>
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
        /// Retrieves a localized string from a resource file based on the specified resource key.
        /// </summary>
        /// <param name="resourceKey">The key identifying the resource string.</param>
        /// <param name="resourceSource">The type of the resource class that contains the resource file.</param>
        /// <param name="frame">The frame.</param>
        /// <returns>
        /// The localized string corresponding to <paramref name="resourceKey" /> from the specified
        /// <paramref name="resourceSource" /> for the current culture. If the key is not found,
        /// the method returns the resource key itself.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="resourceKey" /> or <paramref name="resourceSource" /> is <c>null</c>.</exception>
        /// <remarks>
        /// This method uses a <see cref="ResourceManager" /> to retrieve the localized string
        /// based on the current culture. If the resource key does not exist in the specified resource file,
        /// the method returns the key itself instead of throwing an exception.
        /// </remarks>
        /// <example>
        /// Example usage:
        /// <code>
        /// string message = GetResourceString("WelcomeMessage", typeof(Resources.Messages));
        /// Console.WriteLine(message); // Outputs localized message or "WelcomeMessage" if not found
        /// </code></example>
        public static string GetResourceString(string resourceKey, Type resourceSource, StackFrame frame)
        {
            ResourceManager resourceManager;
            if (resourceSource == null)
            {
                var resourcePath = string.Empty;
                // Try to determine the member this attribute is applied to
                var resourceAssembly = GetResourceTypeFromCaller(frame, ref resourcePath);
                resourceManager = new ResourceManager(resourcePath, resourceAssembly);
            }
            else
                resourceManager = new ResourceManager(resourceSource);
            try
            {
                return resourceManager.GetString(resourceKey, CultureInfo.CurrentCulture) ?? resourceKey;
            }
            catch (MissingManifestResourceException)
            {
                return resourceKey; // Fallback if resource file is missing
            }
        }

        /// <summary>
        /// Gets the resource type from caller.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns></returns>
        private static Assembly GetResourceTypeFromCaller(StackFrame frame, ref string resourcePath)
        {
            var property = FindPropertyForAttribute(frame);
            if (property == null) return null;
            ResourcePathAttribute resourcePathAttribute = null;
#if NET35
            var resourcePathAttributes = property.GetCustomAttributes(typeof(ResourcePathAttribute), false);
            if (resourcePathAttributes.Length > 0)
                resourcePathAttribute = (ResourcePathAttribute)resourcePathAttributes[0];
#else
            resourcePathAttribute = property.GetCustomAttribute<ResourcePathAttribute>();
#endif
            if (resourcePathAttribute == null) return null;
            var namespacePrefix = property?.DeclaringType.Assembly.GetName().Name;
            resourcePath = !string.IsNullOrEmpty(namespacePrefix) ? $"{namespacePrefix}.{resourcePathAttribute.ResourcePath}" : resourcePathAttribute.ResourcePath;
            return resourcePathAttribute.ResourceAssembly != null ?
                   resourcePathAttribute.GetAssembly() :
                   property.Module.Assembly;
        }

        /// <summary>
        /// Finds the property for attribute.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
        internal static PropertyInfo FindPropertyForAttribute(StackFrame frame)
        {
            if (frame == null) return null;

            var method = frame.GetMethod();
            var declaringType = method?.DeclaringType;
            if (declaringType == null) return null;

            // Scan all properties of the declaring type
            foreach (var property in declaringType.GetProperties())
            {
#if NET35
                var attributes = property.GetCustomAttributes(typeof(ResourcePathAttribute), false);
                if (attributes.Length > 0)
#else
                var attributes = property.GetCustomAttributes<ResourcePathAttribute>();
                if (attributes.Any())
#endif
                    return property;
            }

            return null; // No match found
        }

        /// <summary>
        /// Determines the resource path based on the specified property or related data type.
        /// </summary>
        /// <param name="context">
        /// The type descriptor context, which provides metadata about the property and its container.
        /// </param>
        /// <param name="type">
        /// The data type associated with the resource, typically an enum or a property type.
        /// </param>
        /// <returns>
        /// A string containing the resource path based on the provided property or type.
        /// If no applicable attributes are found, the method returns the default resource path: 
        /// <c>"Properties.Resources"</c>.
        /// </returns>
        /// <remarks>
        /// This method searches for the resource path using the following order of precedence:
        /// <list type="number">
        ///   <item>If the property has a <see cref="DynamicPathSourceAttribute"/>, it retrieves the path 
        ///         from the referenced property specified in the attribute.</item>
        ///   <item>If the property has a <see cref="ResourcePathAttribute"/>, it uses the specified path.</item>
        ///   <item>If the type (or its underlying nullable type) is an enumeration and has a 
        ///         <see cref="ResourcePathAttribute"/>, it uses the path defined by the attribute.</item>
        ///   <item>If none of the above conditions are met, it defaults to <c>"Properties.Resources"</c>.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Example usage:
        /// <code>
        /// [ResourcePath("Custom.Resources")]
        /// public enum MyEnum { Value1, Value2 }
        ///
        /// string path = GetResourcePath(null, typeof(MyEnum));
        /// Console.WriteLine(path); // Outputs: "Custom.Resources"
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="type"/> is <c>null</c>.
        /// </exception>
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
        /// Retrieves the file extension associated with a property, if specified.
        /// </summary>
        /// <param name="context">
        /// The type descriptor context, which provides metadata about the property and its container.
        /// </param>
        /// <returns>
        /// A string containing the file extension for the resource. 
        /// If no valid extension is found, returns an empty string.
        /// </returns>
        /// <remarks>
        /// This method determines the file extension based on the following order of precedence:
        /// <list type="number">
        ///   <item>Checks if the property has a <see cref="FileExtensionAttribute"/> and retrieves the value 
        ///         of the property it references.</item>
        ///   <item>If the referenced property is a string, its value is returned.</item>
        ///   <item>If the referenced property is an enumeration:
        ///     <list type="bullet">
        ///       <item>Returns the enum's string representation, unless it is <c>None</c>, in which case an empty string is returned.</item>
        ///       <item>If the enum field has an <see cref="EnumTextAttribute"/>, returns its custom text value.</item>
        ///       <item>If the enum field has a <see cref="LocalizedEnumTextAttribute"/>, returns its localized text value.</item>
        ///     </list>
        ///   </item>
        ///   <item>If no matching attributes are found, the method returns an empty string.</item>
        /// </list>
        /// 
        /// Normally a user would not call this method directly, but it is 
        /// used by the UIEditors to load values into the <see cref="PropertyGrid" />.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the referenced property is not found or is not public.
        /// </exception>
        /// <example>
        /// Example usage:
        /// <code>
        /// [FileExtension(nameof(FileType))]
        /// public string FileName { get; set; } = "example";
        /// 
        /// public string FileType { get; set; } = "xml";
        ///
        /// var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(FileName)];
        /// var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);
        /// 
        /// string extension = GetFileExtension(context);
        /// Console.WriteLine(extension); // Outputs: "xml"
        /// </code>
        /// </example>
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
