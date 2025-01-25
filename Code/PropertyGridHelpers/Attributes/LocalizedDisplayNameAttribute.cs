using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute for localized display names.
    /// </summary>
    /// <param name="resourceKey">The resource key.</param>
    /// <param name="resourceSource">The resource source.</param>
    /// <seealso cref="DisplayNameAttribute" />
    public class LocalizedDisplayNameAttribute(string resourceKey, Type resourceSource) :
        DisplayNameAttribute(Support.Support.GetResourceString(resourceKey, resourceSource))
    {
    }
#else
    /// <summary>
    /// Attribute for localized display names.
    /// </summary>
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="resourceSource">The resource source.</param>
        public LocalizedDisplayNameAttribute(string resourceKey, Type resourceSource)
            : base(Support.Support.GetResourceString(resourceKey, resourceSource)) { }
    }
#endif
}
