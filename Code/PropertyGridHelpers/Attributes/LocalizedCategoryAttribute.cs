using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute for localized category names.
    /// </summary>
    /// <param name="resourceKey">The resource key.</param>
    /// <param name="resourceSource">The resource source.</param>
    /// <seealso cref="CategoryAttribute" />
    public class LocalizedCategoryAttribute(string resourceKey, Type resourceSource) :
        CategoryAttribute(Support.Support.GetResourceString(resourceKey, resourceSource))
    {
    }
#else
    /// <summary>
    /// Attribute for localized category names.
    /// </summary>
    /// <seealso cref="CategoryAttribute" />
    public class LocalizedCategoryAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedCategoryAttribute"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceSource">The resource source.</param>
        public LocalizedCategoryAttribute(string resourceKey, Type resourceSource)
            : base(Support.Support.GetResourceString(resourceKey, resourceSource)) { }
    }
#endif
}
