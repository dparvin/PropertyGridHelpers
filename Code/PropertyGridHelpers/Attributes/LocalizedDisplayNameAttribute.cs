using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a localized display name for a property, event, or other
    /// member in a class.
    /// </summary>
    /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
    /// <param name="resourceSource">The type of the resource file where the localized string is stored.</param>
    /// <seealso cref="DisplayNameAttribute" />
    /// <remarks>
    /// This attribute retrieves the display name text from a resource file,
    /// allowing names to be localized. Apply this attribute to a member,
    /// providing the resource key and the resource source type containing
    /// the localization.
    /// </remarks>
    /// <example>
    ///   <code>
    /// [LocalizedDisplayName("PropertyName_DisplayName", typeof(Resources))]
    /// public int PropertyName { get; set; }
    ///   </code>
    /// </example>
    public class LocalizedDisplayNameAttribute(string resourceKey, Type resourceSource) :
        DisplayNameAttribute(Support.Support.GetResourceString(resourceKey, resourceSource))
    {
    }
#else
    /// <summary>
    /// Specifies a localized display name for a property, event, or other
    /// member in a class.
    /// </summary>
    /// <seealso cref="DisplayNameAttribute" />
    /// <remarks>
    /// This attribute retrieves the display name text from a resource file,
    /// allowing names to be localized. Apply this attribute to a member,
    /// providing the resource key and the resource source type containing
    /// the localization.
    /// </remarks>
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
        /// <param name="resourceSource">The type of the resource file where the localized string is stored.</param>
        /// <example>
        ///   <code>
        /// [LocalizedDisplayName("PropertyName_DisplayName", typeof(Resources))]
        /// public int PropertyName { get; set; }
        ///   </code>
        /// </example>
        public LocalizedDisplayNameAttribute(string resourceKey, Type resourceSource)
            : base(Support.Support.GetResourceString(resourceKey, resourceSource)) { }
    }
#endif
}
