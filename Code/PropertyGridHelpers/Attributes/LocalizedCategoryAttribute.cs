using System;
using System.ComponentModel;
using System.Diagnostics;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute for specifying a localized category name for a property or event.
    /// </summary>
    /// <param name="resourceKey">The key used to retrieve the localized category name from the resource file.</param>
    /// <param name="resourceSource">The type that contains the resource manager for retrieving localized strings.</param>
    /// <remarks>
    /// This attribute allows category names displayed in property grids to be localized
    /// by retrieving the category name from a resource file.
    /// </remarks>
    /// <seealso cref="CategoryAttribute" />
    public class LocalizedCategoryAttribute(string resourceKey, Type resourceSource = null) :
        CategoryAttribute(Support.Support.GetResourceString(resourceKey, resourceSource, new StackTrace().GetFrame(1)))
    {
        /// <summary>
        /// Gets the resource key used to retrieve the localized category name.
        /// </summary>
        public string ResourceKey { get; } = resourceKey;
    }
#else
    /// <summary>
    /// Attribute for specifying a localized category name for a property or event.
    /// </summary>
    /// <remarks>
    /// This attribute allows category names displayed in property grids to be localized
    /// by retrieving the category name from a resource file.
    /// </remarks>
    /// <seealso cref="CategoryAttribute" />
    public class LocalizedCategoryAttribute : CategoryAttribute
    {
        /// <summary>
        /// Gets the resource key used to retrieve the localized category name.
        /// </summary>
        public string ResourceKey
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedCategoryAttribute"/> class.
        /// </summary>
        /// <param name="resourceKey">The key used to retrieve the localized category name from the resource file.</param>
        /// <param name="resourceSource">The type that contains the resource manager for retrieving localized strings.</param>
        /// <remarks>
        /// The constructor fetches the localized category name using the specified resource key and resource source.
        /// </remarks>
        public LocalizedCategoryAttribute(string resourceKey, Type resourceSource = null)
            : base(Support.Support.GetResourceString(resourceKey, resourceSource, new StackTrace().GetFrame(1))) =>
            ResourceKey = resourceKey;
    }
#endif
}
