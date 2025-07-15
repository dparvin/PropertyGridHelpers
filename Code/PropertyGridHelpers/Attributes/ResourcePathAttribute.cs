using PropertyGridHelpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies the location of a resource type (e.g., strings, images, cursors) to be used 
    /// in conjunction with UI editors, type converters, or design-time attributes.
    /// </summary>
    /// <remarks>
    /// This attribute can be applied to properties, enum types, or classes to associate them with
    /// a strongly-typed resource class (e.g., <c>Properties.Resources</c>).
    /// 
    /// The optional <see cref="ResourceUsage" /> flag allows specifying what the path is used for,
    /// enabling separation of string resources, image resources, and more.
    ///
    /// If multiple <see cref="ResourcePathAttribute"/>s are applied, the resolution order is:
    /// <list type="number">
    /// <item>Property-level attributes</item>
    /// <item>Enum type or property type-level attributes</item>
    /// <item>Class-level attributes (declaring type)</item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Example 1: Use for enum text
    /// <code>
    /// [ResourcePath("MyNamespace.MyEnumResources", resourceUsage: ResourceUsage.Strings)]
    /// public enum ButtonType
    /// {
    ///     [EnumText("Primary")]
    ///     Primary,
    ///     [EnumText("Secondary")]
    ///     Secondary
    /// }
    /// </code>
    ///
    /// Example 2: Separate image path for UI editor
    /// <code>
    /// [ResourcePath("MyNamespace.ButtonImages", resourceUsage: ResourceUsage.Images)]
    /// public enum ButtonType { ... }
    /// </code>
    /// </example>
    /// <param name="resourcePath">The fully qualified name of the resource class (e.g., <c>"MyApp.Resources.MyStrings"</c>).</param>
    /// <param name="resourceAssembly">
    /// Optional assembly name containing the resource. If null, defaults to the calling assembly.
    /// </param>
    /// <param name="resourceUsage">
    /// Optional usage flag to indicate what the resource is used for (e.g., strings, images).
    /// Defaults to <see cref="ResourceUsage.All"/>.
    /// </param>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = true)]
    public class ResourcePathAttribute(string resourcePath, string resourceAssembly = null, ResourceUsage resourceUsage = ResourceUsage.All) : Attribute
#else
    /// <summary>
    /// Specifies the location of a resource type (e.g., strings, images, cursors) to be used 
    /// in conjunction with UI editors, type converters, or design-time attributes.
    /// </summary>
    /// <remarks>
    /// This attribute can be applied to properties, enum types, or classes to associate them with
    /// a strongly-typed resource class (e.g., <c>Properties.Resources</c>).
    /// 
    /// The optional <see cref="ResourceUsage" /> flag allows specifying what the path is used for,
    /// enabling separation of string resources, image resources, and more.
    ///
    /// If multiple <see cref="ResourcePathAttribute"/>s are applied, the resolution order is:
    /// <list type="number">
    /// <item>Property-level attributes</item>
    /// <item>Enum type or property type-level attributes</item>
    /// <item>Class-level attributes (declaring type)</item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Example 1: Use for enum text
    /// <code>
    /// [ResourcePath("MyNamespace.MyEnumResources", resourceUsage: ResourceUsage.Strings)]
    /// public enum ButtonType
    /// {
    ///     [EnumText("Primary")]
    ///     Primary,
    ///     [EnumText("Secondary")]
    ///     Secondary
    /// }
    /// </code>
    ///
    /// Example 2: Separate image path for UI editor
    /// <code>
    /// [ResourcePath("MyNamespace.ButtonImages", resourceUsage: ResourceUsage.Images)]
    /// public enum ButtonType { ... }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = true)]
    public class ResourcePathAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the resource path associated with the property or enum value.
        /// </summary>
        /// <value>
        /// A string representing the resource path.
        /// </value>
        public string ResourcePath
        {
            get;
#if NET8_0_OR_GREATER
        } = resourcePath;
#else
        }
#endif

        /// <summary>
        /// Gets the resource assembly.
        /// </summary>
        /// <value>
        /// The resource assembly.
        /// </value>
        public string ResourceAssembly
        {
            get;
#if NET8_0_OR_GREATER
        } = resourceAssembly;
#else
        }
#endif

        /// <summary>
        /// Gets the intended usage for the resource path (e.g., <see cref="ResourceUsage.Strings"/> or <see cref="ResourceUsage.Images"/>).
        /// Used to filter multiple attributes for different purposes.
        /// </summary>
        public ResourceUsage ResourceUsage
        {
            get;
#if NET8_0_OR_GREATER
        } = resourceUsage;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePathAttribute"/> class,
        /// specifying the path, optional assembly name, and usage.
        /// </summary>
        /// <param name="resourcePath">The fully qualified name of the resource class (e.g., <c>"MyApp.Resources.MyStrings"</c>).</param>
        /// <param name="resourceAssembly">
        /// Optional assembly name containing the resource. If null, defaults to the calling assembly.
        /// </param>
        /// <param name="resourceUsage">
        /// Optional usage flag to indicate what the resource is used for (e.g., strings, images).
        /// Defaults to <see cref="ResourceUsage.All"/>.
        /// </param>
        public ResourcePathAttribute(string resourcePath, string resourceAssembly = null, ResourceUsage resourceUsage = ResourceUsage.All)
        {
            ResourcePath = resourcePath;
            ResourceAssembly = resourceAssembly;
            ResourceUsage = resourceUsage;
        }
#endif

        /// <summary>
        /// Resolves the assembly object from the stored assembly name.
        /// </summary>
        public Assembly GetAssembly() => string.IsNullOrEmpty(ResourceAssembly) ?
                                         Assembly.GetCallingAssembly() :
                                         Assembly.Load(ResourceAssembly);

        /// <summary>
        /// Resolves the most appropriate <see cref="ResourcePathAttribute"/> for the given context and usage.
        /// </summary>
        /// <param name="context">The descriptor context containing the property and instance metadata.</param>
        /// <param name="resourceUsage">The type of resource being requested (e.g., strings, images).</param>
        /// <returns>
        /// The best-matching <see cref="ResourcePathAttribute"/>, or <c>null</c> if none found.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="resourceUsage"/> is <see cref="ResourceUsage.None"/>.</exception>
        /// <remarks>
        /// The method checks the following in order:
        /// <list type="number">
        /// <item>Property-level attributes on <paramref name="context.PropertyDescriptor"/></item>
        /// <item>Type-level attributes on <paramref name="context.Instance.GetType()"/></item>
        /// </list>
        /// </remarks>
        public static ResourcePathAttribute Get(ITypeDescriptorContext context, ResourceUsage resourceUsage = ResourceUsage.All)
        {
            if (context == null)
                return null;

            if (resourceUsage == ResourceUsage.None)
                throw new ArgumentException("resourceUsage must not be None", nameof(resourceUsage));

#if NET5_0_OR_GREATER
            List<ResourcePathAttribute> attributes = [];
#else
            var attributes = new List<ResourcePathAttribute>();
#endif

            // Resolution order: Property → type → Declaring class
            // This ensures the most specific source wins, falling back as needed

            // 1. Look for attribute on the property → type
            if (context.PropertyDescriptor != null)
            {
                var props = context.PropertyDescriptor.Attributes[typeof(ResourcePathAttribute)];
                if (props is ResourcePathAttribute singleAttr)
                    attributes.Add(singleAttr);
                else if (context.PropertyDescriptor.Attributes is AttributeCollection col)
                    attributes.AddRange(col.OfType<ResourcePathAttribute>());
            }

            // 2. Check the instance type
            if (context.Instance != null)
            {
                var typeAttrs = context.Instance.GetType().GetCustomAttributes(typeof(ResourcePathAttribute), true);
                attributes.AddRange(typeAttrs.OfType<ResourcePathAttribute>());
            }

            // 3. Select best match: Prefer exact usage match, then fallback to All
#if NET35
            return attributes.FirstOrDefault(attr => (attr.ResourceUsage & resourceUsage) != ResourceUsage.None);
#else
            return attributes.FirstOrDefault(attr => attr.ResourceUsage.HasFlag(resourceUsage));
#endif
        }
    }
}
