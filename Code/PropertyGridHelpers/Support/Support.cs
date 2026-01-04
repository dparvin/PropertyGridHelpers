using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Enums;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
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
        /// Returns a string array containing the names of all resources in  the assembly where the passed in enum type
        /// is located.
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
        /// Analyzes the resources embedded within a given assembly and prints details  about their type and structure.
        /// </summary>
        /// <param name="assembly">The assembly to inspect for embedded resources.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="assembly"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This method examines the specified <paramref name="assembly"/> and identifies: 
        /// <list type="bullet">
        /// <item><description>Embedded resources.</description></item>
        /// <item><description>Compiled resource files (<c>.resources</c> files).</description></item>
        /// </list>  
        /// The results are written to the standard output (console).  
        /// <para><b>Example Output:</b></para> 
        /// <pre>
        /// Checking resources in assembly: 
        ///   Embedded Resources: - MyNamespace.MyResource.txt (Embedded Resource)  
        ///   
        /// Resource Files: 
        /// - MyNamespace.Strings.resources (Resource File) 
        /// -> WelcomeMessage: System.String 
        /// -> AppVersion: System.Int32
        /// </pre>  
        /// 
        /// <para>If a compiled resource file (<c>.resources</c>) is found, this method also
        /// attempts to deserialize its contents and print the key-value pairs along with their data types.</para> 
        /// 
        /// <para><b>Note:</b> This method is intended primarily for debugging and inspection purposes. It may not be
        /// suitable for use in production applications.</para>
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
            foreach (var resourceName in embeddedResources.Where(
                r => r.EndsWith(".resources", StringComparison.InvariantCulture)))
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
        /// <param name="culture">The culture.</param>
        /// <param name="resourceSource">The type of the resource class that contains the resource file.</param>
        /// <returns>
        /// The localized string corresponding to <paramref name="resourceKey" /> from the specified <paramref name="resourceSource" /> for the current culture. If the key is not found, the method returns the resource
        /// key itself.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="resourceKey" /> or <paramref name="resourceSource" /> is <c>null</c>.</exception>
        /// <remarks>
        /// This method uses a <see cref="ResourceManager" /> to retrieve the localized string based on the current
        /// culture. If the resource key does not exist in the specified resource file, the method returns the key
        /// itself instead of throwing an exception.
        /// </remarks>
        /// <example>
        /// Example usage: 
        /// <code> 
        /// string message = GetResourceString("WelcomeMessage", typeof(Resources.Messages));
        /// Console.WriteLine(message); // Outputs localized message or "WelcomeMessage" if not found
        /// </code>
        /// </example>
        public static string GetResourceString(string resourceKey, CultureInfo culture, Type resourceSource)
        {
            if (string.IsNullOrEmpty(resourceKey))
                throw new ArgumentNullException(nameof(resourceKey));
#if NET8_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(resourceSource);
#else

            if (resourceSource == null)
                throw new ArgumentNullException(nameof(resourceSource));
#endif

            var resourceManager = new ResourceManager(resourceSource);

            try
            {
                var result = resourceManager.GetString(resourceKey, culture ?? CultureInfo.CurrentCulture);
                if (result == null)
                    Debug.WriteLine($"[Localization] Missing resource key: '{resourceKey}' in {resourceSource.FullName}");

                return result ?? resourceKey;
            }
            catch (MissingManifestResourceException)
            {
                return resourceKey; // Fallback if resource file is missing
            }
        }

        /// <summary>
        /// Determines the resource path based on the specified property or related data type,
        /// filtered by the specified <see cref="ResourceUsage" />.
        /// </summary>
        /// <param name="context">
        /// The type descriptor context, which provides metadata about the property and its container.
        /// </param>
        /// <param name="type">
        /// The data type associated with the resource, typically an enum or a property type.
        /// </param>
        /// <param name="resourceUsage">
        /// The kind of resource to resolve (e.g., <see cref="ResourceUsage.Strings" />,
        /// <see cref="ResourceUsage.Images" />). This helps determine the most appropriate resource path
        /// when multiple paths are defined.
        /// </param>
        /// <param name="throwOnError">
        /// if set to <c>true</c> throw on error when the Dynamic Path Attribute is not setup correctly.
        /// </param>
        /// <returns>
        /// A string containing the resource path based on the provided property or type.
        /// If no applicable attributes are found, the method returns the default resource path:
        /// <c>"Properties.Resources"</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="type" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="resourceUsage" /> is <see cref="ResourceUsage.None" />.
        /// </exception>
        /// <remarks>
        /// This method searches for the resource path using the following order of precedence:
        /// <list type="number">
        /// <item>
        /// If the property has a <see cref="DynamicPathSourceAttribute" /> matching the given
        /// <paramref name="resourceUsage" />, it retrieves the path from the referenced property.
        /// </item>
        /// <item>
        /// If the property or its type has a <see cref="ResourcePathAttribute" /> matching the specified
        /// <paramref name="resourceUsage" />, it uses the defined path.
        /// </item>
        /// <item>
        /// If the type (or its underlying nullable type) is an enumeration and has a matching
        /// <see cref="ResourcePathAttribute" />, it uses the associated path.
        /// </item>
        /// <item>
        /// If none of the above conditions are met, it defaults to <c>"Properties.Resources"</c>.
        /// </item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Example usage with static path:
        /// <code>
        ///     [ResourcePath("Custom.Resources", resourceUsage: ResourceUsage.Strings)]
        ///     public enum MyEnum
        ///     {
        ///         Value1,
        ///         Value2
        ///     }
        ///     
        ///     string path = GetResourcePath(null, typeof(MyEnum), ResourceUsage.Strings);
        ///     Console.WriteLine(path); // Outputs: "Custom.Resources"
        /// </code>
        /// 
        /// Example usage with dynamic path:
        /// <code>
        ///     [DynamicPathSource(nameof(MyResourcePath), ResourceUsage.Images)]
        ///     public MyEnum ImageSelector { get; set; }
        ///     
        ///     public string MyResourcePath =&gt; "Dynamic.Image.Resources";
        /// </code>
        /// </example>
        public static string GetResourcePath(
            ITypeDescriptorContext context,
            Type type,
            ResourceUsage resourceUsage = ResourceUsage.All,
            bool throwOnError = false)
        {
            // Check the context for a dynamic path
            if (context?.Instance != null &&
                context.PropertyDescriptor != null)
            {
                // 1. Check for DynamicPathSourceAttribute for the given usage
                var dynamicAttr = DynamicPathSourceAttribute.Get(context, resourceUsage);
                if (dynamicAttr != null)
                {
                    var sourceProp = context.Instance.GetType().GetProperty(dynamicAttr.PathPropertyName);
                    if (sourceProp == null)
                    {
                        if (throwOnError)
                            throw new MissingMemberException($"The '{dynamicAttr.PathPropertyName}' property is not defined in the '{context.Instance.GetType()}' class.");
                    }
                    else
                    {
                        if (sourceProp.PropertyType == typeof(string))
                            return sourceProp.GetValue(context.Instance, null) as string;
                        else if (throwOnError)
                            throw new InvalidOperationException($"The property '{dynamicAttr.PathPropertyName}' on type '{context.Instance.GetType()}' must be of type string.");
                    }

                }
                // 2. Check for ResourcePathAttribute for the given usage
                var pathAttr = ResourcePathAttribute.Get(context, resourceUsage);
                if (pathAttr != null)
                    return pathAttr.ResourcePath;
            }

            // 3. Check for ResourcePathAttribute on the enum type (if applicable)
            if (type != null)
            {
                var enumType = Nullable.GetUnderlyingType(type) ?? type;
                if (enumType.IsEnum)
                {
                    var enumAttrs = enumType.GetCustomAttributes(typeof(ResourcePathAttribute), true)
                                            .OfType<ResourcePathAttribute>();
                    var enumAttr = enumAttrs.FirstOrDefault(attr => (attr.ResourceUsage & resourceUsage) != 0);
                    if (enumAttr != null)
                        return enumAttr.ResourcePath;
                }
            }

            // 4. Fallback default
            return "Properties.Resources";
        }

        /// <summary>
        /// Retrieves the file extension associated with a property, if specified.
        /// </summary>
        /// <param name="context">
        /// The type descriptor context, which provides metadata about the property and its container.
        /// </param>
        /// <returns>
        /// A string containing the file extension for the resource.  If no valid extension is found, returns an empty
        /// string.
        /// </returns>
        /// <remarks>
        /// This method determines the file extension based on the following order of precedence: 
        /// <list type="number">
        /// <item>Checks if the property has a <see cref="FileExtensionAttribute"/> and retrieves the
        /// value of the property it references.</item>
        /// <item>If the referenced property is a string, its value is returned.</item>
        /// <item>If the referenced property is an enumeration:
        /// <list type="bullet">
        /// <item>Returns the enum's string representation, unless it is <c>None</c>, in which case an empty string is
        /// returned.</item>
        /// <item>If the enum field has an <see cref="EnumTextAttribute"/>, returns its custom text value.</item>
        /// <item>If the enum field has a <see cref="LocalizedEnumTextAttribute"/>, returns its localized
        /// text value.</item>
        /// </list>
        /// </item>
        /// <item>If no matching attributes are found, the method returns an empty string.</item>
        /// </list>
        /// 
        /// Normally a user would not call this method directly, but it is  used by the UIEditors
        /// to load values into the <see cref="PropertyGrid"/>.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the referenced property is not found or is not public.
        /// </exception>
        /// <example>
        /// Example usage: 
        /// <code>
        /// [FileExtension(nameof(FileType))] public string FileName { get; set; } = "example"; 
        /// public string FileType { get; set; } = "xml";  
        /// 
        /// var PropertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(FileName)]; 
        /// var context = new CustomTypeDescriptorContext(PropertyDescriptor, this);  
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
                        (Nullable.GetUnderlyingType(fileExtensionProperty.PropertyType) is Type underlyingType &&
                            underlyingType.IsEnum))
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
        /// Retrieves a required public instance property from the specified object by name.
        /// </summary>
        /// <param name="instance">
        /// The object instance whose property should be retrieved. Must not be <c>null</c>.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property to look up. Case-sensitive.
        /// </param>
        /// <returns>
        /// The <see cref="PropertyInfo"/> representing the requested public property if it exists.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the specified property does not exist on the type of <paramref name="instance"/>,
        /// or if the property exists but is not publicly accessible.
        /// </exception>
        /// <remarks>
        /// This method uses reflection to locate a property by name, including non-public declarations,
        /// but enforces that the property has a public getter.
        /// If the property cannot be found or fails the public visibility requirement,
        /// an <see cref="InvalidOperationException"/> is thrown.
        /// </remarks>
        private static PropertyInfo GetRequiredProperty(object instance, string propertyName)
        {
            var property = instance.GetType()
                    .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ??
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

#if NET35
        /// <summary>
        /// Retrieves the first custom attribute of the specified type applied to the given <see cref="MemberInfo"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of attribute to retrieve. Must derive from <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="member">
        /// The member (e.g., property, method, field, type) to inspect for the attribute.
        /// </param>
        /// <returns>
        /// An instance of the attribute type <typeparamref name="T"/> if one is defined on the member; otherwise, <c>null</c>.
        /// </returns>
        /// <remarks>
        /// this uses <see cref="MemberInfo.GetCustomAttributes(Type, bool)"/> internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="member"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// Example usage:
        /// <code>
        /// var property = typeof(MyClass).GetProperty("MyProperty");
        /// var attr = Support.GetFirstCustomAttribute&lt;AutoCompleteSetupAttribute&gt;(property);
        /// if (attr != null)
        /// {
        ///     // Attribute was found
        /// }
        /// </code>
        /// </example>
#else
        /// <summary>
        /// Retrieves the first custom attribute of the specified type applied to the given <see cref="MemberInfo"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of attribute to retrieve. Must derive from <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="member">
        /// The member (e.g., property, method, field, type) to inspect for the attribute.
        /// </param>
        /// <returns>
        /// An instance of the attribute type <typeparamref name="T"/> if one is defined on the member; otherwise, <c>null</c>.
        /// </returns>
        /// <remarks>
        /// On .NET Framework 3.5, this uses <see cref="MemberInfo.GetCustomAttributes(Type, bool)"/> internally;
        /// on later versions, it uses <see cref="CustomAttributeExtensions.GetCustomAttribute{T}(MemberInfo, bool)"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="member"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// Example usage:
        /// <code>
        /// var property = typeof(MyClass).GetProperty("MyProperty");
        /// var attr = Support.GetFirstCustomAttribute&lt;AutoCompleteSetupAttribute&gt;(property);
        /// if (attr != null)
        /// {
        ///     // Attribute was found
        /// }
        /// </code>
        /// </example>
#endif
        public static T GetFirstCustomAttribute<T>(MemberInfo member) where T : Attribute
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(member);
#else
            if (member == null)
                throw new ArgumentNullException(nameof(member));
#endif
#if NET35
            var attrs = member.GetCustomAttributes(typeof(T), true);
            return attrs.Length > 0 ? (T)attrs[0] : null;
#else
            return member.GetCustomAttribute<T>(true);
#endif
        }

        /// <summary>
        /// Retrieves the <see cref="FieldInfo"/> for a specific enum value.
        /// </summary>
        /// <param name="value">The enum value to inspect.</param>
        /// <returns>
        /// The <see cref="FieldInfo"/> corresponding to the specified enum value, or <c>null</c>
        /// if the value does not match a defined member of the enum type.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static FieldInfo GetEnumField(Enum value)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(value);
#else
            if (value == null)
                throw new ArgumentNullException(nameof(value));
#endif

            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return name == null ? null : type.GetField(name);
        }

        /// <summary>
        /// Retrieves the <see cref="PropertyInfo"/> for the property described by the specified <see
        /// cref="ITypeDescriptorContext"/>.
        /// </summary>
        /// <param name="context">
        /// The type descriptor context containing metadata about the property, including its name and declaring
        /// component type.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyInfo"/> object representing the property described by <paramref name="context"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="context"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="context"/> does not contain a valid <see cref="PropertyDescriptor"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the property described by the context could not be found on the component type.
        /// </exception>
        /// <example>
        /// Example usage: <code> var context = TypeDescriptorContext.Create(typeof(MyClass), "MyProperty");
        /// PropertyInfo info = Support.GetPropertyInfo(context);</code>
        /// </example>
        public static PropertyInfo GetPropertyInfo(ITypeDescriptorContext context)
        {
#if NET5_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(context);
#else
            if (context == null)
                throw new ArgumentNullException(nameof(context));
#endif
            if (context.PropertyDescriptor == null)
                throw new ArgumentException("Context does not contain a valid PropertyDescriptor.", nameof(context));

            var componentType = context.PropertyDescriptor.ComponentType;
            var propertyName = context.PropertyDescriptor.Name;
            var propInfo = componentType.GetProperty(propertyName) ??
                throw new InvalidOperationException(
                    $"Property '{propertyName}' not found on type {componentType.FullName}.");
            return propInfo;
        }

        /// <summary>
        /// Sets the current thread's culture and UI culture to the specified language identifier.
        /// </summary>
        /// <param name="language">
        /// The language code (e.g., <c>"en-US"</c>, <c>"fr-FR"</c>, <c>"de-DE"</c>) to apply to
        /// <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/>.
        /// </param>
        /// <remarks>
        /// This method is intended primarily for testing and debugging localization or globalization scenarios,
        /// allowing the current thread to simulate running under a specific culture.
        /// </remarks>
        public static void SetLanguage(string language)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }
    }
}