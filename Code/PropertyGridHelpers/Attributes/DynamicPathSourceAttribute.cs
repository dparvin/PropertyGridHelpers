using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies that another property dynamically provides the path to the resources 
    /// used to display this property in the PropertyGrid.
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Typically used for properties of an Enum type in a PropertyGrid, where the enum values 
    /// are shown in a dropdown and mapped to resources that provide user-friendly display text.
    /// The referenced property contains the path to these resources, allowing the user of the
    /// property to specify which resource set to use for what is displayed.
    /// </remarks>
    /// <example>
    /// <code>
    /// [DynamicPathSource(nameof(ResourcePath))]
    /// public MyEnum DisplayProperty { get; set; }
    /// 
    /// public string ResourcePath { get; set; } = "MyNamespace.Resources";
    /// </code>
    /// </example>
    /// <param name="pathPropertyName">The name of the property containing the resource path.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DynamicPathSourceAttribute(string pathPropertyName) : Attribute
#else
    /// <summary>
    /// Specifies that another property dynamically provides the path to the resources 
    /// used to display this property in the PropertyGrid.
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Typically used for properties of an Enum type in a PropertyGrid, where the enum values 
    /// are shown in a dropdown and mapped to resources that provide user-friendly display text.
    /// The referenced property contains the path to these resources, allowing the user of the
    /// property to specify which resource set to use for what is displayed.
    /// </remarks>
    /// <example>
    /// <code>
    /// [DynamicPathSource(nameof(ResourcePath))]
    /// public MyEnum DisplayProperty { get; set; }
    /// 
    /// public string ResourcePath { get; set; } = "MyNamespace.Resources";
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DynamicPathSourceAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the name of the path property.
        /// </summary>
        /// <value>
        /// The name of the path property.
        /// </value>
        public string PathPropertyName
        {
            get;
#if NET8_0_OR_GREATER
        } = pathPropertyName;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicPathSourceAttribute"/> class.
        /// </summary>
        /// <param name="pathPropertyName">Name of the path property.</param>
        public DynamicPathSourceAttribute(string pathPropertyName) => PathPropertyName = pathPropertyName;
#endif

        /// <summary>
        /// Gets the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static DynamicPathSourceAttribute Get(ITypeDescriptorContext context) =>
            context == null || context.Instance == null || context.PropertyDescriptor == null
                ? null
                : Support.Support.GetFirstCustomAttribute<DynamicPathSourceAttribute>(
                    Support.Support.GetPropertyInfo(context));
    }
}
