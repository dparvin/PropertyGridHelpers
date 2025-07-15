using PropertyGridHelpers.Enums;
using System;
using System.ComponentModel;
using System.Linq;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies that the resource path used to localize or visualize this property should be
    /// dynamically retrieved from another property on the same object.
    /// </summary>
    /// <param name="pathPropertyName">
    /// The name of the property (on the same class) that returns a string resource path.
    /// </param>
    /// <param name="resourceUsage">
    /// The kind of resource this path will be used for. Defaults to <see cref="ResourceUsage.All"/>.
    /// </param>
    /// <remarks>
    /// This attribute is useful for making the resource path configurable at runtime or design time,
    /// especially when dealing with localized enum values, icons, or other resource-driven behaviors.
    ///
    /// You can apply multiple <see cref="DynamicPathSourceAttribute"/> instances to a single property,
    /// each with a different <see cref="ResourceUsage"/> value to target specific resource types (e.g. strings vs. images).
    ///
    /// Resolution logic will select the first matching path based on the requested <paramref name="resourceUsage"/>.
    /// </remarks>
    /// <example>
    /// Example 1: Dynamic string resource binding for enum display
    /// <code>
    /// [DynamicPathSource(nameof(MyResourcePath), ResourceUsage.Strings)]
    /// public MyEnum DisplayOption { get; set; }
    ///
    /// public string MyResourcePath => "MyApp.Strings.EnumResources";
    /// </code>
    ///
    /// Example 2: Separate image path for visual enum dropdown
    /// <code>
    /// [DynamicPathSource(nameof(ImagePath), ResourceUsage.Images)]
    /// public MyEnum IconChoice { get; set; }
    ///
    /// public string ImagePath => "MyApp.Images.Icons";
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DynamicPathSourceAttribute(string pathPropertyName, ResourceUsage resourceUsage = ResourceUsage.All) : Attribute
#else
    /// <summary>
    /// Specifies that the resource path used to localize or visualize this property should be
    /// dynamically retrieved from another property on the same object.
    /// </summary>
    /// <remarks>
    /// This attribute is useful for making the resource path configurable at runtime or design time,
    /// especially when dealing with localized enum values, icons, or other resource-driven behaviors.
    ///
    /// You can apply multiple <see cref="DynamicPathSourceAttribute"/> instances to a single property,
    /// each with a different <see cref="ResourceUsage"/> value to target specific resource types (e.g. strings vs. images).
    ///
    /// Resolution logic will select the first matching path based on the requested resourceUsage.
    /// </remarks>
    /// <example>
    /// Example 1: Dynamic string resource binding for enum display
    /// <code>
    /// [DynamicPathSource(nameof(MyResourcePath), ResourceUsage.Strings)]
    /// public MyEnum DisplayOption { get; set; }
    ///
    /// public string MyResourcePath => "MyApp.Strings.EnumResources";
    /// </code>
    ///
    /// Example 2: Separate image path for visual enum dropdown
    /// <code>
    /// [DynamicPathSource(nameof(ImagePath), ResourceUsage.Images)]
    /// public MyEnum IconChoice { get; set; }
    ///
    /// public string ImagePath => "MyApp.Images.Icons";
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DynamicPathSourceAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the name of the property that contains the resource path to use for this context.
        /// The referenced property should return a string representing a fully qualified resource class name
        /// (e.g., <c>"MyApp.Resources.MyStrings"</c>).
        /// </summary>
        public string PathPropertyName
        {
            get;
#if NET8_0_OR_GREATER
        } = pathPropertyName;
#else
        }
#endif

        /// <summary>
        /// Indicates what type of resource this path is intended for (e.g., <see cref="ResourceUsage.Strings"/>,
        /// <see cref="ResourceUsage.Images"/>). Used to filter when multiple attributes are present.
        /// </summary>
        public ResourceUsage ResourceUsage
        {
            get;
#if NET8_0_OR_GREATER
        } = resourceUsage;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicPathSourceAttribute"/> class.
        /// </summary>
        /// <param name="pathPropertyName">
        /// The name of the property (on the same class) that returns a string resource path.
        /// </param>
        /// <param name="resourceUsage">
        /// The kind of resource this path will be used for. Defaults to <see cref="ResourceUsage.All"/>.
        /// </param>
        public DynamicPathSourceAttribute(string pathPropertyName, ResourceUsage resourceUsage = ResourceUsage.All)
        {
            PathPropertyName = pathPropertyName;
            ResourceUsage = resourceUsage;
        }
#endif

        /// <summary>
        /// Resolves the most appropriate <see cref="DynamicPathSourceAttribute"/> for the given context and usage.
        /// </summary>
        /// <param name="context">The context containing property and instance metadata.</param>
        /// <param name="resourceUsage">
        /// The intended type of resource (e.g., strings, images) to retrieve the matching attribute.
        /// </param>
        /// <returns>
        /// The best-matching <see cref="DynamicPathSourceAttribute"/>, or <c>null</c> if none found.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="resourceUsage"/> is <see cref="ResourceUsage.None"/>.
        /// </exception>
        public static DynamicPathSourceAttribute Get(ITypeDescriptorContext context, ResourceUsage resourceUsage = ResourceUsage.All)
        {
            if (context?.Instance == null || context.PropertyDescriptor == null)
                return null;

            if (resourceUsage == ResourceUsage.None)
                throw new ArgumentException("resourceUsage must not be None", nameof(resourceUsage));

            var property = Support.Support.GetPropertyInfo(context);
            var all = GetCustomAttributes(property, typeof(DynamicPathSourceAttribute))
                               .OfType<DynamicPathSourceAttribute>();

#if NET35
            return all.FirstOrDefault(a => (a.ResourceUsage & resourceUsage) != ResourceUsage.None);
#else
            return all.FirstOrDefault(a => a.ResourceUsage.HasFlag(resourceUsage));
#endif
        }
    }
}
