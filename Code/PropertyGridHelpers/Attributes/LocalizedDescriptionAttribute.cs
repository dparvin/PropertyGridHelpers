using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a localized description for a property, event, or other member in a class.
    /// </summary>
    /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
    /// <param name="resourceSource">The type of the resource file where the localized string is stored.</param>
    /// <remarks>
    /// This attribute retrieves the description text from a resource file, allowing descriptions to be localized.
    /// Apply this attribute to a member, providing the resource key and the resource source type containing the localization.
    /// </remarks>
    /// <example>
    /// <code>
    /// [LocalizedDescription("PropertyName_Description", typeof(Resources))]
    /// public int PropertyName { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="DescriptionAttribute" />
    public class LocalizedDescriptionAttribute(string resourceKey, Type resourceSource) :
        DescriptionAttribute(Support.Support.GetResourceString(resourceKey, resourceSource))
    {
    }
#else
    /// <summary>
    /// Specifies a localized description for a property, event, or other member in a class.
    /// </summary>
    /// <remarks>
    /// This attribute retrieves the description text from a resource file, allowing descriptions to be localized.
    /// Apply this attribute to a member, providing the resource key and the resource source type containing the localization.
    /// </remarks>
    /// <seealso cref="DescriptionAttribute" />
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
        /// <param name="resourceSource">The type of the resource file where the localized string is stored.</param>
        /// <example>
        /// <code>
        /// [LocalizedDescription("PropertyName_Description", typeof(Resources))]
        /// public int PropertyName { get; set; }
        /// </code>
        /// </example>
        public LocalizedDescriptionAttribute(string resourceKey, Type resourceSource)
            : base(Support.Support.GetResourceString(resourceKey, resourceSource)) { }
    }
#endif
}
